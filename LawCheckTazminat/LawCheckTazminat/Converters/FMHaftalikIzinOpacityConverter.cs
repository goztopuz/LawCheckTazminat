using System;
using System.Globalization;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class FMHaftalikIzinOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string deger = (string)value;

            if(deger !="İzin")
            {
                return 1;
            }else
            {
                return 0.25;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
