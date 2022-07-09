using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages.Admin {
  public partial class Estadisticas : System.Web.UI.Page {
    private static DateTime from = DateTime.Parse(DateTime.Now.ToShortDateString());
    private static DateTime to = DateTime.Parse(DateTime.Now.ToShortDateString());
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed) {
          Cargar();
          CargarRepuestos();
        }
      }
    }
    public void Cargar() {
      if (from > to) {
        listReparaciones.Items.Clear();
        return;
      }

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Reparacion> LR = C.GetReparacionesFiltro(from, to);

      listReparaciones.Items.Clear();
      foreach (Reparacion reparacion in LR)
        listReparaciones.Items.Add(new ListItem(reparacion.Stat()));
    }

    protected void cFrom_SelectionChanged(object sender, EventArgs e) {
      DateTime seleted = cFrom.SelectedDate;
      from = DateTime.Parse(seleted.ToShortDateString());
      Cargar();
    }

    protected void cTo_SelectionChanged(object sender, EventArgs e) {
      DateTime seleted = cTo.SelectedDate;
      to = DateTime.Parse(seleted.ToShortDateString());
      Cargar();
    }

    private void CargarRepuestos() {
      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<RepuestoCantidad> RC = C.GetRuepuestoEstadistica();

      listRepuestos.Items.Clear();
      foreach (RepuestoCantidad repuestoCantidad in RC)
        listRepuestos.Items.Add(new ListItem(repuestoCantidad.Stat()));
    }
  }
}