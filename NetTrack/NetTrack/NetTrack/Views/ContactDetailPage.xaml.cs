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
    public partial class ContactDetailPage : ContentPage
    {
        public ContactDetailPage()
        {
            InitializeComponent();
            BindingContext = new ContactDetailViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (primary.IsToggled)
            {
                PrimaryLbl.IsVisible = true;
            }
            else
            {
                NotPrimaryLbl.IsVisible = true;
            }
        }
    }
}