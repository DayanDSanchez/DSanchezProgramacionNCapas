using ML;
using System;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using System.Web.UI.WebControls;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll(string Nombre, string ApellidoPaterno, string ApellidoMaterno, int? IdRol)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = Nombre ?? "";
            usuario.ApellidoPaterno = ApellidoPaterno ?? "";
            usuario.ApellidoPaterno = ApellidoPaterno == null ? "" : ApellidoPaterno;
            usuario.ApellidoMaterno = ApellidoMaterno ?? "";
            usuario.Rol.IdRol = IdRol ?? 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol);

            ML.Result resultRol = BL.Rol.GetAllEFLinq();

            if (resultRol.Correct)
            {
                usuario.Rol.Rols = resultRol.Objects; //llenando la lista de Roles
            }

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            if (IdUsuario != null) //UPDATE
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Sexo = usuario.Sexo?.Trim().ToUpper();
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    if (resultMunicipio.Correct)
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    }
                    else
                    {
                        usuario.Direccion.Colonia.Municipio.Municipios = new List<object>();
                    }
                        ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    if (resultColonia.Correct)
                    {
                        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                    }
                }
                else
                {
                    ViewBag.MensajeError = "Ocuarrio un error";
                }
            }

            ML.Result resultEstado = BL.Estado.EstadoGetAllEF();
            if (resultEstado.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
            }

            ML.Result resultRol = BL.Rol.GetAllEFLinq();
            if (resultRol.Correct)
            {
                usuario.Rol.Rols = resultRol.Objects;
            }
            else
            {
                usuario.Rol.Rols = new List<object>();
            }
            return View(usuario);
        }

        [HttpPost] //Agregar o actualizar
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            //DateTime auxiliar = DateTime.ParseExact(usuario.FechaNacimiento, "dd-mm-yyyy", CultureInfo.InvariantCulture);
            //usuario.FechaNacimiento = auxiliar.ToString("dd-MM-yyyy");

            HttpPostedFileBase file = Request.Files["ImagenUsua"];

            if (file != null && file.ContentLength > 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }
            else
            {
                string ImagenActual = Request.Form["ImagenActual"];
                if (!string.IsNullOrEmpty(ImagenActual))
                {
                    usuario.Imagen = Convert.FromBase64String(ImagenActual);
                }
            }

            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.AddEF(usuario);
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
                result = BL.Usuario.UpdateEF(usuario);
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

        [HttpGet]//Delete
        public ActionResult Delete(int? IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.DeleteEF(IdUsuario.Value);

            if (result.Correct)
            {
                ViewBag.Message = "Eliminado con exito";
            }
            else
            {
                ViewBag.Message = "Ocuarrio un error";
            }

            return PartialView("Modal");
        }

        public JsonResult GetMunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambioStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.CambioStatus(IdUsuario, Status);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

        [HttpGet]
        public ActionResult Reporting(string Nombre, string ApellidoPaterno, string ApellidoMaterno, int? IdRol)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = Nombre ?? "";
            usuario.ApellidoPaterno = ApellidoPaterno ?? "";
            usuario.ApellidoPaterno = ApellidoPaterno == null ? "" : ApellidoPaterno;
            usuario.ApellidoMaterno = ApellidoMaterno ?? "";
            usuario.Rol.IdRol = IdRol ?? 0;

            ML.Result result = BL.Usuario.GetAllEF(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Rol.IdRol);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\UsuarioReport.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Usuario", result.Objects));
            ViewBag.ReportViewer = reportViewer;
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {

            }
            return View(usuario);
        }

    }
}