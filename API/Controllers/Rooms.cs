using API.Context;
using API.Form;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Rooms : Controller
{
    [HttpGet]
    public object IndexGet()
    {
        var database = new MyDbContext();
        var inDateTime = DateTime.Today;
        var endDateTime = DateTime.Today.AddDays(1);
        var rooms = database.room_types.Select(type => type).Select(type => new
        {
            quantity = type.rooms.Count(r => !r.status.ToString()!.Equals("1")) - type.rooms.Count(room => room.booked_rooms.Any(b =>
                (b.booking.check_in <= inDateTime && b.booking.check_out > inDateTime ||
                 b.booking.check_in >= inDateTime && b.booking.check_in < endDateTime) && !room.status.ToString()!.Equals("1"))),
            type.description,
            type.type,
            type.id,
            type.guest,
            type.number_of_beds,
            type.price,
            image = type.images.First(image => image.type == type.type)
        });
        return Ok(new Response {success = true, data = rooms});
    }

    [HttpPost]
    public object IndexPost([FromBody] Available available)
    {
        var database = new MyDbContext();
        var inDateTime = available.start;
        var endDateTime = available.end;
        var days = (endDateTime - inDateTime).Days;
        if (days <= 0 || inDateTime < DateTime.Today)
        {
            return Ok(new Response {success = false, message= "Invalid Date Range"});
        }
        var rooms = database.room_types.Select(type => type).Select(type => new
        {
            quantity = type.rooms.Count(r => !r.status.ToString()!.Equals("1")) - type.rooms.Count(room => room.booked_rooms.Any(b =>
                (b.booking.check_in <= inDateTime && b.booking.check_out > inDateTime ||
                 b.booking.check_in >= inDateTime && b.booking.check_in < endDateTime) && !room.status.ToString()!.Equals("1"))),
            type.description,
            type.type,
            type.id,
            type.guest,
            type.number_of_beds,
            type.price,
            mage = type.images.First(image => image.type == type.type)
        });
        return Ok(new Response {success = true, data = rooms});
    }
}