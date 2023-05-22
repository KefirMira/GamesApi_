namespace Models.Genres.GenreDB;

public class GenreDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public static GenreDB Convert(GenreBlank.GenreBlank genreblank)
    {
        return new GenreDB()
        {
            Name = genreblank.Name
        };
    } 
    public static GenreDB Convert(int genreId ,GenreBlank.GenreBlank genreblank)
    {
        return new GenreDB()
        {
            Id = genreId,
            Name = genreblank.Name
        };
    }
}
