using System;
using System.ComponentModel;
using System.Mvvm;
using System.Windows.Forms;

namespace AgileTeam.UI.WF.Views
{
	internal class ViewBase : Form
	{
		public IViewModel DataContext { get; set; }

		public bool IsClosed { get; private set; }

		public bool IsClosing { get; private set; }

		protected override void OnClosing(CancelEventArgs e)
		{
			IsClosing = true;
			base.OnClosing(e);
		}

		protected override void OnClosed(EventArgs e)
		{
			IsClosed = true;
			base.OnClosed(e);
		}
	}
}