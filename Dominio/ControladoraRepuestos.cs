using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Domain.domain {
  public class ControladoraRepuestos {
    private static List<Repuesto> listaRepuestos = new List<Repuesto>();
    public static List<Repuesto> ListaRepuestos {
      get { return listaRepuestos; }
      set { listaRepuestos = value; }
    }

    public Repuesto BuscarRepuestoPorCodigo(string aCodigo) {
      foreach (Repuesto repuesto in ListaRepuestos)
        if (repuesto.Codigo == aCodigo)
          return repuesto;

      return null;
    }
    public bool NuevoRepuesto(Repuesto aRepuesto) {
      foreach (Repuesto repuesto in ListaRepuestos)
        if (repuesto.Codigo == aRepuesto.Codigo)
          return false;

      if (PRepuesto.addRepuesto(aRepuesto)) {
        ListaRepuestos.Add(aRepuesto);
        return true;
      }
      return false;
    }

    public bool EliminarRepuesto(Repuesto aRepuesto) {
      if (PRepuesto.removeRepuesto(aRepuesto)) {
        ListaRepuestos.Remove(aRepuesto);
        return true;
      }
      return false;
    }

    public bool ModificarRepuesto(Repuesto aRepuesto) {
      if (PRepuesto.modifyRepuesto(aRepuesto)) {
        foreach (Repuesto repuesto in ListaRepuestos)
          if (repuesto.Codigo == aRepuesto.Codigo) {
            repuesto.Descripcion = aRepuesto.Descripcion;
            repuesto.Costo = aRepuesto.Costo;
            repuesto.Proveedor = aRepuesto.Proveedor;
            repuesto.Tipo = aRepuesto.Tipo;
            return true;
          }
        return true;
      }
      return false;
    }

    public bool nuevaReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto, int aCantidad) {
      if (aReparacion == null || aRepuesto == null || aCantidad < 1)
        return false;

      if (!PReparacion.nuevaReparacionRepuesto(aReparacion, aRepuesto, aCantidad))
        return false;

      aReparacion.RepuestosUsados.Add(new RepuestoCantidad(aRepuesto, aCantidad));
      return true;
    }

    public bool actualizarReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto, int aCantidad) {
      if (aReparacion == null || aRepuesto == null || aCantidad < 1)
        return false;

      if (!PReparacion.actualizarReparacionRepuesto(aReparacion, aRepuesto, aCantidad))
        return false;

      foreach (RepuestoCantidad RC in aReparacion.RepuestosUsados)
        if (RC.Repuesto.Codigo == aRepuesto.Codigo)
          RC.Cantidad = RC.Cantidad = aCantidad;

      return true;
    }

    public bool eliminarReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto) {
      if (aReparacion == null || aRepuesto == null)
        return false;

      if (!PReparacion.eliminarReparacionRepuesto(aReparacion, aRepuesto))
        return false;

      RepuestoCantidad rc = null;
      foreach (RepuestoCantidad RC in aReparacion.RepuestosUsados)
        if (RC.Repuesto.Codigo == aRepuesto.Codigo)
          rc = RC;
          
      if (rc != null)
        aReparacion.RepuestosUsados.Remove(rc);

      return true;
    }
  }
}