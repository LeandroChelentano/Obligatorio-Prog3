using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PTipo : Conn {
    public static List<RepuestoTipo> getTipos() {
      List<RepuestoTipo> tipos = new List<RepuestoTipo>();
      
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetTipos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            RepuestoTipo tipo = new RepuestoTipo();
            tipo.Id = char.Parse(reader["Id"].ToString());
            tipo.Nombre = reader["Nombre"].ToString();
            tipos.Add(tipo);
          }
        }
      } catch {
        throw;
      }

      return tipos;
    }
  }
}
