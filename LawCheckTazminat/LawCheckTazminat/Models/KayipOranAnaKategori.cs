using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Models
{
   public class KayipOranAnaKategori
    {

        public List<AnaKategori> Liste { get; set; }

        public KayipOranAnaKategori()
        {

            Liste = new List<AnaKategori>();

            Liste.Add
           (new AnaKategori
           {
               Id = "1",
               Kategori= "Baş Arızaları ",
               Aciklama= "(Kafa kemikleri, nöroloji, nöroşirürji, psikiyatri arıza ve hastalıkları)"
           }) ;
            Liste.Add
       (new AnaKategori
       {
           Id = "2",
           Kategori = "Göz   Arızaları  ",
           Aciklama = ""
       }) ;
            Liste.Add
       (new AnaKategori
       {
           Id = "3",
           Kategori = "Kulak   Arızaları  ",
           Aciklama=""
       });
            Liste.Add
       (new AnaKategori
       {
           Id = "4",
           Kategori = "Yüz   Arızaları ",
           Aciklama = ""
       }) ;
            Liste.Add
       (new AnaKategori
       {
           Id = "5",
           Kategori = "Boyun   Arızaları  ",
           Aciklama=""
       });

            Liste.Add
     (new AnaKategori
     {
         Id = "6",
         Kategori = "Göğüs hastalıkları",
         Aciklama = ""
     });


            Liste.Add
        (new AnaKategori
        {
             Id = "7",
             Kategori = "Omuz  ve   Kol  Arızaları",
            Aciklama = ""
        });
          
            Liste.Add
      (new AnaKategori
        {
            Id = "8",
            Kategori = "El   Bileği  ve El Arızaları",
            Aciklama = ""
         });

            Liste.Add
    (new AnaKategori
        {
            Id = "9",
           Kategori = "El Parmakları Arızaları",
            Aciklama = ""
        });
            
           Liste.Add
    (new AnaKategori
    {
        Id = "10",
        Kategori = "Omurga Arızaları",
        Aciklama = ""
    });

            Liste.Add
    (new AnaKategori
    {
        Id = "11",
        Kategori = "Karın Hastalıkları ve Arızaları   ",
        Aciklama = ""
    });
       
            Liste.Add
    (new AnaKategori
    {
        Id = "12",
        Kategori = "Pelvis ve Alt Ekstremite Arızaları",
        Aciklama = ""
       });
         
           Liste.Add
     (new AnaKategori
        {
            Id = "13",
            Kategori = "Endokrin, Metabolizma, Kollagen Doku, Periferik Damar Hastalıkları, Hematolojik ve Romotoid Hastalıkları",
            Aciklama = ""
        });

         Liste.Add
    (new AnaKategori
        {
            Id = "14",
            Kategori = "Deri Arızaları ve Yanıklar",
            Aciklama = ""
        });


        }

    }

    public class AnaKategori
    {
        public string Id { get; set; }
        public string Kategori { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }
    }

    public class KayipOranAltKategori1
    {
        public List<AltKategori1> ListeAltKategori1 { get; set; }

        public KayipOranAltKategori1()
        {
            ListeAltKategori1 = new List<AltKategori1>();

            ListeAltKategori1.Add
          (new AltKategori1
          {
              Id = "1-1",
              AnaKategoriId="1",
              Kategori = "Trepenasyon veya travma sonucu baş kemiklerinin açıklıkları ",
              Aciklama = ""
          });

            ListeAltKategori1.Add
       (new AltKategori1
       {
           Id = "1-2",
           AnaKategoriId = "1",
           Kategori = "Epilepsiler",
           Aciklama = ""
       });

            ListeAltKategori1.Add
     (new AltKategori1
     {
         Id = "1-3",
         AnaKategoriId = "1",
         Kategori = "Konuşma bozuklukları",
         Aciklama = ""
     });
          
            ListeAltKategori1.Add
  (new AltKategori1
  {
      Id = "1-4",
      AnaKategoriId = "1",
      Kategori = "Vasküler, enfeksiyon, entoksikasyon veya travmaya bağlı denge bozuklukları, vertigo",
      Aciklama = ""
  });

            ListeAltKategori1.Add
    (new AltKategori1
    {
        Id = "1-5",
        AnaKategoriId = "1",
        Kategori = "Ekstra pramidal sistem hastalıkları",
        Aciklama = ""
    });
      
            ListeAltKategori1.Add
        (new AltKategori1
        {
            Id = "1-6",
            AnaKategoriId = "1",
            Kategori = "Serebro vasküler hastalıklar",
            Aciklama = ""
        });
            ListeAltKategori1.Add
  (new AltKategori1
  {
      Id = "1-7",
      AnaKategoriId = "1",
      Kategori = "Vasküler, enfeksiyon, tümör ve travmaya bağlı paraparaziler",
      Aciklama = ""
  });

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-8",
AnaKategoriId = "1",
Kategori = "Vasküler, enfeksiyon, tümör ve travmaya bağlı paraparaziler, nedeniyle meydana gelen dipleji, parapleji, ve kuadropleji",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-9",
AnaKategoriId = "1",
Kategori = "Menenjit, ensefalit, menengoensefalit gibi beyin zarları ve beyin dokusu enfeksiyonu sekelleri.",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "1-10",
    AnaKategoriId = "1",
    Kategori = "Herediter, heredo-familyal dejeneratif ve henüz sebebi bilinmeyen hastalıklar.",
    Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-11",
AnaKategoriId = "1",
Kategori = "Boyun sapının ( pedunkulus, pons, bulbus ) travmatik, vasküler, enfeksiyoz, toksik vb. lezyonları sonucu oluşan kafa çifti paralizileri )",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-12",
AnaKategoriId = "1",
Kategori = "Kafa travmaları ( kommosyo, kontizyo serebri ), kafa kemikleri fissuru, çökme kırığı sonucu oluşan psişik bozukluklar ( vertigo, amnezi, semptomatik epilepsi vb. )",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-13",
AnaKategoriId = "1",
Kategori = "Travmatik, enfeksiyoz ( apse ), vasküler nedenlere, tümörlere bağlı beyin arızalarıyla bunların ameliyatları sonucu meydana gelen sekeller, ameliyat edilemeyen beyin tümörleri",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-14",
AnaKategoriId = "1",
Kategori = "Poliradikülönevrit, radikülit, polinevrit ve polinöropatiler ",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-15",
AnaKategoriId = "1",
Kategori = "Psikozlar ",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-16",
AnaKategoriId = "1",
Kategori = "Nevrotik bozukluklar, kişilik bozuklukları ve başka psikotik olmayan ruhsal bozukluklar ",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-17",
AnaKategoriId = "1",
Kategori = "Alkol ve ilaç ( morfin, esrar, kokain, barbütrik, amfetamin ) bağımlılığı  ",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-18",
AnaKategoriId = "1",
Kategori = "Organik beyin zedelenmesinden sonra oluşan psikotik olmayan özgül bozukluklar ",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "1-19",
    AnaKategoriId = "1",
    Kategori = "Oligofreniler ",
    Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "1-20",
AnaKategoriId = "1",
Kategori = "Travma veya travma dışı cinsel sapma ve bozukluklar ",
Aciklama = ""
});


            //----------------------------------------------
            // ANA KATEGORİ 2-
            //--------------------------------------------------

            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-1",
    AnaKategoriId = "2",
    Kategori = "Bir gözün  1.0 ,  diğerinin 0  görmesi",
    Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-2",
AnaKategoriId = "2",
Kategori = "Bir gözün 1.0 , diğerinin  0.1 görmesi",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-3",
    AnaKategoriId = "2",
    Kategori = "Bir gözün  1.0 ,  diğerinin  0.2   görmesi",
    Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-4",
AnaKategoriId = "2",
Kategori = "Bir gözün  1.0 ,  diğerinin  0.3   görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-5",
AnaKategoriId = "2",
Kategori = "Bir gözün  1.0 ,  diğerinin  0.4   görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-6",
    AnaKategoriId = "2",
    Kategori = "Bir gözün  1.0 ,  diğerinin  0.5   görmesi",
    Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-7",
    AnaKategoriId = "2",
    Kategori = "Bir gözün  1.0 ,  diğerinin  0.6   görmesi",
    Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-8",
AnaKategoriId = "2",
Kategori = "Bir gözün  1.0 ,  diğerinin  0.7   görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-9",
AnaKategoriId = "2",
Kategori = "Bir gözün  1.0 ,  diğerinin  0.8   görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-10",
AnaKategoriId = "2",
Kategori = "Bir gözün 1.0 ,  diğerinin  0.9  görmesi",
Aciklama = ""
});

    ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-11",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-12",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.1 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-13",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.2 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-14",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.3 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-15",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.4 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-16",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.5 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-17",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.6 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-18",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.7 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-19",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.8 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-20",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.9 , diğerinin  0.9 görmesi",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-21",
    AnaKategoriId = "2",
    Kategori = "Bir gözün  0.8, diğerinin  0 görmesi",
    Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-22",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.1 görmesi",
