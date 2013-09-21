using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Possible response codes returned from the LaunchKey REST APIs.
	/// </summary>
	public enum LaunchKeyResponseCode
	{
		/// <summary>
		/// Server did not specify a code
		/// </summary>
		Unspecified = 0,

		/// <summary>
		/// POST; Pushed notification
		/// </summary>
		AuthsSuccessPushedNotification = 40220,

		/// <summary>
		/// POST; Pushed notification
		/// </summary>
		AuthsErrorPushedNotification = 40421,

		/// <summary>
		/// POST; Credentials incorrect for app and app secret
		/// </summary>
		AuthsErrorAppKeyOrAppSecretIncorrect = 40422,

		/// <summary>
		/// POST; Error verifying app
		/// </summary>
		AuthsErrorCouldNotVerifyApp = 40423,

		/// <summary>
		/// POST; No paired devices
		/// </summary>
		AuthsErrorNoPairedDevices = 40424,

		/// <summary>
		/// POST; Invalid app key
		/// </summary>
		AuthsErrorInvalidAppKey = 40425,

		/// <summary>
		/// POST; No such user
		/// </summary>
		AuthsErrorNoSuchUser = 40426,

		/// <summary>
		/// POST; Signature does not match
		/// </summary>
		AuthsErrorSignatureDoesNotMatch = 40428,

		/// <summary>
		/// POST; App credentials incorrect
		/// </summary>
		AuthsErrorAppCredentialsIncorrect = 40429,

		/// <summary>
		/// POST; Authorization expired
		/// </summary>
		AuthsErrorAuthorizationExpired = 40431,

		/// <summary>
		/// POST; Error checking signature, ensure padding is valid
		/// </summary>
		AuthsErrorInvalidSignature = 40432,

		/// <summary>
		/// POST; Signature matches, but error decrypting secret_key
		/// </summary>
		AuthsErrorSignatureMatchesButDecryptionFailed = 40433,

		/// <summary>
		/// POST; Decrypted secret_key, but malformed structure
		/// </summary>
		AuthsErrorSecretKeyStructureInvalid = 40434,

		/// <summary>
		/// POST; App disabled
		/// </summary>
		AuthsErrorAppDisabled = 40435,

		/// <summary>
		/// PUT; Successfully updated
		/// </summary>
		LogsSuccessfullyUpdated = 50240,

		/// <summary>
		/// PUT; Incorrect data for API call
		/// </summary>
		LogsErrorIncorrectDataForCall = 50441,

		/// <summary>
		/// PUT; Credentials incorrect
		/// </summary>
		LogsErrorCredentialsIncorrect = 50442,

		/// <summary>
		/// PUT; Error validating app
		/// </summary>
		LogsErrorCouldNotVerifyApp = 50443,

		/// <summary>
		/// PUT; Log inconsistent, unable to update
		/// </summary>
		LogsErrorLogInconsistent = 50444,

		/// <summary>
		/// PUT; Unknown auth request
		/// </summary>
		LogsErrorUnknownAuthRequest = 50445,

		/// <summary>
		/// PUT; Auth request does not correlate to session
		/// </summary>
		LogsErrorAuthRequestDoesNotCorrelateToSession = 50446,

		/// <summary>
		/// GET; Return the public key
		/// </summary>
		PingSuccess = 60200,

		/// <summary>
		/// GET; Key is up to date
		/// </summary>
		PingSuccessKeyUpToDate = 60201,

		/// <summary>
		/// GET; Incorrect data for API call
		/// </summary>
		PingErrorIncorrectDataForCall = 60401,

		/// <summary>
		/// GET; Return the auth package
		/// </summary>
		PollSuccess = 70200,

		/// <summary>
		/// GET; Incorrect data for API call
		/// </summary>
		PollErrorIncorrectDataForCall = 70401,

		/// <summary>
		/// GET; There is no pending request
		/// </summary>
		PollErrorNoPendingRequest = 70402,

		/// <summary>
		/// GET; Pending response
		/// </summary>
		PollErrorResponsePending = 70403,

		/// <summary>
		/// GET; Expired request
		/// </summary>
		PollErrorExpiredRequest = 70404

	}
}
