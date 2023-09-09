using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace INF272_PracticalAssignment4.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SendMail(string name, string surname, string email)
        {
            //Generate random password
            string generatedpass = RandomString();

            var senderEmail = new MailAddress("me@mail.com"); //your email goes here
            var receiverEmail = new MailAddress(email, "ReceiverNameHere");
            var password = "mypassword"; //your email password goes here
            var sub = "Your generated password";
            var body = "Hi there " + name + " " + surname + ", here is your generated password: " + generatedpass; //put password here
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com", //this might change accordingly
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }
            return View();
        }

        private string RandomString()
        {
            //For each session, use that session's variable instead of the other.
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //session 1
            //var numbers = "0123456789"; //session 2
            //var symbols = "!@#$%^&*()-=+"; //session 3
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < 8; i++)
            {
                //session 1:
                stringChars[i] = chars[random.Next(chars.Length)];
                //session 2: 
                //stringChars[i] = numbers[random.Next(numbers.Length)]; 
                //session 3: 
                //stringChars[i] = symbols[random.Next(symbols.Length)]; 
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}