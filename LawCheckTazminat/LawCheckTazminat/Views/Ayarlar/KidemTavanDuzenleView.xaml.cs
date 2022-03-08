using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class KidemTavanDuzenleView : ContentPage
    {
        public KidemTavanDuzenleView(Models.KidemTavan kk)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.Ayarlar.KidemTavanDuzenleViewModelDuzenle(kk);
       
        }
    }
}
