using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// base class for most API responses, includes common parameters
	/// </summary>
	public class LaunchKeyResponse
	{
		/// <summary>
		/// Whether the API call was a success or not
		/// </summary>
		public bool Successful { get; set; }

		/// <summary>
		/// Status code indicating the state of the request. Corresponds to HTTP response codes.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// Error details associated with a non-0 value in <see cref="MessageCode"/>
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Error code, if any
		/// </summary>
		public LaunchKeyResponseCode MessageCode { get; set; }

		/// <summary>
		/// Response body, if applicable
		/// </summary>
		public string Response { get; set; }

		/// <summary>
		/// Default constructor. Not all API requests contain detailed information. Assume successful if not present
		/// </summary>
		public LaunchKeyResponse()
		{
			this.Successful = true;
		}
	}
}
