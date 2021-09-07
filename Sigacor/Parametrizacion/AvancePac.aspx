<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AvancePac.aspx.vb" Inherits="Sigacor.AvancePac" MasterPageFile="~/Principal.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">

    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>

    <div class="row">
        <div class="col-3">
            <h5>AVANCE DEL PAC</h5>
        </div>
        <div class="col-9">
            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
        </div>
    </div>


    <div class="col-12 mt-2">
        <a class="card-report-2" data-toggle="collapse" href="#filtro" role="button" aria-expanded="false" aria-controls="collapseExample">
            <div class="card-header-report">
                <div class="row">
                    <div class="col-12">
                        <h5 class="mb-0">
                            <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                Filtro <i class="fa fa-arrow-down ml-3"></i>
                            </button>
                        </h5>
                    </div>
                </div>
            </div>
        </a>
        <div class="collapse" id="filtro">
            <div class="card-report card-body mb-3" style="margin-top: 0.4rem;">
                <div class="card-body">
                    <div class="row">
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
                                <div class="card-report card-body mb-3" style="margin-top: 0.4rem;">
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
                                                    <asp:Label ID="lblLineas" runat="server" CssClass="h6"></asp:Label>
                                                    <asp:DropDownList ID="cmbLineas" class="form-control mt-2" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2" id="pnlNiv2" runat="server">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNiv2" runat="server" CssClass="h6"></asp:Label>
                                                    <asp:DropDownList ID="cmbNiv2" class="form-control mt-2" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2" id="pnlNiv3" runat="server">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNiv3" runat="server" CssClass="h6"></asp:Label>
                                                    <asp:DropDownList ID="cmbNiv3" class="form-control mt-2" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2" id="pnlNiv4" runat="server">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNiv4" runat="server" CssClass="h6"></asp:Label>
                                                    <asp:DropDownList ID="cmbNiv4" class="form-control mt-2" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2" id="pnlNiv5" runat="server" CssClass="h6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNiv5" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="cmbNiv5" class="form-control mt-2" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-12 mt-2" id="pnlNiv6" runat="server" CssClass="h6">
                                                <div class="form-group">
                                                    <asp:Label ID="lblNiv6" runat="server"></asp:Label>
                                                    <asp:DropDownList ID="cmbNiv6" class="form-control mt-2" runat="server" AutoComplete="Off"></asp:DropDownList>
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
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>

    <asp:Panel ID="pnlResultados" runat="server" class="row mt-4">
    </asp:Panel>


    <style>
        .espacioBtnAlerta {
            margin-right: 40px;
        }
    </style>

    <script>
        $(window).on('load', function () {
            $('#4').addClass("MnuActive");
        });

        function abrirModal() {
            $(window).on('load', function () {
                $('#mdlVisualizador').modal('show');
            });
        };

        function AlertaSN() {
            const swalWithBootstrapButtons = swal.mixin({
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger espacioBtnAlerta',
                buttonsStyling: false,
            })
            swalWithBootstrapButtons({
                title: 'Verificación',
                text: "¿Desea eliminar el PAC?",
                type: 'info',
                showCancelButton: true,
                confirmButtonText: 'Si',
                cancelButtonText: 'No',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    document.getElementById('contenedor2_eliminarPac').click();
                } else if (
                    // Read more about handling dismissals
                    result.dismiss === swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons(
                        'Has cancelado la eliminación del PAC',
                        '',
                        'error'
                    )
                }
            })
        };

        window.onload = function () {
            var pos = window.name || 0;
            window.scrollTo(0, pos);
        }
        window.onunload = function () {
            window.name = self.pageYOffset || (document.documentElement.scrollTop + document.body.scrollTop);
        }


        $("#dialog").dialog({
            autoOpen: false,
            width: 400,
            buttons: [
                {
                    text: "Ok",
                    click: function () {
                        $(this).dialog("close");
                    }
                },
                {
                    text: "Cancel",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });

    </script>
</asp:Content>


