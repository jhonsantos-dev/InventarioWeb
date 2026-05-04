using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioWeb.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal DineroRecibido { get; set; }
        public decimal Cambio { get; set; }
    }
}