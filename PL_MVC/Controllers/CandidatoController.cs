using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PL_MVC.Controllers
{
    public class CandidatoController : Controller
    {
        [HttpGet]
        public ActionResult GetAllWebApi()
        {
            ML.Candidato resultCandidato = new ML.Candidato();
            resultCandidato.Candidatos = new List<object>();
            using (var client = new HttpClient())
            {
                string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
                client.BaseAddress = new Uri(endPoint);
                var responseTask = client.GetAsync("Candidato/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();


                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Candidato resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Candidato>(resultItem.ToString());
                        resultCandidato.Candidatos.Add(resultItemList);
                    }
                }
            }
            return View(resultCandidato);
        }

        public ActionResult AddWebApi(ML.Candidato candidato)
        {
            using (var client = new HttpClient())
            {
                string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
                client.BaseAddress = new Uri("CandidatoEndPoint");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Add", candidato);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }

            return View("GetAll");
        }

        //public ActionResult DeleteWebApi(int IdCandidato)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
        //        client.BaseAddress = new Uri("CandidatoEndPoint");

        //        //HTTP POST
        //        var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Delete", IdCandidato);
        //        postTask.Wait();

        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetAll");
        //        }
        //    }
        //    return PartialView("Modal");
        //}

        // GET: Candidato
        [HttpGet]
        public ActionResult CandidatoGetAll()
        {
            //usando webApi
            //ML.Candidato resultCandidato = new ML.Candidato();
            //resultCandidato.Candidatos = new List<object>();
            //using (var client = new HttpClient())
            //{
            //    string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
            //    client.BaseAddress = new Uri(endPoint);
            //    var responseTask = client.GetAsync("Candidato/GetAll");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<ML.Result>();
            //        readTask.Wait();


            //        foreach (var resultItem in readTask.Result.Objects)
            //        {
            //            ML.Candidato resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Candidato>(resultItem.ToString());
            //            resultCandidato.Candidatos.Add(resultItemList);
            //        }
            //    }
            //}
            //return View(resultCandidato);

            //Usando SOAP
            string action = "http://tempuri.org/ICandidato/CandidatoGetAllEF";
            string url = "http://localhost:64228/Candidato.svc?wsdl";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            // Crear el sobre SOAP
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                       <soapenv:Header/>
                                       <soapenv:Body>
                                          <tem:CandidatoGetAllEF/>
                                       </soapenv:Body>
                                    </soapenv:Envelope>";

            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);

                        // Deserializar el XML
                        var candidatos = GetAllCandidatos(result); // Captura el objeto completo
                        return View(candidatos); // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }
            return View();


            //Usando SP
            //    ML.Candidato candidato = new ML.Candidato();
            //    ML.Result result = BL.Candidato.CandidatoGetAllEF();
            //    if (result.Correct)
            //    {
            //        candidato.Candidatos = result.Objects;
            //    }
            //    else
            //    {
            //        ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            //    }
            //    return View(candidato);
        }

        public ML.Candidato GetAllCandidatos(string xml)
        {
            var candidato1 = new ML.Candidato();
            candidato1.Candidatos = new List<object>();

            var xdoc = XDocument.Parse(xml);

            // Acceder a GetAllUsuarioResult
            //XNamespace array = "http://schemas.datacontract.org/2004/07/ML";
            XNamespace objexr = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
            var elementos = xdoc.Descendants(objexr + "anyType");


            foreach (var elem in elementos)
            {
                var candidato = new ML.Candidato();
                candidato.Universidad = new ML.Universidad();
                candidato.BolsaTrabajo = new ML.BolsaTrabajo();
                candidato.Carrera = new ML.Carrera();
                candidato.Vacante = new ML.Vacante();

                // Manejo de IdUsuario
                int idCandidato;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdCandidato")?.Value, out idCandidato);
                candidato.IdCandidato = idCandidato;
                int idUniversidad;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUniversidad")?.Value, out idUniversidad);
                candidato.Universidad.IdUniversidad = idUniversidad;
                int idBolsaTrabajo;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdBolsaTrabajo")?.Value, out idBolsaTrabajo);
                candidato.BolsaTrabajo.IdBolsaTrabajo = idBolsaTrabajo;
                int idCarrera;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdCarrera")?.Value, out idCarrera);
                candidato.Carrera.IdCarrera = idCarrera;
                int idVacante;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdVacante")?.Value, out idVacante);
                candidato.Vacante.IdVacante = idVacante;

                // Manejo de Edad
                int edad;
                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Edad")?.Value, out edad);
                candidato.Edad = Convert.ToString(edad);

                //Manejo de documentos
                candidato.Foto = Convert.FromBase64String(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Foto")?.Value);
                candidato.Curriculum = Convert.FromBase64String(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Curriculum")?.Value);

                // Acceso a otros campos
                candidato.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);
                candidato.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);
                candidato.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                candidato.Correo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Correo")?.Value ?? string.Empty);
                candidato.Telefono = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Telefono")?.Value ?? string.Empty);
                candidato.Direccion = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion")?.Value ?? string.Empty);

                candidato.Universidad.NombreUniversidad = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Universidad").Element("{http://schemas.datacontract.org/2004/07/ML}NombreUniversidad")?.Value ?? string.Empty);


                candidato.BolsaTrabajo.NombreBolsaTrabajo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}BolsaTrabajo").Element("{http://schemas.datacontract.org/2004/07/ML}NombreBolsaTrabajo")?.Value ?? string.Empty);

                candidato.Carrera.NombreCarrera = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Carrera").Element("{http://schemas.datacontract.org/2004/07/ML}NombreCarrera")?.Value ?? string.Empty);

                candidato.Vacante.NombreVacante = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Vacante").Element("{http://schemas.datacontract.org/2004/07/ML}NombreVacante")?.Value ?? string.Empty);

                candidato1.Candidatos.Add(candidato);
            }

            return candidato1; // Devuelve el objeto completo
        }

        [HttpGet]
        public ActionResult CandidatoGetAllCard()
        {
            //usando webApi
            //usando webApi
            ML.Candidato resultCandidato = new ML.Candidato();
            resultCandidato.Candidatos = new List<object>();
            using (var client = new HttpClient())
            {
                string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
                client.BaseAddress = new Uri(endPoint);
                var responseTask = client.GetAsync("Candidato/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();


                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Candidato resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Candidato>(resultItem.ToString());
                        resultCandidato.Candidatos.Add(resultItemList);
                    }
                }
            }
            return View(resultCandidato);

            //Usando soap
            //string action = "http://tempuri.org/ICandidato/CandidatoGetAllEF";
            //string url = "http://localhost:64228/Candidato.svc?wsdl";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Headers.Add("SOAPAction", action);
            //request.ContentType = "text/xml;charset=\"utf-8\"";
            //request.Accept = "text/xml";
            //request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP

            //// Crear el sobre SOAP
            //string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            //                        <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
            //                           <soapenv:Header/>
            //                           <soapenv:Body>
            //                              <tem:CandidatoGetAllEF/>
            //                           </soapenv:Body>
            //                        </soapenv:Envelope>";

            //// Enviar la solicitud
            //using (Stream stream = request.GetRequestStream())
            //{
            //    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
            //    stream.Write(content, 0, content.Length);
            //}

            //// Obtener la respuesta
            //try
            //{
            //    using (WebResponse response = request.GetResponse())
            //    {
            //        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //        {
            //            string result = reader.ReadToEnd();
            //            Console.WriteLine("Respuesta SOAP:");
            //            Console.WriteLine(result);

            //            // Deserializar el XML
            //            var candidatos = GetAllCandidatos(result); // Captura el objeto completo
            //            return View(candidatos); // Asegúrate de que tu vista esté lista para recibir este objeto
            //        }
            //    }
            //}
            //catch (WebException ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //    ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            //}
            //return View();


            //ML.Candidato candidato = new ML.Candidato();
            //ML.Result result = BL.Candidato.CandidatoGetAllEF();
            //if (result.Correct)
            //{
            //    candidato.Candidatos = result.Objects;
            //}
            //else
            //{
            //    ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            //}
            //return View(candidato);
        }
        [HttpGet]
        public ActionResult Form(int? IdCandidato)
        {
            ML.Candidato candidato = new ML.Candidato();
            candidato.Universidad = new ML.Universidad();
            candidato.Carrera = new ML.Carrera();
            candidato.BolsaTrabajo = new ML.BolsaTrabajo();
            candidato.Vacante = new ML.Vacante();

            if (IdCandidato.HasValue)
            {
                // Obtener el usuario por ID
                string action = "http://tempuri.org/ICandidato/CandidatoGetByIdEF";
                string url = "http://localhost:64228/Candidato.svc?wsdl";

                // Crear el sobre SOAP para obtener un usuario por su ID
                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:CandidatoGetByIdEF>
                         <!--Optional:-->
                         <tem:IdCandidato>{IdCandidato}</tem:IdCandidato>
                      </tem:CandidatoGetByIdEF>
                   </soapenv:Body>
                </soapenv:Envelope>";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // Enviar la solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }

                // Obtener la respuesta
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine("Respuesta SOAP:");
                            Console.WriteLine(result);

                            // Deserializar el usuario
                            candidato = GetCandidatoById(result);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
                }
            }

            if (IdCandidato != null) //UPDATE
            {
                ML.Result result = BL.Candidato.CandidatoGetByIdEF(IdCandidato.Value);
                if (result.Correct)
                {
                    candidato = (ML.Candidato)result.Object;
                    ML.Result resultUniversidad = BL.Universidad.UniversidadGetAllEFLinq();
                    if (resultUniversidad.Correct)
                    {
                        candidato.Universidad.Universidades = resultUniversidad.Objects;
                    }
                    ML.Result resultCarrera = BL.Carrera.CarreraGetAllEFLinq();
                    if (resultCarrera.Correct)
                    {
                        candidato.Carrera.Carreras = resultCarrera.Objects;
                    }
                    ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.BolsaTrabajoGetAllEFLinq();
                    if (resultBolsaTrabajo.Correct)
                    {
                        candidato.BolsaTrabajo.BolsaTrabajos = resultBolsaTrabajo.Objects;
                    }
                    ML.Result resultVacante = BL.Vacante.VacanteGetAllEFLinq();
                    if (resultVacante.Correct)
                    {
                        candidato.Vacante.Vacantes = resultVacante.Objects;
                    }
                }
                else
                {
                    ViewBag.MensajeError = "Ocuarrio un error";
                }
            }
            else //ADD
            {
                candidato.Universidad = new ML.Universidad();
                ML.Result resultUniversidad = BL.Universidad.UniversidadGetAllEFLinq();
                if (resultUniversidad.Correct)
                {
                    candidato.Universidad.Universidades = resultUniversidad.Objects;
                }
                ML.Result resultCarrera = BL.Carrera.CarreraGetAllEFLinq();
                if (resultCarrera.Correct)
                {
                    candidato.Carrera.Carreras = resultCarrera.Objects;
                }
                ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.BolsaTrabajoGetAllEFLinq();
                if (resultBolsaTrabajo.Correct)
                {
                    candidato.BolsaTrabajo.BolsaTrabajos = resultBolsaTrabajo.Objects;
                }
                ML.Result resultVacante = BL.Vacante.VacanteGetAllEFLinq();
                if (resultVacante.Correct)
                {
                    candidato.Vacante.Vacantes = resultVacante.Objects;
                }
            }

            return View(candidato); // Devuelve la vista con el usuario si existe
        }

        public ML.Candidato GetCandidatoById(string xml)
        {
            var xdoc = XDocument.Parse(xml);

            // Acceder a CandidatoGetByIdEFResult usando el namespace correcto
            var candidatoElement = xdoc.Descendants().FirstOrDefault(e =>
                e.Name.LocalName == "CandidatoGetByIdEFResult" &&
                e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");

            if (candidatoElement != null)
            {
                // Obtiene el objeto Candidato desde el XML
                var candidatoObject = candidatoElement.Descendants(XName.Get("Object", "http://schemas.datacontract.org/2004/07/SL_WCF"))
                    .FirstOrDefault();
                if (candidatoObject != null)
                {
                    return new ML.Candidato
                    {
                        IdCandidato = int.TryParse(candidatoObject.Element(XName.Get("IdCandidato", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idCandidato) ? idCandidato : 0,
                        Nombre = candidatoObject.Element(XName.Get("Nombre", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        ApellidoMaterno = candidatoObject.Element(XName.Get("ApellidoMaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        ApellidoPaterno = candidatoObject.Element(XName.Get("ApellidoPaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Correo = candidatoObject.Element(XName.Get("Correo", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Direccion = candidatoObject.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Edad = candidatoObject.Element(XName.Get("Edad", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Telefono = candidatoObject.Element(XName.Get("Telefono", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,

                        //Procesar documentos
                        Foto = Convert.FromBase64String(candidatoObject.Element(XName.Get("Foto", "http://schemas.datacontract.org/2004/07/ML"))?.Value),

                        // Procesar BolsaTrabajo
                        BolsaTrabajo = new ML.BolsaTrabajo
                        {
                            IdBolsaTrabajo = int.TryParse(candidatoObject.Element(XName.Get("IdBolsaTrabajo", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idBolsaTrabajo) ? idBolsaTrabajo : 0,
                            NombreBolsaTrabajo = candidatoObject.Element(XName.Get("NombreBolsaTrabajo", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty
                        },

                        // Procesar Carrera
                        Carrera = new ML.Carrera
                        {
                            IdCarrera = int.TryParse(candidatoObject.Element(XName.Get("IdCarrera", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idCarrera) ? idCarrera : 0,
                            NombreCarrera = candidatoObject.Element(XName.Get("NombreCarrera", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty
                        },

                        // Procesar Universidad
                        Universidad = new ML.Universidad
                        {
                            IdUniversidad = int.TryParse(candidatoObject.Element(XName.Get("IdUniversidad", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idUniversidad) ? idUniversidad : 0,
                            NombreUniversidad = candidatoObject.Element(XName.Get("NombreUniversidad", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty
                        },

                        // Procesar Vacante
                        Vacante = new ML.Vacante
                        {
                            IdVacante = int.TryParse(candidatoObject.Element(XName.Get("IdVacante", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idVacante) ? idVacante : 0,
                            NombreVacante = candidatoObject.Element(XName.Get("NombreVacante", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty
                        }
                    };
                }
            }

            return null; // O lanzar una excepción si no se encontró el usuario

            //ML.Candidato candidato = new ML.Candidato();
            //candidato.Universidad = new ML.Universidad();
            //candidato.Carrera = new ML.Carrera();
            //candidato.BolsaTrabajo = new ML.BolsaTrabajo();
            //candidato.Vacante = new ML.Vacante();

            //if (IdCandidato != null) //UPDATE
            //{
            //    ML.Result result = BL.Candidato.CandidatoGetByIdEF(IdCandidato.Value);
            //    if (result.Correct)
            //    {
            //        candidato = (ML.Candidato)result.Object;
            //        ML.Result resultUniversidad = BL.Universidad.UniversidadGetAllEFLinq();
            //        if (resultUniversidad.Correct)
            //        {
            //            candidato.Universidad.Universidades = resultUniversidad.Objects;
            //        }
            //        ML.Result resultCarrera = BL.Carrera.CarreraGetAllEFLinq();
            //        if (resultCarrera.Correct)
            //        {
            //            candidato.Carrera.Carreras = resultCarrera.Objects;
            //        }
            //        ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.BolsaTrabajoGetAllEFLinq();
            //        if (resultBolsaTrabajo.Correct)
            //        {
            //            candidato.BolsaTrabajo.BolsaTrabajos = resultBolsaTrabajo.Objects;
            //        }
            //        ML.Result resultVacante = BL.Vacante.VacanteGetAllEFLinq();
            //        if (resultVacante.Correct)
            //        {
            //            candidato.Vacante.Vacantes = resultVacante.Objects;
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.MensajeError = "Ocuarrio un error";
            //    }
            //}
            //else //ADD
            //{
            //    candidato.Universidad = new ML.Universidad();
            //    ML.Result resultUniversidad = BL.Universidad.UniversidadGetAllEFLinq();
            //    if (resultUniversidad.Correct)
            //    {
            //        candidato.Universidad.Universidades = resultUniversidad.Objects;
            //    }
            //    ML.Result resultCarrera = BL.Carrera.CarreraGetAllEFLinq();
            //    if (resultCarrera.Correct)
            //    {
            //        candidato.Carrera.Carreras = resultCarrera.Objects;
            //    }
            //    ML.Result resultBolsaTrabajo = BL.BolsaTrabajo.BolsaTrabajoGetAllEFLinq();
            //    if (resultBolsaTrabajo.Correct)
            //    {
            //        candidato.BolsaTrabajo.BolsaTrabajos = resultBolsaTrabajo.Objects;
            //    }
            //    ML.Result resultVacante = BL.Vacante.VacanteGetAllEFLinq();
            //    if (resultVacante.Correct)
            //    {
            //        candidato.Vacante.Vacantes = resultVacante.Objects;
            //    }
            //}
            //return View(candidato);
        }
        [HttpPost] //Agregar o actualizar
        public ActionResult Form(ML.Candidato candidato)
        {
            HttpPostedFileBase file = Request.Files["ImagenUsuario"];
            HttpPostedFileBase cv = Request.Files["CurriculumCandidato"];

            // Imagen
            if (file != null && file.ContentLength > 0)
            {
                candidato.FotoString = Convert.ToBase64String(ConvertirAArrayBytes(file));
            }
            else
            {
                string ImagenActual = Request.Form["ImagenActual"];
                if (!string.IsNullOrEmpty(ImagenActual))
                {
                    candidato.FotoString = ImagenActual; // Usar FotoString ya que este es el campo que envías
                }
                else
                {
                    candidato.FotoString = null; // Asegurarse de que sea null si no hay imagen
                }
            }

            // Curriculum
            if (cv != null && cv.ContentLength > 0)
            {
                candidato.Curriculum = ConvertirAArrayBytes(cv);
            }
            else
            {
                candidato.Curriculum = null; // Dejar null si no hay curriculum
            }

            //Soap
            string url = "http://localhost:64228/Candidato.svc?wsdl"; // URL del servicio
            string soapEnvelope;
            string action;

            // Verificar si IdCandidato es 0, lo que indica que es un nuevo candidato
            if (candidato.IdCandidato == 0)
            {
                using (var client = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
                    client.BaseAddress = new Uri(endPoint);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Add", candidato);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "El resgistro se agrego correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Error al agregar registro";
                    }
                }

                //return View("GetAll");
                // Crear el sobre SOAP para agregar un nuevo candidato
                action = "http://tempuri.org/ICandidato/CandidatoAddEF";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:CandidatoAddEF>
                         <tem:candidato>
                            <ml:ApellidoMaterno>{candidato.ApellidoMaterno}</ml:ApellidoMaterno>
                            <ml:ApellidoPaterno>{candidato.ApellidoPaterno}</ml:ApellidoPaterno>
                            <ml:BolsaTrabajo>
                               <ml:IdBolsaTrabajo>{candidato.BolsaTrabajo.IdBolsaTrabajo}</ml:IdBolsaTrabajo>
                            </ml:BolsaTrabajo>
                            <ml:Carrera>
                               <ml:IdCarrera>{candidato.Carrera.IdCarrera}</ml:IdCarrera>
                            </ml:Carrera>
                            <ml:Correo>{candidato.Correo}</ml:Correo>
                            <ml:Curriculum>{(candidato.Curriculum != null ? Convert.ToBase64String(candidato.Curriculum) : "")}</ml:Curriculum>
                            <ml:Direccion>{candidato.Direccion}</ml:Direccion>
                            <ml:Edad>{candidato.Edad}</ml:Edad>
                            <ml:FotoString>{(candidato.FotoString != null ? candidato.FotoString : "")}</ml:FotoString>
                            <ml:Nombre>{candidato.Nombre}</ml:Nombre>
                            <ml:Telefono>{candidato.Telefono}</ml:Telefono>
                            <ml:Universidad>
                               <ml:IdUniversidad>{candidato.Universidad.IdUniversidad}</ml:IdUniversidad>
                            </ml:Universidad>
                            <ml:Vacante>
                               <ml:IdVacante>{candidato.Vacante.IdVacante}</ml:IdVacante>
                            </ml:Vacante>
                         </tem:candidato>
                      </tem:CandidatoAddEF>
                   </soapenv:Body>
                </soapenv:Envelope>";
            }
            else
            {

                using (var client = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["CandidatoEndPoint"];
                    client.BaseAddress = new Uri(endPoint);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Candidato>("Candidato/Update/", candidato);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "El resgistro se actualizo correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Error al actualizar registro";
                    }
                }

                // Crear el sobre SOAP para actualizar un candidato existente
                action = "http://tempuri.org/ICandidato/CandidatoUpdateEF";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <tem:CandidatoUpdateEF>
                         <tem:candidato>
                            <ml:ApellidoMaterno>{candidato.ApellidoMaterno}</ml:ApellidoMaterno>
                            <ml:ApellidoPaterno>{candidato.ApellidoPaterno}</ml:ApellidoPaterno>
                            <ml:BolsaTrabajo>
                               <ml:IdBolsaTrabajo>{candidato.BolsaTrabajo.IdBolsaTrabajo}</ml:IdBolsaTrabajo>
                            </ml:BolsaTrabajo>
                            <ml:Carrera>
                               <ml:IdCarrera>{candidato.Carrera.IdCarrera}</ml:IdCarrera>
                            </ml:Carrera>
                            <ml:Correo>{candidato.Correo}</ml:Correo>
                            <ml:Curriculum>{(candidato.Curriculum != null ? Convert.ToBase64String(candidato.Curriculum) : null)}</ml:Curriculum>
                            <ml:Direccion>{candidato.Direccion}</ml:Direccion>
                            <ml:Edad>{candidato.Edad}</ml:Edad>
                            <ml:FotoString>{(candidato.FotoString != null ? candidato.FotoString : "")}</ml:FotoString>
                            <ml:IdCandidato>{candidato.IdCandidato}</ml:IdCandidato>
                            <ml:Nombre>{candidato.Nombre}</ml:Nombre>
                            <ml:Telefono>{candidato.Telefono}</ml:Telefono>
                            <ml:Universidad>
                               <ml:IdUniversidad>{candidato.Universidad.IdUniversidad}</ml:IdUniversidad>
                            </ml:Universidad>
                            <ml:Vacante>
                               <ml:IdVacante>{candidato.Vacante.IdVacante}</ml:IdVacante>
                            </ml:Vacante>
                         </tem:candidato>
                      </tem:CandidatoUpdateEF>
                   </soapenv:Body>
                </soapenv:Envelope>";
            }

            // Enviar la solicitud al servicio SOAP
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }

                // Obtener la respuesta
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);

                        // Asignar mensaje de éxito
                        ViewBag.Message = candidato.IdCandidato == 0 ? "Candidato agregado correctamente." : "Candidato actualizado correctamente.";
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Message = "Error al agregar/actualizar el registro"; // Mostrar mensaje de error en la vista
            }

            return PartialView("Modal");


            //if (candidato.IdCandidato == 0)
            //{
            //    result = BL.Candidato.CandidatoAddEF(candidato);
            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "El resgistro se agrego correctamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Error al agregar registro";
            //    }
            //}
            //else
            //{
            //    result = BL.Candidato.CandidatoUpdateEF(candidato);
            //    if (result.Correct)
            //    {
            //        ViewBag.Message = "El resgistro se actualizo correctamente";
            //    }
            //    else
            //    {
            //        ViewBag.Message = "Error al actualizar registro";
            //    }
            //}
            //return PartialView("Modal");
        }
        [HttpGet]//Delete
        public ActionResult Delete(int? IdCandidato)
        {
            string url = "http://localhost:64228/Candidato.svc?wsdl";
            string action = "http://tempuri.org/ICandidato/CandidatoDeleteEF";

            // Crear el sobre SOAP para eliminar un candidato
            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
               <soapenv:Header/>
               <soapenv:Body>
                  <tem:CandidatoDeleteEF>
                     <tem:IdCandidato>{IdCandidato.Value}</tem:IdCandidato>
                  </tem:CandidatoDeleteEF>
               </soapenv:Body>
            </soapenv:Envelope>";

            try
            {
                // Enviar la solicitud SOAP
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // Escribir el sobre SOAP en la solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }

                // Obtener la respuesta
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("Respuesta SOAP:");
                        Console.WriteLine(result);

                        ViewBag.Message = "Candidato eliminado con éxito.";
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ViewBag.Message = "Ocurrió un error al eliminar el candidato.";
            }

            return PartialView("Modal");

            //ML.Result result = new ML.Result();

            //result = BL.Candidato.CandidatoDeleteEF(IdCandidato.Value);

            //if (result.Correct)
            //{
            //    ViewBag.Message = "Eliminado con exito";
            //}
            //else
            //{
            //    ViewBag.Message = "Ocuarrio un error";
            //}

            //return PartialView("Modal");
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }
        public ActionResult DownloadCurriculum(int id)
        {
            ML.Result result = BL.Candidato.CandidatoGetByIdEF(id);

            if (!result.Correct || result.Object == null)
            {
                return HttpNotFound();
            }

            ML.Candidato candidato = (ML.Candidato)result.Object;

            if (candidato.Curriculum == null)
            {
                return HttpNotFound();
            }

            var fileContent = candidato.Curriculum;
            var fileName = "Curriculum_" + candidato.Nombre + ".pdf";

            return File(fileContent, "application/pdf", fileName);
        }

        //Agendar entrevista
        [HttpGet]
        public ActionResult AgendarEntrevista(int? IdVacante)
        {
            ML.Candidato candidato = new ML.Candidato();
            candidato.Vacante = new ML.Vacante();
            candidato.Vacante.IdVacante = IdVacante ?? 0;
            ML.Result result = BL.Candidato.CandidatoGetByVacanteEF(IdVacante);

            ML.Result resultVacante = BL.Vacante.VacanteGetAllEFLinq();

            if (resultVacante.Correct)
            {
                candidato.Vacante.Vacantes = resultVacante.Objects; //llenando la lista de Roles
            }

            if (result.Correct)
            {
                candidato.Candidatos = result.Objects;
            }
            else
            {
                ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            }

            return View(candidato);
        }
        [HttpGet]
        public ActionResult FormAgendar(int? IdCandidato)
        {
            ML.Cita cita = new ML.Cita();
            cita.Candidato = new ML.Candidato();
            cita.Piso = new ML.Piso();
            cita.EstatusCita = new ML.EstatusCita();
            //candidato.Universidad = new ML.Universidad();
            //candidato.Carrera = new ML.Carrera();
            //candidato.BolsaTrabajo = new ML.BolsaTrabajo();
            //candidato.Vacante = new ML.Vacante();

            if (IdCandidato != null) //UPDATE
            {
                ML.Result resultCandidato = BL.Candidato.CandidatoGetByIdEF(IdCandidato.Value);
                if (resultCandidato.Correct)
                {
                    cita.Candidato = (ML.Candidato)resultCandidato.Object; // Asignar los datos del candidato

                    // Obtener la cita del candidato
                    ML.Result resultCita = BL.Cita.CitaGetByIdCandidatoLinq(IdCandidato.Value);
                    if (resultCita.Correct && resultCita.Object != null)
                    {
                        ML.Cita citaExistente = (ML.Cita)resultCita.Object;

                        // Asignar los datos de la cita si ya existe
                        cita.IdCita = citaExistente.IdCita;
                        cita.FechaHora = citaExistente.FechaHora;
                        cita.URL = citaExistente.URL;
                        cita.Piso.IdPiso = citaExistente.Piso.IdPiso;
                        cita.EstatusCita.IdEstatusCita = citaExistente.EstatusCita.IdEstatusCita;
                    }
                    ML.Result resultPiso = BL.Piso.PisoGetAllEFLinq();
                    if (resultPiso.Correct)
                    {
                        cita.Piso.Pisos = resultPiso.Objects;
                    }
                    ML.Result resultEstatusCita = BL.EstatusCita.EstatusCitaGetAllEFLinq();
                    if (resultEstatusCita.Correct)
                    {
                        cita.EstatusCita.EstatusCitas = resultEstatusCita.Objects;
                    }
                }
                else
                {
                    ViewBag.MensajeError = "Ocuarrio un error";
                }
            }
            else //ADD
            {
                cita.Piso = new ML.Piso();
                ML.Result resultPiso = BL.Piso.PisoGetAllEFLinq();
                if (resultPiso.Correct)
                {
                    cita.Piso.Pisos = resultPiso.Objects;
                }
                ML.Result resultEstatusCita = BL.EstatusCita.EstatusCitaGetAllEFLinq();
                if (resultEstatusCita.Correct)
                {
                    cita.EstatusCita.EstatusCitas = resultEstatusCita.Objects;
                }
            }
            return View(cita);
        }
        public ActionResult FormAgendar(ML.Cita cita)
        {
            ML.Result result = new ML.Result();

            if (cita.IdCita == 0)
            {
                result = BL.Cita.CitaAddEF(cita);
                if (result.Correct)
                {
                    ViewBag.Message = "El resgistro se agrego correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al agregar registro";
                }
            }
            else
            {
                result = BL.Cita.CitaUpdateEF(cita);
                if (result.Correct)
                {
                    ViewBag.Message = "El resgistro se actualizo correctamente";
                }
                else
                {
                    ViewBag.Message = "Error al actualizar registro";
                }
            }
            return PartialView("Modal");
        }
    }
}