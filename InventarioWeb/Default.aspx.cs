using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventarioWeb.Data;

namespace InventarioWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDashboard();
            }
        }

        private void CargarDashboard()
        {
            lblProductos.Text = DashboardDAL.TotalProductos().ToString();

            lblCategorias.Text = DashboardDAL.TotalCategorias().ToString();

            lblVentasHoy.Text = DashboardDAL.TotalVentasHoy().ToString("N0");

            lblStockBajo.Text = DashboardDAL.ProductosStockBajo().ToString();
        }
    }
}