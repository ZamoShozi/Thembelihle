using System.Text.RegularExpressions;
using API.Context;
using API.Entities;
using API.Form;
using Microsoft.AspNetCore.Mvc;
using Thembelihle_API;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Account : Controller
{
    [HttpGet]
    public object _Account()
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        var user = users.Select(u => new
        {
            u.name,
            u.surname,
            u.email,
            u.phone_number,
            address = u.address != null ? new {
                u.address.country,
                u.address.state,
                u.address.city,
                u.address.zip,
            } : null
        }).First();
        return Ok(new Response { message = "", data = user, success = true});
    }
    [HttpPost]

    [HttpPost("update/details")]
    public object UpdateAccount([FromBody] UpdateAccount account)
    {
        
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        var user = users.First();
        if(!Utils.VerifyPasswordHash(account.PasswordO, user.password_hash, user.password_salt))
        {
            return Ok(new Response { message = "Invalid confirm password", success = false});
        }
        user.name = account.Name;
        user.phone_number = account.PhoneNumber;
        user.surname = account.Surname;
        db.Update(user);
        try
        {
            var results = db.SaveChanges();
            if (results == -1)
            {
                return Ok(new Response { success = false, message = "Something went wrong while update profile" });
            }
        }
        catch (Exception)
        {
            return Ok(new Response { success = false, message = "Something went wrong while update profile" });
        }
        return Ok(new Response {success = true, message = "Account updated successful" });
    }

    [HttpPost("update/address")]
    public object UpdateAddress([FromBody]Address address)
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        var addresses = db.addresses.Where(a => a.customer_id == id);
        var add = addresses.Any() ? addresses.First() : new address();
        add.city = address.City;
        add.country = address.Country;
        add.state = address.State;
        add.zip = address.Zip;
        if (addresses.Any())
        {
            db.Update(add);
        }
        else
        {
            db.Add(add);
        }
        try
        {
            var results = db.SaveChanges();
            if (results == -1)
            {
                return Ok(new Response { success = false, message = "Something went wrong while update address" });
            }
        }
        catch (Exception)
        {
            return Ok(new Response { success = false, message = "Something went wrong while update address" });
        }
        return Ok(new Response { success = false, message = "Address updated success full" });
    }

    [HttpPost("update/password")]
    public object UpdatePassword([FromBody] UpdatePassword password)
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        if (password.PasswordN != password.PasswordC)
        {
            return Ok(new Response {success = false, message = "Password does not match"});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        var user = users.First();
        if(!Utils.VerifyPasswordHash(password.PasswordO, user.password_hash, user.password_salt))
        {
            return Ok(new Response { message = "Invalid confirm password", success = false});
        }
        Utils.CreatePasswordHash(password.PasswordN, out var passwordHash, out var passwordSalt);
        user.password_hash = passwordHash;
        user.password_salt = passwordSalt;
        db.Add(user);
        try
        {
            var results = db.SaveChanges();
            if (results == -1)
            {
                return Ok(new Response { success = false, message = "Something went wrong while updating password" });
            }
        }
        catch (Exception)
        {
            return Ok(new Response { success = false, message = "Something went wrong while updating password" });
        }
        return Ok(new Response { success = false, message = "Password updated success full" });
    }
    
    [HttpPost("payment")]
    public object AccountPayment([FromBody]CreateCard card)
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        if (card.ExpiryDate < DateTime.Today)
        {
            return Ok(new Response { message = "Card is Expired", success = false});
        }
        const string regexCcv = "^[0-9]{3,4}$";
        const string regexCard = "(?<visa>4[0-9]{12}(?:[0-9]{3}))|(?<mastercard>5[1-5][0-9]{14})";
        const RegexOptions optionCcv = RegexOptions.Singleline;
        const RegexOptions optionCard = RegexOptions.Singleline;
        if (!Regex.Matches(card.Cvv.ToString(), regexCcv, optionCcv).Any())
        {
            return Ok(new Response { message = "Invalid ccv number", success = false});
        }
        if (Regex.Matches(card.CardNumber, regexCard, optionCard).Any())
        {
            return Ok(new Response {message = "Invalid card number", success = false});
        }
        var user = db.users.First(u => u.id.Equals(id));
        var payment = new payment
        {
            cvv = card.Cvv,
            card_holder = card.CardHolder,
            card_number = card.CardNumber,
            expiry_date = card.ExpiryDate,
            customer_id = user.id
        };
        db.Add(payment);
        try
        {
            var results = db.SaveChanges();
            return Ok(results != -1 ? new Response {success = true, message = "Card saved successful"} : new Response {success = false, message = "Failed to save the card"});
        }
        catch (Exception)
        {
           return  Ok(new Response { success = false, message = "Failed to save the card" });
        }
    }
        
    [HttpGet("payment")]
    public object AccountPayment()
    {
        var id = HttpContext.Session.GetInt32("id");
        if (id == null)
        {
            return Ok(new Response { message = "You need to login", success = false});
        }
        var db = new MyDbContext();
        var users = db.users.Where(user => user.id == id);
        if (!users.Any())
        {
            return Ok(new Response { message = "Your account is not available on the system", success = false});
        }
        var cards = db.payments.Where(payment => payment.customer_id == id && payment.expiry_date > DateTime.Today)
            .Select(card => new
            {
                card.card_number,
                card.id
            });
        return Ok(new Response {success = true, data = cards});
    }
}