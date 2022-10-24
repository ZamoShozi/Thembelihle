using API.Context;
using API.Entities;
using API.Form;
using Microsoft.AspNetCore.Mvc;
using Thembelihle_API;

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
            image = Utils.GenerateImageLink(HttpContext.Request.Host.Value, database.images.Where(image => image.type == type.type).ToList())
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
           image = Utils.GenerateImageLink(HttpContext.Request.Host.Value, database.images.Where(image => image.type == type.type).ToList())
        });
        
        return Ok(new Response {success = true, data = rooms});
    }
    

    [HttpPost("insert-room")]
    public object InsertRoom([FromBody] AddRoom room)
    {
        var database = new MyDbContext();
        var newRoom = new Entities.room
        {
          type_id = room.TypeId,
          number = Convert.ToByte(room.RoomNo),
          status = Convert.ToByte(room.Status)
        };
        database.Add(newRoom);
        try
        {
            var results = database.SaveChanges();
            if (results != -1)
            {
                return Ok(new Response {message = "upload successful", success = true});
            }
            return BadRequest(new Response { message = "upload unsuccessful", success = false});
        }
        catch (Exception)
        {
            return BadRequest(new Response { message = "upload unsuccessful", success = false});
        }
    }
    [HttpPost("insert-type")]
    public object InsertType([FromBody] AddType type)
    {
        var database = new MyDbContext();
        var roomType = new Entities.room_type
        {
            description = type.Description,
            guest = type.Guest,
            number_of_beds = type.NumberOfBeds,
            price = type.Price,
            type = type.Type
        };
        database.Add(roomType);
        try
        {
            var results = database.SaveChanges();
            if (results != -1)
            {
                return Ok(new Response {message = "upload successful", success = true});
            }
            return BadRequest(new Response { message = "upload unsuccessful", success = false});
        }
        catch (Exception)
        {
            return BadRequest(new Response { message = "upload unsuccessful", success = false});
        }
    }
}