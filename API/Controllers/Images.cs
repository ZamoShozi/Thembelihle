using API.Context;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Images : Controller
{
    [HttpGet]
    public object GetImage([FromQuery] int id)
    {
        var db = new MyDbContext();
        var images = db.images.Where(image => image.id == id);
        if (images.Any())
        {
            return File(images.First().data, "image/png");
        }
        return BadRequest("Image not found");
    }

    [HttpPost("insert")]
    public object InsertImage(IFormFile file, [FromQuery] int id)
    {
        var database = new MyDbContext();
        var type = database.room_types.Where(roomType => roomType.id == id);
        if (!type.Any())
        {
            return BadRequest(new Response { success = false, message = "Unable to find the corresponding room" });
        }
        var input = file.OpenReadStream();
        var ms = new MemoryStream();
        input.CopyTo(ms);
        var data = ms.ToArray();
        var image = new image
        {
            type = type.First().type,
            data = data
        };
        database.Add(image);
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
    
    [HttpPost("update")]
    public object UpdateImage(IFormFile file, [FromQuery] int id)
    {
        var database = new MyDbContext();
        var images = database.images.Where(image => image.typeNavigation.id == id);
        var input = file.OpenReadStream();
        var ms = new MemoryStream();
        input.CopyTo(ms);
        var data = ms.ToArray();
        image img;
        if (!images.Any())
        {
            var types = database.room_types.Where(roomType => roomType.id == id);
            if (!types.Any())
            {
                return BadRequest(new Response { message = "upload unsuccessful room type is not present on the database", success = false});
            }
            img = new image{type = types.First().type,data = data};
            database.Add(img);
        }
        else
        {
            img = images.First();
            img.data = data;
            database.Update(img);
        }
        try
        {
            var results = database.SaveChanges();
            if (results != -1)
            {
                return Ok(new Response {message = "update successful", success = true});
            }
            return BadRequest(new Response { message = "update unsuccessful", success = false});
        }
        catch (Exception)
        {
            return BadRequest(new Response { message = "update unsuccessful", success = false});
        }
    }
}