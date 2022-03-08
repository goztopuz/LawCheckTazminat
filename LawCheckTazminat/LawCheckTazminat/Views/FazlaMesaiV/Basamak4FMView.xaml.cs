using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak4FMView : ContentPage
    {
        public Basamak4FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak4FMViewModel(_fm);

        }
    }
}
