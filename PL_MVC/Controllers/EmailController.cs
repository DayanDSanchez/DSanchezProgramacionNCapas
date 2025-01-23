using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult EnviarEmail()
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("dayan.ddsr@gmail.com", "qoxejksiwdubxnjz"),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress("dayan.ddsr@gmail.com", "displayName"),
                    Subject = "Cita Agendada",
                    Body = "Cita Agendada",
                    IsBodyHtml = false
                };

                mensaje.To.Add("dayan.ddsr@gmail.com");
                smtpClient.Send(mensaje);

            }
            catch (Exception) {
                ViewBag.Message = "Error al enviar correo";
            }
            return PartialView("Modal");
        }
    }
}