using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages.Admin {
  public partial class Marcas : System.Web.UI.Page {
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
      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Marca> ListaMarcas = C.GetMarca();
      repuestosDisplayer.DataSource = ListaMarcas;
      repuestosDisplayer.DataTextField = "Nombre";
      repuestosDisplayer.DataValueField = "Id";
      repuestosDisplayer.DataBind();

      Limpiar();
    }
    private void Limpiar() {
      txtMarca.Value = "";
    }


    protected void add_Click(object sender, EventArgs e) {
      string nombre = txtMarca.Value;

      bool somethingEmpty = utils.utils.ValidateEmpty(nombre);
      if (somethingEmpty) {
        utils.utils.SendAlert("El nombre no puede estar en blanco.");
        return;
      }

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Marca> ListaMarcas = C.GetMarca();
      short id = short.Parse(ListaMarcas[ListaMarcas.Count - 1].Id + 1.ToString());

      Marca marca = new Marca(id, nombre);

      if (!C.NuevaMarca(marca))
        utils.utils.SendAlert("Se ha producido un error, parece que el nombre ingresado ya existe.");

      Cargar();
    }

    protected void delete_Click(object sender, EventArgs e) {
      string id = repuestosDisplayer.SelectedValue;

      bool somethingEmpty = utils.utils.ValidateEmpty(id);
      if (somethingEmpty) {
        utils.utils.SendAlert("Debes seleccionar una marca antes.");
        return;
      }

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Marca marca = C.BuscarMarca(short.Parse(id));

      if (!C.EliminarMarca(marca))
        utils.utils.SendAlert("Se ha producido un error, parece que el nombre ingresado ya existe.");

      Cargar();
    }

    protected void modify_Click(object sender, EventArgs e) {
      string nombre = txtMarca.Value;
      string id = repuestosDisplayer.SelectedValue;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { id, nombre });
      if (somethingEmpty) {
        utils.utils.SendAlert("El nombre no puede estar en blanco.");
        return;
      }

      Domain.domain.Controladora C = new Domain.domain.Controladora();

      Marca marca = new Marca(short.Parse(id), nombre);

      if (!C.ModificarMarca(marca))
        utils.utils.SendAlert("Se ha producido un error, parece que el nombre ingresado ya existe.");

      Cargar();
    }

    protected void clear_Click(object sender, EventArgs e) {
      Limpiar();
    }

    protected void repuestosDisplayer_SelectedIndexChanged(object sender, EventArgs e) {
      string id = repuestosDisplayer.SelectedValue;

      bool somethingEmpty = utils.utils.ValidateEmpty(id);
      if (somethingEmpty)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Marca marca = C.BuscarMarca(short.Parse(id));

      txtMarca.Value = marca.Nombre;
    }
  }
}