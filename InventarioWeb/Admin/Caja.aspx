<%@ Page Title="Caja" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Caja.aspx.cs" Inherits="InventarioWeb.Admin.Caja" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="container mt-4">

    <h3 class="mb-4">Caja / Ventas</h3>

    <div class="row">

        <!-- PANEL IZQUIERDO -->
        <div class="col-md-4">

            <!-- BUSCAR PRODUCTO -->
            <div class="card shadow mb-3">

                <div class="card-header bg-primary text-white">
                    Buscar Producto
                </div>

                <div class="card-body">

                    <asp:TextBox
                        ID="txtBuscarProductoCaja"
                        runat="server"
                        CssClass="form-control mb-2"
                        placeholder="Buscar producto..."
                        onkeyup="filtrarProductos()" />

                    <asp:ListBox
                        ID="lstProductos"
                        runat="server"
                        CssClass="form-select"
                        Rows="6">
                    </asp:ListBox>

                    <div class="mt-2">

                        <label>Cantidad</label>

                        <asp:TextBox
                            ID="txtCantidad"
                            runat="server"
                            CssClass="form-control"
                            Text="1"
                            TextMode="Number"
                            min="1"
                            step="1" />
                    </div>

                    <asp:Button
                        ID="btnAgregarProducto"
                        runat="server"
                        Text="Agregar"
                        CssClass="btn btn-success w-100 mt-3"
                        OnClick="btnAgregarProducto_Click" />

                </div>

            </div>


            <!-- PRODUCTO MANUAL -->
            <div class="card shadow">

                <div class="card-header bg-warning">
                    Producto Manual
                </div>

                <div class="card-body">

                    <div class="mb-2">

                        <label>Nombre</label>

                        <asp:TextBox
                            ID="txtNombreManual"
                            runat="server"
                            CssClass="form-control" />

                    </div>

                    <div class="mb-2">

                        <label>Precio</label>

                        <div class="input-group">

                        <span class="input-group-text">$</span>

                        <asp:TextBox
                            ID="txtPrecioManual"
                            runat="server"
                            CssClass="form-control"
                            onkeyup="formatoCOP(this)"
                            placeholder="0" />

                    </div>

                    </div>

                    <div class="mb-2">

                        <label>Cantidad</label>

                        <asp:TextBox
                            ID="txtCantidadManual"
                            runat="server"
                            CssClass="form-control"
                            Text="1"
                            TextMode="Number"
                            min="1"
                            step="1" />

                    </div>

                    <asp:Button
                        ID="btnAgregarManual"
                        runat="server"
                        Text="Agregar Producto Manual"
                        CssClass="btn btn-dark w-100" 
                        OnClick="btnAgregarManual_Click"
                         />

                </div>

            </div>

        </div>


        <!-- PANEL DERECHO -->
        <div class="col-md-8">

            <!-- CARRITO -->
            <div class="card shadow">

                <div class="card-header bg-dark text-white">
                    Carrito de Compra
                </div>

                <div class="card-body">


                    <!-- GRIDVIEW DEL CARRITO -->
                    <asp:GridView
                        ID="gvCarrito"
                        runat="server"
                        CssClass="table table-striped"
                        AutoGenerateColumns="False" 
                        OnRowCommand="gvCarrito_RowCommand"> 
                        

                        <Columns>

                            <asp:BoundField
                                DataField="Producto"
                                HeaderText="Producto" />

                            <asp:BoundField
                                DataField="Cantidad"
                                HeaderText="Cantidad" />

                            <asp:BoundField
                                DataField="Precio"
                                HeaderText="Precio"
                                DataFormatString="$ {0:N0}" />

                            <asp:BoundField
                                DataField="SubTotal"
                                HeaderText="SubTotal"
                                DataFormatString="$ {0:N0}" />

                            <asp:TemplateField HeaderText="Acción">

                                <ItemTemplate>

                                    <asp:Button
                                        runat="server"
                                        Text="Eliminar"
                                        
                                        CssClass="btn btn-danger btn-sm"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Container.DataItemIndex %>' />
                                    

                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>

                </div>

            </div>


            <!-- TOTALES -->
            <div class="card mt-3 shadow">

                <div class="card-body">

                    <div class="row mb-3">

                        <div class="col-md-6">
                            <label>Total</label>
                        </div>

                        <div class="col-md-6 text-end">

                            <asp:Label
                                ID="lblTotal"
                                runat="server"
                                Text="$ 0"
                                CssClass="fw-bold fs-4 text-success"
                                ClientIDMode="Static" />

                        </div>

                    </div>


                    <div class="row mb-3">

                        <div class="col-md-6">
                            <label>Dinero recibido</label>
                        </div>

                        <div class="col-md-6">

                            <div class="input-group">

                                <span class="input-group-text">$</span>

                                 <asp:TextBox
                                    ID="txtDineroRecibido"
                                    runat="server"
                                    CssClass="form-control"
                                    onkeyup="formatoCOP(this); calcularCambio();"
                                    placeholder="0" />
                            </div>

                        </div>

                    </div>


                    <div class="row mb-3 align-items-center">

                        <div class="col-md-4">
                            <label class="fw-bold fs-5">Cambio</label>
                        </div>

                        <div class="col-md-8 text-end">

                            <asp:Label
                                ID="lblCambio"
                                runat="server"
                                Text="$ 0"
                                CssClass="fw-bold text-primary fs-1"
                                ClientIDMode="Static" />

                        </div>

                    </div>


                    <asp:Button
                        ID="btnFinalizarVenta"
                        runat="server"
                        Text="Finalizar Venta"
                        CssClass="btn btn-success w-100 btn-lg"
                        OnClick="btnFinalizarVenta_Click" />

                </div>

            </div>

        </div>

    </div>

