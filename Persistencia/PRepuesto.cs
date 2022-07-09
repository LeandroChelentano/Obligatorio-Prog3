using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PRepuesto : Conn {
    public static List<Repuesto> getRepuestos() {
      List<Repuesto> repuestos = new List<Repuesto>();

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetRepuestos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Repuesto repuesto = new Repuesto();

            repuesto.Codigo = reader["Codigo"].ToString();
            repuesto.Descripcion = reader["Descripcion"].ToString();
            repuesto.Costo = double.Parse(reader["Costo"].ToString());
            
            repuesto.Tipo = new RepuestoTipo();
            repuesto.Tipo.Id = char.Parse(reader["Tipo"].ToString());

            repuesto.Proveedor = new Proveedor();
            repuesto.Proveedor.Id = short.Parse(reader["Proveedor"].ToString());

            repuestos.Add(repuesto);
          }
        }
      } catch {
        throw;
      }

      return repuestos;
    }

    public static bool addRepuesto(Repuesto aRepuesto) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("AddRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Codigo", aRepuesto.Codigo));
        cmd.Parameters.Add(new SqlParameter("@Descripcion", aRepuesto.Descripcion));
        cmd.Parameters.Add(new SqlParameter("@Costo", aRepuesto.Costo));
        cmd.Parameters.Add(new SqlParameter("@Tipo", aRepuesto.Tipo.Id));
        cmd.Parameters.Add(new SqlParameter("@Proveedor", aRepuesto.Proveedor.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool modifyRepuesto(Repuesto aRepuesto) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("ModifyRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Codigo", aRepuesto.Codigo));
        cmd.Parameters.Add(new SqlParameter("@Descripcion", aRepuesto.Descripcion));
        cmd.Parameters.Add(new SqlParameter("@Costo", aRepuesto.Costo));
        cmd.Parameters.Add(new SqlParameter("@Tipo", aRepuesto.Tipo.Id));
        cmd.Parameters.Add(new SqlParameter("@Proveedor", aRepuesto.Proveedor.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool removeRepuesto(Repuesto aRepuesto) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("RemoveRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Codigo", aRepuesto.Codigo));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static List<RepuestoCantidad> getRuepuestoEstadistica() {
      List<RepuestoCantidad> repuestos = new List<RepuestoCantidad>();

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetRepuestosEstadistica", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            RepuestoCantidad RC = new RepuestoCantidad();

            Repuesto repuesto = new Repuesto();
            repuesto.Codigo = reader["Repuesto"].ToString();

            RC.Repuesto = repuesto;
            RC.Cantidad = int.Parse(reader["Cantidad"].ToString());
            

            repuestos.Add(RC);
          }
        }
      } catch {
        throw;
      }

      return repuestos;
    }
  }
}
