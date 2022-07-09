<%@ Page Title="Estadisticas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="Web.Pages.Admin.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPageContent" runat="server">
  <article>
    <h2 class="article-title">Reparaciones</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listReparaciones" CssClass="w-full" AutoPostBack="true" />
        <div>
          <div class="top mt-1">
            <span><b>Desde:</b></span>
            <asp:Calendar runat="server" ID="cFrom" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="cFrom_SelectionChanged">
              <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
              <NextPrevStyle VerticalAlign="Bottom" />
              <OtherMonthDayStyle ForeColor="#808080" />
              <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
              <SelectorStyle BackColor="#CCCCCC" />
              <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
              <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
              <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
          </div>
          <div class="top mt-1">
            <span><b>Hasta:</b></span>
            <asp:Calendar runat="server" ID="cTo" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="cTo_SelectionChanged">
              <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
              <NextPrevStyle VerticalAlign="Bottom" />
              <OtherMonthDayStyle ForeColor="#808080" />
              <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
              <SelectorStyle BackColor="#CCCCCC" />
              <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
              <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
              <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
          </div>
        </div>
      </div>
    </div>
  </article>
  <article>
    <h2 class="article-title">Repuestos mas vendidos</h2>
    <hr />
    <div class="articleRoot">
      <div class="article-body">
        <asp:ListBox runat="server" ID="listRepuestos" CssClass="w-full" AutoPostBack="true" />
      </div>
    </div>
  </article>
</asp:Content>
