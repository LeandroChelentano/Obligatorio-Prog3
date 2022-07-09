using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PMecanico : Conn {
    public static List<Mecanico> getMecanicos() {
      List<Mecanico> mecanicos = new List<Mecanico>();

      var connection = new SqlConnection(Cadena);
      connection.Open();

      SqlCommand cmd = new SqlCommand("GetMecanicos", connection);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.ExecuteNonQuery();

      using (SqlDataReader reader = cmd.ExecuteReader()) {
        while (reader.Read()) {
          Mecanico mecanico = new Mecanico();

          mecanico.Codigo = short.Parse(reader["MecCod"].ToString());
          mecanico.Nombre = reader["MecNom"].ToString();
          mecanico.Ci = reader["MecCi"].ToString();
          mecanico.Telefono = reader["MecTel"].ToString();
          mecanico.FechaIngreso = reader.GetDateTime(4);
          mecanico.Activo = reader["MecActivo"].ToString() == "1" ? true : false;

          mecanicos.Add(mecanico);
        }
      }

      return mecanicos;
    }
    public static bool addMecanico(Mecanico aMecanico) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("addMecanico", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@MecCod", aMecanico.Codigo));
        cmd.Parameters.Add(new SqlParameter("@MecNom", aMecanico.Nombre.ToString()));
        cmd.Parameters.Add(new SqlParameter("@MecCi", aMecanico.Ci.ToString()));
        cmd.Parameters.Add(new SqlParameter("@MecTel", aMecanico.Telefono.ToString()));
        cmd.Parameters.Add(new SqlParameter("@MecFchIng", aMecanico.FechaIngreso));
        cmd.Parameters.Add(new SqlParameter("@MecActivo", aMecanico.Activo));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }

    public static bool removeMecanico(Mecanico aMecanico) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("removeMecanico", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@MecCod", aMecanico.Codigo));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }

    public static bool modifyMecanico(Mecanico aMecanico) {
      try {
        SqlConnection connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("modifyMecanico", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@MecCod", aMecanico.Codigo));
        cmd.Parameters.Add(new SqlParameter("@MecNom", aMecanico.Nombre));
        cmd.Parameters.Add(new SqlParameter("@MecCi", aMecanico.Ci));
        cmd.Parameters.Add(new SqlParameter("@MecTel", aMecanico.Telefono));
        cmd.Parameters.Add(new SqlParameter("@MecActivo", aMecanico.Activo));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;

      } catch {
        throw;
      }
    }
  }
}
