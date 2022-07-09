using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Domain.domain {
  public class ControladoraProveedores {
    private static List<Proveedor> listaProveedores = new List<Proveedor>();
    public static List<Proveedor> ListaProveedores {
      set { listaProveedores = value; }
      get { return listaProveedores; }
    }

    public Proveedor BuscarProveedor(short aId) {
      foreach (Proveedor proveedor in ListaProveedores)
        if (proveedor.Id == aId)
          return proveedor;

      return null;
    }

    public bool NuevoProveedor(Proveedor aProveedor) {
      if (aProveedor == null)
        return false;

      if (!PProveedor.addProveedor(aProveedor))
        return false;

      listaProveedores.Add(aProveedor);
      return true;
    }
    public bool EliminarProveedor(Proveedor aProveedor) {
      if (aProveedor == null)
        return false;

      if (!PProveedor.removeProveedor(aProveedor))
        return false;

      listaProveedores.Remove(aProveedor);
      return true;
    }
    public bool ModificarProveedor(Proveedor aProveedor) {
      if (aProveedor == null)
        return false;

      if (!PProveedor.modifyProveedor(aProveedor))
        return false;

      Proveedor proveedor = BuscarProveedor(aProveedor.Id);
      proveedor.Nombre = aProveedor.Nombre;
      proveedor.Direccion = aProveedor.Direccion;
      proveedor.Telefono = aProveedor.Telefono;
      return true;
    }
  }
}