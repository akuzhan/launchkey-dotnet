using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaunchKey.SDK.Rest;
using System.Configuration;

namespace LaunchKey.Examples.AspNetWebForms.Security
{
	public static class LaunchKeyClientFactory
	{
		public static LaunchKeyRestClient GetInstanceFromConfig()
		{
			return new LaunchKeyRestClient(
				ConfigurationManager.AppSettings["lk_appKey"],
				ConfigurationManager.AppSettings["lk_appSecret"],
				ConfigurationManager.AppSettings["lk_appPrivateKey"]
			);
		}
	}
}