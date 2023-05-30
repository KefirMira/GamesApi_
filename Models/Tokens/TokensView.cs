using Models.Clients;

namespace Models.Tokens;

public class TokensView
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}