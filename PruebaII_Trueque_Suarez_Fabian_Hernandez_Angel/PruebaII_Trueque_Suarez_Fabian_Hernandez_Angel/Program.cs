using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prueba de ejecución método 'MostrarCliente'.
            //Cliente cli = new Cliente("Angel", "18364965-5", 931057396);
            //cli.MostrarCliente();
            //GestionProducto.RellenarListaCliente();
            //GestionProducto.ListarClientes();

            //GestionProducto.RellenarListaProducto();
            //GestionProducto.ModificarDisponible();

            //Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
            string resp = "0";
            do
            {
                GestionProducto.RellenarListaProducto();
                GestionProducto.RellenarListaCliente();
                Console.Clear();
                GestionProducto.ConteoGeneral();
                Console.WriteLine(
                    "-------- MENÚ --------\n" +
                    "(1) Agregar Cliente\n" +//Listo
                    "(2) Agregar Producto\n" +//Listo
                    "----------------------\n" +
                    "(3) Listar Productos Disponibles\n" +//Listo
                    "(4) Listar Productos NO Disponibles\n" +//Listo
                    "(5) Listar Articulos Antiguos por fecha\n" +//
                    "(6) Listar Clientes\n" +//Listo
                    "----------------------\n" +
                    "(7) Modificar Producto\n" +//
                    "(8) Salir");
                Console.Write("\nSelecciona una opción: ");
                resp = Console.ReadLine();

                switch (resp)
                {
                    case "1":
                        //GestionProducto.AgregarCliente();
                        break;

                    case "2":

                        //GestionProducto.AgregarProducto();
                        break;

                    case "3":
                        GestionProducto.ProductosDisponibles();
                        break;

                    case "4":
                        GestionProducto.ProductosNoDisponibles();
                        break;

                    case "5":
                        //GestionProducto.BuscarProductoPorFecha();
                        break;

                    case "6":
                        GestionProducto.ListarClientes();
                        break;
                    case "7":
                        GestionProducto.ModificarDisponible();
                        break;

                }
            } while (resp != "8");
            Console.WriteLine("Que tenga buen día!");
            Console.ReadKey();
        }
    }
}
