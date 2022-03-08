using System;
using System.Globalization;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class AciklamaToBackGroudColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string aciklama = (string)value;

            if(aciklama!="Ek")
            {
                return Color.LightGray;

            }else
            {
                return Color.White;
            }
       
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
