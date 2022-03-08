using System;
namespace LawCheckTazminat.Models
{
    public class Destek
    {

        public string Id { get; set; }

        public string UserId { get; set; }

        public string DestekTuru { get; set; }

        public DateTime Tarih { get; set; }

        public string Baslik { get; set; }

        public string uygulamaId { get; set; }

        public string Aciklama { get; set; }

        public string Durum { get; set; }

        public string Tamamlayan { get; set; }

        public DateTime DurumTarih { get; set; }

        public string telefon { get; set; }

        public string eposta { get; set; }

        public string YanıtMesajı { get; set; }


        public Destek()
        {

        }


    }
}
