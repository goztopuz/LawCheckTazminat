using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak10FMView : ContentPage
    {
        public Basamak10FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak10FMViewModel(_fm);

        }
    }
}
