using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NetTrack.ViewModels
{
    public class NewContactViewModel : BaseViewModel
    {
        public string firstname;
        public string lastname;
        public string email;
        public string phone;
        public string message;
        public bool primary;

        public NewContactViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(firstname)
                && !String.IsNullOrWhiteSpace(lastname)
                && !String.IsNullOrWhiteSpace(email)
                && !String.IsNullOrWhiteSpace(phone)
                && !String.IsNullOrWhiteSpace(message);
        }

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

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        
        private async void OnSave()
        {
            Contact newContact = new Contact()
            {
                id = Guid.NewGuid().ToString(),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Phone = Phone,
                Message = Message,
                Primary = Primary
            };

            await DataStore.AddItemAsync(newContact);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }        
    }
}
