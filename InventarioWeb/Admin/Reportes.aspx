<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="InventarioWeb.Admin.Reportes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

        <h2 class="mb-4">Reporte de Ventas</h2>

        <div class="card mb-4">
            <div class="card-body">

                <div class="row">

                    <div class="col-md-3">
                        <label>Fecha Inicio</label>
                        <asp:TextBox
                            ID="txtFechaInicio"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date" />
                    </div>

                    <div class="col-md-3">
                        <label>Fecha Fin</label>
                        <asp:TextBox
                            ID="txtFechaFin"
                            runat="server"
                            CssClass="form-control"
                            TextMode="Date" />
                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button
                            ID="btnBuscar"
                            runat="server"
                            Text="Buscar"
                            CssClass="btn btn-primary w-100"
                            OnClick="btnBuscar_Click" />

                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button
                            ID="btnExcelFechas"
                            runat="server"
                            Text="📊 Excel Fechas"
                            CssClass="btn btn-success w-100"
                            OnClick="btnExcelFechas_Click" />

                    </div>

                    <div class="col-md-2 d-flex align-items-end">

                        <asp:Button
                            ID="btnExcelTodo"
                            runat="server"
                            Text="📥 Excel Completo"
                            CssClass="btn btn-dark w-100"
                            OnClick="btnExcelTodo_Click" />

                    </div>

                </div>

            </div>
        </div>


        <div class="card">

            <div class="card-body">

                <asp:GridView
                    ID="gvVentas"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false" 
                    OnRowCommand="gvVentas_RowCommand">

                    <Columns>

                        <asp:BoundField
                            DataField="IdVenta"
                            HeaderText="Venta #" />

                        <asp:BoundField
                            DataField="Fecha"
                            HeaderText="Fecha"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                        <asp:BoundField
                            DataField="Total"
                            HeaderText="Total"
                            DataFormatString="$ {0:N0}" />

                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>

                                <asp:Button
                                    runat="server"
                                    Text="Ver Detalle"
                                    CssClass="btn btn-sm btn-info"
                                    CommandName="Detalle"
                                    CommandArgument='<%# Eval("IdVenta") %>' />

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

            </div>

        </div>


        <div class="row mt-3">

            <div class="col-md-6">

                <div class="alert alert-info">

                    <strong>Total Ventas:</strong>
                    <asp:Label ID="lblTotalVentas" runat="server" />

                </div>

            </div>

            <div class="col-md-6">

                <div class="alert alert-success">

                    <strong>Total Dinero:</strong>
                    <asp:Label ID="lblTotalDinero" runat="server" />

                </div>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modalDetalleVenta" tabindex="-1">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">

            <div class="modal-header bg-dark text-white">
                <h5 class="modal-title">Detalle de Venta</h5>
                <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body">

                <asp:GridView
                    ID="gvDetalleVenta"
                    runat="server"
                    CssClass="table table-bordered"
                    AutoGenerateColumns="false">

                    <Columns>

                        <asp:BoundField
                            DataField="Producto"
                            HeaderText="Producto" />

                        <asp:BoundField
                            DataField="Cantidad"
                            HeaderText="Cantidad" />

                        <asp:BoundField
                            DataField="PrecioUnitario"
                            HeaderText="Precio"
                            DataFormatString="$ {0:N0}" />

                        <asp:BoundField
                            DataField="SubTotal"
                            HeaderText="SubTotal"
                            DataFormatString="$ {0:N0}" />

                    </Columns>

                </asp:GridView>

            </div>

        </div>
    </div>
</div>



    <script>
    function mostrarDetalleVenta() {
        var modal = new bootstrap.Modal(document.getElementById('modalDetalleVenta'));
        modal.show();
    }
</script>

</asp:Content>
