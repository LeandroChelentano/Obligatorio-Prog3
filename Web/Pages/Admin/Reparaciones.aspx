<%@ Page Title="Gestion de reparaciones" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Reparaciones.aspx.cs" Inherits="Web.Pages.Admin.Reparaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Nuevos</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listNews" CssClass="w-full" AutoPostBack="true" />
        <div class="article-buttons">
          <asp:Button runat="server" Text="Confirmar" ID="btnConfirm" OnClick="btnConfirm_Click" />
          <asp:Button runat="server" Text="Cancelar" ID="btnCancel" OnClick="btnCancel_Click" />
        </div>
      </div>
    </div>
  </article>
  <article>
    <h2 class="article-title">Cancelados</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listCanceled" CssClass="w-full" AutoPostBack="true" />
      </div>
    </div>
  </article>
  <article>
    <h2 class="article-title">Confirmados</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listConfirmed" CssClass="w-full" AutoPostBack="true" />
        <div class="article-buttons">
          <asp:Button runat="server" Text="Ver detalles" ID="btnComplete" OnClick="btnComplete_Click" />
        </div>
      </div>
    </div>
  </article>
  <article>
    <h2 class="article-title">Completados</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listCompleted" CssClass="w-full" AutoPostBack="true" />
        <div class="article-buttons">
          <asp:Button runat="server" Text="Ver detalles" ID="btnDetails" OnClick="btnDetails_Click" />
        </div>
      </div>
    </div>
  </article>
</asp:Content>
