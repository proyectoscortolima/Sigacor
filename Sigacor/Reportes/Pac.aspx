<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pac.aspx.vb" Inherits="Sigacor.Pac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sigacor</title>

    <!--Iconos-->
    <link href="../Componentes/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <!--Font-->
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet">
    <!--Aplicacion-->
    <link href="../Componentes/css/sb-admin-2.min.css" rel="stylesheet" />
    <script src="../Componentes/sweetAlert2/sweetalert2.all.min.js"></script>

</head>
<body style="background-image: url('../Componentes/img/FondoReport.jpg'); background-size: cover;">

    <form id="form1" runat="server">
        <asp:Label ID="lblPac" runat="server"></asp:Label>
        <div class="container-fluid">
            <nav class="navbar header-report" style="height: 70px;">
                <div class="container-fluid">
                    <a class="navbar-brand" href="../Index.aspx">
                        <img src="../Componentes/img/sigaporverde.png" alt="" width="140" height="40" class="d-inline-block align-text-top" />
                    </a>
                    <a class="btn btn-primary" href="../Index.aspx"><i class="fa fa-arrow-left"></i>Volver</a>
                </div>

            </nav>
            <div class="row" style="margin-top: 7rem;">
                
                <div class="col-3 mt-2">
                    <div class="form-group">
                        <h6>Periodo</h6>
                        <asp:DropDownList ID="cmbPac" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>                
                <div class="col-3 mt-2">
                    <a class="card-report-2" data-toggle="collapse" href="#fltGeneral" role="button" aria-expanded="false" aria-controls="collapseExample">
                        <div class="card-header-report">
                            <div class="row">
                                <div class="col-12">
                                    <h5 class="mb-0">
                                        <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            Filtro General <i class="fa fa-arrow-down ml-3"></i>
                                        </button>
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </a>
                    <div class="collapse" id="fltGeneral">
                        <div class="card-report card-body mb-3" style="margin-top: 0.4rem; padding: 0rem;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 mt-2">
                                        <div class="form-group">
                                            <h6>Nivel</h6>
                                            <asp:DropDownList ID="cmbNivel" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2 text-center">
                                        <asp:LinkButton ID="btnConsultarGeneral" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-3 mt-2">
                    <a class="card-report-2" data-toggle="collapse" href="#fltAvanzado" role="button" aria-expanded="false" aria-controls="collapseExample">
                        <div class="card-header-report">
                            <div class="row">
                                <div class="col-12">
                                    <h5 class="mb-0">
                                        <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            Filtro Avanzado <i class="fa fa-arrow-down ml-3"></i>
                                        </button>
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </a>
                    <div class="collapse" id="fltAvanzado">
                        <div class="card-report card-body mb-3" style="margin-top: 0.4rem;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 mt-2">
                                        <div class="form-group">
                                            <asp:Label ID="lblLineas" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbLineas" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2" id="pnlNiv2" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblNiv2" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbNiv2" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2" id="pnlNiv3" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblNiv3" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbNiv3" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2" id="pnlNiv4" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblNiv4" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbNiv4" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2" id="pnlNiv5" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblNiv5" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbNiv5" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2" id="pnlNiv6" runat="server">
                                        <div class="form-group">
                                            <asp:Label ID="lblNiv6" runat="server"></asp:Label>
                                            <asp:DropDownList ID="cmbNiv6" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2 text-center">
                                        <asp:LinkButton ID="btnConsultar" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-3 mt-2">
                    <a class="card-report-2" data-toggle="collapse" href="#fltPalabraClv" role="button" aria-expanded="false" aria-controls="collapseExample">
                        <div class="card-header-report">
                            <div class="row">
                                <div class="col-12">
                                    <h5 class="mb-0">
                                        <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            Filtro palabra clave  <i class="fa fa-arrow-down ml-3"></i>
                                        </button>
                                    </h5>
                                </div>
                            </div>
                        </div>
                    </a>
                    <div class="collapse" id="fltPalabraClv">
                        <div class="card-report card-body mb-3" style="margin-top: 0.4rem;">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 mt-2">
                                        <div class="form-group">
                                            <h6>Palabra clave</h6>
                                            <asp:TextBox ID="txtPalabraClave" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-12 mt-2 text-center">
                                        <asp:LinkButton ID="btnConsultarPalabraClave" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mt-2">
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkNoProgramado" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl1.svg" class="icon-report" height="60" width="60" />
                    <h6 class="mt-3 label-conveciones">No Programado</h6>
                </div>
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkEjecMenos25" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl2.svg" class="icon-report" height="60" width="60" />
                    <h6 class="mt-3 label-conveciones">Se Ha ejecutado menos del 25%</h6>
                </div>
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkEjec25Al49" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl3.svg" class="icon-report" height="60" width="60" />
                    <h6 class="mt-3 label-conveciones">Se ha ejecutado del 25% al 49%</h6>
                </div>
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkEjec50Al74" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl4.svg" class="icon-report" height="60" width="60" />
                    <h6 class="mt-3 label-conveciones">Se ha ejecutado del 50% al 74%</h6>
                </div>
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkEjec75Al99" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl5.svg" class="icon-report" height="60" width="60" />
                    <h6 class="mt-3 label-conveciones">Se ha ejecutado del 75% al 99.9%</h6>
                </div>
                <div class="col-xs-12 col-md-2 text-center">
                    <asp:CheckBox ID="chkEjecMas100" runat="server" OnCheckedChanged="chkConvenciones_CheckedChanged" AutoPostBack="true" />
                    <img src="../Componentes/img/nvl6.svg" class="icon-report" height="70" width="70" />
                    <h6 class="mt-3 label-conveciones">Se ha ejecutado mas del 100%</h6>
                </div>
            </div>

            <asp:Panel ID="pnlResultados" runat="server" class="row mt-2">
            </asp:Panel>
        </div>


        <div>
        </div>

        <asp:Label ID="lblError" runat="server" Style="color: red;"></asp:Label>
    </form>

    <footer class="sticky-footer bg-white mt-5">
        <div class="container my-auto">
            <div class="copyright text-center my-auto">
                <span>Copyright &copy; Cortolima 2022</span>
            </div>
        </div>
    </footer>

    <style>
        input[type=checkbox] {
            height: 25px;
            width: 25px;
        }
    </style>

    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>
    <script src="../Componentes/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="../Componentes/js/sb-admin-2.min.js"></script>
    <script src="../Componentes/vendor/datatables/datatables.min.js"></script>
    <script src="../Componentes/vendor/datatables/scriptTable.js"></script>
</body>
</html>
