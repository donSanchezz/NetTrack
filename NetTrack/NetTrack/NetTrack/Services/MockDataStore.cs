using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetTrack.Services
{
    public class MockDataStore : IDataStore<Contact>
    {
        readonly List<Contact> contacts;
        
        public MockDataStore()
        {
            this.contacts = new List<Contact>();
        }
        
        public async Task<bool> AddItemAsync(Contact item)
        {
            contacts.Add(item);

            return await Task.FromResult(true);
        }
        
        public async Task<bool> UpdateItemAsync(Contact item)
        {
            var oldItem = contacts.Where((Contact arg) => arg.id == item.id).FirstOrDefault();
            contacts.Remove(oldItem);
            contacts.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = contacts.Where((Contact arg) => arg.id == id).FirstOrDefault();
            contacts.Remove(oldItem);
            
            return await Task.FromResult(true);
        }
        
        public async Task<Contact> GetItemAsync(string id)
        {
            return await Task.FromResult(contacts.FirstOrDefault(s => s.id == id));
        }

        public async Task<IEnumerable<Contact>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(contacts);
        }
    }
}