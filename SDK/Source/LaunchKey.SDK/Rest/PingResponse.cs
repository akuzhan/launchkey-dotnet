using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Response object returned from /ping/ requests
	/// </summary>
	public class PingResponse : LaunchKeyResponse
	{
		/// <summary>
		/// timestamp associated with the <see cref="Key"/> property. 
		/// </summary>
		public DateTime DateStamp { get; set; }

		/// <summary>
		/// Current time on LaunchKey servers
		/// </summary>
		public string LaunchkeyTime { get; set; }

		/// <summary>
		/// PEM-OAEP encoded public RSA key for the API. 
		/// </summary>
		public string Key { get; set; }
	}
}
