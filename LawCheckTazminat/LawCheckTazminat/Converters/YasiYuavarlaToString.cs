using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class YasiYuavarlaToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int durum = (int)value;
            string yazi = "";

            if (durum== 0)
            {
                yazi = "Yaşı YuvarlaMA(32,6=32)";
            }
            else if(durum==1)
            {
                yazi = "Yaşı Yuvarla(32,6=33)";

            }
            return yazi;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string yazi = (string)value;
        
            if(yazi== "Yaşı YuvarlaMA(32,6=32)")
            {
                return 0;
            }else
            {
                return 1;
            }
        }
    }
}
