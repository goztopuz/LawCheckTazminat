using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.KidemIhbarB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.KidemIhbarV
{
    public partial class Basamak2IKView : ContentPage
    {
        public Basamak2IKView(KidemIhbar _ik)
        {
            InitializeComponent();

            this.BindingContext = new Basamak2IKViewModel(_ik);
        }
    }
}
