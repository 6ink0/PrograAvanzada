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
            Cliente cli = new Cliente("Angel", "18364965-5", 931057396);
            cli.MostrarCliente();



            Console.ReadKey();
        }
    }
}
