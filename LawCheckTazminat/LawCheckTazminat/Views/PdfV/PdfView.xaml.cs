using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.PdfV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfView : ContentPage
    {
        public PdfView(MemoryStream stream, string documentName, string documentExtension)
        {
            InitializeComponent();

            this.BindingContext = new ViewModels.PdfViewB.PdfViewModel(stream, documentName, documentExtension);


        }
    }
}