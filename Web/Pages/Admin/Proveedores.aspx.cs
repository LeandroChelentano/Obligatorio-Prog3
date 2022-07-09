using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages.Admin {
  public partial class Proveedores : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed) {
          Cargar();
        }
      }
    }
    private void Cargar() {
      List<Proveedor> ListaProveedores = Domain.domain.ControladoraProveedores.ListaProveedores;

      proveedoresDisplayer.Items.Clear();
      foreach (Proveedor proveedor in ListaProveedores)
        proveedoresDisplayer.Items.Add(new ListItem(proveedor.ToString(), proveedor.Id.ToString()));

      Limpiar();
    }
    private void Limpiar() {
      txtNombre.Value = "";
      txtDireccion.Value = "";
      txtTelefono.Value = "";
    }

    protected void add_Click(object sender, EventArgs e) {
      string nombre = txtNombre.Value;
      string direccion = txtDireccion.Value;
      string telefono = txtTelefono.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { nombre, direccion, telefono });
      if (somethingEmpty) {
        utils.utils.SendAlert("El nombre no puede estar en blanco.");
        return;
      }

      if (telefono.Length > 9) {
        utils.utils.SendAlert("El telefono no puede tener mas de nueve caracteres.");
        return;
      }

      List<Proveedor> ListaProveedores = Domain.domain.ControladoraProveedores.ListaProveedores;
      short id = short.Parse((ListaProveedores[ListaProveedores.Count - 1].Id + 1).ToString());

      Proveedor proveedor = new Proveedor(id, nombre, direccion, telefono);

      Domain.domain.ControladoraProveedores CP = new Domain.domain.ControladoraProveedores();
      if (!CP.NuevoProveedor(proveedor))
        utils.utils.SendAlert("Se ha producido un error, parece que el nombre ingresado ya existe.");

      Cargar();
    }

    protected void delete_Click(object sender, EventArgs e) {
      string id = proveedoresDisplayer.SelectedValue;

      bool somethingEmpty = utils.utils.ValidateEmpty(id);
      if (somethingEmpty) {
        utils.utils.SendAlert("Debes seleccionar un proveedor antes.");
        return;
      }

      Domain.domain.ControladoraProveedores CP = new Domain.domain.ControladoraProveedores();
      Proveedor proveedor = CP.BuscarProveedor(short.Parse(id));

      if (!CP.EliminarProveedor(proveedor))
        utils.utils.SendAlert("Se ha producido un error..");

      Cargar();
    }

    protected void modify_Click(object sender, EventArgs e) {
      string id = proveedoresDisplayer.SelectedValue;
      string nombre = txtNombre.Value;
      string direccion = txtDireccion.Value;
      string telefono = txtTelefono.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { id, nombre, direccion, telefono });
      if (somethingEmpty) {
        utils.utils.SendAlert("El nombre no puede estar en blanco.");
        return;
      }

      if (telefono.Length > 9) {
        utils.utils.SendAlert("El telefono no puede tener mas de nueve caracteres.");
        return;
      }

      Proveedor proveedor = new Proveedor(short.Parse(id), nombre, direccion, telefono);

      Domain.domain.ControladoraProveedores CP = new Domain.domain.ControladoraProveedores();
      if (!CP.ModificarProveedor(proveedor))
        utils.utils.SendAlert("Se ha producido un error..");

      Cargar();
    }

    protected void clear_Click(object sender, EventArgs e) {
      Limpiar();
    }

    protected void proveedoresDisplayer_SelectedIndexChanged(object sender, EventArgs e) {
      string id = proveedoresDisplayer.SelectedValue;

      bool somethingEmpty = utils.utils.ValidateEmpty(id);
      if (somethingEmpty)
        return;

      Domain.domain.ControladoraProveedores CP = new Domain.domain.ControladoraProveedores();
      Proveedor proveedor = CP.BuscarProveedor(short.Parse(id));

      txtNombre.Value = proveedor.Nombre;
      txtDireccion.Value = proveedor.Direccion;
      txtTelefono.Value = proveedor.Telefono;
    }
  }
}