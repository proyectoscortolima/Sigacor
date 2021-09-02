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
    <script src="Componentes/sweetAlert2/sweetalert2.all.min.js"></script>
    <script src="Componentes/vendor/jquery/jquery.min.js"></script>     

</head>
<body style="background-image: url('Componentes/img/imagen_cortolima.png'); background-size: cover;">
    <form id="form1" runat="server">

            <div class="row mt-2">
                <div class="col-xl-10 col-lg-10 col-md-10"></div>
                <div class="col-xl-2 col-lg-2 col-md-2 text-center">
                    <asp:LinkButton ID="btnLogin" runat="server" class="btn btn-primary" style="border-radius:100px">
                        <img src="Componentes/img/funcionarios-02.svg"  height="80" width="70" style="padding: 12px;" /> 
                    </asp:LinkButton>
                    <h5>Funcionarios</h5>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 text-center">
                    <img src="Componentes/img/sigaporverde.png" height="200" width="600" />
                </div>
            </div>
            <div class="row mt-5">
                <div class="col-xl-3 col-lg-3 col-md-3"></div>
                <div class="col-xl-2 col-lg-2 col-md-2 text-center">
                    <a href="Reportes/Pac.aspx" class="btn btn-primary btn-index">
                        <img src="Componentes/img/plantaPac.svg" height="150" width="140" class="padding-index"  />
                    </a>
                    <br />
                    <label class="tlPanel mt-2">PAC</label>
                </div>
                <div class="col-xl-2 col-lg-2 col-md-2 text-center">
                    <a href="Reportes/Pac.aspx" class="btn btn-primary btn-index">
                        <img src="Componentes/img/btnPgar.svg" height="150" width="140" class="padding-index"  />
                    </a>
                    <br />
                    <label class="tlPanel mt-2">PGAR</label>
                </div>
                <div class="col-xl-2 col-lg-2 col-md-2 text-center">
                    <a href="Reportes/Pac.aspx" class="btn btn-primary btn-index">
                        <img src="Componentes/img/btnProyectoBanco.svg" height="150" width="140" class="padding-index" />
                    </a>
                    <br />
                    <label class="tlPanel mt-2">
                        BANCO DE
                                <br />
                        PROYECTOS
                    </label>
                </div>
                <div class="col-xl-3 col-lg-3 col-md-3"></div>
            </div>

        


        <footer>
            <div class="row mt-2">
                <div class="col-2"></div>
                <div class="col-8" style="border-bottom: 2px solid #337270;"></div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2"></div>
                <div class="col-3">
                    <img src="Componentes/img/cortolima.png" height="100" width="150" />
                    <img class="mt-3" src="Componentes/img/LOGOS-ICONTEC-CERTIFICACIONES-2020.png" height="50" width="150" />
                </div>
                <div class="col-2">
                    <label class="tlPanel" style="font-size: 22px; margin-top: 23%;">www.cortolima.gov.co</label>
                </div>
                <div class="col-2 text-center" style="padding-top: 2.5rem;">
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

                <div class="col-2"></div>
            </div>
        </footer>


        <div class="modal fade modal-right" id="mdlLogin" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title"></h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-12">
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
                                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" class="btn btn-primary btn-user btn-block" />
                                    </div>
                                    <div class="col-3"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <script>
        function abrirModalLogin() {
            $(window).on('load', function () {
                $('#mdlLogin').modal('show');
            });
        };
    </script>


        <%--        <div class="col-xl-12 col-lg-12 col-md-12">

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


        </div>--%>
    </form>
    <script src="Componentes/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="Componentes/js/sb-admin-2.min.js"></script>    
</body>
</html>
