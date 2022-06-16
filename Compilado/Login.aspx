<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Sigacor.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sigacor</title>


    <link href="Componentes/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />
    <link href="Componentes/css/sb-admin-2.min.css" rel="stylesheet" />
    <script src="Componentes/sweetAlert2/sweetalert2.all.min.js"></script>
    <script src="Componentes/vendor/jquery/jquery.min.js"></script>

</head>
<body style="background-image: url('Componentes/img/imagen_cortolima.png'); background-size: cover;">
    <form id="form1" runat="server">

        <div class="container">
            <div class="row my-3">
                <div class="col-7"></div>
                <div class="col-5">
                    <img src="Componentes/img/sigaporverde.png" height="130" width="450" /><br />
                </div>
            </div>
            <div class="row">
                <div class="col-3"></div>
                <div class="col-6">
                    <div class="card o-hidden border-0 shadow-lg my-3">
                        <div class="card-body p-0">
                            <div class="p-5">
                                <div class="form-group">
                                    <label>Usuario</label>
                                    <asp:TextBox ID="txtUsuario" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Contraseña</label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="row">
                                    <div class="col-3"></div>
                                    <div class="col-6">
                                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" class="btn btn-primary btn-user btn-block"/>                                        
                                    </div>
                                    <div class="col-3"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3"></div>
            </div>
        </div>
        <div class="row my-2">
            <div class="col-7"></div>
            <div class="col-5">
                <footer>
                        <div class="row">
                            <div class="col-7 text-center">
                                <img style="border-right: 2px solid #36523F" src="Componentes/img/cortolima.png" height="130" width="220" /><br />                                
                            </div>
                            <div class="col-5 text-center">
                                <br />
                                <img class="mt-3" src="Componentes/img/LOGOS-ICONTEC-CERTIFICACIONES-2020.png" height="60" width="180" />                                
                            </div>
                            <div class="col-12 text-center mb-4"> 
                                <label class="tlPanel">www.cortolima.gov.co</label><br />
                                <a href="#" class="btn btn btnRedesociales btn-circle">
                                    <i class="fab fa-facebook-f"></i>
                                </a>
                                <a href="#" class="btn btnRedesociales btn-circle">
                                    <i class="fab fa-instagram"></i>
                                </a>
                                <a href="#" class="btn btnRedesociales btn-circle">
                                    <i class="fab fa-whatsapp"></i>
                                </a>
                                <a href="#" class="btn btnRedesociales btn-circle">
                                    <i class="fab fa-youtube"></i>
                                </a>
                            </div>
                        </div>
                    </footer>
            </div>
        </div>
    </form>
</body>
</html>
