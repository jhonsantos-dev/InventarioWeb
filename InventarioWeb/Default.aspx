<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InventarioWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

    <h2 class="mb-4">Panel de Control</h2>

    <div class="row">

        <div class="col-md-3">
            <div class="card text-white bg-primary mb-3 shadow">
                <div class="card-body">
                    <h5 class="card-title">Ventas Hoy</h5>
                    <h3><asp:Label ID="lblVentasHoy" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-success mb-3 shadow">
                <div class="card-body">
                    <h5 class="card-title">Productos</h5>
                    <h3><asp:Label ID="lblProductos" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-warning mb-3 shadow">
                <div class="card-body">
                    <h5 class="card-title">Categorías</h5>
                    <h3><asp:Label ID="lblCategorias" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="card text-white bg-danger mb-3 shadow">
                <div class="card-body">
                    <h5 class="card-title">Stock Bajo</h5>
                    <h3><asp:Label ID="lblStockBajo" runat="server" Text="0"></asp:Label></h3>
                </div>
            </div>
        </div>

    </div>

    <hr class="my-4"/>

    <h4>Accesos Rápidos</h4>

    <div class="row mt-3">

        <div class="col-md-3">
            <a href="Admin/Categorias.aspx" class="btn btn-outline-primary w-100 mb-3">
                Gestionar Categorías
            </a>
        </div>

        <div class="col-md-3">
            <a href="Admin/Productos.aspx" class="btn btn-outline-success w-100 mb-3">
                Gestionar Productos
            </a>
        </div>

        <div class="col-md-3">
            <a href="Admin/Caja.aspx" class="btn btn-outline-warning w-100 mb-3">
                Ir a Caja
            </a>
        </div>

        <div class="col-md-3">
            <a href="Admin/Reportes.aspx" class="btn btn-outline-danger w-100 mb-3">
                Ver Reportes
            </a>
        </div>

    </div>

</div>

    
</asp:Content>
