namespace Models.Clients;

public class ClientView
{
    public  int Id { get;  set; }
    public string Surname { get; set; } 
    public string Name { get; set; } 
    public string Patronymic { get; set; } 
    public string Login { get; set; } 
    public string Password { get; set; } 
    public string Mail { get; set; } 
    public int IdRole { get; set; }
    public static ClientView Convert(ClientDomain clientDB)
    {
        return new ClientView()
        {
            Id = clientDB.Id,
            Surname = clientDB.Surname,
            Name = clientDB.Name,
            Patronymic = clientDB.Patronymic,
            Login = clientDB.Login,
            Password = clientDB.Password,
            Mail = clientDB.Mail,
            IdRole = clientDB.IdRole
        };
    }

    public static IEnumerable<ClientView> Convert(IEnumerable<ClientDomain> domains)
    {
        return domains.Select(Convert);
    }
}