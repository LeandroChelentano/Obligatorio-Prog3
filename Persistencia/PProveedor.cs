using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PProveedor : Conn {
    public static List<Proveedor> getProveedores() {
      List<Proveedor> proveedores = new List<Proveedor>();

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetProveedores", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Proveedor proveedor = new Proveedor();

            proveedor.Id = short.Parse(reader["Id"].ToString());
            proveedor.Nombre = reader["Nombre"].ToString();
            proveedor.Direccion = reader["Direccion"].ToString();
            proveedor.Telefono = reader["Telefono"].ToString();

            proveedores.Add(proveedor);
          }
        }
      } catch {
        throw;
      }

      return proveedores;
    }

    public static bool addProveedor(Proveedor aProveedor) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("addProveedor", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aProveedor.Id));
        cmd.Parameters.Add(new SqlParameter("@Nombre", aProveedor.Nombre));
        cmd.Parameters.Add(new SqlParameter("@Direccion", aProveedor.Direccion));
        cmd.Parameters.Add(new SqlParameter("@Telefono", aProveedor.Telefono));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool removeProveedor(Proveedor aProveedor) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("removeProveedor", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aProveedor.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool modifyProveedor(Proveedor aProveedor) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("modifyProveedor", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aProveedor.Id));
        cmd.Parameters.Add(new SqlParameter("@Nombre", aProveedor.Nombre));
        cmd.Parameters.Add(new SqlParameter("@Direccion", aProveedor.Direccion));
        cmd.Parameters.Add(new SqlParameter("@Telefono", aProveedor.Telefono));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }
  }
}
