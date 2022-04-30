using NetTrack.Services;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NetTrack.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent(); 
            DependencyService.Get<IStatusBar>().HideStatusBar();
        }
    }
}