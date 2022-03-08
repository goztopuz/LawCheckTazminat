using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    public partial class RaporBilgiView : ContentPage
    {
        public RaporBilgiView(Models.FazlaMesaiKisiIzinKayitlari _rapor, Models.FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.FazlaMesaiB.RaporBilgiViewModel(_rapor, _fm);

        }
    }
}
