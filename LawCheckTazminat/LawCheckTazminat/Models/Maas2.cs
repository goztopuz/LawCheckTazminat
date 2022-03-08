using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
    public class Maas2 :ViewModelBase
    {
        string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id= value;
                OnPropertyChanged();
            }
        }

        string _dosyaId;
        public string dosyaId {
            get => _dosyaId;
            set
            {
                _dosyaId= value    ;
                OnPropertyChanged();                    
            }
        }

        decimal _brutMaas;
        public decimal brutMaas {
            get => _brutMaas;
            set
            {
                _brutMaas = value;
             //   OnPropertyChanged();

            }
        }

        decimal _netMaas;
        public decimal netMaas { 
            get => _netMaas; 
            set {
                _netMaas= value;
               // OnPropertyChanged();
            }
        }

        DateTime _yilBas;
        public DateTime yilBas {
            get=>_yilBas;
            set {
                _yilBas= value;
                OnPropertyChanged();
            } 
        }

        DateTime _yilBit;
        public DateTime yilBit { 
            get => _yilBit; 
            set {
                _yilBit= value;
                OnPropertyChanged();
            }
        }

        string _yil;
        public string yil { 
            get => _yil;
            set {
                yil= value;
               // OnPropertyChanged();

            }
        }

        int _yilBolum;
        public int yilBolum {
            get => _yilBolum;
            set { 
                _yilBolum= value;
                OnPropertyChanged();
            }
        }

        string _ekBilgi1;
        public string ekBilgi1
        {
            get=>_ekBilgi1;
            set
            {
                _ekBilgi1 = value;
                OnPropertyChanged();
            }
        }

        string _ekBilgi2;
        public string ekBilgi2 {
            get => _ekBilgi2;
            set {
                _ekBilgi2 = value;
                OnPropertyChanged();
            }
        }

        double _ekBilgi3;
        public double ekBilgi3 {
            get => _ekBilgi3;
            set
            {
                _ekBilgi3= value;
                OnPropertyChanged();

            } 
        }
        long _ekBilgi4;
        public long  ekBilgi4 { 
            get => _ekBilgi4;
            set {
                _ekBilgi4 = value;
                OnPropertyChanged();
            } 
        }


    }

}
