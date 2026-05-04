<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="InventarioWeb.Admin.Productos" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="container mt-4">

    <div class="d-flex justify-content-between align-items-center mb-4">

        <h3>Gestión de Productos</h3>

        <asp:Button ID="btnNuevoProducto"
            runat="server"
            Text="Nuevo Producto"
            CssClass="btn btn-primary" 
            OnClick="btnNuevoProducto_Click"
             />

    </div>


    <div class="card shadow">

        <div class="card-body">

            <div class="row mb-3">

                <div class="col-md-4">

                    <div class="input-group">

                        <span class="input-group-text">🔎</span>

                        <asp:TextBox
                            ID="txtBuscarProducto"
                            runat="server"
                            CssClass="form-control"
                            placeholder="Buscar producto..."
                            onkeyup="filtrarTabla()" />

                    </div>

                </div>

            </div>

            <asp:GridView 
                ID="gvProductos"
                runat="server"
                CssClass="table table-hover"
                AutoGenerateColumns="False" 
                OnRowCommand="gvProductos_RowCommand" 
                ClientIDMode="Static"
                AllowSorting="true" 
                OnSorting="gvProductos_Sorting"

                >

                <Columns>

                    <asp:BoundField DataField="IdProducto"
                        HeaderText="ID" />

                    <asp:BoundField DataField="Nombre"
                        HeaderText="Producto" SortExpression="Nombre" />

                    <asp:BoundField DataField="Precio"
                        HeaderText="Precio"
                        DataFormatString="{0:C}" SortExpression="Precio" />

                    <asp:TemplateField HeaderText="Stock">

                        <ItemTemplate>

                            <%# MostrarStock(
                                Convert.ToInt32(Eval("Stock")),
                                Convert.ToInt32(Eval("StockMinimo"))
                            ) %>

                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:BoundField DataField="StockMinimo"
                        HeaderText="Stock Mínimo"  />

                    <asp:BoundField DataField="Categoria"
                        HeaderText="Categoría" SortExpression="Categoria" />

                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>

                            <asp:Button
                                runat="server"
                                Text="Editar"
                                CssClass="btn btn-warning btn-sm"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdProducto") %>' />

                            <asp:Button
                                runat="server"
                                Text="Eliminar"
                                CssClass="btn btn-danger btn-sm"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdProducto") %>' />

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>

    </div>

</div>


    <!-- MODAL NUEVO PRODUCTO -->
<div class="modal fade"
     id="modalProducto"
     data-bs-backdrop="static"
     tabindex="-1">

    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">

                <h5 class="modal-title">
                    Producto
                </h5>

                <button class="btn-close"
                        data-bs-dismiss="modal"></button>

            </div>

            <div class="modal-body">

                <asp:HiddenField ID="hfIdProducto"
                    runat="server" />

                <div class="mb-3">

                    <label>Nombre</label>

                    <asp:TextBox
                        ID="txtNombre"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="mb-3">

                    <label>Precio</label>

                    <asp:TextBox
                        ID="txtPrecio"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="mb-3">

                    <label>Stock</label>

                    <asp:TextBox
                        ID="txtStock"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="mb-3">

                    <label>Stock Mínimo</label>

                    <asp:TextBox
                        ID="txtStockMinimo"
                        runat="server"
                        CssClass="form-control" />

                </div>


                <div class="mb-3">

                    <label>Categoría</label>

                    <asp:DropDownList
                        ID="ddlCategoria"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>

                </div>

            </div>

            <div class="modal-footer">

                <asp:Button
                    ID="btnGuardarProducto"
                    runat="server"
                    Text="Guardar"
                    CssClass="btn btn-success" 
                    OnClick="btnGuardarProducto_Click"
                     />

            </div>

        </div>
    </div>

</div>

    <!-- MODAL CONFIRMAR ELIMINAR -->
<div class="modal fade"
     id="modalEliminarProducto"
     data-bs-backdrop="static"
     data-bs-keyboard="false"
     tabindex="-1">

    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

            <div class="modal-header bg-danger text-white">
                <h5 class="modal-title">Eliminar Producto</h5>
            </div>

            <div class="modal-body">

                <p>¿Seguro que deseas eliminar este producto?</p>

                <asp:HiddenField ID="hfEliminarProducto"
                    runat="server" />

            </div>

            <div class="modal-footer">

                <button type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                    Cancelar
                </button>

                <asp:Button
                    ID="btnConfirmarEliminarProducto"
                    runat="server"
                    Text="Eliminar"
                    CssClass="btn btn-danger" 
                    OnClick="btnConfirmarEliminarProducto_Click"                
                     />

            </div>

        </div>
    </div>
