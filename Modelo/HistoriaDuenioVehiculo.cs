using System;
using System.Collections.Generic;

namespace Modelo {
  public class HistoriaDuenioVehiculo {
    private Cliente cliente;
    private DateTime fechaCompra;

    #region Getters-Setters
    public Cliente Cliente {
      get { return cliente; }
      set { cliente = value; }
    }
    public DateTime FechaCompra {
      get { return fechaCompra; }
      set { fechaCompra = value; }
    }
    #endregion

    public override string ToString() {
      return $"{cliente}, {fechaCompra}";
    }

    public HistoriaDuenioVehiculo() { }

    public HistoriaDuenioVehiculo(Cliente aCliente, DateTime aFechaCompra) {
      Cliente = aCliente;
      FechaCompra = aFechaCompra;
    }
  }
}