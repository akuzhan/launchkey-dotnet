namespace LaunchKey.Examples.WinFormsApplication
{
	partial class TreasureControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreasureControl));
			this.btnLogOut = new System.Windows.Forms.Button();
			this.pbTreasure = new System.Windows.Forms.PictureBox();
			this.lblTreasureReward = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbTreasure)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLogOut
			// 
			this.btnLogOut.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnLogOut.Location = new System.Drawing.Point(335, 641);
			this.btnLogOut.Name = "btnLogOut";
			this.btnLogOut.Size = new System.Drawing.Size(177, 23);
			this.btnLogOut.TabIndex = 5;
			this.btnLogOut.Text = "I\'m bored. Log me out.";
			this.btnLogOut.UseVisualStyleBackColor = true;
			// 
			// pbTreasure
			// 
			this.pbTreasure.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pbTreasure.Image = ((System.Drawing.Image)(resources.GetObject("pbTreasure.Image")));
			this.pbTreasure.Location = new System.Drawing.Point(199, 114);
			this.pbTreasure.Name = "pbTreasure";
			this.pbTreasure.Size = new System.Drawing.Size(448, 409);
			this.pbTreasure.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbTreasure.TabIndex = 3;
			this.pbTreasure.TabStop = false;
			// 
			// lblTreasureReward
			// 
			this.lblTreasureReward.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTreasureReward.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTreasureReward.Location = new System.Drawing.Point(20, 557);
			this.lblTreasureReward.Name = "lblTreasureReward";
			this.lblTreasureReward.Size = new System.Drawing.Size(807, 48);
			this.lblTreasureReward.TabIndex = 4;
			this.lblTreasureReward.Text = "You found the treasure. That was easy.";
			this.lblTreasureReward.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TreasureControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.btnLogOut);
			this.Controls.Add(this.pbTreasure);
			this.Controls.Add(this.lblTreasureReward);
			this.Name = "TreasureControl";
			this.Size = new System.Drawing.Size(847, 779);
			((System.ComponentModel.ISupportInitialize)(this.pbTreasure)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnLogOut;
		private System.Windows.Forms.PictureBox pbTreasure;
		private System.Windows.Forms.Label lblTreasureReward;
	}
}
