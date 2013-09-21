using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LaunchKey.Examples.WinFormsApplication
{
	public static class LaunchKeyClientSingleton
	{
		static LaunchKeyRestClient client;
		public static LaunchKeyRestClient GetInstanceFromConfig()
		{
			if (client == null)
			{
				client = new LaunchKeyRestClient(
					ConfigurationManager.AppSettings["lk_appKey"],
					ConfigurationManager.AppSettings["lk_appSecret"],
					ConfigurationManager.AppSettings["lk_appPrivateKey"]
				);
			}
			return client;
		}
	}
}
