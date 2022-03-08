using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.FazlaMesaiB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.FazlaMesaiV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak5aFMView : ContentPage
    {
        public Basamak5aFMView(FazlaMesai _fm)
        {
            InitializeComponent();
            this.BindingContext = new Basamak5aFMViewModel(_fm);
        }

        private void drpEvliBekar_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
    }
}