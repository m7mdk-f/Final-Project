namespace Final_Project.Models
{
    public class GoogleUserInfo
    {

        public string Email { get; set; }
        public string given_name { get; set; } // This is the first name (Google uses "given_name")
        public string family_name { get; set; } // This is the last name (Google uses "family_name")
        public string picture { get; set; }
    }
}
