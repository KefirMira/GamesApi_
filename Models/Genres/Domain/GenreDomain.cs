namespace Models.Genres.GenreDomain;

public class GenreDomain
{
    public int Id { get; set; }
    public string Name { get; set; }
    public static GenreDomain Convert(GenreDB.GenreDB genreDB)
    {
        return new GenreDomain()
        {
            Id = genreDB.Id,
            Name = genreDB.Name
        };
    }

    public static IEnumerable<GenreDomain> Convert(IEnumerable<GenreDB.GenreDB> dbs)
    {
        return dbs.Select(Convert);
    }
}