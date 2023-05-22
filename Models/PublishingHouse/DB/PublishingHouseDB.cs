using Models.Genres.GenreBlank;

namespace Models.PublishingHouse.PublishingHouseDB;

public class PublishingHouseDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    
    public static PublishingHouseDB Convert(PublishingHouseBlank.PublishingHouseBlank publishingblank)
    {
        return new PublishingHouseDB()
        {
            Name = publishingblank.Name,
            FoundingDate = publishingblank.FoundingDate
        };
    } 
    public static PublishingHouseDB Convert(int publishingId ,PublishingHouseBlank.PublishingHouseBlank publishingblank)
    {
        return new PublishingHouseDB()
        {
            Id = publishingId,
            Name = publishingblank.Name,
            FoundingDate = publishingblank.FoundingDate
        };
    }
}