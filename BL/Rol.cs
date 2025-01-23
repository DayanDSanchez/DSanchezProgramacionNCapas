using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaRoles = (from rol in context.Rols
                                         select new
                                         {
                                             rol.IdRol,
                                             rol.Nombre,
                                         }).ToList();
                    if (listaRoles.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var rol in listaRoles)
                        {
                            ML.Rol rolItem = new ML.Rol();
                            rolItem.IdRol = rol.IdRol;
                            rolItem.Nombre = rol.Nombre;

                            result.Objects.Add(rolItem);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }

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
    }
}
