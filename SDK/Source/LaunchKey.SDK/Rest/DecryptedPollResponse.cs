using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Decrypted details returned from a request to /poll/
	/// </summary>
	public class DecryptedPollResponse
	{
		/// <summary>
		/// Authentication outcome. true for success, false for failure
		/// </summary>
		public bool Response { get; set; }

		/// <summary>
		/// List of previous pins sent by the user's device. Can be used as an additional verification step.
		/// </summary>
		public string[] AppPins { get; set; }

		/// <summary>
		/// Unique ID associated with the auth request
		/// </summary>
		public string AuthRequest { get; set; }

		/// <summary>
		/// User's device ID
		/// </summary>
		public string DeviceId { get; set; }

	}

}
