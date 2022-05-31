using NetTrack.Models;
using NetTrack.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NetTrack.ViewModels
{
    public class ProfileThreeViewModel : BaseViewModel
    {
        private Contact _selectedItem;

        public ObservableCollection<Contact> Contacts { get; }
        public Command LoadContactsCommand { get; }
        public Command AddContactCommand { get; }
        public Command<Contact> ContactTapped { get; }

        private readonly HttpClient _httpClient = new HttpClient();
        public ProfileThreeViewModel()
        {
            Title = "Browse";
            Contacts = new ObservableCollection<Contact>();
            LoadContactsCommand = new Command(async () => await ExecuteLoadContacsCommand());

            ContactTapped = new Command<Contact>(OnContactSelected);

            AddContactCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadContacsCommand()
        {
            IsBusy = true;

            try
            {
                Contacts.Clear();
                var contacts = await DataStore.GetItemsAsync(true);
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        
        public Contact SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnContactSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewContactPage));
        }

        async void OnContactSelected(Contact contact)
        {
            if (contact == null)
                return;

            // This will push the ContactDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}?{nameof(ContactDetailViewModel.ItemId)}={contact.id}");
        }
        

        public async Task Regiter(User user)
        {
            try
            {
                var jsonObject = JsonConvert.SerializeObject(user);

                StringContent content = new StringContent(jsonObject, UnicodeEncoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("https://nettrackapi.azurewebsites.net/register", content);
                var contentResponse = await response.Content.ReadAsStringAsync();
                var RegisteredUser = JsonConvert.DeserializeObject<User>(contentResponse);
                await UserStore.AddUser(RegisteredUser);
                if (!response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Oops", "An error occured on our end, please try again", "OK");
                    return;
                }
                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
      
        }

        
    }
}
