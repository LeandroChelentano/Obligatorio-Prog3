<%@ Page Title="Personas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Personas.aspx.cs" Inherits="Web.Pages.Personas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Promover usuarios</h2>
    <hr />
    <asp:ListBox runat="server" ID="listUsers" CssClass="w-full" AutoPostBack="true" />
    <div class="article-buttons">
      <asp:Button runat="server" Text="Promover" ID="btnPromote" OnClick="btnPromote_Click" />
      <asp:Button runat="server" Text="Eliminar" ID="btnDelete" OnClick="btnDelete_Click" />
    </div>
  </article>
  <% if (Web.Controladora.ControladoraWeb.isOwner()) {%>
  <article>
    <h2 class="article-title">Degradar administradores</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listAdmins" CssClass="w-full" AutoPostBack="true" />
        <div class="article-buttons">
          <asp:Button runat="server" Text="Degradar" ID="btnDegrade" OnClick="btnDegrade_Click" />
        </div>
      </div>
    </div>
  </article>
  <%}%>
</asp:Content>
