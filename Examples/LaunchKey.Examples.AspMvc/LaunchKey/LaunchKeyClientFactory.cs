using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LaunchKey.Examples.AspMvc.LaunchKey
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