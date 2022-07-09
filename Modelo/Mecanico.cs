using System;
using System.Collections.Generic;

namespace Modelo {
  public class Mecanico {
    private short codigo;
    private string nombre;
    private string ci;
    private string telefono;
    private DateTime fechaIngreso;
    private bool activo;

    #region Getters-Setters
    public short Codigo {
      get { return codigo; }
      set { codigo = value; }
    }
    public string Nombre {
      get { return nombre; }
      set { nombre = value; }
    }
    public string Ci {
      get { return ci; }
      set { ci = value; }
    }
    public string Telefono {
      get { return telefono; }
      set { telefono = value; }
    }
    public DateTime FechaIngreso {
      get { return fechaIngreso; }
      set { fechaIngreso = value; }
    }
    public bool Activo {
      get { return activo; }
      set { activo = value; }
    }
    #endregion

    public override string ToString() {
      return $"{nombre}, {ci}, {telefono}, {(activo ? "ACTIVO" : "INACTIVO")}";
    }

    public Mecanico(short aCodigo, string aNombre, string aCi, string aTelefono, DateTime aFechaIngreso) {
      Codigo = aCodigo;
      Nombre = aNombre;
      Ci = aCi;
      Telefono = aTelefono;
      FechaIngreso = aFechaIngreso;
    }

    public Mecanico() { }
  }
}