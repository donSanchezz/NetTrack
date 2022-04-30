using NetTrack.Services;
using NetTrack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetTrack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        LoginViewModel _viewModel;
        public Login()
        {
            InitializeComponent();
            DependencyService.Get<IStatusBar>().HideStatusBar();
            _viewModel = new LoginViewModel();
            BindingContext = _viewModel = new LoginViewModel();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            _viewModel.OnLoginClicked();
        }

        private void Signup(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile_One());
        }
    }
}