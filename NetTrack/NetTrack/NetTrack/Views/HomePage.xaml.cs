using NetTrack.Models;
using NetTrack.Services;
using NetTrack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace NetTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        //public Alert Alert { get; set; } = new Alert();
        public bool tracking;
        HomePageViewModel homePageViewModel;
        public HomePage()
        {
            InitializeComponent();
            DependencyService.Get<IStatusBar>().ShowStatusBar();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = homePageViewModel = new ViewModels.HomePageViewModel();

            Device.StartTimer(TimeSpan.FromSeconds(10),  () =>
            {
                activateLottie();
                BeginTracking();
                return true;
            });
        }


        private async Task<bool> BeginTracking()
        {
            var isTracking = (bool)this.BindingContext.GetType().GetProperty("trackingActive").GetValue(this.BindingContext);
            if (isTracking)
            {
                var alert = (Alert)this.BindingContext.GetType().GetProperty("alert").GetValue(this.BindingContext);

                var pathcontent = await homePageViewModel.LoadRoute();


                map.MapElements.Clear();




                var polyline = new Xamarin.Forms.Maps.Polyline();
                polyline.StrokeColor = Color.Black;
                polyline.StrokeWidth = 3;

                foreach (var p in pathcontent)
                {
                    polyline.Geopath.Add(p);


                }
                map.MapElements.Add(polyline);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(polyline.Geopath[0].Latitude, polyline.Geopath[0].Longitude), Xamarin.Forms.Maps.Distance.FromMiles(0.50f)));

                var pin = new Xamarin.Forms.Maps.Pin
                {
                    Type = PinType.SearchResult,
                    Position = new Xamarin.Forms.Maps.Position(polyline.Geopath.First().Latitude, polyline.Geopath.First().Longitude),
                    Label = "Pin",
                    Address = "Pin",
                    //Tag = "CirclePoint",
                    //Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("ic_circle_point.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "ic_circle_point.png", WidthRequest = 25, HeightRequest = 25 })

                };
                map.Pins.Add(pin);

                var positionIndex = 1;

                map.Pins.Clear();
                map.Pins.Add(new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(alert.Latitude, alert.Longitude),
                    Label = "User",
                });
            };
            return true;
        }
        
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
        private void activateLottie()
        {

            var alertActive = (bool)this.BindingContext.GetType().GetProperty("alertActive").GetValue(this.BindingContext);

            if (alertActive)
            {
                animationView.RepeatMode = Lottie.Forms.RepeatMode.Infinite;
                animationView.PlayAnimation();
            }
            else
            {
               //animationView.PauseAnimation();
            }

        }

    }

  
}