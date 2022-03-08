using System;
using System.Windows.Input;
using LawCheckTazminat.Models;
using LawCheckTazminat.Services;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Views.FazlaMesaiV;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.FazlaMesaiB
{
    public class Basamak10BFMViewModel : ViewModelBase
    {
        FazlaMesaiService islem = new FazlaMesaiService();
        public Basamak10BFMViewModel(FazlaMesai _fmm)
        {
            this.FM = new FazlaMesai();
            this.FM = _fmm;

            if(FM.duzenlemede==false)
            {
                FM.VergiMatrah = 0;
                FM.Vergiyil = DateTime.Now.Year;
            }
        }

        private FazlaMesai _fazlamesai;
        public FazlaMesai FM
        {
            get => _fazlamesai;
            set
            {
                _fazlamesai = value;
                OnPropertyChanged();

            }
        }

        public ICommand KaydetCommand => new Command(OnKaydet);
        private async void OnKaydet(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            // Verilerin Kontrolü...
            //...................Yıl Kontrolü Yapacak..........

            if (FM.Vergiyil > DateTime.Now.Year)
            {
                this.HataMesaji = "Vergi Yılı Gelecek Yıl";
                this.IsBusy = false;
                return;
            }



            // KAYDET-- TÜM Fazla Mesai
            //----------------------

            if (FM.Id == "" || FM.Id == null)
            {
                // Yeni Kayıt

                FM.Id = Guid.NewGuid().ToString().Substring(0, 10);
                FM.EskiId = "";
            }
            else
            {
                FM.EskiId = FM.Id;
                FM.Id = Guid.NewGuid().ToString().Substring(0, 10);
            }

            foreach(var t in FM.FMHaftalarBilgi)
                {
                    t.FazlaMesaiId = FM.Id;
                    t.fazlaMesai = FM;
                }

            foreach(var t in FM.HaftalikIzinHaftalarBilgi)
                {
                    t.FazlaMesaiId = FM.Id;
                    t.fazlaMesai = FM;
                }

            foreach(var t in FM.IzinGunleriBilgi)
                {
                    t.FazlaMesaiId = FM.Id;
                    t.fazlaMesai = FM;
                }

            foreach(var t in FM.IzinKaytilariBilgi)
                {
                    t.FazlaMesaiId = FM.Id;
                    t.fazlaMesai = FM;
                }

            foreach(var t in FM.MaasBilgi)
                {
                    t.mesaiId = FM.Id;
                    t.fazlaMesai = FM;
                }

            foreach(var  t in FM.ResmiTatilBilgi)
                {
                    t.mesaiId = FM.Id;
                    t.fazlaMesai = FM;
                    
                }

         await   islem.AddItemAsync(FM);

            
            //else
            //{
            //    // Düzenle -Güncelle

            //    foreach (var t in FM.FMHaftalarBilgi)
            //    {
            //        t.FazlaMesaiId = FM.Id;
            //    }
            //    foreach (var t in FM.HaftalikIzinHaftalarBilgi)
            //    {
            //        t.FazlaMesaiId = FM.Id;
            //    }
            //    foreach (var t in FM.IzinGunleriBilgi)
            //    {
            //        t.FazlaMesaiId = FM.Id;
            //    }
            //    foreach (var t in FM.IzinKaytilariBilgi)
            //    {
            //        t.FazlaMesaiId = FM.Id;
            //    }
            //    foreach (var t in FM.MaasBilgi)
            //    {
            //        t.mesaiId = FM.Id;
            //    }
            //    foreach (var t in FM.ResmiTatilBilgi)
            //    {
            //        t.mesaiId = FM.Id;
            //    }

            //    await islem.UpdateItem(FM);

            //}


            var sayfa = new Basamak11FMView(FM);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);

            IsBusy = false;
        }
       

        //------------------------
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
