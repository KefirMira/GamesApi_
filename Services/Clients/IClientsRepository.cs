using Microsoft.AspNetCore.Http;
using Models.Clients;

namespace Services.Clients;

public interface IClientsRepository
{
    bool CreateClient(ClientDB clientBlank);
    bool GetClient(string login, string password);
    IEnumerable<ClientDB> GetAllClients();
}