using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak10BFMView : ContentPage
    {
        public Basamak10BFMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak10BFMViewModel(_fm);
           
        }
    }
}
