using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Parameter passed to /logs/ API. Used in conjuction with <see cref="LogsStatus"/> 
	/// </summary>
	public enum LogsAction
	{
		/// <summary>
		/// Represents an authentication attempt
		/// </summary>
		Authenticate,

		/// <summary>
		/// Represents an authentication revocation 
		/// </summary>
		Revoke
	}
}
