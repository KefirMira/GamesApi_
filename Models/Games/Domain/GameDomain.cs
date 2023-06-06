using System.Text.Json.Serialization;
using Models.Developers.DeveloperBlank;
using Models.Genres.GenreBlank;
using Models.PublishingHouse.PublishingHouseBlank;

namespace Models.Games.GameDomain;

public class GameDomain
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Poster { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("publishing_date")]
    public DateTime PublicationDate { get; set; }
    public PublishingHouseBlank? PublishingHouseBlank { get; set; }
    public int? IdPublishingHouse { get; set; }
    public List<DeveloperBlank>? DeveloperBlanks { get; set; }
    public List<GenreBlank>? GenreBlanks { get; set; }
    
    public static GameDomain Convert(GameDB.GameDB gameDB)
    {
        return new GameDomain()
        {
            Id = gameDB.Id,
            Name = gameDB.Name,
            Description = gameDB.Description,
            Poster = gameDB.Poster,
            PublicationDate = gameDB.PublicationDate
        };
    }
    public static GameDomain Convert(GameDB.GameDB gameDB, List<GenreBlank> genreBlanks, List<DeveloperBlank> developerBlanks, PublishingHouseBlank publishingHouse)
    {
        return new GameDomain()
        {
            Id = gameDB.Id,
            Name = gameDB.Name,
            Description = gameDB.Description,
            Poster = gameDB.Poster,
            PublishingHouseBlank = publishingHouse,
            DeveloperBlanks = new List<DeveloperBlank>(developerBlanks),
            GenreBlanks = new List<GenreBlank>(genreBlanks),
            PublicationDate = gameDB.PublicationDate
        };
    }

    public static IEnumerable<GameDomain> Convert(IEnumerable<GameDB.GameDB> dbs)
    {
        return dbs.Select(Convert);
    }

}