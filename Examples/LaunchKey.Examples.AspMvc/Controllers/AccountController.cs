using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LaunchKey.Examples.AspMvc.Models;
using LaunchKey.SDK.Rest;
using System.Configuration;
using LaunchKey.Examples.AspMvc.LaunchKey;

namespace LaunchKey.Examples.AspMvc.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		[AllowAnonymous]
		[NoCache]
		public ActionResult LoginJson(string username)
		{
			var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
			var authsResponse = lkClient.Authenticate(username, AuthenticationType.Session);
			return Json(authsResponse, JsonRequestBehavior.AllowGet);
		}

		[AllowAnonymous]
		[NoCache]
		public ActionResult LoginPollJson(string authRequest)
		{
			var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
			var pollResponse = lkClient.Poll(authRequest);
			
			// request failed for some reason, let client error out
			if (!pollResponse.Successful)
			{
				return Json(new { Successful = false, Waiting = false, ErrorCode = pollResponse.MessageCode, ErrorMessage = pollResponse.Message }, JsonRequestBehavior.AllowGet);
			}

			// request succeeded but still waiting
			if (pollResponse.UserHash == null)
			{
				return Json(new { Successful = true, Waiting = true }, JsonRequestBehavior.AllowGet);
			}

			// request succeeded, device responded with an OK
			if (pollResponse.DecryptedAuth.Response)
			{
				return Json(new { Successful = true, Waiting = false, Accepted = true, RedirectUrl = Url.Action("LoginConfirm", new { authRequest = authRequest }) }, JsonRequestBehavior.AllowGet);
			}
			// request succeeded, device rejected
			else
			{
				lkClient.Logs(LogsAction.Authenticate, LogsStatus.Denied, authRequest);
				return Json(new { Successful = true, Waiting = false, Accepted = false }, JsonRequestBehavior.AllowGet);
			}

		}


		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[AllowAnonymous]
		public ActionResult LoginConfirm(string authRequest)
		{
			// confirm successful request.
			// Check hash against user database
			// If hash exists, login to that user 
			// If hash not exists, redirect to confirm details view

			var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();

			// verify authenticity of auth request
			var pollResponse = lkClient.Poll(authRequest);

			if (lkClient.IsAuthorized(authRequest, pollResponse))
			{
				var db = new LkExampleDatabaseDataContext();

				// auth success, let's check if we know this person
				var currentUser = db.Users.Where(u => u.LaunchKeyUserHash == pollResponse.UserHash).FirstOrDefault();

				// we do, so set their auth cookie and send them back to the home page
				if (currentUser != null)
				{
					currentUser.LastAuthRequest = authRequest;
					db.SubmitChanges();
					this.SetAuthCookie(currentUser);
					return RedirectToAction("Index", "Home");
				}
				// unknown user. serve them the new user form
				else
				{
					return View("LoginNewUser", new LoginNewUserModel { AuthRequest = authRequest });
				}
			}
			else
			{
				// show login error, send back to Login()
				return View("Login", new { Error = true, ErrorMessage = pollResponse.Message });
			}
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult LoginNewUser(LoginNewUserModel model)
		{
			if (ModelState.IsValid)
			{
				var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
				var pollResponse = lkClient.Poll(model.AuthRequest);

				if (!lkClient.IsAuthorized(model.AuthRequest, pollResponse))
				{
					ModelState.AddModelError(string.Empty, string.Format("Error communicating with LaunchKey. Response code: {0}, message: {1}", pollResponse.MessageCode, pollResponse.MessageCode));
					return View();
				}

				// create new user and login 
				var db = new LkExampleDatabaseDataContext();
				var newUser = new User { FirstName = model.FriendlyName, LastAuthRequest = model.AuthRequest, LaunchKeyUserHash = pollResponse.UserHash };
				db.Users.InsertOnSubmit(newUser);
				db.SubmitChanges();

				this.SetAuthCookie(newUser);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return View();
			}
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult LogOff()
		{
			// logout locally
			FormsAuthentication.SignOut();

			var lkIdentity = User != null ? User.Identity as LaunchKeyIdentity : null;
			if (lkIdentity != null)
			{
				// notify launchKey
				var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
				lkClient.Logout(lkIdentity.AuthRequest);
			}

			return RedirectToAction("Index", "Home");
		}


		private void SetAuthCookie(User user)
		{
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				2,
				user.LaunchKeyUserHash,
				DateTime.Now,
				DateTime.Now.AddMinutes(15),
				false,
				string.Format("{0};;{1}", user.LastAuthRequest, user.FirstName)
			);
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			cookie.Expires = DateTime.Now.AddMinutes(15);
			Response.Cookies.Add(cookie);
		}


	}
}
