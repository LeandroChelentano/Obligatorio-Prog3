<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Web.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Registrarse - Obl. 1 - Prog. 3</title>
  <link rel="stylesheet" runat="server" media="screen" href="~/Content/Login.css" />
</head>
<body>
  <form id="form1" runat="server">
    <div id="root">
      <section>
        <h1 class="title">Registrarme</h1>
        <div class="row">
          <label for="name">Nombre:</label>
          <input runat="server" id="txtNombre" type="text" name="name" />
        </div>
        <div class="row">
          <label for="email">Email:</label>
          <input runat="server" id="txtEmail" type="text" name="email" />
        </div>
        <div class="row">
          <label for="ci">Cedula:</label>
          <input runat="server" id="txtCi" type="number" name="ci" />
        </div>
        <div class="row">
          <label for="telefono">Telefono:</label>
          <input runat="server" id="txtTelefono" type="number" name="telefono" />
        </div>
        <div class="row">
          <label for="pass">Contraseña:</label>
          <input runat="server" id="txtPass" type="password" name="pass" />
        </div>
        <div class="buttons">
          <asp:Button runat="server" Text="Registrarse" CssClass="button primary" OnClick="Unnamed1_Click" />
          <a href="/login" class="button secondary">Ya tengo cuenta</a>
        </div>
      </section>
    </div>
  </form>
</body>
</html>
