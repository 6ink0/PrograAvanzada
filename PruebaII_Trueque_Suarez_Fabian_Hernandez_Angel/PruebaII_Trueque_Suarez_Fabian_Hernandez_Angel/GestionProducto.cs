﻿using System;
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

        #region ConteoGeneral
        //-------------METODO PARA CONTEO DE CLIENTES Y PRODUCTOS----------------------
        public static void ConteoGeneral()
        {
            int i = listaCliente.Count;
            int j = listaProducto.Count;
            Console.WriteLine("Cantidad de Clientes: " + i + "\n" +
                "Cantidad de Productos: " + j + "\n");
        }
        #endregion

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
                    Console.WriteLine("Error -> " + ex.ToString());
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
            if (listaCliente.Count != 0)
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
            else { Console.WriteLine("Listado sin clientes."); }
        }
        #endregion

        #region ProductosEstadoDisponible
        public static void ProductosDisponibles()
        {
            Console.Clear();
            Console.WriteLine("---- PRODUCTOS DISPONIBLES ----\n");
            //Cada producto con estado "true" se agrega a estadoDisponible.
            IEnumerable<Producto> estadoDisponible = from Producto in listaProducto where Producto.Disponible.Equals(true) select Producto;
            foreach (Producto disponibles in estadoDisponible)
            {
                disponibles.MostrarProducto();
            }
            Console.ReadKey();
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

        #region NuevoCliente
        //-------------METODO PARA INGRESAR NUEVO CLIENTE.----------------------
        public static void AgregarCliente()
        {
            Console.Clear();
            //VALIDA DATOS INGRESADOS.
            string strRut, cliente, nombre, strFono, fecha = "";
            bool valido = false;
            string[] rut;
            int fono;

            Console.WriteLine("Nuevo Usuario\n");
            Console.WriteLine("Ingrese Nombre: ");
            nombre = Console.ReadLine().Trim();//Se quitán espacios en blanco al principio y fin.
            do
            {
                Console.WriteLine("Ingrese Rut: 11111111-1");
                strRut = Console.ReadLine().Trim().ToString();
                rut = strRut.Split('-');//divide el string en dos dentro del array,  run + digito verificador separado por "-".
            } while (strRut.Length > 10 || strRut.Length < 9 || rut.Length != 2);

            do
            {
                Console.WriteLine("Fono: 9 9999 9999");
                strFono = Console.ReadLine();
            } while (int.TryParse(strFono, out fono) != true || strFono.Length != 9);
            //se verifica que sea un número (Si la cadena se puede convertir devuelve TRUE) y que sea de largo 9

            cliente = nombre + "|" + strRut + "|" + fono;//Se asignan valores a string para ser
                                                         //ingresados por medio de StreamWriter a un txt.
            Console.Clear();

            //VÁLIDA RUT DISPONIBLE PARA USO.
            listaCliente = new List<Cliente>();
            IEnumerable<Cliente> busquedaDisponible = from Cliente in listaCliente where Cliente.Rut == strRut select Cliente;
            //Con cliente rut, se verifica que no hay otro rut igual en el listado.

            if (busquedaDisponible.Count() > 0)//Por medio de count se verifica que haya 0 coincidencias, sino se agrega.
                                               //Count indica la cantidad de objetos en la lista.
            {
                Console.WriteLine("Rut ya existe en sistema.");
                Console.Read();
            }
            else
            {
                valido = true;
                listaCliente.Add(new Cliente(nombre, strRut, fono));
            }

            //LEER LA LISTA DE CLIENTES 
            if (valido)
            {

                string linea = string.Empty;
                fecha = DateTime.Now.ToString();
                using (StreamReader sr = new StreamReader(rutaCliente))
                {
                    try
                    {
                        while (!sr.EndOfStream)//se busca el texto y se agrega a linea, luego salto de linea,
                                               //y se agrega la siguiente linea hasta que se hayan LEIDO todas las lineas del txt.
                        {
                            linea = linea + sr.ReadLine() + Environment.NewLine;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error ->" + ex.ToString());
                    }
                    sr.Dispose();
                    sr.Close();
                }

                //LEER LA LISTA DE CLIENTES AGREGAR A LA LISTA DE CLIENTES
                using (StreamWriter sw = new StreamWriter(rutaCliente))
                {
                    try
                    {
                        if (linea == string.Empty)
                        {
                            cliente = fecha + "|" + cliente;
                        }
                        else
                        {
                            cliente = linea + fecha + "|" + cliente;// Con el texto anterior guardado( en linea), se concatena la fecha en la que fue ingresado el nuevo cliente
                                                                    // y se concatena el cliente para ser agregado al txt.                            
                        }
                        sw.WriteLine(cliente);
                        Console.WriteLine(cliente + "\n" + "Cliente Agregado Exitosamente");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error ->" + ex.ToString());
                    }
                    sw.Dispose();
                    sw.Close();
                }
            }
            else
            {
                Console.WriteLine("Cliente no agregado.");
            }
            Console.Read();
        }
            #endregion

        #region NuevoProducto
            #endregion

        #region ModificarDisponible
            internal static void ModificarDisponible()
            {
                Console.Clear();
                int contador = 0;
                //Mostrar Productos
                IEnumerable<Producto> listadoProductos = from Producto in listaProducto select Producto;
                foreach (Producto seleccion in listaProducto)
                {
                    contador++;
                    Console.WriteLine("(" + contador + ")");
                    seleccion.MostrarProducto();
                }
                //Seleccionar Producto
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Indique el código del producto a modificar: ");
                string cod = Console.ReadLine();
                Console.Clear();

                if (int.TryParse(cod, out int opc) || opc < listadoProductos.Count() && opc != 0)
                {
                    int i = opc - 1;
                    if (listaProducto[i].Disponible)
                    {
                        listaProducto[i].Disponible = false;
                    }
                    else
                    {
                        listaProducto[i].Disponible = true;
                    }
                    insertarTxt();
                }

            }
            #endregion

        #region InsertarTxt
            public static void insertarTxt()
            {
                try
                {
                    string texto = "";
                    using (StreamWriter sw = new StreamWriter(rutaProducto))
                    {
                        foreach (Producto producto in listaProducto)
                        {
                            string p = +producto.CodigoProducto + "|" + producto.ClienteId + "|" + producto.FechaIngreso + "|" +
                                producto.Descripcion + "|" + producto.Preferencias + "|" + producto.Disponible + "|";
                            texto = texto + p;
                        }
                        sw.WriteLine(texto);
                        sw.Dispose();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion




            #endregion
        }
    }


