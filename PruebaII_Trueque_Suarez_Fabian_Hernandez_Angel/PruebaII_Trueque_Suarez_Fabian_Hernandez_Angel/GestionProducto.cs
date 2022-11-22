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
            listaCliente = new List<Cliente>(); //nueva lista
            using (StreamReader sr = new StreamReader(rutaCliente))
            {
                try 
                { 
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
        public static void RellenarListaProductos()
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
