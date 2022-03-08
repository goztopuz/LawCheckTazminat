using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.YillikIzinB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.YillikIzinV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak3BYYView : ContentPage
    {
        public Basamak3BYYView(YillikIzin _yillik)
        {
            InitializeComponent();

            this.BindingContext = new Basamak3BYYViewModel(_yillik);
        }
    }
}