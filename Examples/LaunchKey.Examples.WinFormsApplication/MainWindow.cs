using LaunchKey.SDK.Rest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaunchKey.Examples.WinFormsApplication
{
	public partial class MainWindow : Form
	{
		private PollResponse currentAuth = null;
		private LaunchKeyAsyncHelper lkAsyncHelper;
		private Timer timer;

		public MainWindow()
		{
			InitializeComponent();

			this.lkAsyncHelper = new LaunchKeyAsyncHelper(LaunchKeyClientSingleton.GetInstanceFromConfig());

			this.treasureControl.LogOutClicked += (s, e) =>
			{
				// send the launch key deorbit request 
				// this should mark our current auth as expired, causing our continuous poll to die out
				this.lkAsyncHelper.BeginLogs(LogsAction.Revoke, LogsStatus.Granted, this.currentAuth.DecryptedAuth.AuthRequest, _ => lkAsyncHelper.ForcePollNow(), _ => lkAsyncHelper.ForcePollNow());
			};

			this.lkLoginControl.AuthenticationSuccess += (authResponse) =>
			{
				this.currentAuth = authResponse;

				// hide the login view
				this.pnlLogin.Visible = false;

				// show our treasure
				this.pnlTreasure.Visible = true;

				this.tspLabel.Text = "User authenticated, will check again in 1 minute ... ";
				this.tspForcePollButton.Visible = true;

				// tell our async helper to continuously poll LaunchKey to validate our session
				this.lkAsyncHelper.BeginContinuousPoll(this.currentAuth.DecryptedAuth.AuthRequest,
					onDeorbit: deorbitResponse =>
					{
						// this code will execute if the session is killed, either by us or by the device
						this.Invoke((Action)(() =>
						{
							MessageBox.Show("Session was deorbited.", "LaunchKey", MessageBoxButtons.OK, MessageBoxIcon.Information);

							this.lkAsyncHelper.BeginLogs(LogsAction.Revoke, LogsStatus.Granted, this.currentAuth.DecryptedAuth.AuthRequest, null, null);

							// bring us back to login, hide the treasure
							this.lkLoginControl.Reset();
							this.treasureControl.Reset();
							this.pnlLogin.Visible = true;
							this.pnlTreasure.Visible = false;
							this.tspLabel.Text = string.Empty;
							this.tspForcePollButton.Visible = false;
						}));

						// kill our local session
						this.currentAuth = null;
					},
					onPollBegin: () => 
					{
						this.Invoke((Action)(() =>
						{
							this.tspLabel.Text = "Polling ... ";
						}));
					},
					onPollSuccesful: () => 
					{
						this.Invoke((Action)(() =>
						{
							this.tspLabel.Text = "User authenticated, will check again in 1 minute ... ";
						}));
					}
				);
			};
		}

		private void tspForcePollButton_ButtonClick(object sender, EventArgs e)
		{
			this.lkAsyncHelper.ForcePollNow();
		}
	}
}
