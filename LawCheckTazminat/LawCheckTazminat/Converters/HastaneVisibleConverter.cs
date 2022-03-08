using System;
using System.Globalization;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class HastaneVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string turr = (string)value;
            if(turr=="Hastane")
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
