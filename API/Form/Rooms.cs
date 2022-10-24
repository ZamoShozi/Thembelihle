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

public class AddRoom
{
    public AddRoom(int typeId, int status, int roomNo)
    {
        TypeId = typeId;
        Status = status;
        RoomNo = roomNo;
    }

    public int TypeId { get; }
    public int Status { get; }
    public int RoomNo { get;  }
}

public class AddType
{
    public AddType(double price, int guest, string type, int numberOfBeds, string description)
    {
        Price = price;
        Guest = guest;
        Type = type;
        NumberOfBeds = numberOfBeds;
        Description = description;
    }

    public double Price { get; }
    public int Guest { get; }
    public string Type { get; }
    public int NumberOfBeds { get; }
    public string Description { get; }
}