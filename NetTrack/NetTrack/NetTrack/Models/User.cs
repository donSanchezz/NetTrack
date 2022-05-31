using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace NetTrack.Models
{
    public class User
    {

        public string Id { get; set; }

       
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phonenumber { get; set; }
        public string country { get; set; }
        public int day { get; set; }
        public string month { get; set; }
        public int year { get; set; }
        public string body { get; set; }
        public string skin { get; set; }
        public string eye { get; set; }

        public List<Contact> contacts { get; set; }
        public List<Contact> protectees { get; set; }
    }
}
