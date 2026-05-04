using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventarioWeb.Data
{
    public class DashboardDAL
    {

        //Muestra el total de productos activos
        public static int TotalProductos()
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM Productos Where Activo = 1";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        //Muestra el total de las categorias
        public static int TotalCategorias()
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = "SELECT COUNT(*) FROM Categorias Where Activo = 1";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        //Muestra el total de ventas del día
        public static decimal TotalVentasHoy()
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT ISNULL(SUM(Total),0)
                                FROM Ventas
                                WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        public static int ProductosStockBajo()
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(*)
                                FROM Productos
                                WHERE Stock <= StockMinimo
                                AND Activo = 1";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}