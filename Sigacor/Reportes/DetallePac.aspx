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
        <asp:Label ID="lblPac" runat="server"></asp:Label>
        <div class="container-fluid">
            <nav class="navbar header-report">
                <div class="container-fluid">
                    <a class="navbar-brand" href="../Index.aspx">
                        <img src="../Componentes/img/sigaporverde.png" alt="" width="250" height="80" class="d-inline-block align-text-top" />
                    </a>
                </div>
            </nav>

            <div class="row" style="margin-top: 9rem;">
                <div class="col-xs-12 col-md-12">
                    <div class="card-report">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-xs-6 col-md-6">
                                    <asp:Label ID="lblNomPac" runat="server" Text="Siembra tu Futuro PAC 2020-2023" CssClass="h3"></asp:Label>
                                </div>
                                <div class="col-xs-6 col-md-6">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>
                                <div class="col-xs-12 col-md-12" style="overflow-x: auto">
                                    <asp:GridView ID="tblJerarquia" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="code" HeaderText="Código" />
                                            <asp:BoundField DataField="name" HeaderText="Nombre" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>           
        </div>


        <div>
        </div>

        <asp:Label ID="lblError" runat="server" style="color:red;"></asp:Label>
    </form>

    <footer class="sticky-footer bg-white mt-5">
                    <div class="container my-auto">
                        <div class="copyright text-center my-auto">
                            <span>Copyright &copy; Cortolima 2021</span>
                        </div>
                    </div>
                </footer>

    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>
    <script src="../Componentes/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="../Componentes/js/sb-admin-2.min.js"></script>
    <script src="../Componentes/vendor/datatables/datatables.min.js"></script>
    <script src="../Componentes/vendor/datatables/scriptTable.js"></script>
</body>
</html>
