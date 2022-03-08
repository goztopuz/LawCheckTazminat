using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Input;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.ViewModels.Base;
using Syncfusion.Data.Extensions;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.NetBrut
{
    public class BrutNetSonViewModel  : ViewModelBase
    {
        public BrutNetSonViewModel(List<NetBrutAylar>  _aylar)
        {

            this.Liste = new ObservableCollection<NetBrutAylar>();

            Liste = _aylar.ToObservableCollection();


        }


        private ObservableCollection<NetBrutAylar> _liste;
        public ObservableCollection<NetBrutAylar> Liste
        {
            get => _liste;
            set
            {

                _liste = value;
                OnPropertyChanged();

            }
        }



        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        private async void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            await Application.Current.MainPage.Navigation.PopModalAsync();



            this.HataMesaji = "";
            IsBusy = false;
        }

        public ICommand BitisCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new Views.AnaSayfaV.Anasayfa();

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;

        }


        private string _hataMesaji;
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        public ICommand RaporAlCommand => new Command(OnRaporAl);
        private async void OnRaporAl(object obj)
        {
            if(IsBusy== true)
            {
                return;

            }
            IsBusy = true;

            //----
            RaporAl();


            IsBusy = false;

        }

        async private void RaporAl()
        {

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Orientation = PageOrientation.Landscape;
            section.PageSetup.Margins.Top = 72;
            section.PageSetup.Margins.Left = 25;
            section.PageSetup.Margins.Right = 10;
            section.PageSetup.Margins.Bottom = 72;

            section.PageSetup.PageSize = PageSize.A4;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            WTextRange textRange;

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Brütten Net Hesaplama".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";



            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 12;
            cf3.FontName = "Times New Roman";

            IWTable tabloDonemler = section.AddTable();
            tabloDonemler.TableFormat.Borders.LineWidth = 0.5f;
            tabloDonemler.ApplyStyle(BuiltinTableStyle.LightGrid);

            int donemSatSay = 13;

            tabloDonemler.ResetCells(donemSatSay, 8);

        





            tabloDonemler[0, 0].AddParagraph().AppendText("\n Ay\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 1].AddParagraph().AppendText("\n Brüt \t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 2].AddParagraph().AppendText("\n Net \t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 3].AddParagraph().AppendText("\n  Net\n (AGİ'li)\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 4].AddParagraph().AppendText("\n Gelir Vergisi \t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 5].AddParagraph().AppendText("\n SGK(İşci)\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 6].AddParagraph().AppendText("\n Damga Verg.\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 7].AddParagraph().AppendText("\n  İşsizlik \t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 0].Width = 35;
            tabloDonemler[0, 1].Width = 75;
            tabloDonemler[0, 2].Width = 75;
            tabloDonemler[0, 3].Width = 75;
            tabloDonemler[0, 4].Width = 75;
            tabloDonemler[0, 5].Width = 75;
            tabloDonemler[0, 6].Width = 75;
            tabloDonemler[0, 7].Width = 75;

            int ii = 0;
            foreach (var t in Liste)
            {
                ii = ii + 1;
                tabloDonemler.Rows[ii].Height = 32;

                tabloDonemler[ii, 0].AddParagraph().AppendText(ii.ToString() + "\t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 0].Width = 35;


                string brutYazi = String.Format("{0:C2}", t.brut);
                tabloDonemler[ii, 1].AddParagraph().AppendText(brutYazi + "  \t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 1].Width = 75;

                string netYazi = String.Format("{0:C2}", t.net);
                tabloDonemler[ii, 2].AddParagraph().AppendText(netYazi + "\t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 2].Width = 75;

                string netAgiliYazi = String.Format("{0:C2}", t.netAgili);
                tabloDonemler[ii, 3].AddParagraph().AppendText(netAgiliYazi).ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 3].Width = 75;

                string gelirVergiYazi= String.Format("{0:C2}", t.gelirVergisi);
                tabloDonemler[ii, 4].AddParagraph().AppendText( gelirVergiYazi + " \t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 4].Width = 75;


                string sgkYazi = String.Format("{0:C2}", t.sgk);                
                tabloDonemler[ii, 5].AddParagraph().AppendText("" + sgkYazi + "\t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 5].Width = 75;


                string damgaYazi = String.Format("{0:C2}", t.damgaVergisi);
                tabloDonemler[ii, 6].AddParagraph().AppendText("" + damgaYazi + "\t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[6].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 6].Width = 75;

                string issizlilkYazi = String.Format("{0:C2}", t.issizlik);
                tabloDonemler[ii, 7].AddParagraph().AppendText(" " + issizlilkYazi + " \t ").ApplyCharacterFormat(cf3);
                tabloDonemler.Rows[ii].Cells[7].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloDonemler[ii, 7].Width = 75;

            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "BruttenNetRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //Save the stream as a file in the device and invoke it for viewing
            //var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("BrutnetRapor.docx", "application/msword", stream);

        }


    }
}
