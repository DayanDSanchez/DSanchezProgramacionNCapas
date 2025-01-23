using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Carrera
    {
        public static ML.Result CarreraGetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaCarreras = (from carrera in context.Carreras
                                              select new
                                              {
                                                  carrera.IdCarrera,
                                                  carrera.NombreCarrera,
                                              }).ToList();
                    if (listaCarreras.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var carrera in listaCarreras)
                        {
                            ML.Carrera carreraItem = new ML.Carrera();
                            carreraItem.IdCarrera = carrera.IdCarrera;
                            carreraItem.NombreCarrera = carrera.NombreCarrera;

                            result.Objects.Add(carreraItem);
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
