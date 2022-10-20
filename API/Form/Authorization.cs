namespace API.Form;

public class Login
{
    public Login(string email, string password)
    {
        Email = email;
        Password = password;
    }
    public string Email { get;}
    public string Password { get;}
}
public class Register
{
    public Register(string name, string surname, string phoneNumber, string email, string password, string passwordC, Address address)
    {
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        PasswordC = passwordC;
        Address = address;
    }

    public string Name { get;}
    public string Surname { get;}
    public string PhoneNumber { get;}
    public string Email { get;}
    public string Password { get;}
    public string PasswordC { get;}
    public Address Address { get;}
}

public class Address
{
    public Address(string country, string state, string city, int zip)
    {
        Country = country;
        State = state;
        City = city;
        Zip = zip;
    }

    public string Country { get;}
    public string State { get;}
    public string City{ get;}
    public int Zip { get;}
}