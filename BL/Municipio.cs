using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaMunicipios = context.MunicipioGetByIdEstado(IdEstado).ToList();
                    if (listaMunicipios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var municipio in listaMunicipios)
                        {
                            ML.Municipio municipioItem = new ML.Municipio();
                            municipioItem.IdMunicipio = municipio.IdMunicipio;
                            municipioItem.Nombre = municipio.Nombre;
                            result.Objects.Add(municipioItem);
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
