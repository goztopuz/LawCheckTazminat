using System;
using System.Collections.Generic;
using System.IO;
using LawCheckTazminat.Dependency;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    public partial class PdfView1 : ContentPage
    {
        public PdfView1()
        {
            InitializeComponent();
            Stream fileStream;


            string aa = Xamarin.Forms.DependencyService.Get<ISave>().YoluOgrenPdf();

            FileStream fs = new FileStream(aa, FileMode.Open, FileAccess.Read);
            //fs.CopyTo(fileStream);

            //StreamReader r = new StreamReader(fs);

            pdfViewerControl.LoadDocument(fs);


        }
    }
}
