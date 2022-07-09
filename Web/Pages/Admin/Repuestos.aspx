<%@ Page Title="Repuestos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Repuestos.aspx.cs" Inherits="Web.Repuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Repuestos</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
      <div class="article-body-row">
        <p>Codigo:</p>
        <input runat="server" type="text" class="article-body-row" id="txtCodigo" />
      </div>
      <div class="article-body-row">
        <p>Descripcion:</p>
        <textarea runat="server" class="article-body-row" id="txtDescripcion"></textarea>
      </div>
      <div class="article-body-row">
        <p>Costo:</p>
        <input runat="server" type="number" min="0" class="article-body-row" id="txtCosto" />
      </div>
      <div class="article-body-row">
        <p>Tipo:</p>
        <asp:DropDownList runat="server"  CssClass="article-body-row" ID="cmbTipoR" />
      </div>
      <div class="article-body-row">
        <p>Proveedor:</p>
        <asp:DropDownList runat="server" class="article-body-row" id="cmbProveedor" />
      </div>
      <div class="article-buttons">
        <asp:Button runat="server" Text="Añadir" ID="add" OnClick="add_Click" />
        <asp:Button runat="server" Text="Eliminar" ID="delete" OnClick="delete_Click" />
        <asp:Button runat="server" Text="Modificar" ID="modify" OnClick="modify_Click" />
        <asp:Button runat="server" Text="Limpiar" ID="clear" OnClick="clear_Click" />
      </div>
    </div>
      <div>
        <asp:ListBox runat="server" CssClass="display" id="repuestosDisplayer" AutoPostBack="True" OnSelectedIndexChanged="repuestosDisplayer_SelectedIndexChanged" />
      </div>
    </div>
  </article>
</asp:Content>
