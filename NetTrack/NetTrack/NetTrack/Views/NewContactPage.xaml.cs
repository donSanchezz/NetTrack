using NetTrack.Models;
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
    public partial class NewContactPage : ContentPage
    {
        public Contact Contact { get; set; }
        public NewContactPage()
        {
            InitializeComponent();
            BindingContext = new NewContactViewModel();
        }
    }
}