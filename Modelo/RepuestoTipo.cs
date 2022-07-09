using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo {
  public class RepuestoTipo {
    private char id;
    private string nombre;

    public char Id {
      get { return id; }
      set { id = value; }
    }
    public string Nombre {
      get { return nombre; }
      set { nombre = value; }
    }

    public override string ToString() {
      return $"{Nombre}";
    }

    public RepuestoTipo() { }

    public RepuestoTipo(char aId, string aName) {
      Id = aId;
      Nombre = aName;
    }
  }
}
