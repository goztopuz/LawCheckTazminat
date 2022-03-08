using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class TrafikToTabloConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //------------------------------------

            var durum = (string)value;
            if(durum=="Evet")
            {
                return "TRH-2010";
            }
            else
            {
                return "PMF-1931";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value;
        }
    }
}
