using Models.Games.GameBlank;
using Models.Games.GameDB;
using Models.Games.GameDomain;
using Models.Games.GameView;

namespace Services.Games;

public interface IGamesRepository
{
    IEnumerable<GameDB> GetAllGames();
    GameDB GetGame(int gameId);
    bool CreateGame(GameDomain gameDB);
    bool UpdateGame(GameDomain gameDB);
    bool DeleteGame(int gameId);
    bool CreateDeveloperToGame(DeveloperToGame devToGame);
    bool CreateGenresToGame(GenresToGame genToGame);
}