using System;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Syncfusion.Data.Extensions;


namespace LawCheckTazminat.Services
{
    public class FazlaMesaiService
    {

        // Yeni Kayıt Ekleme...

        public bool AddItem(FazlaMesai item)
        {
          using(var context= new Repo(App.baglantiDB))
            {
                context.fazlaMesais.Add(item);
                try
                {
                    context.SaveChanges();
                    

                }catch(Exception ex)
                {

                }
             

                
            }

            return true;
        }
        public async Task<bool> AddItemAsync(FazlaMesai item)
        {

            bool durum = false; ;
            if (item.EskiId != "")
            {
              durum=    DeleteItem(item.EskiId);
        

                if(durum != true)
                     {
                            return false;
                        }
            }

            using (var context = new Repo(App.baglantiDB))
            {
                try
                {
                    await context.fazlaMesais.AddAsync(item).ConfigureAwait(false);
                    await context.SaveChangesAsync().ConfigureAwait(false);

                }catch(Exception ex)
                {

                }
        
            }

           
            return true;

        }


        // Kayıtları Listeleme
        public async Task<FazlaMesai> GetItem(string id)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.fazlaMesais where s.Id == id select s)
                  
                    .FirstOrDefaultAsync().ConfigureAwait(false);
                FazlaMesai kayit = new FazlaMesai();
                 kayit = sonuc;

                var sonuc2 = (from s2 in context.fazlaMesaiHaftalars where s2.FazlaMesaiId == id select s2);
                kayit.FMHaftalarBilgi = sonuc2.ToObservableCollection();

                var sonuc3 = (from s3 in context.maasFazlaMesais where s3.mesaiId == id select s3);
                kayit.MaasBilgi = sonuc3.ToObservableCollection();

                var sonuc4 = (from s4 in context.fazlaMesaiResmiTatils where s4.mesaiId == id select s4);      
                kayit.ResmiTatilBilgi = sonuc4.ToObservableCollection();

                var sonuc5 = (from s5 in context.fazlaMesaiKisiIzinGunleris where s5.FazlaMesaiId == id select s5);
                kayit.IzinGunleriBilgi = sonuc5.ToObservableCollection();

                var sonuc6 = (from s6 in context.fazlaMesaiKisiIzinKayitlaris where s6.FazlaMesaiId == id select s6);
                kayit.IzinKaytilariBilgi = sonuc6.ToObservableCollection();

                var sonuc7 = (from s7 in context.fazlaMesaiHaftalikIzinHaftalars where s7.FazlaMesaiId == id select s7);
                kayit.HaftalikIzinHaftalarBilgi = sonuc7.ToObservableCollection();

