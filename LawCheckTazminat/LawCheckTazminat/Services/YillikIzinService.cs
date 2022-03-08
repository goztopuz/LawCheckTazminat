using System;
using System.Linq;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using Syncfusion.Data.Extensions;

namespace LawCheckTazminat.Services
{
    public class YillikIzinService
    {

        // Ekle
        public bool AddItem(YillikIzin yy)
        {
            bool durum = false;
            if (yy.EskiId != "")
            {


                durum = Sil(yy.EskiId);

                if (durum == false)
                {
                    return false;
                }
            }
            using (var context = new Repo(App.baglantiDB))
            {
                context.yillikIzins.Add(yy);
                context.SaveChanges();

            }

                return true;
        }

        //Listele Tümü---
        public List<YillikIzin> GetItemsByKisi(string _Id)
        {
            List<YillikIzin> liste = new List<YillikIzin>();
            using(var context= new Repo(App.baglantiDB))
            {

                var sonuc = from s in context.yillikIzins where s.kisiId == _Id select s;
                liste = sonuc.ToList();
            }

            return liste;

        }

        // Tek Seç...
        public YillikIzin GetItem(string  _id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                YillikIzin kayit = new YillikIzin();
                var sonuc = (from s in context.yillikIzins where s.Id == _id select s).FirstOrDefault();

                var sonuc2 = (from s2 in context.YillikIzinIzinGunleris where s2.FazlaMesaiId == _id select s2);

                var sonuc3 = (from s3 in context.YillikIzinIzinKayitlaris where s3.FazlaMesaiId == _id select s3);

                var sonuc4 = (from s4 in context.yYHaftaIznis where s4.YYId == _id select s4);

                var sonuc5=(from s5 in context.yYResmiTatils where s5.YYId==_id select s5);

                var sonuc6 = (from s6 in context.yYYCalisilan where s6.YYId == _id select s6);

                var sonuc7 = (from s7 in context.yYYHesaplanan where s7.YYId == _id select s7);


                kayit = sonuc;

                kayit.IzinGunleriBilgi = sonuc2.ToObservableCollection();
                kayit.IzinKaytilariBilgi = sonuc3.ToObservableCollection();
                kayit.IzindekiHaftaIzniBilgi = sonuc4.ToObservableCollection();
                kayit.IzindekiResmiTatillerBilgi = sonuc5.ToObservableCollection();
                kayit.CalisilanYillarBilgi = sonuc6.ToObservableCollection();
                kayit.HesapYillariBilgi = sonuc7.ToObservableCollection();


                return kayit;

            }
        }

        //Güncelle
        public bool YillikIzinGuncelle(YillikIzin yy)
        {

            using(var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.yillikIzins where s.Id == yy.Id select s).FirstOrDefault();

                if(sonuc != null)
                {
                    sonuc.Ad = yy.Ad;
                    sonuc.BrutUcret = yy.BrutUcret;
                    sonuc.DamgaV = yy.DamgaV;
                    sonuc.Gun = yy.Gun;
                    sonuc.Gun2 = yy.Gun2;
                    sonuc.GunlukUcret = yy.GunlukUcret;
                    sonuc.HakEdilen = yy.HakEdilen;
                    sonuc.HesapBaslangicTar = yy.HesapBaslangicTar;
                    sonuc.HesapBitisTar = yy.HesapBitisTar;
                    sonuc.HesapTarihi = yy.HesapTarihi;

                    sonuc.IseGirisTarihi = yy.IseGirisTarihi;
                    sonuc.Kullanilan = yy.Kullanilan;
                    sonuc.Net = yy.Net;
                    sonuc.NetUcret = yy.NetUcret;
                    sonuc.SGK = yy.SGK;
                    sonuc.Soyad = yy.Soyad;
                    sonuc.Toplam = yy.Toplam;
                    sonuc.Vergi = yy.Vergi;
                    sonuc.VergiMatrah = yy.VergiMatrah;
                    sonuc.VergiYil = yy.VergiYil;


                    var guncelListe1 = from s2 in context.YillikIzinIzinGunleris where s2.FazlaMesaiId == yy.Id select s2;

                    foreach(var t in guncelListe1)
                    {
                        context.YillikIzinIzinGunleris.Remove(t);
                    }
                    foreach(var t2 in sonuc.IzinGunleriBilgi)
                    {
                        context.YillikIzinIzinGunleris.Add(t2);
                    }

                    var guncelListe2 = from s3 in context.YillikIzinIzinKayitlaris where s3.FazlaMesaiId == yy.Id select s3;

                    foreach(var t in guncelListe2)
                    {
                        context.YillikIzinIzinKayitlaris.Remove(t);
                    }

                    foreach(var t2 in sonuc.IzinKaytilariBilgi)
                    {
                        context.YillikIzinIzinKayitlaris.Add(t2);
                    }


                    context.SaveChanges();

                }
            }


            return true;

        }

        //Sil
        public bool Sil(string _id)
        {
            using (var context = new Repo(App.baglantiDB))
            {

                var sonuc = (from s in context.yillikIzins where s.Id == _id select s).FirstOrDefault();
                if(sonuc != null)
                {


                    var sonuc2 = from s2 in context.YillikIzinIzinGunleris where s2.FazlaMesaiId == _id select s2;

                    foreach(var t2 in sonuc2)
                    {
                        context.YillikIzinIzinGunleris.Remove(t2);
                    }

                    var sonuc3 = from s3 in context.YillikIzinIzinKayitlaris where s3.FazlaMesaiId == _id select s3;

                    foreach(var t3 in sonuc3)
                    {
                        context.YillikIzinIzinKayitlaris.Remove(t3);
                    }

                    var sonuc4 = from s4 in context.yYHaftaIznis where s4.YYId == _id select s4;
                    foreach(var t4 in sonuc4)
                    {
                        context.yYHaftaIznis.Remove(t4);
                    }

                    var sonuc5 = from s5 in context.yYResmiTatils where s5.YYId == _id select s5;
                    foreach(var t5 in sonuc5)
                    {
                        context.yYResmiTatils.Remove(t5);
                    }

                    var sonuc6 = from s6 in context.yYYCalisilan where s6.YYId == _id select s6;
                    foreach(var t6 in sonuc6)
                    {
                        context.yYYCalisilan.Remove(t6);
                    }

                    var sonuc7 = from s7 in context.yYYHesaplanan where s7.YYId == _id select s7;
                    foreach(var t7 in sonuc7)
                    {
                        context.yYYHesaplanan.Remove(t7);
                    }

                    context.yillikIzins.Remove(sonuc);

                    context.SaveChanges();

                }

            }



                return true;
        }


    }
}
