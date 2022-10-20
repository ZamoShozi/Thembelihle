namespace API.Form;

public class UpdateAccount
{
    public UpdateAccount(string name, string surname, string phoneNumber, string passwordO)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        PasswordO = passwordO;
    }

    public string Name { get;}
    public string Surname { get;}
    public string PhoneNumber { get;}
    public string PasswordO { get; }
}

public class UpdatePassword
{
    public UpdatePassword(string passwordO, string passwordN, string passwordC)
    {
        PasswordO = passwordO;
        PasswordN = passwordN;
        PasswordC = passwordC;
    }

    public string PasswordO { get;}
    public string PasswordN { get;}
    public string PasswordC { get;}
}

public class CreateCard
{
    public CreateCard(string cardHolder, DateTime expiryDate, int cvv, string cardNumber)
    {
        CardHolder = cardHolder;
        ExpiryDate = expiryDate;
        Cvv = cvv;
        CardNumber = cardNumber;
    }
    public string CardHolder {get;}
    public DateTime ExpiryDate {get; }
    public int Cvv {get; }
    public string CardNumber { get; }
}