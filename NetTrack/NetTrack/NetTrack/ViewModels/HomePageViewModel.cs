using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Android.Provider;
using Android.Content;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Linq;

namespace NetTrack.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public User user { get; set; }
        CancellationTokenSource cts;
        public Command AlertPressed { get; }
        private readonly HttpClient _httpClient = new HttpClient();
        public Command ItemTappedCommand { get; }

        public ObservableRangeCollection<Models.Contact> Contacts { get; set; } = new ObservableRangeCollection<Models.Contact>();
        public ObservableRangeCollection<Models.Contact> Protectees { get; set; } = new ObservableRangeCollection<Models.Contact>();

        public ObservableRangeCollection<Grouping<string, Models.Contact>> MainGroup { get; set; } = new ObservableRangeCollection<Grouping<string, Models.Contact>>();

        public AsyncCommand RefreshCommand { get; }
        public Models.Contact SelectedProtectee { get; set; }

        public Alert alert { get; set; }

        public bool trackingActive { get; set; } = false; 

        
        public HomePageViewModel()
        {
            this.user = UserStore.GetUser();
            AlertPressed = new Command(Alert);
            ItemTappedCommand = new Command(ItemTapped);

            foreach (var item in this.user.contacts)
            {
                Contacts.Add(new Models.Contact { FirstName = item.FirstName, LastName = item.LastName, Email = item.Email, Active = item.Active, UserId = item.UserId });
            }

            foreach (var item in this.user.protectees)
            {
                Protectees.Add(new Models.Contact { FirstName = item.FirstName, LastName = item.LastName, Email = item.Email, Active = item.Active, UserId = item.UserId });
            }

            MainGroup.Add(new Grouping<string, Models.Contact>("Contacts", Contacts));
            MainGroup.Add(new Grouping<string, Models.Contact>("Protectees", Contacts));

            RefreshCommand = new AsyncCommand(Refresh);
        }

        
        private async void ItemTapped()
        {

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                FireAlert();
                return true;
            });

          

            trackingActive = true;
        }
        

        private async Task<bool> FireAlert()
        {
            return await Task.Run(async () => {
                 var response = await _httpClient.GetAsync($"http://10.0.2.2:5212/alert?userId={SelectedProtectee.UserId}");

                if (!response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Oops", "An error occured on our end, please try again", "OK");
                    
                }

                var contentResponse = await response.Content.ReadAsStringAsync();
                alert = JsonConvert.DeserializeObject<Alert>(contentResponse);
                return true;
            });
        }

        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            GoogleDirection googleDirection = new GoogleDirection();
            try
            {


                var response = await _httpClient.GetAsync($"https://maps.googleapis.com/maps/api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key=AIzaSyD1l_AICyISOzs-nwRYBCvgAKvwDHMIoOc").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        googleDirection = await Task.Run(() =>
                           JsonConvert.DeserializeObject<GoogleDirection>(json)
                        ).ConfigureAwait(false);

                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return googleDirection;
        }

        internal async Task<System.Collections.Generic.List<Xamarin.Forms.Maps.Position>> LoadRoute()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            try
            {
                var googleDirection = await GetDirections(location.Latitude.ToString(), location.Longitude.ToString(), alert.Latitude.ToString(), alert.Longitude.ToString());

                if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
                {
                    var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                    return positions;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Oops", "Something went wrong.", "Ok");
                    return null;

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;

        }
        private async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            IsBusy = false;
        }

        private async void Alert(object obj)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");



                    Alert alert = new Alert();
                    alert.Latitude = location.Latitude;
                    alert.Longitude = location.Longitude;
                    alert.UserId = user.Id;

                    var jsonObject = JsonConvert.SerializeObject(alert);

                    StringContent content = new StringContent(jsonObject, UnicodeEncoding.UTF8, "application/x-www-form-urlencoded");


                    var response = await _httpClient.PostAsync("http://10.0.2.2:5212/alert", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        await Shell.Current.DisplayAlert("Oops", "An error occured on our end, please try again", "OK");
                        return;
                    }
                    
                    var photo = await TakePhotoAsync();
                    var ImgContent = new MultipartFormDataContent();
                    ImgContent.Add(new StringContent("Id"),user.Id);
                    ImgContent.Add(new StreamContent(await photo.OpenReadAsync()), "file", photo.FileName);


                    var imgResponse = await _httpClient.PostAsync("http://10.0.2.2:5212/image", ImgContent);

                    if (!imgResponse.IsSuccessStatusCode)
                    {
                        await Shell.Current.DisplayAlert("Oops", "An error occured on our end, please try again", "OK");
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // Unable to get location
            } 
            
            // Unable to get location
        }


        async Task<FileResult> TakePhotoAsync()
        {
            FileResult photo = null;
            try
            {
                photo = await MediaPicker.CapturePhotoAsync();
                Console.WriteLine(photo);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

            return photo;
        }


 }






}

