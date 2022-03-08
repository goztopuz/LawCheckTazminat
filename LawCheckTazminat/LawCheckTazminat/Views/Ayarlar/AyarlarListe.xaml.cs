using LawCheckTazminat.ViewModels.Ayarlar;
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
    public partial class AyarlarListe : ContentPage
    {
        public AyarlarListe()
        {
            InitializeComponent();
            this.BindingContext = new AyarlarListeViewModel();
        }
    }
}