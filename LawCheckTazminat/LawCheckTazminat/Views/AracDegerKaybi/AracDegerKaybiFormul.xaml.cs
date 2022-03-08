using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.AracDegerKaybi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AracDegerKaybiFormul : ContentPage
    {
        public AracDegerKaybiFormul()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.AracDegerKaybi.AracDegerKaybiFormulViewModel();

        }
    }
}