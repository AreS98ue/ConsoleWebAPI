using System;

using RestSharp;
using Newtonsoft.Json;
using System.IO;
using KorisnikSet;
using TokenResponseForm;


namespace WebAPIClient
{
    class Program
    {
        
        public static string PostTokenReqUserAuth()
        {
            var urlTokenAddress = "https://ejoo.shotgunstudio.com/api/v1/auth/access_token";
            var client = new RestClient(urlTokenAddress);
            client.Timeout = -1;
            KorisnikKlasa Userinfo = JsonConvert.DeserializeObject<KorisnikKlasa>(File.ReadAllText(@"user.json"));
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", Userinfo.username);
            request.AddParameter("password", Userinfo.password);
            IRestResponse response = client.Execute(request);

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("RESPONSE");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(response.Content);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("END OF RESPONSE");
            Console.WriteLine("---------------------------------------");
            
            
            return response.Content;
        }
        public static void GetRecords(string _response)
        {
            TokenResponse m;
            if(_response == ""){
                m = new TokenResponse();
            } 
            else{
                m = JsonConvert.DeserializeObject<TokenResponse>(_response);
            }
            
           
            var urlAddress = "https://ejoo.shotgunstudio.com/api/v1/entity/shots" ;
            var client = new RestClient( urlAddress ) ;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + m.access_token );
            var response = client.Execute(request);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("RESPONSE");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(response.Content);
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("END OF RESPONSE");
            Console.WriteLine("---------------------------------------");
        }
     


        static void Main(string[] args)
        {
            string resp = "";
            
            while (true)
            {
                Console.WriteLine("Za dobijanje Access Token-a uneti get acctoken, za citanje records-a uneti get records, za brisanje tokena uneti delete token za izlazak iz aplikacije uneti exit:");
                string unos = Console.ReadLine();
                Console.WriteLine("");
                
               if (unos == "get acctoken")
               {
                   resp = PostTokenReqUserAuth();
               }
           
               else if (unos == "delete token")
               {
                   resp ="";
               }

                else if (unos == "get records")
               {
                   GetRecords(resp);
               }
               else if (unos == "exit")
               {
                   Environment.Exit(0);
                   
               }
               else 
               {
                   Console.WriteLine("Nedozvoljen unos");
               }

            }
            
            
            
            
          

        }
         
    }
}
