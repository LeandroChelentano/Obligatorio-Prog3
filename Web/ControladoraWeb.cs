using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Modelo;


namespace Web.Controladora {
  public class ControladoraWeb {
    public static string currentToken = null;
    public static Cliente currentClient = null;
    public static Reparacion reparacion = null;

    public static void setCliente(Cliente aCliente) {
      currentToken = aCliente.Token;
      currentClient = aCliente;
    }

    public static void setReparacion(Reparacion aReparacion) {
      reparacion = aReparacion;
    }

    public static void cerrarSesion() {
      currentToken = null;
      currentClient = null;
    }

    public static void validateToken() {
      Domain.domain.Controladora C = new Domain.domain.Controladora();

      if (currentToken == null)
        return;

      Cliente cliente = C.ValidarToken(currentToken);
      if (cliente == null)
        return;
      
      setCliente(cliente);
    }

    public static bool isAdmin() {
      if (currentToken == null)
        return false;

      Domain.domain.Controladora controladora = new Domain.domain.Controladora();
      List<Cliente> clientes = controladora.GetClientes();

      foreach(var cliente in clientes)
        if (cliente.Token == currentToken)
          if (cliente.GetType() == typeof(Administrador)) {
            return true;
          }

      return false;
    }

    public static bool isOwner() {
      Domain.domain.Controladora controladora = new Domain.domain.Controladora();
      if (currentClient == null)
        return false;

      Cliente cliente = controladora.BuscarCliente(currentClient.Id);
      return cliente is Administrador admin && admin.EsOwner;
    }

    public static bool Registrarse(string aName, string aMail, string aCi, string aTelefono, string aPass) {
      short newId = Domain.domain.Controladora.getNextIdForClient();
      Cliente cliente = new Cliente(newId, aName, aCi, aTelefono, aMail, aPass, DateTime.Now);

      Domain.domain.Controladora C = new Domain.domain.Controladora();
      Cliente responseClient = C.Registrarse(cliente);

      if (responseClient != null)
        setCliente(responseClient);

      return responseClient == null ? false : true;
    }
  }
}