using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.IsgucuKayipB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.IsgucuKayipV
{
    public partial class YeniKayip : ContentPage
    {
        public YeniKayip(IsgucuKayip _isgucu, KayipOran _oran, ObservableCollection<KayipOran> liste1)
        {

            InitializeComponent();

            this.BindingContext = new YeniKayipViewModel(_isgucu, _oran, liste1);

        }
    }
}
