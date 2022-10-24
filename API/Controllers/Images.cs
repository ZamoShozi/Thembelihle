// using API.Context;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API.Controllers;
//
// [ApiController]
// [Route("api/[controller]")]
// public class Images : Controller
// {
//     [HttpGet]
//     public object GetImage([FromQuery] int id)
//     {
//         var db = new MyDbContext();
//         var images = db.Images.Where(image => image.id == id);
//         if (images.Any())
//         {
//             return File(images.First().data, "image/png");
//         }
//         return BadRequest("Image not found");
//     }
//     
//     [HttpPost("insert")]
//     public object InsertImage(IFormFile file)
//     {
//         var input = file.OpenReadStream();
//         return "";
//         // var ms = new MemoryStream();
//         // input.CopyTo(ms);
//         // var data = ms.ToArray();
//         // var database = new Database();
//         // var image = new Repository.Models.Image {Data = data};
//         // database.Add(image);
//         // var results = database.SaveChanges();
//         // if (results != -1)
//         // {
//         //     return Ok(new Response {Data = new {message = "upload successful"}, Success = true});
//         // }
//         // return BadRequest(new Response {Data = new {message = "upload unsuccessful"}, Success = false});
//     }
//     
//     [HttpPost("update")]
//     public object UpdateImage()
//     {
//         return "ok";
//     }
// }