using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Exception object thrown by LaunchKey API wrapper
	/// </summary>
	public class LaunchKeyApiException : Exception
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public LaunchKeyApiException() : base() { }

		/// <summary>
		/// Construct with message 
		/// </summary>
		/// <param name="msg">Error details</param>
		public LaunchKeyApiException(string msg) : base(msg) { }

		/// <summary>
		/// Construct with message and inner exception
		/// </summary>
		/// <param name="msg">Error details</param>
		/// <param name="inner">Inner exception</param>
		public LaunchKeyApiException(string msg, Exception inner) : base(msg, inner) { }
	}
}
