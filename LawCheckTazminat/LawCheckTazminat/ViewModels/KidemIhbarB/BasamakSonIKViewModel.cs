using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.KidemIhbarV;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KidemIhbarB
{
    public class BasamakSonIKViewModel: ViewModelBase
    {

        HesaplaKidemIhbar islemHesapla = new HesaplaKidemIhbar();
        KidemTavanService islem = new KidemTavanService();

        decimal brutTavan = 0;
        bool brutTavanVar = false;

        public BasamakSonIKViewModel(KidemIhbar _kidem)
        {

            this.IK = new KidemIhbar();
            this.IK = _kidem;


            var kL = islem.Listele().ToList();

            KidemTavan bt = new KidemTavan();

            bt = kL.Find(o => o.baslangic < _kidem.BitisTarihi && o.bitis > _kidem.BitisTarihi);

            DateTime sontarih = kL[0].bitis;
            if(_kidem.BitisTarihi> sontarih)
            {
                brutTavan = kL[0].miktar;
            }

            if(bt != null)
            {
                brutTavan = bt.miktar;
            }

            Hesapla();

        }

        private KidemIhbar _ik;
        public KidemIhbar IK
        {
            get => _ik;
            set
            {
                _ik = value;
                OnPropertyChanged();
            }
        }


        private async void Hesapla()
        {
            // Kıdem Tazminatı Bölümü....
            IK.GiydirilmisBrutUcret = IK.BrutUcret + IK.EkUcretler;

            if((IK.GiydirilmisBrutUcret>brutTavan) &&(brutTavan !=0))
            {
                IK.GiydirilmisBrutUcret = brutTavan;
                brutTavanVar = true;
            }
            Double damgaVergi = 0.00759;

            var sonuc = islemHesapla.KidemHesapla(IK.BaslangicTarihi,
                IK.BitisTarihi, IK.GiydirilmisBrutUcret, damgaVergi);


            IK.KidemSonucToplam = sonuc.brutCarpilmisToplam;
            IK.KidemSonucDV = sonuc.damgaVergiOdenecek;
            IK.KidemSonucNet = sonuc.netKıdemTazminati;
            KidemYazi = sonuc.yil.ToString() + " Yıl " + sonuc.ay.ToString() + " Ay " +
                                    sonuc.gun.ToString() + " Gün";

            // İhbar Tazminatı Bölümü....
            Double gelirVergi = 0.15;
            IK.GiydirilmisBrutUcret = IK.BrutUcret + IK.EkUcretler;
            var sonuc2 = islemHesapla.IhbarHesapla(IK.BaslangicTarihi, IK.BitisTarihi, IK.GiydirilmisBrutUcret,
                damgaVergi, gelirVergi, IK.VergiYili, IK.VergiMatrah, IK.EkGelir);

            IK.IhbarSonucToplam = sonuc2.brutIhbarTazminati;
            IK.IhbarSonucNet = sonuc2.netIhbarTazminati;
            IK.IhbarSonucGV = sonuc2.gelirVergiOdenecek;
            IK.IhbarSonucDV = sonuc2.damgaVergiOdenecek;
            IK.IhbarHaftaSayisi = sonuc2.haftaSayisi;

            IhbarYazi = IK.IhbarHaftaSayisi.ToString() + " Hafta";

        }

        private string _kidemYazi;
        public string KidemYazi
        {
            get => _kidemYazi;
            set
            {
                _kidemYazi = value;
                OnPropertyChanged();
            }
        }


        private string _ihbarYazi;
        public string IhbarYazi
        {
            get => _ihbarYazi;
            set
            {
                _ihbarYazi = value;
                OnPropertyChanged();
            }
        }

        public ICommand RaporAlCommand => new Command(OnRaporAl);
        private async void OnRaporAl(object obj)
        {
            RaporAl();
        }

       

        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        private async void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            IK.duzenlemede = true;

            //----
            var sayfa = new Basamak1IKView(IK);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



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

        private async void RaporAl()
        {

            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section

            section.PageSetup.Margins.Top = 72;
            section.PageSetup.Margins.Left = 25;
            section.PageSetup.Margins.Right = 10;
            section.PageSetup.Margins.Bottom = 72;

            section.PageSetup.PageSize = PageSize.A4;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Kıdem İhbar Tazminatı Rapor".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tablooBaslangic = section.AddTable();

            tablooBaslangic.TableFormat.Borders.LineWidth = 0.0f;
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";
            tablooBaslangic.ResetCells(8, 2);
            tablooBaslangic.IndentFromLeft = 50;

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 13;
            cf3.FontName = "Times New Roman";

            tablooBaslangic[0, 0].Width = 200;
            tablooBaslangic[0, 1].Width = 200;
            tablooBaslangic[1, 0].Width = 200;
            tablooBaslangic[1, 1].Width = 200;
            tablooBaslangic[2, 0].Width = 200;
            tablooBaslangic[2, 1].Width = 200;
            tablooBaslangic[3, 0].Width = 200;
            tablooBaslangic[3, 1].Width = 200;
            tablooBaslangic[4, 0].Width = 200;
            tablooBaslangic[4, 1].Width = 200;
            tablooBaslangic[5, 0].Width = 200;
            tablooBaslangic[5, 1].Width = 200;
            tablooBaslangic[6, 0].Width = 200;
            tablooBaslangic[6, 1].Width = 200;
            tablooBaslangic[7, 0].Width = 200;
            tablooBaslangic[7, 1].Width = 200;


            tablooBaslangic.Rows[0].Height = 35;
            tablooBaslangic.Rows[1].Height = 35;
            tablooBaslangic.Rows[2].Height = 35;
            tablooBaslangic.Rows[3].Height = 35;
            tablooBaslangic.Rows[4].Height = 35;
            tablooBaslangic.Rows[5].Height = 35;
            tablooBaslangic.Rows[6].Height = 35;
            tablooBaslangic.Rows[7].Height = 35;

            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[7].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[7].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            //  tablooBalangic.ApplyStyle(BuiltinTableStyle.DarkList);
            tablooBaslangic[0, 0].AddParagraph().AppendText("Ad Soyad :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[0, 1].AddParagraph().AppendText("  ---------------------" + "\t").ApplyCharacterFormat(cf3);
            tablooBaslangic[1, 0].AddParagraph().AppendText(" İşe Giriş Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[1, 1].AddParagraph().AppendText( IK.BaslangicTarihi.ToShortDateString() + "\t").ApplyCharacterFormat(cf3);
            tablooBaslangic[2, 0].AddParagraph().AppendText(" İşten Çıkış Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[2, 1].AddParagraph().AppendText(" " + IK.BitisTarihi.ToShortDateString() + "\t ").ApplyCharacterFormat(cf3);

            tablooBaslangic[3, 0].AddParagraph().AppendText(" Brüt Ücret :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string _brutUcret = String.Format("{0:C2}", IK.BrutUcret);
            tablooBaslangic[3, 1].AddParagraph().AppendText(" " + _brutUcret + "\t".ToUpper()).ApplyCharacterFormat(cf3);

            tablooBaslangic[4, 0].AddParagraph().AppendText(" Ek Ödemeler :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string ekUcret = String.Format("{0:C2}", IK.EkUcretler);
            tablooBaslangic[4, 1].AddParagraph().AppendText(" " + ekUcret + "\t".ToUpper()).ApplyCharacterFormat(cf3);

            // Farklı Gelirler Toplamı
            tablooBaslangic[5, 0].AddParagraph().AppendText(" Ek Ödemeler :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string ekGelir = String.Format("{0:C2}", IK.EkGelir);
            tablooBaslangic[5, 1].AddParagraph().AppendText(" " + ekUcret + "\t").ApplyCharacterFormat(cf3);

            tablooBaslangic[6, 0].AddParagraph().AppendText(" Vergi Yılı :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            tablooBaslangic[6, 1].AddParagraph().AppendText("\n"+IK.VergiYili +"\t \n").ApplyCharacterFormat(cf3);

            // Vergi Matrah
            tablooBaslangic[7, 0].AddParagraph().AppendText(" Vergi Matrahı :\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string vergiMatrah = String.Format("{0:C2}", IK.VergiMatrah);
            tablooBaslangic[7, 1].AddParagraph().AppendText(" " + vergiMatrah + "\t").ApplyCharacterFormat(cf3);

            paragraph = section.AddParagraph();

           


            if (brutTavanVar == true)
            {
                
            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Kıdem Tazminatı") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            textRange = paragraph.AppendText("\t\t" + KidemYazi) as WTextRange;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.5f;
            tabloo.TableFormat.CellSpacing = 0;

            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.IndentFromLeft = 50;


            tabloo.ResetCells(3, 2);
            tabloo[0, 0].Width = 160;
            tabloo[0, 1].Width = 160;
            tabloo[1, 0].Width = 160;
            tabloo[1, 1].Width = 160;
            tabloo[2, 0].Width = 160;
            tabloo[2, 1].Width = 160;

            tabloo.Rows[0].Height = 35;
            tabloo.Rows[1].Height = 35;
            tabloo.Rows[2].Height = 35;

            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo[0, 0].AddParagraph().AppendText(" Brüt  Tazminat:\t".ToUpper()).ApplyCharacterFormat(cf3);
            string kidemBrut = String.Format("{0:C2}", IK.KidemSonucToplam);
            tabloo[0, 1].AddParagraph().AppendText("   " + kidemBrut + "\t").ApplyCharacterFormat(cf3);

            tabloo[1, 0].AddParagraph().AppendText(" Damga Vergi:\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string kidemDV = String.Format("{0:C2}", IK.KidemSonucDV);
            tabloo[1, 1].AddParagraph().AppendText("   " + kidemDV + "\t").ApplyCharacterFormat(cf3);

            tabloo[2, 0].AddParagraph().AppendText(" Net Tazminat:\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string kidemNet = String.Format("{0:C2}", IK.KidemSonucNet);
            tabloo[2, 1].AddParagraph().AppendText("   " + kidemNet + "\t").ApplyCharacterFormat(cf3);


            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("İhbar Tazminatı".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            textRange = paragraph.AppendText("\t\t" + IhbarYazi) as WTextRange;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();



            IWTable tabloo2 = section.AddTable();

            tabloo2.TableFormat.Borders.LineWidth = 0.4f;

            tabloo2.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo2.ResetCells(4, 2);

            tabloo2.IndentFromLeft = 50;


            tabloo2[0, 0].Width = 160;
            tabloo2[0, 1].Width = 160;
            tabloo2[1, 0].Width = 160;
            tabloo2[1, 1].Width = 160;
            tabloo2[2, 0].Width = 160;
            tabloo2[2, 1].Width = 160;
            tabloo2[3, 0].Width = 160;
            tabloo2[3, 1].Width = 160;


            tabloo2.Rows[0].Height = 35;
            tabloo2.Rows[1].Height = 35;
            tabloo2.Rows[2].Height = 35;
            tabloo2.Rows[3].Height = 35;

            tabloo2.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo2.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;




            tabloo2[0, 0].AddParagraph().AppendText(" Brüt  Tazminat:\t".ToUpper()).ApplyCharacterFormat(cf3);
            string ihbarBrut = String.Format("{0:C2}", IK.IhbarSonucToplam);
            tabloo2[0, 1].AddParagraph().AppendText( ihbarBrut.ToUpper() + "\t").ApplyCharacterFormat(cf3);

            tabloo2[1, 0].AddParagraph().AppendText(" Damga Vergi:\t ").ApplyCharacterFormat(cf3);
            string ihbarDV = String.Format("{0:C2}", IK.IhbarSonucDV);
            tabloo2[1, 1].AddParagraph().AppendText( ihbarDV + "\t").ApplyCharacterFormat(cf3);

            tabloo2[2, 0].AddParagraph().AppendText(" Gelir Vergi:\t ".ToUpper()).ApplyCharacterFormat(cf3);
            string ihbarGV = String.Format("{0:C2}", IK.IhbarSonucGV);
            tabloo2[2, 1].AddParagraph().AppendText(" " + ihbarGV + "\t").ApplyCharacterFormat(cf3);

            tabloo2[3, 0].AddParagraph().AppendText("Net Tazminat: \t".ToUpper()).ApplyCharacterFormat(cf3);
            string ihbarNet = String.Format("{0:C2}", IK.IhbarSonucNet);
            tabloo2[3, 1].AddParagraph().AppendText( ihbarNet + "\t").ApplyCharacterFormat(cf3);


            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "KidemIhbarRapor", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);
            //      var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("KidemIhbar.docx", "application/msword", stream);


        }

    }
}
