using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEstados = context.EstadoGetAll().ToList();
                    if (listaEstados.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var estado in listaEstados)
                        {
                            ML.Estado estadoItem = new ML.Estado();
                            estadoItem.IdEstado = estado.IdEstado;
                            estadoItem.Nombre = estado.Nombre;
                            result.Objects.Add(estadoItem);
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

        public static ML.Result EstadosGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEstados = (from estado in context.Estadoes
                                        select new
                                      {
                                          estado.IdEstado,
                                          estado.Nombre,
                                      }).ToList();
                    if (listaEstados.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var estado in listaEstados)
                        {
                            ML.Estado estadoItem = new ML.Estado();
                            estadoItem.IdEstado = estado.IdEstado;
                            estadoItem.Nombre = estado.Nombre;

                            result.Objects.Add(estadoItem);
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
