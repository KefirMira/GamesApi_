namespace Models.Developers.DeveloperDomain;

public class DeveloperDomain
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    
    public static DeveloperDomain Convert(DeveloperDB.DeveloperDB developerDb)
    {
        return new DeveloperDomain()
        {
            Id = developerDb.Id,
            Name = developerDb.Name,
            FoundingDate = developerDb.FoundingDate
        };
    }

    public static IEnumerable<DeveloperDomain> Convert(IEnumerable<DeveloperDB.DeveloperDB> dbs)
    {
        return dbs.Select(Convert);
    }
}