namespace Models.Developers.DeveloperDB;

public class DeveloperDB
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    
    public static DeveloperDB Convert(DeveloperBlank.DeveloperBlank developerBlank)
    {
        return new DeveloperDB()
        {
            Name = developerBlank.Name,
            FoundingDate = developerBlank.FoundingDate
        };
    } 
    public static DeveloperDB Convert(int developerId ,DeveloperBlank.DeveloperBlank developerBlank)
    {
        return new DeveloperDB()
        {
            Id = developerId,
            Name = developerBlank.Name,
            FoundingDate = developerBlank.FoundingDate
        };
    }
}