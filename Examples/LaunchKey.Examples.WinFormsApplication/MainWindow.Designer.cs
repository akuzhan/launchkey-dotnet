namespace LaunchKey.Examples.WinFormsApplication
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.tspLblLaunchKeyStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlTreasure = new System.Windows.Forms.Panel();
			this.pnlLogin = new System.Windows.Forms.Panel();
			this.tspLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tspForcePollButton = new System.Windows.Forms.ToolStripSplitButton();
			this.lkLoginControl = new LaunchKey.Examples.WinFormsApplication.LaunchKeyLoginControl();
			this.treasureControl = new LaunchKey.Examples.WinFormsApplication.TreasureControl();
			this.statusStrip.SuspendLayout();
			this.pnlTreasure.SuspendLayout();
			this.pnlLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspLblLaunchKeyStatus,
            this.tspLabel,
            this.tspForcePollButton});
			this.statusStrip.Location = new System.Drawing.Point(0, 608);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(812, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// tspLblLaunchKeyStatus
			// 
			this.tspLblLaunchKeyStatus.Name = "tspLblLaunchKeyStatus";
			this.tspLblLaunchKeyStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// pnlTreasure
			// 
			this.pnlTreasure.Controls.Add(this.treasureControl);
			this.pnlTreasure.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlTreasure.Location = new System.Drawing.Point(0, 0);
			this.pnlTreasure.Name = "pnlTreasure";
			this.pnlTreasure.Size = new System.Drawing.Size(812, 608);
			this.pnlTreasure.TabIndex = 2;
			// 
			// pnlLogin
			// 
			this.pnlLogin.Controls.Add(this.lkLoginControl);
			this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlLogin.Location = new System.Drawing.Point(0, 0);
			this.pnlLogin.Name = "pnlLogin";
			this.pnlLogin.Size = new System.Drawing.Size(812, 608);
			this.pnlLogin.TabIndex = 2;
			// 
			// tspLabel
			// 
			this.tspLabel.Name = "tspLabel";
			this.tspLabel.Size = new System.Drawing.Size(0, 17);
			// 
			// tspForcePollButton
			// 
			this.tspForcePollButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tspForcePollButton.ForeColor = System.Drawing.Color.MediumBlue;
			this.tspForcePollButton.Image = ((System.Drawing.Image)(resources.GetObject("tspForcePollButton.Image")));
			this.tspForcePollButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tspForcePollButton.Name = "tspForcePollButton";
			this.tspForcePollButton.Size = new System.Drawing.Size(116, 20);
			this.tspForcePollButton.Text = "Force Check Now";
			this.tspForcePollButton.Visible = false;
			this.tspForcePollButton.ButtonClick += new System.EventHandler(this.tspForcePollButton_ButtonClick);
			// 
			// lkLoginControl
			// 
			this.lkLoginControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lkLoginControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.lkLoginControl.Location = new System.Drawing.Point(0, 0);
			this.lkLoginControl.Name = "lkLoginControl";
			this.lkLoginControl.Size = new System.Drawing.Size(812, 608);
			this.lkLoginControl.TabIndex = 0;
			// 
			// treasureControl
			// 
			this.treasureControl.BackColor = System.Drawing.Color.White;
			this.treasureControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treasureControl.Location = new System.Drawing.Point(0, 0);
			this.treasureControl.Name = "treasureControl";
			this.treasureControl.Size = new System.Drawing.Size(812, 608);
			this.treasureControl.TabIndex = 0;
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(812, 630);
			this.Controls.Add(this.pnlLogin);
			this.Controls.Add(this.pnlTreasure);
			this.Controls.Add(this.statusStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainWindow";
			this.Text = "My Treasures ...";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.pnlTreasure.ResumeLayout(false);
			this.pnlLogin.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel tspLblLaunchKeyStatus;
		private System.Windows.Forms.Panel pnlTreasure;
		private System.Windows.Forms.Panel pnlLogin;
		private LaunchKeyLoginControl lkLoginControl;
		private TreasureControl treasureControl;
		private System.Windows.Forms.ToolStripStatusLabel tspLabel;
		private System.Windows.Forms.ToolStripSplitButton tspForcePollButton;
	}
}