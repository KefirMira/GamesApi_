using Models.PublishingHouse.PublishingHouseDomain;

namespace Models.PublishingHouse.PublishingHouseView;

public class PublishingHouseView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    
    public static PublishingHouseView Convert(PublishingHouseDomain.PublishingHouseDomain publishingHouseDomain)
    {
        return new PublishingHouseView()
        {
            Id = publishingHouseDomain.Id,
            Name = publishingHouseDomain.Name,
            FoundingDate = publishingHouseDomain.FoundingDate
        };
    }
    
    public static IEnumerable<PublishingHouseView> Convert(IEnumerable<PublishingHouseDomain.PublishingHouseDomain> domains)
    {
        return domains.Select(Convert);
    }
}