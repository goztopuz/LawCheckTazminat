using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.NetBrut;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.NetBrutV
{
    public partial class BrutNetSon : ContentPage
    {
        public BrutNetSon(List<NetBrutAylar> _aylar)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.NetBrut.BrutNetSonViewModel(_aylar);


        }
    }
}
