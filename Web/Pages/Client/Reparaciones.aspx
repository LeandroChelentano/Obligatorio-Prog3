<%@ Page Title="" Language="C#" MasterPageFile="~/Users.Master" AutoEventWireup="true" CodeBehind="Reparaciones.aspx.cs" Inherits="Web.Pages.Client.Reparaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <article>
    <h2>Reservar turno</h2>
    <hr>
    <div>
      <p>Vehiculo:</p>
      <asp:DropDownList runat="server" ID="selVehiculos" AutoPostBack="True">
        <asp:ListItem Text="Seleccione un vehiculo" Selected="True" />
      </asp:DropDownList>
    </div>
    <summary class="col">
      <p>Seleccione una fecha</p>
      <asp:Calendar ID="datePicker" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" >
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    </summary>
    <div class="buttons">
      <asp:Button ID="btnNuevaReserva" Text="Reservar" runat="server" OnClick="btnReservar_Click" />
    </div>
  </article>
  <article>
    <h2>Estado de reservas</h2>
    <asp:ListBox runat="server" ID="listReservas" CssClass="dataList" AutoPostBack="true"></asp:ListBox>
    <div class="buttons">
      <asp:Button ID="btnCancel" Text="Cancelar selección" runat="server" OnClick="btnCancel_Click" />
    </div>
  </article>
</asp:Content>
