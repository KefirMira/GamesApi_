namespace Models.PublishingHouse.PublishingHouseDomain;

public class PublishingHouseDomain
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    public static PublishingHouseDomain Convert(PublishingHouseDB.PublishingHouseDB publishingHouseDb)
    {
        return new PublishingHouseDomain()
        {
            Id = publishingHouseDb.Id,
            Name = publishingHouseDb.Name,
            FoundingDate = publishingHouseDb.FoundingDate
        };
    }

    public static IEnumerable<PublishingHouseDomain> Convert(IEnumerable<PublishingHouseDB.PublishingHouseDB> dbs)
    {
        return dbs.Select(Convert);
    }
}