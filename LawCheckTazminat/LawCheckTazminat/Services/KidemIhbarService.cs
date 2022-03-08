using System;
using System.Linq;
using System.Collections.Generic;
using LawCheckTazminat.Models;
using Syncfusion.Data.Extensions;
namespace LawCheckTazminat.Services
{
    public class KidemIhbarService
    {
        //EKLE

        public bool AddItem(KidemIhbar kd)
        {
            //Önce Eskisini Silme

            if (kd.eskId != "")
            {
                bool durum = false;

                durum = Sil(kd.eskId);
                if (durum == false)
                {
                    return false;
                }
            }

            using (var context = new Repo(App.baglantiDB))
            {
                context.kidemIhbars.Add(kd);
                context.SaveChanges();

            }
            return true;
        }

        // Get ByKisi

        public List<KidemIhbar> KidemListeByKisi(string _kisiId)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var liste = (from s in context.kidemIhbars where s.kisiId == _kisiId select s).ToList();

                return liste;
            }

        }

        // GetById

        public KidemIhbar GetItem(string _id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var kayit = (from s in context.kidemIhbars where s.Id == _id select s).FirstOrDefault();
                return kayit;
            }

        }



        // Sil
        public bool  Sil(string _id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.kidemIhbars where s.Id == _id select s).FirstOrDefault();
                if(sonuc != null)
                {
                    context.kidemIhbars.Remove(sonuc);
                    context.SaveChanges();
                return true;

                }else
                {
                    return true;

                }

            }
        }


    }
}
