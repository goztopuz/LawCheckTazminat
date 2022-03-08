using System;
using UIKit;
using LawCheckTazminat.iOS.Renderers;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(DatePicker), typeof(DatePickerRenderer))]
namespace LawCheckTazminat.iOS.Renderers
{
    public class DatePickerRenderer: Xamarin.Forms.Platform.iOS.DatePickerRenderer
    {
		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				UITextField entry = Control;
				UIDatePicker picker = (UIDatePicker)entry.InputView;
				picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
			}
		}


	}
}
