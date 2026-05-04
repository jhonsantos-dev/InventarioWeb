using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventarioWeb.Data
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cs = ConfigurationManager.ConnectionStrings["InventarioDB"].ConnectionString;
            return new SqlConnection(cs);
        }


    }
}