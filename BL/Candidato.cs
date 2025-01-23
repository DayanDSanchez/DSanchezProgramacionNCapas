using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Candidato
    {
        public static ML.Result CandidatoAddEF(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CandidatoAdd(candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto = Convert.FromBase64String(candidato.FotoString), candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);
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
        public static ML.Result CandidatoDeleteEF(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CandidatoDelete(IdCandidato);
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
        public static ML.Result CandidatoUpdateEF(ML.Candidato candidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CandidatoUpdate(candidato.IdCandidato, candidato.Nombre, candidato.ApellidoPaterno, candidato.ApellidoMaterno, candidato.Edad, candidato.Correo, candidato.Telefono, candidato.Direccion, candidato.Foto/* = Convert.FromBase64String(candidato.FotoString)*/, candidato.Curriculum, candidato.Universidad.IdUniversidad, candidato.Carrera.IdCarrera, candidato.BolsaTrabajo.IdBolsaTrabajo, candidato.Vacante.IdVacante);
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
        public static ML.Result CandidatoGetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaCandidatos = context.CandidatoGetAll().ToList();
                    if (listaCandidatos.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var candidato in listaCandidatos)
                        {
                            ML.Candidato candidatoItem = new ML.Candidato();
                            candidatoItem.IdCandidato = candidato.IdCandidato;
                            candidatoItem.Nombre = candidato.Nombre;
                            candidatoItem.ApellidoPaterno = candidato.ApellidoPaterno;
                            candidatoItem.ApellidoMaterno = candidato.ApellidoMaterno;
                            candidatoItem.Edad = candidato.Edad;
                            candidatoItem.Correo = candidato.Correo;
                            candidatoItem.Telefono = candidato.Telefono;
                            candidatoItem.Direccion = candidato.Direccion;
                            candidatoItem.Foto = candidato.Foto;
                            candidatoItem.Curriculum = candidato.Curriculum;
                            candidatoItem.Universidad = new ML.Universidad();
                            candidatoItem.Universidad.NombreUniversidad = candidato.NombreUniversidad;
                            candidatoItem.Carrera = new ML.Carrera();
                            candidatoItem.Carrera.NombreCarrera = candidato.NombreCarrera;
                            candidatoItem.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidatoItem.BolsaTrabajo.NombreBolsaTrabajo = candidato.NombreBolsaTrabajo;
                            candidatoItem.Vacante = new ML.Vacante();
                            candidatoItem.Vacante.NombreVacante = candidato.NombreVacante;
                            result.Objects.Add(candidatoItem);
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
        public static ML.Result CandidatoGetByIdEF(int IdCandidato)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var candidato = context.CandidatoGetById(IdCandidato).SingleOrDefault();
                    if (candidato != null)
                    {
                        ML.Candidato candidatoItem = new ML.Candidato();
                        candidatoItem.IdCandidato = candidato.IdCandidato;
                        candidatoItem.Nombre = candidato.Nombre;
                        candidatoItem.ApellidoPaterno = candidato.ApellidoPaterno;
                        candidatoItem.ApellidoMaterno = candidato.ApellidoMaterno;
                        candidatoItem.Edad = candidato.Edad;
                        candidatoItem.Correo = candidato.Correo;
                        candidatoItem.Telefono = candidato.Telefono;
                        candidatoItem.Direccion = candidato.Direccion;
                        candidatoItem.Foto = candidato.Foto;
                        candidatoItem.Curriculum = candidato.Curriculum;
                        candidatoItem.Universidad = new ML.Universidad();
                        candidatoItem.Universidad.IdUniversidad = candidato.IdUniversidad;
                        candidatoItem.Universidad.NombreUniversidad = candidato.NombreUniversidad;
                        candidatoItem.Carrera = new ML.Carrera();
                        candidatoItem.Carrera.IdCarrera = candidato.IdCarrera;
                        candidatoItem.Carrera.NombreCarrera = candidato.NombreCarrera;
                        candidatoItem.BolsaTrabajo = new ML.BolsaTrabajo();
                        candidatoItem.BolsaTrabajo.IdBolsaTrabajo = candidato.IdBolsaTrabajo;
                        candidatoItem.BolsaTrabajo.NombreBolsaTrabajo = candidato.NombreBolsaTrabajo;
                        candidatoItem.Vacante = new ML.Vacante();
                        candidatoItem.Vacante.IdVacante = candidato.IdVacante;
                        candidatoItem.Vacante.NombreVacante = candidato.NombreVacante;

                        result.Object = candidatoItem;
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
        public static ML.Result CandidatoGetByVacanteEF(int? IdVacante)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaCandidatos = context.CandidatoGetByVacante(IdVacante).ToList();
                    if (listaCandidatos.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var candidato in listaCandidatos)
                        {
                            ML.Candidato candidatoItem = new ML.Candidato();
                            candidatoItem.IdCandidato = candidato.IdCandidato;
                            candidatoItem.Nombre = candidato.Nombre;
                            candidatoItem.ApellidoPaterno = candidato.ApellidoPaterno;
                            candidatoItem.ApellidoMaterno = candidato.ApellidoMaterno;
                            candidatoItem.Edad = candidato.Edad;
                            candidatoItem.Correo = candidato.Correo;
                            candidatoItem.Telefono = candidato.Telefono;
                            candidatoItem.Direccion = candidato.Direccion;
                            candidatoItem.Foto = candidato.Foto;
                            candidatoItem.Curriculum = candidato.Curriculum;
                            candidatoItem.Universidad = new ML.Universidad();
                            candidatoItem.Universidad.NombreUniversidad = candidato.NombreUniversidad;
                            candidatoItem.Carrera = new ML.Carrera();
                            candidatoItem.Carrera.NombreCarrera = candidato.NombreCarrera;
                            candidatoItem.BolsaTrabajo = new ML.BolsaTrabajo();
                            candidatoItem.BolsaTrabajo.NombreBolsaTrabajo = candidato.NombreBolsaTrabajo;
                            candidatoItem.Vacante = new ML.Vacante();
                            candidatoItem.Vacante.NombreVacante = candidato.NombreVacante;
                            result.Objects.Add(candidatoItem);
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
