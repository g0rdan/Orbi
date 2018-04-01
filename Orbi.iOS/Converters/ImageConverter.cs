using System;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace Orbi.iOS.Converters
{
    public class ImageConverter : MvxValueConverter<byte[], UIImage>
    {
        protected override UIImage Convert(byte[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var data = NSData.FromArray(value);
            return UIImage.LoadFromData(data);
        }
    }
}
