using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Represents a type of orbit
	/// </summary>
	public enum AuthenticationType
	{
		/// <summary>
		/// Indicates a session-based orbit 
		/// </summary>
		Session,

		/// <summary>
		/// Indicates a transaction-based orbit type
		/// </summary>
		Transaction
		
	}
}
