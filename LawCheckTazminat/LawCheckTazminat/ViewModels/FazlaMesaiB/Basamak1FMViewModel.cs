using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak1FMViewModel: ViewModelBase
    {
        public Basamak1FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;

            if(FM.duzenlemede== false)
            {
                FM.BasTarih = DateTime.Now;
                FM.BitTarih = DateTime.Now;
            }

        }

        private FazlaMesai _fazlaMesai;

        public FazlaMesai FM
        {
            get => _fazlaMesai;
            set
            {
                _fazlaMesai = value;
                OnPropertyChanged();
            }
        }


     async   private Task<bool> Kontrol()
        {
            bool durum = true;
            if(FM.BasTarih> FM.BitTarih)
            {
                this.HataMesaji = "Başlangıç Bitiş Tarihinden Sonra";
                durum = false;
            }
            if(FM.BitTarih.Date.Year> DateTime.Now.Year)
            {
                this.HataMesaji = "Bitiş Tarihi Gelecek Bir Tarih";
                durum = false;
            }

            if(FM.BitTarih.AddYears(-5) > FM.BasTarih)
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Süre Aşımı?", "Tarihlerde 5 Yıllık Süre Aşımı Var Devam Etmek İstiyormsunuz?", "Evet","Hayır");


                if(answer== false)
                {
                    durum = false;
                }
            }


            return durum;
        }


        public ICommand IlerleCommand => new Command(OnIlerle);

        async private void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            if(await Kontrol()==false)
            {
                IsBusy = false;
                return;

            }



            FM.BasTarihMesai = FM.BasTarih;
            FM.BasTarihHaftaSonu = FM.BasTarih;
            FM.BasTarihResmiTatil = FM.BasTarih;

            FM.BitTarihMesai = FM.BitTarih;
            FM.BitTarihHaftaSonu = FM.BitTarih;
            FM.BitTarihResmiTatil = FM.BitTarih;

            this.HataMesaji = "";

            var basamak2 = new Basamak2FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak2);

            IsBusy = false;
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
