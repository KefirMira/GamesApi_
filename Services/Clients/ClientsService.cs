using Microsoft.AspNetCore.Http;
using Models.Clients;
using Models.Tokens;

namespace Services.Clients;

public class ClientsService:IClientsService
{
    private readonly ClientsRepository _repository;
    
    public ClientsService()
    {
        _repository = new ClientsRepository();
    }
    public bool CreateClient(ClientBlank companyBlank)
    {
        ClientDB client = ClientDB.Convert(companyBlank);
        if(_repository.CreateClient(client))
            return true;
        else
        {
            return false;
        }
    }

    public bool GetClient(string login, string password)
    {
        if (_repository.GetClient(login, password))
            return true;
        else
            return false;
    }

    public ClientDomain GetClientTok(string login, string password)
    {
        return _repository.GetClientTok(login, password);
    }

    public TokensView CreateToken(ClientDomain clientDomain, HttpContext context)
    {
        return _repository.AddTokensToDB(clientDomain, context);
    }

   
}