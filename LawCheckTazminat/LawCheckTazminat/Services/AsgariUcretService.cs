using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XForms.AvatarView;

namespace LawCheckTazminat.Services
{
    public class AsgariUcretService
    {

        public async Task<bool> AddItemAsync(AsgariUcretTablosu item)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                await context.AsgariUcretTablosus.AddAsync(item).ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var itemToRemove = await (from s in context.AsgariUcretTablosus where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                if (itemToRemove != null)
                {
                    context.AsgariUcretTablosus.Remove(itemToRemove);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

            }
            return true;

        }
        
        public async Task<IEnumerable<AsgariUcretTablosu>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                //var allItems = await context.AsgariUCretTablosus.ToListAsync().ConfigureAwait(false);
                //return allItems;

                var sonuc = await (from s in context.AsgariUcretTablosus orderby s.yil descending select s).ToListAsync().ConfigureAwait(false);
                return sonuc;        
            }
        }

        public  List<AsgariUcretTablosu> GetItems2()
        {
            using(var context= new Repo(App.baglantiDB))
                {

                var sonuc2 = (from s in context.AsgariUcretTablosus select s).ToList();
                return sonuc2;
            }
        }

        public async Task<bool> UpdateItemAsync(AsgariUcretTablosu item)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                context.AsgariUcretTablosus.Update(item);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }


        }

        public async Task<AsgariUcretTablosu> GetItemAsync(string id)
        {

            using (var context = new Repo(App.baglantiDB))
            {
                //var item = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                //return item;
                var sonuc = await (from s in context.AsgariUcretTablosus where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                return sonuc;
            }

        }


        public async Task<int> AsgariUcret_MevcutKontrol(string donem)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.AsgariUcretTablosus where s.yil == donem select s).FirstOrDefaultAsync().ConfigureAwait(false);
           
            if(sonuc== null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
               }
        }

        public async Task<int> AsgariUcret_MevcutKontro2l(string donem, string idd)
        {
            using (var context = new Repo(App.baglantiDB))
            {
                var sonuc = await (from s in context.AsgariUcretTablosus where s.yil == donem select s).FirstOrDefaultAsync().ConfigureAwait(false);

                if (sonuc == null)
                {
                    return 0;
                }
                else
                {
                    //return 1;
                    if(sonuc.Id != idd)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
        }
    
    }
}
