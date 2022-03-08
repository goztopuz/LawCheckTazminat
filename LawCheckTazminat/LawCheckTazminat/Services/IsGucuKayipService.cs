using System;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LawCheckTazminat.Services
{
    public class IsGucuKayipService
    {
        public IsGucuKayipService()
        {
        }


        //Yeni Kayıt Ekleme
        public async Task<bool> AddItemAsync(IsgucuKayip item)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                await context.IsgucuKayips.AddAsync(item).ConfigureAwait(false);

                await context.SaveChangesAsync().ConfigureAwait(false);


            }

            return true;

        }


        public bool AddItem(IsgucuKayip item)
        {
            bool durum = false;
            if (item.EskiId != "")
            {


                durum = Sil(item.EskiId);

                if (durum == false)
                {
                    return false;
                }
            }


            using (var context= new Repo(App.baglantiDB))
            {
                context.IsgucuKayips.Add(item);
                context.SaveChanges();
            }
            return true;

        }
        
        public bool Sil(string _id)
        {
            using (var context = new Repo(App.baglantiDB))
            {

                var sonuc = (from s in context.IsgucuKayips where s.Id == _id select s).FirstOrDefault();

                if(sonuc != null)
                {
                    var sonuc2 = from s2 in context.MaasIsGucus where s2.dosyaId== _id select s2;
                    foreach(var t2 in sonuc2)
                    {
                        context.MaasIsGucus.Remove(t2);
                    }

                    var sonuc3 = from s3 in context.MasrafIsgucus where s3.dosyaId == _id select s3;
                    foreach(var t3 in sonuc3)
                    {
                        context.MasrafIsgucus.Remove(t3);
                    }

                    var sonuc4 = from s4 in context.YakinIsgucus where s4.dosyaId == _id select s4;
                    foreach(var t4 in sonuc4)
                    {
                        context.YakinIsgucus.Remove(t4);
                    }

                    var sonuc5 = from s5 in context.KayipOrans where s5.dosyaId == _id select s5;
                    foreach(var t5 in sonuc5)
                    {
                        context.KayipOrans.Remove(t5);
                    }

                }


                try
                {
                    context.IsgucuKayips.Remove(sonuc);
                    context.SaveChanges();
                }catch(Exception ex)
                {

                }

          
            }
                return true;
        }


            // Kayıt Silme
            public async Task<bool> DeleteItemAsync(string id)
        {

            using (var context = new Repo(App.baglantiDB))
            {


                var silMasraf = await (from s1 in context.MasrafIsgucus where s1.dosyaId == id select s1).ToListAsync().ConfigureAwait(false);

                var silMasrafL = silMasraf.ToList();

                foreach (var t1 in silMasrafL)
                {
                    context.MasrafIsgucus.Remove(t1);
                }

                var silKayip = await (from s2 in context.KayipOrans where s2.dosyaId == id select s2).ToListAsync().ConfigureAwait(false);
                var silKayipL = silKayip.ToList();
                foreach (var t2 in silKayipL)
                {


                    context.KayipOrans.Remove(t2);
                }

                var silMaas = await (from s3 in context.MaasIsGucus where s3.dosyaId == id select s3).ToListAsync().ConfigureAwait(false);

                var silMaasL = silMaas.ToList();
                foreach (var t3 in silMaasL)
                {
                    context.MaasIsGucus.Remove(t3);
                }

                var silYakin = await (from s4 in context.YakinIsgucus where s4.dosyaId == id select s4).ToListAsync().ConfigureAwait(false);

                var silYakinL = silYakin.ToList();


                foreach (var t4 in silYakinL)
                {
                    context.YakinIsgucus.Remove(t4);
                }

                var sonuc = await (from s in context.IsgucuKayips where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                if (sonuc != null)
                {
                    context.IsgucuKayips.Remove(sonuc);
                }

                await context.SaveChangesAsync().ConfigureAwait(false);

                return true;

            }
        }


        // Tek Kayıt....
        public async Task<IsgucuKayip> GetItemAsync(string id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.IsgucuKayips where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                return sonuc;
            }
        }


        //Kayıt Listesi
        public async Task<List<IsgucuKayip>> GetItemSAsync()
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.IsgucuKayips select s).ToListAsync().ConfigureAwait(false);

                return sonuc;

            }
        }


        // Güncelleme

        public async Task<bool> UpdateItem(IsgucuKayip item)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.IsgucuKayips where s.Id == item.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                sonuc.aciklama = item.aciklama;
                sonuc.ad = item.ad;
                sonuc.askereGidisAy = item.askereGidisAy;
                sonuc.askereGidisYil = item.askereGidisYil;
                sonuc.askerlikSuresi = item.askerlikSuresi;
                sonuc.askerlikYapti = item.askerlikYapti;
                sonuc.calismaDurumu = item.calismaDurumu;
                sonuc.cinsiyet = item.cinsiyet;
                sonuc.dogumTarihi = item.dogumTarihi;
                sonuc.emeklilikYasi = item.emeklilikYasi;
                sonuc.hastaneAciklama = item.hastaneAciklama;
                sonuc.hastaneCikisTarihi = item.hastaneCikisTarihi;
                sonuc.hastaneYatisi = item.hastaneYatisi;
                sonuc.hastaneYatisTarihi = item.hastaneYatisTarihi;
                sonuc.kayipOrani = item.kayipOrani;
                sonuc.kazaTarihi = item.kazaTarihi;
                sonuc.kusurOrani = item.kusurOrani;
                sonuc.meslek = item.meslek;
                sonuc.meslekAciklama = item.meslekAciklama;
                sonuc.paylastirmaTur = item.paylastirmaTur;
                sonuc.raporTarihi = item.raporTarihi;
                sonuc.soyad = item.soyad;
                sonuc.trafikKazasi = item.trafikKazasi;
                sonuc.yasamTablosu = item.yasamTablosu;
                sonuc.yasiYuvarla = item.yasiYuvarla;

                // Kişi Yakınları

                var sonuc2 = await (from s2 in context.YakinIsgucus where s2.dosyaId == item.Id select s2).ToListAsync().ConfigureAwait(false);

                var sonuc2L = sonuc2.ToList();

                foreach (var t2 in item.IsGucuKayipYakinlar)
                {

                    var kayit1 = sonuc2.Find(o => o.Id == t2.Id);
                    if (kayit1 == null)
                    {
                        // Yeni Kayıt Ekleme

                        t2.dosyaId = item.Id;
                        t2.IsgucuKayip = item;
                        context.YakinIsgucus.Add(t2);

                    }
                    else
                    {
                        // Kayıt Güncelleme

                        kayit1.ad = t2.ad;
                        kayit1.anneBabaMaasDurumu = t2.anneBabaMaasDurumu;
                        kayit1.cinsiyet = t2.cinsiyet;
                        kayit1.dogumTarihi = t2.dogumTarihi;
                        kayit1.esCalisiyorMu = t2.esCalisiyorMu;
                        kayit1.esEvlenmeDurumAyim = t2.esEvlenmeDurumAyim;
                        kayit1.esEvlenmeDurumMoser = t2.esEvlenmeDurumMoser;
                        kayit1.esEvlenmeDurumStaauffer = t2.esEvlenmeDurumStaauffer;
                        kayit1.meslek = t2.meslek;
                        kayit1.meslekAciklama = t2.meslekAciklama;
                        kayit1.okulBitisYas = t2.okulBitisYas;
                        kayit1.okumaDurum = t2.okumaDurum;
                        kayit1.soyad = t2.soyad;

                        kayit1.yakinlik = t2.yakinlik;

                    }

                }

                foreach (var t2b in sonuc2L)
                {

                    var kayit2 = item.IsGucuKayipYakinlar.ToList().Find(o => o.Id == t2b.Id);
                    
                        if(kayit2 == null)
                    {
                        context.YakinIsgucus.Remove(t2b);
                    }

                }


                // Kişi  Kayipları

                var sonuc3 = await (from s3 in context.KayipOrans where s3.dosyaId == item.Id select s3).ToListAsync().ConfigureAwait(false);
                
                var sonuc3L = sonuc3.ToList();

                foreach(var t3 in item.IsGucuKayipOranlar)
                {
                    var kayit1 = sonuc3.Find(o => o.id == t3.id);

                    if(kayit1 == null)
                    {
                        // yeni  kayit ekleme
                        t3.dosyaId = item.Id;
                        t3.IsgucuKayip = item;

                        context.KayipOrans.Add(t3);


                    }else
                    {
                        // Kayıt Güncelleme

                        kayit1.aciklama = t3.aciklama;
                        kayit1.aciklama2 = t3.aciklama2;
                        kayit1.baslangicTarihi = t3.baslangicTarihi;
                        kayit1.cikisTarihi = t3.cikisTarihi;
                        kayit1.kayipOrani = t3.kayipOrani;               

                    }


                }


                foreach(var t3b in sonuc3L)
                {
                    var kayit2 = item.IsGucuKayipOranlar.ToList().Find(o => o.id == t3b.id);
                    if(kayit2 == null)
                    {
                        context.KayipOrans.Remove(t3b);
                    }
                }


                // Masraflar

                var sonuc4 = await (from s4 in context.MasrafIsgucus where s4.dosyaId == item.Id select s4).ToListAsync().ConfigureAwait(false);

                var sonuc4L = sonuc4.ToList();

                foreach(var t4 in item.IsGucuKayipMasraflar)
                {
                    var kayit1 = sonuc4.Find(o => o.Id == t4.Id);
                    if(kayit1==  null)
                   {
                        //Ekleme

                        t4.dosyaId = item.Id;
                        t4.IsgucuKayip = item;
                        context.MasrafIsgucus.Add(t4);


                    }
                    else
                    {
                        // Güncelleme

                        kayit1.ekBilgi1 = t4.ekBilgi1;
                        kayit1.ekBilgi2 = t4.ekBilgi2;
                        kayit1.masrafTur1 = t4.masrafTur1;
                        kayit1.masraftur2 = t4.masraftur2;
                        kayit1.miktar = t4.miktar;
                        kayit1.odemeTur = t4.odemeTur;
                        kayit1.tarihBas = t4.tarihBas;
                        kayit1.tarihBit = t4.tarihBit;
                       


                    }

                }

                foreach(var t4b in sonuc4L)
                {
                    var kayit2 = item.IsGucuKayipMasraflar.ToList().Find(o => o.Id == t4b.Id);
                    if(kayit2 == null)
                    {
                        context.MasrafIsgucus.Remove(t4b);

                    }
                }



                // Maaşlar

                var sonuc5 = await (from s5 in context.MaasIsGucus where s5.dosyaId == item.Id select s5).ToListAsync().ConfigureAwait(false);

                var sonuc5L = sonuc5.ToList();

                foreach(var t5 in item.IsGucuKayipMaaslar)
                {
                    var kayit1 = sonuc5.Find(o => o.Id == t5.Id);

                    if(kayit1 == null)
                    {
                        // Yeni Ekleme
                        t5.dosyaId = item.Id;
                        t5.IsgucuKayip = item;

                        context.MaasIsGucus.Add(t5);


                    }
                    else
                    {
                        // Güncelleme
                        kayit1.brutMaas = t5.brutMaas;
                        kayit1.ekBilgi1 = t5.ekBilgi1;
                        kayit1.ekBilgi2 = t5.ekBilgi2;
                        kayit1.ekBilgi3 = t5.ekBilgi3;
                        kayit1.ekBilgi4 = t5.ekBilgi4;
                        kayit1.netMaas = t5.netMaas;
                        kayit1.yil = t5.yil;
                        kayit1.yilBas = t5.yilBas;
                        kayit1.yilBit = t5.yilBit;
                        kayit1.yilBolum = t5.yilBolum;

                    }
                }

                foreach(var t5b in sonuc5L)
                {
                    var kayit2 = item.IsGucuKayipMaaslar.ToList().Find(o => o.Id == t5b.Id);

                    if(kayit2== null)
                    {
                        context.MaasIsGucus.Remove(t5b);
                    }
                }




                try
                {
                    context.SaveChanges(false);
                }
                catch (Exception ex)
                {

                }

                return true;
            }

        }

        // Kişi Kayıtları
        public async Task<IEnumerable<IsgucuKayip>> KisiKayitlari(string kisiId, bool forceRefresh =false)
        {
            using(var context= new Repo(App.baglantiDB))
            {

                var sonuc = await (from s in context.IsgucuKayips where s.musteriId == kisiId select s).ToListAsync().ConfigureAwait(false);


                var sonuc2 = await (from ss in context.IsgucuKayips  select ss).ToListAsync().ConfigureAwait(false);
                
                var kayiplar = await (from s in context.MasrafIsgucus select s).ToListAsync().ConfigureAwait(false);
                var masraflar = await (from s2 in context.MaasIsGucus select s2).ToListAsync().ConfigureAwait(false);
                var oranlar = await (from s3 in context.KayipOrans select s3).ToListAsync().ConfigureAwait(false);
                var maaslar = await (from s4 in context.MaasIsGucus select s4).ToListAsync().ConfigureAwait(false);

                return sonuc;

            }
      
        }


        }

    }


