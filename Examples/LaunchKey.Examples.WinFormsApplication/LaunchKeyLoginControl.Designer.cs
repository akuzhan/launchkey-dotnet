namespace LaunchKey.Examples.WinFormsApplication
{
	partial class LaunchKeyLoginControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchKeyLoginControl));
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.btnLogin = new System.Windows.Forms.Button();
			this.lblTreasuresTaunt = new System.Windows.Forms.Label();
			this.lblHeader = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblUsername
			// 
			this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblUsername.Location = new System.Drawing.Point(184, 130);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(197, 24);
			this.lblUsername.TabIndex = 0;
			this.lblUsername.Text = "LaunchKey Username";
			// 
			// txtUsername
			// 
			this.txtUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUsername.Location = new System.Drawing.Point(140, 157);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(289, 29);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
			// 
			// btnLogin
			// 
			this.btnLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnLogin.BackColor = System.Drawing.Color.MediumBlue;
			this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
			this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLogin.ForeColor = System.Drawing.Color.White;
			this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
			this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLogin.Location = new System.Drawing.Point(140, 192);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Padding = new System.Windows.Forms.Padding(15, 5, 0, 0);
			this.btnLogin.Size = new System.Drawing.Size(289, 70);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Log in";
			this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnLogin.UseVisualStyleBackColor = false;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// lblTreasuresTaunt
			// 
			this.lblTreasuresTaunt.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTreasuresTaunt.AutoSize = true;
			this.lblTreasuresTaunt.Location = new System.Drawing.Point(189, 59);
			this.lblTreasuresTaunt.Name = "lblTreasuresTaunt";
			this.lblTreasuresTaunt.Size = new System.Drawing.Size(186, 13);
			this.lblTreasuresTaunt.TabIndex = 4;
			this.lblTreasuresTaunt.Text = "There just might be a reward inside ....";
			// 
			// lblHeader
			// 
			this.lblHeader.AutoSize = true;
			this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeader.Location = new System.Drawing.Point(10, 10);
			this.lblHeader.Name = "lblHeader";
			this.lblHeader.Size = new System.Drawing.Size(371, 37);
			this.lblHeader.TabIndex = 5;
			this.lblHeader.Text = "Log In with LaunchKey.";
			// 
			// LaunchKeyLoginControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblHeader);
			this.Controls.Add(this.lblTreasuresTaunt);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblUsername);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.Name = "LaunchKeyLoginControl";
			this.Size = new System.Drawing.Size(568, 408);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.Label lblTreasuresTaunt;
		private System.Windows.Forms.Label lblHeader;
	}
}
