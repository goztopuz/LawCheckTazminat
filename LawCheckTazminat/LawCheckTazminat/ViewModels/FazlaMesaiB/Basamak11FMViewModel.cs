using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak11FMViewModel : ViewModelBase
    {
        public Basamak11FMViewModel(FazlaMesai _ffmm)
        {
            this.FM = new FazlaMesai();
            this.FM = _ffmm;

            FazlaMesaiToplaHesapla();
            HaftalikIzinToplaHesapla();
            ResmiTatilToplaHesapla();

        }

        Services.HesaplaVergi islemVergi = new Services.HesaplaVergi();

        private FazlaMesai _fm;
        public  FazlaMesai FM
        {
            get => _fm;
            set
            {
                _fm = value;
                OnPropertyChanged();
            }
        }





        // Fazla Mesai Bölümü
        private decimal _fmBrut;
        public decimal FMBrut
        {
            get => _fmBrut;
            set
            {
                _fmBrut = value;
                OnPropertyChanged();
            }
        }

        private decimal _fmVergi;
        public decimal FMVergi
        {
            get => _fmVergi;
            set
            {
                _fmVergi = value;
                OnPropertyChanged();
            }

        }

        private decimal _fmSGK;
        public decimal FMSGK
        {
            get => _fmSGK;
            set
            {
                _fmSGK = value;
                OnPropertyChanged();
            }
        }

        private decimal _fmDamga;
        public decimal FMDamga
        {
            get => _fmDamga;
            set
            {
                _fmDamga = value;
                OnPropertyChanged();
            }
        }

        private decimal _fmNet;
        public decimal FMNet
        {
            get => _fmNet;
            set
            {
                _fmNet = value;
                OnPropertyChanged();
            }
        }


        public ICommand BitisCommand => new Command(OnBitis);
        async private void OnBitis()
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


        public ICommand YenidenHesaplaCommand => new Command(OnYenidenHesapla);
        async private void OnYenidenHesapla()
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            FM.duzenlemede = true;

            //----
            var sayfa = new Basamak1FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);




            IsBusy = false;
        }

        public ICommand RaporFazlaMesaiCommand => new Command(OnRaporFazlaMesai);
        async private void OnRaporFazlaMesai()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;

            //Fazla Mesai
            FazlaMesaiRapor();

            IsBusy = false;

        }
        Double damgaVergi = 0.00759;



        private double _fmToplamSaat;
        public double FMToplamSaat
        {
            get => _fmToplamSaat;
            set
            {
                _fmToplamSaat = value;
                OnPropertyChanged();
            }
        }
        
        private void FazlaMesaiToplaHesapla()
        {
            decimal _fmMiktar = 0;
            double _topSaat = 0;
            foreach(var  t in FM.FMHaftalarBilgi)

            {
               
                if(t.FazlaMesaiVar==true)
                {
                    _topSaat += t.FazlaMesaiOHaftadaki-FM.SozlesmeCalismaSaat;
                    _fmMiktar = _fmMiktar + t.MesaiUcret;
                    
                    
                }
            }

            FM.Toplam = _fmMiktar;
            FMToplamSaat = _topSaat;
            
            FM.ToplamSGK = FM.Toplam * Convert.ToDecimal(0.14);
            FM.ToplamDamgaVergi = FM.Toplam * Convert.ToDecimal(damgaVergi);
            FM.ToplamIssizlik= FM.Toplam * (Convert.ToDecimal(0.01));

            decimal dusecekMiktar = FM.ToplamSGK + FM.ToplamDamgaVergi + FM.ToplamIssizlik;

            decimal mik1 = FM.Toplam - dusecekMiktar;

            decimal gelirVergi = islemVergi.VergiHesapla(
                Convert.ToDecimal(FM.VergiMatrah), mik1,Convert.ToInt32(FM.Vergiyil));
            FM.ToplamVergi = gelirVergi;

            FM.ToplamNet = FM.Toplam - FM.ToplamVergi - dusecekMiktar;

        }
        public ICommand RaporHaftalikIzinCommand => new Command(OnRaporHaftalikIzin);
        async private void OnRaporHaftalikIzin()
        {
            if(IsBusy==true)
            {
                return;
            }
            IsBusy = true;


            // İzin Raporr.
            HaftaIzniRapor();

            IsBusy = false;
        }
        private void HaftalikIzinToplaHesapla()
        {
            decimal fmHIzinMiktar = 0;

            foreach(var  t in FM.HaftalikIzinHaftalarBilgi)
            {
                if(t.HaftaSonuIzinVar==true)
                {
                    fmHIzinMiktar = fmHIzinMiktar + t.HaftaSonuUcret;
                }
            }
            FM.ToplamHsonu = fmHIzinMiktar;

            FM.ToplamHsonuSGK = FM.ToplamHsonu * Convert.ToDecimal(0.14);
            FM.ToplamHsonuDamgaVergi = FM.ToplamHsonu * Convert.ToDecimal(damgaVergi);
            FM.ToplamHsonuIssizlik = FM.ToplamHsonu * (Convert.ToDecimal(0.01));

            decimal dusulecekMiktar = FM.ToplamHsonuSGK + FM.ToplamHsonuDamgaVergi + FM.ToplamHsonuIssizlik;

            decimal mik1 = FM.ToplamHsonu - dusulecekMiktar;
            decimal gelirVergi = islemVergi.VergiHesapla(
            Convert.ToDecimal(FM.VergiMatrah), mik1, Convert.ToInt32(FM.Vergiyil));
            FM.ToplamHsonuVergi = gelirVergi;

            FM.ToplamHsonuNet = FM.ToplamHsonu - FM.ToplamHsonuVergi - dusulecekMiktar;
        }
         
        public ICommand RaporResmiTatilCommand => new Command(OnRaporResmiTatil);
       async private void OnRaporResmiTatil()
        {
            if(IsBusy==true)
            {
                return;

            }
            IsBusy = true;
             ResmiTatilRapor();
            IsBusy = false;
        }

        private void ResmiTatilToplaHesapla()
        {
            decimal fmResmiMiktar = 0;

            foreach(var t in FM.ResmiTatilBilgi)
            {
                if(t.AktifDurumu==1)
                {
                    fmResmiMiktar = fmResmiMiktar + t.Miktar;
                }
            }
            FM.ToplamResmiTatil = fmResmiMiktar;
            FM.ToplamResmiTatilSGK = FM.ToplamResmiTatil * Convert.ToDecimal(0.14);
            FM.ToplamResmiTatilDamgaVergi = FM.ToplamResmiTatil * Convert.ToDecimal(damgaVergi);
            FM.ToplamResmiTatilIssizlik = FM.ToplamResmiTatil * (Convert.ToDecimal(0.01));

            decimal dusulecekMiktar = FM.ToplamResmiTatilSGK + FM.ToplamResmiTatilDamgaVergi + FM.ToplamResmiTatilIssizlik;

            decimal mik1 = FM.ToplamResmiTatil - dusulecekMiktar;
            decimal gelirVergi = islemVergi.VergiHesapla(
            Convert.ToDecimal(FM.VergiMatrah), mik1, Convert.ToInt32(FM.Vergiyil));
            FM.ToplamResmiTatilVergi = gelirVergi;

            FM.ToplamResmiTatilNet = FM.ToplamResmiTatil - FM.ToplamResmiTatilVergi - dusulecekMiktar;

        }


        public bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        string _hataMesaji = "";
        public string HataMesaji
        {
            get => _hataMesaji;
            set
            {
                _hataMesaji = value;
                OnPropertyChanged();
            }
        }


        private async void ResmiTatilRapor()
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

            //Set page size of the section

            // section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);
            section.PageSetup.PageSize = PageSize.A4;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Resmi Tatillerde Çalışma".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tablooBaslangic = section.AddTable();

            tablooBaslangic.TableFormat.Borders.LineWidth = 0f;
       
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 20;
            cf3.FontName = "Times New Roman";

            WCharacterFormat cf2 = new WCharacterFormat(document);
            cf2.Bold = true;
            cf2.FontSize = 12;
            cf2.FontName = "Times New Roman";

            //  tablooBalangic.ApplyStyle(BuiltinTableStyle.DarkList);
            tablooBaslangic.ResetCells(3, 2);

            tablooBaslangic.IndentFromLeft = 80;
            tablooBaslangic.Rows[0].Height = 55;
            tablooBaslangic.Rows[1].Height = 55;
            tablooBaslangic.Rows[2].Height = 55;

            tablooBaslangic.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooBaslangic[0, 0].AddParagraph().AppendText(" Ad Soyad :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[0, 0].Width = 200;
            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[0, 1].AddParagraph().AppendText(" " + "----------------------" + "\t").ApplyCharacterFormat(cf1);
            tablooBaslangic[0, 1].Width = 200;
            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[1, 0].AddParagraph().AppendText(" Başlangıç Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[1, 0].Width = 200;
            tablooBaslangic.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[1, 1].AddParagraph().AppendText(" " + FM.BasTarihResmiTatil.ToShortDateString() + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic[1, 1].Width = 200;
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[2, 0].AddParagraph().AppendText(" Bitiş Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[2, 0].Width = 200;
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic[2, 1].AddParagraph().AppendText(" " + FM.BitTarihResmiTatil.ToShortDateString() + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic[2, 1].Width = 200;
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();



            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.1f;
            tabloo.TableFormat.CellSpacing = 0;

            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.ResetCells(6, 2);
            tabloo.IndentFromLeft = 80;
            tabloo.Rows[0].Height = 55;
            tabloo.Rows[1].Height = 55;
            tabloo.Rows[2].Height = 55;
            tabloo.Rows[3].Height = 55;
            tabloo.Rows[4].Height = 55;
            tabloo.Rows[5].Height = 55;
          
           

            tabloo[0, 0].AddParagraph().AppendText(" Toplam Brüt :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[0, 0].Width = 200;

            string toplamBrut = String.Format("{0:C2}", FM.ToplamResmiTatil);
            tabloo[0, 1].AddParagraph().AppendText("  " + toplamBrut + "").ApplyCharacterFormat(cf1);
            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[0, 1].Width = 200;

            tabloo[1, 0].AddParagraph().AppendText(" Sgk : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[1, 0].Width = 200;

            string toplamSgk = String.Format("{0:C2}", FM.ToplamResmiTatilSGK);
            tabloo[1, 1].AddParagraph().AppendText("   " + toplamSgk + "").ApplyCharacterFormat(cf1);
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[1, 1].Width = 200;


            tabloo[2, 0].AddParagraph().AppendText(" Vergi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[2, 0].Width = 200;

            string toplamVergi = String.Format("{0:C2}", FM.ToplamResmiTatilVergi);
            tabloo[2, 1].AddParagraph().AppendText("   " + toplamVergi + "").ApplyCharacterFormat(cf1);
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[2, 1].Width = 200;

            tabloo[3, 0].AddParagraph().AppendText(" Damga Vergisi :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[3, 0].Width = 200;

            string toplamDamgaV = String.Format("{0:C2}", FM.ToplamResmiTatilDamgaVergi);
            tabloo[3, 1].AddParagraph().AppendText("   " + toplamDamgaV + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[3, 1].Width = 200;

            tabloo[4, 0].AddParagraph().AppendText(" İşsizlik Sigortası :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[4, 0].Width = 200;

            string toplamIssizlik = String.Format("{0:C2}", FM.ToplamResmiTatilIssizlik);
            tabloo[4, 1].AddParagraph().AppendText("   " + toplamIssizlik + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[4, 1].Width = 200;

            tabloo[5, 0].AddParagraph().AppendText(" Net :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[5, 0].Width = 200;

            string toplamNet = String.Format("{0:C2}", FM.ToplamResmiTatilNet);
            tabloo[5, 1].AddParagraph().AppendText("   " + toplamNet + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[5, 1].Width = 200;





            // Dönem İçindeki Maaşların Tablosu.....
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Maaş Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();



            int maasSat = FM.MaasBilgi.Count + 1;
            IWTable tablooMaas = section.AddTable();

            tablooMaas.TableFormat.Borders.LineWidth = 0.4f;
            tablooMaas.TableFormat.CellSpacing = 0;

            tablooMaas.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooMaas.ResetCells(maasSat, 3);
            tablooMaas.IndentFromLeft = 30;

            tabloo.Rows[0].Height = 40;
     


            tablooMaas[0, 0].AddParagraph().AppendText("\t Dönemi : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 0].Width = 150;

            tablooMaas[0, 1].AddParagraph().AppendText("\t Brüt : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 1].Width = 150;

            tablooMaas[0, 2].AddParagraph().AppendText("\t Net : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 2].Width = 150;
            tablooMaas.Rows[0].Height = 40;

            int s1 = 0;

            var listeMaas = FM.MaasBilgi.OrderBy(o => o.yilBas).ToList();
            foreach (var t in listeMaas)
            {
                s1 = s1 + 1;
                tablooMaas.Rows[s1].Height = 34;
               

                tablooMaas[s1, 0].AddParagraph().AppendText("\t" + t.yil ).ApplyCharacterFormat(cf2);
                tablooMaas.Rows[s1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 0].Width = 150;

                string brutMaasYazi = String.Format("{0:C2}", t.brutMaas);
                tablooMaas[s1, 1].AddParagraph().AppendText("\t" + brutMaasYazi + "").ApplyCharacterFormat(cf2);
                tablooMaas.Rows[s1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 1].Width = 150;

                string netMaasYazi = String.Format("{0:C2}", t.netMaas);
                tablooMaas[s1, 2].AddParagraph().AppendText("\t" + netMaasYazi + "").ApplyCharacterFormat(cf2);
                tablooMaas.Rows[s1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 2].Width = 150;

            }



            //-------------------
            // Dönem İçindeki İzinlerin Tablosu........
            //--------

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzinler".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            int izinSat = FM.IzinKaytilariBilgi.Count + 1;
            IWTable tablooIzin = section.AddTable();

            tablooIzin.TableFormat.Borders.LineWidth = 0.2f;
            tablooIzin.TableFormat.CellSpacing = 0;

            tablooIzin.ApplyStyle(BuiltinTableStyle.LightGrid);
            tablooIzin.ResetCells(izinSat, 3);
            tablooIzin.IndentFromLeft = 30;

            int s2 = 0;
            tablooIzin.Rows[0].Height = 40;
            tablooIzin[0, 0].AddParagraph().AppendText("  Başlangıç : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 0].Width = 120;

            tablooIzin[0, 1].AddParagraph().AppendText("  Bitiş  : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 1].Width = 120;

            tablooIzin[0, 2].AddParagraph().AppendText("  Açıklama : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 2].Width = 120;

            var listeIzin = FM.IzinKaytilariBilgi.OrderBy(o => o.BaslangicTar).ToList();
            foreach (var t in listeIzin)
            {
                s2 = s2 + 1;
                tablooIzin.Rows[s2].Height = 33;

                tablooIzin[s2, 0].AddParagraph().AppendText("  " + t.BaslangicTar.ToShortDateString() );
                tablooIzin.Rows[s2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 0].Width = 120;

                tablooIzin[s2, 1].AddParagraph().AppendText("  " + t.BitisTar.ToShortDateString() );
                tablooIzin.Rows[s2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 1].Width = 120;

                tablooIzin[s2, 2].AddParagraph().AppendText("  " + t.Aciklama );
                tablooIzin.Rows[s2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 2].Width = 120;

            }


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();


            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Resmi Tatiller Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            int satSay = FM.ResmiTatilBilgi.Count + 1;

            paragraph = section.AddParagraph();

            // Sira - Tarih - MaaşBrüt - Mesai Ücreti - Genel Toplam.- Açıklama
            int sutSay = 5;

            IWTable tabloHaftalar = section.AddTable();

            tabloHaftalar.TableFormat.Borders.LineWidth = 0.3f;
            tabloHaftalar.TableFormat.CellSpacing = 0;

            tabloHaftalar.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloHaftalar.ResetCells(satSay, sutSay);
            tabloHaftalar.IndentFromLeft = 22;
            tabloHaftalar.Rows[0].Height = 40;

            tabloHaftalar[0, 0].AddParagraph().AppendText(" Sıra : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHaftalar.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 0].Width = 45;

            tabloHaftalar[0, 1].AddParagraph().AppendText(" Tarih : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHaftalar.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 1].Width = 95;

            tabloHaftalar[0, 2].AddParagraph().AppendText("Açıklama ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHaftalar.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 2].Width = 190;

            //  tabloHaftalar[0, 3].AddParagraph().AppendText("\n Genel Toplam :\t \n").ApplyCharacterFormat(cf1);
            tabloHaftalar[0, 3].AddParagraph().AppendText(" Ücret  :  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHaftalar.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 3].Width = 95;

            tabloHaftalar[0, 4].AddParagraph().AppendText(" Genel Toplam:".ToUpper()).ApplyCharacterFormat(cf1);
            tabloHaftalar.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 4].Width = 105;


            decimal genelToplamFM = 0;

            var kayitListe = FM.ResmiTatilBilgi.OrderBy(o => o.tarih).ToList();
            int i = 0;
            //for (int i = 1; i <= satSay; i++)
            foreach(var t in kayitListe)
            {
                i = i + 1;
                var kayit = FM.ResmiTatilBilgi[i - 1];

                tabloHaftalar.Rows[i].Height = 33;

                tabloHaftalar[i, 0].AddParagraph().AppendText("  " + i.ToString() + " ").ApplyCharacterFormat(cf2);
                tabloHaftalar.Rows[i].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 0].Width = 45;

                tabloHaftalar[i, 1].AddParagraph().AppendText(" " + kayit.tarih.ToShortDateString()).ApplyCharacterFormat(cf2);
                tabloHaftalar.Rows[i].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 1].Width = 95;


                tabloHaftalar[i, 2].AddParagraph().AppendText("  " + kayit.Aciklama + "\t");
                tabloHaftalar.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i,2].Width = 190;


                if (kayit.AktifDurumu == 0)
                {
                    kayit.Miktar = 0;
                }
                string mesaiUcretYazi = String.Format("{0:C2}", kayit.Miktar);
                tabloHaftalar[i, 3].AddParagraph().AppendText("  " + mesaiUcretYazi + "\t");
                tabloHaftalar.Rows[i].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 3].Width = 95;

                genelToplamFM = genelToplamFM + kayit.Miktar;
                string genelToplamYazi = String.Format("{0:C2}", genelToplamFM);
                tabloHaftalar[i, 4].AddParagraph().AppendText("  " + genelToplamYazi + "\t");
                tabloHaftalar.Rows[i].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 4].Width = 105;
            }
            
            // Kaydetme Bölümü....
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "ResmiTatilRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //Save the stream as a file in the device and invoke it for viewing
         //   var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("FM_ResmiTatil.docx", "application/msword", stream);


        }


        private async void HaftaIzniRapor()
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

            //Set page size of the section
            //   section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

            section.PageSetup.PageSize = PageSize.A4;

            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;

            //Appends paragraph


            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("Hafta İzninde Çalışma Raporu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.Bold = true;




            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            IWTable tablooBaslangic = section.AddTable();

            tablooBaslangic.TableFormat.Borders.LineWidth = 0.3f;
            tablooBaslangic.TableFormat.CellSpacing = 0;
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf2 = new WCharacterFormat(document);
            cf2.Bold = true;
            cf2.FontSize = 12;
            cf2.FontName = "Times New Roman";

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 20;
            cf3.FontName = "Times New Roman";

            //  tablooBalangic.ApplyStyle(BuiltinTableStyle.DarkList);
            tablooBaslangic.ResetCells(4, 2);
            tablooBaslangic.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooBaslangic.IndentFromLeft = 80;
            tablooBaslangic[0, 0].AddParagraph().AppendText(" Ad Soyad :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[0, 0].Width = 200;

            tablooBaslangic[0, 1].AddParagraph().AppendText("   " + "----------------------" + "\t").ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[0, 1].Width = 200;

            tablooBaslangic[1, 0].AddParagraph().AppendText(" Başlangıç Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[1, 0].Width = 200;

            tablooBaslangic[1, 1].AddParagraph().AppendText(" " + FM.BasTarihHaftaSonu.ToShortDateString() + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[1, 1].Width = 200;

            tablooBaslangic[2, 0].AddParagraph().AppendText(" Bitiş Tarihi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[2, 0].Width = 200;

            tablooBaslangic[2, 1].AddParagraph().AppendText(" " + FM.BitTarihHaftaSonu.ToShortDateString() + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[2, 1].Width = 200;

            string izinGun = FM.TatilGunu;
            tablooBaslangic[3, 0].AddParagraph().AppendText(" İzin Günü :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[3, 0].Width = 200;

            tablooBaslangic[3, 1].AddParagraph().AppendText(" " + izinGun + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic[3, 1].Width = 200;

            tablooBaslangic.Rows[0].Height = 48;
            tablooBaslangic.Rows[1].Height = 48;
            tablooBaslangic.Rows[2].Height = 48;
            tablooBaslangic.Rows[3].Height = 48;



            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.2f;
            tabloo.TableFormat.CellSpacing = 0;


            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.ResetCells(6, 2);
            tabloo.IndentFromLeft = 80;
            tabloo.Rows[0].Height = 48;
            tabloo.Rows[1].Height = 48;
            tabloo.Rows[2].Height = 48;
            tabloo.Rows[3].Height = 48;
            tabloo.Rows[4].Height = 48;
            tabloo.Rows[5].Height = 48;



            tabloo[0, 0].AddParagraph().AppendText(" Toplam Brüt :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[0, 0].Width = 200;

            string toplamBrut = String.Format("{0:C2}", FM.ToplamHsonu);
            tabloo[0, 1].AddParagraph().AppendText("   " + toplamBrut + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[0, 1].Width = 200;

            tabloo[1, 0].AddParagraph().AppendText(" Sgk :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[1, 0].Width = 200;

            string toplamSgk = String.Format("{0:C2}", FM.ToplamHsonuSGK);
            tabloo[1, 1].AddParagraph().AppendText("  " + toplamSgk + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[1, 1].Width = 200;

            tabloo[2, 0].AddParagraph().AppendText(" Vergi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[2, 0].Width = 200;

            string toplamVergi = String.Format("{0:C2}", FM.ToplamHsonuVergi);
            tabloo[2, 1].AddParagraph().AppendText("  " + toplamVergi + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[2, 1].Width = 200;

            tabloo[3, 0].AddParagraph().AppendText(" Damga Vergisi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[3, 0].Width = 200;

            string toplamDamgaV = String.Format("{0:C2}", FM.ToplamHsonuDamgaVergi);
            tabloo[3, 1].AddParagraph().AppendText("  " + toplamDamgaV + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[3, 1].Width = 200;

            tabloo[4, 0].AddParagraph().AppendText(" İşsizlik Sigortası : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[4, 0].Width = 200;


            string toplamIssizlik = String.Format("{0:C2}", FM.ToplamHsonuIssizlik);
            tabloo[4, 1].AddParagraph().AppendText("   " + toplamIssizlik + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[4, 1].Width = 200;

            tabloo[5, 0].AddParagraph().AppendText(" Net :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo.Rows[5].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[5, 0].Width = 200;

            string toplamNet = String.Format("{0:C2}", FM.ToplamHsonuNet);
            tabloo[5, 1].AddParagraph().AppendText("   " + toplamNet + "\t").ApplyCharacterFormat(cf1);
            tabloo.Rows[5].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo[5, 1].Width = 200;


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Maaş Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            // Dönem İçindeki Maaşların Tablosu.....
            int maasSat = FM.MaasBilgi.Count + 1;
            IWTable tablooMaas = section.AddTable();

            tablooMaas.TableFormat.Borders.LineWidth = 0.2f;
            tablooMaas.TableFormat.CellSpacing = 0;

            tablooMaas.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooMaas.ResetCells(maasSat, 3);

            tablooMaas.IndentFromLeft = 45;
            tablooMaas.Rows[0].Height = 40;

            tablooMaas[0, 0].AddParagraph().AppendText("  Dönemi :\t  ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 0].Width = 150;

            tablooMaas[0, 1].AddParagraph().AppendText(" Brüt :\t  ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 1].Width = 150;

            tablooMaas[0, 2].AddParagraph().AppendText(" Net :\t  ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooMaas[0, 2].Width = 150;

            var listeMaas = FM.MaasBilgi.OrderBy(o => o.yilBas).ToList();
            int s1 = 0;
            foreach (var t in listeMaas)
            {
                s1 = s1 + 1;

                tablooMaas.Rows[s1].Height = 36;

                tablooMaas[s1, 0].AddParagraph().AppendText("" + t.yil + "\t").ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 0].Width = 150;

                string brutMaasYazi = String.Format("{0:C2}", t.brutMaas);
                tablooMaas[s1, 1].AddParagraph().AppendText("" + brutMaasYazi).ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 1].Width = 150;

                string netMaasYazi = String.Format("{0:C2}", t.netMaas);
                tablooMaas[s1, 2].AddParagraph().AppendText("" + netMaasYazi + "").ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 2].Width = 150;
            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzin Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();

            //-------------------
            // Dönem İçindeki İzinlerin Tablosu........
            //--------
            int izinSat = FM.IzinKaytilariBilgi.Count + 1;
            IWTable tablooIzin = section.AddTable();

            tablooIzin.TableFormat.Borders.LineWidth = 0.2f;
            tablooIzin.TableFormat.CellSpacing = 0;

            tablooIzin.ApplyStyle(BuiltinTableStyle.LightGrid);
            tablooIzin.ResetCells(izinSat, 3);
            int s2 = 0;
            tablooIzin.IndentFromLeft = 30;
            tablooIzin.Rows[0].Height = 40;

            tablooIzin[0, 0].AddParagraph().AppendText(" Başlangıç :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 0].Width = 140;

            tablooIzin[0, 1].AddParagraph().AppendText(" Bitiş  :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 1].Width = 140;

            tablooIzin[0, 2].AddParagraph().AppendText(" Açıklama :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 2].Width = 160;

            var izinListe = FM.IzinKaytilariBilgi.OrderBy(o => o.BaslangicTar).ToList();
            foreach (var t in izinListe)
            {
                s2 = s2 + 1;
                tablooIzin.Rows[s2].Height = 36;

                tablooIzin[s2, 0].AddParagraph().AppendText("\n" + t.BaslangicTar.ToShortDateString() + "\t").ApplyCharacterFormat(cf1);
                tablooIzin.Rows[s2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 0].Width = 140;

                tablooIzin[s2, 1].AddParagraph().AppendText("\n" + t.BitisTar.ToShortDateString() + "\t").ApplyCharacterFormat(cf1);
                tablooIzin.Rows[s2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 1].Width = 140;

                tablooIzin[s2, 2].AddParagraph().AppendText("\n" + t.Aciklama + "\t");
                tablooIzin.Rows[s2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 2].Width = 160;
            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Haftalık İzin Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            int satSay = FM.HaftalikIzinHaftalarBilgi.Count + 1;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            // Sira- Baş-Bit-MaaşBrüt- İzin Günü -Mesai Ücreti -Genel Toplam.- Açıklama
            int sutSay = 6;

            IWTable tabloHaftalar = section.AddTable();

            tabloHaftalar.TableFormat.Borders.LineWidth = 0.3f;
            tabloHaftalar.TableFormat.CellSpacing = 0;

            tabloHaftalar.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloHaftalar.ResetCells(satSay, sutSay);
            tabloHaftalar.IndentFromLeft = 25;
            tabloHaftalar.Rows[0].Height = 40;

            tabloHaftalar[0, 0].AddParagraph().AppendText(" Sıra : ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 0].Width = 45;

            tabloHaftalar[0, 1].AddParagraph().AppendText(" Tarihler : ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 1].Width = 100;

            //tabloHaftalar[0, 2].AddParagraph().AppendText("  Bit. Tarih : ").ApplyCharacterFormat(cf2);
            //tabloHaftalar.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //tabloHaftalar[0, 2].Width = 100;

            tabloHaftalar[0, 2].AddParagraph().AppendText("  Brüt Maaş : ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 2].Width = 100;

            tabloHaftalar[0, 3].AddParagraph().AppendText("   İzin Günü :  ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 3].Width = 90
                ;
            tabloHaftalar[0, 4].AddParagraph().AppendText("   Mesai Ücreti :  ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 4].Width = 100;

            tabloHaftalar[0, 5].AddParagraph().AppendText("   Genel Toplam :  ".ToUpper()).ApplyCharacterFormat(cf2);
            tabloHaftalar.Rows[0].Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloHaftalar[0, 5].Width = 100;

            decimal genelToplamFM = 0;

            var kayitListe = FM.HaftalikIzinHaftalarBilgi.OrderBy(o => o.BasTarih).ToList();
            int i= 0;
            //for (int i = 1; i <= satSay; i++)
            foreach (var kayit in kayitListe)
            {
                i = i + 1;
                tabloHaftalar.Rows[i].Height = 32;

                tabloHaftalar[i, 0].AddParagraph().AppendText("  " + i.ToString() + " ");
                tabloHaftalar.Rows[i].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 0].Width = 40;

                tabloHaftalar[i, 1].AddParagraph().AppendText("  " + kayit.BasTarih.ToShortDateString() + "\n " + "  " + kayit.BitTarih.ToShortDateString() );
                tabloHaftalar.Rows[i].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 1].Width = 100;

                //tabloHaftalar[i, 2].AddParagraph().AppendText("  " + kayit.BitTarih.ToShortDateString() + " ");
                //tabloHaftalar.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //tabloHaftalar[i, 2].Width = 100;

                string brutMaasYazi = String.Format("{0:C2}", kayit.BrutUcret);
                tabloHaftalar[i, 2].AddParagraph().AppendText("  " + brutMaasYazi + " ");
                tabloHaftalar.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 2].Width = 100;

                tabloHaftalar[i, 3].AddParagraph().AppendText("  " + kayit.IzinGunu + " ");
                tabloHaftalar.Rows[i].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 3].Width = 90;

                if (kayit.HaftaSonuIzinVar == false)
                {
                    kayit.HaftaSonuUcret = 0;
                }
                string mesaiUcretYazi = String.Format("{0:C2}", kayit.HaftaSonuUcret);
                tabloHaftalar[i, 4].AddParagraph().AppendText("  " + mesaiUcretYazi + " ");
                tabloHaftalar.Rows[i].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 4].Width = 100;

                genelToplamFM = genelToplamFM + kayit.HaftaSonuUcret;
                string genelToplamYazi = String.Format("{0:C2}", genelToplamFM);
                tabloHaftalar[i, 5].AddParagraph().AppendText("  " + genelToplamYazi + " ");
                tabloHaftalar.Rows[i].Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloHaftalar[i, 5].Width = 100;

            }

            // Kaydetme Bölümü....
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "HaftaIzinRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //Save the stream as a file in the device and invoke it for viewing
       //    var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("FM_HaftaIzin.docx", "application/msword", stream);

        }


        // FAZLA MESAİ RAPOR BÖLÜMÜ.....................
        private async void FazlaMesaiRapor()
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

            //Set page size of the section
         //   section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);

            section.PageSetup.PageSize = PageSize.A4;

            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;

            //Appends paragraph
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("FAZLA MESAİ RAPORU") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.Bold = true;

         
   
        
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            

            IWTable tablooBaslangic = section.AddTable();

            tablooBaslangic.TableFormat.Borders.LineWidth = 0.5f;
            tablooBaslangic.TableFormat.CellSpacing = 0;
            WCharacterFormat cf1 = new WCharacterFormat(document);
            cf1.Bold = true;
            cf1.FontSize = 14;
            cf1.FontName = "Times New Roman";

            WCharacterFormat cf3 = new WCharacterFormat(document);
            cf3.Bold = true;
            cf3.FontSize = 20;
            cf3.FontName = "Times New Roman";

            //  tablooBalangic.ApplyStyle(BuiltinTableStyle.DarkList);
            tablooBaslangic.ResetCells(5, 2);
            tablooBaslangic.IndentFromLeft = 80;

            tablooBaslangic[0, 0].AddParagraph().AppendText(" Ad Soyad :".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[0, 0].Width = 200;

            tablooBaslangic[0, 1].AddParagraph().AppendText(" " + "----------------------" + "").ApplyCharacterFormat(cf1);
            tablooBaslangic[0, 1].Width = 200;

            tablooBaslangic[1, 0].AddParagraph().AppendText(" Başlangıç Tarihi : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[1, 1].AddParagraph().AppendText(" "+FM.BasTarihMesai.ToShortDateString() +"\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic[1, 0].Width = 200;
            tablooBaslangic[1, 1].Width = 200;

            tablooBaslangic[2, 0].AddParagraph().AppendText(" Bitiş Tarihi : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[2, 1].AddParagraph().AppendText("" + FM.BitTarihMesai.ToShortDateString() + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic[2, 0].Width = 200;
            tablooBaslangic[2, 1].Width = 200;

            tablooBaslangic[3, 0].AddParagraph().AppendText(" Sözleşme Saati : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[3, 1].AddParagraph().AppendText(" " + FM.SozlesmeCalismaSaat + "\t ").ApplyCharacterFormat(cf1);
            tablooBaslangic[3, 0].Width = 200;
            tablooBaslangic[3, 1].Width = 200;

            tablooBaslangic[4, 0].AddParagraph().AppendText(" Haftalık Çalışma Saati : ".ToUpper()).ApplyCharacterFormat(cf1);
            tablooBaslangic[4, 1].AddParagraph().AppendText("" + FM.HaftaCalismaSaat2 ).ApplyCharacterFormat(cf1);
            tablooBaslangic[4, 0].Width = 200;
            tablooBaslangic[4, 1].Width = 200;

            tablooBaslangic.Rows[0].Height = 50;
            tablooBaslangic.Rows[1].Height = 50;
            tablooBaslangic.Rows[2].Height = 50;
            tablooBaslangic.Rows[3].Height = 50;
            tablooBaslangic.Rows[4].Height = 50;

            tablooBaslangic.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooBaslangic.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooBaslangic.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();





            IWTable tabloo = section.AddTable();

            tabloo.TableFormat.Borders.LineWidth = 0.3f;
            tabloo.TableFormat.CellSpacing = 0;


            tabloo.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo.ResetCells(6, 2);
            tabloo.IndentFromLeft = 80;



            tabloo[0, 0].AddParagraph().AppendText(" Toplam Brüt :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamBrut = String.Format("{0:C2}", FM.Toplam);
            tabloo[0, 1].AddParagraph().AppendText("" + toplamBrut + "\t").ApplyCharacterFormat(cf1);
            tabloo[0, 0].Width = 200;
            tabloo[0, 1].Width = 200;

            tabloo[1, 0].AddParagraph().AppendText(" Sgk :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamSgk = String.Format("{0:C2}", FM.ToplamSGK);
            tabloo[1, 1].AddParagraph().AppendText("\n   " + toplamSgk + "\t").ApplyCharacterFormat(cf1);
            tabloo[1, 0].Width = 200;
            tabloo[1, 1].Width = 200;

            tabloo[2, 0].AddParagraph().AppendText(" Vergi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamVergi = String.Format("{0:C2}", FM.ToplamVergi);
            tabloo[2, 1].AddParagraph().AppendText("" + toplamVergi + "\t").ApplyCharacterFormat(cf1);
            tabloo[2, 0].Width = 200;
            tabloo[2, 1].Width = 200;

            tabloo[3, 0].AddParagraph().AppendText("Damga Vergisi :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamDamgaV = String.Format("{0:C2}", FM.ToplamDamgaVergi);
            tabloo[3, 1].AddParagraph().AppendText("  " + toplamDamgaV + "\t").ApplyCharacterFormat(cf1);
            tabloo[3, 0].Width = 200;
            tabloo[3, 1].Width = 200;

            tabloo[4, 0].AddParagraph().AppendText(" İşsizlik Sigortası :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamIssizlik = String.Format("{0:C2}", FM.ToplamIssizlik);
            tabloo[4, 1].AddParagraph().AppendText(" " + toplamIssizlik + "\t").ApplyCharacterFormat(cf1);
            tabloo[4, 0].Width = 200;
            tabloo[4, 1].Width = 200;


            tabloo[5, 0].AddParagraph().AppendText(" Net :\t ".ToUpper()).ApplyCharacterFormat(cf1);
            string toplamNet = String.Format("{0:C2}", FM.ToplamNet);
            tabloo[5, 1].AddParagraph().AppendText("" + toplamNet + "\t").ApplyCharacterFormat(cf1);
            tabloo[5, 0].Width = 200;
            tabloo[5, 1].Width = 200;

            tabloo.Rows[0].Height = 50;
            tabloo.Rows[1].Height = 50;
            tabloo.Rows[2].Height = 50;
            tabloo.Rows[3].Height = 50;
            tabloo.Rows[4].Height = 50;
            tabloo.Rows[5].Height = 50;

            tabloo.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[3].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[4].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[3].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo.Rows[4].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Maaş Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";

            paragraph = section.AddParagraph();
            // Dönem İçindeki Maaşların Tablosu.....
            int maasSat = FM.MaasBilgi.Count + 1;
            IWTable tablooMaas = section.AddTable();

            tablooMaas.TableFormat.Borders.LineWidth = 0.3f;
            tablooMaas.TableFormat.CellSpacing = 0;

            tablooMaas.ApplyStyle(BuiltinTableStyle.LightGrid);

            tablooMaas.ResetCells(maasSat, 3);
            tablooMaas.IndentFromLeft = 30;

            tablooMaas[0, 0].AddParagraph().AppendText("\n Dönemi :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas[0, 1].AddParagraph().AppendText("\n Brüt :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas[0, 2].AddParagraph().AppendText("\n Net :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooMaas[0, 0].Width = 100;
            tablooMaas[0, 1].Width = 150;
            tablooMaas[0, 2].Width = 150;

            int s1 = 0;

            var listeMaas = FM.MaasBilgi.OrderByDescending(o => o.yilBas).ToList();

            foreach (var t in listeMaas)
            {
                s1 = s1 + 1;
                tablooMaas.Rows[s1].Height = 35;

                tablooMaas[s1, 0].AddParagraph().AppendText( t.yil ).ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 0].Width = 100;

                string brutMaasYazi = String.Format("{0:C2}", t.brutMaas);
                tablooMaas[s1, 1].AddParagraph().AppendText( brutMaasYazi).ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 1].Width = 150;

                string netMaasYazi = String.Format("{0:C2}", t.netMaas);
                tablooMaas[s1, 2].AddParagraph().AppendText( netMaasYazi ).ApplyCharacterFormat(cf1);
                tablooMaas.Rows[s1].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooMaas[s1, 2].Width = 150;

            }

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();


            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("İzin Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            paragraph = section.AddParagraph();

            // Dönem İçindeki İzinlerin Tablosu........
            int izinSat = FM.IzinKaytilariBilgi.Count + 1;
            IWTable tablooIzin = section.AddTable();
            tablooIzin.IndentFromLeft = 30;

            tablooIzin.TableFormat.Borders.LineWidth = 0.3f;
            tablooIzin.TableFormat.CellSpacing = 0;

            tablooIzin.ApplyStyle(BuiltinTableStyle.LightGrid);
            tablooIzin.ResetCells(izinSat, 3);
            int s2 = 0;
            tablooIzin[0, 0].AddParagraph().AppendText("\n Başlangıç :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin[0, 0].Width = 110;
            tablooIzin.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tablooIzin[0, 1].AddParagraph().AppendText("\n Bitiş  :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 1].Width = 110;

            tablooIzin[0, 2].AddParagraph().AppendText("\n Açıklama :\t \n".ToUpper()).ApplyCharacterFormat(cf1);
            tablooIzin.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tablooIzin[0, 2].Width = 220;

            var listeIzin = FM.IzinKaytilariBilgi.OrderBy(o => o.BaslangicTar).ToList();
            foreach (var t in listeIzin)
            {
                s2 = s2 + 1;
                tablooIzin.Rows[s2].Height = 35;

                tablooIzin[s2, 0].AddParagraph().AppendText( t.BaslangicTar.ToShortDateString() );
                tablooIzin.Rows[s2].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 0].Width = 110;

                tablooIzin[s2, 1].AddParagraph().AppendText( t.BitisTar.ToShortDateString() );
                tablooIzin.Rows[s2].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 1].Width = 110;
                string aciklama = "";
                if(t.Aciklama != null)
                {
                    aciklama = t.Aciklama;
                }
                tablooIzin[s2, 2].AddParagraph().AppendText( aciklama );
                tablooIzin.Rows[s2].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tablooIzin[s2, 2].Width = 220;

            }

            //paragraph = section.AddParagraph();
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            //textRange = paragraph.AppendText("Yıl Bazında Fazla Mesai") as WTextRange;
            //textRange.CharacterFormat.FontSize = 15f;
            //textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.FontName = "Calibri";

            // YILLAR BAZINDA TOPLAM......
            //int yil1 = FM.BasTarihMesai.Year;
            //int yil2 = FM.BitTarihMesai.Year;
            //int ss1 = 0;
            //while (yil1 <= yil2)
            //{
            //    ss1 = ss1 + 1;
            //    yil1 = yil1 + 1;
            //}

            //IWTable tabloYillar = section.AddTable();

            //tabloYillar.TableFormat.Borders.LineWidth = 2f;
            //tabloYillar.TableFormat.CellSpacing = 10;

            //tabloYillar.ApplyStyle(BuiltinTableStyle.LightGrid);
            //tabloYillar.ResetCells(ss1 + 1, 5);



            //var FMListe = FM.FMHaftalarBilgi.ToList();
            //tabloYillar[0, 0].AddParagraph().AppendText("\n Yıl \t\n");
            //tabloYillar[0, 1].AddParagraph().AppendText("\n Başlangıç \t\n");
            //tabloYillar[0, 2].AddParagraph().AppendText("\n Bitiş \t\n");
            //tabloYillar[0, 3].AddParagraph().AppendText("\n Hafta Sayısı \t\n");
            //tabloYillar[0, 4].AddParagraph().AppendText("\n BrütToplam \t\n");

            //int ss3 = 1;
            //while (yil1 < yil2)
            //{
            //    var liste = FMListe.Where(o => o.BasTarih.Year == yil1).ToList();

            //    decimal mik1 = 0;
            //    foreach (var t in liste)
            //    {
            //        mik1 = mik1 + t.MesaiUcret;

            //    }
            //    string miktarYazi = String.Format("{0:C2}", mik1);

            //    tabloYillar[ss3, 0].AddParagraph().AppendText("\n" + yil1 + "\t");
            //    tabloYillar[ss3, 1].AddParagraph().AppendText("\n" + liste[0].BasTarih.ToShortDateString() + "\t");
            //    tabloYillar[ss3, 2].AddParagraph().AppendText("\n" + liste[liste.Count - 1].BitTarih.ToShortDateString() + "\t");
            //    tabloYillar[ss3, 3].AddParagraph().AppendText("\n" + liste.Count + "\t");
            //    tabloYillar[ss3, 4].AddParagraph().AppendText("\n" + miktarYazi + "\t");
            //    yil1 = yil1 + 1;

            //    ss3 = ss3 + 1;
            //}


            //*-----------------------------------------
            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Haftalık Fazla Mesai Tablosu".ToUpper()) as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Times New Roman";
            int satSay = FM.FMHaftalarBilgi.Count + 1;
            // Sira- Baş-Bit-MaaşBrüt- Haftalık Saat -Mesai Ücreti -Genel Toplam...
            int sutSay = 6;
            paragraph = section.AddParagraph();
            IWTable tabloo2 = section.AddTable();

            tabloo2.TableFormat.Borders.LineWidth = 0.2f;
            tabloo2.TableFormat.CellSpacing = 0;

            tabloo2.ApplyStyle(BuiltinTableStyle.LightGrid);

            tabloo2.ResetCells(satSay, sutSay);

            tabloo2[0, 0].AddParagraph().AppendText("Sıra".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 1].AddParagraph().AppendText(" Tarih  ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 2].AddParagraph().AppendText(" Toplam Saat  :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 3].AddParagraph().AppendText(" Saatlik Ücret :".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 4].AddParagraph().AppendText(" Ücret : ".ToUpper()).ApplyCharacterFormat(cf1);
            tabloo2[0, 5].AddParagraph().AppendText(" Genel Toplam :".ToUpper()).ApplyCharacterFormat(cf1);
            //tabloo2[0, 6].AddParagraph().AppendText("").ApplyCharacterFormat(cf1);

            tabloo2[0, 0].Width = 45;
            tabloo2[0, 1].Width = 95;
            tabloo2[0, 2].Width =95 ;
            tabloo2[0, 3].Width = 95;
            tabloo2[0, 4].Width = 95;
            tabloo2[0, 5].Width = 95;

            tabloo2.Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[0].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[0].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[0].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[0].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            tabloo2.Rows[0].Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            tabloo2.Rows[0].Height = 45;
         


            int i = 0;
            decimal genelToplamFM = 0;
            // for(int i=1; i<satSay;i++)

            var kayitListe = FM.FMHaftalarBilgi.OrderBy(o => o.BasTarih).ToList();
           foreach(var kayit in kayitListe)
            {
                i = i + 1;
                tabloo2.Rows[i].Height = 33;

                tabloo2[i, 0].AddParagraph().AppendText( i.ToString() );
                tabloo2.Rows[i].Cells[0].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 0].Width = 45;

                tabloo2[i, 1].AddParagraph().AppendText( kayit.BasTarih.ToShortDateString() + "\n"  + kayit.BitTarih.ToShortDateString());
                tabloo2.Rows[i].Cells[1].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 1].Width = 95;

                //tabloo2[i, 2].AddParagraph().AppendText("----");
                //tabloo2.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //tabloo2[i, 2].Width = 80;

              //  string brutMaasYazi= String.Format( );
                tabloo2[i, 2].AddParagraph().AppendText(kayit.FazlaMesaiOHaftadaki.ToString());
                tabloo2.Rows[i].Cells[2].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 2].Width = 95;

                string brutMaasYazi = String.Format("{0:C2}", kayit.BrutUcret);
                tabloo2[i, 3].AddParagraph().AppendText( brutMaasYazi);
                tabloo2.Rows[i].Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 3].Width = 95;

                if (kayit.FazlaMesaiVar == false)
                {
                    kayit.MesaiUcret = 0;
                }

                string mesaiUcretYazi = String.Format("{0:C2}", kayit.MesaiUcret);
                tabloo2[i, 4].AddParagraph().AppendText( mesaiUcretYazi );
                tabloo2.Rows[i].Cells[4].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 4].Width = 95;

                genelToplamFM = genelToplamFM + kayit.MesaiUcret;
                string genelToplamYazi = String.Format("{0:C2}", genelToplamFM);
                tabloo2[i, 5].AddParagraph().AppendText( genelToplamYazi );
                tabloo2.Rows[i].Cells[5].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                tabloo2[i, 5].Width = 95;

            }


            // Kaydetme Bölümü....
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);


            var sayfa = new Views.PdfV.PdfView(stream, "FazlaMesaiRapor.docx", "docx");
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            //Save the stream as a file in the device and invoke it for viewing
     //       var yoll = await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView2("FM_FM.docx", "application/msword", stream);


        }


    }
}
