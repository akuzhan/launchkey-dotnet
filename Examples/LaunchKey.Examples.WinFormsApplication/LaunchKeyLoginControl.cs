using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LaunchKey.SDK.Rest;
using System.Threading;

namespace LaunchKey.Examples.WinFormsApplication
{
	public partial class LaunchKeyLoginControl : UserControl
	{
		public event Action<PollResponse> AuthenticationSuccess;

		public LaunchKeyLoginControl()
		{
			InitializeComponent();
		}

		public void Reset()
		{
			this.txtUsername.Clear();
			this.EnableAuth();
		}


		private void btnLogin_Click(object sender, EventArgs e)
		{
			HandleButtonPush();
		}

		private void HandleButtonPush()
		{
			if (string.IsNullOrWhiteSpace(this.txtUsername.Text))
			{
				this.txtUsername.Focus();
			}
			else
			{
				this.DisableAuth();
				ThreadPool.QueueUserWorkItem(_ => { DoAuth(); });
			}
		}

		private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				HandleButtonPush();
				e.Handled = true;
			}
		}


		private void DoAuth()
		{
			var lkClient = LaunchKeyClientSingleton.GetInstanceFromConfig();
			var lkSessionManager = new LaunchKeyAsyncHelper(lkClient);

			InvokeSetButtonStatus("Contacting LaunchKey ... ");
			

			lkSessionManager.BeginAuthentication(
				username: txtUsername.Text,
				onSuccess: authResponse =>
				{
					InvokeSetButtonStatus("Launch Request Sent ... ");
					lkSessionManager.BeginPoll(
						authRequest: authResponse.AuthRequest,
						onSuccess: pollResponse =>
						{
							if (lkClient.IsAuthorized(authResponse.AuthRequest, pollResponse))
							{
								InvokeSetButtonStatus("Success. :)");
								if (AuthenticationSuccess != null)
									this.Invoke(AuthenticationSuccess, pollResponse);
							}
							else
							{
								InvokeSetButtonStatus("Device rejected. :(");
								this.DelayEnableAuth();
							}
						},
						onFailure: pollResponse =>
						{
							InvokeSetButtonStatus("Error Authenticating :(");
							this.DelayEnableAuth();
						}
					);
				},
				onFailure: authResponse =>
				{
					InvokeSetButtonStatus("Launch Request Failed :(");
					this.DelayEnableAuth();
				}
			);
		}

		private void DelayEnableAuth()
		{
			Thread.Sleep(3000);
			this.EnableAuth();
		}


		private void InvokeSetButtonStatus(string status)
		{
			this.Invoke((Action)(() =>
			{
				this.btnLogin.Text = status;
			}));
		}

		private void DisableAuth()
		{
			this.Invoke(new Action(() =>
			{
				this.btnLogin.Enabled = false;
				this.txtUsername.Enabled = false;
			}));
		}

		private void EnableAuth()
		{
			this.Invoke(new Action(() =>
			{
				this.btnLogin.Text = "Log in";
				this.btnLogin.Enabled = true;
				this.txtUsername.Enabled = true;
				this.txtUsername.Focus();
			}));
		}

	}
}
