<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="Web.Pages.Admin.Marcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Marcas</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
      <div class="article-body-row">
        <p>Nombre:</p>
        <input runat="server" type="text" class="article-body-row" id="txtMarca" />
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
