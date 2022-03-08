using Foundation;
using LawCheckTazminat.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimePickerRenderer))]
namespace LawCheckTazminat.iOS.Renderers
{
     public  class TimePickerRenderer : Xamarin.Forms.Platform.iOS.TimePickerRenderer
    {
		protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				UITextField entry = Control;
				var picker = (UIDatePicker)entry.InputView;
				picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
			}
		}
	}
}