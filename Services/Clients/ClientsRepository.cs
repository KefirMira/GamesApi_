﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Clients;
using Models.Tokens;
using Npgsql;

namespace Services.Clients;

public class ClientsRepository:IClientsRepository
{
    private string connectionString = "Host = localhost; Database = gamesdb; User ID = postgres; Password= vjnbkmlff2004";
    private NpgsqlConnection _connection;
    private IConfiguration _configuration;
    private TokensView _tokensView;
    
    public ClientsRepository()
    {
        _connection = new NpgsqlConnection(connectionString);
    }
    
    public bool CreateClient(ClientDB clientBlank)
    {
        //try
        //{
            if (!GetClientByLogin(clientBlank.Login))
            {
                 string pas = GetHash(clientBlank.Password);
                 _connection.Open();
                 NpgsqlCommand command = new NpgsqlCommand($"insert into client(surname, name, patronymic, login, password, email) values ('{clientBlank.Surname}','{clientBlank.Name}'," +
                                                           $"'{clientBlank.Patronymic}','{clientBlank.Login}', '{pas}', '{clientBlank.Mail}')", _connection);
                 command.ExecuteNonQuery();
                 _connection.Close();
                 return true;
            }
            else
            {
                return false;
            }
        //}
        //catch
        //{
        //    return false;
        //}
    }
    
    
    public string GetHash(string input)
    {
        var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
 
        return Convert.ToBase64String(hash);
    }

    public bool GetClient(string login, string password)
    {
        try
        {
            _connection.Open();
            string pas = GetHash(password);
            NpgsqlCommand command =
                new NpgsqlCommand($"select * from client where login = '{login}' and password = '{pas}'", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            ClientBlank client = new ClientBlank();
            while (reader.Read())
            {
                client.Surname = reader["surname"].ToString();
                client.Name = reader["name"].ToString();
                client.Patronymic = reader["patronymic"].ToString();
                client.Login = reader["login"].ToString();
                client.Password = reader["password"].ToString();
                client.Mail = reader["email"].ToString();
                client.IdRole = Convert.ToInt32(reader["id_role"].ToString());
            }
            _connection.Close();
            if (client.Mail != "")
                return true;
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<ClientDB> GetAllClients()
    {
        _connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("select * from client", _connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                List<ClientDB> allclients = new List<ClientDB>();
                while (reader.Read())
                {
                    allclients.Add(ClientDB.Convert(Convert.ToInt32(reader["id"]), new ClientBlank()
                    {
                        Surname = reader["surname"].ToString(),
                        Name = reader["name"].ToString(),
                        Patronymic = reader["patronymic"].ToString(),
                        Login = reader["login"].ToString(),
                        Password = reader["password"].ToString(),
                        Mail = reader["email"].ToString(),
                        IdRole = Convert.ToInt32(reader["id_role"].ToString())
                    }));    
                }
                _connection.Close();
                return allclients;
    }

    public ClientDomain GetClientTok(string login, string password)
    {
         _connection.Open();
         string pas = GetHash(password);
            NpgsqlCommand command =
                new NpgsqlCommand($"select * from client where login = '{login}' and password = '{pas}'", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            ClientDomain client = new ClientDomain();
            while (reader.Read())
            {
                client.Id = Convert.ToInt32(reader["id"]);
                client.Surname = reader["surname"].ToString();
                client.Name = reader["name"].ToString();
                client.Patronymic = reader["patronymic"].ToString();
                client.Login = reader["login"].ToString();
                client.Password = reader["password"].ToString();
                client.Mail = reader["email"].ToString();
                client.IdRole = Convert.ToInt32(reader["id_role"]);
            }

            _connection.Close();
            return client;
    }
    public bool GetClientByLogin(string login)
    {
        try
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"select * from client where login = {login}", _connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            ClientBlank client = new ClientBlank();
            while (reader.Read())
            {
                client.Surname = reader["surname"].ToString();
                client.Name = reader["name"].ToString();
                client.Patronymic = reader["patronymic"].ToString();
                client.Login = reader["login"].ToString();
                client.Password = reader["password"].ToString();
                client.Mail = reader["mail"].ToString();
                client.IdRole = Convert.ToInt32(reader["id_role"].ToString());
            }

            _connection.Close();
            if (client != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            _connection.Close();
            return false;
        }
    }
    private IPAddress? GetIpAddress(string ip)
    {
        try
        {
            if(ip == "localhost")
                return IPAddress.Parse("127.0.0.1"); 
            return IPAddress.Parse(ip);
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    
    
    public TokensView AddTokensToDB(ClientDomain client, HttpContext context)
    {
        try
        {
            _connection.Open();
            _tokensView = new TokensView();
            string access = CreateAccessToken(client);
            string refresh = CreateRefreshToken(client);
            var ip = GetIpAddress(context.Request.Host.Host);
            _tokensView.IdRole = client.IdRole;
            
            
            NpgsqlCommand command = new NpgsqlCommand($"insert into token(jwt_token, refresh_token, ip, idclient) values " +
                                                      $"('{access}','{refresh}','{ip}','{client.Id}')", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return _tokensView;
        }
        catch(Exception e )
        {
            
            throw new Exception(e.Message) ;
        }
    }
    
   public string CreateRefreshToken(ClientDomain _client)
   {
       List<Claim> claims;
        if (_client.IdRole == 1)
        {
            claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _client.Login),
                new Claim("id", _client.Id.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
        }
        else
        {
            claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _client.Login),
                new Claim("id", _client.Id.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };
        }
        string validIssuer = "aboba";
        string validAudience = "aboba";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretkey!1234abob"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: validIssuer,
            audience:validAudience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(21),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        _tokensView.RefreshToken = jwt;
        return jwt;
    }
    public string CreateAccessToken(ClientDomain _client)
    {
        List<Claim> claims;
        if (_client.IdRole == 1)
        {
            claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _client.Login),
                new Claim("id", _client.Id.ToString()),
                new Claim(ClaimTypes.Role,"Admin")
            };
        }
        else
        {
            claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _client.Login),
                new Claim("id", _client.Id.ToString()),
                new Claim(ClaimTypes.Role,"User")
            };
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretkey!1234abob"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        _tokensView.JwtToken = jwt;
        return jwt;
    }
    
    public TokensView RefreshToken(string token)
    {
        try
        {
            _tokensView = new TokensView();
            ClientDomain client = GetClientByToken(token);
            string access = CreateAccessToken(client);
            _tokensView.IdRole = client.IdRole;
            _tokensView.RefreshToken = token;
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"UPDATE token SET jwt_token = '{access}' WHERE refresh_token = '{token}'", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return _tokensView;
        }
        catch(Exception e )
        {
            throw new Exception(e.Message) ;
        }
    }
//select * from client join token on client.id = token.idclient where refresh_token={}
    public ClientDomain GetClientByToken(string token)
    {
        _connection.Open();
        NpgsqlCommand command =
            new NpgsqlCommand($"select * from client join token on client.id = token.idclient where refresh_token='{token}'", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        ClientDomain client = new ClientDomain();
        while (reader.Read())
        {
            client.Id = Convert.ToInt32(reader["id"]);
            client.Surname = reader["surname"].ToString();
            client.Name = reader["name"].ToString();
            client.Patronymic = reader["patronymic"].ToString();
            client.Login = reader["login"].ToString();
            client.Password = reader["password"].ToString();
            client.Mail = reader["email"].ToString();
            client.IdRole = Convert.ToInt32(reader["id_role"]);
        }

        _connection.Close();
        return client;
    }
}