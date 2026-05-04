<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="InventarioWeb.Admin.Categorias" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">

    <div class="d-flex justify-content-between align-items-center mb-4">
        <h3>Gestión de Categorías</h3>

        <asp:Button ID="btnNuevaCategoria"
            runat="server"
            Text="Nueva Categoría"
            CssClass="btn btn-primary" OnClick="btnNuevaCategoria_Click"
             />
    </div>


    <!-- TABLA -->
    <div class="card shadow">
        <div class="card-body">

            <asp:GridView ID="gvCategorias"
                runat="server"
                CssClass="table table-hover"
                AutoGenerateColumns="False" OnRowCommand="gvCategorias_RowCommand"
                
                >

                <Columns>

                    <asp:BoundField DataField="IdCategoria"
                        HeaderText="ID" />

                    <asp:BoundField DataField="Nombre"
                        HeaderText="Nombre" />

                    <asp:TemplateField HeaderText="Acciones">

                        <ItemTemplate>

                            <asp:Button
                                runat="server"
                                Text="Editar"
                                CssClass="btn btn-warning btn-sm"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdCategoria") %>' />

                            <asp:Button
                                runat="server"
                                Text="Eliminar"
                                CssClass="btn btn-danger btn-sm"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdCategoria") %>' />
                                

                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>

            </asp:GridView>

        </div>
    </div>

</div>

    <!-- MODAL -->
<div class="modal fade"
     id="modalCategoria"
     tabindex="-1">

    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">

                <h5 class="modal-title">
                    Categoría
                </h5>

                <button type="button"
                        class="btn-close"
                        data-bs-dismiss="modal">
                </button>

            </div>

            <div class="modal-body">

                <asp:HiddenField ID="hfIdCategoria"
                    runat="server" />

                <div class="mb-3">

                    <label class="form-label">
                        Nombre
                    </label>

                    <asp:TextBox ID="txtNombre"
                        runat="server"
                        CssClass="form-control" />

                </div>

            </div>

            <div class="modal-footer">

                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar"
                    CssClass="btn btn-success" OnClick="btnGuardar_Click"
                     />

            </div>

        </div>
    </div>
</div>

    <!-- MODAL CONFIRMAR ELIMINAR -->
<div class="modal fade"
     id="modalEliminar"
     data-bs-backdrop="static"
     data-bs-keyboard="false"
     tabindex="-1">

    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

            <div class="modal-header bg-danger text-white">
                <h5 class="modal-title">Confirmar eliminación</h5>
            </div>

            <div class="modal-body">

                <p>¿Seguro que deseas eliminar esta categoría?</p>

                <asp:HiddenField ID="hfEliminarId"
                    runat="server" />

            </div>

            <div class="modal-footer">

                <button type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                    Cancelar
                </button>

                <asp:Button
                    ID="btnConfirmarEliminar"
                    runat="server"
                    Text="Eliminar"
                    CssClass="btn btn-danger" 
                    OnClick="btnConfirmarEliminar_Click"
                     />

            </div>

        </div>
    </div>
</div>

    <!-- MODAL EDITAR -->
<div class="modal fade"
     id="modalEditar"
     data-bs-backdrop="static"
     tabindex="-1">

    <div class="modal-dialog">
        <div class="modal-content">

            <div class="modal-header">

                <h5 class="modal-title">
                    Editar Categoría
                </h5>

                <button class="btn-close"
                        data-bs-dismiss="modal"></button>

            </div>

            <div class="modal-body">

                <asp:HiddenField ID="HiddenField1"
                    runat="server" />

                <div class="mb-3">

                    <label>Nombre</label>

                    <asp:TextBox
                        ID="txtNombreEditar"
                        runat="server"
                        CssClass="form-control" />

                </div>

            </div>

            <div class="modal-footer">

                <asp:Button
                    ID="btnActualizar"
                    runat="server"
                    Text="Actualizar"
                    CssClass="btn btn-success" 
                    OnClick="btnActualizar_Click" />

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
                    Text=""></asp:Label>
            </div>

            <button type="button"
                    class="btn-close btn-close-white me-2 m-auto"
                    data-bs-dismiss="toast">
            </button>

        </div>

    </div>

</div>

    <script>

    function abrirModalCategoria() {

        var modal = new bootstrap.Modal(
            document.getElementById('modalCategoria')
        );

        modal.show();
    }

</script>

    <script>

        function abrirModalEliminar() {

            var modal = new bootstrap.Modal(
                document.getElementById('modalEliminar')
            );

            modal.show();
        }

        function abrirModalEditar() {

            var modal = new bootstrap.Modal(
                document.getElementById('modalEditar')
            );

            modal.show();
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

</asp:Content>