using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using InventarioWeb.Models;

namespace InventarioWeb.Data
{
    public class VentaDAL
    {
        public static int InsertarVenta(decimal total, decimal dinero, decimal cambio)
        {
            int idVenta = 0;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"INSERT INTO Ventas
                            (Fecha,Total,DineroRecibido,Cambio)
                            VALUES
                            (@Fecha,@Total,@DineroRecibido,@Cambio);

                            SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@DineroRecibido", dinero);
                cmd.Parameters.AddWithValue("@Cambio", cambio);

                cn.Open();

                idVenta = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return idVenta;
        }

        
        public static void InsertarDetalleVenta(int idVenta,int idProducto,string nombreManual,int cantidad,decimal precio,decimal subtotal)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"INSERT INTO DetalleVenta
                       (IdVenta,IdProducto,NombreProductoManual,Cantidad,PrecioUnitario,SubTotal)
                       VALUES
                       (@IdVenta,@IdProducto,@NombreManual,@Cantidad,@Precio,@SubTotal)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                if (idProducto == 0)
                {
                    cmd.Parameters.AddWithValue("@IdProducto", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NombreManual", nombreManual);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@NombreManual", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@SubTotal", subtotal);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static int VentasHoy()
        {
            int total = 0;

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT COUNT(*)
                            FROM Ventas
                            WHERE CAST(Fecha AS DATE) = CAST(GETDATE() AS DATE)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();

                total = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return total;
        }
    }
}