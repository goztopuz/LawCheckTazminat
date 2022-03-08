
using LawCheckTazminat.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using LawCheckTazminat.Models;

namespace LawCheckTazminat.Models
{
    public  class Repo  : DbContext
    {
        string _dbPath;
        public Repo(string dbPath) : base()
        {
            _dbPath = dbPath;

            Database.EnsureCreated();

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<DestektenYoksunluk> DestektenYoksunluks { get; set; }

        public DbSet<Masraf> Masrafs { get; set; }
        public DbSet<Yakin> Yakins { get; set; }
        public DbSet<Maas> MaasS { get; set; }

        public DbSet<AsgariUcretTablosu> AsgariUcretTablosus { get; set; }


        // İş Gücü Kayip
        public DbSet<IsgucuKayip> IsgucuKayips { get; set; }
        public DbSet<YakinIsgucu> YakinIsgucus { get; set; }
        public DbSet<MaasIsGucu> MaasIsGucus { get; set; }
        public DbSet<MasrafIsgucu> MasrafIsgucus { get; set; }

        public DbSet<KayipOran> KayipOrans { get; set; }

        // ResmiTatil Kayıt
        public DbSet<ResmiTatiller> ResmiTatillers { get; set; }


        // Fazla Mesai Veritabanı Tanımları
        public DbSet<FazlaMesai> fazlaMesais { get; set; }
        public DbSet<FazlaMesaiHaftalar> fazlaMesaiHaftalars { get; set; }
        public DbSet<FazlaMesaiResmiTatil> fazlaMesaiResmiTatils { get; set; }
        public DbSet<MaasFazlaMesai> maasFazlaMesais { get; set; }

        public DbSet<FazlaMesaiKisiIzinKayitlari> fazlaMesaiKisiIzinKayitlaris { get; set; }
        public DbSet<FazlaMesaiKisiIzinGunleri> fazlaMesaiKisiIzinGunleris { get; set; }
        public DbSet<FazlaMesaiHaftalikIzinHaftalar> fazlaMesaiHaftalikIzinHaftalars { get; set; }



        // Gece Calisma Vertibanı Tanımları
        public DbSet<GeceCalisma> geceCalismas { get; set; }
        
        public DbSet<GeceMesaiHaftalar> geceMesaiHaftalars { get; set; }
        public DbSet<GeceMesaiKisiIzinKayitlari> geceMesaiKisiIzinKayitlaris { get; set; }
        public DbSet<MaasGeceMesai> maasGeceMesais { get; set; }
        public DbSet<HesaptanDusecekTarihler> geceDusecekTarihler { get; set; }


        // Yıllık İzin
        public DbSet<YillikIzin> yillikIzins{ get; set; }
        public DbSet<YillikIzinIzinGunleri> YillikIzinIzinGunleris { get; set; }
        public DbSet<YillikIzinIzinKayitlari> YillikIzinIzinKayitlaris { get; set; }

        public DbSet<YYResmiTatil> yYResmiTatils { get; set; }
        public DbSet<YYHaftaIzni> yYHaftaIznis { get; set; }
        public DbSet<YYYilBilgi> yYYCalisilan { get; set; }
        public DbSet<YYYilBilgi2> yYYHesaplanan { get; set; }

         //Kıdem Ihbar
         public DbSet<KidemIhbar> kidemIhbars { get; set; }

        // Kıdem Tavanları

        public DbSet<KidemTavan> kidemTavans { get; set; }


        //Destek Talep İşlemleri

        public DbSet<DestekTalep> destekTaleps { get; set; }


        //Kasa Gelir
        public DbSet<Gelir> gelirs { get; set; }
        public DbSet<KismiGelir> kismiGelirs { get; set; }
        public DbSet<GelirTur> gelirTurs { get; set; }

        //Kasa Gider
        public DbSet<Gider> giders { get; set; }
        public DbSet<KismiGider> kismiGiders { get; set; }
        public DbSet<GiderTur> giderTurs { get; set; }



        // Tanım İşlemleri Servisi

        public DbSet<VergiDilimleri> vergiDilimleris { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DestektenYoksunluk>()
                .HasMany(d => d.DestekYoksunlukYakinlar)
                .WithOne(y => y.DestektenYoksunluk);

            modelBuilder.Entity<DestektenYoksunluk>()
           .HasMany(d => d.DosyaDestektenYoksunlukMasraf)
           .WithOne(y => y.DestektenYoksunluk);

            modelBuilder.Entity<DestektenYoksunluk>()
           .HasMany(d => d.DestektekYoksunlukMaaslar)
           .WithOne(y => y.DestektenYoksunluk);


            // Fazla Mesai.
            modelBuilder.Entity<FazlaMesai>().
                HasKey(p => p.Id);
            modelBuilder.Entity<FazlaMesaiHaftalar>().
                HasKey(p => p.Id);
            modelBuilder.Entity<FazlaMesaiResmiTatil>().
                HasKey(p => p.Id);
            modelBuilder.Entity<MaasFazlaMesai>().
                HasKey(p => p.Id);

            modelBuilder.Entity<FazlaMesaiKisiIzinKayitlari>().
                HasKey(p => p.Id);
            modelBuilder.Entity<FazlaMesaiKisiIzinGunleri>().HasKey(p => p.Id);
            modelBuilder.Entity<FazlaMesaiHaftalikIzinHaftalar>().HasKey(p => p.Id);

            modelBuilder.Entity<FazlaMesai>()
                .HasMany(d => d.FMHaftalarBilgi)
                .WithOne(y => y.fazlaMesai);

            modelBuilder.Entity<FazlaMesai>()
                 .HasMany(d => d.MaasBilgi)
                 .WithOne(y => y.fazlaMesai);

            modelBuilder.Entity<FazlaMesai>()
                .HasMany(d => d.ResmiTatilBilgi)
                .WithOne(y => y.fazlaMesai);

            modelBuilder.Entity<FazlaMesai>()
                .HasMany(d => d.IzinKaytilariBilgi)
                .WithOne(y => y.fazlaMesai)
                .HasForeignKey(f=> f.FazlaMesaiId);

            modelBuilder.Entity<FazlaMesai>()
                .HasMany(d => d.IzinGunleriBilgi)
                .WithOne(y => y.fazlaMesai);

            modelBuilder.Entity<FazlaMesai>()
                .HasMany(d => d.HaftalikIzinHaftalarBilgi)
                .WithOne(y => y.fazlaMesai);



            // Gece Mesai....
            modelBuilder.Entity<GeceCalisma>().HasKey(p => p.Id);

            modelBuilder.Entity<GeceMesaiHaftalar>().HasKey(p => p.Id);
            modelBuilder.Entity<GeceMesaiKisiIzinKayitlari>().HasKey(p => p.Id);
            modelBuilder.Entity<MaasGeceMesai>().HasKey(p => p.Id);
            modelBuilder.Entity<HesaptanDusecekTarihler>().HasKey(p => p.Id);

            modelBuilder.Entity<GeceCalisma>().HasMany(d => d.MaasBilgi).WithOne(y => y.geceCalisma);
            modelBuilder.Entity<GeceCalisma>().HasMany(d => d.IzinKaytilariBilgi).WithOne(y => y.geceCalisma);
            modelBuilder.Entity<GeceCalisma>().HasMany(d => d.HaftalarBilgi).WithOne(y => y.geceCalisma);
            modelBuilder.Entity<GeceCalisma>().HasMany(d => d.GeceDusecekTarihler).WithOne(y => y.geceCalisma);


            // DestekTalep
            modelBuilder.Entity<DestekTalep>().HasKey(p => p.Id);

            //-------
            // Yıllık İzin
            modelBuilder.Entity<YillikIzin>().HasKey(p => p.Id);
            modelBuilder.Entity<YillikIzinIzinGunleri>().HasKey(p => p.Id);
            modelBuilder.Entity<YillikIzinIzinKayitlari>().HasKey(p => p.Id);
            modelBuilder.Entity<YYResmiTatil>().HasKey(p => p.Id);
            modelBuilder.Entity<YYHaftaIzni>().HasKey(p => p.Id);
            modelBuilder.Entity<YYYilBilgi>().HasKey(p => p.Id);
            modelBuilder.Entity<YYYilBilgi2>().HasKey(p => p.Id);


            modelBuilder.Entity<YillikIzin>().HasMany(d => d.IzinGunleriBilgi).WithOne(y => y.yillikIzin);
            modelBuilder.Entity<YillikIzin>().HasMany(d => d.IzinKaytilariBilgi).WithOne(y => y.yillikIzin);

            //--------------------
            //KidemIhbar
            modelBuilder.Entity<KidemIhbar>().HasKey(p => p.Id);


            //Kasa Gelir- Gider
            modelBuilder.Entity<Gelir>().HasKey(p => p.Id);
            modelBuilder.Entity<GelirTur>().HasKey(p => p.Id);
            modelBuilder.Entity<KismiGelir>().HasKey(p => p.Id);

            modelBuilder.Entity<Gider>().HasKey(p => p.Id);
            modelBuilder.Entity<GiderTur>().HasKey(p => p.Id);
            modelBuilder.Entity<KismiGider>().HasKey(p => p.Id);






            //-----------
            // İş Gücü Kayip

            modelBuilder.Entity<IsgucuKayip>()
                 .HasKey(p => p.Id);

            modelBuilder.Entity<IsgucuKayip>()
                .HasMany(d => d.IsGucuKayipYakinlar)
                .WithOne(y => y.IsgucuKayip);

            modelBuilder.Entity<IsgucuKayip>()
                .HasMany(d => d.IsGucuKayipOranlar)
                .WithOne(y => y.IsgucuKayip);

            modelBuilder.Entity<IsgucuKayip>()
                .HasMany(d => d.IsGucuKayipMasraflar)
                .WithOne(y => y.IsgucuKayip);

            modelBuilder.Entity<IsgucuKayip>()
                .HasMany(d => d.IsGucuKayipMaaslar)
                .WithOne(y => y.IsgucuKayip);

            modelBuilder.Entity<YakinIsgucu>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<MasrafIsgucu>()
                .HasKey(p => p.Id);
            
            modelBuilder.Entity<MaasIsGucu>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<KayipOran>()
                .HasKey(p => p.id);
            //------------

            // Destekten Yoksunluk
            modelBuilder.Entity<DestektenYoksunluk>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<Masraf>()
                 .HasKey(p => p.Id);

            modelBuilder.Entity<Maas>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Yakin>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<AsgariUcretTablosu>()
                .HasKey(p => p.Id);


            modelBuilder.Entity<Contact>()
                .HasData(new Contact { Id = "11111", Ad = "Seçilmemiş Kayıt ", Soyad = "(TEST)" });

            modelBuilder.Entity<AsgariUcretTablosu>()
                .HasData(

                 new AsgariUcretTablosu { Id = "2022-2", yil = "2022-2", brut = Convert.ToDecimal(5004), net = Convert.ToDecimal(4253), yil2 = 2022, donem = 2 },
                 new AsgariUcretTablosu { Id = "2022-1", yil = "2022-1", brut = Convert.ToDecimal(5004), net = Convert.ToDecimal(4253), yil2 = 2022, donem = 1 },

                 new AsgariUcretTablosu { Id = "2021-2", yil = "2021-2", brut = Convert.ToDecimal(3577.5), net = Convert.ToDecimal(2825.90), yil2 = 2021, donem = 2 },
                 new AsgariUcretTablosu { Id = "2021-1", yil = "2021-1", brut = Convert.ToDecimal(3577.5), net = Convert.ToDecimal(2825.90), yil2 = 2021, donem = 1 },

                 new AsgariUcretTablosu { Id = "2020-2", yil = "2020-2", brut = Convert.ToDecimal(2943), net = Convert.ToDecimal(2324.7), yil2 = 2020, donem = 2 },

                 new AsgariUcretTablosu { Id = "2020-1", yil = "2020-1", brut = Convert.ToDecimal(2943), net = Convert.ToDecimal(2324.7), yil2 = 2020, donem = 1 },
                 
                 new AsgariUcretTablosu { Id = "2019-2", yil = "2019-2", brut = Convert.ToDecimal(2558.40), net = Convert.ToDecimal(2020.90), yil2 = 2019, donem = 2 },
                 new AsgariUcretTablosu { Id = "2019-1", yil = "2019-1", brut = Convert.ToDecimal(2558.40), net = Convert.ToDecimal(2020.90), yil2 = 2019, donem = 1 },

                 new AsgariUcretTablosu { Id = "2018-2", yil = "2018-2", brut = Convert.ToDecimal(2029.50), net = Convert.ToDecimal(1603.12), yil2 = 2018, donem = 2 },
                 new AsgariUcretTablosu { Id = "2018-1", yil = "2018-1", brut = Convert.ToDecimal(2029.50), net = Convert.ToDecimal(1603.12), yil2 = 2018, donem = 1 },

                 new AsgariUcretTablosu { Id = "2017-2", yil = "2017-2", brut = Convert.ToDecimal(1777.50), net = Convert.ToDecimal(1404.06), yil2 = 2017, donem = 2 },
                 new AsgariUcretTablosu { Id = "2017-1", yil = "2017-1", brut = Convert.ToDecimal(1777.50), net = Convert.ToDecimal(1404.06), yil2 = 2017, donem = 1 },

                 new AsgariUcretTablosu { Id = "2016-2", yil = "2016-2", brut = Convert.ToDecimal(1647.00), net = Convert.ToDecimal(1300.99), yil2 = 2016, donem = 2 },
                 new AsgariUcretTablosu { Id = "2016-1", yil = "2016-1", brut = Convert.ToDecimal(1647.00), net = Convert.ToDecimal(1300.99), yil2 = 2016, donem = 1 },

                 new AsgariUcretTablosu { Id = "2015-2", yil = "2015-2", brut = Convert.ToDecimal(1273.50), net = Convert.ToDecimal(1000.55), yil2 = 2015, donem = 2 },
                 new AsgariUcretTablosu { Id = "2015-1", yil = "2015-1", brut = Convert.ToDecimal(1201.50), net = Convert.ToDecimal(949.07), yil2 = 2015, donem = 1 },

                 new AsgariUcretTablosu { Id = "2014-2", yil = "2014-2", brut = Convert.ToDecimal(1134.00), net = Convert.ToDecimal(891.04), yil2 = 2014, donem = 2 },
                 new AsgariUcretTablosu { Id = "2014-1", yil = "2014-1", brut = Convert.ToDecimal(1071.00), net = Convert.ToDecimal(846.00), yil2 = 2014, donem = 1 },

                 new AsgariUcretTablosu { Id = "2013-2", yil = "2013-2", brut = Convert.ToDecimal(1021.50), net = Convert.ToDecimal(803.68), yil2 = 2013, donem = 2 },
                 new AsgariUcretTablosu { Id = "2013-1", yil = "2013-1", brut = Convert.ToDecimal(978.60), net = Convert.ToDecimal(773.01), yil2 = 2013, donem = 1 },

                 new AsgariUcretTablosu { Id = "2012-2", yil = "2012-2", brut = Convert.ToDecimal(940.50), net = Convert.ToDecimal(739.79), yil2 = 2012, donem = 2 },
                 new AsgariUcretTablosu { Id = "2012-1", yil = "2012-1", brut = Convert.ToDecimal(886.50), net = Convert.ToDecimal(701.13), yil2 = 2012, donem = 1 },

                 new AsgariUcretTablosu { Id = "2011-2", yil = "2011-2", brut = Convert.ToDecimal(837.00), net = Convert.ToDecimal(658.95), yil2 = 2011, donem = 2 },
                 new AsgariUcretTablosu { Id = "2011-1", yil = "2011-1", brut = Convert.ToDecimal(796.50), net = Convert.ToDecimal(629.95), yil2 = 2011, donem = 1 },

                 new AsgariUcretTablosu { Id = "2010-2", yil = "2010-2", brut = Convert.ToDecimal(760.50), net =Convert.ToDecimal(599.12),yil2 = 2010, donem = 2 },
                 new AsgariUcretTablosu { Id = "2010-1", yil = "2010-1", brut = Convert.ToDecimal(729), net = Convert.ToDecimal(567.57), yil2 = 2010, donem = 1 }
                
                 );

            // Resmi Tatiller

            modelBuilder.Entity<ResmiTatiller>()
                      .HasKey(p => p.Id);
            modelBuilder.Entity<ResmiTatiller>()
                .HasData(

                // 2022
                new ResmiTatiller
                {
                    Id ="2022-1",
                    yil =2022,
                    tur =1,
                    tarih= new DateTime (2022,1,1),
                    aciklama ="YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id="2022-2",
                    yil =2022
                    ,tur =1,
                    tarih = new DateTime (2022,4,23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"

                },
                new ResmiTatiller { 
                    Id="2022-3",
                    yil=2022,
                    tur=1,
                    tarih = new DateTime (2022,5,1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                }, 
                new ResmiTatiller
                {                
                    Id="2022-4",
                    yil=2022,
                    tur=2,
                    tarih = new DateTime (2022,5,1),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id="2022-5",
                    yil=2022,
                    tur =2,
                    tarih = new DateTime(2022,5,2),
                    aciklama = "RAMAZAN BAYRAMI"
                }, 
                new ResmiTatiller
                {
                    Id="2022-6",
                    yil= 2022,
                    tur= 2,
                    tarih= new DateTime(2022,5,3),
                    aciklama = "RAMAZAN BAYRAMI 2.GÜN"
                },
                new ResmiTatiller
                {
                    Id="2022-7",
                    yil=2022,
                    tur=2,
                    tarih = new DateTime(2022,5,4),
                    aciklama = "RAMAZAN BAYRAMI 3.GÜN"
                },
                new ResmiTatiller
                {
                    Id="2022-8",
                    yil=2022,
                    tur=1,
                    tarih  = new DateTime(2022,5,19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2022-9",
                    yil  =2022,
                    tur= 2,
                    tarih= new DateTime(2022,7,8),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                { 
                    Id="2022-10",
                    yil= 2022,
                    tur=2,  
                    tarih=new DateTime(2022,7,9),
                    aciklama= "KURBAN BAYRAMI 1.GÜN"
                }, 
                new ResmiTatiller
                {
                    Id="2022-11",
                    yil=2022,
                    tur=2,
                    tarih= new DateTime(2022,7,10),
                    aciklama = "KURBAN BAYRAMI 2.GÜN"
                },
                new ResmiTatiller
                {
                    Id="2022-12",
                    yil =2022,
                    tur = 2,
                    tarih  = new DateTime(2022,7,11),
                    aciklama = "KURBAN BAYRAMI 3.GÜN"
                },
                new ResmiTatiller
                {
                    Id= "2022-13",
                    yil =2022,
                    tur=2,
                    tarih= new DateTime(2022,7,12),
                    aciklama = "KURBAN BAYRAMI 4.GÜN"

                },
                new ResmiTatiller
                {
                    Id="2022-14",
                    yil=2022,
                    tur=1,
                    tarih = new DateTime(2022,7,15),
                    aciklama = "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id="2022-15",
                    yil=2022,
                    tur=1,
                    tarih = new DateTime(2022,8,30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2022-16",
                    yil=2022,
                    tur=1,
                    tarih= new DateTime(2022,10,28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                        new ResmiTatiller
                        {
                            Id = "2022-17",
                            yil = 2022,
                            tur = 1,
                            tarih = new DateTime(2022, 10, 29),
                            aciklama = "CUMHURİYET BAYRAMI"
                        },

                // 2021
                            new ResmiTatiller
                {
                    Id = "2021-1",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2021-2",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-3",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2021-4",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2021-5",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 5, 12),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2021-6",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 5, 13),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-7",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 5, 14),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-8",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 5, 15),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-9",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 7, 15),
                    aciklama = "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2021-10",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 7, 19),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2021-11",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 7, 20),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-12",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 7, 21),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-13",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 7, 22),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-14",
                    yil = 2021,
                    tur = 2,
                    tarih = new DateTime(2021, 7, 23),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-15",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-16",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2021-17",
                    yil = 2021,
                    tur = 1,
                    tarih = new DateTime(2021, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },



                // 2020
                new ResmiTatiller
                {
                    Id = "2020-1",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2020-2",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-3",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2020-4",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2020-5",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 5, 23),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2020-6",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 5, 24),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-7",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 5, 25),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-8",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 5, 26),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-9",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 7, 15),
                    aciklama = "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2020-10",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 7, 30),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2020-11",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 7, 31),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-12",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 8, 1),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-13",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 8, 2),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-14",
                    yil = 2020,
                    tur = 2,
                    tarih = new DateTime(2020, 8, 3),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-15",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-16",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2020-17",
                    yil = 2020,
                    tur = 1,
                    tarih = new DateTime(2020, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },

                // 2019
                new ResmiTatiller
                {
                    Id = "2019-1", yil = 2019,   tur = 1, tarih = new DateTime(2019, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2019-2", yil = 2019,  tur = 1,  tarih = new DateTime(2019, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2019-3",  yil = 2019,  tur = 1,tarih = new DateTime(2019, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2019-4", yil = 2019, tur = 1,  tarih = new DateTime(2019, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2019-5",  yil = 2019,     tur = 2, tarih = new DateTime(2019, 6, 3),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id="2019-6", yil=2019,  tur=2, tarih= new DateTime(2019,6,4),
                    aciklama= "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-7",yil=2019, tur=2, tarih= new DateTime(2019,6,5),
                    aciklama= "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-8", yil=2019,tur=2, tarih= new DateTime(2019,6,6),
                    aciklama="RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-9", yil=2019,tur=1, tarih= new DateTime(2019,7,15),
                    aciklama= "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id="2019-10", yil=2019, tur=2, tarih= new DateTime(2019,8,10),
                    aciklama= "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id="2019-11", yil=2019, tur=2, tarih= new DateTime(2019,8,11),
                    aciklama= "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-12", yil=2019, tur=2, tarih= new DateTime(2019,8,12),
                    aciklama= "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-13", yil=2019, tur=2, tarih= new DateTime(2019,8,13),
                    aciklama="KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id= "2019-14", yil=2019, tur=2, tarih= new DateTime(2019,8,14),
                    aciklama="KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-15", yil=2019, tur=1, tarih= new DateTime(2019,8,30),
                    aciklama="ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-16", yil=2019, tur=1, tarih= new DateTime(2019,10,28),
                    aciklama= "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id="2019-17", yil=2019, tur=1, tarih= new DateTime(2019,10,29),
                    aciklama="CUMHURİYET BAYRAMI"
                },

                    // 2018
                new ResmiTatiller
                {
                    Id = "2018-1",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2018-2",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-3",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2018-4",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2018-5",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 6, 14),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2018-6",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 6, 15),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-7",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 6, 16),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-8",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 6, 17),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-9",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 7, 15),
                    aciklama = "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2018-10",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 8, 20),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2018-11",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 8, 21),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-12",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 8, 22),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-13",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 8, 23),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-14",
                    yil = 2018,
                    tur = 2,
                    tarih = new DateTime(2018, 8, 24),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-15",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-16",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2018-17",
                    yil = 2018,
                    tur = 1,
                    tarih = new DateTime(2018, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },


