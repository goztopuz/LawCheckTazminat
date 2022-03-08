using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class FMHaftaDuzenle : ContentPage
    {
        public FMHaftaDuzenle(FazlaMesaiHaftalar _hafta, FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new FMHaftaDuzenleViewModel(_hafta, _fm);
        }
    }
}
