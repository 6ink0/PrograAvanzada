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
            listaProducto = new List<Producto>(); //nueva lista
            using (StreamReader sr = new StreamReader(rutaProducto))
            {
                try
                {
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

        #endregion
    }
}
