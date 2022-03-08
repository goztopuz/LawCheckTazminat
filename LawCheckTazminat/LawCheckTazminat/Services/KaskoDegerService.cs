using LawCheckTazminat.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LawCheckTazminat.Services
{
 public    class KaskoDegerService
    {


        public async Task<List<AracMarka>> MarkalariCek()
        {
            List<AracMarka> sonuc = new List<AracMarka>();
            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            AracMarka mm = new AracMarka();
            var icerik = new StringContent(JsonConvert.SerializeObject(mm),
                Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url + "markalaricek", icerik);

            var mobileResult = await result.Content.ReadAsStringAsync();

            try
            {
                sonuc = JsonConvert.DeserializeObject<List<AracMarka>>(mobileResult);
            }
            catch (Exception ex)
            {
            }
            return sonuc;
        }

        public async Task<List<AracModel>> ModelleriCek(AracMarka mm)
        {
            List<AracModel> sonuc = new List<AracModel>();
            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var icerik = new StringContent(JsonConvert.SerializeObject(mm),
                Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url + "modellericek", icerik);

            var mobileResult = await result.Content.ReadAsStringAsync();

            try
            {
                sonuc = JsonConvert.DeserializeObject<List<AracModel>>(mobileResult);
            }
            catch (Exception ex)
            { 
            }
            return sonuc;
        }

        public async Task<AracDeger> AracDegeriCek(AracDeger dd)
        {
            AracDeger sonuc = new AracDeger();

            HttpClient client = new HttpClient();
            string url = Constants.SunucuSabitleri.SunucuAdresi;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var icerik = new StringContent(JsonConvert.SerializeObject(dd),
                Encoding.UTF8, "application/json");
            var result = await client.PostAsync(url + "modelbilgi", icerik);

            var mobileResult = await result.Content.ReadAsStringAsync();

            try
            {
                sonuc = JsonConvert.DeserializeObject<AracDeger>(mobileResult);
            }
            catch (Exception ex)
            { }
            return sonuc;

        }



    }
}
