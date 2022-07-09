using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PReserva : Conn {
    public static List<Reserva> getReservas() {
      List<Reserva> reservas = new List<Reserva>();
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetReservas", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Reserva reserva = new Reserva();

            reserva.Id = short.Parse(reader["Id"].ToString());
            reserva.Fecha = reader.GetDateTime(1);
            reserva.Estado = char.Parse(reader["Estado"].ToString());

            reserva.Vehiculo = new Vehiculo();
            reserva.Vehiculo.Id = short.Parse(reader["Vehiculo"].ToString());

            reserva.Cliente = new Cliente();
            reserva.Cliente.Id = short.Parse(reader["Cliente"].ToString());

            reservas.Add(reserva);
          }
        }
      } catch {
        throw;
      }
      return reservas;
    }

    public static bool addReserva(Reserva aReserva, Cliente aCliente) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("AddReserva", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Id", aReserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Fecha", aReserva.Fecha));
        cmd.Parameters.Add(new SqlParameter("@Estado", aReserva.Estado));
        cmd.Parameters.Add(new SqlParameter("@Vehiculo", aReserva.Vehiculo.Id));
        cmd.Parameters.Add(new SqlParameter("@Cliente", aCliente.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool changeStateReserva(Reserva aReserva, char newState) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("ChangeState", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Reserva", aReserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Estado", newState));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        throw;
        return false;
      }
    }
  }
}
