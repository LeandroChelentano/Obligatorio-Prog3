create table Clientes(
	CliCod int primary key,
	-- CliTipo char(1) check(CliTipo in ('e', 'p'))
	CliNom varchar(40) not null,
	CliCI varchar(8) unique,
	CliTel varchar(9) not null,
	CliEmail varchar(40) unique,
	CliPass varchar(40) not null,
	FchRegistro DateTime not null,
	EsAdmin numeric(1) check(EsAdmin = 1 or EsAdmin = 0),
	EsOwner numeric(1) check(EsOwner = 1 or EsOwner = 0)
);

create table Marcas(
	ID int primary key,
	Nombre varchar(15)
);

create table Vehiculos(
	VehiculoCod int primary key,
	Matricula varchar(9) unique,
	Anio numeric(4) check(Anio <= year(getdate())) not null,
	Color varchar(15) not null,
	Modelo varchar(15) not null,
	Marca int references Marcas(ID)
);

create table ClienteVehiculo(
	Cliente int references Clientes(CliCod),
	Vehiculo int references Vehiculos(VehiculoCod),
	Fecha DateTime not null,
	primary key (Cliente, Vehiculo)
);

create table Tipos(
  Id char(1) primary key,
  Nombre varchar(25) unique
);

create table Reservas(
  Id int primary key,
  Fecha datetime,
  Estado char(1) check(Estado in ('p','c','d')),
  Vehiculo int references Vehiculos(VehiculoCod),
  Cliente int references Clientes(CliCod),
);

create table Mecanicos(
  MecCod int primary key,
  MecNom varchar(30),
  MecCi varchar(8) unique,
  MecTel varchar(9),
  MecFchIng Datetime,
  MecActivo numeric(1) check(MecActivo in (1,0))
);

create table Reparacion(
  RepCod int primary key,
  Costo money,
  FchSalida DateTime,
  RepDescEntrada varchar(50),
  RepDescSalida varchar(50),
  KmsEntrada int,
  Reserva int references Reservas(Id),
  Mecanico int references Mecanicos(MecCod)
);

create table Proveedores(
  Id int primary key,
  Nombre varchar(25),
  Direccion varchar(40),
  Telefono varchar(9)
);

create table Repuestos(
  Codigo varchar(15) primary key,
  Descripcion varchar(60),
  Costo money,
  Tipo char(1) references Tipos(Id),
  Proveedor int references Proveedores(Id)
);

create table ReparacionRepuestos(
  Reparacion int references Reparacion(RepCod),
  Repuesto varchar(15) references Repuestos(Codigo),
  Cantidad int,
  primary key (Reparacion, Repuesto)
);

-- Procedures



create procedure AddCliente
@Id int,
@Nombre varchar(40),
@Ci varchar(8),
@Telefono varchar(9),
@Email varchar(40),
@Pass varchar(40),
@FechaRegistro datetime
as
begin
insert into Clientes (cliCod, cliNom, cliCI, cliTel, cliEmail, cliPass, FchRegistro)
values (@Id, @Nombre, @Ci, @Telefono, @Email, @Pass, @FechaRegistro)
end

create procedure AddRepuesto
@codigo varchar(15),
@Descripcion varchar(60),
@Costo money,
@Tipo char(1),
@Proveedor int
as
begin
insert into Repuestos (Codigo, Descripcion, Costo, Tipo, Proveedor)
values (@Codigo, @Descripcion, @Costo, @Tipo, @Proveedor)
end

create procedure RemoveRepuesto
@codigo varchar(15)
as
begin
delete from Repuestos where Codigo = @codigo;
end

create procedure ModifyRepuesto
@codigo varchar(15),
@Descripcion varchar(60),
@Costo money,
@Tipo char(1),
@Proveedor int
as
begin
update Repuestos
set Descripcion = @Descripcion, Costo = @Costo, Tipo = @Tipo, Proveedor = @Proveedor
where Codigo = @codigo
end

