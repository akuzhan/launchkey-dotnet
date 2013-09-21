using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using LaunchKey.SDK.Rest;
using LaunchKey.Examples.AspNetWebForms.Security;

namespace LaunchKey.Examples.AspNetWebForms
{
	/// <summary>
	/// Summary description for LaunchKeyJsonWebService
	/// </summary>
	[WebService]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[ScriptService]
	public class LaunchKeyJsonWebService : System.Web.Services.WebService
	{
		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public AuthsResponse Login(string username)
		{
			var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
			var authsResponse = lkClient.Authenticate(username, AuthenticationType.Session);
			return authsResponse;
		}

		[WebMethod]
		[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
		public object LoginPoll(string authRequest)
		{
			var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
			var pollResponse = lkClient.Poll(authRequest);

			// request failed for some reason, let client error out
			if (!pollResponse.Successful)
			{
				return new { Successful = false, Waiting = false, ErrorCode = pollResponse.MessageCode, ErrorMessage = pollResponse.Message };
			}

			// request succeeded but still waiting
			if (pollResponse.UserHash == null)
			{
				return new { Successful = true, Waiting = true };
			}

			// request succeeded, device responded with an OK
			if (pollResponse.DecryptedAuth.Response)
			{
				return new { Successful = true, Waiting = false, Accepted = true, RedirectUrl = "LoginConfirm.aspx?authRequest=" + authRequest };
			}
			// request succeeded, device rejected
			else
			{
				lkClient.Logs(LogsAction.Authenticate, LogsStatus.Denied, authRequest);
				return new { Successful = true, Waiting = false, Accepted = false };
			}
		}
	}
}
