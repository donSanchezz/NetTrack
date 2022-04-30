using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace NetTrack.Models
{
    public class Contact
    {
       
        public int Id { get; set; }

        [JsonIgnore]
        public string id { get; set; }


        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("primary")]
        public bool Primary { get; set; }
    }
}
