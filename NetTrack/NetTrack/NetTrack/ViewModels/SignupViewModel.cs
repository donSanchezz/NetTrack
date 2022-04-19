using NetTrack.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NetTrack.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {

        private const string Url = "https://experiencekgn-api.utechsapna.com/api/points/getAllPointsApp";

        private readonly HttpClient _httpClient = new HttpClient();

        public User user { get; set; }

        public SignupViewModel()
        {
            user = new User();
        }

    }
}
