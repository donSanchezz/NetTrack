using NetTrack.ViewModels;
using NetTrack.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NetTrack
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContactDetailPage), typeof(ContactDetailPage));
            Routing.RegisterRoute(nameof(NewContactPage), typeof(NewContactPage));
        }

    }
}
