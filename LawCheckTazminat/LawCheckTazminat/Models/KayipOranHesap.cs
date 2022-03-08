using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
   public class KayipOranHesap
    {
        public string Id { get; set; }
        public string KisiId { get; set; }

        public double KisiYas { get; set; }
        public string Cinsiyet { get; set; }

        public string AnaKategoriId { get; set; }
        public string AnaKategoriAciklama { get; set; }

        public string AltKategori1Id { get; set; }
        public string AltKategori1Aciklama { get; set; }

        public string AltKategori2Id { get; set; }
        public string AltKategori2Aciklama { get; set; }

        public string AltKategori3Id { get; set; }
        public string AltKategori3Aciklama { get; set; }


        public string IsKoluAnaId { get; set; }
        public string IsKoluAnaAciklama { get; set; }

        public string IsKoluAltDetay1Id { get; set; }
        public string IsKoluAltDetay1Aciklama { get; set; }

        public string IsKoluAltDetay2Id { get; set; }
        public string IsKoluAltDetay2Aciklama { get; set; }

        public int KazaTarihindekiYasi { get; set; }

        public string SimgeTabloC { get; set; }

        public double Yas3839OranTabloD { get; set; }

        public double HesaplananOranTabloE { get; set; }


    }
}
