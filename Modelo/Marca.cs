using System;
using System.Collections.Generic;

namespace Modelo {
  public class Marca {
    private short id;
    private string nombre;

    #region Getters-Setters
    public short Id {
      get { return id; }
      set { id = value; }
    }
    public string Nombre {
      get { return nombre; }
      set { nombre = value; }
    }
    #endregion

    public override string ToString() {
      return $"{id}, {nombre}";
    }

    public Marca(short aId, string aNombre) {
      Id = aId;
      Nombre = aNombre;
    }

    public Marca() { }
  }
}