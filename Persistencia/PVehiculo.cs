using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia {
  public class PVehiculo : Conn {
    public static List<Vehiculo> getVehiculos() {
      List<Vehiculo> vehiculos = new List<Vehiculo>();
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetVehiculos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.Id = short.Parse(reader["VehiculoCod"].ToString());
            vehiculo.Matricula = reader["Matricula"].ToString();
            vehiculo.Anio = short.Parse(reader["Anio"].ToString());
            vehiculo.Color = reader["Color"].ToString();
            vehiculo.Marca = new Marca(short.Parse(reader["Marca"].ToString()), "");
            vehiculo.Modelo = reader["Modelo"].ToString();
            vehiculos.Add(vehiculo);
          }
        }
      } catch {
        throw;
      }

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetClientesVehiculos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            HistoriaDuenioVehiculo HDV = new HistoriaDuenioVehiculo();
            Cliente cliente = new Cliente();
            short cli = short.Parse(reader["Cliente"].ToString());
            cliente.Id = cli;
            short veh = short.Parse(reader["Vehiculo"].ToString());

            HDV.Cliente = cliente;
            HDV.FechaCompra = reader.GetDateTime(2);
            
            foreach (Vehiculo v in vehiculos)
              if (v.Id == veh)
                v.HistorialDuenios.Add(HDV);
          }
        }
      } catch {
        throw;
      }

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetVehiculoReparaciones", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            Reparacion reparacion = new Reparacion();

            Reserva reserva = new Reserva();
            reserva.Id = short.Parse(reader["RepCod"].ToString());

            string aux = reader["Mecanico"].ToString();
            Mecanico mecanico = aux == "" ? null : new Mecanico();
            if (mecanico != null)
              mecanico.Codigo = short.Parse(reader["Mecanico"].ToString());

            reparacion.Reserva = reserva;
            reparacion.Mecanico = mecanico;
            reparacion.RepuestosUsados = new List<RepuestoCantidad>();
            reparacion.Costo = double.Parse(reader["Costo"].ToString());
            reparacion.Finalizada = reader["FchSalida"].ToString() == "" ? DateTime.MinValue : reader.GetDateTime(2);
            reparacion.DescEntrada = reader["RepDescEntrada"].ToString();
            reparacion.DescSalida = reader["RepDescSalida"].ToString();
            string kms = reader["KmsEntrada"].ToString();
            reparacion.KmsEntrada = kms == "" ? 0 : int.Parse(kms);

            foreach (Vehiculo v in vehiculos)
              if (v.Id == short.Parse(reader["Vehiculo"].ToString()))
                v.HistorialReparaciones.Add(reparacion);
          }
        }
      } catch {
        throw;
      }

      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("GetReparacionRepuestos", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.ExecuteNonQuery();

        using (SqlDataReader reader = cmd.ExecuteReader()) {
          while (reader.Read()) {
            RepuestoCantidad repuestoCantidad = new RepuestoCantidad();

            repuestoCantidad.Repuesto = new Repuesto();
            repuestoCantidad.Repuesto.Codigo = reader["Repuesto"].ToString();
            repuestoCantidad.Cantidad = int.Parse(reader["Cantidad"].ToString());

            int ResID = short.Parse(reader["Reparacion"].ToString());
            foreach (Vehiculo vehiculo in vehiculos) {
              foreach (Reparacion reparacion in vehiculo.HistorialReparaciones) {
                if (reparacion.Reserva.Id == ResID) {
                  reparacion.RepuestosUsados.Add(repuestoCantidad);
                }
              }
            }
          }
        }
      } catch {
        throw;
      }

      return vehiculos;
    }

    public static bool addVehiculo(Vehiculo aVehiculo, Cliente aCliente, DateTime aFecha) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("AddVehiculo", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@VehiculoCod", aVehiculo.Id));
        cmd.Parameters.Add(new SqlParameter("@Matricula", aVehiculo.Matricula));
        cmd.Parameters.Add(new SqlParameter("@Anio", aVehiculo.Anio));
        cmd.Parameters.Add(new SqlParameter("@Color", aVehiculo.Color));
        cmd.Parameters.Add(new SqlParameter("@Cliente", aCliente.Id));
        cmd.Parameters.Add(new SqlParameter("@Modelo", aVehiculo.Modelo));
        cmd.Parameters.Add(new SqlParameter("@Marca", aVehiculo.Marca.Id));
        cmd.Parameters.Add(new SqlParameter("@Fecha", aFecha));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        throw;
        return false;
      }
    }

    public static bool modifyVehiculo(Vehiculo aVehiculo) {
      try {
        var connection = new SqlConnection(Cadena);
        connection.Open();

        SqlCommand cmd = new SqlCommand("ModifyVehiculo", connection);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@VehiculoCod", aVehiculo.Id));
        cmd.Parameters.Add(new SqlParameter("@Matricula", aVehiculo.Matricula));
        cmd.Parameters.Add(new SqlParameter("@Anio", aVehiculo.Anio));
        cmd.Parameters.Add(new SqlParameter("@Color", aVehiculo.Color));
        cmd.Parameters.Add(new SqlParameter("@Modelo", aVehiculo.Modelo));
        cmd.Parameters.Add(new SqlParameter("@Marca", aVehiculo.Marca.Id));

        int rowsAffected = cmd.ExecuteNonQuery();

        return rowsAffected > 0;
      } catch {
        //throw;
        return false;
      }
    }
  }
}
