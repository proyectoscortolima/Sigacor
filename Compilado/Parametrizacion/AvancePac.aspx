<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AvancePac.aspx.vb" Inherits="Sigacor.AvancePac" MasterPageFile="~/Principal.Master" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">

    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>


    <div class="row">
        <div class="col-3">
            <h5>AVANCE DEL PAC</h5>
        </div>
        <div class="col-9">
            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
        </div>
    </div>
    <style>
        .tip {
            color: #000;
            display: none; /*--Hides by default--*/
            padding: 10px;
            position: absolute;
            z-index: 1000;
            -webkit-border-radius: 13px;
            -moz-border-radius: 13px;
            border-radius: 13px;
            width: 550px;
            height: auto;
            filter: alpha(opacity=90);
            -moz-opacity: .90;
            opacity: .90;
            border-radius: 30px !important;
            box-shadow: 6px 8px 15px -7px #5c588b;
            background: #fff;
            padding: 1rem;
        }
    </style>
    <div class="col-12 mt-2">
        <a class="card-report-2" data-toggle="collapse" href="#filtro" role="button" aria-expanded="false" aria-controls="collapseExample" id="pnlFiltro">
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
                                           <asp:PlaceHolder ID="phDinamicControls" runat="server"></asp:PlaceHolder>
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
                    <div class="row">
                        <a class="card-report-2" data-toggle="collapse" href="#fltConvenciones" role="button" aria-expanded="false" aria-controls="collapseExample">
                            <div class="card-header-report">
                                <div class="row">
                                    <div class="col-12">
                                        <h5 class="mb-0">
                                            <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                Convenciones <i class="fa fa-arrow-down ml-3"></i>
                                            </button>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <div class="collapse" id="fltConvenciones">
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
                                    <img src="../Componentes/img/nvl6.svg" class="icon-report" height="60" width="60" />
                                    <h6 class="mt-3 label-conveciones">Se ha ejecutado mas del 100%</h6>
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

    <script>
        $(window).on('load', function () {
            $('#4').addClass("MnuActive");
        });

        window.onload = function () {
            var pos = window.name || 0;
            window.scrollTo(0, pos);
        }
        window.onunload = function () {
            window.name = self.pageYOffset || (document.documentElement.scrollTop + document.body.scrollTop);
        }

    </script>

    <script type="text/javascript">

        $(document).ready(function () {
            //Tooltips

            var t = 0;
            $('.tip_trigger').mousedown(function (event) {
                switch (event.which) {
                    case 1:
                        // alert('Left mouse button pressed');
                        break;
                    case 2:
                        // alert('Middle mouse button pressed');
                        break;
                    case 3:
                        // alert('Right mouse button pressed');
                        e.preventDefault();
                        tip.show();
                        t = 1;
                        break;
                    default:
                        alert('You have a strange mouse');
                }
            });


            $(".tip_trigger").hover(function () {
                tip = $(this).find('.tip');
                tip.show(); //Show tooltip
            }, function () {
                if (t != 1) {
                    tip.hide();
                }//Hide tooltip		  
            }).mousemove(function (e) {
                var mousex = 80; //Get X coodrinates
                var mousey = -150; //Get Y coordinates                
                var tipWidth = tip.width(); //Find width of tooltip
                var tipHeight = tip.height(); //Find height of tooltip

                //Distance of element from the right edge of viewport
                var tipVisX = $(window).width() - (mousex + tipWidth);
                //Distance of element from the bottom of viewport
                var tipVisY = $(window).height() - (mousey + tipHeight);



                if (tipVisX < 20) { //If tooltip exceeds the X coordinate of viewport
                    mousex = e.pageX - tipWidth - 20;
                }
                if (tipVisY < 20) { //If tooltip exceeds the Y coordinate of viewport
                    mousey = e.pageY - tipHeight - 20;
                }
                tip.css({ top: mousey, left: mousex });
            });
            $(".tip").hover(function () {
                tip.css({ top: mousey, left: mousex });
            });
        });

    </script>
</asp:Content>


