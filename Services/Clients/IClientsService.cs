using Microsoft.AspNetCore.Http;
using Models.Clients;
using Models.Tokens;

namespace Services.Clients;

public interface IClientsService
{
    bool CreateClient(ClientBlank companyBlank);
    bool GetClient(string login, string password);
    ClientDomain GetClientTok(string login, string password);
    TokensView CreateToken(ClientDomain clientDomain, HttpContext context);
}