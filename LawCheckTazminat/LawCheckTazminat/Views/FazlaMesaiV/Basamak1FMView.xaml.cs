using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class Basamak1FMView : ContentPage
    {
        public Basamak1FMView(FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new Basamak1FMViewModel(_fm);
        }
    }
}
