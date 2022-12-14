using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel
{
    class Cliente
    {
        /**
         * “TRUEQUE” es una compañía que se dedica al intercambio de objetos, dispone de una bodega 
         * donde las personas --> Clase: Cliente <-- van a dejar sus artículos...
         * ...si en algún momento hubo alguien con el producto así se puede 
         * contactar al dueño (Atributos de Clase: Cliente (nombre, rut y fono))...
         */
        #region Propiedades
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public int Fono { get; set; }
        #endregion

        #region Constructor
        public Cliente(string nombre, string rut, int fono)
        {
            Nombre = nombre;
            Rut = rut;
            Fono = fono;
        }
        #endregion

        #region Métodos
        public void MostrarCliente()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Cliente: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Nombre);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" | Rut: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Rut);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" | Fono: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Fono+"\n");
        }
        #endregion



    }
}
