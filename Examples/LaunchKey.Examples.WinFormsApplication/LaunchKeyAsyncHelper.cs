using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaunchKey.Examples.WinFormsApplication
{
	class LaunchKeyAsyncHelper
	{
		private LaunchKeyRestClient client;
		private Thread continuousPollThread;
		private AutoResetEvent continuousPollThreadResetEvent;

		public LaunchKeyAsyncHelper(LaunchKeyRestClient client)
		{
			this.client = client;
			this.continuousPollThreadResetEvent = new AutoResetEvent(false);
		}

		public void BeginAuthentication(string username, Action<AuthsResponse> onSuccess, Action<AuthsResponse> onFailure)
		{
			ThreadPool.QueueUserWorkItem(_ =>
			{
				var auths = this.client.Authenticate(username, AuthenticationType.Session);
				if (auths.Successful)
					onSuccess(auths);
				else
					onFailure(auths);
			});
		}

		public void BeginPoll(string authRequest, Action<PollResponse> onSuccess, Action<PollResponse> onFailure)
		{
			ThreadPool.QueueUserWorkItem(_ =>
			{
				PollResponse pollResponse;
				do
				{
					Thread.Sleep(1000);
					pollResponse = this.client.Poll(authRequest);
				}
				while (pollResponse.MessageCode == LaunchKeyResponseCode.PollErrorResponsePending);
					
				if (pollResponse.Successful)
					onSuccess(pollResponse);
				else
					onFailure(pollResponse);
			});
		}

		public void BeginLogs(LogsAction action, LogsStatus status, string authRequest, Action<LogsResponse> onSuccess, Action<LogsResponse> onFailure)
		{
			ThreadPool.QueueUserWorkItem(_ =>
			{
				var logsResponse = this.client.Logs(action, status, authRequest);
				if (logsResponse.Successful && onSuccess != null)
					onSuccess(logsResponse);
				if (!logsResponse.Successful && onFailure != null)
					onFailure(logsResponse);
			});
		}

		public void BeginContinuousPoll(string authRequest, Action<PollResponse> onDeorbit, Action onPollBegin, Action onPollSuccesful)
		{
			if (this.continuousPollThread != null)
				this.EndContinuousPoll();

			this.continuousPollThread = new Thread(() =>
			{
				PollResponse pollResponse = null;
				while (true)
				{
					this.continuousPollThreadResetEvent.WaitOne(TimeSpan.FromMinutes(1));
					
					if (onPollBegin != null)
						onPollBegin();

					pollResponse = this.client.Poll(authRequest);
					
					if (this.client.IsAuthorized(authRequest, pollResponse))
					{
						if (onPollSuccesful != null)
							onPollSuccesful();
					}
					else
					{
						onDeorbit(pollResponse);
						break;
					}
				}
				this.continuousPollThread = null;
			});
			this.continuousPollThread.IsBackground = true;
			this.continuousPollThread.Start();
		}

		public void ForcePollNow()
		{
			this.continuousPollThreadResetEvent.Set();
		}

		public void EndContinuousPoll()
		{
			if (this.continuousPollThread != null)
			{
				this.continuousPollThread.Abort();
				this.continuousPollThread = null;
			}
		}

	}

}
