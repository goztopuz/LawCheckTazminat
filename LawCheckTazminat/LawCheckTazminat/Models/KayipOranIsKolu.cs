using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
   public class KayipOranIsKolu
    {
        public List<AnaIskolu> Liste { get; set; }

        public KayipOranIsKolu()
        {
            Liste = new List<AnaIskolu>();

            Liste.Add(new AnaIskolu
            {
                Id = "1",
                Meslek = "AĞAÇ SANAYİİ VE ORMAN İŞLETMESİ İŞKOLLARI"
            }) ;


            Liste.Add(new AnaIskolu
            {
                Id = "2",
                Meslek = "AĞIR SANAYİ İŞ KOLLARI "

            }) ;

            Liste.Add(new AnaIskolu
            {
                Id="3",
                Meslek= "AŞCILIK - MUHALLEBİCİLİK,  FIRINCILIK, PASTACILIK, ŞEKERLEMECİLİK  VE  BENZERİ  İŞ  KOLLARI"

            });

            Liste.Add(new AnaIskolu
            {
                Id="4",
                Meslek= "ALKOLLÜ VE ALKOLSÜZ İÇKİLER SANAYİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "5",
                Meslek = " BİTKİSEL  YAĞ  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "6",
                Meslek = "ÇAY  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "7", 
                Meslek = "ÇİMENTO  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "8",
                Meslek = "DERİCİLİK  VE  DERİ  MAMÜLLERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "9",
                Meslek = "LEKTRİK  HAVAGAZI  ENERJİ  MERKEZLERİ  VE SU  TESİSLERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "10",
                Meslek = " ET,  BALIK  VE  KONSERVE  SANAYİİ İŞKOLLARI ( Mezbahalar,  balıkhaneler, et, sebze,  meyve  konserveciliği ve benzeri işyerleri )"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "11",
                Meslek = "GENEL  HİZMETLER  İŞKOLLARI ( Lokanta,  otel,  han,  kahvehane,  hamam,  berber,  salon  ve benzerleri )"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "12",
                Meslek = "GİYİM EŞYASI DİKİM - TEMİZLEME - YIKAMA  VE BOYAMA  YERLERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "13",
                Meslek = "İNŞAAT  İŞLERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "14",
                Meslek = "KAĞIT SELLÜLOZ SANAYİİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "15",
                Meslek = "KAUÇUK  VE KAUÇUKTAN  YAPILAN  EŞYA  SANAYİİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "16",
                Meslek = "KİMYA VE İLAÇ SANAYİ  İŞ KOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "17",
                Meslek = "KUYUMCULUK,  OYMACILIK,  AYNACILIK,  VE BENZERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "18",
                Meslek = "MADEN  ÇIKARTMA,  ERİTME,  TEMİZLEME,  VE BUNLARLA  İLGİLİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "19",
                Meslek = "METAL  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "20",
                Meslek = "ONARIM  İŞ  KOLLARI ( Makine, alet, edavat, cihaz ve benzerleri )"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "21",
                Meslek = "PALAMUT  SANAYİİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "22",
                Meslek = "PLASTİK,  YAPAY,  İPEK  VE  BENZERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "23",
                Meslek = "PORSELEN,  ÇİNİ  VE  TOPRAK  SANAYİİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "24",
                Meslek = "SABUN  SANAYİİ  İŞKOLLARI ( Sabun, deterjan, temizleme tozları, kozmetik ve benzerleri )"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "25",
                Meslek = "SİNEMA, FOTOĞRAF, GAZETE, BASIMEVİ, KİTAPCILIK, OYUNCAK VE KIRTASİYECİLİK İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "26",
                Meslek = "SÜT,  TERYAĞI,  PEYNİR  VE YOĞURT SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "27",
                Meslek = "ŞEKER  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "28",
                Meslek = "ŞİŞE VE CAM SANAYİİ İŞKOLLARI "
            });


            Liste.Add(new AnaIskolu
            {
                Id = "29",
                Meslek = "TEKSTİL SANAYİİ İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "30",
                Meslek = "TRİKOTAJ,  HALI,  YORGAN  VE BENZERİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "31",
                Meslek = "TÜTÜN  VE  SİGARA  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "32",
                Meslek = "ULAŞTIRMA  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "33",
                Meslek = "ÜZÜM  VE  İNCİR  SANAYİİ  İŞKOLLARI"
            });

            Liste.Add(new AnaIskolu
            {
                Id = "34",
                Meslek = "İŞKOLLARI LİSTELERİ DIŞINDA KALAN MESLEKLER VE İŞLER "
            });





        }

    }


    public class KayipOranAltIsKolu1
    {
        public List<IsKoluAltDetay1> Liste { get; set; }

        public KayipOranAltIsKolu1()
        {
            Liste = new List<IsKoluAltDetay1>();
        }
    }

    public class KayipOranAltIsKolu2
    {
        public List<IsKoluAltDetay2> Liste { get; set; }

        public KayipOranAltIsKolu2()
        {
            Liste = new List<IsKoluAltDetay2>();
        }

    }


    public class AnaIskolu
    {
        public string Id { get; set; }
        public string Meslek { get; set; }
        public string Ek1 { get; set; }
        public string Ek2 { get; set; }

    }
    public class IsKoluAltDetay1
    {
        public string Id { get; set; }
        public string AnaIsKolu { get; set; }
        public string Meslek { get; set; }
        public string Ek1 { get; set; }
        public string Ek2 { get; set; }
    }
    public class IsKoluAltDetay2
    {
        public string Id { get; set; }
        public string AnaIsKolu { get; set; }
        public string AltDetay1 { get; set; }
        public string Meslek { get; set; }
        public string Ek1 { get; set; }
        public string Ek2 { get; set; }

    }
}

