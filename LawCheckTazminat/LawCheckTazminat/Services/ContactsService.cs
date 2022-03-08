using LawCheckTazminat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LawCheckTazminat.Services
{
   public class ContactsService
    {
        public async Task<bool> AddItemAsync(Contact item)
        {
            using(var context = new Repo(App.baglantiDB))
            {
                await context.Contacts.AddAsync(item).ConfigureAwait(false);
                await  context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }         
        }

        public async Task<bool> DeleteItemAsync(string id)
        {            
            using(var context= new Repo(App.baglantiDB))
            {
                var itemToRemove = await (from s in context.Contacts where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);
           
                if(itemToRemove != null)
                {
                    context.Contacts.Remove(itemToRemove);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }
            
            }
            return true;

        }

        public async Task<Contact> GetItemAsync(string id)
        {

            using(var context= new Repo(App.baglantiDB))
            {
                //var item = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
                //return item;
                var sonuc =await (from s in context.Contacts where s.Id == id select s).FirstOrDefaultAsync().ConfigureAwait(false);

                return sonuc;
            }
         
        }

        public async Task<IEnumerable<Contact>> GetItemsAsync(bool forceRefresh = false)
        {

            using( var context = new Repo(App.baglantiDB))
            {
                var allItems = await context.Contacts.ToListAsync().ConfigureAwait(false);
                return allItems;
            }

        }

        public async Task<bool> UpdateItemAsync(Contact item)
        {
            using(var context= new Repo(App.baglantiDB))
            {
                context.Contacts.Update(item);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }

         
        }
  
    }
}
