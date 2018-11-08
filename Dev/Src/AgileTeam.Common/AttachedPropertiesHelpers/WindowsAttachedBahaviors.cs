using System;
using System.Windows;
using System.Windows.Input;

namespace AgileTeam.Common.AttachedPropertiesHelpers
{
	public class WindowsAttachedBahaviors
	{
		public static ICommand GetClosed(DependencyObject dependancyObject)
		{
			return (ICommand)dependancyObject.GetValue(ClosedProperty);
		}

		public static void SetClosed(DependencyObject obj, ICommand value)
		{
			obj.SetValue(ClosedProperty, value);
		}

		public static readonly DependencyProperty ClosedProperty =
			DependencyProperty.RegisterAttached("Closed", typeof(ICommand),
			typeof(WindowsAttachedBahaviors), new UIPropertyMetadata(ClosedChanged));

		private static void ClosedChanged(DependencyObject targate, DependencyPropertyChangedEventArgs e)
		{
			if (targate is Window)
			{
				var window = targate as Window;

				if (e.NewValue != null)
					window.Closed += Window_Closed;
				else
					window.Closed -= Window_Closed;
			}
			else
			{
				throw new InvalidOperationException(string.Format("The Type {0} is not a System.Windows.Window", targate.GetType()));
			}
		}

		private static void Window_Closed(object sender, EventArgs e)
		{
			var closed = GetClosed(sender as Window);

			if (closed != null)
			{
				closed.Execute(null);
			}
		}
	}
}