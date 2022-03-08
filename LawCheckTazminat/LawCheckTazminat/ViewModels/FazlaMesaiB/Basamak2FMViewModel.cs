using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak2FMViewModel:ViewModelBase
    {
        public Basamak2FMViewModel(FazlaMesai _fm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fm;
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
            if (FM.BasTarihMesai > FM.BitTarihMesai)
            {
                this.HataMesaji = "Başlangıç Bitiş Tarihinden Sonra";
                durum = false;
            }
            if (FM.BitTarihMesai.Date.Year > DateTime.Now.Year)
            {
                this.HataMesaji = "Bitiş Tarihi Gelecek Bir Tarih";
                durum = false;
            }

            if (FM.BitTarihMesai.AddYears(-5) > FM.BasTarihMesai)
            {
                bool answer = await Application.Current.MainPage.DisplayAlert("Süre Aşımı?", "Tarihlerde 5 Yıllık Süre Aşımı Var Devam Etmek İstiyormsunuz?", "Evet", "Hayır");


                if (answer == false)
                {
                    durum = false;
                }
            }

            if(FM.SozlesmeCalismaSaat<0)
            {
                this.HataMesaji = "Sözleşme Saati Negatif  Olamaz";
                durum = false;
            }

            if(FM.HaftaCalismaSaat<0)
            {
                this.HataMesaji = "Çalışma Saati Negatif Olamaz";
                durum = false;
            }

            if(FM.HaftaCalismaSaat<FM.SozlesmeCalismaSaat)
            {
                this.HataMesaji = "Haftalık Çalışma Saati - Sözleşme Saatinden Düşük Olamaz";
                durum = false;

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


            if (await Kontrol() == false)
            {
                IsBusy = false;
                return;

            }


            FM.HaftaCalismaSaat2 = FM.HaftaCalismaSaat;


            this.HataMesaji = "";
            var basamak3 = new Basamak3FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(basamak3);

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
