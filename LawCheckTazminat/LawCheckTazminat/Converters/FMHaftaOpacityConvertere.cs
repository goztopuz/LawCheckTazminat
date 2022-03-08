using System;
using System.Globalization;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class FMHaftaOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var durum = (string)value;

            if(durum=="İzin")
            {
                return 0.25;
            }else
            {
                return 1;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
