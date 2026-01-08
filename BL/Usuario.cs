using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;
using System.Runtime.Remoting.Contexts;
using DL_EF;
using System.Data.Entity.Migrations;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
using System.IO;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO [Usuario]([Nombre],[ApellidoPaterno],[ApellidoMaterno],[Telefono],[Email]) VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,@Telefono,@Email)";
                        cmd.Connection = context;

                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DELETE FROM [dbo].[Usuario] WHERE id = @IdUsuario";
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        cmd.Connection = context;

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //cmd.CommandText = "DELETE FROM [dbo].[Usuario] WHERE id = @IdUsuario";
                        cmd.CommandText = "UPDATE [dbo].[Usuario] SET [Nombre] = @Nombre, [ApellidoPaterno] = @ApellidoPaterno," +
                            "[ApellidoMaterno] = @ApellidoMaterno,[Telefono] = @Telefono," +
                            "[Email] = @Email WHERE id = @IdUsuario";
                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);

                        cmd.Connection = context;

                        context.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Read(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT * FROM [dbo].[Usuario] WHERE id = @IdUsuario";
                        cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                        cmd.Connection = context;

                        context.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = int.Parse(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.ApellidoPaterno = reader["ApellidoPaterno"].ToString();
                                usuario.ApellidoMaterno = reader["ApellidoMaterno"].ToString();
                                usuario.Telefono = reader["Telefono"].ToString();
                                usuario.Email = reader["Email"].ToString();

                                result.Object = usuario;
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Error";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = "UsuarioGetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        DataTable dataTable = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in dataTable.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.IdUsuario = Convert.ToInt16(row[0]);
                                usuario.Nombre = (row[1].ToString());
                                usuario.ApellidoPaterno = (row[2].ToString());
                                usuario.ApellidoMaterno = (row[3].ToString());
                                usuario.Telefono = (row[4].ToString());
                                usuario.Email = (row[
                                    5].ToString());
                                usuario.UserName = (row[6].ToString());
                                usuario.Password = (row[7].ToString());
                                usuario.Sexo = (row[8].ToString());
                                usuario.Celular = (row[9].ToString());
                                usuario.FechaNacimiento = (row[10].ToString());
                                usuario.Curp = (row[11].ToString());
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Convert.ToInt16(row[12]);

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No hay registros en la tabla";
                        }
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuarioById";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = Convert.ToInt16(row[0]);
                        usuario.Nombre = (row[1].ToString());
                        usuario.ApellidoPaterno = (row[2].ToString());
                        usuario.ApellidoMaterno = (row[3].ToString());
                        usuario.Telefono = (row[4].ToString());
                        usuario.Email = (row[5].ToString());
                        usuario.UserName = (row[6].ToString());
                        usuario.Password = (row[7].ToString());
                        usuario.Sexo = (row[8].ToString());
                        usuario.Celular = (row[9].ToString());
                        usuario.FechaNacimiento = (row[10].ToString());
                        usuario.Curp = (row[11].ToString());
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = Convert.ToInt16(row[12]);

                        result.Object = usuario;
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
        public static ML.Result DeleteSP(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuarioDelete";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    context.Open();
                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar usuario";
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
        public static ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuarioAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Curp", usuario.Curp);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@ImageUser", usuario.Imagen);
                    cmd.Parameters.AddWithValue("@Calle", usuario.Direccion.Calle);
                    cmd.Parameters.AddWithValue("@NumeroExterior", usuario.Direccion.NumeroExterior);
                    cmd.Parameters.AddWithValue("@NumeroInterior", usuario.Direccion.NumeroInterior);
                    cmd.Parameters.AddWithValue("@IdColonia", usuario.Direccion.Colonia.IdColonia);

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
        public static ML.Result UpdateSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuarioUpdate";

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = query;

                    //1
                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);

                    context.Open();
                    int rowAffected = cmd.ExecuteNonQuery();

                    if (rowAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al actualizar usuario";
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
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Telefono, usuario.Email, usuario.UserName, usuario.Password, usuario.Sexo, usuario.Celular, usuario.FechaNacimiento, usuario.Curp, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
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
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.UsuarioDelete(IdUsuario);
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
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Telefono, usuario.Email, usuario.UserName, usuario.Password, usuario.Sexo, usuario.Celular, usuario.FechaNacimiento, usuario.Curp, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
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
        public static ML.Result GetAllEF(string Nombre, string ApellidoPaterno, string ApellidoMaterno, int IdRol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaUsuarios = context.UsuarioGetAll(Nombre, ApellidoPaterno, ApellidoMaterno, IdRol).ToList();
                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var usuario in listaUsuarios)  
                        {
                            ML.Usuario usuarioItem = new ML.Usuario();
                            usuarioItem.IdUsuario = usuario.Id;
                            usuarioItem.Nombre = usuario.Nombre;
                            usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
                            usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
                            usuarioItem.Telefono = usuario.Telefono;
                            usuarioItem.Email = usuario.Email;
                            usuarioItem.UserName = usuario.UserName;
                            usuarioItem.Password = usuario.Password;
                            usuarioItem.Sexo = usuario.Sexo;
                            usuarioItem.Celular = usuario.Celular;
                            usuarioItem.FechaNacimiento = usuario.FechaNacimiento;
                            //if (usuario.FechaNacimiento != null)
                            //{
                            //    DateTime auxiliar = DateTime.ParseExact(usuario.FechaNacimiento.ToString(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            //    usuarioItem.FechaNacimiento = auxiliar.ToString("dd-MM-yyyy");
                            //}
                            usuarioItem.Curp = usuario.Curp;
                            usuarioItem.Rol = new ML.Rol();
                            usuarioItem.Rol.IdRol = Convert.ToInt32(usuario.IdRol);
                            usuarioItem.Imagen = null;
                            usuarioItem.Status = usuario.Status;
                            usuarioItem.Direccion = new ML.Direccion();
                            usuarioItem.Direccion.IdDireccion = Convert.ToInt32(usuario.IdDireccion);
                            usuarioItem.Direccion.Calle = usuario.Calle;
                            usuarioItem.Direccion.NumeroExterior = usuario.NumeroExterior;
                            usuarioItem.Direccion.NumeroInterior = usuario.NumeroInterior;
                            usuarioItem.Direccion.Colonia = new ML.Colonia();
                            usuarioItem.Direccion.Colonia.IdColonia = Convert.ToInt32(usuario.IdColonia);
                            usuarioItem.Direccion.Colonia.CodigoPostal = usuario.CodigoPostal;
                            usuarioItem.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuarioItem.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(usuario.IdMunicipio);
                            usuarioItem.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuarioItem.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(usuario.IdEstado);
                            result.Objects.Add(usuarioItem);
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
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var usuario = context.UsuarioByID(IdUsuario).SingleOrDefault();

                    if (usuario != null)
                    {

                        ML.Usuario usuarioItem = new ML.Usuario();
                        usuarioItem.IdUsuario = usuario.Id;
                        usuarioItem.Nombre = usuario.Nombre;
                        usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
                        usuarioItem.Telefono = usuario.Telefono;
                        usuarioItem.Email = usuario.Email;
                        usuarioItem.UserName = usuario.UserName;
                        usuarioItem.Password = usuario.Password;
                        usuarioItem.Sexo = usuario.Sexo;
                        usuarioItem.Celular = usuario.Celular;
                        if (usuario.FechaNacimiento != null)
                        {
                            DateTime auxiliar = DateTime.ParseExact(usuario.FechaNacimiento.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            usuarioItem.FechaNacimiento = auxiliar.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            usuarioItem.FechaNacimiento = "";
                        }
                        usuarioItem.Curp = usuario.Curp;
                        result.Correct = true;
                        result.Object = usuarioItem;
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
        public static ML.Result GetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var listaUsuarios = (from usuario in context.Usuarios
                                         select new
                                         {
                                             usuario.Id,
                                             usuario.Nombre,
                                             usuario.ApellidoPaterno,
                                             usuario.ApellidoMaterno,
                                             usuario.Telefono,
                                             usuario.Email,
                                             usuario.UserName,
                                             usuario.Password,
                                             usuario.Sexo,
                                             usuario.Celular,
                                             usuario.FechaNacimiento,
                                             usuario.Curp,
                                             usuario.IdRol,
                                             usuario.ImageUser
                                         }).ToList();
                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var usuario in listaUsuarios)
                        {
                            ML.Usuario usuarioItem = new ML.Usuario();
                            usuarioItem.IdUsuario = usuario.Id;
                            usuarioItem.Nombre = usuario.Nombre;
                            usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
                            usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
                            usuarioItem.Telefono = usuario.Telefono;
                            usuarioItem.Email = usuario.Email;
                            usuarioItem.UserName = usuario.UserName;
                            usuarioItem.Password = usuario.Password;
                            usuarioItem.Sexo = usuario.Sexo;
                            usuarioItem.Celular = usuario.Celular;
                            usuarioItem.FechaNacimiento = usuario.FechaNacimiento.ToString();
                            usuarioItem.Curp = usuario.Curp;
                            usuarioItem.Rol = new ML.Rol();
                            usuarioItem.Rol.IdRol = Convert.ToInt32(usuario.IdRol);
                            usuarioItem.Imagen = usuario.ImageUser;

                            result.Objects.Add(usuarioItem);
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
        public static ML.Result GetByIdEFLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var usuarioResult = (from usuario in context.Usuarios
                                         where usuario.Id == IdUsuario
                                         select usuario).SingleOrDefault();
                    if (usuarioResult != null)
                    {

                        ML.Usuario usuarioItem = new ML.Usuario();
                        usuarioItem.IdUsuario = usuarioResult.Id;
                        usuarioItem.Nombre = usuarioResult.Nombre;
                        usuarioItem.ApellidoPaterno = usuarioResult.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = usuarioResult.ApellidoMaterno;
                        usuarioItem.Telefono = usuarioResult.Telefono;
                        usuarioItem.Email = usuarioResult.Email;
                        usuarioItem.UserName = usuarioResult.UserName;
                        usuarioItem.Password = usuarioResult.Password;
                        usuarioItem.Sexo = usuarioResult.Sexo;
                        usuarioItem.Celular = usuarioResult.Celular;
                        usuarioItem.FechaNacimiento = usuarioResult.FechaNacimiento.ToString();
                        usuarioItem.Curp = usuarioResult.Curp;
                        usuarioItem.Rol = new ML.Rol();
                        usuarioItem.Rol.IdRol = Convert.ToInt32(usuarioResult.IdRol);

                        result.Object = usuarioItem;

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
        public static ML.Result DeleteEFLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var usuarioResult = (from usuario in context.Usuarios
                                         where usuario.Id == IdUsuario
                                         select usuario).FirstOrDefault();

                    if (usuarioResult != null)
                    {
                        context.Usuarios.Remove(usuarioResult);
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
        public static ML.Result AddEFLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioItem = new DL_EF.Usuario();
                    usuarioItem.Nombre = usuario.Nombre;
                    usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioItem.Telefono = usuario.Telefono;
                    usuarioItem.Email = usuario.Email;
                    usuarioItem.UserName = usuario.UserName;
                    if (usuarioItem != null)
                    {
                        context.Usuarios.Add(usuarioItem);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Usuario no insertado.";
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
        public static ML.Result UpdateEFLinq(ML.Usuario usuarioML)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    var usuarioItem = (from usuario in context.Usuarios
                                       where usuario.Id == usuarioML.IdUsuario
                                       select usuario).SingleOrDefault();

                    if (usuarioItem != null)
                    {
                        usuarioItem.Nombre = usuarioML.Nombre;
                        usuarioItem.ApellidoPaterno = usuarioML.ApellidoPaterno;
                        usuarioItem.ApellidoMaterno = usuarioML.ApellidoMaterno;
                        usuarioItem.Telefono = usuarioML.Telefono;
                        usuarioItem.Email = usuarioML.Email;
                        usuarioItem.UserName = usuarioML.UserName;
                        usuarioItem.Password = usuarioML.Password;
                        usuarioItem.Sexo = usuarioML.Sexo;
                        usuarioItem.Celular = usuarioML.Celular;
                        usuarioItem.FechaNacimiento = Convert.ToDateTime(usuarioML.FechaNacimiento);
                        usuarioItem.Curp = usuarioML.Curp;
                        usuarioItem.IdRol = Convert.ToInt32(usuarioML.Rol.IdRol);

                        int rowsAffected = context.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Usuario no actualizado";
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error, no se ecnontro usuario";
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
        public static ML.Result CambioStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.DSanchezProgramacionNCapasEntities context = new DL_EF.DSanchezProgramacionNCapasEntities())
                {
                    context.CambioStatus(IdUsuario, Status);
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
        public static ML.Result LeerExcel(string cadenaConnection)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(cadenaConnection))
                {
                    string query = "SELECT * FROM[Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = conn;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario); //Llenamos nuestra tabla con el DataReader
                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.Telefono = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                usuario.Password = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                DateTime fechaNacimiento = DateTime.Parse(row[9].ToString());
                                usuario.FechaNacimiento = fechaNacimiento.ToString("dd-MM-yyyy");
                                usuario.Curp = row[10].ToString();
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = Convert.ToInt32(row[11]);
                                usuario.Imagen = null;
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();
                                usuario.Direccion.NumeroInterior = row[15].ToString();
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(row[16]);

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
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
        public static ML.Result ValidarExcel(List<object> usuarios)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int numeroRegistro = 1;

            foreach (ML.Usuario item in usuarios)
            {
                ML.ResultExcel resultExcel = new ML.ResultExcel();
                resultExcel.NumeroRegistro = numeroRegistro;

                if (item.Nombre == "")
                {
                    resultExcel.MensajeError += "El nombre no puede ir vacio - ";
                }
                if (item.Nombre.Length > 20)
                {
                    resultExcel.MensajeError += "El nombre no puede ser mayor 20 caracteres";
                }
                if (item.ApellidoPaterno == "")
                {
                    resultExcel.MensajeError += "El Apellido Paterno no puede ir vacio -";
                }
                if (item.ApellidoPaterno.Length > 20)
                {
                    resultExcel.MensajeError += "El Apellido Paterno no puede ser mayor 20 caracteres";
                }
                if (item.ApellidoMaterno == "")
                {
                    resultExcel.MensajeError += "El Apellido Materno no puede ir vacio -";
                }
                if (item.ApellidoMaterno.Length > 20)
                {
                    resultExcel.MensajeError += "El Apellido Materno no puede ser mayor 20 caracteres";
                }
                if (item.Telefono == "")
                {
                    resultExcel.MensajeError += "Telefono no puede ir vacio -";
                }
                if (item.Telefono.Length > 15)
                {
                    resultExcel.MensajeError += "El Telefono no puede ser mayor 15 caracteres";
                }
                if (item.Email == "")
                {
                    resultExcel.MensajeError += "Email no puede ir vacio -";
                }
                if (item.Email.Length > 254)
                {
                    resultExcel.MensajeError += "El Email no puede ser mayor 254 caracteres";
                }
                if (item.UserName == "")
                {
                    resultExcel.MensajeError += "UserName no puede ir vacio -";
                }
                if (item.UserName.Length > 50)
                {
                    resultExcel.MensajeError += "El UserName no puede ser mayor 50 caracteres";
                }
                if (item.Password == "")
                {
                    resultExcel.MensajeError += "Password no puede ir vacio -";
                }
                if (item.Password.Length > 50)
                {
                    resultExcel.MensajeError += "El Password no puede ser mayor 50 caracteres";
                }
                if (item.Sexo == "")
                {
                    resultExcel.MensajeError += "Sexo no puede ir vacio -";
                }
                if (item.Sexo.Length > 2)
                {
                    resultExcel.MensajeError += "El Sexo no puede ser mayor 2 caracteres";
                }
                if (item.Celular == "")
                {
                    resultExcel.MensajeError += "Celular no puede ir vacio -";
                }
                if (item.Celular.Length > 20)
                {
                    resultExcel.MensajeError += "El Celular no puede ser mayor 20 caracteres";
                }
                if (item.FechaNacimiento == "")
                {
                    resultExcel.MensajeError += "FechaNacimiento no puede ir vacio -";
                }
                if (item.FechaNacimiento.Length > 10)
                {
                    resultExcel.MensajeError += "El FechaNacimiento no puede ser mayor 20 caracteres";
                }
                if (item.Curp == "")
                {
                    resultExcel.MensajeError += "Curp no puede ir vacio -";
                }
                if (item.Curp.Length > 50)
                {
                    resultExcel.MensajeError += "El Curp no puede ser mayor 50 caracteres";
                }
                if (item.Rol.IdRol == 0 )
                {
                    resultExcel.MensajeError += "Seleccion un Rol -";
                }
                if (item.Direccion.Calle == "")
                {
                    resultExcel.MensajeError += "Calle no puede ir vacio -";
                }
                if (item.Direccion.Calle.Length > 50)
                {
                    resultExcel.MensajeError += "Calle no puede ser mayor 50 caracteres";
                }
                if (item.Direccion.NumeroExterior == "")
                {
                    resultExcel.MensajeError += "Numero exterior no puede ir vacio -";
                }
                if (item.Direccion.NumeroExterior.Length > 50)
                {
                    resultExcel.MensajeError += "Numero exterior no puede ser mayor 50 caracteres";
                }
                if (item.Direccion.NumeroExterior == "")
                {
                    resultExcel.MensajeError += "Numero exterior no puede ir vacio -";
                }
                if (item.Direccion.NumeroInterior.Length > 50)
                {
                    resultExcel.MensajeError += "Numero Interior no puede ser mayor 50 caracteres";
                }
                if (item.Direccion.Colonia.IdColonia == 0)
                {
                    resultExcel.MensajeError += "Seleccion una colonia-";
                }
                if (resultExcel.MensajeError != null || resultExcel.MensajeError != "")
                {
                    result.Objects.Add(resultExcel);
                }
                numeroRegistro ++;
            }
            return result;
        }





    }
}
