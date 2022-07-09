using System;
using System.Collections.Generic;

namespace Modelo {
  public class Repuesto {
    private string codigo;
    private string descripcion;
    private double costo;
    private RepuestoTipo tipo;
    private Proveedor proveedor;

    #region Getters-Setters
    public string Codigo {
      get { return codigo; }
      set { codigo = value; }
    }
    public string Descripcion {
      get { return descripcion; }
      set { descripcion = value; }
    }
    public double Costo {
      get { return costo; }
      set { costo = value; }
    }
    public RepuestoTipo Tipo {
      get { return tipo; }
      set { tipo = value; }
    }
    public Proveedor Proveedor {
      get { return proveedor; }
      set { proveedor = value; }
    }
    #endregion

    public override string ToString() {
      return $"{codigo} - {descripcion} - ${costo}, {tipo}";
    }

    public Repuesto(string aCodigo, string aDescripcion, double aCosto, RepuestoTipo aTipo, Proveedor aProveedor) { 
      Codigo = aCodigo;
      Descripcion = aDescripcion;
      Costo = aCosto;
      Tipo = aTipo;
      Proveedor = aProveedor;
    }

    public Repuesto() { }
  }
}