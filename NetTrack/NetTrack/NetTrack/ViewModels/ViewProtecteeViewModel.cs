using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace NetTrack.ViewModels
{
    public class ViewProtecteeViewModel : BaseViewModel
    {
        public User user { get; set; }

        public ObservableRangeCollection<Models.Contact> Protectees { get; set; } = new ObservableRangeCollection<Models.Contact>();
        public ViewProtecteeViewModel()
        {
            this.user =  UserStore.GetUser();

            for (int i = 0; i < user.protectees.Count; i++)
            {
                if (user.protectees[i].images.Count > 0)
                {
                    try
                    {
                        for (int x = 0; x < user.protectees[i].images.Count; x++)
                        {
                            user.protectees[i].images[x] = "https://nettrackapi.azurewebsites.net/Images/" + user.protectees[i].images[x];
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                }
            }
            this.Protectees = new ObservableRangeCollection<Models.Contact>(this.user.protectees);
        }
        
    }
}
