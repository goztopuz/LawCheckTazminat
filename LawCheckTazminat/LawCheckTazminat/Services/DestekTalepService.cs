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
         
     //async  public Task<ObservableCollection<DestekYanit>> CevaplariAl(List<IdBilgi> liste)
     //   {

     //       var sonuc =await ApidanCevaplariCek(liste);
     //       await App.Current.MainPage.DisplayAlert("Hata3", sonuc.ToString(), "Tamam");

     //       return sonuc.ToObservableCollection();
     //   }
        public async Task<ObservableCollection<DestekYanit>> ApidanCevaplariCek(List<IdBilgi>liste)
        {
            ObservableCollection<DestekYanit> sonuc = new ObservableCollection<DestekYanit>();
            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var icerik = new StringContent(JsonConvert.SerializeObject(liste),
                Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url + "sorularyanitlar", icerik);

            var mobileResult = await result.Content.ReadAsStringAsync();

            try
            {
            var     sonucT = JsonConvert.DeserializeObject<List<DestekYanit>>(mobileResult);
                //   await App.Current.MainPage.DisplayAlert("Hata1", sonuc.ToString(), "Tamam");

                int sayac = 0;
                foreach(var t in sonucT)
                {
                    sayac = sayac + 1;
                    sonuc.Add(t);
//                   await App.Current.MainPage.DisplayAlert(sayac.ToString(), t.soru, "Tamam");

                }

            }
            catch (Exception ex)
            {

               // await App.Current.MainPage.DisplayAlert("Hata2", ex.ToString(), "Tamam");

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
