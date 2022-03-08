using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.KidemIhbarB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.KidemIhbarV
{
    public partial class Basamak1IKView : ContentPage
    {
        public Basamak1IKView(KidemIhbar _ik)
        {
            InitializeComponent();
            this.BindingContext = new Basamak1IKViewModel(_ik);

        }
    }
}
