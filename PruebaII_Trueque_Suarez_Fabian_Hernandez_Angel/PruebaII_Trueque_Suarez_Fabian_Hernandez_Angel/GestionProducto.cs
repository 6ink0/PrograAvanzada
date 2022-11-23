using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel
{
    class GestionProducto
    {
        #region Propiedades
        static List<Cliente> listaCliente;
        static List<Producto> listaProducto;
        static string rutaCliente = @"C:\Users\ginko\Documents\GitHub\PruebaII_Trueque\PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel\PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel\bin\Debug\clientes.txt";
        static string rutaProducto = @"C:\Users\ginko\Documents\GitHub\PruebaII_Trueque\PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel\PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel\bin\Debug\productos.txt";
        #endregion

        #region Métodos

        #region RellenarListaClientes
        public static void RellenarListaCliente()
        {
            string[] linea;
            listaCliente = new List<Cliente>(); //Nueva lista
            using (StreamReader sr = new StreamReader(rutaCliente))
            {
                try 
                {
                    while (!sr.EndOfStream) //Si no es el final del Stream, loop. 
                    {
                        linea = sr.ReadLine().Split('|'); //Divide la linea leída por "|" ingresado al momento de guardar.
                        string nombre = linea[1];
                        string rut = linea[2];
                        int fono = int.Parse(linea[3]);
                        //Se ingresan valores leídos del string a cada parámetro del nuevo cliente, en la lista.
                        listaCliente.Add(new Cliente(nombre, rut, fono));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -> "  + ex.ToString());
                }
                //Al estar usando 'using', se debe cerrar el stream (Close) y eliminar la info (Dispose).
                sr.Dispose();
                sr.Close();
            }
        }
        #endregion

        #region RellenarListaProductos
        public static void RellenarListaProducto()
        {
            string[] linea;
            listaProducto = new List<Producto>(); //nueva lista
            using (StreamReader sr = new StreamReader(rutaProducto))
            {
                try
                {
                    while (!sr.EndOfStream)
                    {
                        linea = sr.ReadLine().Split('|');
                        string[] prefe = linea[5].Split('|');
                        listaProducto.Add(new Producto(int.Parse(linea[0]), linea[1], DateTime.Parse(linea[2]), linea[3], int.Parse(linea[4]), prefe, Convert.ToBoolean(linea[6])));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error -> " + ex.ToString());
                }
                //Al estar usando 'using', se debe cerrar el stream (Close) y eliminar la info (Dispose).
                sr.Dispose();
                sr.Close();
            }
        }
        #endregion

        #region ListarClientes
        public static void ListarClientes()
        {
            if(listaCliente.Count != 0)
            {
                Console.Clear();
                Console.WriteLine("---- LISTADO DE CLIENTES ----" + "\n");
                foreach (Cliente cliente in listaCliente)
                {
                    int posicion = listaCliente.IndexOf(cliente) + 1;
                    Console.Write("(" + posicion + ") ");
                    cliente.MostrarCliente();
                }
                Console.Write("\n" + "Listado Exitoso!");
                Console.ReadLine();
            }
            else { Console.WriteLine("Listado sin clientes.");  }
        }
        #endregion

        #region ProductosEstadoDisponible
        public static void ProductosDisponibles()
        {
            Console.Clear();
            Console.WriteLine("---- PRODUCTOS DISPONIBLES ----\n");
            //Cada producto con estado "true" se agrega a estadoDisponible.
            IEnumerable<Producto> estadoDisponible = from Producto in listaProducto where Producto.Disponible.Equals(true) select Producto;
            foreach(Producto disponibles in estadoDisponible)
            {
                disponibles.MostrarProducto();
            }
        }
        #endregion

        #region ProductosEstadoNoDisponible
        public static void ProductosNoDisponibles()
        {
            Console.Clear();
            Console.WriteLine("---- PRODUCTOS NO DISPONIBLES ---- \n");
            //Cada producto con estado "false" se agrega a estadoNoDisponible.
            IEnumerable<Producto> estadoNoDisponible = from Producto in listaProducto where Producto.Disponible == false select Producto;
            foreach (Producto noDisponibles in estadoNoDisponible)
            {
                noDisponibles.MostrarProducto();
            }
            Console.ReadLine();
        }
        #endregion

        #endregion
    }
}
