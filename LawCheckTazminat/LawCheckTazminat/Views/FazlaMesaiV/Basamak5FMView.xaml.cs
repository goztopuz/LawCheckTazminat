using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using Xamarin.Forms;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak5FMView : ContentPage
    {
        public Basamak5FMView(FazlaMesai _fm)
        {
            InitializeComponent();

            this.BindingContext = new Basamak5FMViewModel(_fm);

        }
    }
}
