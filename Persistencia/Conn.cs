using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia {
  public class Conn {
    private static string cadena = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CarTreatmentCenter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

    public static string Cadena {
      get { return cadena; }
    }
  }
}
