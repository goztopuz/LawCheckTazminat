using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Syncfusion.Data.Extensions;
using System.Net.Http;
using Newtonsoft.Json;

namespace LawCheckTazminat.Services
{
    public class DestekTalepService
    {

     async   public Task Ekle(DestekTalep _d)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                context.destekTaleps.Add(_d);
                context.SaveChanges();

              await ApiyaGonder(_d);

            }
            
        }

         public ObservableCollection<DestekTalep> SorulariAl()
        {
           using(var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.destekTaleps select s).ToObservableCollection();
                return sonuc;
            }
        }
         
     async  public Task<ObservableCollection<DestekYanit>> CevaplariAl(List<IdBilgi> liste)
        {
            var sonuc =await ApidanCevaplariCek(liste);
            return sonuc.ToObservableCollection();
        }
        private async Task<List<DestekYanit>> ApidanCevaplariCek(List<IdBilgi>liste)
        {
            List<DestekYanit> sonuc = new List<DestekYanit>();
            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var icerik = new StringContent(JsonConvert.SerializeObject(liste),
                Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url + "sorularyanitlar", icerik);

            var mobileResult = await result.Content.ReadAsStringAsync();

            try
            {
                sonuc = JsonConvert.DeserializeObject<List<DestekYanit>>(mobileResult);

            }
            catch (Exception ex)
            {

            }

            return sonuc;

        }

         private async  Task<bool> ApiyaGonder(DestekTalep _d)
        {

            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var icerik = new StringContent(JsonConvert.SerializeObject(_d), Encoding.UTF8,
                "application/json");

            var result = await client.PostAsync(url + "destekle", icerik);

            return true;
        }
        public List<DestekTalep> ListeleAcik()
        {

            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.destekTaleps where s.durum == "Açık" select s);

                return sonuc.ToList();
            }

        }

        public DestekTalepService()
        {
        }
    }
}
