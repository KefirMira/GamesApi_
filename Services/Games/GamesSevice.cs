using Models.Developers.DeveloperBlank;
using Models.Games;
using Models.Games.GameBlank;
using Models.Games.GameDB;
using Models.Games.GameDomain;
using Models.Genres.GenreBlank;
using Models.PublishingHouse.PublishingHouseBlank;

namespace Services.Games;

public class GamesSevice:IGamesService
{
    private readonly GamesRepository _repository;

    public GamesSevice()
    {
        _repository = new GamesRepository();
    }
    public bool CreateGame(GameDomain gameDomain)
    {
      
        if(_repository.CreateGame(gameDomain))
            return true;
        else
        {
            return false;
        }
    }

    public bool UpdateGame(GameDomain gameBlank)
    {
        if(_repository.UpdateGame(gameBlank))
            return true;
        else
        {
            return false;
        }
    }

   

    public bool DeleteGame(int gameId)
    {
        if(_repository.DeleteGame(gameId))
            return true;
        else
        {
            return false;
        }
    }

    public GameDomain GetGame(int gameId)
    {
        GameDB gameDB = _repository.GetGame(gameId);
        List<GenreBlank> genresBlanks = _repository.GetGenreGame(gameId).ToList();
        List<DeveloperBlank> developerBlanks = _repository.GetDeveloperGame(gameId).ToList();
        PublishingHouseBlank publishingHouse = _repository.GetPublishGame(gameId);
        // в промежуточную таблицу по ид
        //     конвертация в модель 
        //     объединение
        return GameDomain.Convert(gameDB, genresBlanks,developerBlanks, publishingHouse);
    }

    public IEnumerable<GameDomain> GetAllGame()
    {
        IEnumerable<GameDB> allgames = _repository.GetAllGames();
        List<GameDomain> allinfogame = new List<GameDomain>();
        foreach (var item in allgames)
        {
            List<GenreBlank> genresBlanks = _repository.GetGenreGame(item.Id).ToList();
            List<DeveloperBlank> developerBlanks = _repository.GetDeveloperGame(item.Id).ToList();
            PublishingHouseBlank publishingHouse = _repository.GetPublishGame(item.Id);
            allinfogame.Add(GameDomain.Convert(item, genresBlanks,developerBlanks, publishingHouse));
        }
        return allinfogame;
    }
}