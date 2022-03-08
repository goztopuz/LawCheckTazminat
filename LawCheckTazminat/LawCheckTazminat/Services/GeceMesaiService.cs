using System;
using System.Threading.Tasks;
using LawCheckTazminat.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Syncfusion.Data.Extensions;
namespace LawCheckTazminat.Services
{
    public class GeceMesaiService
    {

        // Ekle
        public bool Ekle(GeceCalisma gcc)
        {

            bool durum = false;
            if (gcc.eskiId != "")
            {


                durum = Sil(gcc.eskiId);

                if (durum == false)
                {
                    return false;
                }

            }
            using (var context = new Repo(App.baglantiDB))
            {
                context.geceCalismas.Add(gcc);
                context.SaveChanges();
            }

            return true;

        }


        //Sil
        public bool Sil(string _id)
        {
    
            using (var context = new Repo(App.baglantiDB))
            {

                var sonuc = (from s in context.geceCalismas where s.Id == _id select s).FirstOrDefault();
                if(sonuc != null)
                {

                    var sonuc2 = (from s2 in context.maasGeceMesais select s2);
                    foreach(var t in sonuc2)
                    {
                        context.maasGeceMesais.Remove(t);
                    }

                    var sonuc3 = from s3 in context.geceMesaiKisiIzinKayitlaris select s3;
                    foreach(var t2 in sonuc3)
                    {
                        context.geceMesaiKisiIzinKayitlaris.Remove(t2);
                    }

                    var sonuc4 = from s4 in context.geceMesaiHaftalars select s4;
                    foreach(var t3 in sonuc4)
                    {
                        context.geceMesaiHaftalars.Remove(t3);
                    }


                    var sonu5 = from s5 in context.geceDusecekTarihler select s5;
                    foreach(var t5 in sonu5)
                    {
                        context.geceDusecekTarihler.Remove(t5);
                    }

                    context.geceCalismas.Remove(sonuc);

                    context.SaveChanges();

                }
            }


            return true;
        }


        //Kişi Kayıtları
        public List<GeceCalisma> GetItemsbyKisiId(string _kisiId)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.geceCalismas where s.KisiId == _kisiId select s).ToList();

                return sonuc;
            }
        }


        // Tek Kayit Seç

        public GeceCalisma TekAl(string idd)
        {
            GeceCalisma kayit = new GeceCalisma();

            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.geceCalismas where s.Id == idd select s).FirstOrDefault();


                var sonuc2 = (from s2 in context.geceMesaiHaftalars where s2.GeceMesaiId == idd select s2);

                var sonuc3 = from s3 in context.geceMesaiKisiIzinKayitlaris where s3.GeceMesaiId == idd select s3;

                var sonuc4 = from s4 in context.maasGeceMesais where s4.GeceMesaiId == idd select s4;

                var sonuc5 = from s5 in context.geceDusecekTarihler where s5.GeceMesaiId == idd select s5;


                kayit = sonuc;

                kayit.HaftalarBilgi=sonuc2.ToObservableCollection();
                kayit.IzinKaytilariBilgi = sonuc3.ToObservableCollection();
                kayit.MaasBilgi = sonuc4.ToObservableCollection();
                kayit.GeceDusecekTarihler = sonuc5.ToObservableCollection();


                return kayit;
            }
        }


      
    }
}
