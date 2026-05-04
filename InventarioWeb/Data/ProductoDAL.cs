using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InventarioWeb.Models;
using System.Data.SqlClient;

namespace InventarioWeb.Data
{
    public class ProductoDAL
    {
        public static List<dynamic> Listar()
        {
            List<dynamic> lista = new List<dynamic>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            { 
                string sql = @"SELECT 
                            p.IdProducto, 
                            p.Nombre, 
                            p.Precio, 
                            p.Stock, 
                            p.StockMinimo, 
                            c.Nombre AS Categoria 
                            FROM Productos p 
                            INNER JOIN Categorias c 
                            ON p.IdCategoria = c.IdCategoria 
                            WHERE p.Activo = 1";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new
                    {
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        Nombre = dr["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(dr["Precio"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        StockMinimo = Convert.ToInt32(dr["StockMinimo"]),
                        Categoria = dr["Categoria"].ToString()
                    });
                }
            }

            return lista;
        }

        public static void Insertar(Producto p)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"INSERT INTO Productos
                                (Nombre,Precio,Stock,StockMinimo,IdCategoria,Activo)
                                VALUES
                                (@Nombre,@Precio,@Stock,@StockMinimo,@IdCategoria,@Activo)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre",p.Nombre);
                cmd.Parameters.AddWithValue("@Precio",p.Precio);
                cmd.Parameters.AddWithValue("@Stock",p.Stock);
                cmd.Parameters.AddWithValue("@StockMinimo",p.StockMinimo);
                cmd.Parameters.AddWithValue("@IdCategoria",p.IdCategoria);
                cmd.Parameters.AddWithValue("@Activo",p.Activo);

                cn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public static Producto ObtenerPorId(int id)
        {
            Producto p = new Producto();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT *
                                FROM Productos
                                WHERE IdProducto = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    p.IdProducto = Convert.ToInt32(dr["IdProducto"]);
                    p.Nombre = dr["Nombre"].ToString();
                    p.Precio = Convert.ToDecimal(dr["Precio"]);
                    p.Stock = Convert.ToInt32(dr["Stock"]);
                    p.StockMinimo = Convert.ToInt32(dr["StockMinimo"]);
                    p.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                    p.Activo = Convert.ToBoolean(dr["Activo"]);
                }
            }

            return p;
        }

        public static void Actualizar(Producto p)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"UPDATE Productos
                                SET
                                Nombre = @Nombre,
                                Precio = @Precio,
                                Stock = @Stock,
                                StockMinimo = @StockMinimo,
                                IdCategoria = @IdCategoria
                                WHERE IdProducto = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre",p.Nombre);
                cmd.Parameters.AddWithValue("@Precio",p.Precio);
                cmd.Parameters.AddWithValue("@Stock",p.Stock);
                cmd.Parameters.AddWithValue("@StockMinimo",p.StockMinimo);
                cmd.Parameters.AddWithValue("@IdCategoria",p.IdCategoria);
                cmd.Parameters.AddWithValue("@Id",p.IdProducto);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"UPDATE Productos
                                SET Activo = 0
                                WHERE IdProducto = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<dynamic> Buscar(string nombre)
        {
            List<dynamic> lista = new List<dynamic>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT
                                p.IdProducto,
                                p.Nombre,
                                p.Precio,
                                p.Stock,
                                p.StockMinimo,
                                c.Nombre AS Categoria
                                FROM Productos p
                                INNER JOIN Categorias c
                                ON p.IdCategoria = c.IdCategoria
                                WHERE P.Activo = 1
                                AND p.Nombre LIKE @Nombre";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", "%" + nombre + "%");

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new
                    {
                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
                        Nombre = dr["Nombre"].ToString(),
                        Precio = Convert.ToDecimal(dr["Precio"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        StockMinimo = Convert.ToInt32(dr["StockMinimo"]),
                        Categoria = dr["Categoria"].ToString()
                    });
                }
            }

            return lista;
        }

        public static void DescontarStock(int idProducto, int cantidad)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"UPDATE Productos
                                SET Stock = Stock - @Cantidad
                                WHERE IdProducto = @IdProducto";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}