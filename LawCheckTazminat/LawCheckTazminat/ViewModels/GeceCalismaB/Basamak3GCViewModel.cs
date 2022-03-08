using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.GeceCalisma;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.GeceCalismaB
{
    public class Basamak3GCViewModel: ViewModelBase
    {

       
        public Basamak3GCViewModel(Models.GeceCalisma _gece)
        {
            this.CakismaVar = false;

            this.GC = new GeceCalisma();
            this.GC = _gece;

            Kontrol();



        }

       

        private void HaftalikTatilGunuKontrol()
        {
            // Tatil Günlerinden Birinde Çalışma İşaretlenmiş mi?
            bool ckVar = false;
            string gunn = GC.TatilGunu;

            if (gunn == "Pazar")
            {
                if (GC.CPzr == true || GC.PPzt == true)
                {
                    ckVar = true;
                }
            }
            else if (gunn == "Pazartesi")
            {
                if (GC.PPzt == true || GC.PSal == true)
                {
                    ckVar = true;
                }
            }
            else if (gunn == "Salı")
            {
                if (GC.PSal == true || GC.SCar==true)
                {
                    ckVar = true;
                }
            }
            else if(gunn=="Çarşamba")
            {
                if(GC.SCar==true || GC.CPer==true)
                {
                    ckVar = true;
                }
            }
            else if(gunn=="Perşembe")
            {
                if(GC.CPer==true || GC.PCum==true)
                {
                    ckVar = true;
                }
            }
            else if(gunn=="Cuma")
            {
                if(GC.PCum==true || GC.CCmt==true)
                {
                    ckVar = true;
                }
            }
            else if(gunn=="Cumartesi")
            {
                if(GC.CCmt==true || GC.CPzr==true)
                {
                    ckVar = true;
                }
            }

            CakismaVar = ckVar;
        }

        private bool _cakismaVar { get; set; }
        public bool CakismaVar
        {
            get => _cakismaVar;
            set
            {
                _cakismaVar = value;
                OnPropertyChanged();
            }
        }
        


        private GeceCalisma _gc;
        public GeceCalisma GC
        {
            get => _gc;
            set
            {
                _gc = value;
                OnPropertyChanged();
            }
        }

        private bool Kontrol()
        {
            bool durum = true;

            HaftalikTatilGunuKontrol();

            if (CakismaVar== false)
            {
                return true;
            }
            else
            {
                int ss1 = 0;
                if(GC.TatilVardiyaEkle==true)
                {
                    ss1 = ss1 + 1;
                }
                if(GC.TatilVardiyaiDus==true)
                {
                    ss1 = ss1 + 1;
                }


                if(ss1==0)
                {
                    this.HataMesaji = "Tatil Çakışması Var Seçneklerden Birini Seçiniz";
                    return false;
                }

                if(ss1==2)
                {
                    this.HataMesaji = "2 Seçenek Birden Seçilemez.";
                    return false;

                }


            }
           

            return durum;
        }

        public ICommand KontrolDusCommand => new Command<bool>(OnKontrolDus);
        private void OnKontrolDus(bool durum)
        {
            if(durum == true)
            {
                GC.TatilVardiyaiDus = true;
                GC.TatilVardiyaEkle = false;
            }
            else
            {
                GC.TatilVardiyaiDus = false;
                GC.TatilVardiyaEkle = true;
            }

        }

        public ICommand KontrolEkleCommand => new Command<bool>(OnKontrolEkle);
        private void OnKontrolEkle(bool durum)
        {
            if (durum == true)
            {
                GC.TatilVardiyaiDus = false;
                GC.TatilVardiyaEkle = true;
            }
            else
            {
                GC.TatilVardiyaiDus = true;
                GC.TatilVardiyaEkle = false;
            }
        }


    public ICommand IlerleCommand => new Command(OnIlerle);
        async private void OnIlerle(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;

            if (Kontrol() == false)
            {
                IsBusy = false;
                return;
            }

            //İlerle.....

            var sayfa = new Basamak4GCView(GC);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            this.HataMesaji = "";
            this.IsBusy = false;
        }

        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;

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

        //---------------------------------------------------

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



    }
}
