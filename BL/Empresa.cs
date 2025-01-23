using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result EmpresaAddEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.EmpresaAdd(empresa.NombreEmpresa,empresa.EmpresaDireccion.Latitud,empresa.EmpresaDireccion.Longitud); // Llama al procedimiento almacenado
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaDeleteEF(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.EmpresaDelete(IdEmpresa); // Llama al procedimiento almacenado
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaUpdateEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.EmpresaUpdate(empresa.IdEmpresa,empresa.NombreEmpresa,empresa.EmpresaDireccion.Latitud,empresa.EmpresaDireccion.Longitud); // Llama al procedimiento almacenado
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaGetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEmpresas = context.EmpresaGetAll().ToList(); // Llama al procedimiento almacenado
                    if (listaEmpresas.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var empresa in listaEmpresas)
                        {
                            ML.Empresa empresaItem = new ML.Empresa();
                            empresaItem.IdEmpresa = empresa.IdEmpresa;
                            empresaItem.NombreEmpresa = empresa.NombreEmpresa;

                            result.Objects.Add(empresaItem);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result EmpresaGetByIdEF(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var empresa = context.EmpresaGetById(IdEmpresa).SingleOrDefault(); // Llama al procedimiento almacenado
                    if (empresa != null)
                    {
                        ML.Empresa empresaItem = new ML.Empresa();
                        empresaItem.IdEmpresa = empresa.IdEmpresa;
                        empresaItem.NombreEmpresa = empresa.NombreEmpresa;
                        empresaItem.EmpresaDireccion = new ML.EmpresaDireccion();
                        empresaItem.EmpresaDireccion.Longitud = empresa.Longitud;
                        empresaItem.EmpresaDireccion.Latitud = empresa.Latitud;

                        result.Object = empresaItem;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
