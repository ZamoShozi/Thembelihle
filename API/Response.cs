namespace API;

public class Response
{
    public string message { get; set; } = "";
    public object data { get; set; } = new { };
    public bool success { get; set; }
}