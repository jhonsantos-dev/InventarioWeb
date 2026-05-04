using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using InventarioWeb.Data;

namespace InventarioWeb.Admin
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void ExportarExcel(DataTable dt, string nombreArchivo)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();

            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime inicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fin = Convert.ToDateTime(txtFechaFin.Text);

            DataTable dt = ReportesDAL.VentasPorFecha(inicio, fin);

            gvVentas.DataSource = dt;
            gvVentas.DataBind();

            lblTotalVentas.Text = dt.Rows.Count.ToString();

            decimal totalDinero = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalDinero += Convert.ToDecimal(row["Total"]);
            }

            lblTotalDinero.Text = "$ " + totalDinero.ToString("N0");
        }

        protected void btnExcelTodo_Click(object sender, EventArgs e)
        {
            DataTable dt = ReportesDAL.VentasCompleto();

            ExportarExcel(dt, "ReporteVentasCompleto");
        }

        protected void btnExcelFechas_Click(object sender, EventArgs e)
        {
            DateTime inicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fin = Convert.ToDateTime(txtFechaFin.Text);

            DataTable dt = ReportesDAL.VentasPorFecha(inicio, fin);

            ExportarExcel(dt, "ReporteVentasPorFecha");
        }

        protected void gvVentas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalle")
            {
                int idVenta = Convert.ToInt32(e.CommandArgument);

                DataTable dt = ReportesDAL.DetalleVenta(idVenta);

                gvDetalleVenta.DataSource = dt;
                gvDetalleVenta.DataBind();

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "modal",
                    "mostrarDetalleVenta();",
                    true
                );
            }
        }
    }
}