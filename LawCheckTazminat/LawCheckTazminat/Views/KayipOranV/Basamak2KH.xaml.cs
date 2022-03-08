using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.KayipOranV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak2KH : ContentPage
    {
        public Basamak2KH(AnaKategori _kategori)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.KayipOranB.Basamak2KHViewModel(_kategori);

        }
    }
}