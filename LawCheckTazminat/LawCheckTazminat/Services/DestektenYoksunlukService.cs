using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Security.Cryptography;

namespace LawCheckTazminat.Services
{
  public  class DestektenYoksunlukService
    {

        public async Task<bool> AddItemAsync(DestektenYoksunluk item)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                await context.DestektenYoksunluks.AddAsync(item).ConfigureAwait(false);
       
                    
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var itemToRemove = await (from s in context.DestektenYoksunluks
                                          where s.Id== id select s).FirstOrDefaultAsync().ConfigureAwait(false);
                if (itemToRemove != null)
                {
                    context.DestektenYoksunluks.Remove(itemToRemove);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

            }
            return true;

        }


        public bool DeleteDestektenYoksunluk(string _id)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc1 = (from s in context.Yakins where s.dosyaId == _id select s).ToList();
                var sonucL = sonuc1.ToList();
                foreach(var t in sonucL)
                {
                    context.Yakins.Remove(t);                        
                }

                context.SaveChanges();
                }

            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc2 = (from s in context.Masrafs where s.dosyaId == _id select s).ToList();
                var sonucL = sonuc2.ToList();
                foreach(var t in sonucL)
                {
                    context.Masrafs.Remove(t);
                }
                context.SaveChanges();
            }

            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc3 = (from s in context.MaasS where s.dosyaId == _id select s).ToList();
                var sonucL = sonuc3.ToList();

                foreach(var t in sonucL)
                {
                    context.MaasS.Remove(t);
                }

                var sonucc = (from ss in context.DestektenYoksunluks where ss.Id == _id select ss).FirstOrDefault();
                if(sonucc != null)
                {
                    context.DestektenYoksunluks.Remove(sonucc);
                }
                context.SaveChanges();
            }

          
            return true;
        }

        public async Task<DestektenYoksunluk> GetItemAsync(string id)
        {

            using (var context = new Repo(App.baglantiDB))
            {
     
                var sonuc = await (from s in context.DestektenYoksunluks where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                return sonuc;
            }

        }

        public async Task<IEnumerable<DestektenYoksunluk>> GetItemsAsync(bool forceRefresh = false)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var allItems = await context.DestektenYoksunluks.ToListAsync().ConfigureAwait(false);
                return allItems;
            }

        }

        public async Task<bool> UpdateItemAsync(DestektenYoksunluk item)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc1 =await (from s in context.DestektenYoksunluks  where s.Id == item.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);

               
                sonuc1.aciklama = item.aciklama;
                sonuc1.ad = item.ad;
                sonuc1.AgiDus = item.AgiDus;
                sonuc1.askereGidisAy = item.askereGidisAy;
                sonuc1.askereGidisYil = item.askereGidisYil;
                sonuc1.askerlikSuresi = item.askerlikSuresi;
                sonuc1.askerlikYapti = item.askerlikYapti;




                sonuc1.BearCocukCocukDestekCikisYasi = item.BearCocukCocukDestekCikisYasi;
                sonuc1.BekarCocukCocuklarArasiYil = item.BekarCocukCocuklarArasiYil;
                sonuc1.BekarCocukCocukSayisi = item.BekarCocukCocukSayisi;
                sonuc1.BekarCocukDurum = item.BekarCocukDurum;
                sonuc1.BekarCocukEvlenmeYas = item.BekarCocukEvlenmeYas;
                sonuc1.BekarCocuk_18OncesiMaas = item.BekarCocuk_18OncesiMaas;
                sonuc1.BekarCocuk_CalismaBasTarihi = item.BekarCocuk_CalismaBasTarihi;
                sonuc1.BekarCocuk_CalismaBasYasi = item.BekarCocuk_CalismaBasYasi;
                sonuc1.BekarCocuk_EvlenmeTarihi = item.BekarCocuk_EvlenmeTarihi;
                sonuc1.BekarCocuk_GelecekCalismaUcreti = item.BekarCocuk_GelecekCalismaUcreti;
                sonuc1.BekarCocuk_SuAnCalisiyor = item.BekarCocuk_SuAnCalisiyor;
                sonuc1.BekarCocuk_SuAnkiUcret = item.BekarCocuk_SuAnkiUcret;
                sonuc1.BekarCouckCocukIlkSene = item.BekarCouckCocukIlkSene;




                sonuc1.calismaDurumu = item.calismaDurumu;
                sonuc1.cinsiyet = item.cinsiyet;
                sonuc1.dogumTarihi = item.dogumTarihi;
                sonuc1.Duzenlemede = item.Duzenlemede;
                sonuc1.emekliMaas = item.emekliMaas;
                sonuc1.emeklilikYasi = item.emeklilikYasi;
                sonuc1.esEvlenmeElle = item.esEvlenmeElle;
                sonuc1.esEvlenmeElleHesapla = item.esEvlenmeElleHesapla;
                sonuc1.esEvlenmeYuzdesi = item.esEvlenmeYuzdesi;
                sonuc1.isleyecekMaas = item.isleyecekMaas;
                sonuc1.kusurOrani = item.kusurOrani;
                sonuc1.meslek = item.meslek;
                sonuc1.meslekAciklama = item.meslekAciklama;
                sonuc1.paylastirmaTur = item.paylastirmaTur;
                sonuc1.raporTarihi = item.raporTarihi;
                sonuc1.soyad = item.soyad;
                sonuc1.trafikKazasi = item.trafikKazasi;
                sonuc1.vefatEden = item.vefatEden;
                sonuc1.vefatTarihi = item.vefatTarihi;
                sonuc1.yasamTablosu = item.yasamTablosu;
                sonuc1.yasiYuvarla = item.yasiYuvarla;

                sonuc1.trafikPMF = item.trafikPMF;
                sonuc1.trafikTRh = item.trafikTRh;


                  
             // YAKIN BİLGİLERİ GÜNCELLEME
                var sonuc2 =await( from s2 in context.Yakins where s2.dosyaId == item.Id select s2).ToListAsync().ConfigureAwait(false);

                var sonuc2L = sonuc2.ToList();
                foreach(var t2 in item.DestekYoksunlukYakinlar)
                {

                    var kayit1 = sonuc2.Find(o => o.Id == t2.Id);
                    if(kayit1 == null)
                    {
                        //Ekle
                        t2.dosyaId = sonuc1.Id;
                        t2.DestektenYoksunluk = sonuc1;
                        context.Yakins.Add(t2);

                        
                    }
                    else
                    {
                        // Güncelle
                        kayit1.ad = t2.ad;
                        kayit1.anneBabaMaasDurumu = t2.anneBabaMaasDurumu;
                        kayit1.cinsiyet = t2.cinsiyet;
                        kayit1.dogumTarihi = t2.dogumTarihi;
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

                foreach(var t2b in sonuc2L)
                {
                    var kayit2 = item.DestekYoksunlukYakinlar.ToList().Find(o => o.Id == t2b.Id);
                    if(kayit2 == null)
                    {
                        context.Yakins.Remove(t2b);
                    }
                }

                // MASRAF BİLGİLERİ GÜNCELLEME....

                var sonuc3 = await (from s3 in context.Masrafs where s3.dosyaId == item.Id select s3).ToListAsync().ConfigureAwait(false);

                var sonuc3L = sonuc3.ToList();

                foreach(var t3 in item.DosyaDestektenYoksunlukMasraf)
                {
                    var kayit1b = sonuc3.Find(o => o.Id == t3.Id);

                    if(kayit1b == null)
                    {
                        t3.dosyaId = sonuc1.Id;
                        t3.DestektenYoksunluk = sonuc1;
                        context.Masrafs.Add(t3);
                    }
                    else
                    {
                        kayit1b.ekBilgi1 = t3.ekBilgi1;
                        kayit1b.ekBilgi2 = t3.ekBilgi2;
                        kayit1b.masrafTur1 = t3.masrafTur1;
                        kayit1b.masraftur2 = t3.masraftur2;
                        kayit1b.miktar = t3.miktar;
                        kayit1b.odemeTur = t3.odemeTur;
                        kayit1b.tarihBas = t3.tarihBas;
                        kayit1b.tarihBit = t3.tarihBit;
                        
                    }
                }


                foreach(var t3b in sonuc3L)
                {
                    var kayit3 = item.DosyaDestektenYoksunlukMasraf.ToList().Find(o => o.Id == t3b.Id);

                    if(kayit3 == null)
                    {
                        context.Masrafs.Remove(t3b);
                    }
                          
                }


                // MAAŞ BİLGİLERİ GÜNCELLE

                var sonuc4 = await (from s4 in context.MaasS where s4.dosyaId == item.Id select s4).ToListAsync().ConfigureAwait(false);

                var sonuc4L = sonuc4.ToList();

                foreach(var t4 in item.DestektekYoksunlukMaaslar)
                {
                    var kayit1c = sonuc4.Find(o => o.Id == t4.Id);
                    if(kayit1c == null)
                    {
                        t4.dosyaId = sonuc1.Id;
                        t4.DestektenYoksunluk = sonuc1;
                        context.MaasS.Add(t4);

                    }else
                    {
                        kayit1c.ekBilgi1 = t4.ekBilgi1;
                        kayit1c.ekBilgi2 = t4.ekBilgi2;
                        kayit1c.ekBilgi3 = t4.ekBilgi3;
                        kayit1c.ekBilgi4 = t4.ekBilgi4;
                        kayit1c.brutMaas = t4.brutMaas;

                        kayit1c.netMaas = t4.netMaas;
                        kayit1c.yil = t4.yil;
                        kayit1c.yilBas = t4.yilBas;
                        kayit1c.yilBit = t4.yilBit;
                        kayit1c.yilBolum = t4.yilBolum;
                       

                    }
                }
                foreach(var t4b in sonuc4L)
                {
                    var kayit4 = item.DestektekYoksunlukMaaslar.ToList().Find(o => o.Id == t4b.Id);
                    if(kayit4 == null)
                    {
                        context.MaasS.Remove(t4b);
                    }
                }




             //   context.DestektenYoksunluks.Update(item);
            //    context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             
                try
                {
                     context.SaveChanges(false);
                }
                catch(Exception ex)
                {

                }
               
                return true;
            }


        }

        public async Task<IEnumerable<DestektenYoksunluk>> GetItemsByKisiAsync(string idd,bool forceRefresh = false)
        {

            List<DestektenYoksunluk> sonuc2 = new List<DestektenYoksunluk>();
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.DestektenYoksunluks where s.musteriId == idd select s).
                    ToListAsync().ConfigureAwait(false);




             foreach(var t in sonuc)
                {
                    DestektenYoksunluk dd = t;
                    dd.DestektekYoksunlukMaaslar = t.DestektekYoksunlukMaaslar;
                    dd.DestekYoksunlukYakinlar = t.DestekYoksunlukYakinlar;
                    dd.DosyaDestektenYoksunlukMasraf = t.DosyaDestektenYoksunlukMasraf;
                    sonuc2.Add(dd);
                }


                var maaslar2 =await  (from s2 in context.MaasS select s2).ToListAsync().ConfigureAwait(false);

                var yakinlar2 = await (from s3 in context.Yakins select s3).ToListAsync().ConfigureAwait(false);

                var masraf4 = await (from s4 in context.Masrafs select s4).ToListAsync().ConfigureAwait(false);
           
                return sonuc;
                //var allItems = await context.DestektenYoksunluks.ToListAsync().ConfigureAwait(false);
                //return allItems;
            }

        }


        public async Task<bool> MaasEkle(List<Maas> maasListe, string _dosyaId)
        {

            using (var context = new Repo(App.baglantiDB))
            {

                var silList = await (from s in context.MaasS where s.dosyaId == _dosyaId select s).ToListAsync();

                foreach(var t2 in silList)
                {
                   await  MaasTekSil(t2);
                }

                foreach(var t in maasListe)
                {
                    await MaasTekEkle(t);

                }
             
              
                return true;


            }
        }

        private async Task<bool> MaasTekEkle(Maas m)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                context.MaasS.Add(m);
                try
                {
                    await context.SaveChangesAsync().ConfigureAwait(false);

                }catch(Exception ex)
                {

                }

                return true;
            }
        }

        private async Task<bool> MaasTekSil(Maas m)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                var sil1 = await (from s in context.MaasS where s.Id == m.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }

        public async Task<bool> YakinEkle(List<Yakin> yakinListe, string _dosyaId)
        {

            using (var context = new Repo(App.baglantiDB))
            {

                var silList = await (from s in context.Yakins where s.dosyaId == _dosyaId select s).ToListAsync();

                foreach (var t2 in silList)
                {
                    await  YakinTekSil(t2);
                }

                foreach (var t in yakinListe)
                {
                    await YakinTekEkle(t);
                }

              

                return true;


            }
        }

        private async Task<bool> YakinTekSil(Yakin yy)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                var sil1 = await (from s in context.Yakins where s.Id == yy.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }

        private async Task<bool> YakinTekEkle(Yakin yy)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                context.Yakins.Add(yy);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;

            }
        }

        private async Task<bool> MasrafTekSil(Masraf m)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sil1 = await (from s in context.Masrafs where s.Id == m.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }

        private async Task<bool> MasrafTekEkle(Masraf m)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                await context.Masrafs.AddAsync(m).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }


        public async Task<bool> MasrafEkle(List<Masraf> masrafListe, string _dosyaId)
        {
            using (var context = new Repo(App.baglantiDB))
            {

                var silList = await (from s in context.Masrafs where s.dosyaId == _dosyaId select s).ToListAsync();

                foreach (var t2 in silList)
                {
                    await MasrafTekSil(t2);
                }

                foreach (var t in masrafListe)
                {
                    await MasrafTekEkle(t);
                }

             
                return true;


            }


        }
    }
}