</div>


    <!-- MODAL CONFIRMAR EDITAR -->
<div class="modal fade"
     id="modalConfirmarEditar"
     data-bs-backdrop="static"
     data-bs-keyboard="false"
     tabindex="-1">

    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

            <div class="modal-header bg-warning">
                <h5 class="modal-title">Editar Producto</h5>
            </div>

            <div class="modal-body">

                <p>¿Deseas editar este producto?</p>

                <asp:HiddenField ID="hfEditarProducto"
                    runat="server" />

            </div>

            <div class="modal-footer">

                <button class="btn btn-secondary"
                        data-bs-dismiss="modal">
                    Cancelar
                </button>

                <asp:Button
                    ID="btnConfirmarEditar"
                    runat="server"
                    Text="Editar"
                    CssClass="btn btn-warning" 
                    OnClick="btnConfirmarEditar_Click"
                     />

            </div>

        </div>
    </div>
</div>

    <!-- MODAL EDITAR PRODUCTO -->
<div class="modal fade"
     id="modalEditarProducto"
     data-bs-backdrop="static"
     tabindex="-1">

    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">

                <h5 class="modal-title">
                    Editar Producto
                </h5>

                <button class="btn-close"
                        data-bs-dismiss="modal"></button>

            </div>

            <div class="modal-body">

                <asp:HiddenField ID="HiddenField1"
                    runat="server" />

                <div class="mb-3">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombreEditar"
                        runat="server"
                        CssClass="form-control"/>
                </div>

                <div class="mb-3">
                    <label>Precio</label>
                    <asp:TextBox ID="txtPrecioEditar"
                        runat="server"
                        CssClass="form-control"/>
                </div>

                <div class="mb-3">
                    <label>Stock</label>
                    <asp:TextBox ID="txtStockEditar"
                        runat="server"
                        CssClass="form-control"/>
                </div>

                <div class="mb-3">
                    <label>Stock Mínimo</label>
                    <asp:TextBox ID="txtStockMinimoEditar"
                        runat="server"
                        CssClass="form-control"/>
                </div>

                <div class="mb-3">
                    <label>Categoría</label>
                    <asp:DropDownList
                        ID="ddlCategoriaEditar"
                        runat="server"
                        CssClass="form-select"/>
                </div>

            </div>

            <div class="modal-footer">

                <asp:Button
                    ID="btnActualizarProducto"
                    runat="server"
                    Text="Actualizar"
                    CssClass="btn btn-success" 
                    OnClick="btnActualizarProducto_Click"
                    />

            </div>

        </div>
    </div>
</div>

    <!-- TOAST MENSAJES -->
<div class="toast-container position-fixed top-0 end-0 p-3">

    <div id="toastMensaje"
         class="toast align-items-center text-bg-success border-0"
         role="alert">

        <div class="d-flex">

            <div class="toast-body">

                <asp:Label ID="lblToastMensaje"
                    runat="server"
                    Text="">
                </asp:Label>

            </div>

            <button type="button"
                    class="btn-close btn-close-white me-2 m-auto"
                    data-bs-dismiss="toast">
            </button>

        </div>

    </div>

</div>

    <script>

function abrirModalProducto() {

    var modal = new bootstrap.Modal(
        document.getElementById('modalProducto')
    );

    modal.show();
}

</script>

    <script>

        function abrirModalEliminarProducto() {
            new bootstrap.Modal(
                document.getElementById('modalEliminarProducto')
            ).show();
        }

        function abrirModalConfirmarEditar() {
            new bootstrap.Modal(
                document.getElementById('modalConfirmarEditar')
            ).show();
        }

        function abrirModalEditarProducto() {
            new bootstrap.Modal(
                document.getElementById('modalEditarProducto')
            ).show();
        }

</script>

    <script>

        function mostrarToast() {

            var toastEl = document.getElementById('toastMensaje');

            var toast = new bootstrap.Toast(toastEl, {
                delay: 3000
            });

            toast.show();
        }

</script>

    

    <script>

        function filtrarTabla() {

            var input = document.getElementById("<%= txtBuscarProducto.ClientID %>");
            var filtro = input.value.toLowerCase();

            var tabla = document.getElementById("gvProductos");
            var filas = tabla.getElementsByTagName("tr");

            for (var i = 1; i < filas.length; i++) {

                var texto = filas[i].textContent.toLowerCase();

                if (texto.indexOf(filtro) > -1) {
                    filas[i].style.display = "";
                } else {
                    filas[i].style.display = "none";
                }
            }
        }

</script>

</asp:Content>
