namespace Models.Games.GameDB;

public class GameDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Poster { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public int? IdPublisherHouse { get; set; }
    
    public static GameDB Convert(GameBlank.GameBlank gameBlank)
    {
        return new GameDB()
        {
            Name = gameBlank.Name,
            Description = gameBlank.Description,
            PublicationDate = gameBlank.PublicationDate,
            Poster = gameBlank.Poster
        };
    }
    public static GameDB Convert(int gameId,GameBlank.GameBlank gameBlank)
    {
        return new GameDB()
        {
            Id = gameId,
            Name = gameBlank.Name,
            Description = gameBlank.Description,
            PublicationDate = gameBlank.PublicationDate,
            Poster = gameBlank.Poster
        };
    }
}