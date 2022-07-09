using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;

namespace Web {
  public partial class UserVehicles : System.Web.UI.Page {
    public static Vehiculo selectedVehicle = null;
    protected void Page_Load(object sender, EventArgs e) {
      if (!IsPostBack) {
        if (Web.Controladora.ControladoraWeb.currentClient == null) {
          Response.Redirect("/");
          return;
        }

        CargarLista();
        CargarMarcas();
      }
    }

    public void CargarMarcas() {
      Domain.domain.Controladora C = new Domain.domain.Controladora();
      List<Marca> ListaMarcas = C.GetMarca();

      if (ddlMarca != null) {
        ddlMarca.DataSource = ListaMarcas;
        ddlMarca.DataTextField = "Nombre";
        ddlMarca.DataValueField = "Id";
        ddlMarca.DataBind();
      }
    }

    public void CargarLista() {
      Cliente currentClient = Web.Controladora.ControladoraWeb.currentClient;
      List<Vehiculo> VehiculosEnPropiedad = currentClient == null ? new List<Vehiculo>() : currentClient.VehiculosEnPropiedad;

      if (listVehiculos == null)
        return;

      listVehiculos.Items.Clear();
      foreach (Vehiculo v in VehiculosEnPropiedad)
        listVehiculos.Items.Add(new ListItem(v.ToString(), v.Id.ToString()));

      Limpiar();
    }

    public void Limpiar() {
      label.InnerText = "Añade un vehiculo";
      selectedVehicle = null;
      txtMatricula.Value = "";
      numAnio.Value = "";
      ddlMarca.SelectedIndex = 0;
      txtModelo.Value = "";
      txtColor.Value = "";
      listVehiculos.SelectedIndex = -1;
    }

    protected void btnSave_Click(object sender, EventArgs e) {
      //string codigo = txtIdentificador.Value;
      string matricula = txtMatricula.Value;
      string anio = numAnio.Value;
      string marca = ddlMarca.SelectedValue;
      string modelo = txtModelo.Value;
      string color = txtColor.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { matricula, anio, marca, modelo, color });
      if (somethingEmpty) {
        utils.utils.SendAlert("Hay elementos en blanco.");
        return;
      }

      bool wrongFormat = utils.utils.ValidateNum(anio);
      if (wrongFormat) {
        utils.utils.SendAlert("El año debe ser un numero.");
        return;
      }

      short Anio = short.Parse(anio);

      //Validacion de formato de matriculas?

      //Validacion de matricula unica?

      if (selectedVehicle == null) {
        // Agregar vehiculo
        Domain.domain.Controladora C = new Domain.domain.Controladora();
        Marca vMarca = C.BuscarMarca(short.Parse($"{marca}"));

        Cliente currentClient = Controladora.ControladoraWeb.currentClient;
        Vehiculo vehiculo = new Vehiculo(Domain.domain.Controladora.getNewIdForVehicle(), matricula, Anio, color, vMarca, modelo);
        if (!C.NuevoVehiculo(currentClient, vehiculo, DateTime.Now))
          utils.utils.SendAlert("Ha ocurrido un error.");

        Limpiar();
        CargarLista();
      } else {
        // Modificar vehiculo
        Domain.domain.Controladora C = new Domain.domain.Controladora();
        Marca vMarca = C.BuscarMarca(short.Parse($"{marca}"));

        Cliente currentClient = Controladora.ControladoraWeb.currentClient;
        Vehiculo nuevoVehiculo = new Vehiculo(selectedVehicle.Id, matricula, Anio, color, vMarca, modelo);

        if (!C.ModificarVehiculo(currentClient, nuevoVehiculo)) {
          utils.utils.SendAlert("Ha ocurrido un error.");
        }

        Limpiar();
        CargarLista();
      }
    }

    protected void btnClear_Click(object sender, EventArgs e) {
      Limpiar();
    }

    protected void btnEdit_Click(object sender, EventArgs e) {
      CargarDatosDeSeleccion();
    }

    protected void listVehiculos_SelectedIndexChanged(object sender, EventArgs e) {
      string value = listVehiculos.SelectedValue;
      if (string.IsNullOrEmpty(value))
        return;

      Cliente currentClient = Controladora.ControladoraWeb.currentClient;
      if (currentClient != null)
        selectedVehicle = currentClient.BuscarVehiculo(short.Parse(value));
    }

    public void CargarDatosDeSeleccion() {
      if (selectedVehicle != null) {
        label.InnerText = "Modifica tu vehiculo";
        txtMatricula.Value = selectedVehicle.Matricula;
        numAnio.Value = selectedVehicle.Anio.ToString();
        txtModelo.Value = selectedVehicle.Modelo;
        txtColor.Value = selectedVehicle.Color;

        Domain.domain.Controladora C = new Domain.domain.Controladora();
        List<Marca> ListaMarcas = C.GetMarca();
        for (int i = 0; i < ListaMarcas.Count; i++)
          if (ListaMarcas[i] == selectedVehicle.Marca)
            ddlMarca.SelectedIndex = i;
      }
    }

    protected void btnSell_Click(object sender, EventArgs e) {
      utils.utils.SendAlert("Debes contactar con un administrador para esto.");
    }

    protected void btnChangePass_Click(object sender, EventArgs e) {
      string old = txtVieja.Value;
      string now = txtNueva.Value;

      if (old == "" || now == "")
        return;

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      if (!C.CambiarContrasenia(Web.Controladora.ControladoraWeb.currentClient, old, now)) {
        utils.utils.SendAlert("Contraseña incorrecta.");
        return;
      }

      txtVieja.Value = "";
      txtNueva.Value = "";

      utils.utils.SendAlert("Operacion aceptada, contraseña cambiada.");
    }
  }
}