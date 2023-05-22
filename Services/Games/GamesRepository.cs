using Models.Developers.DeveloperBlank;
using Models.Games.GameBlank;
using Models.Games.GameDB;
using Models.Games.GameDomain;
using Models.Genres.GenreBlank;
using Models.PublishingHouse.PublishingHouseBlank;
using Npgsql;

namespace Services.Games;

public class GamesRepository:IGamesRepository
{
    private string connectionString = "Host = localhost; Database = gamesdb; User ID = postgres; Password= vjnbkmlff2004";
    private NpgsqlConnection _connection;

    public GamesRepository()
    {
        _connection = new NpgsqlConnection(connectionString);
    }
    public IEnumerable<GameDB> GetAllGames()
    {
        _connection.Open();
        NpgsqlCommand command = new NpgsqlCommand("select * from game", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        List<GameDB> allgames = new List<GameDB>();
        while (reader.Read())
        {
            allgames.Add(GameDB.Convert(Convert.ToInt32(reader["id"]), new GameBlank()
            {
                Name = reader["name"].ToString(),
                PublicationDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["publication_date"])),
                Description = reader["description"].ToString(),
                Poster = reader["poster"].ToString()
            }));    
        }
        _connection.Close();
        return allgames;
    }

    public GameDB GetGame(int gameId)
    {
        _connection.Open();
        NpgsqlCommand command = new NpgsqlCommand($"select * from game where id= {gameId}", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        GameBlank game = new GameBlank();
        while (reader.Read())
        {
            game.Name = reader["name"].ToString();
            game.PublicationDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["publication_date"]));
            game.Description = reader["description"].ToString();
            game.Poster = reader["poster"].ToString();
        }
        _connection.Close();
        return GameDB.Convert(game);
    }

    public bool CreateGame(GameDomain gameDomain)
    {
        try
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(
                $"insert into game( name, idpublishing_house, poster, publication_date, description) values " +
                $"({gameDomain.Name},{gameDomain.IdPublishingHouse},{gameDomain.Poster},{gameDomain.Poster},'{gameDomain.PublicationDate}'," +
                $"{gameDomain.Description})",
                _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateGame(GameDomain gameDomain)
    {
        try
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(
                $"update game set (name, idpublishing_house, poster, publication_date, description) values ({gameDomain.Name},{gameDomain.IdPublishingHouse},{gameDomain.Poster},'{gameDomain.PublicationDate}',{gameDomain.Description}) where id = {gameDomain.Id}", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteGame(int gameId)
    {
        try
        {
            _connection.Open();
            NpgsqlCommand command = new NpgsqlCommand($"delete from game where id={gameId}", _connection);
            command.ExecuteNonQuery();
            _connection.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public IEnumerable<GenreBlank> GetGenreGame(int gameId)
    {
        _connection.Open();
        NpgsqlCommand command = new NpgsqlCommand($"select * from game_genres join genre g on g.id = game_genres.idgenre where g.id = {gameId}", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        List <GenreBlank> genres= new List<GenreBlank>();
        while (reader.Read())
        {
            genres.Add(new GenreBlank(){ Name = reader["name"].ToString()} );
        }
        _connection.Close();
        return genres;
    }
    
    public IEnumerable<DeveloperBlank> GetDeveloperGame(int gameId)
    {
        _connection.Open();
        NpgsqlCommand command = new NpgsqlCommand($"select * from game_developer join developer g on g.id = game_developer.iddeveloper where idgame = {gameId}", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        List <DeveloperBlank> developers= new List<DeveloperBlank>();
        while (reader.Read())
        {
            developers.Add(new DeveloperBlank(){ Name = reader["name"].ToString(), FoundingDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["founding_date"]))} );
        }
        _connection.Close();
        return developers;
    }
    public PublishingHouseBlank GetPublishGame(int gameId)
    {
        _connection.Open();
        NpgsqlCommand command = new NpgsqlCommand($"select * from publishing_house join game g on g.idpublishing_house = publishing_house.id where g.id = {gameId}", _connection);
        NpgsqlDataReader reader = command.ExecuteReader();
        PublishingHouseBlank publishingHouse= new PublishingHouseBlank();
        while (reader.Read())
        {
             publishingHouse =  new PublishingHouseBlank(){ Name = reader["name"].ToString(), FoundingDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["founding_date"]))} ;
        }
        _connection.Close();
        return publishingHouse;
    }
}