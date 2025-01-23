using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cita
    {
        public static ML.Result CitaAddEF(ML.Cita cita)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CitaAdd(cita.FechaHora, (byte)cita.Piso.IdPiso, cita.Candidato.IdCandidato, (byte)cita.EstatusCita.IdEstatusCita, cita.URL);
                    context.SaveChanges();

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
        public static ML.Result CitaGetByIdCandidatoEF(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var cita = context.CitaGetByIdCandidato(IdCandidato).SingleOrDefault();
                    if (cita != null)
                    {
                        ML.Cita citaItem = new ML.Cita();
                        citaItem.IdCita = cita.IdCita;
                        citaItem.FechaHora = cita.FechaHora;
                        citaItem.Piso = new ML.Piso();
                        citaItem.Piso.IdPiso = (byte)cita.IdPiso;
                        citaItem.Candidato = new ML.Candidato();
                        citaItem.EstatusCita = new ML.EstatusCita();
                        citaItem.EstatusCita.IdEstatusCita = (byte)cita.IdEstatusCita;
                        citaItem.URL = cita.URL;

                        result.Object = citaItem;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }
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
        public static ML.Result CitaGetByIdCandidatoLinq(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var citaResult = (from cita in context.Citas
                                      where cita.IdCandidato == IdCandidato
                                      select cita).SingleOrDefault();
                    if (citaResult != null)
                    {

                        ML.Cita citaItem = new ML.Cita();
                        citaItem.IdCita = citaResult.IdCita;
                        citaItem.FechaHora = citaResult.FechaHora;
                        citaItem.Piso = new ML.Piso();
                        citaItem.Piso.IdPiso = Convert.ToInt32(citaResult.IdPiso);
                        citaItem.Candidato = new ML.Candidato();
                        citaItem.Candidato.IdCandidato = Convert.ToInt32(citaResult.IdCandidato);
                        citaItem.EstatusCita = new ML.EstatusCita();
                        citaItem.EstatusCita.IdEstatusCita = Convert.ToInt32(citaResult.IdEstatusCita);
                        citaItem.URL = citaResult.URL;

                        result.Object = citaItem;

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
        public static ML.Result CitaUpdateEF(ML.Cita cita)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CitaUpdate(cita.IdCita, cita.FechaHora, (byte)cita.Piso.IdPiso, cita.Candidato.IdCandidato, (byte)cita.EstatusCita.IdEstatusCita, cita.URL);
                    context.SaveChanges();

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
        public static ML.Result CitaDeleteLinq(int IdCita)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var citaResult = (from cita in context.Citas
                                         where cita.IdCita == IdCita
                                         select cita).FirstOrDefault();

                    if (citaResult != null)
                    {
                        context.Citas.Remove(citaResult);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no encontrado.";
                    }
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
