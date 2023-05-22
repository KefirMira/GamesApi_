using Models.Games.GameBlank;
using Models.Games.GameDomain;

namespace Services.Games;

public interface IGamesService
{
    bool CreateGame(GameDomain gameBlank);
    bool UpdateGame(GameDomain gameBlank);
    bool DeleteGame(int gameId);
    GameDomain GetGame(int gameId);
    IEnumerable<GameDomain> GetAllGame();
    bool CreateDeveloperToGame(DeveloperToGame devToGame);
    bool CreateGenresToGame(GenresToGame genToGame);

}