</div>

    
    <!-- FACTURA EN PANTALLA DESPUES DE LA VENTA -->
    <div class="modal fade"
     id="modalFactura"
     tabindex="-1"
     data-bs-backdrop="static"
     data-bs-keyboard="false">

    <div class="modal-dialog modal-lg">

        <div class="modal-content">

            <div class="modal-header bg-dark text-white">
                <h5 class="modal-title">Factura de Venta</h5>
            </div>

            <div class="modal-body">

                <div id="contenidoFactura">

                    <h4 class="text-center mb-3">
                        Sistema Inventario
                    </h4>

                    <p>
                        Fecha: <asp:Label ID="lblFacturaFecha" runat="server" />
                    </p>

                    <hr />

                    <asp:GridView
                        ID="gvFactura"
                        runat="server"
                        CssClass="table table-bordered"
                        AutoGenerateColumns="False">

                        <Columns>

                            <asp:BoundField
                                DataField="Producto"
                                HeaderText="Producto" />

                            <asp:BoundField
                                DataField="Cantidad"
                                HeaderText="Cantidad" />

                            <asp:BoundField
                                DataField="Precio"
                                HeaderText="Precio"
                                DataFormatString="$ {0:N0}" />

                            <asp:BoundField
                                DataField="SubTotal"
                                HeaderText="Subtotal"
                                DataFormatString="$ {0:N0}" />

                        </Columns>

                    </asp:GridView>

                    <hr />

                    <div class="row">

                        <div class="col-md-6">
                            <strong>Total:</strong>
                        </div>

                        <div class="col-md-6 text-end">
                            <asp:Label ID="lblFacturaTotal"
                                runat="server" />
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6">
                            <strong>Pagó con:</strong>
                        </div>

                        <div class="col-md-6 text-end">
                            <asp:Label ID="lblFacturaPago"
                                runat="server" />
                        </div>

                    </div>

                    <div class="row">

                        <div class="col-md-6">
                            <strong>Cambio:</strong>
                        </div>

                        <div class="col-md-6 text-end">
                            <asp:Label ID="lblFacturaCambio"
                                runat="server" />
                        </div>

                    </div>

                </div>

            </div>

            <div class="modal-footer">

                <button class="btn btn-secondary"
                        data-bs-dismiss="modal">
                    Aceptar
                </button>

                <button class="btn btn-success"
                        onclick="imprimirFactura()">
                    Imprimir
                </button>

            </div>

        </div>

    </div>

</div>

    <script>

function filtrarProductos() {

    var input = document.getElementById("<%= txtBuscarProductoCaja.ClientID %>");
    var filtro = input.value.toLowerCase();

    var lista = document.getElementById("<%= lstProductos.ClientID %>");
    var opciones = lista.options;

    for (var i = 0; i < opciones.length; i++) {

        var texto = opciones[i].text.toLowerCase();

        if (texto.includes(filtro)) {
            opciones[i].style.display = "";
        } else {
            opciones[i].style.display = "none";
        }

    }
}

</script>

    <script>

        function soloNumeros(input) {

            input.value = input.value.replace(/[^0-9.]/g, '');

        }

</script>

    <script>

            function formatoCOP(input) {

                let valor = input.value.replace(/\./g, '');

                if (valor === "") return;

                let numero = parseInt(valor);

                if (!isNaN(numero)) {
                    input.value = numero.toLocaleString('es-CO');
                }

            }

    </script>

    <script>

        function calcularCambio() {

            let totalTexto = document.getElementById("lblTotal").innerText;
            let dineroTexto = document.getElementById("<%= txtDineroRecibido.ClientID %>").value;

            let total = parseFloat(totalTexto.replace("$", "").replace(/\./g, "")) || 0;
            let dinero = parseFloat(dineroTexto.replace(/\./g, "")) || 0;

            let cambio = dinero - total;

            if (cambio < 0) {
                cambio = 0;
            }

            document.getElementById("lblCambio").innerText =
                "$ " + cambio.toLocaleString('es-CO');

        }

</script>

    <script>

        function mostrarFactura() {

            var modal = new bootstrap.Modal(
                document.getElementById('modalFactura')
            );

            modal.show();
        }

</script>

    <script>

        function mostrarFactura() {

            var modal = new bootstrap.Modal(
                document.getElementById('modalFactura')
            );

            modal.show();
        }

</script>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>

        function alertaCarritoVacio() {
            Swal.fire({
                icon: 'warning',
                title: 'Carrito vacío',
                text: 'No hay productos agregados en el carrito',
                confirmButtonText: 'Aceptar'
            });
        }

        function alertaDineroVacio() {
            Swal.fire({
                icon: 'warning',
                title: 'Dinero no ingresado',
                text: 'Ingrese el dinero recibido',
                confirmButtonText: 'Aceptar'
            });
        }

        function alertaDineroInsuficiente() {
            Swal.fire({
                icon: 'warning',
                title: 'Dinero insuficiente',
                text: 'El dinero recibido es menor que el total de la compra',
                confirmButtonText: 'Aceptar'
            });
        }

</script>

</asp:Content>
