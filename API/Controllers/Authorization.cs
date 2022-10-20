using System.Text.RegularExpressions;
using API.Context;
using API.Entities;
using API.Form;
using Microsoft.AspNetCore.Mvc;
using Thembelihle_API;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Authorization : Controller
{
    [HttpPost("login")]
    public object Login([FromBody] Login login)
    {
        var context = new MyDbContext();
        var users = context.users.Where(user => user.email == login.Email);
        if (!users.Any())
        {
            return Ok(new Response { message = "Invalid login", success = false});
        }
        var user = users.First();
        if(!Utils.VerifyPasswordHash(login.Password, user.password_hash, user.password_salt))
        {
            return Ok(new Response { message = "Invalid login", success = false});
        }
        if (user.blocked == 1)
        {
            return Ok(new Response {success = false, message = "user is blocked"});
        }
        HttpContext.Session.SetInt32("id", user.id);
        return Ok(new Response {success = true, message = "login successful"});
    }
    
    [HttpPost("register")]
    public object Register([FromBody] Register register)
    {
        if (register.Password != register.PasswordC)
        {
            return Ok(new Response {success = false, message = "Password does not match"});
        }
        var db = new MyDbContext();
        if (db.users.Any(user => user.phone_number!.Equals(register.PhoneNumber)))
        {
            return Ok(new Response {success = false, message = "Phone number is already registered with another account"});
        }
        if (db.users.Any(user => user.email!.Equals(register.Email)))
        {
            return Ok(new Response {success = false, message = "Email address is already registered with another account"});
        }
        var user = new user();
        var address = new address();
        const string password = @"^(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{6,12}$";
        const RegexOptions option = RegexOptions.Singleline;
        if (!Regex.Matches(register.Password, password, option).Any())
        {
            return Ok(new Response {success = false, message = "At least one capital letter, one character, one digit and must be length of 6 to 12 characters. Special Characters and space not allowed"});
        }
        const string email = @"^[\w]+(?:\.[\w]+)*@[\w]+(?:\.[\w]+)*$";
        if (!Regex.Matches(register.Email, email, option).Any())
        {
            return Ok(new Response {success = false, message = "Invalid email"});
        }
        if (register.Name.Length < 3)
        {
            return Ok(new Response {success = false, message="Invalid name"});
        }
        if (register.Surname.Length < 3)
        {
            return Ok(new Response {success = false, message="Invalid surname"});
        }
        Utils.CreatePasswordHash(register.Password, out var passwordHash, out var passwordSalt);
        user.password_hash = passwordHash;
        user.password_salt = passwordSalt;
        user.name = register.Name;
        user.surname = register.Surname;
        user.blocked = 0;
        user.role = 1;
        user.image = 5;
        user.email = register.Email;
        user.phone_number = register.PhoneNumber;
        address.country = register.Address.Country;
        address.city = register.Address.City;
        address.state = register.Address.State;
        address.zip = register.Address.Zip;
        db.Add(user);
        try
        {
            var results = db.SaveChanges();
            if (results == -1)
            {
                return Ok(new Response { success = false, message = "Something went wrong while saving data" });
            }
        }
        catch (Exception)
        {
            return Ok(new Response { success = false, message = "Something went wrong while saving data" });
        }
        var id = db.users.First(u => u.email!.Equals(register.Email)).id;
        address.customer_id = id;
        db.Add(address);
        try
        {
            var results = db.SaveChanges();
            if (results == -1)
            {
                db.Remove(user);
                db.SaveChanges();
                return Ok(new Response { success = false, message = "Something went wrong while saving data" });
            }
        }
        catch (Exception)
        {
            db.Remove(user);
            db.SaveChanges();
            return Ok(new Response { success = false, message = "Something went wrong while saving data" });
        }
        HttpContext.Session.SetInt32("id", id);
        return Ok(new Response { success = true, message = "Account created successful" });
    }
    [HttpGet("logout")]
    public object Logout()
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null) return Ok(new Response { success = false, message = "Account is not logged in" });
        HttpContext.Session.Remove("id");
        return Ok(new Response { success = false, message = "Account logged out successful" });
    }
}