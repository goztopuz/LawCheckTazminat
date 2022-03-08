using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.YillikIzinV;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.YillikIzinB
{
    public class BasamakYYSonViewModel :ViewModelBase
    {

        Services.HesaplaVergi islemVergi = new Services.HesaplaVergi();
        decimal _gunlukUcret = 0;
        public BasamakYYSonViewModel(YillikIzin _yillik)
        {
            this.YY = new YillikIzin();
            this.YY = _yillik;

            Hesapla();

        }


        private YillikIzin _yy;
        public YillikIzin YY
        {
            get => _yy;
            set
            {
                _yy = value;
                OnPropertyChanged();
            }
        }

        private void Hesapla()
        {

            decimal _brutToplam = 0;

            
            Double damgaVergi = 0.00759;



            _gunlukUcret = YY.BrutUcret / 30;

            YY.Toplam =(decimal) YY.Gun2 * _gunlukUcret;

            YY.SGK = YY.Toplam * Convert.ToDecimal(0.14);
            YY.DamgaV = YY.Toplam * Convert.ToDecimal(damgaVergi);
            YY.Issizlik = YY.Toplam * (Convert.ToDecimal(0.01));

            decimal dusecekMiktar = YY.SGK + YY.DamgaV + YY.Issizlik;

            decimal mik1 = YY.Toplam - dusecekMiktar;
            decimal gelirVergi = islemVergi.VergiHesapla(
                Convert.ToDecimal(YY.VergiMatrah), mik1, Convert.ToInt32(YY.VergiYil));

            YY.Vergi = gelirVergi;

            YY.Net = YY.Toplam - YY.Vergi - dusecekMiktar;

        }


        public ICommand RaporAlCommand => new Command(OnRaporAl);
        private async void OnRaporAl(object obj)
        {

            YYRapor();
        }


        //------

        public ICommand ElleHesaplaCommand => new Command(OnElleHesapla);
        private async void OnElleHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            var sayfa = new YYElleView(YY);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }

        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        private async void OnYenidenHesapla(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            YY.duzenlemede = true;

            //----
            var sayfa = new Basamak1YYView(YY);
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

            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa) ;

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


       async private void YYRapor()
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
            textRange = paragraph.AppendText("Yıllık İzin Tazminat Hesaplama".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tablooBaslangic = section.AddTable();

            tablooBaslangic.TableFormat.Borders.LineWidth = 0.2f;
            tablooBaslangic.TableFormat.CellSpacing = 0;
            tablooBaslangic.ApplyStyle(BuiltinTableStyle.LightGrid);
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName= "Times New Roman";

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 20;
            cf3.FontName = "Times New Roman";

            //  tablooBalangic.ApplyStyle(BuiltinTableStyle.DarkList);
            tablooBaslangic.ResetCells(11, 2);
            tablooBaslangic[0, 0].AddParagraph().AppendText(" Ad Soyad : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[0].Height = 41;
            tablooBaslangic[0, 0].Width = 200;
            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            

            tablooBaslangic[0, 1].AddParagraph().AppendText("   " + "--------------------" + "").ApplyCharacterFormat(cf1);
            tablooBaslangic[0, 1].Width = 200;
            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[1, 0].AddParagraph().AppendText(" İşe Giriş Tarihi :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[1].Height = 41;
            tablooBaslangic[1, 0].Width = 200;
            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[1, 1].AddParagraph().AppendText(" " + YY.IseGirisTarihi.ToShortDateString() + "").ApplyCharacterFormat(cf1);
            tablooBaslangic[1, 1].Width = 200;
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[2, 0].AddParagraph().AppendText(" Bitiş Tarihi :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[2].Height = 41;
            tablooBaslangic[2, 0].Width = 200;
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[2, 1].AddParagraph().AppendText(" " + YY.HesapBitisTar.ToShortDateString() + "").ApplyCharacterFormat(cf1);
            tablooBaslangic[2, 1].Width = 200;
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[3, 0].AddParagraph().AppendText("Brüt Ücret :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[3].Height = 41;
            tablooBaslangic[3, 0].Width = 200;
            tablooBaslangic.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string _brutUcret = String.Format("{0:C2}", YY.BrutUcret);
            tablooBaslangic[3, 1].AddParagraph().AppendText(" " + _brutUcret + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[3, 1].Width = 200;
            tablooBaslangic.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[4, 0].AddParagraph().AppendText(" Son 5 Yıl Hak Edilen Toplamı : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[4].Height = 41;
            tablooBaslangic[4, 0].Width = 200;
            tablooBaslangic.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[4, 1].AddParagraph().AppendText(" "+ YY.Gun.ToString()  +" ").ApplyCharacterFormat(cf1);
            tablooBaslangic[4, 1].Width = 200;
            tablooBaslangic.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[5, 0].AddParagraph().AppendText(" Son 5 Yıl Kullanılan  Toplamı: ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[5].Height = 41;
            tablooBaslangic[5, 0].Width = 200;
            tablooBaslangic.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[5, 1].AddParagraph().AppendText("  " + YY.Kullanilan + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[5, 1].Width = 200;
            tablooBaslangic.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[6, 0].AddParagraph().AppendText(" İzinden Düşülecek Haftalık İzinler : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[6].Height = 41;
            tablooBaslangic[6, 0].Width = 200;
            tablooBaslangic.Rows[6].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[6, 1].AddParagraph().AppendText(" " +YY.HaftalikIzinSayi + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[6, 1].Width = 200;
            tablooBaslangic.Rows[6].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[7, 0].AddParagraph().AppendText(" İzinden Düşülecek Resmi Tatiller :  ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[7].Height = 41;
            tablooBaslangic[7, 0].Width = 200;
            tablooBaslangic.Rows[7].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[7, 1].AddParagraph().AppendText(" " + YY.ResmiTatilSayi + " " ).ApplyCharacterFormat(cf1);
            tablooBaslangic[7, 1].Width = 200;
            tablooBaslangic.Rows[7].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[8, 0].AddParagraph().AppendText(" İzin Sürecinde Alınan Raporlar:  ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[8].Height = 41;
            tablooBaslangic[8, 0].Width = 200;
            tablooBaslangic.Rows[8].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[8, 1].AddParagraph().AppendText(" " + YY.izindeAlinanRaporSayisi + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[8, 1].Width = 200;
            tablooBaslangic.Rows[8].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[9, 0].AddParagraph().AppendText(" Hesaplanan Net Gün Sayısı : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[9].Height = 41;
            tablooBaslangic[9, 0].Width = 200;
            tablooBaslangic.Rows[9].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[9, 1].AddParagraph().AppendText(" " + YY.Gun2 + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[9, 1].Width = 200;
            tablooBaslangic.Rows[9].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[10, 0].AddParagraph().AppendText(" Günlük Net Ücret : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[10].Height = 41;
            tablooBaslangic[10, 0].Width = 200;
            tablooBaslangic.Rows[10].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string gunlukk = String.Format("{0:C2}", _gunlukUcret);
            tablooBaslangic[10, 1].AddParagraph().AppendText(" " + gunlukk + " ").ApplyCharacterFormat(cf1);
            tablooBaslangic[10, 1].Width = 200;
            tablooBaslangic.Rows[10].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            //------ Hesplama Yazıları
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.2f;
            tabloo.TableFormat.CellSpacing = 0;

            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.ResetCells(6, 2);


            tabloo[0, 0].AddParagraph().AppendText(" Toplam Brüt :  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[0].Height = 41;
            tabloo[0, 0].Width = 200;
            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string toplamBrut = String.Format("{0:C2}", YY.Toplam);
            tabloo[0, 1].AddParagraph().AppendText("" + toplamBrut ).ApplyCharacterFormat(cf1);
            tabloo[0, 1].Width = 200;
            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            tabloo[1, 0].AddParagraph().AppendText(" Sgk :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[1].Height = 41;
            tabloo[1, 0].Width = 200;
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            string toplamSgk = String.Format("{0:C2}", YY.SGK);
            tabloo[1, 1].AddParagraph().AppendText("   " + toplamSgk + "").ApplyCharacterFormat(cf1);
            tabloo[1, 1].Width = 200;
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            tabloo[2, 0].AddParagraph().AppendText(" Vergi : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[2].Height = 41;
            tabloo[2, 0].Width = 200;
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;



            string toplamVergi = String.Format("{0:C2}", YY.Vergi);
            tabloo[2, 1].AddParagraph().AppendText("  " + toplamVergi + " ").ApplyCharacterFormat(cf1);
            tabloo[2, 1].Width = 200;
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            tabloo[3, 0].AddParagraph().AppendText(" Damga Vergisi : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[3].Height = 41;
            tabloo[3, 0].Width = 200;
            tabloo.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string toplamDamgaV = String.Format("{0:C2}", YY.DamgaV);
            tabloo[3, 1].AddParagraph().AppendText(" " + toplamDamgaV + " ").ApplyCharacterFormat(cf1);
            tabloo[3, 1].Width = 200;
            tabloo.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo[4, 0].AddParagraph().AppendText(" İşsizlik Sigortası : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[4].Height = 41;
            tabloo[4, 0].Width = 200;
            tabloo.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string toplamIssizlik = String.Format("{0:C2}", YY.Issizlik);
            tabloo[4, 1].AddParagraph().AppendText(" " + toplamIssizlik + " ").ApplyCharacterFormat(cf1);
            tabloo[4, 1].Width = 200;
            tabloo.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo[5, 0].AddParagraph().AppendText(" Net : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[5].Height = 41;
            tabloo[5, 0].Width = 200;
            tabloo.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            string toplamNet = String.Format("{0:C2}", YY.Net);
            tabloo[5, 1].AddParagraph().AppendText("  " + toplamNet + " ").ApplyCharacterFormat(cf1);
            tabloo[5, 1].Width = 200;
            tabloo.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            //Hesaplanan İzin Yılları Bilgi
            // Çalışma Yılı - Başlangıç - Bitiş -izin Hakkı

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Hesaplanan İzin Dönemleri") as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            IWTable tabloDonemler = section.AddTable();
            tabloDonemler.TableFormat.Borders.LineWidth = 0.3f;
            tabloDonemler.TableFormat.CellSpacing = 0;
            tabloDonemler.ApplyStyle(BuiltinTableStyle.LightGrid);

            int donemSatSay = YY.HesapYillariBilgi.Count + 1;
            tabloDonemler.ResetCells(donemSatSay, 4);


            tabloDonemler.Rows[0].Height = 41;

            tabloDonemler[0, 0].AddParagraph().AppendText(" Çalışma Yılı :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 0].Width = 115;
            tabloDonemler.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloDonemler[0, 1].AddParagraph().AppendText(" Başlangıç :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 1].Width = 115;
            tabloDonemler.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloDonemler[0, 2].AddParagraph().AppendText(" Bitiş :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 2].Width = 115;
            tabloDonemler.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloDonemler[0, 3].AddParagraph().AppendText("  İzin Gün Sayısı :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloDonemler[0, 3].Width = 115;
            tabloDonemler.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            int yilSay = YY.HesapYillariBilgi.Count;
            int s1 = 0;

            var listey2 = YY.HesapYillariBilgi.OrderBy(o => o.BasTarih);
            foreach (var t in listey2)
            {
                s1 = s1 + 1;
                if(yilSay != s1)
                {
                    // Bilgiler Tabloya Aktarılacak!!......
                    tabloDonemler.Rows[s1].Height = 41;

                    tabloDonemler[s1, 0].AddParagraph().AppendText(""+ t.YilSay.ToString() + "").ApplyCharacterFormat(cf1);
                    tabloDonemler[s1, 0].Width = 115;
                    tabloDonemler.Rows[s1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloDonemler[s1, 1].AddParagraph().AppendText("" + t.BasTarih.ToShortDateString() + "").ApplyCharacterFormat(cf1);
                    tabloDonemler[s1, 1].Width = 115;
                    tabloDonemler.Rows[s1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloDonemler[s1, 2].AddParagraph().AppendText("" + t.BitTarih.ToShortDateString() + "").ApplyCharacterFormat(cf1);
                    tabloDonemler[s1, 2].Width = 115;
                    tabloDonemler.Rows[s1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloDonemler[s1, 3].AddParagraph().AppendText("" + t.GunSay.ToString() + "").ApplyCharacterFormat(cf1);
                    tabloDonemler[s1, 3].Width = 115;
                    tabloDonemler.Rows[s1].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }
            }


            //Kullanılan İzin Kayıtları
            // Başlangıç - Bitiş : Gün Sayısı

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Bu Dönemlerde Kullanılan İzinler".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            IWTable tabloIzinKayitlari = section.AddTable();
            tabloIzinKayitlari.TableFormat.Borders.LineWidth = 0.2f;
            tabloIzinKayitlari.TableFormat.CellSpacing = 0;
            tabloIzinKayitlari.ApplyStyle(BuiltinTableStyle.LightGrid);

            int izinSatSay = YY.HesapYillariBilgi.Count + 1;
            tabloIzinKayitlari.ResetCells(izinSatSay, 4);

            tabloIzinKayitlari.Rows[0].Height = 41;

            tabloIzinKayitlari[0, 0].AddParagraph().AppendText(" Sıra :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIzinKayitlari[0, 0].Width = 115;
            tabloIzinKayitlari.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIzinKayitlari[0, 1].AddParagraph().AppendText(" Başlangıç : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIzinKayitlari[0, 1].Width = 115;
            tabloIzinKayitlari.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIzinKayitlari[0, 2].AddParagraph().AppendText(" Bitiş(Son Gün) : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIzinKayitlari[0, 2].Width = 115;
            tabloIzinKayitlari.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloIzinKayitlari[0, 3].AddParagraph().AppendText("  Gün Sayısı : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloIzinKayitlari[0, 3].Width = 115;
            tabloIzinKayitlari.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            var ll1 = YY.IzinKaytilariBilgi.OrderBy(o => o.BaslangicTar).ToList();

            int izinKayitSira = 0;
            foreach(var t in ll1)
            {
                if(t.BaslangicTar >= YY.HesapBaslangicTar)
                {


                    izinKayitSira = izinKayitSira + 1;

                    tabloIzinKayitlari.Rows[izinKayitSira].Height = 41;

                    tabloIzinKayitlari[izinKayitSira, 0].AddParagraph().AppendText(" " + izinKayitSira.ToString() + " ").ApplyCharacterFormat(cf1);
                    tabloIzinKayitlari[izinKayitSira, 0].Width = 115;
                    tabloIzinKayitlari.Rows[izinKayitSira].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloIzinKayitlari[izinKayitSira, 1].AddParagraph().AppendText(" " + t.BaslangicTar.ToShortDateString() + " ").ApplyCharacterFormat(cf1);
                    tabloIzinKayitlari[izinKayitSira, 1].Width = 115;
                    tabloIzinKayitlari.Rows[izinKayitSira].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloIzinKayitlari[izinKayitSira, 2].AddParagraph().AppendText(" " + t.BitisTar.ToShortDateString() + " ") .ApplyCharacterFormat(cf1);
                    tabloIzinKayitlari[izinKayitSira, 2].Width = 115;
                    tabloIzinKayitlari.Rows[izinKayitSira].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    int gsay = 0;
                    var gliste = YY.IzinGunleriBilgi.Where(o => o.FazlaMesaiId == YY.Id).ToList();
                    gsay = gliste.Count;
                    tabloIzinKayitlari[izinKayitSira, 3].AddParagraph().AppendText(" " + gsay.ToString() + " ").ApplyCharacterFormat(cf1);
                    tabloIzinKayitlari[izinKayitSira, 3].Width = 115;
                    tabloIzinKayitlari.Rows[izinKayitSira].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }

            }

            //  Düşülen Resmi Tatiller
            // Tarih- Açıklama..

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzinlerdeki Resmi Tatiller".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            IWTable tabloResmiTatiller= section.AddTable();
            tabloResmiTatiller.TableFormat.Borders.LineWidth = 0.3f;
            tabloResmiTatiller.TableFormat.CellSpacing = 0;
            tabloResmiTatiller.ApplyStyle(BuiltinTableStyle.LightGrid);
            int resmiSay = YY.IzindekiResmiTatillerBilgi.Count + 1;

            tabloResmiTatiller.ResetCells(resmiSay, 3);
            tabloResmiTatiller.Rows[0].Height = 41;


            tabloResmiTatiller[0, 0].AddParagraph().AppendText(" Sıra : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloResmiTatiller[0, 0].Width = 60;
            tabloResmiTatiller.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloResmiTatiller[0, 1].AddParagraph().AppendText(" Tarih : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloResmiTatiller[0, 1].Width = 120;
            tabloResmiTatiller.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloResmiTatiller[0, 2].AddParagraph().AppendText(" Açıklama : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloResmiTatiller[0, 2].Width = 240;
            tabloResmiTatiller.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            int resmiTatilSira = 0;

            var ll2 = YY.IzindekiResmiTatillerBilgi.OrderBy(o => o.Tarih).ToList();

            foreach(var t in ll2)
            {
                if (t.Tarih >= YY.HesapBaslangicTar)
                {
                    tabloResmiTatiller.Rows[resmiTatilSira].Height = 41;

                    resmiTatilSira = resmiTatilSira + 1;

                    tabloResmiTatiller[resmiTatilSira, 0].AddParagraph().AppendText(" " + resmiTatilSira.ToString() + " ").ApplyCharacterFormat(cf1);
                    tabloResmiTatiller[resmiTatilSira, 0].Width = 60;
                    tabloResmiTatiller.Rows[resmiTatilSira].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloResmiTatiller[resmiTatilSira, 1].AddParagraph().AppendText(" " + t.Tarih.ToShortDateString() + " ").ApplyCharacterFormat(cf1);
                    tabloResmiTatiller[resmiTatilSira, 1].Width = 120;
                    tabloResmiTatiller.Rows[resmiTatilSira].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloResmiTatiller[resmiTatilSira, 2].AddParagraph().AppendText(" " + t.Aciklama + " ").ApplyCharacterFormat(cf1);
                    tabloResmiTatiller[resmiTatilSira, 2].Width = 240;
                    tabloResmiTatiller.Rows[resmiTatilSira].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                }

            }


            // Düşülen Hafta Sonları...
            // Tarih- Haftanın Günü...
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzindeki Hafta Tatili".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            IWTable tabloHIzinler = section.AddTable();
            tabloHIzinler.TableFormat.Borders.LineWidth = 0.3f;
            tabloHIzinler.TableFormat.CellSpacing = 0;
            tabloHIzinler.ApplyStyle(BuiltinTableStyle.LightGrid);


            int HIzinSay = YY.IzindekiHaftaIzniBilgi.Count + 1;
            tabloHIzinler.ResetCells(HIzinSay, 3);

            tabloHIzinler.Rows[0].Height = 41;

            tabloHIzinler[0, 0].AddParagraph().AppendText(" Sıra :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHIzinler[0, 0].Width = 60;
            tabloHIzinler.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloHIzinler[0, 1].AddParagraph().AppendText(" Tarih :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHIzinler[0, 1].Width = 120;
            tabloHIzinler.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
          
            tabloHIzinler[0, 2].AddParagraph().AppendText(" Gün :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHIzinler[0, 2].Width = 120;
            tabloHIzinler.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            int hIzinSira = 0;

            var ll3 = YY.IzindekiHaftaIzniBilgi.OrderBy(o => o.Tarih).ToList();

            foreach(var t in ll3)
            {
                if(t.Tarih>= YY.HesapBaslangicTar)
                {
                    hIzinSira = hIzinSira + 1;

                    tabloHIzinler.Rows[hIzinSira].Height = 41;

                    tabloHIzinler[hIzinSira, 0].AddParagraph().AppendText(" " + hIzinSira + "").ApplyCharacterFormat(cf1);
                    tabloHIzinler[hIzinSira, 0].Width = 60;
                    tabloHIzinler.Rows[hIzinSira].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloHIzinler[hIzinSira, 1].AddParagraph().AppendText(" " + t.Tarih.ToShortDateString() + " ").ApplyCharacterFormat(cf1);
                    tabloHIzinler[hIzinSira, 1].Width = 120;
                    tabloHIzinler.Rows[hIzinSira].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                    tabloHIzinler[hIzinSira, 2].AddParagraph().AppendText(" " + YY.izinGunu + " ").ApplyCharacterFormat(cf1);
                    tabloHIzinler[hIzinSira, 2].Width = 120;
                    tabloHIzinler.Rows[hIzinSira].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                }
            }




            //Toplam Çalışma Yılları..
            //Toplam İzin Hakları Listesi.
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Hesaplanan İzin Dönemleri".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            IWTable tabloTumCalisilan = section.AddTable();
            tabloTumCalisilan.TableFormat.Borders.LineWidth = 0.3f;
            tabloTumCalisilan.TableFormat.CellSpacing = 0;
            tabloTumCalisilan.ApplyStyle(BuiltinTableStyle.LightGrid);

            int calisilanSatSay = YY.CalisilanYillarBilgi.Count + 1;
            tabloTumCalisilan.ResetCells(donemSatSay, 4);

            tabloTumCalisilan.Rows[0].Height = 41;

            tabloTumCalisilan[0, 0].AddParagraph().AppendText(" Çalışma Yılı : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloTumCalisilan[0, 0].Width = 115;
            tabloTumCalisilan.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloTumCalisilan[0, 1].AddParagraph().AppendText(" Başlangıç : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloTumCalisilan[0, 1].Width = 115;
            tabloTumCalisilan.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloTumCalisilan[0, 2].AddParagraph().AppendText(" Bitiş : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloTumCalisilan[0, 2].Width = 115;
            tabloTumCalisilan.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloTumCalisilan[0, 3].AddParagraph().AppendText("İzin Gün Sayısı : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloTumCalisilan[0, 3].Width = 115;
            tabloTumCalisilan.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            int s2 = 0;
            foreach(var t in YY.CalisilanYillarBilgi)
            {
                s2 = s2 + 1;
                tabloTumCalisilan.Rows[s2].Height = 41;

                tabloTumCalisilan[s2, 0].AddParagraph().AppendText(" " + t.YilSay + " ").ApplyCharacterFormat(cf1);
                tabloTumCalisilan[s2, 0].Width = 115;
                tabloTumCalisilan.Rows[s2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloTumCalisilan[s2, 1].AddParagraph().AppendText(" "+ t.BasTarih.ToShortDateString() + " ").ApplyCharacterFormat(cf1);
                tabloTumCalisilan[s2, 1].Width = 115;
                tabloTumCalisilan.Rows[s2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloTumCalisilan[s2, 2].AddParagraph().AppendText(" "+ t.BitTarih.ToShortDateString() + " ").ApplyCharacterFormat(cf1);
                tabloTumCalisilan[s2, 2].Width = 115;
                tabloTumCalisilan.Rows[s2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

                tabloTumCalisilan[s2, 3].AddParagraph().AppendText(" "+ t.GunSay + " ").ApplyCharacterFormat(cf1);
                tabloTumCalisilan[s2, 3].Width = 115;
                tabloTumCalisilan.Rows[s2].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            }

            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "YillikIzinRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            //var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("YillikIzinRapor.docx", "application/msword", stream);


        }




    }
}
