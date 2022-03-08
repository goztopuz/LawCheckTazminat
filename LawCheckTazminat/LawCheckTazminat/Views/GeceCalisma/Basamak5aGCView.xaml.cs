using LawCheckTazminat.ViewModels.GeceCalismaB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.GeceCalisma
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak5aGCView : ContentPage
    {
        public Basamak5aGCView(Models.GeceCalisma _gece)
        {
            InitializeComponent();

            this.BindingContext = new Basamak5aGCViewModel(_gece);

        }

        private void drpEvliBekar_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {

        }
    }
}