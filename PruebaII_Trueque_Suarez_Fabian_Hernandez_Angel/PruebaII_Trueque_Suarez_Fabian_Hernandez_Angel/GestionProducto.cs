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
        //-------------METODO PARA INGRESAR NUEVO PRODUCTO.----------------------
        public static void AgregarProducto()
        {
            Console.Clear();
            string descripcion, strFecha, strValor, strEleccion1, strEleccion2, idCliente = "";
            int valor, eleccion2;
            //USUARIO NUEVO O USUARIO ANTIGUO
            do
            {
                //Se consulta si el usuario ya está ingresado a Sistema,
                //para obtener el rut, que es utilizado como codigoCliente (Clave foranea) en el producto.
                Console.WriteLine("TIPO DE CLIENTE:\n " +
                    "(1) Nuevo." +
                    "(2) Antiguo.\n");
                strEleccion1 = Console.ReadLine();

                if (strEleccion1 == "1")//en caso de que sea nuevo, se gestionará el metodo AgregarUsuario
                {
                    AgregarCliente();
                    idCliente = listaCliente.LastOrDefault().Rut;//Por medio de LastOrDefault()
                                                                  //en el listado general de clientes,se obtendrá el rut.
                }
                else if (strEleccion1 == "2")
                {
                    if (listaCliente.Count() != 0)
                    {
                        do
                        {
                            Console.Clear();
                            ListarClientes();

                            Console.WriteLine("Indique un número de cliente: ");
                            strEleccion2 = Console.ReadLine();

                        } while (!int.TryParse(strEleccion2, out eleccion2) || eleccion2 < 0 || eleccion2 > listaCliente.Count());
                        idCliente = listaCliente[eleccion2 - 1].Rut;
                    }
                }
            } while (!int.TryParse(strEleccion1, out int eleccion1) || eleccion1 < 1 || eleccion1 > 2);

            Console.Clear();

            //Se valida el resto de la información del producto
            DateTime fecha = DateTime.Now;
            strFecha = fecha.ToString();
            Console.WriteLine("Producto Nuevo: ");
            Console.WriteLine("\nFecha Ingreso Producto: " + fecha + "\n");
            do
            {
                Console.WriteLine("Ingrese Descripcion: ");
                descripcion = Console.ReadLine();
            } while (descripcion == string.Empty);

            do
            {
                Console.WriteLine("\nValor Aproximado: ");
                strValor = Console.ReadLine().Trim();
            } while (!int.TryParse(strValor, out valor));
            string[] preferencias;
            string strPref;
            do
            {
                Console.WriteLine("\nIngrese sus preferencias separadas por un guión, Ej: (opc1-opc2-opc3): ");
                strPref = Console.ReadLine();
                preferencias = strPref.Split('-');
            } while (preferencias.Length < 3);

            //Para no repetir codigos, se sumará 1 al producto anterior
            //dando así un codigo diferente para cada producto con ta lde no hacerlo manual.
            listaProducto = new List<Producto>();
            IEnumerable<Producto> busquedaDisponible = from Producto in listaProducto select Producto;

            int codigo;
            try
            {
                if (listaProducto.Count() != 0)
                {
                    codigo = listaProducto.LastOrDefault().CodigoProducto;
                    codigo = codigo + 1;
                }
                else
                {
                    codigo = 1;
                }
            }
            catch
            {
                codigo = 1;
            }

            listaProducto.Add(new Producto(codigo, idCliente, fecha, descripcion, valor, preferencias, true));
            string linea = "";
            using (StreamReader sr = new StreamReader(rutaProducto))
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

            string producto = codigo + "|" + idCliente + "|" + strFecha + "|" + descripcion + "|" + valor + "|" + strPref + "|true";

            using (StreamWriter sw = new StreamWriter(rutaProducto))
            {
                try
                {
                    if (linea == string.Empty)
                    {
                    }
                    else
                    {
                        producto = linea + producto;// Con el texto anterior guardado( en linea), 
                                                    // y se concatena el producto para ser agregado al txt.
                    }
                    sw.WriteLine(producto);
                    Console.Clear();
                    Console.WriteLine(producto + "\n" + "Producto Agregado Exitosamente!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error ->" + ex.ToString());
                }
                sw.Dispose();
                sw.Close();
            }

        }
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

        #region BuscarPorFecha
        public static void BuscarProductoPorFecha()
        {
            IEnumerable<Producto> busquedaFecha = from Producto in listaProducto orderby Producto.FechaIngreso descending select Producto;

            //VALIDAMOS DÍA, MES Y AÑO NO SUPERIOR A LA FECHA ACTUAL
            string d, m, a;
            string fecha;
            int r = 0;
            Console.Clear();
            Console.WriteLine("--- Búsqueda por fecha ---\n" +
                "Ingrese el limite de la búsqueda por fecha de los productos: \n");
            do
            {
                Console.WriteLine("Dia: 00");
                d = Console.ReadLine();
            } while (!(int.TryParse(d, out r)) || r > 31);

            do
            {
                Console.WriteLine("Mes: 00");
                m = Console.ReadLine();
            } while (!(int.TryParse(m, out r) || r > 12));

            do
            {
                Console.WriteLine("Año: 0000");
                a = Console.ReadLine();
            } while (a.Length != 4 || !(int.TryParse(a, out r)) || r < 1990 & r < int.Parse(DateTime.Today.Year.ToString()));


            //Asignamos la fecha de hoy a Fecha Final
            DateTime fechaFinal = DateTime.Today;

            try
            {
                fecha = d + "/" + m + "/" + a;


                fechaFinal = DateTime.Parse(fecha);
                int resultado = DateTime.Compare(fechaFinal, DateTime.Today.AddDays(1));
                //Compara dos fechas, si la ingresada es mayor que la actual, se debe volver a ingresar una fecha.
                if (resultado <= 0)
                {
                    Console.Clear();
                    //Indica las dos fechas al usuario que se están verificando.
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Fecha Hoy: {0}\n" +
                        "Fecha Límite: {1}\n", DateTime.Now, fechaFinal);


                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("-----PRODUCTOS EN FECHA-----");

                    foreach (Producto busquedaProducto in busquedaFecha)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        resultado = DateTime.Compare(fechaFinal, busquedaProducto.FechaIngreso);
                        if (resultado <= 0)
                        {
                            busquedaProducto.MostrarProducto();
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("-----PRODUCTOS FUERA DE FECHA  (ANTIGUOS)-----");
                    foreach (Producto busquedaProducto in busquedaFecha)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        resultado = DateTime.Compare(fechaFinal, busquedaProducto.FechaIngreso);
                        if (resultado > 0)
                        {
                            busquedaProducto.MostrarProducto();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nDebe ingresar una fecha anterior al día de hoy!");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ->" + ex.ToString());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
        #endregion


        #endregion
    }
}


