using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Piso
    {
        public static ML.Result PisoGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaPisos = (from piso in context.Pisoes
                                              select new
                                              {
                                                  piso.IdPiso,
                                                  piso.Nombre,
                                              }).ToList();
                    if (listaPisos.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var piso in listaPisos)
                        {
                            ML.Piso pisoItem = new ML.Piso();
                            pisoItem.IdPiso = piso.IdPiso;
                            pisoItem.Nombre = piso.Nombre;

                            result.Objects.Add(pisoItem);
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
