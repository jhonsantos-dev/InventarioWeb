using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioWeb.Data;
using InventarioWeb.Models;

namespace InventarioWeb.Admin
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }         
        }

        private void CargarCategorias()
        {
            gvCategorias.DataSource = CategoriaDAL.Listar();
            gvCategorias.DataBind();
        }

        protected void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            hfIdCategoria.Value = "";
            txtNombre.Text = "";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "modal",
                "abrirModalCategoria();",
                true);               
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Categoria c = new Categoria();

            c.Nombre = txtNombre.Text;
            c.Activo = true;

            if (string.IsNullOrEmpty(hfIdCategoria.Value))
            {
                CategoriaDAL.Insertar(c);

                lblToastMensaje.Text = "Se agregó una nueva categoría";
            }
            else
            {
                c.IdCategoria = Convert.ToInt32(hfIdCategoria.Value);
                CategoriaDAL.Actualizar(c);

                lblToastMensaje.Text = "Categoría actualizada";
            }

            

            CargarCategorias();

            ScriptManager.RegisterStartupScript(
        this,
        this.GetType(),
        "toast",
        "mostrarToast();",
        true);
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Categoria c = CategoriaDAL.ObtenerPorId(id);

                hfIdCategoria.Value = c.IdCategoria.ToString();

                txtNombreEditar.Text = c.Nombre;

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "modal",
                    "abrirModalEditar();",
                    true);            
            }

            if (e.CommandName == "Eliminar")
            {
                hfEliminarId.Value = id.ToString();

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "modal",
                    "abrirModalEliminar();",
                    true);
            }
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfEliminarId.Value);

            CategoriaDAL.Eliminar(id);

            CargarCategorias();

            lblToastMensaje.Text = "Categoría eliminada correctamente";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                "mostrarToast();",
                true);
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Categoria c = new Categoria();

            c.IdCategoria = Convert.ToInt32(hfIdCategoria.Value);
            c.Nombre = txtNombreEditar.Text;

            CategoriaDAL.Actualizar(c);

            CargarCategorias();

            lblToastMensaje.Text = "Categoría actualizada correctamente";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                "mostrarToast();",
                true);
        }
    }
}