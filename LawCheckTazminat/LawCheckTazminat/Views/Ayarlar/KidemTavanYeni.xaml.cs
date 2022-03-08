using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.Ayarlar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KidemTavanYeni : ContentPage
    {
        public KidemTavanYeni(List<KidemTavan> _ll)
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.Ayarlar.KidemTavanDuzenleViewModelYeni(_ll);

        }

        private void drpDonemi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}