using System;
namespace LawCheckTazminat.Models
{
    public class ResmiTatiller
    {


        public string Id { get; set; }
        public int yil { get; set; }
        public DateTime tarih { get; set; }
        public string aciklama { get; set; }
        public int tur { get; set; }
        public string ekbilgi { get; set; }
        public double tam { get; set; }
        public string ekBilgi1 { get; set; }
        public string ekBilgi2 { get; set; }
        public Nullable<double> ekBilgi3 { get; set; }
        public Nullable<long> ekBilgi4 { get; set; }
        public string ekBilgi5 { get; set; }
        public string ekBilgi6 { get; set; }

    }
}
