using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Domain;

namespace Web.Pages.Client {
  public partial class Reparaciones : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        if (Web.Controladora.ControladoraWeb.currentClient == null) {
          Response.Redirect("/");
          return;
        }
      
        CargarReservas();
        CargarVehiculosDePropietario();
      }
    }

    protected void btnReservar_Click(object sender, EventArgs e) {
      Cliente currentCliente = Web.Controladora.ControladoraWeb.currentClient;

      DateTime date = datePicker.SelectedDate;
      string value = selVehiculos.SelectedValue;
      if (value == null || date == null)
        return;

      if (date.Day < DateTime.Now.Day) {
        utils.utils.SendAlert("La fecha debe ser posterior a la actual.");
        return;
      }

      Vehiculo vehiculo = null;
      foreach (Vehiculo v in currentCliente.VehiculosEnPropiedad)
        if (v.Id == short.Parse(value))
          vehiculo = v;

      if (vehiculo == null) {
        utils.utils.SendAlert("El vehiculo no existe..");
        return;
      }

      Reserva reserva = new Reserva(getNewIdForReservas(), date, currentCliente, vehiculo);
      Domain.domain.Controladora C = new Domain.domain.Controladora();

      if (!C.NuevaReserva(reserva, currentCliente)) {
        utils.utils.SendAlert("Hay un error con su reserva, intentelo nuevamente.");
        return;
      }

      utils.utils.SendAlert("Reserva colocada, a la brevedad un administrador se contactará con usted para coordinar la hora de la reparación.");
      CargarReservas();
      CargarVehiculosDePropietario();
    }

    protected void btnCancel_Click(object sender, EventArgs e) {
      if (listReservas == null)
        return;

      if (listReservas.Items.Count == 0)
        return;

      string value = listReservas.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva reserva = C.BuscarReserva(short.Parse(value));

      if (reserva.Estado == 'c') {
        utils.utils.SendAlert("No puedes cancelar una reparacion completada..");
        return;
      }

      C.CambiarEstadoReserva(reserva, 'x');

      CargarReservas();
      CargarVehiculosDePropietario();
    }

    public void CargarReservas() {
      Cliente currentClient = Web.Controladora.ControladoraWeb.currentClient;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Reserva> ListaReservas = C.GetReservas();

      listReservas.Items.Clear();
      foreach (Reserva reserva in ListaReservas)
        if (reserva.Cliente.Ci == currentClient.Ci)
          listReservas.Items.Add(new ListItem(reserva.Show(), reserva.Id.ToString()));
    }

    public void CargarVehiculosDePropietario() {
      Cliente currentClient = Web.Controladora.ControladoraWeb.currentClient;

      selVehiculos.Items.Clear();
      foreach (Vehiculo v in currentClient.VehiculosEnPropiedad)
        selVehiculos.Items.Add(new ListItem(v.ToString(), v.Id.ToString()));
    }

    public static short getNewIdForReservas() {
      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Reserva> ListaReservas = C.GetReservas();

      short id = 0;
      foreach (Reserva r in ListaReservas)
        if (r.Id > id)
          id = r.Id;

      return short.Parse($"{id + 1}");
    }
  }
}