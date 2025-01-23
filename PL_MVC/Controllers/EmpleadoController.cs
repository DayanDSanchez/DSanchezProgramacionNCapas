using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult EmpleadoGetAll()
        {
            //Usando SP
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.EmpleadoGetAllEF();
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                ViewBag.MensajeError = "No se ecnontraron registros" + result.ErrorMessage;
            }
            return View(empleado);
        }
        [HttpPost]
        //public ActionResult SubirTxt(HttpPostedFileBase txtFile)
        //{
        //    if (txtFile != null && txtFile.ContentLength > 0)
        //    {
        //        try
        //        {
        //            // Leer el archivo línea por línea
        //            using (StreamReader sr = new StreamReader(txtFile.InputStream))
        //            {
        //                string line;
        //                int lineNumber = 0;
        //                List<ML.Empleado> empleados = new List<ML.Empleado>();

        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    if (lineNumber == 0)
        //                    {
        //                        // Ignorar la primera línea (encabezados)
        //                        lineNumber++;
        //                        continue;
        //                    }

        //                    // Separar los valores usando el delimitador '|'
        //                    string[] values = line.Split('|');
        //                    if (values.Length == 5)
        //                    {
        //                        ML.Empleado empleado = new ML.Empleado
        //                        {
        //                            RFC = values[0],
        //                            NumeroEmpleado = values[1],
        //                            Nombre = values[2],
        //                            FechaControl = values[3],
        //                            Salario = values[4]
        //                        };

        //                        empleados.Add(empleado);
        //                    }
        //                    else
        //                    {
        //                        ViewBag.Message = $"Error en formato en la línea {lineNumber + 1}.";
        //                        return View("EmpleadoGetAll");
        //                    }

        //                    lineNumber++;
        //                }

        //                // Insertar empleados en la base de datos
        //                foreach (var empleado in empleados)
        //                {
        //                    ML.Result result = BL.Empleado.AddEF(empleado);
        //                    if (!result.Correct)
        //                    {
        //                        ViewBag.Message = $"Error al insertar empleado con RFC: {empleado.RFC}";
        //                        return View("EmpleadoGetAll");
        //                    }
        //                }

        //                ViewBag.Message = "Archivo procesado e insertado correctamente.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = $"Ocurrió un error: {ex.Message}";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "No se seleccionó ningún archivo.";
        //    }
        //    return PartialView("Modal");
        //}
        public ActionResult SubirTxt(HttpPostedFileBase txtFile)
        {
            if (txtFile != null && txtFile.ContentLength > 0)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(txtFile.InputStream))
                    {
                        string line;
                        int lineNumber = 0;
                        List<ML.Empleado> empleados = new List<ML.Empleado>();
                        List<string> errores = new List<string>();

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (lineNumber == 0)
                            {
                                // Ignorar encabezados
                                lineNumber++;
                                continue;
                            }

                            string[] values = line.Split('|');
                            if (values.Length == 5)
                            {
                                // Validar campos
                                string rfc = values[0];
                                string numeroEmpleado = values[1];
                                string nombre = values[2];
                                string fechaControl = values[3];
                                string salario = values[4];

                                if (string.IsNullOrWhiteSpace(rfc) || rfc.Length != 13)
                                {
                                    errores.Add($"Línea {lineNumber + 1}: RFC inválido.");
                                    continue;
                                }

                                if (!DateTime.TryParse(fechaControl, out DateTime fechaValida))
                                {
                                    errores.Add($"Línea {lineNumber + 1}: FechaControl inválida.");
                                    continue;
                                }

                                if (!decimal.TryParse(salario, out decimal salarioValido) || salarioValido <= 0)
                                {
                                    errores.Add($"Línea {lineNumber + 1}: Salario inválido.");
                                    continue;
                                }

                                // Crear empleado válido
                                ML.Empleado empleado = new ML.Empleado
                                {
                                    RFC = rfc,
                                    NumeroEmpleado = numeroEmpleado,
                                    Nombre = nombre,
                                    FechaControl = fechaValida.ToString("yyyy-MM-dd"),
                                    Salario = salarioValido
                                };

                                empleados.Add(empleado);
                            }
                            else
                            {
                                errores.Add($"Línea {lineNumber + 1}: Formato incorrecto.");
                            }

                            lineNumber++;
                        }

                        // Mostrar errores si existen
                        if (errores.Count > 0)
                        {
                            ViewBag.Message = string.Join("<br>", errores);
                            return PartialView("Modal");
                        }

                        // Insertar empleados en la base de datos
                        foreach (var empleado in empleados)
                        {
                            ML.Result result = BL.Empleado.AddEF(empleado);
                            if (!result.Correct)
                            {
                                ViewBag.Message = $"Error al insertar empleado con RFC: {empleado.RFC}";
                                return PartialView("Modal");
                            }
                        }

                        ViewBag.Message = "Archivo procesado e insertado correctamente.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Ocurrió un error: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "No se seleccionó ningún archivo.";
            }
            return PartialView("Modal");
        }


    }
}