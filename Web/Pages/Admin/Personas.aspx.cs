using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web.Pages {
  public partial class Personas : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        bool allowed = Web.Controladora.ControladoraWeb.isAdmin();

        if (!allowed)
          Response.Redirect("/");

        if (allowed)
          CargarPersonas();
      }
    }

    protected void btnPromote_Click(object sender, EventArgs e) {
      if (listUsers == null)
        return;

      if (listUsers.Items.Count == 0)
        return;

      string value = listUsers.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Cliente sC = C.BuscarCliente(short.Parse(listUsers.SelectedValue));

      if (C.PromoverCliente(sC, Web.Controladora.ControladoraWeb.currentToken)) {
        Limpiar();
        CargarPersonas();
      } else {
        utils.utils.SendAlert("Ha ocurrido un error..");
      }
    }


    protected void btnDegrade_Click(object sender, EventArgs e) {
      if (listAdmins == null)
        return;

      if (listAdmins.Items.Count == 0)
        return;

      string value = listAdmins.SelectedValue;
      if (value == null)
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Cliente sC = C.BuscarCliente(short.Parse(listAdmins.SelectedValue));
      if (sC == null)
        return;
 
      if (C.EliminarAdministrador(sC, Web.Controladora.ControladoraWeb.currentToken)) {
        Limpiar();
        CargarPersonas();
      } else {
        utils.utils.SendAlert("Ha ocurrido un error..");
      }
    }

    public bool isAdmin(Cliente aCliente) {
      if (aCliente.GetType() == typeof(Administrador))
        return true;

      return false;
    }

    public void CargarPersonas() {
      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Cliente> allPeople = C.GetClientes();

      List<Cliente> onlyClients = new List<Cliente>();
      List<Administrador> onlyAdmins = new List<Administrador>();
      foreach (Cliente cliente in allPeople)
        if (cliente is Administrador admin) {
          onlyAdmins.Add(admin);
        } else {
          onlyClients.Add(cliente);
        }

      listUsers.Items.Clear();
      foreach (Cliente cliente in onlyClients)
        listUsers.Items.Add(new ListItem(cliente.ToString(), cliente.Id.ToString()));

      listAdmins.Items.Clear();
      foreach (Administrador admin in onlyAdmins)
        if (!admin.EsOwner)
          listAdmins.Items.Add(new ListItem(admin.ToString(), admin.Id.ToString()));
    }

    public void Limpiar() {
      listAdmins.SelectedIndex = -1;
      listUsers.SelectedIndex = -1;
    }

    protected void btnDelete_Click(object sender, EventArgs e) {
      if (listUsers == null)
        return;

      if (listUsers.Items.Count == 0)
        return;

      string value = listUsers.SelectedValue;
      if (value == null || value == "")
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Cliente sC = C.BuscarCliente(short.Parse(listUsers.SelectedValue));

      if (C.EliminarCliente(sC)) {
        Limpiar();
        CargarPersonas();
      } else {
        utils.utils.SendAlert("Parece que el cliente tiene dependencias y no puede ser eliminado.");
      }
    }
  }
}