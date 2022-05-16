using NetTrack.Models;
using NetTrack.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace NetTrack.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {


        public Models.Login details { get; set; }
        private readonly HttpClient _httpClient = new HttpClient();
        public Command LoginCommand { get; }
        
        

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            details = new Models.Login();
        }

        public async void OnLoginClicked()
        {
            try
            {
                if (string.IsNullOrEmpty(this.details.email) || string.IsNullOrEmpty(this.details.password))
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", "Please enter your email and password", "OK");
                    return;
                }

                var jsonObject = JsonConvert.SerializeObject(this.details);

                StringContent content = new StringContent(jsonObject, UnicodeEncoding.UTF8, "application/json");


                var response = await _httpClient.PostAsync("http://10.0.2.2:5212/login", content);
                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", "Invalid credentials", "OK");
                    return;
                }
                var contentResponse = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(contentResponse);
                await UserStore.AddUser(user);
               
                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
