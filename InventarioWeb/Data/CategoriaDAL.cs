using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using InventarioWeb.Models;

namespace InventarioWeb.Data
{
    public class CategoriaDAL
    {
        public static List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT IdCategoria, Nombre, Activo
                                FROM Categorias
                                WHERE Activo = 1";
                SqlCommand cmd = new SqlCommand(sql, cn);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categoria c = new Categoria();

                    c.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                    c.Nombre = dr["Nombre"].ToString();
                    c.Activo = Convert.ToBoolean(dr["Activo"]);

                    lista.Add(c);
                }
            }

            return lista;
        }

        public static void Insertar(Categoria c)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"INSERT INTO Categorias (Nombre, Activo)
                                VALUES (@Nombre, @Activo)";

                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Activo", c.Activo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Categoria ObtenerPorId(int id)
        {
            Categoria c = new Categoria();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"SELECT IdCategoria, Nombre, Activo
                                FROM Categorias
                                WHERE IdCategoria = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c.IdCategoria = Convert.ToInt32(dr["IdCategoria"]);
                    c.Nombre = dr["Nombre"].ToString();
                    c.Activo = Convert.ToBoolean(dr["Activo"]);
                }
            }

            return c;
        }

        public static void Actualizar(Categoria c)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"UPDATE Categorias
                                SET Nombre = @Nombre
                                WHERE IdCategoria = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Id", c.IdCategoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Eliminar(int id)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string sql = @"UPDATE Categorias
                                SET Activo = 0
                                WHERE IdCategoria = @Id";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Id", id);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}