<%@ Page Title="Gestion de Vehiculos" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Vehicles.aspx.cs" Inherits="Web.UserVehicles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <article>
    <h2 runat="server" id="label">Añade un vehiculo</h2>
    <hr>
    <div>
      <p>Matricula:</p>
      <input runat="server" type="text" id="txtMatricula" />
    </div>
    <div>
      <p>Año:</p>
      <input runat="server" type="number" min="1900" max="2022" step="1" value="2022" id="numAnio" />
    </div>
    <div>
      <p>Color:</p>
      <input runat="server" type="text" id="txtColor" />
    </div>
    <div>
      <p>Marca:</p>
      <asp:DropDownList ID="ddlMarca" runat="server">
        <asp:ListItem Text="Selecciona una opcion" />
      </asp:DropDownList>
    </div>
    <div>
      <p>Modelo:</p>
      <input runat="server" type="text" id="txtModelo" />
    </div>
    <div class="buttons">
      <asp:Button ID="btnSave" Text="Guardar" runat="server" OnClick="btnSave_Click" />
      <asp:Button ID="btnClear" Text="Limpiar" runat="server" OnClick="btnClear_Click" />
    </div>
  </article>
  <article>
    <h2>Añade un vehiculo</h2>
    <asp:ListBox runat="server" ID="listVehiculos" CssClass="dataList" AutoPostBack="True" OnSelectedIndexChanged="listVehiculos_SelectedIndexChanged" />
    <div class="buttons">
      <asp:Button ID="btnEdit" Text="Modificar selección" runat="server" OnClick="btnEdit_Click" />
      <asp:Button ID="btnSell" Text="Vender" runat="server" OnClick="btnSell_Click" />
    </div>
  </article>
  <article>
    <h2 runat="server" id="H1">Cambiar contraseña</h2>
    <hr>
    <div>
      <p>Nueva:</p>
      <input runat="server" type="password" id="txtNueva" />
    </div>
    <div>
      <p>Anterior:</p>
      <input runat="server" type="password" id="txtVieja" />
    </div>
    <div class="buttons">
      <asp:Button ID="btnChangePass" Text="Guardar" runat="server" OnClick="btnChangePass_Click" />
    </div>
  </article>
</asp:Content>
