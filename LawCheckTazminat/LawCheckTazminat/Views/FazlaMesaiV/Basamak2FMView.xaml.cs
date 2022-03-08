using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak2FMView : ContentPage
    {
        public Basamak2FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak2FMViewModel(_fm);
        }



    }
}
