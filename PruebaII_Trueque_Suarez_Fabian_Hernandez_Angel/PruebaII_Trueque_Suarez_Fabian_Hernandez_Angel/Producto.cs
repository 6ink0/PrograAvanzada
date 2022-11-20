using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaII_Trueque_Suarez_Fabian_Hernandez_Angel
{
    class Producto
    {
        /**
         * “TRUEQUE” es una compañía que se dedica al intercambio de objetos (Atributo de Clase: Producto (Código, Nombre, 
         * Descripción)), dispone de una bodega donde las personas (Atributos de Clase: Producto (ClienteId)) van a dejar 
         * sus artículos y se les pregunta por cual objeto desean intercambiarlo (Atributo de Clase: Producto (ValorAprox, 
         * Preferencias[])), buscan en una planilla Excel si existe para ofrecer el trueque, también buscan en planillas 
         * históricas si en algún momento (Atributo de Clase: Producto (FechaIngreso, Disponible)) hubo alguien con el 
         * producto así se puede contactar al dueño e informarle que existe posibilidad de intercambio si tiene otro 
         * producto igual o similar. 
         */

        #region Propiedades
        public int CodigoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public int ValorAprox { get; set; }
        public string[] Preferencias { get; set; }
        public bool Disponible { get; set; }
        //Se Debe considerar al menos una asociación de clases 1 -> n.
        public string ClienteId { get; set; }
        #endregion

        #region Constructor
        public Producto(int codigoProducto, string clienteId, DateTime fechaIngreso, string descripcion, int valorAprox, string[] preferencias, bool disponible)
        {
            CodigoProducto = codigoProducto;
            FechaIngreso = fechaIngreso;
            Descripcion = descripcion;
            ValorAprox = valorAprox;
            Preferencias = preferencias;
            Disponible = disponible;
            ClienteId = clienteId;
        }
        #endregion

        #region Métodos
        #endregion
    }
}
