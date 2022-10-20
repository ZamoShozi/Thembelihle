namespace API.Form;

public class MakeBooking
{
    public MakeBooking(int typeId, int[] extraId, DateTime checkin, DateTime checkout, int breakfast, int promo, string card, string message)
    {
        TypeId = typeId;
        ExtraId = extraId;
        Checkin = checkin;
        Checkout = checkout;
        Breakfast = breakfast;
        Promo = promo;
        this.card = card;
        Message = message;
    }
    public int TypeId { get;}
    public int[] ExtraId { get;}
    public DateTime Checkin { get;}
    public DateTime Checkout { get;}
    public int Breakfast { get;} = 0;
    public int Promo { get;} = 0;
    public string card { get;}
    
    public string Message { get;}
}