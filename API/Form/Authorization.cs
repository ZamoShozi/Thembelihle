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

public class ResetPassword
{
    public ResetPassword(int code, string password, string passwordC)
    {
        Code = code;
        Password = password;
        PasswordC = passwordC;
    }
    public int Code { get; }
    public string Password { get; }
    public string PasswordC { get; }
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

    public string isValid()
    {
        if (Name.Length < 3)
        {
            return "Invalid name";
        }
        if (Surname.Length < 3)
        {
            return "Invalid surname";
        }
        if (PhoneNumber.Length < 12)
        {
            return "Invalid Phone number";
        }
        if (!Address.validCountry())
        {
            return "Invalid country name";
        }
        if (Address.City.Length < 3)
        {
            return "Invalid city";
        }
        return Address.Zip < 1 ? "Invalid zip code" : "";
    }
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

    public bool validCountry() {
            string[] data = {
                "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua and Barbuda", "Argentina", "Armenia",
                "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
                "Belize", "Benin", "Bhutan", "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria",
                "Burkina Faso", "Burundi", "Cabo Verde", "Cambodia", "Cameroon", "Canada", "Central African Republic",
                "Chad", "Chile", "China", "Colombia", "Comoros", "Congo, Democratic Republic of the", "Congo, Republic of the",
                "Costa Rica", "Cote d'Ivoire", "Croatia", "Cuba", "Cyprus", "Czechia", "Denmark", "Djibouti", "Dominica",
                "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Eswatini",
                "Ethiopia", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada",
                "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India",
                "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya",
                "Kiribati", "Kosovo", "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya",
                "Liechtenstein", "Lithuania", "Luxembourg", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta",
                "Marshall Islands", "Mauritania", "Mauritius", "Mexico", "Micronesia", "Moldova", "Monaco", "Mongolia",
                "Montenegro", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand",
                "Nicaragua", "Niger", "Nigeria", "North Korea", "North Macedonia", "Norway", "Oman", "Pakistan", "Palau",
                "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Qatar",
                "Romania", "Russia", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines",
                "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles",
                "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands", "Somalia", "South Africa",
                "South Korea", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Sweden", "Switzerland", "Syria",
                "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tonga", "Trinidad and Tobago",
                "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates",
                "United Kingdom", "United States of America ", "Uruguay", "Uzbekistan", "Vanuatu",
                "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe" };
            return data.Any(country => string.Equals(country, Country, StringComparison.CurrentCultureIgnoreCase));
    }
}