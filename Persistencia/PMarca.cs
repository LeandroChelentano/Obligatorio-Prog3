using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PMarca : Conn {
    public static List<Marca> getMarcas() {
      List<Marca> marcas = new List<Marca>();
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("getMarcas", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Marca marca = new Marca();
            marca.Id = short.Parse(reader["ID"].ToString());
            marca.Nombre = reader["Nombre"].ToString();
            marcas.Add(marca);
          }
        }
      } catch {
        throw;
      }
      return marcas;
    }

    public static bool addMarca(Marca aMarca) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("addMarca", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ID", aMarca.Id));
        cmd.Parameters.Add(new SqlParameter("@Nombre", aMarca.Nombre));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }

    public static bool removeMarca(Marca aMarca) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("removeMarca", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ID", aMarca.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }

    public static bool modifyMarca(Marca aMarca) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("modifyMarca", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ID", aMarca.Id));
        cmd.Parameters.Add(new SqlParameter("@Nombre", aMarca.Nombre));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }
  }
}
