using System;

using RestSharp;
using Newtonsoft.Json;
using System.IO;

namespace WebAPIClient
{
    class Program
    {
   


        public class Tok{
            public string token_type { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }
        public class User{
            public string username { get; set; }
            public string password { get; set; }


        }

        static void Main(string[] args)
        {
            var client = new RestClient("https://ejoo.shotgunstudio.com/api/v1/auth/access_token");
            client.Timeout = -1;
            User u = JsonConvert.DeserializeObject<User>(File.ReadAllText(@"user.json"));
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", u.username);
            request.AddParameter("password", u.password);
            IRestResponse response = client.Execute(request);

            Tok m = JsonConvert.DeserializeObject<Tok>(response.Content); 

            Console.WriteLine(response.Content);
             
            
            
           
       
            client = new RestClient("https://ejoo.shotgunstudio.com/api/v1/entity/shots?acess_token=" + m.access_token) ;
            request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + m.access_token );
            response = client.Execute(request);
            Console.WriteLine(response.Content);

        }
    }
}
