using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages.Admin {
  public partial class SpecificReparation : System.Web.UI.Page {
    private static Reparacion reparacion = null;
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        if (Web.Controladora.ControladoraWeb.currentClient == null ||
            Web.Controladora.ControladoraWeb.reparacion == null) {
          Response.Redirect("/admin");
          return;
        }

        reparacion = Web.Controladora.ControladoraWeb.reparacion;

        lblNombre.Text = reparacion.Reserva.Cliente.Nombre;
        lblCI.Text = reparacion.Reserva.Cliente.Ci;

        List<Repuesto> ListaRepuestos = Domain.domain.ControladoraRepuestos.ListaRepuestos;
        ddlRepuestos.Items.Clear();
        foreach (Repuesto repuesto in ListaRepuestos)
          ddlRepuestos.Items.Add(new ListItem(repuesto.ToString(), repuesto.Codigo));

        Cargar();
        cargarMecanicos();
        seleccionarMecanico();
      }
    }

    public void cargarMecanicos() {
      List<Mecanico> ListaMecanicos = Domain.domain.ControladoraMecanicos.ListaMecanicos;
      ddlMecanicos.Items.Clear();
      foreach (Mecanico mecanico in ListaMecanicos)
        if (mecanico.Activo)
          ddlMecanicos.Items.Add(new ListItem(mecanico.ToString(), mecanico.Codigo.ToString()));

      //por si el mecanico se encuentra ahora inactivo, pero no cuando se le fue asignado
      if (reparacion.Mecanico != null)
        if (!reparacion.Mecanico.Activo)
          ddlMecanicos.Items.Add(new ListItem(reparacion.Mecanico.ToString(), reparacion.Mecanico.Codigo.ToString()));
    }
    public void seleccionarMecanico() {
      if (reparacion.Mecanico != null)
        for (int i = 0; i < ddlMecanicos.Items.Count; i++)
          if (ddlMecanicos.Items[i].Value == reparacion.Mecanico.Codigo.ToString())
            ddlMecanicos.SelectedIndex = i;
    }

    protected void ddlMecanicos_SelectedIndexChanged(object sender, EventArgs e) {
      //Domain.domain.Controladora C = new Domain.domain.Controladora();
      //Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();

      //short mecId = short.Parse(ddlMecanicos.SelectedValue);
      //Mecanico mecanico = CM.BuscarMecanico(mecId);

      //if (!C.AsignarMecanico(reparacion, mecanico)) {
      //  utils.utils.SendAlert("Se ha producido un error..");
      //  return;
      //}

      //cargarMecanicos();
      //seleccionarMecanico();
    }

    protected void btnSave_Click(object sender, EventArgs e) {
      string costo = txtCosto.Text;
      string desEntrada = txtDescEntrada.Value;
      string desSalida = txtDescSalida.Value;
      string kms = txtKms.Text;
      string mec = ddlMecanicos.SelectedValue;

      bool empty = utils.utils.ValidateEmptyList(new List<string> { costo, desEntrada, desSalida, kms, mec });
      if (empty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();
      Mecanico mecanico = CM.BuscarMecanico(short.Parse(mec));

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Reserva reserva = C.BuscarReserva(reparacion.Reserva.Id);

      Reparacion rep = new Reparacion(reserva);
      rep.Mecanico = mecanico;
      
      try {
        int.Parse(costo);
        int.Parse(kms);
      } catch {
        utils.utils.SendAlert("El costo y los kilometros deben ser numericos.");
        return;
      }
      
      rep.Costo = int.Parse(costo);
      rep.KmsEntrada = int.Parse(kms);
      rep.DescEntrada = desEntrada;
      rep.DescSalida = desSalida;

      if (!C.ActualizarReparacion(rep)) {
        utils.utils.SendAlert("Parece que ha ocurrido un error en la base de datos.");
        return;
      }

      Cargar();
    }
    public void Importe() {
      double importe = 0;

      if (reparacion.RepuestosUsados != null)
        foreach (RepuestoCantidad r in reparacion.RepuestosUsados)
          importe += r.Repuesto.Costo * r.Cantidad;

      lblTotal.Text = importe.ToString();
    }

    public void Cargar() {
      seleccionarMecanico();
      Importe();
      txtDescEntrada.Value = reparacion.DescEntrada;
      txtDescSalida.Value = reparacion.DescSalida;
      txtCosto.Text = reparacion.Costo.ToString();
      txtKms.Text = reparacion.KmsEntrada.ToString();

      txtAmount.Text = "1";

      listAsociados.Items.Clear();
      if (reparacion.RepuestosUsados != null)
        foreach (RepuestoCantidad RC in reparacion.RepuestosUsados)
          listAsociados.Items.Add(new ListItem(RC.ToString(), RC.Repuesto.Codigo));
    }

    protected void Unnamed1_Click(object sender, EventArgs e) {
      //Asociar

      string repuesto = ddlRepuestos.SelectedValue;
      string cantidad = txtAmount.Text;
      if (repuesto == "" || cantidad == "")
        return;

      try {
        int.Parse(cantidad);
      } catch {
        utils.utils.SendAlert("La cantidad debe ser numerica.");
        return;
      }

      Domain.domain.ControladoraRepuestos CR = new Domain.domain.ControladoraRepuestos();

      Repuesto rep = CR.BuscarRepuestoPorCodigo(repuesto);

      bool alreadyAdded = false;
      int amountAlreadyAdded = 0;
      foreach (RepuestoCantidad RC in reparacion.RepuestosUsados)
        if (RC.Repuesto.Codigo == rep.Codigo) {
          alreadyAdded = true;
          amountAlreadyAdded = RC.Cantidad;
        }

      if (alreadyAdded) {
        int amount = amountAlreadyAdded + int.Parse(cantidad);
        if (!CR.actualizarReparacionRepuesto(reparacion, rep, amount)) {
          utils.utils.SendAlert("Ha ocurrido un error..");
          return;
        }
      } else {
        if (!CR.nuevaReparacionRepuesto(reparacion, rep, int.Parse(cantidad))) {
          utils.utils.SendAlert("Ha ocurrido un error..");
          return;
        }
      }

      txtAmount.Text = "1";

      Cargar();
    }

    protected void Unnamed2_Click(object sender, EventArgs e) {
      //Delete

      string rep = listAsociados.SelectedValue;

      if (rep == "")
        return;

      Domain.domain.ControladoraRepuestos CR = new Domain.domain.ControladoraRepuestos();
      Repuesto repuesto = CR.BuscarRepuestoPorCodigo(rep);

      if (!CR.eliminarReparacionRepuesto(reparacion, repuesto)) {
        utils.utils.SendAlert("Ha ocurrido un error..");
        return;
      }

      Cargar();
    }

    protected void btnComplete_Click(object sender, EventArgs e) {
      if (reparacion == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();

      if (!C.FinalizarReparacion(reparacion)) {
        utils.utils.SendAlert("Ha ocurrido un error..");
        return;
      }
    }
  }
}