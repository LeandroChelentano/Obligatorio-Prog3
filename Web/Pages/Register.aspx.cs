using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web {
  public partial class Register : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      Web.Controladora.ControladoraWeb.validateToken();
    }

    //Register
    protected void Unnamed1_Click(object sender, EventArgs e) {
      string email = txtEmail.Value;
      string name = txtNombre.Value;
      string ci = txtCi.Value;
      string telefono = txtTelefono.Value;
      string pass = txtPass.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { email, name, ci, telefono, pass });
      if (somethingEmpty)
        utils.utils.SendAlert("Hay elementos en blanco.");

      bool status = Controladora.ControladoraWeb.Registrarse(name, email, ci, telefono, pass);

      
      if (!status) {
        utils.utils.SendAlert("El correo o la cedula ya se encuentran registradas.");
        return;
      }

      utils.utils.SendAlert("Sesion iniciada");
      Response.Redirect("/");
    }
  }
}