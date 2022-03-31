using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.Destek;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Destek
{
    public partial class YeniDestekView : ContentPage
    {
        public YeniDestekView()
        {
            InitializeComponent();

            this.BindingContext = new YeniDestekViewModel();
        }

        async void Editor_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var height = 40;
                await scrll.ScrollToAsync(0,500, true);
            });
        }
    }
}
