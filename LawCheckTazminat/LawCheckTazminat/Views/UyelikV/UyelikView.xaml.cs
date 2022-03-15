using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.UyelikB;


using Xamarin.Forms;

namespace LawCheckTazminat.Views.UyelikV
{
    public partial class UyelikView : ContentPage
    {

        UyelikViewModel vm;
        public UyelikView()
        {
            InitializeComponent();

            this.BindingContext = vm = new UyelikViewModel();

        }
    }
}
