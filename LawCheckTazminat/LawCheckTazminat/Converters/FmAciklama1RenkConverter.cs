using System;
using System.Globalization;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class FmAciklama1RenkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string sonuc = (string)value;

            if(sonuc=="İzin")
            {
                return Xamarin.Forms.Color.OrangeRed;
            }
            else
            {
                return Xamarin.Forms.Color.Green;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
