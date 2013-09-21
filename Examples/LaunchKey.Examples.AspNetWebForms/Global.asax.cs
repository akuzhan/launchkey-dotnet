using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using LaunchKey.Examples.AspNetWebForms.Security;
using System.Security.Principal;

namespace LaunchKey.Examples.AspNetWebForms
{
	public class Global : System.Web.HttpApplication
	{

		void Application_Start(object sender, EventArgs e)
		{
			// Code that runs on application startup

		}

		void Application_End(object sender, EventArgs e)
		{
			//  Code that runs on application shutdown

		}

		void Application_Error(object sender, EventArgs e)
		{
			// Code that runs when an unhandled error occurs

		}

		void Session_Start(object sender, EventArgs e)
		{
			// Code that runs when a new session is started

		}

		void Session_End(object sender, EventArgs e)
		{
			// Code that runs when a session ends. 
			// Note: The Session_End event is raised only when the sessionstate mode
			// is set to InProc in the Web.config file. If session mode is set to StateServer 
			// or SQLServer, the event is not raised.

		}

		public void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs args)
		{
			HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null)
			{
				FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
				string[] userDataTokens = ticket.UserData.Split(new string[] { ";;" }, StringSplitOptions.None);

				LaunchKeyIdentity identity = new LaunchKeyIdentity(userDataTokens[0], userDataTokens[1], ticket.Name);

				// verify user hasn't de-orbited
				var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
				var pollResponse = lkClient.Poll(identity.AuthRequest);

				if (pollResponse.Successful)
				{
					args.User = new GenericPrincipal(identity, null);
				}
				else
				{
					// unset cookie
					FormsAuthentication.SignOut();
					lkClient.Logs(SDK.Rest.LogsAction.Revoke, SDK.Rest.LogsStatus.Granted, identity.AuthRequest);
					Context.Response.Redirect("~");
				}
			}
		}

	}
}
