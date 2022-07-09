<%@ Page Title="Mecanicos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Mecanicos.aspx.cs" Inherits="Web.Pages.Admin.Mecanicos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Mecanicos</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
      <div class="article-body-row">
        <p>Nombre:</p>
        <input runat="server" type="text" class="article-body-row" id="txtNombre" />
      </div>
      <div class="article-body-row">
        <p>Cedula:</p>
        <input runat="server" type="text" class="article-body-row" id="txtCedula" />
      </div>
      <div class="article-body-row">
        <p>Telefono:</p>
        <input runat="server" type="number" min="0" class="article-body-row" id="txtTelefono" />
      </div>
      <div class="article-body-row">
        <p>Actividad:</p>
        <asp:DropDownList runat="server"  CssClass="article-body-row" ID="cmbActividad" />
      </div>
      <div class="article-buttons">
        <asp:Button runat="server" Text="Añadir" ID="add" OnClick="add_Click" />
        <asp:Button runat="server" Text="Eliminar" ID="delete" OnClick="delete_Click" />
        <asp:Button runat="server" Text="Modificar" ID="modify" OnClick="modify_Click" />
        <asp:Button runat="server" Text="Limpiar" ID="clear" OnClick="clear_Click" />
      </div>
    </div>
      <div>
        <asp:ListBox runat="server" CssClass="display" id="mecanicosDisplayer" AutoPostBack="True" OnSelectedIndexChanged="mecanicosDisplayer_SelectedIndexChanged" />
      </div>
    </div>
  </article>
</asp:Content>
