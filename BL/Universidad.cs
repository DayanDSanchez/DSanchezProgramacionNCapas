using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Universidad
    {
        public static ML.Result UniversidadGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaUniversidades = (from universidad in context.Universidads
                                              select new
                                              {
                                                  universidad.IdUniversidad,
                                                  universidad.NombreUniversidad,
                                              }).ToList();
                    if (listaUniversidades.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var universidad in listaUniversidades)
                        {
                            ML.Universidad universidadItem = new ML.Universidad();
                            universidadItem.IdUniversidad = universidad.IdUniversidad;
                            universidadItem.NombreUniversidad = universidad.NombreUniversidad;

                            result.Objects.Add(universidadItem);
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
        public static ML.Result UniversidadGetByIdEFLinq(int IdUniversidad)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var universidadResult = (from universidad in context.Universidads
                                         where universidad.IdUniversidad == IdUniversidad
                                             select universidad ).SingleOrDefault();
                    if (universidadResult != null)
                    {

                        ML.Universidad universidadItem = new ML.Universidad ();
                        universidadItem.IdUniversidad = universidadResult.IdUniversidad;
                        universidadItem.NombreUniversidad = universidadResult.NombreUniversidad;

                        result.Object = universidadItem;

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
