using LaunchKey.Examples.AspNetWebForms.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LaunchKey.Examples.AspNetWebForms.Account
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			if (User != null)
			{
				var lkIdentity = User.Identity as LaunchKeyIdentity;
				if (lkIdentity != null)
				{
					var lkClient = LaunchKeyClientFactory.GetInstanceFromConfig();
					lkClient.Logout(lkIdentity.AuthRequest);
				}
			}

			Response.Redirect("~/");
		}
	}
}