create procedure AddVehiculo
@VehiculoCod int,
@Matricula varchar(9),
@Anio numeric(4),
@Color varchar(15),
@Modelo varchar(15),
@Cliente int,
@Fecha datetime,
@Marca int
as
begin
insert into Vehiculos (VehiculoCod, Matricula, Anio, Color, Modelo, Marca)
values (@VehiculoCod, @Matricula, @Anio, @Color, @Modelo, @Marca);
insert into ClienteVehiculo (Vehiculo, Cliente, Fecha) 
values (@VehiculoCod, @Cliente, @Fecha);
end

create procedure ModifyVehiculo
@VehiculoCod int,
@Matricula varchar(9),
@Anio numeric(4),
@Color varchar(15),
@Modelo varchar(15),
@Marca int
as
begin
update Vehiculos
set Matricula = @Matricula, Anio = @Anio, Color = @Color, Modelo = @Modelo, Marca = @Marca
where VehiculoCod = @VehiculoCod;
end

create procedure AddReserva
@Id int,
@Fecha datetime,
@Estado char(1),
@Vehiculo int,
@Cliente int
as
begin
insert into Reservas (Id, Fecha, Estado, Vehiculo, Cliente)
values (@Id, @Fecha, @Estado, @Vehiculo, @Cliente)
end

create procedure ChangeState
@Reserva int,
@Estado char(1)
as
begin
update Reservas set Estado = @Estado where Id = @Reserva;
if (@Estado = 'r')
begin
insert into Reparacion (RepCod, Reserva, Costo, RepDescEntrada, RepDescSalida, KmsEntrada)
values (@Reserva, @Reserva, 0, '', '', 0)
end
end

create procedure addReparacion
@RepCod int,
@RepDescEntrada varchar(50),
@Reserva int
as
begin
insert into Reparacion
(RepCod, RepDescEntrada, Reserva)
values (@RepCod, @RepDescEntrada, @Reserva)
end

create procedure addMecanico
@MecCod int,
@MecNom varchar(30),
@MecCi varchar(8),
@MecTel varchar(9),
@MecFchIng Datetime,
@MecActivo numeric(1)
as
begin 
insert into Mecanicos (MecCod, MecNom, MecCi, MecTel, MecFchIng, MecActivo)
values (@MecCod, @MecNom, @MecCi, @MecTel, @MecFchIng, @MecActivo);
end

create procedure removeMecanico
@MecCod int
as
begin 
delete from Mecanicos where MecCod = @MecCod;
end

create procedure modifyMecanico
@MecCod int,
@MecNom varchar(30),
@MecCi varchar(8),
@MecTel varchar(9),
@MecActivo numeric(1)
as
begin 
update Mecanicos set 
MecNom = @MecNom,
MecCi = @MecCi,
MecTel = @MecTel,
MecActivo = @MecActivo
where MecCod = @MecCod;
end

create procedure addProveedor
@Id int,
@Nombre varchar(25),
@Direccion varchar(40),
@Telefono varchar(9)
as
begin 
insert into Proveedores (Id, Nombre, Direccion, Telefono)
values (@Id, @Nombre, @Direccion, @Telefono);
end

create procedure removeProveedor
@Id int
as
begin 
delete from Proveedores where Id = @Id;
end

create procedure modifyProveedor
@Id int,
@Nombre varchar(25),
@Direccion varchar(40),
@Telefono varchar(9)
as
begin 
update Proveedores set 
Nombre = @Nombre,
Direccion = @Direccion,
Telefono = @Telefono
where Id = @Id;
end

create procedure addMecanico
@MecCod int,
@MecNom varchar(30),
@MecCi numeric(8),
@MecTel numeric(9),
@MecFchIng Datetime,
@MecActivo numeric(1)
as
begin 
insert into Mecanicos (MecCod, MecNom, MecCi, MecTel, MecFchIng, MecActivo)
values (@MecCod, @MecNom, @MecCi, @MecTel, @MecFchIng, @MecActivo);
end

