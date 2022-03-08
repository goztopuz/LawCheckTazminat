using LawCheckTazminat.ViewModels.NetBrut;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.NetBrutV
{
    public partial class NetBrutSon : ContentPage
    {
        public NetBrutSon(List<NetBrutAylar> _aylar)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.NetBrut.NetBrutSonViewModel(_aylar);
        }
    }
}
