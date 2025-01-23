using BL;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public static void Add()
        {
            Console.Clear();
            Console.WriteLine("********** Agregar Usuario **********\n");

            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el email del usuario");
            usuario.Email = Console.ReadLine();

            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Usuario insertado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al insertar el usuario. Error:" + result.ErrorMessage);
            }
        }
        public static void Delete()
        {
            Console.Clear();
            Console.WriteLine("********** Eliminar Usuario **********\n");
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el ID del usuario a eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct)
            {
                Console.WriteLine("Usuario eliminado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al eliminar usuario Error:" + result.ErrorMessage);
            }
        }
        public static void Update()
        {
            Console.Clear();
            Console.WriteLine("********** Actualizar Usuario **********\n");
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el ID del usuario a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Email");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                Console.WriteLine("Usuario actualizado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrió un error al actualizar usuario Error:" + result.ErrorMessage);
            }
        }
        public static void Read()
        {
            Console.Clear();
            Console.WriteLine("********** Buscar Usuario **********\n");
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el ID del usuario a consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            usuario.IdUsuario = IdUsuario;

            ML.Result result = BL.Usuario.Read(IdUsuario);

            if (result.Correct)
            {
                ML.Usuario Usuario = (ML.Usuario)result.Object;
                Console.WriteLine("\n Datos del usuario");
                Console.WriteLine("Nombre: " + Usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + Usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + Usuario.ApellidoMaterno);
                Console.WriteLine("Telefono: " + Usuario.Telefono);
                Console.WriteLine("Email: " + Usuario.Email);
            }
            else
            {
                Console.WriteLine("Ocurrió un error al buscar usuario Error:" + result.ErrorMessage);
            }
        }
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("*************");
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Email);
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Curp);
                    Console.WriteLine(usuario.Rol.IdRol);
                    Console.WriteLine("*************");
                }
            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void GetById()
        {
            Console.WriteLine("Ingrese el ID del usuario a consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object; ///unboxing 

                Console.WriteLine("*************");
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
                Console.WriteLine("*************");
            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void DeleteSP()
        {
            Console.WriteLine("Ingrese el ID del usuario a eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.DeleteSP(IdUsuario);
            if (result.Correct)
            {

                Console.WriteLine("Usuario eliminado....");

            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void AddSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.AddSP(usuario);
            if (result.Correct)
            {

                Console.WriteLine("Usuario agregado....");

            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        public static void UpdateSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese ID del usuario a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.UpdateSP(usuario);
            if (result.Correct)
            {

                Console.WriteLine("Usuario Actualizado....");

            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        public static void AddEF()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario agregado....");
            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        public static void DeleteEF()
        {
            Console.WriteLine("Ingrese el ID del usuario a eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario eliminado....");
            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void UpdateEF()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese ID del usuario a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.UpdateEF(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario Actualizado....");
            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        //public static void GetAllEF()
        //{
        //    ML.Result result = BL.Usuario.GetAllEF();
        //    if (result.Correct)
        //    {
        //        foreach (ML.Usuario usuario in result.Objects)
        //        {
        //            Console.WriteLine("*************");
        //            Console.WriteLine(usuario.IdUsuario);
        //            Console.WriteLine(usuario.Nombre);
        //            Console.WriteLine(usuario.ApellidoPaterno);
        //            Console.WriteLine(usuario.ApellidoMaterno);
        //            Console.WriteLine(usuario.Telefono);
        //            Console.WriteLine(usuario.Email);
        //            Console.WriteLine(usuario.UserName);
        //            Console.WriteLine(usuario.Password);
        //            Console.WriteLine(usuario.Sexo);
        //            Console.WriteLine(usuario.Celular);
        //            Console.WriteLine(usuario.FechaNacimiento);
        //            Console.WriteLine(usuario.Curp);
        //            Console.WriteLine(usuario.Rol.IdRol);
        //            Console.WriteLine("*************");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Sin resultados");
        //    }
        //}
        public static void GetByIdEF()
        {
            Console.WriteLine("Ingrese el ID del usuario a consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                Console.WriteLine("*************");
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
                Console.WriteLine("*************");

            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void AddEFLinq()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.AddEFLinq(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario agregado....");
            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        public static void DeleteEFLinq()
        {
            Console.WriteLine("Ingrese el ID del usuario a eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.DeleteEFLinq(IdUsuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario eliminado....");
            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void UpdateEFLinq()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Ingrese ID del usuario a actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese el Nombre del usuario");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Apellido Materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Ingrese el Telefono del usuario");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Ingrese el Email del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el UserName del usuario");
            usuario.UserName = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el password del usuario");
            usuario.Password = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Sexo del usuario");
            usuario.Sexo = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el Celular del usuario");
            usuario.Celular = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese fecha de nacimiento del usuario");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Ingrese el Curp del usuario");
            usuario.Curp = Console.ReadLine().ToString();

            Console.WriteLine("Ingrese el IdRol del usuario");
            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.UpdateEFLinq(usuario);
            if (result.Correct)
            {
                Console.WriteLine("Usuario Actualizado....");
            }
            else
            {
                Console.WriteLine("Error al agregar usuario");
            }
        }
        public static void GetAllEFLinq()
        {
            ML.Result result = BL.Usuario.GetAllEFLinq();
            if (result.Correct)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine("*************");
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Email);
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Curp);
                    Console.WriteLine(usuario.Rol.IdRol);
                    Console.WriteLine("*************");
                }
            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static void GetByIdEFLinq()
        {
            Console.WriteLine("Ingrese el ID del usuario a consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Usuario.GetByIdEFLinq(IdUsuario);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                Console.WriteLine("*************");
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Email);
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
                Console.WriteLine("*************");

            }
            else
            {
                Console.WriteLine("Sin resultados");
            }
        }
        public static ML.Result CargaMasivaTxt3()
        {
            Console.WriteLine("Entrando");
            ML.Result result = new ML.Result();
            string path = @"C:\Users\digis\Documents\Dayan Diego Sanchez Resendiz\DAYAN DIEGO SANCHEZ RESENDIZ\Nuevos.txt";
            try
            {


                StreamReader streamReader = new StreamReader(path);
                string linea = "";
                bool encabezados = true;
                while ((linea = streamReader.ReadLine()) != null) //Trae los datos, que pude leer un linea
                {
                    if (encabezados)
                    {
                        encabezados = false;
                        continue;
                    }

                    string[] valores = linea.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = valores[0];
                    usuario.ApellidoPaterno = valores[1];
                    usuario.ApellidoMaterno = valores[2];
                    usuario.Telefono = valores[3];
                    usuario.Email = valores[4];
                    usuario.UserName = valores[5];
                    usuario.Password = valores[6];
                    usuario.Sexo = valores[7];
                    usuario.Celular = valores[8];
                    usuario.FechaNacimiento = valores[9];
                    usuario.Curp = valores[10];
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = Convert.ToInt32(valores[11]);
                    usuario.Imagen = null;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = valores[13];
                    usuario.Direccion.NumeroExterior = valores[14];
                    usuario.Direccion.NumeroInterior = valores[15];
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(valores[16]);

                    BL.Usuario.AddEF(usuario);
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return new ML.Result();
        }
    }
}
