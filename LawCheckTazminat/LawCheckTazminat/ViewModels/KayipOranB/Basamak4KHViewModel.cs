﻿using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.KayipOranB
{
    public class Basamak4KHViewModel : ViewModelBase
    {
        public Basamak4KHViewModel(KayipOranHesap kh, 
            AltKategori2 _altkategori2)
        {
            Liste.Clear();
            this.KH = new KayipOranHesap();
            this.KH = kh;

            this.Baslik1 = kh.AnaKategoriAciklama;
            this.Baslik2 = kh.AltKategori1Aciklama;
            this.Baslik3 = kh.AltKategori2Aciklama;


            var aa = new KayipOranAltKategori3();

            Liste = aa.ListeAltKategori3.Where(o => o.AnaKategoriId == kh.AnaKategoriId && o.AltKategori1Id == kh.AltKategori1Id
            && o.AltKategori2Id== kh.AltKategori2Id).ToList();


            if (Liste.Count == 0)
            {
                ListeVar = false;
                ButtonVar = true;

            }
            else
            {
                ListeVar = true;
                ButtonVar = false;
            }





        }



        private bool _listeVar;
        public bool ListeVar
        {
            get => _listeVar;
            set
            {
                _listeVar = value;
                OnPropertyChanged();
            }
        }

        private bool _buttonVar;
        public bool ButtonVar
        {
            get => _buttonVar;
            set
            {
                _buttonVar = value;
                OnPropertyChanged();
            }
        }


        private KayipOranHesap _kh;
        public KayipOranHesap KH
        {
            get => _kh;
            set
            {
                _kh = value;
                OnPropertyChanged();
            }
        }


        private string _baslik1;
        public string Baslik1
        {
            get => _baslik1;
            set
            {
                _baslik1 = value;
                OnPropertyChanged();
            }
        }

        private string _baslik2;
        public string Baslik2
        {
            get => _baslik2;
            set
            {
                _baslik2 = value;
                OnPropertyChanged();
            }
        }

        private string _baslik3;
        public string Baslik3
        {
            get => _baslik3;
            set
            {
                _baslik3 = value;
                OnPropertyChanged();
            }
        }


        private List<AltKategori3> _liste = new List<AltKategori3>();
        public List<AltKategori3> Liste
        {
            get => _liste;
            set
            {
                _liste = value;
                OnPropertyChanged();
            }
        }




        public ICommand IlerleCommand => new Command(OnIlerle);
        private async void OnIlerle(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;



            KH.AltKategori3Id = "";
            KH.AltKategori3Aciklama = "";

            AltKategori3 alt3 = new AltKategori3();
            alt3.Id = "";
            alt3.Kategori = "";

            // MEslek Seçim Sayfa Geçme Bölümü
            var sayfa = new Views.KayipOranV.Basamak5KH(KH);
            await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);


            this.HataMesaji = "";
            IsBusy = false;

        }



        public ICommand AltKategori3TappedCommand => new Command<AltKategori3>(OnAltKategori3Tapped);

        private async void OnAltKategori3Tapped(AltKategori3 _altKategori3)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;

            KayipOranHesap KH = new KayipOranHesap();



            KH.AltKategori3Id = _altKategori3.Id;
            KH.AltKategori3Aciklama = _altKategori3.Kategori;


            // 5.. Sayfaya Geçme Bölümü

            //var sayfa = new Views.KayipOranV.Basamak4KH(KH, _altKategori2);
            //await Application.Current.MainPage.Navigation.PushModalAsync(sayfa);



            IsBusy = false;

        }



        public ICommand IptalCommand => new Command(OnIptal);
        private async void OnIptal(object obj)
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
    }


}
