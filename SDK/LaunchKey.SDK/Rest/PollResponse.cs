using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Response object returned from /poll/ requests
	/// </summary>
	public class PollResponse : LaunchKeyResponse
	{
		/// <summary>
		/// Encrypted authentication package delivered from the API
		/// </summary>
		public string Auth { get; set; }

		/// <summary>
		/// Decrypted object extracted from <see cref="Auth"/> property
		/// </summary>
		public DecryptedPollResponse DecryptedAuth { get; set; }

		/// <summary>
		/// Unique user hash. Represents a permanently unique identifier to identify a user within a LaunchKey app.
		/// </summary>
		public string UserHash { get; set; }
	}

}
