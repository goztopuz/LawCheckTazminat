using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class ResmiTatilDuzenleView : ContentPage
    {
        public ResmiTatilDuzenleView(ResmiTatiller _tatil, ObservableCollection<ResmiTatiller> liste)
        {
            InitializeComponent();

            this.BindingContext = new ResmiTatilDuzenleViewModel(_tatil, liste);

        }



    }
}
