using BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Excel()
        {
            //GetAll
            HttpPostedFileBase excel = Request.Files["excel"];//Cahando archivo
            string extension = Path.GetExtension(excel.FileName);//Obteniendo la extension del archivo

            string extensionWebConfig = ConfigurationManager.AppSettings["ExtensionExcel"];
            if (Session["RutaExcel"] == null)
            {

                if (extensionWebConfig == extension)
                {
                    string pathExcel = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excel.FileName) + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";

                    if (!System.IO.File.Exists(pathExcel))
                    {
                        excel.SaveAs(pathExcel);//Guardando copia de archivo

                        string cadenaConnection = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + pathExcel;

                        ML.Result resultLeerExcel = BL.Usuario.LeerExcel(cadenaConnection);

                        if (resultLeerExcel.Correct)
                        {
                            ML.Result resultValidar = BL.Usuario.ValidarExcel(resultLeerExcel.Objects);
                            if (resultValidar.Objects.Count != 0)
                            {
                                //Todo bien
                                Session["RutaExcel"] = pathExcel;
                                ViewBag.Message = "Todo bien";
                            }
                            else
                            {
                                //Errores
                                foreach (ML.ResultExcel item in resultValidar.Objects)
                                {
                                    ViewBag.Message += $"error en registro: {item.GetType().GetProperty("NumeroRegistro").GetValue(item)} - {item.GetType().GetProperty("MensajeError").GetValue(item)}";
                                }
                            }
                            return PartialView("~/Views/Usuario/Modal.cshtml");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Error al leer archivo";
                            return PartialView("~/Views/Usuario/Modal.cshtml");
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "El archivo Ya existe";
                        return PartialView("~/Views/Usuario/Modal.cshtml");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Tipo de archivo no Valido";
                    return PartialView("~/Views/Usuario/Modal.cshtml");
                }
                
            }
            else
            {
                //Insertar 
                string cadenaConnection = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + Session["RutaExcel"];
                ML.Result resultLeerExcel = BL.Usuario.LeerExcel(cadenaConnection);

                if (resultLeerExcel.Correct)
                {
                    foreach (ML.Usuario usuario in resultLeerExcel.Objects)
                    {
                        ML.Result result = BL.Usuario.AddEF(usuario);
                        if (!result.Correct)
                        {
                            //Errores
                            ViewBag.ErrorMessage = "Error al archivo";
                        }
                        else
                        {
                            ViewBag.Message = "Datos insertados con exito";
                        }
                    }
                }
            }
            return PartialView("~/Views/Usuario/Modal.cshtml");
        }
    }
}