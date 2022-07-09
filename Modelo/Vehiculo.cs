using System;
using System.Collections.Generic;

namespace Modelo {
  public class Vehiculo {
    private short id;
    private List<HistoriaDuenioVehiculo> historialDuenios = new List<HistoriaDuenioVehiculo>();
    private string matricula;
    private short anio;
    private string color;
    private Marca marca;
    private string modelo;
    private List<Reparacion> historialReparaciones = new List<Reparacion>();

    #region Getters-Setters
    public short Id {
      get { return id; }
      set { id = value; }
    }
    public List<HistoriaDuenioVehiculo> HistorialDuenios {
      get { return historialDuenios; }
      set { historialDuenios = value; }
    }
    public string Matricula {
      get { return matricula; }
      set { matricula = value; }
    }
    public short Anio {
      get { return anio; }
      set { anio = value; }
    }
    public string Color {
      get { return color; }
      set { color = value; }
    }
    public Marca Marca {
      get { return marca; }
      set { marca = value; }
    }
    public string Modelo {
      get { return modelo; }
      set { modelo = value; }
    }
    public List<Reparacion> HistorialReparaciones {
      get { return historialReparaciones; }
      set { historialReparaciones = value; }
    }
    #endregion

    public Cliente ObtenerUltimoPropietario() {
      DateTime mayor = DateTime.MinValue;
      Cliente cliente = null;

      foreach (HistoriaDuenioVehiculo momento in historialDuenios)
        if (momento.FechaCompra > mayor) {
          mayor = momento.FechaCompra;
          cliente = momento.Cliente;
        }

      return cliente;
    }

    public override string ToString() {
      return $"{id}, {matricula}, {anio}, {color}, {marca}, {modelo}";
    }

    public Vehiculo(short aId, string aMatricula, short aAnio, string aColor, Marca aMarca, string aModelo) {
      Id = aId;
      Matricula = aMatricula;
      Anio = aAnio;
      Color = aColor;
      Marca = aMarca;
      Modelo = aModelo;
    }

    public Vehiculo() { }
  }
}