namespace Models.Genres.GenreView;

public class GenreView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public static GenreView Convert(GenreDomain.GenreDomain genreDoamin)
    {
        return new GenreView()
        {
            Id = genreDoamin.Id,
            Name = genreDoamin.Name
        };
    }

    public static IEnumerable<GenreView> Convert(IEnumerable<GenreDomain.GenreDomain> domains)
    {
        return domains.Select(Convert);
    }
}