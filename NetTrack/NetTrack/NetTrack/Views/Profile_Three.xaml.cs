using NetTrack.Models;
using NetTrack.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile_Three : ContentPage
    {
        ProfileThreeViewModel _viewModel;
        public User user;
        public Profile_Three(object user)
        {
            InitializeComponent();

            this.user = (User)user.GetType().GetProperty("user").GetValue(user, null);
            BindingContext = _viewModel = new ProfileThreeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void Submit(object sender, EventArgs e)
        {
            user.contacts = _viewModel.Contacts.ToList();

            await _viewModel.Regiter(user);
            Debug.WriteLine(user);
        }
    }
}