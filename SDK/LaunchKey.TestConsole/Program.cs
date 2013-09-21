using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace LaunchKey.TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			var orbitdata = "{\"launchkey_time\": \"2013-09-11 23:08:11\", \"user_hash\": \"m3mCQpEj2letzD2TS1Ynxm45KtnQPVFGaOptnxiZ8Pl\"}";
			var signature = "sbdYMYtnjlGIwL8SHZuAI+wsNjqhVbtdJ6cnphISbOcU8AUtrwIrH24Z2vccwspZMo6dQQyiD/Y9wxJCq1e7+62CSnSrHDSPkWnw42ELbEN1fZyUZx/s80GqT4I5eyEdd2r/Yp12iNNEebP2ps6v01tYqyIHPowav9K3/ndBdcx+tw21e0zz+BL8rVUQgXfYbryNUVzT6v/Gs9d3/Lc2j/r4lclptRiJDy0yUNh77WZo6cZAU+lbiHTIzxyFl0MW/kwbPRp03WHMlcDuil3waUG0ml3oVW/p1o22pnaAnAgpa34a2I7hsqS0YuOJOlzoVoArwgRH+TEruQg1LClaBw==";

			var client = new LaunchKeyRestClient(
				ConfigurationManager.AppSettings["lk_appKey"],
				ConfigurationManager.AppSettings["lk_appSecret"],
				ConfigurationManager.AppSettings["lk_appPrivateKey"]
			);

			client.Deorbit(orbitdata, signature);
		}
	}
}
