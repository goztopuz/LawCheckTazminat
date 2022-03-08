using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Converters
{
   public  class ItemTappedConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemTappedEventArgs eventArgs)
            {
                return eventArgs.Item;
            }else if(value is Syncfusion.ListView.XForms.ItemTappedEventArgs eventArgs2)
            {
                return eventArgs2.ItemData;
            }
             
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
