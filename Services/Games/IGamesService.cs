using Models.Games.GameBlank;
using Models.Games.GameDomain;
using Models.PublishingHouse.PublishingHouseDomain;

namespace Services.Games;

public interface IGamesService
{
    bool CreateGame(GameDomain gameBlank);
    bool UpdateGame(GameDomain gameBlank);
    bool DeleteGame(int gameId);
    GameDomain GetGame(int gameId);
    IEnumerable<GameDomain> GetAllGame();
    IEnumerable<PublishingHouseDomain> GetAllPublishers();
    bool CreateDeveloperToGame(DeveloperToGame devToGame);
    bool CreateGenresToGame(GenresToGame genToGame);

}