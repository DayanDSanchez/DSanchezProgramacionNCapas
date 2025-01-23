using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaColonias = context.ColoniaGetByIdMunicipio(IdMunicipio).ToList();
                    if (listaColonias.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var colonia in listaColonias)
                        {
                            ML.Colonia coloniaItem = new ML.Colonia();
                            coloniaItem.IdColonia = colonia.IdColonia;
                            coloniaItem.Nombre = colonia.Nombre;
                            coloniaItem.CodigoPostal = colonia.CodigoPostal;
                            result.Objects.Add(coloniaItem);
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
