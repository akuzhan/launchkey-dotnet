using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using LaunchKey.Examples.AspNetWebForms.Security;
using LaunchKey.SDK.Rest;

namespace LaunchKey.Examples.AspNetWebForms.Account
{
	public partial class LoginConfirm : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["authRequest"] != null)
			{
				AuthRequest.Value = Request.QueryString["authRequest"];
			}
			else
			{
				Response.Redirect("~");
			}
		}

		protected void SubmitButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				var authRequest = AuthRequest.Value;
				var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
				var pollResponse = lkClient.Poll(authRequest);

				if (lkClient.IsAuthorized(authRequest, pollResponse))
				{
					this.SetAuthCookie(pollResponse.UserHash, authRequest, FriendlyName.Text);
					Response.Redirect("~");
				}
			}
		}

		private void SetAuthCookie(string userHash, string authRequest, string friendlyName)
		{
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				2,
				friendlyName,
				DateTime.Now,
				DateTime.Now.AddMinutes(15),
				false,
				string.Format("{0};;{1}", userHash, authRequest)
			);
			string encryptedTicket = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			cookie.Expires = DateTime.Now.AddMinutes(15);
			Response.Cookies.Add(cookie);
		}
		
	}
}