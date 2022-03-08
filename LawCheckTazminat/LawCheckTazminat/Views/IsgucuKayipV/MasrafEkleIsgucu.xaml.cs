using System;
using System.Collections.Generic;

using Xamarin.Forms;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class MasrafEkleIsgucu : ContentPage
    {
        public MasrafEkleIsgucu(IsgucuKayip _kayip, Models.MasrafIsgucu _masraf, ObservableCollection<MasrafIsgucu> Liste)
        {
            InitializeComponent();

            this.BindingContext = new MasrafEkleIsgucuViewModel(_kayip, _masraf, Liste);

        }
    }
}
