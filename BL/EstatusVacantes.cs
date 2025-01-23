using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusVacantes
    {
        public static ML.Result EstatusVacantesGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEstatusVacantes = (from estatusVacante in context.EstatusVacantes
                                              select new
                                              {
                                                  estatusVacante.IdEstatusVacante,
                                                  estatusVacante.Nombre,
                                              }).ToList();
                    if (listaEstatusVacantes.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var estatusVacante in listaEstatusVacantes)
                        {
                            ML.EstatusVacante estatusVacanteItem = new ML.EstatusVacante();
                            estatusVacanteItem.IdEstatusVacante = estatusVacante.IdEstatusVacante;
                            estatusVacanteItem.Nombre = estatusVacante.Nombre;

                            result.Objects.Add(estatusVacanteItem);
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
