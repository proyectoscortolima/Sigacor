<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Index.aspx.vb" Inherits="Sigacor.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sigacor</title>


    <link href="Componentes/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />
    <link href="Componentes/css/sb-admin-2.min.css" rel="stylesheet" />

</head>
<body style="background-image: url('Componentes/img/imagen_cortolima.png'); background-size: cover;">
    <form id="form1" runat="server">

        <div class="col-xl-12 col-lg-12 col-md-12">

            <div class="row">
                <div class="col-lg-7 d-none d-lg-block bg-login-image"></div>
                <div class="col-lg-5">
                    <div class="text-right">
                        <a href="Login.aspx">
                            <img src="Componentes/img/funcionarios-02.svg" height="100" width="120" />
                        </a>
                    </div>
                    <div class="form-group text-center">
                        <a href="Reportes/Pac.aspx">
                            <img src="Componentes/img/Artboard7.png" height="90" width="80" /><br />
                            <label class="tlPanel">PAC</label>
                        </a>
                    </div>
                    <div class="form-group text-center">
                        <a href="#">
                            <img src="Componentes/img/Artboard8.png" height="90" width="90" /><br />
                            <label class="tlPanel">PGAR</label>
                        </a>
                    </div>
                    <div class="form-group text-center">
                        <a href="#">
                            <img src="Componentes/img/Artboard6.png" height="90" width="90" /><br />
                            <label class="tlPanel">
                                BANCO DE
                                <br />
                                PROYECTOS
                            </label>
                        </a>
                    </div>
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


        </div>

    </form>
</body>
</html>
