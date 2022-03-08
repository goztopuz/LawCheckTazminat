using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class Basamak1GCview : ContentPage
    {
        public Basamak1GCview(Models.GeceCalisma _geceCalisma)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.GeceCalismaB.Basmak1GCViewModel(_geceCalisma);

        }
    }
}
