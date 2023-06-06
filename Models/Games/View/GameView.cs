using Models.Developers.DeveloperBlank;
using Models.Genres.GenreBlank;
using Models.PublishingHouse.PublishingHouseBlank;

namespace Models.Games.GameView;

public class GameView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Poster { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public PublishingHouseBlank? PublishingHouseBlank { get; set; }
    public List<DeveloperBlank>? DeveloperBlanks { get; set; }
    public List<GenreBlank>? GenreBlanks { get; set; }
    
    public static GameView Convert(GameDomain.GameDomain gameDomain)
    {
        return new GameView()
        {
            Id = gameDomain.Id,
            Name = gameDomain.Name,
            Poster = gameDomain.Poster,
            Description = gameDomain.Description,
            DeveloperBlanks = gameDomain.DeveloperBlanks,
            GenreBlanks = gameDomain.GenreBlanks,
            PublicationDate = gameDomain.PublicationDate,
            PublishingHouseBlank = gameDomain.PublishingHouseBlank
        };
    }

    public static IEnumerable<GameView> Convert(IEnumerable<GameDomain.GameDomain> gameDomains)
    {
        return gameDomains.Select(Convert);
    }
}