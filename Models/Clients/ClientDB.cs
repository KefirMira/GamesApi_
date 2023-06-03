namespace Models.Clients;

public class ClientDB
{
    public  int Id { get; private set; }
    public string Surname { get;private set; } 
    public string Name { get;private set; } 
    public string Patronymic { get;private set; } 
    public string Login { get;private set; } 
    public string Password { get;private set; } 
    public string Mail { get;private set; } 
    public int IdRole { get; set; }
    public static ClientDB Convert(ClientBlank clientBlank)
    {
        return new ClientDB()
        {
            Surname = clientBlank.Surname,
            Name = clientBlank.Name,
            Patronymic = clientBlank.Patronymic,
            Login = clientBlank.Login,
            Password = clientBlank.Password,
            Mail = clientBlank.Mail,
            IdRole = clientBlank.IdRole
        };
    }
    public static ClientDB Convert(int clientId,ClientBlank clientBlank)
    {
        return new ClientDB()
        {   Id = clientId,
            Surname = clientBlank.Surname,
            Name = clientBlank.Name,
            Patronymic = clientBlank.Patronymic,
            Login = clientBlank.Login,
            Password = clientBlank.Password,
            Mail = clientBlank.Mail,
            IdRole = clientBlank.IdRole
        };
    }
}