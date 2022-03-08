using System;
using LawCheckTazminat.ViewModels.Base;
using LawCheckTazminat.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace LawCheckTazminat.ViewModels.IsgucuKayipB
{
    public class MasrafEkleIsgucuViewModel:ViewModelBase
    {
        public MasrafEkleIsgucuViewModel(IsgucuKayip _kayip, MasrafIsgucu _mm,
            ObservableCollection<MasrafIsgucu> Liste)
        {
            this.IsGucu = new IsgucuKayip();
            this.IsGucu = _kayip;

            MasrafListe = Liste;

            this.MasrafBilgi = new MasrafIsgucu();
            this.MasrafBilgi = _mm;
        }

        private ObservableCollection<MasrafIsgucu> _masrafListe;
        public ObservableCollection<MasrafIsgucu> MasrafListe
        {
            get => _masrafListe;
            set
            {
                _masrafListe = value;
                OnPropertyChanged();
            }
        }

        private MasrafIsgucu _masraf;
        public MasrafIsgucu MasrafBilgi
        {
            get => _masraf;
            set
            {
                _masraf = value;
                OnPropertyChanged();
            }
        }

        private IsgucuKayip _isgucuKayip;

        public IsgucuKayip IsGucu
        {
            get => _isgucuKayip;

            set
            {
                _isgucuKayip = value;
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

        public ICommand IptalCommand => new Command(OnIptal);
        async private void OnIptal(Object obj)
        {

            await Application.Current.MainPage.Navigation.PopModalAsync();


        }

        public ICommand KaydetCommand => new Command(OnKaydet);
        async private void OnKaydet(object obj)
        {
            if (this.MasrafBilgi.miktar == 0)
            {
                this.HataMesaji = "Miktar Boş Kalamaz";
                return;

            }

            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;


            // Ekleme Kodları...
            if (MasrafBilgi.Id == null || MasrafBilgi.Id == "")
            {
                MasrafBilgi.Id = Guid.NewGuid().ToString().Substring(0, 10).ToString();
                MasrafListe.Add(MasrafBilgi);

            }
            else
            {
                MasrafIsgucu masraff = this.MasrafListe.Where(o => o.Id == MasrafBilgi.Id).FirstOrDefault();

                masraff.ekBilgi1 = MasrafBilgi.ekBilgi1;
                masraff.ekBilgi2 = MasrafBilgi.ekBilgi2;
                masraff.masrafTur1 = MasrafBilgi.masrafTur1;
                masraff.masraftur2 = MasrafBilgi.masraftur2;
                masraff.miktar = MasrafBilgi.miktar;
                masraff.odemeTur = MasrafBilgi.odemeTur;
                masraff.tarihBas = MasrafBilgi.tarihBas;
                masraff.tarihBit = MasrafBilgi.tarihBit;

            }
            MessagingCenter.Send<ObservableCollection<MasrafIsgucu>>(MasrafListe, "YenileMasraf");

            await Application.Current.MainPage.Navigation.PopModalAsync();

            IsBusy = false;

        }

    }
}




