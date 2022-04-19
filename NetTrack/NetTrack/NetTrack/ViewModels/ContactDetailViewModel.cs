using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NetTrack.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ContactDetailViewModel : BaseViewModel
    {


        private string itemId;
        
        public string firstname;
        public string lastname;
        public string email;
        public string phone;
        public string message;
        public bool primary;
        public string Id { get; set; }

        public string FirstName
        {
            get => firstname;
            set => SetProperty(ref firstname, value);
        }

        public string LastName
        {
            get => lastname;
            set => SetProperty(ref lastname, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        
        public bool Primary
        {
            get => primary;
            set => SetProperty(ref primary, value);
        }


        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.id;
                FirstName = item.FirstName;
                LastName = item.LastName;
                Email = item.Email;
                Phone = item.Phone;
                Message = item.Message;
                Primary = item.Primary;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

    }
}
