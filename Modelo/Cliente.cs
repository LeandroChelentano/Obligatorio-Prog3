using System;
using System.Collections.Generic;

namespace Modelo {
  public class Cliente {
    private short id;
    private string nombre;
    private string ci;
    private string telefono;
    private string email;
    private string pass;
    private DateTime fechaRegistro;
    private string token;
    private List<Vehiculo> vehiculosEnPropiedad = new List<Vehiculo>();

    #region Getters-Setters
    public short Id {
      get { return id; }
      set { id = value; }
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
    public string Email {
      get { return email; }
      set { email = value; }
    }
    public string Pass {
      get { return pass; }
      set { pass = value; }
    }
    public DateTime FechaRegistro {
      get { return fechaRegistro; }
      set { fechaRegistro = value; }
    }
    public string Token {
      get { return token; }
      set { token = value; }
    }
    public List<Vehiculo> VehiculosEnPropiedad {
      get { return vehiculosEnPropiedad; }
      set { vehiculosEnPropiedad = value; }
    }
    #endregion

    public override string ToString() {
      return $"{id} - {nombre}, {ci}, {telefono}, {email}";
    }

    public Vehiculo BuscarVehiculo(short aId) {
      foreach (Vehiculo vehiculo in vehiculosEnPropiedad)
        if (vehiculo.Id == aId)
          return vehiculo;

      return null;
    }

    public bool VerificarExistencia(Vehiculo aVehiculo) {
      foreach (Vehiculo vehiculo in vehiculosEnPropiedad)
        if (vehiculo.Id == aVehiculo.Id)
          return true;

      return false;
    }

    public bool AgregarVehiculo(Vehiculo aVehiculo) {
      bool existencia = VerificarExistencia(aVehiculo);

      if (existencia)
        return false;

      vehiculosEnPropiedad.Add(aVehiculo);

      if (aVehiculo.HistorialDuenios.Count > 0) {
        Cliente propietarioAnterior = aVehiculo.ObtenerUltimoPropietario();

        if (propietarioAnterior != null)
          propietarioAnterior.VehiculosEnPropiedad.Remove(aVehiculo);
      }

      aVehiculo.HistorialDuenios.Add(new HistoriaDuenioVehiculo(this, DateTime.Now));
      return true;
    }

    public bool QuitarPropiedad(Vehiculo aVehiculo) {
      try {
        VehiculosEnPropiedad.Remove(aVehiculo);
        return true;
      } catch {
        return false;
      }
    }

    public string GenerateToken() {
      char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

      string newToken = "";
      Random rnd = new Random();
      for (int i = 0; i < 12; i++) {
        char letter = alpha[rnd.Next(25)];

        int gonnaParse = rnd.Next(2); // 0 o 1
        newToken = (gonnaParse == 1)
          ? $"{newToken}{Char.ToLower(letter)}"
          : $"{newToken}{letter}";
      }

      Token = newToken;
      return newToken;
    }


    public Cliente(short aId, string aNombre, string aCi, string aTelefono, string aEmail, string aPass, DateTime aFechaRegistro) {
      Id = aId;
      Nombre = aNombre;
      Ci = aCi;
      Telefono = aTelefono;
      Email = aEmail;
      Pass = aPass;
      FechaRegistro = aFechaRegistro;
    }

    public Cliente() { }

  }
}