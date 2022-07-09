using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
  public class RepuestoCantidad {
    private Repuesto repuesto;
    private int cantidad;

    public Repuesto Repuesto {
      get { return repuesto; }
      set { repuesto = value; }
    }
    public int Cantidad {
      get { return cantidad; }
      set { cantidad = value; }
    }

    public override string ToString() {
      return $"[{cantidad}] {repuesto.Codigo} - {repuesto.Descripcion}";
    }
    public string Stat() {
      return $"[{cantidad}] {repuesto.Codigo}";
    }

    public RepuestoCantidad() { }

    public RepuestoCantidad(Repuesto aRepuesto, int aCantidad) { 
      Repuesto = aRepuesto;
      Cantidad = aCantidad;
    }
  }
}
