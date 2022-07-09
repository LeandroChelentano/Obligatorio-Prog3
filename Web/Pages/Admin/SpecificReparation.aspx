<%@ Page Title="Reparacion específica" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="SpecificReparation.aspx.cs" Inherits="Web.Pages.Admin.SpecificReparation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Informacion general</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <div class="article-body-row full-left">
          <p>
            <b>Nombre:</b>
          </p>
          <span>
            <asp:Literal runat="server" ID="lblNombre"></asp:Literal>
          </span>
        </div>
        <div class="article-body-row full-left">
          <p>
            <b>Cedula:</b>
          </p>
          <span>
            <asp:Literal runat="server" ID="lblCI"></asp:Literal>
          </span>
        </div>
        <div class="article-body-row full-left">
          <p><b>Mecanico:</b></p>
          <asp:DropDownList runat="server" ID="ddlMecanicos" OnSelectedIndexChanged="ddlMecanicos_SelectedIndexChanged" />
        </div>
        <div class="article-body-row full-left">
          <p><b>Descripcion Entrada:</b></p>
          <textarea runat="server" class="article-body-row" id="txtDescEntrada"></textarea>
        </div>
        <div class="article-body-row full-left">
          <p><b>Descripcion Salida:</b></p>
          <textarea runat="server" class="article-body-row" id="txtDescSalida"></textarea>
        </div>
        <div class="article-body-row full-left">
          <p><b>Kms entrada:</b></p>
          <asp:TextBox runat="server" ID="txtKms" />
        </div>
        <div class="article-body-row full-left">
          <p><b>Costo:</b></p>
          <asp:TextBox runat="server" ID="txtCosto" />
        </div>
        <div class="article-buttons">
          <asp:Button runat="server" Text="Guardar" ID="btnSave" OnClick="btnSave_Click" />
          <asp:Button runat="server" Text="Finalizar" ID="btnComplete" OnClick="btnComplete_Click"  />
        </div>
      </div>
    </div>
  </article>
  <article>
    <h2 class="article-title">Repuestos usados</h2>
    <hr />
    <div class="root">
      <div class="row">
        <div class="top">
          <div>
            <p><b>Repuesto:</b></p>
            <asp:DropDownList runat="server" ID="ddlRepuestos" CssClass="cmb" />
          </div>
          <div>
            <p><b>Cantidad:</b></p>
            <asp:TextBox runat="server" ID="txtAmount" CssClass="amount" />
          </div>
          <div>
            <p>.</p>
            <asp:Button runat="server" Text="Asociar" OnClick="Unnamed1_Click" />
          </div>
        </div>
        <div class="top">
          <div>
            <p><b>Repuestos asociados a la reparación:</b></p>
            <asp:ListBox runat="server" ID="listAsociados" CssClass="wide" />
          </div>
          <div>
            <p>.</p>
            <asp:Button runat="server" Text="Eliminar" OnClick="Unnamed2_Click" />
          </div>
        </div>
        <div class="top">
          <div>
            <p>
              <b>Total: $</b>
            </p>
          </div>
          <div>
            <p>
              <asp:Literal runat="server" ID="lblTotal"></asp:Literal>
            </p>
          </div>
        </div>
      </div>
    </div>
  </article>
</asp:Content>
