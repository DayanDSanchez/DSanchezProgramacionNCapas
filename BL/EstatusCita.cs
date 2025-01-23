using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusCita
    {
        public static ML.Result EstatusCitaGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEstatusCitas = (from estatusCita in context.EstatusCitas
                                      select new
                                      {
                                          estatusCita.IdEstatusCita,
                                          estatusCita.Nombre,
                                      }).ToList();
                    if (listaEstatusCitas.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var estatusCita in listaEstatusCitas)
                        {
                            ML.EstatusCita estatusCitaItem = new ML.EstatusCita();
                            estatusCitaItem.IdEstatusCita = estatusCita.IdEstatusCita;
                            estatusCitaItem.Nombre = estatusCita.Nombre;

                            result.Objects.Add(estatusCitaItem);
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
