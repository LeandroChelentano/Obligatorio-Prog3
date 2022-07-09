using System;
using System.Collections.Generic;

namespace Modelo {
  public class Proveedor {
    private short id;
    private string nombre;
    private string direccion;
    private string telefono;

    #region Getters-Setters
    public short Id {
      get { return id; }
      set { id = value; }
    }
    public string Nombre {
      get { return nombre; }
      set { nombre = value; }
    }
    public string Direccion {
      get { return direccion; }
      set { direccion = value; }
    }
    public string Telefono {
      get { return telefono; }
      set { telefono = value; }
    }
    #endregion

    public override string ToString() {
      return $"{id}, {nombre}, {direccion}, {telefono}";
    }

    public Proveedor(short aId, string aNombre, string aDireccion, string aTelefono) {
      Id = aId;
      Nombre = aNombre;
      Direccion = aDireccion;
      Telefono = aTelefono;
    }

    public Proveedor() { }
  }
}