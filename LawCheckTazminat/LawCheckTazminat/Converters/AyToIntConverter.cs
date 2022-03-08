using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
    public class AyToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int ay = (int)value;
            string ayYazi = "";
            switch(ay)
            {
                case 1:
                    ayYazi = "Ocak";
                    break;
                case 2:
                    ayYazi = "Şubat";
                    break;
                case 3:
                    ayYazi = "Mart";
                    break;
                case 4:
                    ayYazi = "Nisan";
                    break;
                case 5:
                    ayYazi = "Mayıs";
                    break;
                case 6:
                    ayYazi = "Haziran";
                    break;
                case 7:
                    ayYazi = "Temmuz";
                    break;
                case 8:
                    ayYazi = "Ağustos";
                    break;
                case 9:
                    ayYazi = "Eylül";
                    break;
                case 10:
                    ayYazi = "Ekim";
                    break;
                case 11:
                    ayYazi = "Kasım";
                    break;
                case 12:
                    ayYazi = "Aralık";
                    break;
                default:
                    ayYazi = "Ocak";
                    break;
            }

            return ayYazi;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ay = (string)value;

            int aysayi=1;
            switch(ay)
            {
                case "Ocak":
                    aysayi = 1;
                    break;
                case "Şubat":
                    aysayi = 2;
                    break;
                case "Mart":
                    aysayi = 3;
                    break;
                case "Nisan":
                    aysayi = 4;
                    break;
                case "Mayıs":
                    aysayi = 5;
                    break;
                case "Haziran":
                    aysayi = 6;
                    break;
                case "Temmuz":
                    aysayi = 7;
                    break;
                case "Ağustos":
                    aysayi = 8;
                    break;
                case "Eylül":
                    aysayi = 9;
                    break;
                case "Ekim":
                    aysayi = 10;
                    break;
                case "Kasım":
                    aysayi = 11;
                    break;
                case "Aralık":
                    aysayi = 12;
                    break;

            }

            return aysayi;


        }
    }
}
