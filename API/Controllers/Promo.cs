using API.Context;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Promo : Controller
{
    [HttpPost("validate")]
    public object Validate([FromBody]int code)
    {
        var database = new MyDbContext();
        var list = database.promotions.Where(promotion => promotion.code == code);
        if (!list.Any())
        {
            return Ok(new Response {success = false, message = "Invalid promo code"});
        }
        var promo = list.First();
        return Ok(promo.expiry_date < DateTime.Now ? new Response {success = false, message = "Promo code has expired"} : new Response {success = true, data = new {promo.percentage, promo.code}});
    }

}