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