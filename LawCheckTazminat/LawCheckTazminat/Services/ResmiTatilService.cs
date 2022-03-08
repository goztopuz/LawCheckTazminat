using System;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LawCheckTazminat.Services

{
    public class ResmiTatilService
    {



        // Yeni Kayıt Ekleme...
        public async Task<bool> AddItemAsync(ResmiTatiller item)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                await context.ResmiTatillers.AddAsync(item).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

        }
        
        // Kayıtları Listeleme
        public async Task<ResmiTatiller> GetItem(string id)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.ResmiTatillers where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                return sonuc;
            }
        }

        public async Task<List<ResmiTatiller>> GetItemByYil(int yill)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.ResmiTatillers where s.yil == yill select s).ToListAsync().ConfigureAwait(false);

                return sonuc;
            }
        }


        public async Task<List<ResmiTatiller>> GetItemS()
        {

            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.ResmiTatillers select s).ToListAsync().ConfigureAwait(false);
                return sonuc;

            }

        }

        public List<ResmiTatiller> GetItemss()
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = from s in context.ResmiTatillers select s;
                return sonuc.ToList();
            }
        }


        // Silme
        public async Task<bool> DeleteItem(string idd)
        {
            using (var context = new Repo(App.baglantiDB))

            {
                var sonuc =await (from s in context.ResmiTatillers where s.Id == idd select s).FirstOrDefaultAsync().ConfigureAwait(false);

                if(sonuc != null)
                {
                    context.ResmiTatillers.Remove(sonuc);

                    context.SaveChanges();
                        return true;                       
                }
                else
                {
                    return false;
                }
            }


        }


        // Güncelleme
        public async Task UpdateItem(ResmiTatiller tt)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.ResmiTatillers where s.Id == tt.Id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                if(sonuc != null)
                {
                    sonuc.aciklama = tt.aciklama;
                    sonuc.ekbilgi = tt.ekbilgi;
                    sonuc.ekBilgi1 = tt.ekBilgi1;
                    sonuc.ekBilgi2 = tt.ekBilgi2;
                    sonuc.ekBilgi3 = tt.ekBilgi3;
                    sonuc.ekBilgi4 = tt.ekBilgi4;
                    sonuc.ekBilgi5 = tt.ekBilgi5;
                    sonuc.ekBilgi6 = tt.ekBilgi6;
                    sonuc.tam = tt.tam;
                    sonuc.tarih = tt.tarih;
                    sonuc.tur = tt.tur;
                    sonuc.yil = tt.yil;

                    context.SaveChanges();

                }
            }
        }


      
    }
}