                // .Include(p1 => p1.FMHaftalarBilgi)
                //    .Include(p5 => p5.MaasBilgi)
                //    .Include(p6 => p6.ResmiTatilBilgi)
                //    .Include(p3 => p3.IzinGunleriBilgi);
                ////   .Include(p2 => p2.HaftalikIzinHaftalarBilgi)
                //// .Include(p4 => p4.IzinKaytilariBilg
                return kayit;
            }
        }

        public List<FazlaMesai> GetItemS()
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = from s in context.fazlaMesais select s;
                return sonuc.ToList();

            }

        }

        public List<FazlaMesaiHaftalar> GetItemSHaftalar()
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.fazlaMesaiHaftalars select s).ToList();

                return sonuc;
            }
        }


        //Kişi Kayıtları........

        public List<FazlaMesai> KisiKayitlari(string kisiId, bool forceRefresh = false)
        {
            using (var context = new Repo(App.baglantiDB))
            {

                List<FazlaMesai> liste = new List<FazlaMesai>();
                var sonuc = (from s in context.fazlaMesais where s.KisiId == kisiId select s);
                //.Include(p1 => p1.FMHaftalarBilgi)

                //.Include(p5 => p5.MaasBilgi)
                //.Include(p6 => p6.ResmiTatilBilgi)
                //.Include(p3 => p3.IzinGunleriBilgi);
                //   .Include(p2 => p2.HaftalikIzinHaftalarBilgi)
                // .Include(p4 => p4.IzinKaytilariBilgi)
                //.ToList();

                var sonuc2 = (from s2 in context.fazlaMesais select s2);

                var aa = sonuc.ToList();
                if (aa.Count== 0)
                {
                    liste = sonuc2.ToList();
                }

                else
                {
                    liste = sonuc.ToList();
                }
                

               
                return liste;


            }
        }

        public List<FazlaMesai> KisiKayitlari2(string _kisiId)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.fazlaMesais where s.KisiId == _kisiId select s);
                return sonuc.ToList();
            }

        }


        //Kayıt Silme
        public bool DeleteItem(string idd)
        {

            using (var context = new Repo(App.baglantiDB))
            {

                var silMaas =  (from s in context.maasFazlaMesais where s.mesaiId == idd select s);
                var silMaasL = silMaas.ToList();

                foreach (var t in silMaasL)
                {
                    context.maasFazlaMesais.Remove(t);
                }

                //--------

                var silResmi =  (from s2 in context.fazlaMesaiResmiTatils where s2.mesaiId == idd select s2);
                var silResmiL = silResmi.ToList();
                foreach (var t2 in silResmiL)
                {
                    context.fazlaMesaiResmiTatils.Remove(t2);
                }

                var silHafta =  (from s3 in context.fazlaMesaiHaftalars where s3.FazlaMesaiId == idd select s3);
                var silHaftaL = silHafta.ToList();
                foreach (var t3 in silHaftaL)
                {
                    context.fazlaMesaiHaftalars.Remove(t3);
                }


                var h1 = (from s4 in context.fazlaMesaiHaftalikIzinHaftalars where s4.FazlaMesaiId == idd select s4);
                var h1L = h1.ToList();
                foreach(var t4 in h1L)
                {
                    context.fazlaMesaiHaftalikIzinHaftalars.Remove(t4);
                }

                var h2 =  (from s5 in context.fazlaMesaiKisiIzinGunleris where s5.FazlaMesaiId == idd select s5);
                var h2L = h2.ToList();
                foreach(var t5 in h2L)
                {
                    context.fazlaMesaiKisiIzinGunleris.Remove(t5);
                }

                var h3 =  (from s6 in context.fazlaMesaiKisiIzinKayitlaris where s6.FazlaMesaiId == idd select s6);
                var h3L = h3.ToList();
                foreach(var t6 in h3L)
                {
                    context.fazlaMesaiKisiIzinKayitlaris.Remove(t6);
                }


                var sonuc = (from s in context.fazlaMesais where s.Id == idd select s).FirstOrDefault();
                context.fazlaMesais.Remove(sonuc);

                 context.SaveChanges();

                return true;

            }
        }

        //Fazla Mesai Kayıt Güncelleme.......

        public async Task<bool> UpdateItem(FazlaMesai item)
        {

            using (var context= new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.fazlaMesais where s.Id == item.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                sonuc.Aciklama = item.Aciklama;
                sonuc.Ad = item.Ad;
                sonuc.BasTarih = item.BasTarih;
                sonuc.BitTarih = item.BitTarih;
                sonuc.BrutUcret = item.BrutUcret;
                sonuc.Ek_Gece3 = item.Ek_Gece3;
                sonuc.FazlaCalismaMiktar = item.FazlaCalismaMiktar;
                sonuc.FazlaMesaiMiktar = item.FazlaMesaiMiktar;
                sonuc.FazlaMesaiMiktar2 = item.FazlaMesaiMiktar2;
                sonuc.FazlaMesaiVar = item.FazlaMesaiVar;
                sonuc.FM_Gunler = item.FM_Gunler;
                sonuc.FM_GunlerGece = item.FM_GunlerGece;
                sonuc.HaftaCalismaSaat = item.HaftaCalismaSaat;
                sonuc.HaftaCalismaSaat2 = item.HaftaCalismaSaat2;
                //sonuc.HaftalarBilgi = item.HaftalarBilgi;
                sonuc.HaftasonuDus = item.HaftasonuDus;
                sonuc.HaftasonuVar = item.HaftasonuVar;
                sonuc.HesapTarihi = item.HesapTarihi;
                
                sonuc.SaatUcret = item.SaatUcret;
                sonuc.Soyad = item.Soyad;
                sonuc.SozlesmeCalismaSaat = item.SozlesmeCalismaSaat;
                sonuc.TatilGunu = item.TatilGunu;
                sonuc.TatilGunu2 = item.TatilGunu2;
                sonuc.Toplam = item.Toplam;
                sonuc.ToplamHsonu = item.ToplamHsonu;
                sonuc.ToplamHsonuDamgaVergi = item.ToplamHsonuDamgaVergi;
                sonuc.ToplamHsonuNet = item.ToplamHsonuNet;
                sonuc.ToplamHsonuSGK = item.ToplamHsonuSGK;
                sonuc.ToplamHsonuVergi = item.ToplamHsonuVergi;

                sonuc.ToplamNet = item.ToplamNet;
                sonuc.ToplamResmiTatil = item.ToplamResmiTatil;
                sonuc.ToplamResmiTatilDamgaVergi = item.ToplamResmiTatilDamgaVergi;
                sonuc.ToplamResmiTatilNet = item.ToplamResmiTatilNet;
                sonuc.ToplamResmiTatilSGK = item.ToplamResmiTatilSGK;
                sonuc.ToplamResmiTatilVergi = item.ToplamResmiTatilVergi;
                sonuc.ToplamSGK = item.ToplamSGK;
                sonuc.ToplamVergi = sonuc.ToplamVergi;
                sonuc.VergiMatrah = sonuc.VergiMatrah;
                sonuc.Vergiyil = sonuc.Vergiyil;

                // Haftalar
                var sonuc2 = await (from s2 in context.fazlaMesaiHaftalars where s2.FazlaMesaiId == item.Id select s2).ToListAsync().ConfigureAwait(false);

                var sonuc2L = sonuc2.ToList();

                foreach(var t in item.FMHaftalarBilgi)
                {
                    t.Id = Guid.NewGuid().ToString().Substring(0, 8);

                    context.fazlaMesaiHaftalars.Add(t);

                }
                foreach (var t in sonuc2L)
                {

                    context.fazlaMesaiHaftalars.Remove(t);
                }
                
                //foreach (var t in item.FMHaftalarBilgi)
                //{

                //    var kayit1 = sonuc2.Find(o => o.Id == t.Id);

                //    if (kayit1 == null)
                //    {
                //        t.FazlaMesaiId = item.Id;
                //        t.fazlaMesai = item;

                //        context.fazlaMesaiHaftalars.Add(t);
                //    }
                //    else
                //    {
                //        kayit1.BasTarih = t.BasTarih;
                //        kayit1.BitTarih = t.BitTarih;
                //        kayit1.BrutUcret = t.BrutUcret;
                //        kayit1.FazlaCalismaOHaftadaki = t.FazlaCalismaOHaftadaki;
                //        kayit1.FazlaMesaiOHaftadaki = t.FazlaMesaiOHaftadaki;
                //        kayit1.FazlaMesaiSozlesme = t.FazlaMesaiSozlesme;
                //        kayit1.FazlaMesaiVar = t.FazlaMesaiVar;
                //        kayit1.GunlukUcret = t.GunlukUcret;
                //        kayit1.HaftaSonuCalismaGunSayisi = t.HaftaSonuCalismaGunSayisi;
                //        kayit1.HaftaSonuCalismaVar = t.HaftaSonuCalismaVar;
                //        kayit1.HaftaSonuUcret = t.HaftaSonuUcret;
                //        kayit1.HaftaSonuUcret2 = t.HaftaSonuUcret2;
                //        kayit1.MesaiUcret = t.MesaiUcret;
                //        kayit1.MesaiUcret2 = t.MesaiUcret2;
                //        kayit1.ResmiTatilCalismaGunSayisi = t.ResmiTatilCalismaGunSayisi;
                //        kayit1.ResmiTatilCalismaVar = t.ResmiTatilCalismaVar;
                //        kayit1.ResmiTatilUcret = t.ResmiTatilUcret;
                //        kayit1.ResmiTatilUcret2 = t.ResmiTatilUcret2;
                //        kayit1.SaatlikUcret = t.SaatlikUcret;
                //        kayit1.Sira = t.Sira;


                //    }

                //}

                // Maaşlar
                var sonuc3 = await (from s3 in context.maasFazlaMesais where s3.mesaiId == item.Id select s3).ToListAsync().ConfigureAwait(false);
                var sonuc3L = sonuc3.ToList();

                foreach(var t2 in item.MaasBilgi)
                {
                    t2.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    context.maasFazlaMesais.Add(t2);
                }

                foreach (var t2 in sonuc3L)
                {
                    context.maasFazlaMesais.Remove(t2);
                }


                //foreach (var t2 in item.MaasBilgi)
                //{
                //    var kayit2 = sonuc3.Find(o => o.Id == t2.Id);

                //    if (kayit2 == null)
                //    {
                //        t2.mesaiId = item.Id;
                //        t2.fazlaMesai = item;
                //        context.maasFazlaMesais.Add(t2);
                //    }
                //    else
                //    {
                //        kayit2.brutMaas = t2.brutMaas;
                //        kayit2.ekBilgi1 = t2.ekBilgi1;
                //        kayit2.ekBilgi2 = t2.ekBilgi2;
                //        kayit2.ekBilgi3 = t2.ekBilgi3;
                //        kayit2.ekBilgi4 = t2.ekBilgi4;

                //        kayit2.netMaas = t2.netMaas;
                //        kayit2.yil = t2.yil;
                //        kayit2.yilBas = t2.yilBas;
                //        kayit2.yilBit = t2.yilBit;
                //        kayit2.yilBolum = t2.yilBolum;

                //    }
                //}

                //-Resmi Tatil

                var sonuc4 = await (from s4 in context.fazlaMesaiResmiTatils where s4.mesaiId == item.Id select s4).ToListAsync().ConfigureAwait(false);
                var sonuc4L = sonuc4.ToList();

                foreach(var t4 in item.ResmiTatilBilgi)
                {
                    t4.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    context.fazlaMesaiResmiTatils.Add(t4);
                }

                foreach (var t4 in sonuc4L)
                {
                    context.fazlaMesaiResmiTatils.Remove(t4);
                }
                



                //foreach (var t3 in item.ResmiTatilBilgi)
                //{
                //    var kayit3 = sonuc4.Find(o => o.Id == t3.Id);

                //    if (kayit3 == null)
                //    {
                //        t3.mesaiId = item.Id;
                //        t3.fazlaMesai = item;
                //        context.fazlaMesaiResmiTatils.Add(t3);

                //    }
                //    else
                //    {
                //        kayit3.ekbilgi = t3.ekbilgi;
                //        kayit3.ekBilgi1 = t3.ekBilgi1;
                //        kayit3.ekBilgi2 = t3.ekBilgi2;
                //        kayit3.ekBilgi3 = t3.ekBilgi3;
                //        kayit3.ekBilgi4 = t3.ekBilgi4;
                //        kayit3.tarih = t3.tarih;

                //    }

                //}


                // IzinKaytilariBilgi -FazlaMesaiKisiIzinKayitlari

                var sonuc5 = await (from s5 in context.fazlaMesaiKisiIzinKayitlaris where s5.FazlaMesaiId == item.Id select s5).ToListAsync().ConfigureAwait(false);

                var sonuc5L = sonuc5.ToList();
                foreach(var t5 in item.IzinKaytilariBilgi)
                {
                    t5.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    context.fazlaMesaiKisiIzinKayitlaris.Add(t5);
                }

                foreach(var t5 in sonuc5L)
                {
                    
                    context.fazlaMesaiKisiIzinKayitlaris.Remove(t5);
                }

                //foreach(var t in sonuc5L)
                //{
                //    context.fazlaMesaiKisiIzinKayitlaris.Remove(t);
                //}

                //foreach(var t2 in item.IzinKaytilariBilgi)
                //{
                //    t2.FazlaMesaiId = item.Id;
                //    t2.fazlaMesai = item;
                //    context.fazlaMesaiKisiIzinKayitlaris.Add(t2);
                //}


                // İzin Günleri Bilgi.....

                var sonuc6 = await (from s6 in context.fazlaMesaiKisiIzinGunleris where s6.FazlaMesaiId == item.Id select s6).ToListAsync().ConfigureAwait(false);

                var sonuc6L = sonuc6.ToList();
                foreach(var t6  in item.IzinGunleriBilgi)
                {
                    t6.Id = Guid.NewGuid().ToString().Substring(0, 8);
                    context.fazlaMesaiKisiIzinGunleris.Add(t6);
                }

                foreach(var t6 in sonuc6L)
                {
                    context.fazlaMesaiKisiIzinGunleris.Remove(t6);
                }

                //foreach(var t in sonuc6L)
                //{
                //    context.fazlaMesaiKisiIzinGunleris.Remove(t);
                //}
                //foreach(var t2 in item.IzinGunleriBilgi)
                //{
                //    t2.FazlaMesaiId = item.Id;
                //    t2.fazlaMesai = item;
                //    context.fazlaMesaiKisiIzinGunleris.Add(t2);
                //}


                // Fazla Mesai Haftalık İzin Haftalar Bilgi.

                var sonuc7 = await (from s7 in context.fazlaMesaiHaftalikIzinHaftalars where s7.FazlaMesaiId == item.Id select s7).ToListAsync().ConfigureAwait(false);

                var sonuc7L = sonuc7.ToList();
                foreach(var t7 in item.HaftalikIzinHaftalarBilgi)
                {
                    t7.Id = Guid.NewGuid().ToString().Substring(0, 8);

                    context.fazlaMesaiHaftalikIzinHaftalars.Add(t7);
                }

                foreach(var t7 in sonuc7L)
                {
                    context.fazlaMesaiHaftalikIzinHaftalars.Remove(t7);
                }

                //foreach(var t in sonuc7L)
                //{
                //    context.fazlaMesaiHaftalikIzinHaftalars.Remove(t);
                //}

                //foreach(var t2 in item.HaftalikIzinHaftalarBilgi)
                //{
                //    context.fazlaMesaiHaftalikIzinHaftalars.Add(t2);
                //}


                //-------------------


                //  TRY-CATCH

                try
                {
                    context.SaveChanges(false);
                }
                catch (Exception ex)
                {

                }

                

            }



            //using(var context= new Repo(App.baglantiDB))
            //{
            //    foreach( var t2 in item.FMHaftalarBilgi)
            //    {
            //        context.fazlaMesaiHaftalars.Add(t2);
            //    }
            //    foreach(var t3 in item.MaasBilgi)
            //    {
            //        context.maasFazlaMesais.Add(t3);
            //    }
            //    //  context.fazlaMesaiResmiTatils
            //    foreach(var t4 in item.ResmiTatilBilgi)
            //    {
            //        context.fazlaMesaiResmiTatils.Add(t4);
            //    }

            //    foreach(var t5 in item.IzinKaytilariBilgi)
            //    {
            //        context.fazlaMesaiKisiIzinKayitlaris.Add(t5);
            //    }

            //    foreach(var t6 in item.HaftalikIzinHaftalarBilgi)
            //    {
            //        context.fazlaMesaiHaftalikIzinHaftalars.Add(t6);
            //    }


            //    try
            //    {
            //        context.SaveChanges();

            //    }
            //    catch(Exception ex)
            //    {

            //    }

            //}

            return true;

        }



    }
}
