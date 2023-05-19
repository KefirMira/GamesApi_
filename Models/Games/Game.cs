namespace Models.Games;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Developer Developer { get; set; }
    public Publisher Publisher { get; set; }
    public List<Genre> Genres { get; set; }
    public DateOnly DatePublish { get; set; }
    public string Description { get; set; }
}