using API.Context;
using API.Entities;
using API.Form;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : Controller
{
    [HttpGet]
    public object IndexGet()
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

        var data = db.bookings.Where(booking => booking.customerNavigation.id == id);
        var bookings = data.Select(booking => new
        {
            booking.id,
            booking.amount,
            booking.date,
            booking.status,
            booking.check_in,
            booking.check_out,
            booking.number_guests,
            feedback = booking.feedback.rating,
            booking.breakfast,
            room = new
            {
                booking.booked_room.room.number, booking.booked_room.room.type!.description,
                booking.booked_room.room.type.type
            }
        });
        return Ok(new Response { success = true, data = bookings });
    }
    
    [HttpGet("filter")]
    public object IndexGet([FromQuery] DateTime start, DateTime end)
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
        var data = db.bookings.Where(booking => booking.customerNavigation.id == id && booking.check_in >= start && booking.check_out <= end);
        var bookings = data.Select(booking => new
        {
            booking.id,
            booking.amount,
            booking.date,
            booking.status,
            booking.check_in,
            booking.check_out,
            booking.number_guests,
            feedback = booking.feedback.rating,
            booking.breakfast,
            room = new
            {
                booking.booked_room.room!.number, booking.booked_room.room.type!.description,
                booking.booked_room.room.type.type
            }
        });
        return Ok(new Response { success = true, data = bookings });
    }

    [HttpPost]
    public object IndexPost([FromBody] MakeBooking data)
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
        var database = new MyDbContext();
        var inDateTime = Convert.ToDateTime(data.Checkout);
        var endDateTime = Convert.ToDateTime(data.Checkout);
        var rooms = database.rooms.Where(room => !room.status.ToString()!.Equals("1"))
            .Where(room => !room.booked_rooms.Any(b=>
                b.booking.check_in <= inDateTime && b.booking.check_in > inDateTime ||
                b.booking.check_in >= inDateTime && b.booking.check_in < endDateTime))
            .Where(room => room.type_id == data.TypeId).ToList();
        if (rooms.Count <= 0)
        { 
            return Ok(new Response{success = false, message="Room is already booked"});
        }
        var days = (Convert.ToDateTime(data.Checkout) - Convert.ToDateTime(data.Checkin)).Days;
        var room = rooms.First();
        var user = database.users.First(user => user.id == id);
        var promotion = database.promotions.First(promotion => promotion.code == data.Promo);
        var card = database.payments.First(c => c.card_number.Equals(data.card));
        float roomPrice = (int)database.room_types.First(type => type.id == room.type_id).price! * days;
        float breakFastPrice = data.Breakfast == 1 ? 75 * days : 0;
        double parkingPrice = data.ExtraId.Length > 0 ? 75 * days : 0;
        var price = (int)(roomPrice + breakFastPrice + parkingPrice);
        var booking = new booking
        {
            amount = price,
            customer = user.id,
            status = "Pending",
            check_in = Convert.ToDateTime(data.Checkin).AddHours(13),
            check_out = Convert.ToDateTime(data.Checkout).AddHours(10),
            number_guests = (int)database.room_types.First(type => type.id == room.type_id).number_of_beds!,
            promotion = promotion.id,
            card = card.id,
            breakfast = data.Breakfast
        };
        database.Add(booking);
        var results = database.SaveChanges();
        if (results != -1)
        {
            if (!data.Message.Equals(""))
            {
                var message = new booking_message{booking = booking.id, message = data.Message, read = 0,};
                database.Add(message);
            }
            var bookedRoom = new booked_room {room_id = room.id, booking_id = booking.id};
            database.Add(bookedRoom);
            foreach (var i in data.ExtraId)
            {
                var extra = new booked_extra{booking_id = booking.id, extra_id = i};
                database.Add(extra);
            }
            var fb = new feedback
            {
                booking = booking.id,
                message = "",
                rating = 0
            };
            database.Add(fb);
            database.SaveChanges();
        }
        var response = results != -1
            ? new Response{success = true, message="Room booked successful"}
            : new Response{success = false, message="Failed to book a room"};
        return Ok(response);
    }
}