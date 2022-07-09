using System;
using System.Collections.Generic;

namespace Modelo {
  public class Administrador : Cliente {
    private bool esOwner;

    #region Getters-Setters
    public bool EsOwner {
      get { return esOwner; }
      set { esOwner = value; }
    }
    #endregion

    public override string ToString() {
      return $"{base.ToString()} {esOwner}";
    }

    public Administrador(short aId, string aNombre, string aCi, string aTelefono, string aEmail, string aPass, DateTime aFechaRegistro, bool aEsOwner)
      : base(aId, aNombre, aCi, aTelefono, aEmail, aPass, aFechaRegistro) {
      EsOwner = aEsOwner;
    }

    public Administrador() { }
  }
}