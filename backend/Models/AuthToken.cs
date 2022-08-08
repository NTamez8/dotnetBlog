namespace backend.Models
{
    public class AuthToken
    {
        public string? Token {get; set;}

        public DateTime ExpiresAt {get; set;}
    }
}