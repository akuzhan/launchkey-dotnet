using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaunchKey.Examples.ConsoleApplication
{
	class Program
	{
		class MyLaunchKeyApplication
		{
			private string appKey, secretKey, privateRsaKey;

			public MyLaunchKeyApplication(string appKey, string secretKey, string privateRsaKey)
			{
				this.appKey = appKey;
				this.secretKey = secretKey;
				this.privateRsaKey = privateRsaKey;
			}

			public void Run()
			{
				LaunchKeyRestClient client = new LaunchKeyRestClient(
					this.appKey,
					this.secretKey,
					this.privateRsaKey
				);
				
				var pingResponse = client.Ping();
				if (!pingResponse.Successful)
				{
					Console.WriteLine("ERROR: Error getting ping response.");
					return;
				}

				Console.WriteLine("SUCCESS: Received public key from API.");
				
				string username;
				do
				{
					Console.Write("Enter username to authenticate as:");
					username = Console.ReadLine();
				} while (string.IsNullOrWhiteSpace(username));

				var authResponse = client.Authenticate(username, AuthenticationType.Session);

				if (!authResponse.Successful)
				{
					Console.WriteLine("ERROR: Error getting auth response. Error code: {0}", authResponse.StatusCode);
					return;
				}
				Console.WriteLine("SUCCESS: Authentication request sent to device. Reference code: {0}", authResponse.AuthRequest);

				Console.WriteLine("Beginning poll-wait state");

				PollResponse pollResponse;
				do
				{
					Thread.Sleep(1000);
					pollResponse = client.Poll(authResponse.AuthRequest);
					Console.WriteLine("Poll response: {0}: {1}", pollResponse.MessageCode, pollResponse.Message);
				} while (pollResponse.MessageCode == LaunchKeyResponseCode.PollErrorResponsePending);

				if (!client.IsAuthorized(authResponse.AuthRequest, pollResponse))
				{
					// rejected!
					Console.WriteLine("Authentication failed. ", pollResponse.Message);
					return;
				}

				// Authentication success!
				Console.WriteLine("SUCCESS: {0} authenticated!", username);
				Console.WriteLine("Press enter to dorbit.");
				Console.ReadLine();

				Console.WriteLine("Deorbiting ... ");
				var logsResponse = client.Logout(authResponse.AuthRequest);
				Console.WriteLine("Logs API response: {0} - {1}", logsResponse.MessageCode, logsResponse.Message);
				if (!logsResponse.Successful)
				{
					Console.WriteLine("Error deorbiting.");
				}

			}
		}

		static void Main(string[] args)
		{
			MyLaunchKeyApplication application = new MyLaunchKeyApplication(
				ConfigurationManager.AppSettings["appKey"],
				ConfigurationManager.AppSettings["secretKey"],
				ConfigurationManager.AppSettings["privateRsaKey"]
			);
			application.Run();

			Console.WriteLine("Application terminated.");

			Console.ReadLine();
		}
	}
}
