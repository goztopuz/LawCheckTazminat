using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class VergiDilimiDetayView : ContentPage
    {
        public VergiDilimiDetayView(VergiDilimleri _vd,ObservableCollection<VergiDilimleri> liste)
        {
            InitializeComponent();
            this.BindingContext = new VergiDilimiDetayViewModel(_vd,liste);
        }
    }
}
