using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak9FMView : ContentPage
    {
        public Basamak9FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak9FMViewModel(_fm);

        }
    }
}
