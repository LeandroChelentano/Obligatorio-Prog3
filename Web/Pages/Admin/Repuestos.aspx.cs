using Domain.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Domain;

namespace Web {
  public partial class Repuestos : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed) {
          CargarLista();

          Domain.domain.Controladora C = new Domain.domain.Controladora();
          List<RepuestoTipo> ListaTipos = C.GetTipos();
          cmbTipoR.DataSource = ListaTipos;
          cmbTipoR.DataTextField = "Nombre";
          cmbTipoR.DataValueField = "Id";
          cmbTipoR.DataBind();

          List<Proveedor> ListaProveedores = ControladoraProveedores.ListaProveedores;
          cmbProveedor.DataSource = ListaProveedores;
          cmbProveedor.DataTextField = "Nombre";
          cmbProveedor.DataValueField = "Id";
          cmbProveedor.DataBind();
        }
      }
    }

    public void CargarLista() {
      List<Repuesto> ListaRepuestos = ControladoraRepuestos.ListaRepuestos;

      repuestosDisplayer.Items.Clear();
      foreach (Repuesto r in ListaRepuestos)
        repuestosDisplayer.Items.Add(new ListItem(r.ToString(), r.Codigo));

      Limpiar();
    }

    protected void add_Click(object sender, EventArgs e) {
      string codigo = txtCodigo.Value;
      string descripcion = txtDescripcion.Value;
      string costo = txtCosto.Value;
      string tipo = cmbTipoR.SelectedItem.Value;
      string proveedor = cmbProveedor.SelectedItem.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { codigo, descripcion, costo });
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      ControladoraRepuestos CR = new ControladoraRepuestos();
      ControladoraProveedores CP = new ControladoraProveedores();
      Domain.domain.Controladora C = new Domain.domain.Controladora();

      RepuestoTipo RT = C.BuscarTipoPorCodigo(char.Parse(tipo));
      Proveedor P = CP.BuscarProveedor(short.Parse(proveedor));
      Repuesto repuesto = new Repuesto(codigo, descripcion, double.Parse(costo), RT, P);

      if (!CR.NuevoRepuesto(repuesto))
        utils.utils.SendAlert("Se ha producido un error, parece que el codigo ingresado ya existe.");

      CargarLista();
    }

    protected void delete_Click(object sender, EventArgs e) {
      string codigo = txtCodigo.Value;

      bool somethingEmpty = utils.utils.ValidateEmpty(codigo);
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      ControladoraRepuestos CR = new ControladoraRepuestos();

      Repuesto repuesto = CR.BuscarRepuestoPorCodigo(codigo);

      if (!CR.EliminarRepuesto(repuesto))
        utils.utils.SendAlert("Se ha producido un error.");

      CargarLista();
    }

    protected void modify_Click(object sender, EventArgs e) {
      string value = repuestosDisplayer.SelectedValue;
      if (value == null)
        return;

      string descripcion = txtDescripcion.Value;
      string costo = txtCosto.Value;
      string tipo = cmbTipoR.SelectedItem.Value;
      string proveedor = cmbProveedor.SelectedItem.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { descripcion, costo });
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      ControladoraRepuestos CR = new ControladoraRepuestos();
      ControladoraProveedores CP = new ControladoraProveedores();
      Domain.domain.Controladora C = new Domain.domain.Controladora();

      RepuestoTipo RT = C.BuscarTipoPorCodigo(char.Parse(tipo));
      Proveedor P = CP.BuscarProveedor(short.Parse(proveedor));
      Repuesto repuesto = new Repuesto(value, descripcion, double.Parse(costo), RT, P);

      if (!CR.ModificarRepuesto(repuesto))
        utils.utils.SendAlert("Se ha producido un error..");

      CargarLista();
    }

    protected void clear_Click(object sender, EventArgs e) {
      Limpiar();
    }

    public void Limpiar() {
      txtCodigo.Value = "";
      txtDescripcion.Value = "";
      txtCosto.Value = "";
      cmbTipoR.SelectedIndex = 0;
      cmbProveedor.SelectedIndex = 0;
    }


    protected void repuestosDisplayer_SelectedIndexChanged(object sender, EventArgs e) {
      string value = repuestosDisplayer.SelectedValue;
      if (value == null)
        return;

      ControladoraRepuestos CR = new ControladoraRepuestos();

      Repuesto repuesto = CR.BuscarRepuestoPorCodigo(value);

      if (repuesto == null) {
        utils.utils.SendAlert("Se ha producido un error.");
        return;
      }

      txtCodigo.Value = repuesto.Codigo;
      txtDescripcion.Value = repuesto.Descripcion;
      txtCosto.Value = repuesto.Costo.ToString();

      for (int i = 0; i < cmbTipoR.Items.Count; i++) {
        if (cmbTipoR.Items[i].Value == repuesto.Tipo.Id.ToString()) {
          cmbTipoR.SelectedIndex = i;
        }
      }

      for (int i = 0; i < cmbProveedor.Items.Count; i++) {
        if (cmbProveedor.Items[i].Value == repuesto.Proveedor.Id.ToString()) {
          cmbProveedor.SelectedIndex = i;
        }
      }
    }
  }
}