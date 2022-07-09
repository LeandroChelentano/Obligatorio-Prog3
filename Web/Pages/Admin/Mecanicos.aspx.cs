using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages.Admin {
  public partial class Mecanicos : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed) {
          CargarLista();

          cmbActividad.Items.Clear();
          cmbActividad.Items.Add(new ListItem("Activo", "a"));
          cmbActividad.Items.Add(new ListItem("Inactivo", "i"));
        }
      }
    }
    public void CargarLista() {
      List<Mecanico> ListaMecanicos = Domain.domain.ControladoraMecanicos.ListaMecanicos;

      mecanicosDisplayer.Items.Clear();
      foreach (Mecanico mecanico in ListaMecanicos)
        mecanicosDisplayer.Items.Add(new ListItem(mecanico.ToString(), mecanico.Codigo.ToString()));

      Limpiar();
    }
    public void Limpiar() {
      txtCedula.Value = "";
      txtNombre.Value = "";
      txtTelefono.Value = "";
      cmbActividad.SelectedIndex = 0;
    }

    protected void mecanicosDisplayer_SelectedIndexChanged(object sender, EventArgs e) {
      string value = mecanicosDisplayer.SelectedValue;
      if (value == null || value == "")
        return;

      Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();

      Mecanico mecanico = CM.BuscarMecanico(short.Parse(value));

      if (mecanico == null) {
        utils.utils.SendAlert("Se ha producido un error.");
        return;
      }

      txtCedula.Value = mecanico.Ci;
      txtNombre.Value = mecanico.Nombre;
      txtTelefono.Value = mecanico.Telefono;
      cmbActividad.SelectedIndex = mecanico.Activo ? 0 : 1;
    }

    protected void add_Click(object sender, EventArgs e) {
      string cedula = txtCedula.Value;
      string nombre = txtNombre.Value;
      string telefono = txtTelefono.Value;
      string actividad = cmbActividad.SelectedItem.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { cedula, nombre, telefono, actividad });
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      List<Mecanico> mecanicos = Domain.domain.ControladoraMecanicos.ListaMecanicos;
      string id = "0";
      if (mecanicos.Count > 0)
        id = (mecanicos[mecanicos.Count - 1].Codigo + 1).ToString();

      bool act = actividad == "a" ? true : false;
      Mecanico mecanico = new Mecanico(short.Parse(id), nombre, cedula, telefono, DateTime.Now);
      mecanico.Activo = act;

      Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();
      if (!CM.NuevoMecanico(mecanico))
        utils.utils.SendAlert("Se ha producido un error, parece que la cedula ingresada ya existe.");

      CargarLista();
    }

    protected void delete_Click(object sender, EventArgs e) {
      string codigo = mecanicosDisplayer.SelectedItem.Value;

      bool somethingEmpty = utils.utils.ValidateEmpty(codigo);
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();

      Mecanico mecanico = CM.BuscarMecanico(short.Parse(codigo));

      if (!CM.EliminarMecanico(mecanico))
        utils.utils.SendAlert("Se ha producido un error.");

      CargarLista();

    }

    protected void modify_Click(object sender, EventArgs e) {
      string value = mecanicosDisplayer.SelectedValue;
      if (value == null || value == "")
        return;

      string cedula = txtCedula.Value;
      string nombre = txtNombre.Value;
      string telefono = txtTelefono.Value;
      string actividad = cmbActividad.SelectedItem.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { cedula, nombre, telefono, actividad });
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      Domain.domain.ControladoraMecanicos CM = new Domain.domain.ControladoraMecanicos();
      Mecanico oldMec = CM.BuscarMecanico(short.Parse(value));

      bool act = actividad == "a" ? true : false;
      Mecanico mecanico = new Mecanico(short.Parse(value), nombre, cedula, telefono, oldMec.FechaIngreso);
      mecanico.Activo = act;

      if (!CM.ModificarMecanico(mecanico))
        utils.utils.SendAlert("Se ha producido un error..");

      CargarLista();
    }

    protected void clear_Click(object sender, EventArgs e) {
      Limpiar();
    }
  }
}