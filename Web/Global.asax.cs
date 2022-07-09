using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web {
  public class Global : HttpApplication {
    void Application_Start(object sender, EventArgs e) {
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      // Public
      RouteTable.Routes.MapPageRoute("Home", "Index", "~/Default.aspx");
      RouteTable.Routes.MapPageRoute("Login", "Login", "~/Pages/Login.aspx");
      RouteTable.Routes.MapPageRoute("Register", "Register", "~/Pages/Register.aspx");

      // Admin side
      RouteTable.Routes.MapPageRoute("Admin", "Admin", "~/Pages/Admin/Home.aspx");
      RouteTable.Routes.MapPageRoute("Repuestos", "Admin/Repuestos", "~/Pages/Admin/Repuestos.aspx");
      RouteTable.Routes.MapPageRoute("Personas", "Admin/Personas", "~/Pages/Admin/Personas.aspx");
      RouteTable.Routes.MapPageRoute("AdminReparaciones", "Admin/Reparaciones", "~/Pages/Admin/Reparaciones.aspx");
      RouteTable.Routes.MapPageRoute("ReparacionEspecifica", "Admin/Reparacion", "~/Pages/Admin/SpecificReparation.aspx");
      RouteTable.Routes.MapPageRoute("Marcas", "Admin/Marcas", "~/Pages/Admin/Marcas.aspx");
      RouteTable.Routes.MapPageRoute("Proveedores", "Admin/Proveedores", "~/Pages/Admin/Proveedores.aspx");
      RouteTable.Routes.MapPageRoute("Mecanicos", "Admin/Mecanicos", "~/Pages/Admin/Mecanicos.aspx");
      RouteTable.Routes.MapPageRoute("Estadisticas", "Admin/Estadisticas", "~/Pages/Admin/Estadisticas.aspx");

      // User side
      RouteTable.Routes.MapPageRoute("Vehiculos", "Usuario/Vehiculos", "~/Pages/Client/Vehicles.aspx");
      RouteTable.Routes.MapPageRoute("Reparaciones", "Usuario/Reparaciones", "~/Pages/Client/Reparaciones.aspx");

    }
  }
}