using System;
using System.Collections.Generic;

namespace Modelo {
  public class Reserva {
    private short id;
    private DateTime fecha;
    private char estado = 'p';
    private Cliente cliente;
    private Vehiculo vehiculo;

    #region Getters-Setters
    public short Id {
      get { return id; }
      set { id = value; }
    }
    public DateTime Fecha {
      get { return fecha; }
      set { fecha = value; }
    }
    public char Estado {
      get { return estado; }
      set { estado = value; }
    }
    public Cliente Cliente {
      get { return cliente; }
      set { cliente = value; }
    }
    public Vehiculo Vehiculo {
      get { return vehiculo; }
      set { vehiculo = value; }
    }
    #endregion

    public override string ToString() {
      return $"{id} - {fecha} - {cliente.Nombre} ({cliente.Telefono})";
    }

    public string Show() {
      return $"{id} - {fecha.ToShortDateString()} | {vehiculo.Matricula} | {FullState(Estado)}";
    }

    public string FullState(char e) {
      if (e == 'p')
        return "PENDIENTE";
      if (e == 'x')
        return "CANCELADO";
      if (e == 'r')
        return "CONFIRMADO";
      if (e == 'c')
        return "COMPLETADO";
      return "UNDEFINED";
    }

    public void Cancelar() {
      Estado = 'x';
    }

    public void Confirmar() {
      Estado = 'r';
    }

    public void Completar() {
      Estado = 'c';
    }

    public Reserva(short aId, DateTime aFecha, Cliente aCliente, Vehiculo aVehiculo) {
      Id = aId;
      Fecha = aFecha;
      Cliente = aCliente;
      Vehiculo = aVehiculo;
    }

    public Reserva() { }
  }
}