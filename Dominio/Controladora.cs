using System;
using System.Collections.Generic;
using Modelo;
using Persistencia;

namespace Domain.domain {
  public class Controladora {
    private static List<Reserva> ListaReservas = new List<Reserva>();
    private static List<Cliente> ListaClientes = null;
    private static List<Marca> ListaMarcas = new List<Marca>();
    private static List<RepuestoTipo> ListaTipos = new List<RepuestoTipo>();

    #region Lists Gets
    public List<Reserva> GetReservas() {
      return ListaReservas;
    }
    public List<Cliente> GetClientes() {
      return ListaClientes;
    }
    public List<Marca> GetMarca() {
      return ListaMarcas;
    }
    public List<RepuestoTipo> GetTipos() {
      return ListaTipos;
    }
    #endregion

    public Cliente BuscarCliente(short aId) {
      foreach (Cliente cliente in ListaClientes)
        if (cliente.Id == aId)
          return cliente;

      return null;
    }
    public Cliente BuscarClientePorEmail(string aEmail) {
      foreach (Cliente cliente in ListaClientes)
        if (cliente.Email == aEmail)
          return cliente;

      return null;
    }
    public RepuestoTipo BuscarTipoPorCodigo(char aId) {
      foreach (RepuestoTipo rt in ListaTipos)
        if (rt.Id == aId)
          return rt;

      return null;
    }
    public Marca BuscarMarca(short aId) {
      foreach (Marca marca in ListaMarcas)
        if (marca.Id == aId)
          return marca;

      return null;
    }
    public Reserva BuscarReserva(short aId) {
      foreach (Reserva reserva in ListaReservas)
        if (reserva.Id == aId)
          return reserva;

      return null;
    }
    public Cliente ValidarInicioDeSesion(string aEmail, string aPass) {
      foreach (Cliente cliente in ListaClientes)
        if (cliente.Email == aEmail && cliente.Pass == aPass) {
          cliente.GenerateToken();
          return cliente;
        }

      return null;
    }
    public Cliente Registrarse(Cliente pCliente) {
      Cliente unCliente = BuscarCliente(pCliente.Id);
      if (unCliente != null)
        return null;

      unCliente = BuscarClientePorEmail(pCliente.Email);
      if (unCliente != null)
        return null;

      if (PCliente.addCliente(pCliente)) {
        ListaClientes.Add(pCliente);
        pCliente.GenerateToken();
        return pCliente;
      }
      return null;
    }
    public Cliente ValidarToken(string aToken) {
      foreach (Cliente cliente in ListaClientes)
        if (cliente.Token == aToken)
          return cliente;

      return null;
    }
    public bool NuevaReserva(Reserva aReserva, Cliente aCliente) {
      if (aReserva == null || aCliente == null)
        return false;

      if (!PReserva.addReserva(aReserva, aCliente))
        return false;

      ListaReservas.Add(aReserva);
      return true;
    }
    public bool CambiarEstadoReserva(Reserva aReserva, char aNuevoEstado) {
      if (aNuevoEstado != 'x' && aNuevoEstado != 'r' && aNuevoEstado != 'c')
        return false;

      if (!PReserva.changeStateReserva(aReserva, aNuevoEstado))
        return false;

      if (aNuevoEstado == 'x')
        aReserva.Cancelar();
      if (aNuevoEstado == 'r')
        aReserva.Confirmar();
      if (aNuevoEstado == 'c')
        aReserva.Completar();

      if (aNuevoEstado == 'r') {
        Reparacion reparacion = new Reparacion();
        reparacion.Reserva = aReserva;
        aReserva.Vehiculo.HistorialReparaciones.Add(reparacion);
      }

      return true;
    }
    public bool PromoverCliente(Cliente sC, string aToken) {
      Cliente currentClient = ValidarToken(aToken);

      if (currentClient == null)
        return false;

      if (currentClient is Administrador) {
        if (PCliente.promoteCliente(sC)) {
          Cliente newClient = new Administrador(sC.Id, sC.Nombre, sC.Ci, sC.Telefono, sC.Email, sC.Pass, sC.FechaRegistro, false);

          ListaClientes.Remove(sC);
          ListaClientes.Add(newClient);

          newClient.GenerateToken();
          newClient.VehiculosEnPropiedad = new List<Vehiculo>(sC.VehiculosEnPropiedad);

          return true;
        }
        return false;
      }
      return false;
    }
    public bool EliminarAdministrador(Cliente aCliente, string aToken) {
      Cliente currentClient = ValidarToken(aToken);

      if (currentClient == null)
        return false;

      if (currentClient is Administrador self && self.EsOwner) {
        Cliente selectedClient = BuscarCliente(aCliente.Id);
        if (selectedClient is Administrador sA) {
          if (sA.EsOwner)
            return false;

          if (PCliente.degradeCliente(selectedClient)) {
            Cliente newClient = new Cliente(sA.Id, sA.Nombre, sA.Ci, sA.Telefono, sA.Email, sA.Pass, sA.FechaRegistro);

            ListaClientes.Remove(selectedClient);
            ListaClientes.Add(newClient);

            newClient.GenerateToken();
            newClient.VehiculosEnPropiedad = new List<Vehiculo>(selectedClient.VehiculosEnPropiedad);

            return true;
          }
          return false;
        }
        return false;
      }
      return false;
    }
    public static short getNextIdForClient() {
      short newId = 0;
      foreach (Cliente c in ListaClientes)
        if (c.Id > newId)
          newId = c.Id;

      return short.Parse($"{newId + 1}");
    }
    public static short getNewIdForVehicle() {
      short id = 0;
      foreach (Cliente cliente in ListaClientes)
        foreach (Vehiculo vehiculo in cliente.VehiculosEnPropiedad)
          if (vehiculo.Id > id)
            id = vehiculo.Id;

      return short.Parse($"{id + 1}");
    }
    public bool NuevoVehiculo(Cliente aCliente, Vehiculo aVehiculo, DateTime aFecha) {
      if (aCliente == null || aVehiculo == null)
        return false;

      if (PVehiculo.addVehiculo(aVehiculo, aCliente, aFecha)) {
        aCliente.AgregarVehiculo(aVehiculo);
        aVehiculo.HistorialDuenios.Add(new HistoriaDuenioVehiculo(aCliente, aFecha));
        return true;
      }
      return false;
    }
    public bool ModificarVehiculo(Cliente aCliente, Vehiculo aVehiculo) {
      if (aCliente == null || aVehiculo == null)
        return false;

      if (PVehiculo.modifyVehiculo(aVehiculo)) {
        foreach (Vehiculo v in aCliente.VehiculosEnPropiedad)
          if (v.Id == aVehiculo.Id) {
            v.Matricula = aVehiculo.Matricula;
            v.Anio = aVehiculo.Anio;
            v.Marca = aVehiculo.Marca;
            v.Modelo = aVehiculo.Modelo;
            v.Color = aVehiculo.Color;
          }
        return true;
      }
      return false;
    }
    public bool NuevaMarca(Marca aMarca) {
      if (aMarca == null)
        return false;

      if (!PMarca.addMarca(aMarca))
        return false;

      ListaMarcas.Add(aMarca);
      return true;
    }
    public bool EliminarMarca(Marca aMarca) {
      if (aMarca == null)
        return false;

      if (!PMarca.removeMarca(aMarca))
        return false;

      ListaMarcas.Remove(aMarca);
      return true;
    }
    public bool ModificarMarca(Marca aMarca) {
      if (aMarca == null)
        return false;

      if (!PMarca.modifyMarca(aMarca))
        return false;

      Marca marca = BuscarMarca(aMarca.Id);
      marca.Nombre = aMarca.Nombre;
      return true;
    }
    public bool ActualizarReparacion(Reparacion aReparacion) {
      if (aReparacion == null)
        return false;

      if (!PReparacion.modifyReparacion(aReparacion))
        return false;

      foreach (Vehiculo vehiculo in aReparacion.Reserva.Cliente.VehiculosEnPropiedad)
        foreach (Reparacion reparacion in vehiculo.HistorialReparaciones)
          if (reparacion.Reserva.Id == aReparacion.Reserva.Id) {
            reparacion.Costo = aReparacion.Costo;
            reparacion.DescEntrada = aReparacion.DescEntrada;
            reparacion.DescSalida = aReparacion.DescSalida;
            reparacion.KmsEntrada = aReparacion.KmsEntrada;
            reparacion.Mecanico = aReparacion.Mecanico;
          }

      return true;
    }
    public List<Reparacion> GetReparacionesFiltro(DateTime aFrom, DateTime aTo) {
      List<Reparacion> LR = PReparacion.getReparacionesFiltro(aFrom, aTo);

      foreach (Reparacion reparacion in LR) {
        Reserva reserva = BuscarReserva(reparacion.Reserva.Id);
        reparacion.Reserva = reserva;
      }

      return LR;
    }
    public List<RepuestoCantidad> GetRuepuestoEstadistica() {
      return PRepuesto.getRuepuestoEstadistica();
    }
    public bool FinalizarReparacion(Reparacion aReparacion) {
      if (aReparacion == null)
        return false;

      DateTime date = DateTime.Now;
      if (!PReparacion.FinalizarReparacion(aReparacion, date))
        return false;

      aReparacion.Finalizada = date;

      CambiarEstadoReserva(aReparacion.Reserva, 'c');

      return true;
    }
    public bool EliminarCliente(Cliente aCliente) {
      if (aCliente == null)
        return false;

      if (!PCliente.eliminarCliente(aCliente))
        return false;

      ListaClientes.Remove(aCliente);
      return true;
    }
    public bool CambiarContrasenia(Cliente aCliente, string aCurrentPass, string aNewPass) {
      if (aCliente == null || aNewPass == "")
        return false;

      if (aCurrentPass != aCliente.Pass)
        return false;

      if (!PCliente.cambiarContrasenia(aCliente, aNewPass))
        return false;

      aCliente.Pass = aNewPass;
      return true;
    }
    public void init() {
      //si es null nunca se consultó a la base de datos
      if (ListaClientes == null) {
        //creamos la lista efectivamente
        ListaClientes = new List<Cliente>();

        ControladoraMecanicos CM = new ControladoraMecanicos();
        ControladoraProveedores CP = new ControladoraProveedores();
        ControladoraRepuestos CR = new ControladoraRepuestos();

        List<Cliente> Dclientes = PCliente.getClientes();
        ListaClientes = new List<Cliente>(Dclientes);

        List<Marca> Dmarcas = PMarca.getMarcas();
        ListaMarcas = new List<Marca>(Dmarcas);

        List<RepuestoTipo> Dtipos = PTipo.getTipos();
        ListaTipos = new List<RepuestoTipo>(Dtipos);

        List<Reserva> Dreservas = PReserva.getReservas();
        foreach (Reserva reserva in Dreservas) {
          Cliente cliente = BuscarCliente(reserva.Cliente.Id);
          reserva.Cliente = cliente;
        }
        ListaReservas = new List<Reserva>(Dreservas);

        List<Mecanico> Dmecanicos = PMecanico.getMecanicos();
        ControladoraMecanicos.ListaMecanicos = new List<Mecanico>(Dmecanicos);

        List<Proveedor> Dproveedores = PProveedor.getProveedores();
        ControladoraProveedores.ListaProveedores = new List<Proveedor>(Dproveedores);

        List<Repuesto> Drepuestos = PRepuesto.getRepuestos();
        foreach (Repuesto repuesto in Drepuestos) {
          RepuestoTipo repuestoTipo = BuscarTipoPorCodigo(repuesto.Tipo.Id);
          repuesto.Tipo = repuestoTipo;

          Proveedor proveedor = CP.BuscarProveedor(repuesto.Proveedor.Id);
          repuesto.Proveedor = proveedor;
        }
        ControladoraRepuestos.ListaRepuestos = new List<Repuesto>(Drepuestos);

        List<Vehiculo> Dvehiculos = PVehiculo.getVehiculos();
        foreach (Vehiculo v in Dvehiculos) {
          Marca marca = BuscarMarca(v.Marca.Id);
          v.Marca = marca;

          foreach (HistoriaDuenioVehiculo HDV in v.HistorialDuenios) {
            Cliente cliente = BuscarCliente(HDV.Cliente.Id);
            HDV.Cliente = cliente;
          }

          foreach (Reparacion reparacion in v.HistorialReparaciones) {
            if (reparacion.Mecanico != null) {
              Mecanico mecanico = CM.BuscarMecanico(reparacion.Mecanico.Codigo);
              reparacion.Mecanico = mecanico;
            }

            Reserva reserva = BuscarReserva(reparacion.Reserva.Id);
            reparacion.Reserva = reserva;


            foreach (RepuestoCantidad RC in reparacion.RepuestosUsados) {
              Repuesto repuesto = CR.BuscarRepuestoPorCodigo(RC.Repuesto.Codigo);
              RC.Repuesto = repuesto;
            }
          }

          Cliente propietario = v.ObtenerUltimoPropietario();
          if (propietario != null)
            propietario.VehiculosEnPropiedad.Add(v);
        }

        foreach (Reserva reserva in Dreservas)
          foreach (Cliente cliente in ListaClientes) 
            foreach (Vehiculo vehiculo in cliente.VehiculosEnPropiedad)
              if (reserva.Vehiculo.Id == vehiculo.Id)
                reserva.Vehiculo = vehiculo;
      }
    }
  }
}
