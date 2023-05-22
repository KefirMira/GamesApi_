namespace Models.Developers.DeveloperView;

public class DeveloperView
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    
    public static DeveloperView Convert(DeveloperDomain.DeveloperDomain developerDomain)
    {
        return new DeveloperView()
        {
            Id = developerDomain.Id,
            Name = developerDomain.Name,
            FoundingDate = developerDomain.FoundingDate
        };
    }
    
    public static IEnumerable<DeveloperView> Convert(IEnumerable<DeveloperDomain.DeveloperDomain> domains)
    {
        return domains.Select(Convert);
    }
}