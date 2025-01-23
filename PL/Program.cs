using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opc;
            String seguir;
            do
            {
                Console.Clear();
                Console.WriteLine("Manejo de usuarios \n");
                Console.WriteLine("¿Que quieres hacer? \n");
                Console.WriteLine("Sql");
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Eliminar Usuario");
                Console.WriteLine("3. Actualizar Usuario");
                Console.WriteLine("4. Consultar Usuario");
                Console.WriteLine("Stored Procedures");
                Console.WriteLine("5. Consultar Todos los Usuarios");
                Console.WriteLine("6. Consultar Usuario por ID");
                Console.WriteLine("7. Eliminar Usuario por ID");
                Console.WriteLine("8. Agregar Usuario");
                Console.WriteLine("9. Actualizar Usuario");
                Console.WriteLine("EF");
                Console.WriteLine("10. Agregar usuario EF");
                Console.WriteLine("11. Eliminar usuario EF");
                Console.WriteLine("12. Actualizar usuario EF");
                Console.WriteLine("13. Consultar Todos los Usuarios EF");
                Console.WriteLine("14. Consultar Usuario por ID");
                Console.WriteLine("Linq");
                Console.WriteLine("15. Agregar usuario EFLinq");
                Console.WriteLine("16. Eliminar usuario EFLinq");
                Console.WriteLine("17. Actualizar usuario EFLinq");
                Console.WriteLine("18. Consultar Todos los Usuarios EFLinq");
                Console.WriteLine("19. Consultar Usuario por ID EFLinq");
                Console.WriteLine("20. Salir\n");
                Console.WriteLine("Ingresa el numero ded opcion");
                opc = Convert.ToInt32(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        Usuario.Add();
                        break;
                    case 2:
                        Usuario.Delete();
                        break;
                    case 3:
                        Usuario.Update();
                        break;
                    case 4:
                        Usuario.Read();
                        break;
                    case 5:
                        Usuario.GetAll();
                        break;
                    case 6:
                        Usuario.GetById();
                        break;
                    case 7:
                        Usuario.DeleteSP();
                        break;
                    case 8:
                        Usuario.AddSP();
                        break;
                    case 9:
                        Usuario.UpdateSP();
                        break;
                    case 10:
                        Usuario.AddEF();
                        break;
                    case 11:
                        Usuario.DeleteEF();
                        break;
                    case 12:
                        Usuario.UpdateEF();
                        break;
                    case 13:
                        //Usuario.GetAllEF();
                        break;
                    case 14:
                        Usuario.GetByIdEF();
                        break;
                    case 15:
                        Usuario.AddEFLinq();
                        break;
                    case 16:
                        Usuario.DeleteEFLinq();
                        break;
                    case 17:
                        Usuario.UpdateEFLinq();
                        break;
                    case 18:
                        Usuario.GetAllEFLinq();
                        break;
                    case 19:
                        Usuario.CargaMasivaTxt3();
                        break;
                    case 20:
                        return;
                    default:
                        Console.WriteLine("\n Opcion no valida");
                        break;
                }
                Console.WriteLine("\n Quieres seguir usando el programa? (S/N) \n");
                seguir = Console.ReadLine();
            } while(seguir == "S" || seguir == "s");

            //Console.ReadKey();
        }
    }
}
