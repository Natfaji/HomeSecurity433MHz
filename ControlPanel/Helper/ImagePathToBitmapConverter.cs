using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

public class ImagePathToBitmapConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is string imagePath)
		{
			return new BitmapImage(new Uri($"pack://application:,,,/images/{imagePath}"));
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
