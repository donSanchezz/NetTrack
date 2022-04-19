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
    public partial class Profile_Three : ContentPage
    {
        ProfileThreeViewModel _viewModel;
        public Profile_Three()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ProfileThreeViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}