﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web {
  public partial class Users : System.Web.UI.MasterPage {
    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void logout1_Click(object sender, EventArgs e) {
      Web.Controladora.ControladoraWeb.cerrarSesion();
      Response.Redirect("/");
    }
  }
}