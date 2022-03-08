using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak11FMView : ContentPage
    {
        public Basamak11FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak11FMViewModel(_fm);
        }
    }
}
