using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BolsaTrabajo
    {
        public static ML.Result BolsaTrabajoGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaBolsaTrabajos = (from bolsaTrabajo in context.BolsaTrabajoes
                                         select new
                                         {
                                             bolsaTrabajo.IdBolsaTrabajo,
                                             bolsaTrabajo.NombreBolsaTrabajo,
                                         }).ToList();
                    if (listaBolsaTrabajos.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var bolsaTrabajo in listaBolsaTrabajos)
                        {
                            ML.BolsaTrabajo bolsaTrabajoItem = new ML.BolsaTrabajo();
                            bolsaTrabajoItem.IdBolsaTrabajo = bolsaTrabajo.IdBolsaTrabajo;
                            bolsaTrabajoItem.NombreBolsaTrabajo = bolsaTrabajo.NombreBolsaTrabajo;

                            result.Objects.Add(bolsaTrabajoItem);
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
