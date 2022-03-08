using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
 
[assembly: ExportRenderer(typeof(LawCheckTazminat.Controls.BorderlessEntry),typeof(LawCheckTazminat.iOS.BorderlessEntryRenderer)) ]
namespace LawCheckTazminat.iOS
{
   public class BorderlessEntryRenderer: EntryRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(this.Control != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;
            }

        }

    }
}