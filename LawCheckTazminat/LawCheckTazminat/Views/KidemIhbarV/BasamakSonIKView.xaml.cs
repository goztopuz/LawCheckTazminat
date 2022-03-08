using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.KidemIhbarB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.KidemIhbarV
{
    public partial class BasamakSonIKView : ContentPage
    {
        public BasamakSonIKView(KidemIhbar _kidem)
        {
            InitializeComponent();

            this.BindingContext = new BasamakSonIKViewModel(_kidem);

        }
    }
}
