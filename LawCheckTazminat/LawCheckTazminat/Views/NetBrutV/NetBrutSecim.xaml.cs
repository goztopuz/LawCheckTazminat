using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.NetBrutV
{
    public partial class NetBrutSecim : ContentPage
    {
        public NetBrutSecim()
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.NetBrut.NetBrutSecimViewModel();
        }
    }
}
