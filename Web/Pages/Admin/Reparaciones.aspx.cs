using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Domain;

namespace Web.Pages.Admin {
  public partial class Reparaciones : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed)
          Cargar();
      }
    }

    protected void btnConfirm_Click(object sender, EventArgs e) {
      if (listNews == null)
        return;

      if (listNews.Items.Count == 0)
        return;

      string value = listNews.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva selectedReserva = C.BuscarReserva(short.Parse(listNews.SelectedValue));
      C.CambiarEstadoReserva(selectedReserva, 'r');

      Cargar();
      Limpiar();
    }

    protected void btnCancel_Click(object sender, EventArgs e) {
      if (listNews == null)
        return;

      if (listNews.Items.Count == 0)
        return;

      string value = listNews.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva selectedReserva = C.BuscarReserva(short.Parse(listNews.SelectedValue));

      C.CambiarEstadoReserva(selectedReserva, 'x');

      Cargar();
      Limpiar();
    }

    protected void btnComplete_Click(object sender, EventArgs e) {
      if (listConfirmed == null)
        return;

      if (listConfirmed.Items.Count == 0)
        return;

      string value = listConfirmed.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva selectedReserva = C.BuscarReserva(short.Parse(listConfirmed.SelectedValue));


      foreach (Vehiculo vehiculo in selectedReserva.Cliente.VehiculosEnPropiedad)
        foreach (Reparacion reparacion in vehiculo.HistorialReparaciones)
          if (reparacion.Reserva.Id == selectedReserva.Id)
            Controladora.ControladoraWeb.setReparacion(reparacion);

      Response.Redirect("/admin/reparacion");
    }

    protected void btnDetails_Click(object sender, EventArgs e) {
      if (listCompleted == null)
        return;

      if (listCompleted.Items.Count == 0)
        return;

      string value = listCompleted.SelectedValue;
      if (value == null || value == "")
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva selectedReserva = C.BuscarReserva(short.Parse(listCompleted.SelectedValue));

      foreach (Vehiculo vehiculo in selectedReserva.Cliente.VehiculosEnPropiedad)
        foreach (Reparacion reparacion in vehiculo.HistorialReparaciones)
          if (reparacion.Reserva.Id == selectedReserva.Id)
            Controladora.ControladoraWeb.setReparacion(reparacion);

      Response.Redirect("/admin/reparacion");
    }

    public void Limpiar() {
      listNews.SelectedIndex = -1;
      listCanceled.SelectedIndex = -1;
      listConfirmed.SelectedIndex = -1;
      listCompleted.SelectedIndex = -1;
    }

    public void Cargar() {
      Limpiar();

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Reserva> ListaReservas = C.GetReservas();

      listNews.Items.Clear();
      listCanceled.Items.Clear();
      listConfirmed.Items.Clear();
      listCompleted.Items.Clear();
      foreach (Reserva reserva in ListaReservas)
        switch (reserva.Estado) {
          case 'p':
            listNews.Items.Add(new ListItem(reserva.ToString(), reserva.Id.ToString()));
            break;
          case 'x':
            listCanceled.Items.Add(new ListItem(reserva.ToString(), reserva.Id.ToString()));
            break;
          case 'r':
            listConfirmed.Items.Add(new ListItem(reserva.ToString(), reserva.Id.ToString()));
            break;
          case 'c':
            listCompleted.Items.Add(new ListItem(reserva.ToString(), reserva.Id.ToString()));
            break;
        }
    }
  }
}