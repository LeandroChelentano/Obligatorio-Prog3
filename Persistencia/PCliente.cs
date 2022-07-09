using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PCliente : Conn {
    public static List<Cliente> getClientes() {
      List<Cliente> clientes = new List<Cliente>();
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetClientes", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            if (reader["EsAdmin"].ToString() == "1") {
              Administrador administrador = new Administrador();
              administrador.Id = short.Parse(reader["cliCod"].ToString());
              administrador.Nombre = reader["cliNom"].ToString();
              administrador.Ci = reader["cliCI"].ToString();
              administrador.Telefono = reader["cliTel"].ToString();
              administrador.Email = reader["cliEmail"].ToString();
              administrador.Pass = reader["cliPass"].ToString();
              administrador.FechaRegistro = reader.GetDateTime(6);
              administrador.Pass = reader["cliPass"].ToString();
              administrador.EsOwner = reader["EsOwner"].ToString() == "1" ? true : false;
              clientes.Add(administrador);
            } else {
              Cliente cliente = new Cliente();
              cliente.Id = short.Parse(reader["cliCod"].ToString());
              cliente.Nombre = reader["cliNom"].ToString();
              cliente.Ci = reader["cliCI"].ToString();
              cliente.Telefono = reader["cliTel"].ToString();
              cliente.Email = reader["cliEmail"].ToString();
              cliente.Pass = reader["cliPass"].ToString();
              cliente.FechaRegistro = reader.GetDateTime(6);
              clientes.Add(cliente);
            }
          }
        }
      } catch {
        throw;
      }

      //falta el tema de la lista de los vehiculos en propiedad;
      return clientes;
    }

    public static bool addCliente(Cliente aCliente) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("AddCliente", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aCliente.Id));
        cmd.Parameters.Add(new SqlParameter("@Nombre", aCliente.Nombre));
        cmd.Parameters.Add(new SqlParameter("@Ci", aCliente.Ci));
        cmd.Parameters.Add(new SqlParameter("@Telefono", aCliente.Telefono));
        cmd.Parameters.Add(new SqlParameter("@Email", aCliente.Email));
        cmd.Parameters.Add(new SqlParameter("@Pass", aCliente.Pass));
        cmd.Parameters.Add(new SqlParameter("@FechaRegistro", aCliente.FechaRegistro));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool promoteCliente(Cliente aCliente) {
      if (aCliente == null)
        return false;

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("PromoteCliente", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aCliente.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool degradeCliente(Cliente aCliente) {
      if (aCliente == null)
        return false;

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("DegradeCliente", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aCliente.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool eliminarCliente(Cliente aCliente) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("RemoveCliente", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Cliente", aCliente.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool cambiarContrasenia(Cliente aCliente, string aPass) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("CambiarContrasenia", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Cliente", aCliente.Id));
        cmd.Parameters.Add(new SqlParameter("@Pass", aPass));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }
  }
}
