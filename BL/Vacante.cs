using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vacante
    {
        public static ML.Result VacanteGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaVacantes = (from vacante in context.Vacantes
                                                select new
                                                {
                                                    vacante.IdVacante,
                                                    vacante.NombreVacante,
                                                    vacante.FechaPublicacion,
                                                    vacante.FechaLimite,
                                                    vacante.UrlVacante,
                                                    vacante.IdEstatusVacante,
                                                }).ToList();
                    if (listaVacantes.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var vacante in listaVacantes)
                        {
                            ML.Vacante vacanteItem = new ML.Vacante();
                            vacanteItem.IdVacante = vacante.IdVacante;
                            vacanteItem.NombreVacante = vacante.NombreVacante;
                            //DateTime auxiliar = DateTime.ParseExact(vacante.FechaPublicacion.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                            //vacanteItem.FechaPublicacion = auxiliar.ToString("dd-MM-yyyy");
                            //DateTime auxiliar2 = DateTime.ParseExact(vacante.FechaLimite.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            //vacanteItem.FechaLimite = auxiliar2.ToString("dd-MM-yyyy");
                            vacanteItem.UrlVacante = vacante.UrlVacante;
                            //vacanteItem.EstatusVacante = new ML.EstatusVacante();
                            //vacanteItem.EstatusVacante.IdEstatusVacante = vacante.IdEstatusVacante;

                            result.Objects.Add(vacanteItem);
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
