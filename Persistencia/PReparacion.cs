using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PReparacion : Conn {
    public static bool modifyReparacion(Reparacion aReparacion) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("ModificarReparacion", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@RepCod", aReparacion.Reserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Costo", aReparacion.Costo));
        cmd.Parameters.Add(new SqlParameter("@RepDescSalida", aReparacion.DescSalida));
        cmd.Parameters.Add(new SqlParameter("@RepDescEntrada", aReparacion.DescEntrada));
        cmd.Parameters.Add(new SqlParameter("@KmsEntrada", aReparacion.KmsEntrada));
        cmd.Parameters.Add(new SqlParameter("@Mecanico", aReparacion.Mecanico.Codigo.ToString()));
        
        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        throw;
        return false;
      }
    }

    public static List<Reparacion> getReparacionesFiltro(DateTime aFrom, DateTime aTo) {
      List<Reparacion> ListaReparaciones = new List<Reparacion>();
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetReparacionesFiltro", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@From", aFrom));
        cmd.Parameters.Add(new SqlParameter("@To", aTo));
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Reparacion reparacion = new Reparacion();

            Reserva reserva = new Reserva();
            reserva.Id = short.Parse(reader["Reserva"].ToString());
            reparacion.Reserva = reserva;

            reparacion.Costo = double.Parse(reader["Costo"].ToString());

            //lo demas no importa

            ListaReparaciones.Add(reparacion);
          }
        }
        return ListaReparaciones;
      } catch {
        throw;
      }
    }

    public static bool nuevaReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto, int aCantidad) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("NuevoReparacionRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Reparacion", aReparacion.Reserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Repuesto", aRepuesto.Codigo));
        cmd.Parameters.Add(new SqlParameter("@Cantidad", aCantidad));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool actualizarReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto, int aCantidad) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("ModificarReparacionRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Reparacion", aReparacion.Reserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Repuesto", aRepuesto.Codigo));
        cmd.Parameters.Add(new SqlParameter("@Cantidad", aCantidad));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        throw;
        return false;
      }
    }

    public static bool eliminarReparacionRepuesto(Reparacion aReparacion, Repuesto aRepuesto) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("EliminarReparacionRepuesto", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Reparacion", aReparacion.Reserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Repuesto", aRepuesto.Codigo));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        return false;
      }
    }

    public static bool FinalizarReparacion(Reparacion aReparacion, DateTime aDate) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("FinalizarReparacion", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Reparacion", aReparacion.Reserva.Id));
        cmd.Parameters.Add(new SqlParameter("@Date", aDate));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        throw;
        return false;
      }
    }
  }
}
