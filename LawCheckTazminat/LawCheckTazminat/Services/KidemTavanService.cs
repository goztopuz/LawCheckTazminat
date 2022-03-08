using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using LawCheckTazminat.Models;
using Syncfusion.Data.Extensions;

namespace LawCheckTazminat.Services
{
    public class KidemTavanService
    {
        
        public void Ekle(KidemTavan kt)
        {
            using (var context= new Repo(App.baglantiDB))
            {
                context.kidemTavans.Add(kt);
                context.SaveChanges();
            }
        }
        public void Duzenle(KidemTavan kt)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.kidemTavans 
                             where s.Id == kt.Id select s).FirstOrDefault();
                sonuc.miktar = kt.miktar;

                context.SaveChanges();

            }
        }

        public ObservableCollection<KidemTavan> Listele()
        {

            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = from s in context.kidemTavans orderby s.bitis descending select s  ;

                return sonuc.ToObservableCollection();
            }

            
        }


        public void Sil(string idd)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                var sonuc = (from s in context.kidemTavans where s.Id == idd select s).FirstOrDefault();
                if(sonuc != null)
                {

                    context.kidemTavans.Remove(sonuc);

                    context.SaveChanges();
                }
            }
        }


    }
}
