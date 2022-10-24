using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using API.Entities;

namespace Thembelihle_API
{
    public class Utils
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, IEnumerable<byte> passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computerHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computerHash.SequenceEqual(passwordHash);
        }

        public static bool SendEmail(string email, string message, string subject)
        {
            try
            {
                var mm = new MailMessage();
                var sc = new SmtpClient("smtp.gmail.com");
                mm.From = new MailAddress("thembelihle.do.not.reply@gmail.com");
                mm.To.Add(email);
                mm.Subject = subject;
                mm.Body = message;
                sc.Port = 587;
                sc.Credentials = new NetworkCredential("thembelihle.do.not.reply@gmail.com", "kluihqnsopnsgkgy");
                sc.EnableSsl = true;
                sc.Send(mm);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static int GenerateNumber()
        {
            var number = "";
            for (var i = 0; i < 6; i++)
            {
                number += new Random().Next(9);
            }
            return int.Parse(number);
        }
        public static string GenerateImageLink(string host, List<image> images)
        {
            if (images.Any())
            {
                return "https://" + host + "/api/images?id=" + images.First().id;
            }
            return "";
        
        }
    }
}