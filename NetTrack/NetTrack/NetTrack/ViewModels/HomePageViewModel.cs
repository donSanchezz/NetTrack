using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetTrack.ViewModels
{
    public  class HomePageViewModel : BaseViewModel
    {
        public User user { get; set; }
        public HomePageViewModel()
        {
            this.user = UserStore.GetUser();
        }
    }
}
