using NetTrack.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NetTrack.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}