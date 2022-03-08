using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
   public class AracMarka
    {
        public int MarkaId { get; set; }
        public string Marka { get; set; }
    }

    public class AracModel
    {
        public int ModelId { get; set; }
        public string Model { get; set; }
        public int KayitId { get; set; }

    }
    public class AracDeger
    {
        public string Model { get; set; }
        public int ModelId { get; set; }
        public string Marka { get; set; }
        public int MarkaId { get; set; }
        public int Yil { get; set; }
        public int KayitId { get; set; }

        public decimal Deger { get; set; }

    }
    public class KaskoBilgi2021
    {
        public int Id { get; set; }
        public int MarkaKodu { get; set; }
        public int TipKodu { get; set; }
        public string MarkaAdi { get; set; }
        public string TipAdi { get; set; }

        public decimal D2021 { get; set; }

        public decimal D2020 { get; set; }

        public decimal D2019 { get; set; }

        public decimal D2018 { get; set; }

        public decimal D2017 { get; set; }

        public decimal D2016 { get; set; }

        public decimal D2015 { get; set; }

        public decimal D2014 { get; set; }

        public decimal D2013 { get; set; }

        public decimal D2012 { get; set; }

        public decimal D2011 { get; set; }

        public decimal D2010 { get; set; }

        public decimal D2009 { get; set; }

        public decimal D2008 { get; set; }

        public decimal D2007 { get; set; }


    }

}
