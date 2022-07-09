<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Iniciar sesion - Obl. 1 - Prog. 3</title>
  <link rel="stylesheet" runat="server" media="screen" href="~/Content/Login.css" />
</head>
<body>
  <form id="form1" runat="server">
    <div id="root">
      <section>
        <h1 class="title">Inicia sesión</h1>
        <div class="row">
          <label for="user">Email:</label>
          <input runat="server" id="txtUser" type="text" name="user" />
        </div>
        <div class="row">
          <label for="pass">Contraseña:</label>
          <input runat="server" id="txtPass" type="password" name="pass" />
        </div>
        <div class="buttons">
          <asp:Button runat="server" Text="Acceder" CssClass="button primary" OnClick="Unnamed1_Click" />
          <a href="/register" class="button secondary">No tengo cuenta</a>
        </div>
      </section>
    </div>
  </form>
</body>
</html>