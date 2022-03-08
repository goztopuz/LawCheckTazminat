using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LawCheckTazminat.Controls.BorderlessEditor),
    typeof(LawCheckTazminat.Droid.BorderlessEditorRenderer))]
namespace LawCheckTazminat.Droid
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        public BorderlessEditorRenderer() : base(Android.App.Application.Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if(this.Control != null)
            {
                this.Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);

            }

        }

    }
}