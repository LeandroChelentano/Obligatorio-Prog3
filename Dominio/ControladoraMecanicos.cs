using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Domain.domain {
  public class ControladoraMecanicos {
    private static List<Mecanico> listaMecanicos = new List<Mecanico>();
    public static List<Mecanico> ListaMecanicos {
      get { return listaMecanicos; }
      set { listaMecanicos = value; }
    }

    public Mecanico BuscarMecanico(short aCodigo) {
      foreach (Mecanico mecanico in ListaMecanicos)
        if (mecanico.Codigo == aCodigo)
          return mecanico;

      return null;
    }

    public bool NuevoMecanico(Mecanico aMecanico) {
      if (aMecanico == null)
        return false;

      if (!PMecanico.addMecanico(aMecanico))
        return false;

      ListaMecanicos.Add(aMecanico);
      return true;
    }
    public bool EliminarMecanico(Mecanico aMecanico) {
      if (aMecanico == null)
        return false;

      if (!PMecanico.removeMecanico(aMecanico))
        return false;

      ListaMecanicos.Remove(aMecanico);
      return true;
    }
    public bool ModificarMecanico(Mecanico aMecanico) {
      if (aMecanico == null)
        return false;

      if (!PMecanico.modifyMecanico(aMecanico))
        return false;

      Mecanico mecanico = BuscarMecanico(aMecanico.Codigo);
      mecanico.Nombre = aMecanico.Nombre;
      mecanico.Ci = aMecanico.Ci;
      mecanico.Telefono = aMecanico.Telefono;
      mecanico.Activo = aMecanico.Activo;
      return true;
    }
  }
}