using System;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class FMHaftalikIzınView : ContentPage
    {
        public FMHaftalikIzınView(FazlaMesaiHaftalikIzinHaftalar _hafta, FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new FMHaftalikIzinViewModel(_hafta, _fm);
        }
    }
}
