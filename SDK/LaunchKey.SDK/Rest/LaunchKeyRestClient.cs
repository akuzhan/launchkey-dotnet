using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Thin wrapper of the LaunchKey REST API. 
	/// </summary>
	public class LaunchKeyRestClient
	{
		private static readonly string ApiEndpoint = "https://api.launchkey.com/{0}/";

		private string appKey;
		private string secretKey;
		private AsymmetricCipherKeyPair appPrivateKey;
		private RsaKeyParameters apiPublicKey;
		private DateTime? apiPublicKeyDateStamp;
		private DateTime? lastPing;
		private DateTime? apiTime;
		private string apiVersion;

		/// <summary>
		/// Instantiate a LaunchKey REST client using app credentials
		/// </summary>
		/// <param name="appKey">app key</param>
		/// <param name="secretKey">app secret key</param>
		/// <param name="privateRsaKeyContent">RSA private key in PEM-OAEP format</param>
		/// <param name="apiVersion">API endpoint to use. Defaults to v1</param>
		public LaunchKeyRestClient(string appKey, string secretKey, string privateRsaKeyContent, string apiVersion = "v1")
		{
			if (string.IsNullOrWhiteSpace(appKey))
				throw new ArgumentException("appKey must be provided");
			if (string.IsNullOrWhiteSpace(secretKey))
				throw new ArgumentException("secretKey must be provided");
			if (string.IsNullOrWhiteSpace(privateRsaKeyContent))
				throw new ArgumentException("privateRsaKeyContent must be provided");

			this.appPrivateKey = this.LoadKeyPairFromPemString(privateRsaKeyContent);

			if (this.appPrivateKey == null)
				throw new ArgumentException("There was an error attempting to load your private key.");

			this.appKey = appKey;
			this.secretKey = secretKey;
			this.apiVersion = apiVersion;
		}

		/// <summary>
		/// Sends a /ping request to the API, retrieving the current API public key. Called implicitly by other API functions as needed.
		/// </summary>
		/// <returns><see cref="PingResponse"/> object including public key for LaunchKey API</returns>
		public PingResponse Ping()
		{
			RestRequest request = new RestRequest("ping", Method.GET);
			var pingResponse = this.RestRequest<PingResponse>(request);
			this.apiPublicKey = this.LoadPublicKeyFromPemString(pingResponse.Key);
			this.apiPublicKeyDateStamp = pingResponse.DateStamp;
			this.apiTime = DateTime.Parse(pingResponse.LaunchkeyTime);
			this.lastPing = DateTime.Now;
			return pingResponse;
		}


		/// <summary>
		/// Begin an authentication cycle for user identified by <paramref name="username"/>. Implicitly calls Ping().
		/// </summary>
		/// <param name="username">Username to authenticate</param>
		/// <param name="authType">Authentication type, either transaction or session</param>
		/// <returns><see cref="AuthsResponse"/> object including unique token for further information about this request</returns>
		public AuthsResponse Authenticate(string username, AuthenticationType authType)
		{
			this.EnsureAuthorizationRecent();
			RestRequest request = new RestRequest("auths", Method.POST);
			request.AddParameter("username", username);
			if (authType == AuthenticationType.Transaction)
				request.AddParameter("session", 0);
			return this.AuthenticatedRestRequest<AuthsResponse>(request);
		}

		/// <summary>
		/// Poll the status of an authentication request using the reference id supplied in <paramref name="authRequest"/>. Implicitly calls Ping().
		/// </summary>
		/// <param name="authRequest">unique authentication request reference ID to check</param>
		/// <returns><see cref="PollResponse"/> object including status of request</returns>
		public PollResponse Poll(string authRequest)
		{
			if (string.IsNullOrWhiteSpace(authRequest))
				throw new ArgumentException("authRequest must be provided");

			this.EnsureAuthorizationRecent();

			RestRequest request = new RestRequest("poll", Method.GET);
			request.AddParameter("auth_request", authRequest);
			request.AddParameter("SUPPRESS_RESPONSE_CODES", "TRUE");

			var response = this.AuthenticatedRestRequest<PollResponse>(request);
			if (response.Auth != null)
			{
				var encryptionProvider = new LaunchKeyEncryptionProvider();
				var authPayload = Convert.FromBase64String(response.Auth.Replace("\\/", "/"));
				var decryptedPayload = encryptionProvider.Decrypt(this.appPrivateKey.Private, authPayload);
				var decryptedText = Encoding.ASCII.GetString(decryptedPayload);

				var jobj = JObject.Parse(decryptedText);

				response.DecryptedAuth = new DecryptedPollResponse()
				{
					AuthRequest = jobj.Property("auth_request") != null ? jobj.Property("auth_request").Value.ToString() : null,
					DeviceId = jobj.Property("device_id") != null ? jobj.Property("device_id").Value.ToString() : null,
					AppPins = jobj.Property("app_pins") != null ? jobj.Property("app_pins").Value.ToString().Split(',') : null,
					Response = jobj.Property("response") != null ? bool.Parse(jobj.Property("response").Value.ToString()) : false
				};
			}

			return response;
		}

		/// <summary>
		/// Notify the LaunchKey API of a status change, such as successful or unsuccessful logins or revocations. Implicitly calls <see cref="Ping"/>.
		/// </summary>
		/// <param name="action">Action that was performed</param>
		/// <param name="status">Outcome of action</param>
		/// <param name="authenticationRequest">Authentication request associated with the action</param>
		/// <returns><see cref="LogsResponse"/> object indicating success or failure of the status update</returns>
		public LogsResponse Logs(LogsAction action, LogsStatus status, string authenticationRequest)
		{
			if (string.IsNullOrWhiteSpace(authenticationRequest))
				throw new ArgumentException("authRequest must be provided");

			this.EnsureAuthorizationRecent();

			var request = new RestRequest("logs", Method.PUT);
			request.AddParameter("action", Enum.GetName(typeof(LogsAction), action));
			request.AddParameter("status", status == LogsStatus.Granted ? "True" : "False");
			request.AddParameter("auth_request", authenticationRequest);
			return this.AuthenticatedRestRequest<LogsResponse>(request);
		}

		/// <summary>
		/// given a <see cref="PollResponse" /> object, returns whether the authentication has been approved by a user yet.
		/// if the authentication request is approved, makes an implicit call to <see cref="Logs"/> to update the device status
		/// </summary>
		/// <param name="authRequest">the associated quth request</param>
		/// <param name="response">The response to validate</param>
		/// <returns>true if the user has approved the authorization, or false if they have not</returns>
		public bool IsAuthorized(string authRequest, PollResponse response)
		{
			bool isAuthenticated = response.Successful && response.UserHash != null && response.DecryptedAuth != null && response.DecryptedAuth.Response;
			if (isAuthenticated)
				this.Logs(LogsAction.Authenticate, LogsStatus.Granted, authRequest);
			else
				this.Logs(LogsAction.Authenticate, LogsStatus.Denied, authRequest);

			return isAuthenticated;
		}

		/// <summary>
		/// Convenience method for logging out an auth session
		/// </summary>
		/// <param name="authRequest">The authRequest to deorbit</param>
		/// <returns><see cref="LogsResponse"/> object received from server</returns>
		public LogsResponse Logout(string authRequest)
		{
			return this.Logs(LogsAction.Revoke, LogsStatus.Granted, authRequest);
		}


		/// <summary>
		/// Attempts to decode a deorbit request delivered via callback from the LaunchKey API. When a request is received, pass the request parameters to this method. If a user hash cannot be decoded for any reason, an exception will be thrown
		/// </summary>
		/// <param name="orbitData">The orbit data from the 'deorbit' GET parameter</param>
		/// <param name="signature">The signature which verifies the data in <paramref name="orbitData"/>, from the 'signature' GET parameter</param>
		/// <returns>The user hash being deorbited</returns>
		public string Deorbit(string orbitData, string signature)
		{
			var pingResponse = this.Ping();
			if (!pingResponse.Successful)
				throw new LaunchKeyApiException("Could not verify valid deorbit request, was unable ping API to get server time.");

			var launchKeyEncryptionProvider = new LaunchKeyEncryptionProvider();
			var signatureValid = launchKeyEncryptionProvider.VerifySignature(
				this.apiPublicKey,
				Encoding.ASCII.GetBytes(orbitData),
				Convert.FromBase64String(signature)
			);

			// signature checks out
			if (signatureValid)
			{
				var jobj = JObject.Parse(orbitData);

				if (jobj.Property("launchkey_time") == null)
					throw new LaunchKeyApiException("Invalid request parameters: launchkey_time was not present");

				if (jobj.Property("user_hash") == null)
					throw new LaunchKeyApiException("Invalid request parameters: user_hash was not present");


				var requestLaunchKeyTime = (DateTime)jobj.Property("launchkey_time").Value;

				if (this.apiTime.Value.Subtract(requestLaunchKeyTime).TotalMinutes < 5.0)
				{
					return (string)jobj.Property("user_hash");
				}
				else
				{
					throw new LaunchKeyApiException("Request is older than 5 minutes. Possible replay attack?");
				}
			}
			else
			{
				throw new LaunchKeyApiException("Signature validation failed!");
			}
		}


		private void EnsureAuthorizationRecent()
		{
			if (this.apiPublicKey == null)
				this.Ping();
		}


		private AsymmetricCipherKeyPair LoadKeyPairFromPemString(string privateRsaKeyPemString)
		{
			AsymmetricCipherKeyPair keyPair;
			PemReader pemReader = new PemReader(new StringReader(privateRsaKeyPemString));
			keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
			return keyPair;
		}

		private RsaKeyParameters LoadPublicKeyFromPemString(string publicRsaKeyPemString)
		{
			RsaKeyParameters keyParameters;
			PemReader pemReader = new PemReader(new StringReader(publicRsaKeyPemString));
			keyParameters = (RsaKeyParameters)pemReader.ReadObject();
			return keyParameters;
		}

		private TResponseType RestRequest<TResponseType>(IRestRequest request)
			where TResponseType : LaunchKeyResponse, new()
		{
			var client = new RestClient(string.Format(ApiEndpoint, this.apiVersion));
			var response = client.Execute<TResponseType>(request);
			if (response.ResponseStatus != ResponseStatus.Completed)
			{
				throw new LaunchKeyApiException(
					string.Format("LaunchKey API request failed to complete ({1}) {0}",
						response.ErrorException != null ? string.Format("An exception of type {0} was thrown", response.ErrorException.GetType().ToString())
														: "No exception was thrown",
						response.ErrorMessage
					)
				);
			}

			if (response.StatusCode > HttpStatusCode.InternalServerError)
			{
				throw new LaunchKeyApiException(
					string.Format("LaunchKey API request returned non-200 response. Response was {0} - {1}. Content body: {2}",
						response.StatusCode, Enum.GetName(typeof(HttpStatusCode), response.StatusCode), response.Content
					)
				);
			}

			if (response.Data == null)
			{
				throw new LaunchKeyApiException("LaunchKey API returned an invalid response which could not be deserialized.");
			}

			return response.Data;
		}

		private TResponseType AuthenticatedRestRequest<TResponseType>(IRestRequest request)
			where TResponseType : LaunchKeyResponse, new()
		{
			request.AddParameter("app_key", this.appKey);
			var secretInfo = string.Format("{{\"secret\": '{0}', \"stamped\": '{1}'}}", this.secretKey, this.apiTime.Value.Add(DateTime.Now.Subtract(this.lastPing.Value)).ToString("yyyy-MM-dd HH:mm:ss"));
			var encryptionProvider = new LaunchKeyEncryptionProvider();
			var encryptedSecretKey = encryptionProvider.Encrypt(this.apiPublicKey, Encoding.ASCII.GetBytes(secretInfo));
			request.AddParameter("secret_key", Convert.ToBase64String(encryptedSecretKey));
			request.AddParameter("signature", Convert.ToBase64String(encryptionProvider.Sign(this.appPrivateKey.Private, encryptedSecretKey)));

			return this.RestRequest<TResponseType>(request);
		}


	}
}