                  // 2017
                new ResmiTatiller
                {
                    Id = "2017-1",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2017-2",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-3",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2017-4",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2017-5",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 6, 24),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2017-6",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 6, 25),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-7",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 6, 26),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-8",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 6, 27),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-9",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 7, 15),
                    aciklama = "DEMOKRASİ VE MİLLİ BİRLİK GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2017-10",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 8, 31),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2017-11",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 9, 1),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-12",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 9, 2),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-13",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 9, 3),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-14",
                    yil = 2017,
                    tur = 2,
                    tarih = new DateTime(2017, 8, 4),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-15",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-16",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2017-17",
                    yil = 2017,
                    tur = 1,
                    tarih = new DateTime(2017, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },


                // 2016
                new ResmiTatiller
                {
                    Id = "2016-1",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2016-2",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-3",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2016-4",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },

                new ResmiTatiller
                {
                    Id = "2016-5",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 7, 4),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2016-6",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 7, 5),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-7",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 7, 6),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-8",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 7, 7),
                    aciklama = "RAMAZAN BAYRAMI"
                },
             
                new ResmiTatiller
                {
                    Id = "2016-10",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 9, 11),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2016-11",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 9, 12),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-12",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 9, 13),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-13",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 9, 14),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-14",
                    yil = 2016,
                    tur = 2,
                    tarih = new DateTime(2016, 9, 15),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-15",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-16",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2016-17",
                    yil = 2016,
                    tur = 1,
                    tarih = new DateTime(2016, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },

                // 2015
                new ResmiTatiller
                {
                    Id = "2015-1",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2015-2",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-3",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2015-4",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2015-5",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 7, 16),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2015-6",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 7, 17),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-7",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 7, 18),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-8",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 7, 19),
                    aciklama = "RAMAZAN BAYRAMI"
                },
              
                new ResmiTatiller
                {
                    Id = "2015-10",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 9, 23),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2015-11",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 9, 24),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-12",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 9, 25),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-13",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 9, 26),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-14",
                    yil = 2015,
                    tur = 2,
                    tarih = new DateTime(2015, 9, 27),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-15",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-16",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2015-17",
                    yil = 2015,
                    tur = 1,
                    tarih = new DateTime(2015, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },


                //-------------
                // 2014
                new ResmiTatiller
                {
                    Id = "2014-1",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2014-2",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-3",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2014-4",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2014-5",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 7, 27),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2014-6",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 7, 28),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-7",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 7, 29),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-8",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 7, 30),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                
                new ResmiTatiller
                {
                    Id = "2014-10",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 10, 3),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2014-11",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 10, 4),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-12",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 10, 5),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-13",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 10, 6),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-14",
                    yil = 2014,
                    tur = 2,
                    tarih = new DateTime(2014, 10, 7),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-15",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-16",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2014-17",
                    yil = 2014,
                    tur = 1,
                    tarih = new DateTime(2014, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                },

                // 2013
                new ResmiTatiller
                {
                    Id = "2013-1",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 1, 1),
                    aciklama = "YILBAŞI"
                },
                new ResmiTatiller
                {
                    Id = "2013-2",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 4, 23),
                    aciklama = "ULUSAL EGEMENLİK VE ÇOCUK BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-3",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 5, 1),
                    aciklama = "EMEK VE DAYANIŞMA GÜNÜ"
                },
                new ResmiTatiller
                {
                    Id = "2013-4",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 5, 19),
                    aciklama = "ATATÜRK'Ü ANMA GENÇLİK VE SPOR BAYRAMI"

                },
                new ResmiTatiller
                {
                    Id = "2013-5",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 8, 7),
                    aciklama = "RAMAZAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2013-6",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 8, 8),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-7",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 8, 9),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-8",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 8, 10),
                    aciklama = "RAMAZAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-10",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 10, 14),
                    aciklama = "KURBAN BAYRAMI AREFESi"
                },
                new ResmiTatiller
                {
                    Id = "2013-11",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 10, 15),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-12",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 10, 16),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-13",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 10, 17),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-14",
                    yil = 2013,
                    tur = 2,
                    tarih = new DateTime(2013, 10, 18),
                    aciklama = "KURBAN BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-15",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 8, 30),
                    aciklama = "ZAFER BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-16",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 10, 28),
                    aciklama = "CUMHURİYET BAYRAMI"
                },
                new ResmiTatiller
                {
                    Id = "2013-17",
                    yil = 2013,
                    tur = 1,
                    tarih = new DateTime(2013, 10, 29),
                    aciklama = "CUMHURİYET BAYRAMI"
                }
              

                );

            // Vergi Dilimleri Servisi
            modelBuilder.Entity<VergiDilimleri>().HasKey(p => p.Id);

            modelBuilder.Entity<VergiDilimleri>().HasData(
                 new VergiDilimleri
                 {
                     Id = 2011,
                     Vd1Miktar = 9400,
                     Vd1Yuzde = 15,
                     Vd2Miktar = 23000,
                     Vd2Yuzde = 20,
                     Vd3Miktar = 80000,
                     Vd3Yuzde = 27,
                     Vd4Yuzde = 35,
                     SgkTaban2=(decimal)837,
                     SgkTavan2=(decimal)5440.50,
                     SgkTaban=(decimal)796.50,
                     SgkTavan =(decimal)5177.40

                 },
                 new VergiDilimleri
                 {
                     Id = 2012,
                     Vd1Miktar = 10000,
                     Vd1Yuzde = 15,
                     Vd2Miktar = 25000,
                     Vd2Yuzde = 20,
                     Vd3Miktar = 88000,
                     Vd3Yuzde = 27,
                     Vd4Yuzde = 35,
                     SgkTaban=(decimal)886.50,
                     SgkTavan=(decimal)5762.40,
                     SgkTaban2=(decimal)940.50,
                     SgkTavan2=(decimal)6113.40

                 },
                 new VergiDilimleri
                 {
                     Id = 2013,
                     Vd1Miktar = 10700,
                     Vd1Yuzde = 15,
                     Vd2Miktar = 26000,
                     Vd2Yuzde = 20,
                     Vd3Miktar = 94000,
                     Vd3Yuzde = 27,
                     Vd4Yuzde = 35,
                     SgkTaban=(decimal)978.60,
                     SgkTavan=(decimal)6360.90,
                     SgkTaban2=(decimal)1021.50,
                     SgkTavan2=(decimal)6639.90


                 },
                 new VergiDilimleri
                 {
                     Id = 2014,
                     Vd1Miktar = 11000,
                     Vd1Yuzde = 15,
                     Vd2Miktar = 27000,
                     Vd2Yuzde = 20,
                     Vd3Miktar = 97000,
                     Vd3Yuzde = 27,
                     Vd4Yuzde = 35,
                     SgkTaban = (decimal)1.071,
                     SgkTavan=(decimal)6961.50,
                     SgkTaban2=(decimal )1134,
                     SgkTavan2=(decimal)7371


                 },
                  new VergiDilimleri
                  {
                      Id = 2015,
                      Vd1Miktar = 12000,
                      Vd1Yuzde = 15,
                      Vd2Miktar = 29000,
                      Vd2Yuzde = 20,
                      Vd3Miktar = 106000,
                      Vd3Yuzde = 27,
                      Vd4Yuzde = 35,
                      SgkTaban = (decimal)1201.50,
                      SgkTavan = (decimal)7809.90,
                      SgkTaban2 = (decimal)1273.50,
                      SgkTavan2 = (decimal)8277.90

                  },
                  new VergiDilimleri
                  {
                      Id = 2016,
                      Vd1Miktar = 12600,
                      Vd1Yuzde = 15,
                      Vd2Miktar = 30000,
                      Vd2Yuzde = 20,
                      Vd3Miktar = 110000,
                      Vd3Yuzde = 27,
                      Vd4Yuzde = 35,
                      SgkTaban = (decimal)1647,
                      SgkTavan = (decimal)10705.50,
                      SgkTaban2 = (decimal)1647,
                      SgkTavan2 = (decimal)10705.50
                  },
                new VergiDilimleri
                {
                    Id = 2017,
                    Vd1Miktar = 13000,
                    Vd1Yuzde = 15,
                    Vd2Miktar = 30000,
                    Vd2Yuzde = 20,
                    Vd3Miktar = 110000,
                    Vd4Yuzde = 35,
                    SgkTaban = (decimal)1777.50,
                    SgkTavan = (decimal)13331.40,
                    SgkTaban2 = (decimal)1777.50,
                    SgkTavan2 = (decimal)13331.40

                },
                new VergiDilimleri
                {
                    Id = 2018,
                    Vd1Miktar = 14800,
                    Vd1Yuzde = 15,
                    Vd2Miktar = 34000,
                    Vd2Yuzde = 20,
                    Vd3Miktar = 120000,
                    Vd3Yuzde = 27,
                    Vd4Yuzde = 35,
                    SgkTaban = (decimal)2029.50,
                    SgkTavan = (decimal)15221.40,
                    SgkTaban2 = (decimal)2029.50,
                    SgkTavan2 = (decimal)15221.40
                },
                new VergiDilimleri
                {
                    Id = 2019,
                    Vd1Miktar = 18000,
                    Vd1Yuzde = 15,
                    Vd2Miktar = 40000,
                    Vd2Yuzde = 20,
                    Vd3Miktar = 148000,
                    Vd3Yuzde = 27,
                    Vd4Yuzde = 35,
                    SgkTaban = (decimal)2558.40,
                    SgkTavan = (decimal)19188,
                    SgkTaban2 = (decimal)2558.40,
                    SgkTavan2 = (decimal)19188

                },
                new VergiDilimleri
                {
                    Id = 2020,
                    Vd1Miktar = 22000,
                    Vd1Yuzde = 15,
                    Vd2Miktar = 49000,
                    Vd2Yuzde = 20,
                    Vd3Miktar = 180000,
                    Vd4Yuzde = 35,
                    SgkTaban = (decimal)2943,
                    SgkTavan = (decimal)22072.50,
                    SgkTaban2 = (decimal)2943,
                    SgkTavan2 = (decimal)22072.50
                },
                new VergiDilimleri
                {
                    Id = 2021,
                    Vd1Miktar = 24000,
                    Vd1Yuzde = 15,
                    Vd2Miktar = 53000,
                    Vd2Yuzde = 20,
                    Vd3Miktar = 190000,
                    Vd4Yuzde = 35,
                    SgkTaban = (decimal)3577.50,
                    SgkTavan = (decimal)26831.40,
                    SgkTaban2 = (decimal)3577.50,
                    SgkTavan2 = (decimal)26831.40
                }

                ) ; ; ;


            // Kıdem Tavanları
            modelBuilder.Entity<KidemTavan>().HasKey(p => p.Id);


            modelBuilder.Entity<KidemTavan>()
                .HasData(
                 new KidemTavan
                 {
                     Id = "2022",
                     yil = 2022,
                     donem = 1,
                     miktar = (decimal)10848.59,
                     baslangic = new DateTime(2022, 1, 1),
                     bitis = new DateTime(2022, 6, 30)
                 },
                      new KidemTavan
                      {
                          Id = "2021B",
                          yil = 2021,
                          donem = 2,
                          miktar = (decimal)8284.51,
                          baslangic = new DateTime(2021, 7, 1),
                          bitis = new DateTime(2021, 12, 31)
                      },
                new KidemTavan {
                Id="2021",
                yil=2021,
                donem=1,
                miktar= (decimal)7638.96,
                baslangic = new DateTime(2021,1,1), 
                bitis= new DateTime(2021,6,30)           
                },
                 new KidemTavan
                 {
                     Id = "2020B",
                     yil = 2020,
                     donem = 2,
                     miktar = (decimal)7117.17,
                     baslangic = new DateTime(2020, 7, 1),
                     bitis = new DateTime(2020, 12, 31)

                 },
                 new KidemTavan
                 {
                     Id = "2020",
                     yil = 2020,
                     donem = 1,
                     miktar = (decimal)6730.15,
                     baslangic = new DateTime(2020, 1, 1),
                     bitis = new DateTime(2020, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2019B",
                     yil = 2019,
                     donem = 2,
                     miktar = (decimal)6379.86,
                     baslangic = new DateTime(2019, 7, 1),
                     bitis = new DateTime(2019, 12, 31)

                 },
                 new KidemTavan
                 {
                     Id = "2019",
                     yil = 2019,
                     donem = 1,
                     miktar = (decimal)6017.60,
                     baslangic = new DateTime(2019, 1, 1),
                     bitis = new DateTime(2019, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2018B",
                     yil = 2018,
                     donem = 2,
                     miktar = (decimal)5434.42,
                     baslangic = new DateTime(2018, 7, 1),
                     bitis = new DateTime(2018, 12, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2018",
                     yil = 2018,
                     donem = 1,
                     miktar = (decimal)5001.76,
                     baslangic = new DateTime(2018, 1, 1),
                     bitis = new DateTime(2018, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2017B",
                     yil = 2017,
                     donem = 2,
                     miktar = (decimal)4732.48,
                     baslangic = new DateTime(2017, 7, 1),
                     bitis = new DateTime(2017, 12, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2017",
                     yil = 2017,
                     donem = 1,
                     miktar = (decimal)4426.16,
                     baslangic = new DateTime(2017, 1, 1),
                     bitis = new DateTime(2017, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2016B",
                     yil = 2016,
                     donem = 2,
                     miktar = (decimal)4297.21,
                     baslangic = new DateTime(2016, 7, 1),
                     bitis = new DateTime(2017, 12, 31)

                 },
                 new KidemTavan
                 {
                     Id = "2016",
                     yil = 2016,
                     donem = 1,
                     miktar = (decimal)4092.53,
                     baslangic = new DateTime(2016, 1, 1),
                     bitis = new DateTime(2016, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2015C",
                     yil = 2015,
                     donem = 3,
                     miktar = (decimal)3828.37,
                     baslangic = new DateTime(2015, 9, 1),
                     bitis = new DateTime(2015, 12, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2015B",
                     yil = 2015,
                     donem = 2,
                     miktar = (decimal)3709.98,
                     baslangic = new DateTime(2015, 7, 1),
                     bitis = new DateTime(2015, 8, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2015",
                     yil = 2015,
                     donem = 1,
                     miktar = (decimal)3541.37,
                     baslangic = new DateTime(2015, 1, 1),
                     bitis = new DateTime(2015, 6, 30)
                 },
                 new KidemTavan
                 {
                     Id = "2014",
                     yil = 2014,
                     donem = 1,
                     miktar = (decimal)3438.22,
                     baslangic = new DateTime(2014, 1, 1),
                     bitis = new DateTime(2014, 12, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2013B",
                     yil = 2013,
                     donem = 2,
                     miktar = (decimal)3254.44,
                     baslangic = new DateTime(2013, 7, 1),
                     bitis = new DateTime(2013, 12, 31)

                 },
                 new KidemTavan
                 {
                     Id = "2013",
                     yil = 2013,
                     donem = 1,
                     miktar = (decimal)3129.25,
                     baslangic = new DateTime(2013, 1, 1),
                     bitis = new DateTime(2013, 6, 30)

                 },
                 new KidemTavan
                 {
                     Id = "2012B",
                     yil = 2012,
                     donem = 2,
                     miktar = (decimal)3033.98,
                     baslangic = new DateTime(2012, 7, 1),
                     bitis = new DateTime(2012, 12, 31)
                 },
                 new KidemTavan
                 {
                     Id = "2012",
                     yil = 2012,
                     donem = 1,
                     miktar = (decimal)2917.27,
                     baslangic = new DateTime(2012, 1, 1),
                     bitis = new DateTime(2012, 6, 30)
                 });
                 

                 
            // Kişiler Bigi.....

            modelBuilder.Entity<Contact>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Contact>()
                .Property(p => p.Ad).IsRequired();

            modelBuilder.Entity<Contact>().Ignore(c => c.SearchTerm);
            modelBuilder.Entity<Contact>().Ignore(c => c.FullName);

        }


    }
}
