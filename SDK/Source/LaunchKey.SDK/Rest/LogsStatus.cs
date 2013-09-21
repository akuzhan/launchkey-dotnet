using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchKey.SDK.Rest
{
	/// <summary>
	/// Parameter passed to /logs/ API. Used in conjuction with <see cref="LogsAction"/>.
	/// </summary>
	public enum LogsStatus
	{
		/// <summary>
		/// The action was granted
		/// </summary>
		Granted,

		/// <summary>
		/// The action was denied
		/// </summary>
		Denied
	}
}
