<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DetallePac.aspx.vb" Inherits="Sigacor.DetallePac" %>

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
        <asp:Label ID="lblPac" runat="server" Style="display: none"></asp:Label>
        <asp:Label ID="lblCodMeta" runat="server" Style="display: none"></asp:Label>
        <div class="container-fluid">
            <nav class="navbar header-report">
                <div class="container-fluid">
                    <a class="navbar-brand" href="../Index.aspx">
                        <img src="../Componentes/img/sigaporverde.png" alt="" width="250" height="80" class="d-inline-block align-text-top" />
                    </a>
                </div>
            </nav>

            <div class="row" style="margin-top: 11rem;">
                <div class="col-xs-12 col-md-12">
                    <div class="card-report">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xs-6 col-md-6">
                                    <asp:Label ID="lblNomPac" runat="server" CssClass="h3"></asp:Label>
                                </div>
                                <asp:Panel ID="pnlDescripcionJerarquia" runat="server" class="col-xs-12 col-md-12 mt-3"></asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-top: 3rem;">
                <div class="col-xs-12 col-md-6">
                    <div class="card-report">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xs-3 col-md-3">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>
                                <div class="col-xs-6 col-md-6 text-center">
                                    <h5>Ejecución Fisica</h5>
                                </div>
                                <div class="col-xs-3 col-md-3">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>

                                <div class="col-xs-12 col-md-12 mt-3 card-footer card-report">
                                    Avance de meta física en el cuatrienio:
                                </div>

                                <div class="col-xs-12 col-md-3 mt-3"></div>
                                <div class="col-xs-12 col-md-3 mt-3">
                                    <b>Linea base:</b>
                                    <asp:Label ID="lblLineaBase" runat="server"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-md-3 mt-3 text-right">
                                    <b>Meta:</b>
                                    <asp:Label ID="lblMeta" runat="server"></asp:Label>
                                </div>
                                <div class="col-xs-12 col-md-3 mt-3"></div>

                                <div class="col-xs-12 col-md-3 mt-3"></div>
                                <div class="col-xs-12 col-md-6 mt-3">
                                    <div class="progress">
                                        <div class="progress-bar progress-bar-striped bg-success" role="progressbar" id="progressbarEjecucion" runat="server">
                                            <asp:Label ID="lblValorProgress" runat="server"></asp:Label></div>
                                    </div>
                                </div>
                                <div class="col-xs-12 col-md-3 mt-3"></div>
                                <div class="col-xs-12 col-md-12 mt-3">
                                    <%--<label>Meta alcanzada: 34</label>--%>
                                </div>

                                <div class="col-xs-12 col-md-12 mt-1 card-footer card-report">Actividades ejecutadas:</div>

                                <div class="col-xs-12 col-md-12 mt-3">
                                    <asp:TextBox ID="txtAvances" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="col-xs-12 col-md-12 mt-3 card-footer card-report">Fuente:</div>
                                <div class="col-xs-12 col-md-12 mt-3"><b>Sigacor - Cortolima</b></div>
                                <div class="col-xs-12 col-md-12 mt-3 text-center">
                                    <asp:LinkButton ID="btnVisualizarHojaVida" runat="server" class="btn btn-primary btn-lg">Hoja de vida del indicador</asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>


        <div>
        </div>

        <asp:Label ID="lblError" runat="server" Style="color: red;"></asp:Label>
    

    <footer class="sticky-footer bg-white mt-5">
        <div class="container my-auto">
            <div class="copyright text-center my-auto">
                <span>Copyright &copy; Cortolima 2021</span>
            </div>
        </div>
    </footer>

    <div class="modal fade" id="mdlVisualizador" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Hoja de vida</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Sigla de la hoja de vida</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtSiglaHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Descripción de la hoja de vida</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtDescripHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Definición de la hoja de vida</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtDefinHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Método de medición</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtMetodoMedic" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Formulas de la hoja de vida</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtFormulaHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Variables de la hoja de vida</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtVariablesHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Observaciones</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtObservHojaVida" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Desag. Geográfica</label>
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtGeografica" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-6">
                            <div class="form-group">
                                <label>Periodicidad</label>
                                <asp:DropDownList ID="cmbPeriodicidad" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>
    <script src="../Componentes/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="../Componentes/js/sb-admin-2.min.js"></script>
    <script src="../Componentes/vendor/datatables/datatables.min.js"></script>
    <script src="../Componentes/vendor/datatables/scriptTable.js"></script>
</body>
</html>