Aciklama = ""
});

            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-23",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.2 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-24",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.3 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-25",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.4 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-26",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.5 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-27",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.6 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-28",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.7 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-29",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.8 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-30",
AnaKategoriId = "2",
Kategori = "Bir gözün  0.8, diğerinin  0.9 görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-31",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-32",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.1  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-33",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.2  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-34",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.3  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-35",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.4  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-36",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.5  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
Id = "2-37",
AnaKategoriId = "2",
Kategori = "Bir gözün 0.7,  diğerinin  0.6  görmesi",
Aciklama = ""
});
            ListeAltKategori1.Add
(new AltKategori1
{
    Id = "2-38",
    AnaKategoriId = "2",
    Kategori = "Bir gözün 0.7,  diğerinin  0.7  görmesi",
    Aciklama = ""
});

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-39",
                AnaKategoriId = "2",
                Kategori = "Bir gözün 0.6, diğerinin 0  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-40",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.6,  diğerinin  0.1  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-41",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.6,  diğerinin  0.2  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-42",
                AnaKategoriId = "2",
                Kategori = "Bir gözün 0.6, diğerinin  0.3 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-43",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.6,  diğerinin  0.4  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-44",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.6, diğerinin  0.5 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-45",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.6, diğerinin 0.6 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-46",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5, diğerinin 0  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-47",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5, diğerinin  0.1 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-48",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5, diğerinin  0.2   görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-49",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5, diğerinin  0.3 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-50",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5, diğerinin  0.4 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-51",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.5,  diğerinin  0.5  görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-52",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.4, diğerinin  0 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-53",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.4, diğerinin  0.1 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-54",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.4, diğerinin  0.2 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-55",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.4, diğerinin  0.3 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-56",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.4, diğerinin  0.4 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-57",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.3, diğerinin  0 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-58",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.3, diğerinin  0.1 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-59",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.3, diğerinin  0.2 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-60",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.3, diğerinin  0.3 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-61",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.2, diğerinin  0 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-62",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.2, diğerinin  0.1 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-63",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.2, diğerinin  0.2 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-64",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.1, diğerinin  0 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-65",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0.1, diğerinin  0.1 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-66",
                AnaKategoriId = "2",
                Kategori = "Bir gözün  0, diğerinin  0 görmesi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-67",
                AnaKategoriId = "2",
                Kategori = "Travma ve çeşitli hastalıklara bağlı, sekel olarak kalan ve görüşü bozan retina kanamaları, iltihaplar",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-68",
                AnaKategoriId = "2",
                Kategori = "Çift görmeye neden olan her iki göz kaslarının sekel halindeki paralizisi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-69",
                AnaKategoriId = "2",
                Kategori = "Bir gözün hareketsizliğine ve çift görmesine neden olan göz kaslarının sekel halindeki paralizisi",
                Aciklama = ""
            });


            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-70",
                AnaKategoriId = "2",
                Kategori = "Göz yuvarlarını hareketsiz kılan ve görüş derecesini bozan, sekel bırakan orbita yaralanmaları, tümörleri ve iltihapları",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-71",
                AnaKategoriId = "2",
                Kategori = "Sekel halinde lagoftalmi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-72",
                AnaKategoriId = "2",
                Kategori = "Optik sinir hastalıkları iltihapları, tümörleri, vasküler göz hastalıkları",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-73",
                AnaKategoriId = "2",
                Kategori = "Sekel halindeki hemianopsiler ( bitemporal, binazal vb. )",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-74",
                AnaKategoriId = "2",
                Kategori = "Sekel halindeki ektropiyon veya entropiyon",
                Aciklama = ""
            });
              ListeAltKategori1.Add(new AltKategori1
                      {
                          Id = "2-75",
                          AnaKategoriId = "2",
                          Kategori = "Göz kanalının tıkanması ve kese iltihapları",
                          Aciklama = ""
                      });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-76",
                AnaKategoriId = "2",
                Kategori = "Sekel halinde tam pitozis",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-76b",
                AnaKategoriId = "2",
                Kategori = "Sekel halinde yarım pitozis",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-77",
                AnaKategoriId = "2",
                Kategori = "Şaşılık ( Nistagmus )",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "2-78",
                AnaKategoriId = "2",
                Kategori = "Periferik görme alanının azalması",
                Aciklama = ""
            });



            // İşitme....

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-1",
                AnaKategoriId = "3",
                Kategori = "Her iki kulakta tedavi ve işitme cihazıyla giderilemeyen tam işitme kaybı",
                Aciklama = ""
            });
            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-2",
                AnaKategoriId = "3",
                Kategori = "Bir  kulakta tedavi ve işitme cihazıyla giderilemeyen tam işitme kaybı",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-3",
                AnaKategoriId = "3",
                Kategori = "Doğuştan ( konjenital ) sağır ve dilsiz",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-4",
                AnaKategoriId = "3",
                Kategori = "Odiometrik incelemeyle teşhis edilen iletim, sensorinöral ve mikst tip işitme kayıpları",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-5",
                AnaKategoriId = "3",
                Kategori = "Plastikle düzeltilemeyen tek taraflı sayvan yokluğu",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-6",
                AnaKategoriId = "3",
                Kategori = "Plastikle düzeltilemeyen iki taraflı sayvan yokluğu",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-7",
                AnaKategoriId = "3",
                Kategori = "Kronik orta kulak iltihabı",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-8",
                AnaKategoriId = "3",
                Kategori = "Kulaklarda akıntılı, labirent komplikasyonu sonucu sürekli baş dönmeleri ve denge bozukluklarıyla birlikte iki taraflı tam işitme kaybı",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-9",
                AnaKategoriId = "3",
                Kategori = "Dış kulak yolunun ve orta kulağın kötü tabiatlı tümörleri, ameliyat edilemeyen akustik nörinoma",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "3-10",
                AnaKategoriId = "3",
                Kategori = "Psikoz oluşturacak ağır kulak çınlaması ( tinnitus )",
                Aciklama = ""
            });



            //****************************************************************************************
            // YÜZ ARIZALARI

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-1",
                AnaKategoriId = "4",
                Kategori = "Çiğneme ve konuşmayı ileri derecede güçleştiren alt ve üst çene kırıkları sekeli",
                Aciklama = ""
            });
            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-2",
                AnaKategoriId = "4",
                Kategori = "Dil yokluğu veya konuşma ve yutmayı zorlaştıracak derecede dil harabiyeti",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-3",
                AnaKategoriId = "4",
                Kategori = "Tükürük bezleri kanallarının yaralanması veya hastalıkları sonucu sürekli ve tedavi edilemeyen fistül",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-4",
                AnaKategoriId = "4",
                Kategori = "Periferik fasial sinir paralizisi",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-5",
                AnaKategoriId = "4",
                Kategori = "Burun kaybı",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-6",
                AnaKategoriId = "4",
                Kategori = "Koklama ve tatma duygularının azalması",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-7",
                AnaKategoriId = "4",
                Kategori = "Burunun kemik ve kıkırdak kısımlarının nefes almayı ileri derecede güçleştiren harabiyetleri",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-8",
                AnaKategoriId = "4",
                Kategori = "Protezle düzeltilemeyen damak defektleri",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-9",
                AnaKategoriId = "4",
                Kategori = " Septum daviasyonu",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-10",
                AnaKategoriId = "4",
                Kategori = " Primer atrofik rinit(ozena)",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-11",
                AnaKategoriId = "4",
                Kategori = "Ameliyat edilemeyen burun, paranazal sinüsler, çene, ağız ve farenks tümörleri",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-12",
                AnaKategoriId = "4",
                Kategori = "Bütün dişlerin kaybı ( protez olanaksız )",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "4-13",
                AnaKategoriId = "4",
                Kategori = "Tam fonksiyon bozukluğu yapan dudak arızaları",
                Aciklama = ""
            });

            //************************************************************
            // 5- BOYUN ARIZALARI

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "5-1",
                AnaKategoriId = "5",
                Kategori = " Larenks yokluğu sonucu  sürekli kanül takılmasını gerektiren hastalık ve arızalar",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "5-2",
                AnaKategoriId = "5",
                Kategori = " Ses organlarının tedavi edilemeyen arızaları",
                Aciklama = ""
            });

            //*******************************
            // 6- GÖĞÜS HASTALIKLARI

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "6-1",
                AnaKategoriId = "6",
                Kategori = " Klinik ve radyolojik bulgularla belirlenmiş, solunum ve dolaşım fonksiyonlarını etkileyen akciğer dokusunun," +
                " bronşların, plevranın, kemik kafesinin ( toraks ) hastalıkları, kaburgaların ( kot ) kırılma ve rezeksiyonu, akciğer fıtığı ve " +
                "bunların sekelleri",
                Aciklama = ""
            });

            ListeAltKategori1.Add(new AltKategori1
            {
                Id = "6-2",
                AnaKategoriId = "6",
                Kategori = " Solunum organlarının, ( larenks, akciğer, plevra ) işyeri koşullarına ve göreve bağlı olan veya " +
                "olmayan nedenlerle ortaya çıkan, uyumu bozan ve görevin yapılmasına engel olan ve tedaviyle giderilmesi " +
                "umulmayan ağır tüberkülozları veya buna bağlı sekeller",
                Aciklama = ""
            });

        }

    }

    public class KayipOranAltKategori2
    {
        public List<AltKategori2> ListeAltKategori2 { get; set; }

        public KayipOranAltKategori2()
        {
            ListeAltKategori2 = new List<AltKategori2>();

            ListeAltKategori2.Add
     (new AltKategori2
     {
         Id = "1-1-1",
         AnaKategoriId = "1",
         AltKategori1Id="1-1",
         Kategori = "Txxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
         Aciklama = ""
     });
        }
    }

    public class KayipOranAltKategori3
    {
        public List<AltKategori3> ListeAltKategori3 { get; set; }
       
        public KayipOranAltKategori3()
        {
            ListeAltKategori3 = new List<AltKategori3>();
            ListeAltKategori3.Add
                (new AltKategori3
                {
                    Id = "1-1-1-1",
                    AnaKategoriId="1",AltKategori1Id="1-1", AltKategori2Id="1-1-1",
                    Kategori="AAAAAAAaaaaaaaaaaa",
                    Aciklama=""
                }
                ); 
        }

    }

    public class AltKategori1
    {
        public string Id { get; set; }
        public string AnaKategoriId { get; set; }
        public string Kategori { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }
    }

    public class AltKategori2
    {
        public string Id { get; set; }
        public string AnaKategoriId { get; set; }
        public string AltKategori1Id { get; set; }
        public string Kategori { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }
    }

    public class AltKategori3
    {
        public string Id { get; set; }
        public string AnaKategoriId { get; set; }
        public string AltKategori1Id { get; set; }
        public string AltKategori2Id { get; set; }

        public string Kategori { get; set; }
        public string Aciklama { get; set; }
        public string Ek { get; set; }


    }
}
