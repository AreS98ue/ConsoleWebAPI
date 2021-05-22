namespace TokenResponseForm
{
    public class TokenResponse
    {
        
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; } 

        public TokenResponse()
        {
            token_type = "";

            access_token = "";

            expires_in = 0;

            refresh_token = "";

            
        }
            
    }
}