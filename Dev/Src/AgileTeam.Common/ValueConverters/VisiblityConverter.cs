using System;
using System.Globalization;
using System.Mvvm;
using System.Windows;
using System.Windows.Data;

namespace AgileTeam.Common.ValueConverters
{
	internal class VisiblityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is VMVisibility)
				return GetVisibility((VMVisibility)value);

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
				return GetViewModelVisibility((Visibility)value);

			return null;
		}

		private static VMVisibility GetViewModelVisibility(Visibility visibility)
		{
			switch (visibility)
			{
				case Visibility.Visible:
					return VMVisibility.Visible;

				case Visibility.Hidden:
					return VMVisibility.Hidden;

				case Visibility.Collapsed:
					return VMVisibility.Collapsed;

				default:
					throw new ArgumentOutOfRangeException("visibility");
			}
		}

		private static Visibility GetVisibility(VMVisibility vmVisibility)
		{
			switch (vmVisibility)
			{
				case VMVisibility.Visible:
					return Visibility.Visible;

				case VMVisibility.Hidden:
					return Visibility.Hidden;

				case VMVisibility.Collapsed:
					return Visibility.Collapsed;

				default:
					throw new ArgumentOutOfRangeException("vmVisibility");
			}
		}
	}
}