using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace InventarioWeb.Data
{
    public class ReportesDAL
    {
        public static DataTable VentasCompleto()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT
                            v.IdVenta,
                            v.Fecha,
                            COALESCE(p.Nombre, dv.NombreProductoManual) AS Producto,
                            dv.Cantidad,
                            dv.PrecioUnitario,
                            dv.SubTotal,
                            v.Total
                            FROM Ventas v
                            INNER JOIN DetalleVenta dv ON v.IdVenta = dv.IdVenta
                            LEFT JOIN Productos p ON dv.IdProducto = p.IdProducto
                            ORDER BY v.Fecha DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                da.Fill(dt);
            }

            return dt;
        }


        public static DataTable VentasPorFecha(DateTime inicio, DateTime fin)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT 
               v.IdVenta,
               v.Fecha,
               v.Total
               FROM Ventas v
               WHERE CAST(v.Fecha AS DATE) BETWEEN @Inicio AND @Fin
               ORDER BY v.Fecha DESC";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Inicio", inicio);
                cmd.Parameters.AddWithValue("@Fin", fin);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;
        }

        public static DataTable DetalleVenta(int idVenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT 
                       COALESCE(p.Nombre, dv.NombreProductoManual) AS Producto,
                       dv.Cantidad,
                       dv.PrecioUnitario,
                       dv.SubTotal
                       FROM DetalleVenta dv
                       LEFT JOIN Productos p ON dv.IdProducto = p.IdProducto
                       WHERE dv.IdVenta = @IdVenta";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@IdVenta", idVenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }




    }
}