create procedure removeMecanico
@MecCod int
as
begin 
delete from Mecanicos where MecCod = @MecCod;
end

create procedure modifyMecanico
@MecCod int,
@MecNom varchar(30),
@MecCi numeric(8),
@MecTel numeric(9),
@MecActivo numeric(1)
as
begin 
update Mecanicos set 
MecNom = @MecNom,
MecCi = @MecCi,
MecTel = @MecTel,
MecActivo = @MecActivo
where MecCod = @MecCod;
end

create procedure ModificarReparacion
@RepCod int,
@Costo money,
@RepDescSalida varchar(50),
@RepDescEntrada varchar(50),
@KmsEntrada int,
@Mecanico int
as
begin
update Reparacion
set
Costo = @Costo,
RepDescEntrada = @RepDescEntrada,
RepDescSalida = @RepDescSalida,
KmsEntrada = @KmsEntrada,
Mecanico = @Mecanico
where RepCod = @RepCod;
end

create procedure NuevoReparacionRepuesto
@Reparacion int,
@Repuesto varchar(15),
@Cantidad int
as
begin
insert into ReparacionRepuestos (Reparacion, Repuesto, Cantidad)
values (@Reparacion, @Repuesto, @Cantidad);
end

create procedure ModificarReparacionRepuesto
@Reparacion int,
@Repuesto varchar(15),
@Cantidad int
as
begin
update ReparacionRepuestos 
set Cantidad = @Cantidad
where Reparacion = @Reparacion
and Repuesto = @Repuesto;
end

create procedure EliminarReparacionRepuesto
@Reparacion int,
@Repuesto varchar(15)
as
begin
delete from ReparacionRepuestos
where Reparacion = @Reparacion
and Repuesto = @Repuesto;
end

create procedure GetReparacionesFiltro
@From date,
@To date
as
begin
select rep.*, res.Fecha 
from Reparacion rep 
inner join Reservas res on res.Id=rep.Reserva
where res.Fecha >= @From
and res.Fecha <= @To;
end

create procedure GetRepuestosEstadistica
as
begin
select  sum(Cantidad) As Cantidad, Repuesto
from ReparacionRepuestos
group by Repuesto
order by Cantidad DESC;
end

create procedure FinalizarReparacion
@Date datetime,
@Reparacion int
as
begin
update Reparacion
set FchSalida = @Date
where RepCod = @Reparacion;
end

create procedure RemoveCliente
@Cliente int
as
begin
delete from Clientes where CliCod = @Cliente;
end

create procedure CambiarContrasenia
@Cliente int,
@Pass varchar(40)
as
begin
update Clientes 
set CliPass = @Pass
where CliCod = @Cliente;
end

create procedure GetClientes
as
begin
select * from Clientes;
return;
end

create procedure GetClientesVehiculos
as
begin
select * from ClienteVehiculo;
return;
end

create procedure GetMarcas
as
begin
select * from Marcas;
return;
end

create procedure GetTipos
as
begin
select * from Tipos;
return;
end

create procedure GetVehiculos
as
begin
select * from Vehiculos;
return;
end

create procedure GetVehiculoReparaciones
as
begin
select rep.*, res.*
from Reparacion rep
inner join Reservas res on rep.Reserva=res.Id;
return;
end

create procedure GetReservas
as
begin
select res.*
from Reservas res;
return;
end

create procedure GetMecanicos
as
begin
select *
from Mecanicos;
return;
end

create procedure GetProveedores
as
begin
select *
from Proveedores;
return;
end

create procedure GetRepuestos
as
begin
select *
from Repuestos;
return;
end

create procedure GetReparacionRepuestos
as
begin
select *
from ReparacionRepuestos;
return;
end

create procedure PromoteCliente
@Id int
as
begin
update Clientes set EsAdmin = 1 where CliCod = @Id;
end

create procedure DegradeCliente
@Id int
as
begin
update Clientes set EsAdmin = 0 where CliCod = @Id;
end