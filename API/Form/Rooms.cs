namespace API.Form;

public class Available
{
    public Available(DateTime start, DateTime end)
    {
        this.start = start;
        this.end = end;
    }
    public DateTime start { get; }
    public DateTime end { get; }
}

public class AddType
{
    
    public int guest { get; }
    public string Type { get; }
    public int NumberOfBeds { get; }
    public string Description { get; }
    public IFormFile Image { get; }
    public int RoomId { get; }
}