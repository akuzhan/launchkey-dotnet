using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Response object for requests to /auth/
	/// </summary>
	public class AuthsResponse : LaunchKeyResponse
	{
		/// <summary>
		/// Unique identifier to use when referring to this request in future polls, deorbits, etc.
		/// </summary>
		public string AuthRequest { get; set; }
	}

}
