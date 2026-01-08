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
        [HttpGet]
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EnviarEmail()
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("dayandiegos@gmail.com", "yiltckoipgxcvlpa"),
                    EnableSsl = true
                };

                var mensaje = new MailMessage
                {
                    From = new MailAddress("dayandiegos@gmail.com", "displayName"),
                    Subject = "Prueba",
                    Body = @"<!DOCTYPE html>
<html>
<head>
  <meta charset='UTF-8'>
  <style>
    body {
        font-family: 'Segoe UI', Arial, sans-serif;
        background-color: #f0f2f5;
        margin: 0;
        padding: 0;
    }
    .container {
        max-width: 600px;
        margin: 40px auto;
        background: #f1f0f0;
        border-radius: 12px;
        overflow: hidden;
        box-shadow: 0 4px 15px rgba(0,0,0,0.1);
    }
    .header {
        background: linear-gradient(135deg, #007bff, #0056b3);
        padding: 20px;
        text-align: center;
        color: #ffffff;
    }
    .header h1 {
        margin: 0;
        font-size: 22px;
    }
    .body {
        padding: 30px;
        color: #333333;
        text-align: center;
    }
    .body h2 {
        color: #007bff;
        margin-bottom: 15px;
    }
    .body p {
        font-size: 15px;
        line-height: 1.6;
        margin: 0 0 20px 0;
    }
    .btn {
        display: inline-block;
        padding: 12px 25px;
        background-color: #007bff;
        color: #ffffff !important;
        text-decoration: none;
        font-weight: bold;
        border-radius: 6px;
        transition: background 0.3s ease;
    }
    .btn:hover {
        background-color: #0056b3;
    }
    .footer {
        background: #f8f9fa;
        padding: 15px;
        text-align: center;
        font-size: 12px;
        color: #777777;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>
      <h1>🌴 Sistema de Vacaciones</h1>
    </div>
    <div class='body'>
      <h2>Nueva Solicitud de Vacaciones</h2>
      <p>Hola {NombreJefe}</p>
      <p><strong>{NombreEmpleado}</strong> ha registrado una <br/> 
         solicitud de vacaciones.</p>

      <a href='' class='btn'>Revisar solicitud</a>
    </div>
    <div class='footer'>
      <p>© 2025 Sistema de Vacaciones</p>
    </div>
  </div>
</body>
</html>",
                    IsBodyHtml = true
                };

                mensaje.To.Add("dayandiegos@gmail.com");
                smtpClient.Send(mensaje);

            }
            catch (Exception) {
                ViewBag.Message = "Error al enviar correo";
            }
            return RedirectToAction("SendEmail");
        }
    }
}