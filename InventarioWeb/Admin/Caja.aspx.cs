using InventarioWeb.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioWeb.Models;

namespace InventarioWeb.Admin
{
    public partial class Caja : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Carrito"] = CrearTablaCarrito();

                CargarProductos();
            }
        }

        private DataTable CrearTablaCarrito()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdProducto");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Precio");
            dt.Columns.Add("SubTotal");

            return dt;
        }

        private void CargarProductos()
        {
            lstProductos.DataSource = ProductoDAL.Listar();

            lstProductos.DataTextField = "Nombre";
            lstProductos.DataValueField = "IdProducto";

            lstProductos.DataBind();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            DataTable carrito = (DataTable)Session["Carrito"];

            int idProducto = Convert.ToInt32(lstProductos.SelectedValue);
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            Producto p = ProductoDAL.ObtenerPorId(idProducto);
            int cantidadEnCarrito = 0;

            foreach (DataRow row in carrito.Rows)
            {
                if (Convert.ToInt32(row["IdProducto"]) == idProducto)
                {
                    cantidadEnCarrito = Convert.ToInt32(row["Cantidad"]);
                    break;
                }
            }

            if ((cantidad + cantidadEnCarrito) > p.Stock)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "alert",
                    "Swal.fire('Stock insuficiente','No hay suficiente stock disponible','warning');",
                    true
                );
                return;
            }

            decimal precio = p.Precio;

            bool existe = false;

            foreach (DataRow row in carrito.Rows)
            {
                if (Convert.ToInt32(row["IdProducto"]) == idProducto)
                {
                    int cantidadActual = Convert.ToInt32(row["Cantidad"]);

                    cantidadActual += cantidad;

                    row["Cantidad"] = cantidadActual;
                    row["SubTotal"] = cantidadActual * precio;

                    existe = true;
                    break;
                }
            }

            if (!existe)
            {
                decimal subtotal = precio * cantidad;

                carrito.Rows.Add(
                    idProducto,
                    p.Nombre,
                    cantidad,
                    precio.ToString("N0"),
                    subtotal.ToString("N0")
                );
            }

            Session["Carrito"] = carrito;

            gvCarrito.DataSource = carrito;
            gvCarrito.DataBind();

            CalcularTotal();

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "recalcularCambio",
                "calcularCambio();",
                true
            );
        }

        private void CalcularTotal()
        {
            DataTable carrito = (DataTable)Session["Carrito"];

            decimal total = 0;

            foreach (DataRow row in carrito.Rows)
            {
                total += Convert.ToDecimal(row["SubTotal"]);
            }

            lblTotal.Text = "$ " + total.ToString("N0");
        }

        protected void btnFinalizarVenta_Click(object sender, EventArgs e)
        {
            DataTable carrito = (DataTable)Session["Carrito"];


            // Validar carrito vacio
            if (carrito == null || carrito.Rows.Count == 0)
            {
               ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alert",
                "alertaCarritoVacio();",
                true
            );
                        return;
            }

            // Validar dinero ingresado
            if (string.IsNullOrWhiteSpace(txtDineroRecibido.Text))
            {
                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alert",
                "alertaDineroVacio();",
                true
            );
                return;
            }

            decimal total = Convert.ToDecimal(
                lblTotal.Text.Replace("$", "").Replace(".", "")
            );

            decimal dinero = Convert.ToDecimal(
                txtDineroRecibido.Text.Replace(".", "")
            );

            // Validar dinero menor al total
            if (dinero < total)
            {
                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "alert",
                "alertaDineroInsuficiente();",
                true
            );
                return;
            }

            decimal cambio = dinero - total;

            int idventa = VentaDAL.InsertarVenta(total, dinero, cambio);

            foreach (DataRow row in carrito.Rows)
            {
                int idProducto = Convert.ToInt32(row["IdProducto"]);
                int cantidad = Convert.ToInt32(row["Cantidad"]);
                decimal precio = Convert.ToDecimal(row["Precio"]);
                decimal subtotal = Convert.ToDecimal(row["SubTotal"]);
                string nombreProducto = row["Producto"].ToString();


                VentaDAL.InsertarDetalleVenta(
                    idventa,
                    idProducto,
                    nombreProducto,
                    cantidad,
                    precio,
                    subtotal
                );

                if (idProducto != 0)
                {
                    ProductoDAL.DescontarStock(idProducto, cantidad);
                }
            }

            // MOSTRAR FACTURA (fuera del foreach)

            lblFacturaFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            lblFacturaTotal.Text = "$ " + total.ToString("N0");
            lblFacturaPago.Text = "$ " + dinero.ToString("N0");
            lblFacturaCambio.Text = "$ " + cambio.ToString("N0");

            gvFactura.DataSource = carrito;
            gvFactura.DataBind();

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "factura",
                "mostrarFactura();",
                true
            );

            // LIMPIAR CARRITO

            Session["Carrito"] = CrearTablaCarrito();

            gvCarrito.DataSource = null;
            gvCarrito.DataBind();

            lblTotal.Text = "$ 0";
            lblCambio.Text = "$ 0";

            txtDineroRecibido.Text = "";

            txtNombreManual.Text = "";
            txtPrecioManual.Text = "";
            txtCantidadManual.Text = "1";
        }

        protected void btnAgregarManual_Click(object sender, EventArgs e)
        {
            DataTable carrito = (DataTable)Session["Carrito"];

            string nombre = txtNombreManual.Text;

            decimal precio = Convert.ToDecimal(
                txtPrecioManual.Text.Replace(".","")
                );

            int cantidad = Convert.ToInt32(txtCantidadManual.Text);

            decimal subtotal = precio * cantidad;

            carrito.Rows.Add(
                0,
                nombre,
                cantidad,
                precio.ToString("N0"),
                subtotal.ToString("N0")
                );

            Session["Carrito"] = carrito;

            gvCarrito.DataSource = carrito;
            gvCarrito.DataBind();

            CalcularTotal();

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "recalcularCambio",
                "calcularCambio();",
                true
            );
        }

        protected void txtDineroRecibido_TextChanged(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(
                lblTotal.Text.Replace("$", "").Replace(".", "")
            );

            decimal dinero = Convert.ToDecimal(
                txtDineroRecibido.Text.Replace(".", "")
            );

            decimal cambio = dinero - total;

            lblCambio.Text = "$ " + cambio.ToString("N0");
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataTable carrito = (DataTable)Session["Carrito"];

                carrito.Rows.RemoveAt(index);

                Session["Carrito"] = carrito;

                gvCarrito.DataSource = carrito;
                gvCarrito.DataBind();

                CalcularTotal();
            }

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "recalcularCambio",
                "calcularCambio();",
                true
            );
        }
    }
}