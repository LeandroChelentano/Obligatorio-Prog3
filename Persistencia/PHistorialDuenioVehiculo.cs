using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PHistorialDuenioVehiculo : Conn {
    public static List<HistoriaDuenioVehiculo> getHistorialDuenioVehiculos() {
      List<HistoriaDuenioVehiculo> HDVs = new List<HistoriaDuenioVehiculo>();

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("getClientesVehiculos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            HistoriaDuenioVehiculo HDV = new HistoriaDuenioVehiculo();
            Cliente cli = new Cliente();
            cli.Id = short.Parse(reader["Cliente"].ToString());


            HDV.Cliente = cli;
            HDV.FechaCompra = reader.GetDateTime(2);
          }
        }
      } catch {
        throw;
      }

      return HDVs;
    }
  }
}
