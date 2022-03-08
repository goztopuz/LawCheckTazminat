using System;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace LawCheckTazminat.Services
{
    public class VergiDilimlerService
    {

        public async Task<bool> AddItemAsync(VergiDilimleri vD)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                await context.vergiDilimleris.AddAsync(vD).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }
        
        public bool UpdateItemAsync(VergiDilimleri Vd)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.vergiDilimleris where s.Id == Vd.Id select s).FirstOrDefault();

                if(sonuc != null)
                {
                    sonuc.Vd1Miktar = Vd.Vd1Miktar;
                    sonuc.Vd1Yuzde = Vd.Vd1Yuzde;
                    sonuc.Vd2Miktar = Vd.Vd2Miktar;
                    sonuc.Vd2Yuzde = Vd.Vd2Yuzde;
                    sonuc.Vd3Miktar = Vd.Vd3Miktar;
                    sonuc.Vd3Yuzde = Vd.Vd3Yuzde;
                    sonuc.Vd4Miktar = Vd.Vd4Miktar;
                    sonuc.VdX1Miktar = Vd.VdX1Miktar;
                    sonuc.VdX1Yuzde = Vd.VdX1Yuzde;
                    sonuc.VdX2Miktar = Vd.VdX2Yude;
                    sonuc.VdX2Yude = Vd.VdX2Yude;

                   }

                return true;
            }
        }

        public void DeleteVergiDilim(int idd)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.vergiDilimleris where s.Id == idd select s).FirstOrDefault();
                if(sonuc != null)
                {
                    context.vergiDilimleris.Remove(sonuc);
                    context.SaveChanges();
                }
            }
        }

        public List<VergiDilimleri> GetItems()
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.vergiDilimleris select s).ToList();

                return sonuc;
            }
        }


        public VergiDilimlerService()
        {
        }
    }
}
