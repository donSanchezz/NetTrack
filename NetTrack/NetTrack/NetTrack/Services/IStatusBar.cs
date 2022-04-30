using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;


namespace NetTrack.Services
{
    public interface IStatusBar
    {
        void HideStatusBar();
        void ShowStatusBar();
    }
}