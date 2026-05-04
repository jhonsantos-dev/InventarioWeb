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
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarCategorias();
            }
        }

        private void CargarProductos()
        {
            gvProductos.DataSource = ProductoDAL.Listar();
            gvProductos.DataBind();
        }

        private void CargarCategorias()
        {
            ddlCategoria.DataSource = CategoriaDAL.Listar();

            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "IdCategoria";

            ddlCategoria.DataBind();
        }

        protected void btnNuevoProducto_Click(object sender, EventArgs e)
        {
            hfIdProducto.Value = "";

            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            txtStockMinimo.Text = "";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "modal", 
                "abrirModalProducto();",
                true);
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar" || e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Editar")
                {
                    hfEditarProducto.Value = id.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "modal",
                        "abrirModalConfirmarEditar();",
                        true);
                }

                if (e.CommandName == "Eliminar")
                {
                    hfEliminarProducto.Value = id.ToString();

                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "modal",
                        "abrirModalEliminarProducto();",
                        true);
                }
            }
        }

        protected void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();

            p.Nombre = txtNombre.Text;
            p.Precio = Convert.ToDecimal(txtPrecio.Text);
            p.Stock = Convert.ToInt32(txtStock.Text);
            p.StockMinimo = Convert.ToInt32(txtStockMinimo.Text);
            p.IdCategoria = Convert.ToInt32(ddlCategoria.SelectedValue);
            p.Activo = true;

            ProductoDAL.Insertar(p);

            CargarProductos();

            lblToastMensaje.Text = "Producto agregado correctamente";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                "mostrarToast();",
                true);
        }

        protected void btnConfirmarEditar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfEditarProducto.Value);

            Producto p = ProductoDAL.ObtenerPorId(id);

            // cargar categorias en dropdown
            ddlCategoriaEditar.DataSource = CategoriaDAL.Listar();
            ddlCategoriaEditar.DataTextField = "Nombre";
            ddlCategoriaEditar.DataValueField = "IdCategoria";
            ddlCategoriaEditar.DataBind();

            hfIdProducto.Value = p.IdProducto.ToString();

            txtNombreEditar.Text = p.Nombre;
            txtPrecioEditar.Text = p.Precio.ToString();
            txtStockEditar.Text = p.Stock.ToString();
            txtStockMinimoEditar.Text = p.StockMinimo.ToString();

            ddlCategoriaEditar.SelectedValue = p.IdCategoria.ToString();

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "modal",
                "abrirModalEditarProducto();",
                true);
        }

        protected void btnActualizarProducto_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();

            p.IdProducto = Convert.ToInt32(hfIdProducto.Value);
            p.Nombre = txtNombreEditar.Text;
            p.Precio = Convert.ToDecimal(txtPrecioEditar.Text);
            p.Stock = Convert.ToInt32(txtStockEditar.Text);
            p.StockMinimo = Convert.ToInt32(txtStockMinimoEditar.Text);
            p.IdCategoria = Convert.ToInt32(ddlCategoriaEditar.SelectedValue);

            ProductoDAL.Actualizar(p);

            CargarProductos();

            lblToastMensaje.Text = "Producto actualizado correctamente";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                "mostrarToast();",
                true);
        }

        protected void btnConfirmarEliminarProducto_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hfEliminarProducto.Value);

            ProductoDAL.Eliminar(id);

            CargarProductos();

            lblToastMensaje.Text = "Producto eliminado correctamente";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "toast",
                "mostrarToast();",
                true);
        }

        public string MostrarStock(int stock, int stockMinimo)
        {
            if (stock == 0)
            {
                return "<span class='badge bg-danger'>Sin stock</span>";
            }

            if (stock <= stockMinimo)
            {
                return "<span class='badge bg-warning text-dark'>Stock bajo (" + stock + ")</span>";
            }

            return "<span class='badge bg-success'>Normal (" + stock + ")</span>";
        }

        protected void gvProductos_Sorting(object sender, GridViewSortEventArgs e)
        {
            var lista = ProductoDAL.Listar();

            string sortDirection = "ASC";

            if (ViewState["SortExpression"] != null)
            {
                if (ViewState["SortExpression"].ToString() == e.SortExpression)
                {
                    if (ViewState["SortDirection"].ToString() == "ASC")
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            ViewState["SortExpression"] = e.SortExpression;
            ViewState["SortDirection"] = sortDirection;

            if (sortDirection == "ASC")
            {
                gvProductos.DataSource = lista
                    .OrderBy(x => x.GetType().GetProperty(e.SortExpression).GetValue(x, null))
                    .ToList();
            }
            else
            {
                gvProductos.DataSource = lista
                    .OrderByDescending(x => x.GetType().GetProperty(e.SortExpression).GetValue(x, null))
                    .ToList();
            }

            gvProductos.DataBind();
        }

        //protected void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        //{
        //    string filtro = txtBuscarProducto.Text.Trim();

        //    if (string.IsNullOrEmpty(filtro))
        //    {
        //        CargarProductos();
        //    }
        //    else 
        //    {
        //        gvProductos.DataSource = ProductoDAL.Buscar(filtro);
        //        gvProductos.DataBind();
        //    }
        //}
    }
}