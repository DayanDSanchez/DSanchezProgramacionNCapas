using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BL
{
    public class Empleado
    {
        //public static ML.Result LeerArchivoTXT(string filePath)
        //{
        //    ML.Result result = new ML.Result();
        //    result.Objects = new List<object>(); // Para almacenar resultados de cada inserción

        //    try
        //    {
        //        // Leer todas las líneas del archivo
        //        string[] lines = File.ReadAllLines(filePath);

        //        foreach (string line in lines)
        //        {
        //            // Suponiendo que los datos están separados por comas: RFC,NumeroEmpleado,Nombre,FechaControl,Salario
        //            string[] data = line.Split('|');

        //            if (data.Length == 5) // Validar que hay suficientes datos
        //            {
        //                ML.Empleado empleado = new ML.Empleado
        //                {
        //                    RFC = data[0],
        //                    NumeroEmpleado = data[1],
        //                    Nombre = data[2],
        //                    FechaControl = data[3], // Se guarda directamente como string
        //                    Salario = Convert.ToDecimal(data[4]) // Se guarda directamente como string
        //                };

        //                // Llamar al método de inserción
        //                ML.Result addResult = AddSP(empleado);
        //                result.Objects.Add(addResult);

        //                if (!addResult.Correct)
        //                {
        //                    result.Correct = false;
        //                    result.ErrorMessage += $"Error al insertar el empleado con RFC {empleado.RFC}: {addResult.ErrorMessage}\n";
        //                }
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage += $"Línea con formato incorrecto: {line}\n";
        //            }
        //        }

        //        // Si no hubo errores
        //        if (string.IsNullOrEmpty(result.ErrorMessage))
        //        {
        //            result.Correct = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }

        //    return result;
        //}


        public static ML.Result AddSP(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "EmpleadoAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@RFC", empleado.RFC);
                    cmd.Parameters.AddWithValue("@NumeroEmpleado", empleado.NumeroEmpleado);
                    cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@FechaControl", empleado.FechaControl);
                    cmd.Parameters.AddWithValue("@Salario", empleado.Salario);

                    context.Open();
                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al agregar usuario";
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

        public static ML.Result EmpleadoGetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaEmpleados = context.EmpleadoGetAll().ToList();
                    if (listaEmpleados.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var empleado in listaEmpleados)
                        {
                            ML.Empleado empleadoItem = new ML.Empleado();
                            empleadoItem.RFC = empleado.RFC;
                            empleadoItem.Nombre = empleado.Nombre;
                            empleadoItem.NumeroEmpleado = empleado.NumeroEmpleado;
                            empleadoItem.FechaControl = empleado.FechaControl.ToString();
                            //if (empleado.FechaControl != null)
                            //{
                            //    DateTime auxiliar = DateTime.ParseExact(empleado.FechaControl.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            //    empleadoItem.FechaControl = auxiliar.ToString("dd-MM-yyyy");
                            //}
                            empleadoItem.Salario = Convert.ToDecimal(empleado.Salario);
                           
                            result.Objects.Add(empleadoItem);
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


        public static ML.Result ValidarTxt(List<object> empleados)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int numeroRegistro = 1;

            foreach (ML.Empleado item in empleados)
            {
                ML.ResultTxt resultTxt = new ML.ResultTxt();
                resultTxt.NumeroRegistro = numeroRegistro;

                if (item.Nombre == "")
                {
                    resultTxt.MensajeError += "El nombre no puede ir vacio - ";
                }
                if (item.Nombre.Length > 20)
                {
                    resultTxt.MensajeError += "El nombre no puede ser mayor 20 caracteres";
                }
                if (item.RFC == "")
                {
                    resultTxt.MensajeError += "El RFC no puede ir vacio -";
                }
                if (item.RFC.Length > 20)
                {
                    resultTxt.MensajeError += "El RFC no puede ser mayor 20 caracteres";
                }
                if (item.NumeroEmpleado == "")
                {
                    resultTxt.MensajeError += "El Numero empleado no puede ir vacio -";
                }
                if (item.NumeroEmpleado.Length > 20)
                {
                    resultTxt.MensajeError += "El Numero empleado no puede ser mayor 20 caracteres";
                }
                if (item.FechaControl == "")
                {
                    resultTxt.MensajeError += "Fecha control no puede ir vacio -";
                }
                if (item.FechaControl.Length > 15)
                {
                    resultTxt.MensajeError += "El Fecha control no puede ser mayor 15 caracteres";
                }
                if (Convert.ToString(item.Salario) == "")
                {
                    resultTxt.MensajeError += "salario no puede ir vacio -";
                }
                if (item.Salario <= 0)
                {
                    resultTxt.MensajeError += "Ingrese salario del empleado";
                }
                
                if (resultTxt.MensajeError != null || resultTxt.MensajeError != "")
                {
                    result.Objects.Add(resultTxt);
                }
                numeroRegistro++;
            }
            return result;
        }

        public static ML.Result AddEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.EmpleadoAdd(empleado.RFC, empleado.NumeroEmpleado,empleado.Nombre,Convert.ToDateTime(empleado.FechaControl),empleado.Salario);

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
