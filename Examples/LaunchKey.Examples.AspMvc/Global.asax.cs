using LaunchKey.Examples.AspMvc.LaunchKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace LaunchKey.Examples.AspMvc
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}

		public void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
		{
			HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null)
			{
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
				string[] userDataTokens = ticket.UserData.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

				LaunchKeyIdentity identity = new LaunchKeyIdentity(ticket.Name, userDataTokens[0], userDataTokens[1]);

				// verify user hasn't de-orbited
				var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
				var pollResponse = lkClient.Poll(identity.AuthRequest);

				if (lkClient.IsAuthorized(identity.AuthRequest, pollResponse))
				{
					args.User = new GenericPrincipal(identity, null);
				}
				else
				{
					// unset cookie
					FormsAuthentication.SignOut();
					lkClient.Logout(identity.AuthRequest);
					Context.Response.Redirect("~");
				}
			}
		}

		
	}
}