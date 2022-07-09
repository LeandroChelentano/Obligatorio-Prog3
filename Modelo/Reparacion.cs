using System;
using System.Collections.Generic;

namespace Modelo {
  public class Reparacion {
    private Reserva reserva;
    private Mecanico mecanico = null;
    private List<RepuestoCantidad> repuestosUsados;
    private double costo;
    private DateTime finalizada;
    private string descEntrada;
    private string descSalida;
    private int kmsEntrada = 0;

    #region Getters-Setters
    public Reserva Reserva {
      get { return reserva; }
      set { reserva = value; }
    }
    public Mecanico Mecanico {
      get { return mecanico; }
      set { mecanico = value; }
    }
    public List<RepuestoCantidad> RepuestosUsados {
      get { return repuestosUsados; }
      set { repuestosUsados = value; }
    }
    public double Costo {
      get { return costo; }
      set { costo = value; }
    }
    public DateTime Finalizada {
      get { return finalizada; }
      set { finalizada = value; }
    }
    public string DescEntrada {
      get { return descEntrada; }
      set { descEntrada = value; }
    }
    public string DescSalida {
      get { return descSalida; }
      set { descSalida = value; }
    }
    public int KmsEntrada {
      get { return kmsEntrada; }
      set { kmsEntrada = value; }
    }
    #endregion

    public override string ToString() {
      return $"{reserva}, {mecanico}, {costo}, {finalizada}, {mecanico}";
    }
    public string Stat() {
      return $"[{reserva.Fecha.ToShortDateString()}] {reserva.Cliente.Nombre} - {reserva.Vehiculo.Matricula} - ${costo}";
    }

    public Reparacion(Reserva aReserva) {
      Reserva = aReserva;
    }

    public Reparacion() { }
  }
}