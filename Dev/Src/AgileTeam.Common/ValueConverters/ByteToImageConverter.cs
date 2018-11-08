using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AgileTeam.Common.ValueConverters
{
	internal class ByteToImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var bytes = value as byte[];
			if (bytes == null || bytes.Length <= 0)
				return new BitmapImage(new Uri("../Assets/contact-new.png", UriKind.Relative));

			using (var stream = new MemoryStream(bytes))
			{
				var image = new BitmapImage();
				image.BeginInit();
				image.StreamSource = stream;
				image.EndInit();
				return image;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var renderTargetBitmap = value as RenderTargetBitmap;

			if (null == renderTargetBitmap)
				return null;
			var bitmapImage = new BitmapImage();
			var bitmapEncoder = new BmpBitmapEncoder();

			bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

			using (var stream = new MemoryStream())
			{
				bitmapEncoder.Save(stream);
				stream.Seek(0, SeekOrigin.Begin);
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = stream;
				bitmapImage.EndInit();
			}

			var encoder = new JpegBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

			byte[] data;

			using (var ms = new MemoryStream())
			{
				encoder.Save(ms);
				data = ms.ToArray();
			}

			return data;
		}
	}
}