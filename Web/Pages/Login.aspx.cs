using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelo;
using Domain;

namespace Web {
  public partial class Login : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
      Web.Controladora.ControladoraWeb.validateToken();
    }

    //Log in
    protected void Unnamed1_Click(object sender, EventArgs e) {
      string email = txtUser.Value;
      string pass = txtPass.Value;

      bool somethingEmpty = utils.utils.ValidateEmptyList(new List<string> { email, pass });
      if (somethingEmpty)
        utils.utils.SendAlert("Hay elementos en blanco.");

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Cliente cliente = C.ValidarInicioDeSesion(email, pass);
      if (cliente == null) {
        utils.utils.SendAlert("Credenciales incorrectas.");
        return;
      }

      Controladora.ControladoraWeb.setCliente(cliente);
      Response.Redirect("/");
    }
  }
}