using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaunchKey.Examples.WinFormsApplication
{
	public partial class TreasureControl : UserControl
	{
		public event EventHandler LogOutClicked;
		public TreasureControl()
		{
			InitializeComponent();
			this.btnLogOut.Click += (s, e) =>
			{
				btnLogOut.Enabled = false;
				if (LogOutClicked != null)
					LogOutClicked(s, e);
			};
		}

		public void Reset()
		{
			this.btnLogOut.Enabled = true;
		}
	}

}
