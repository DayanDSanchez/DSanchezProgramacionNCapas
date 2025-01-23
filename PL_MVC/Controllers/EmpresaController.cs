using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult EmpresaGetAll()
        {
            //Usando SP
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.EmpresaGetAllEF();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            }
            return View(empresa);
        }

        [HttpGet]
        public ActionResult FormEmpresa(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (IdEmpresa != null) //UPDATE
            {
                ML.Result result = BL.Empresa.EmpresaGetByIdEF(IdEmpresa.Value);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                }
                else
                {
                    ViewBag.MensajeError = "Ocuarrio un error";
                }
            }
            return View(empresa);
        }

        [HttpPost] //Agregar o actualizar
        public ActionResult FormEmpresa(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            if (empresa.IdEmpresa == 0)
            {
                result = BL.Empresa.EmpresaAddEF(empresa);
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
                result = BL.Empresa.EmpresaUpdateEF(empresa);
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
        public ActionResult Delete(int? IdEmpresa)
        {
            ML.Result result = new ML.Result();

            result = BL.Empresa.EmpresaDeleteEF(IdEmpresa.Value);
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
    }
}