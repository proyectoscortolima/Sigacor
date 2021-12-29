<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Seguimiento.aspx.vb" Inherits="Sigacor.Seguimiento" MasterPageFile="~/Principal.Master" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>

    <asp:Label ID="idReport" runat="server" CssClass="d-none"></asp:Label>
    <asp:Label ID="pac" runat="server" CssClass="d-none"></asp:Label>    
    <asp:Label ID="meta" runat="server" CssClass="d-none"></asp:Label>

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-xs-12"></div>
            <div class="col-md-4 col-xs-12">
                <nav class="nav nav-pills nav-justified">
                    <div class="nav-link text-center">
                        <h4>EJECUCIÓN DE LA META</h4>
                    </div>
                </nav>
            </div>
            <div class="col-md-4 col-xs-12"></div>
        </div>


        <nav class="nav nav-pills nav-justified mt-3" style="height: 60px; border-radius: 10px; box-shadow: 4px 4px 8px #bdbdbd;">
            <asp:LinkButton ID="navMetas" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">1</span>
                Seleccione la meta</asp:LinkButton>
            <asp:LinkButton ID="navActividades" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">2</span>
                Actividades desarrolladas</asp:LinkButton>
            <asp:LinkButton ID="navEvidencias" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">3</span>
                Evidencias</asp:LinkButton>

        </nav>
        <br />
        <br />

        <asp:Panel ID="Panel" runat="server" class="card mb-4 py-3 border-bottom-info" Style="box-shadow: 4px 4px 8px #bdbdbd; padding: 2rem;">
            <asp:Panel ID="pnlMetas" runat="server" class="card mb-4 py-3 border-bottom-info">
                <div class="card-body">
                    <div class="row">
                        <div class="col-2">
                            <h5>Filtro</h5>
                        </div>
                        <div class="col-10">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-3 mt-4">
                            <div class="form-group">
                                <asp:Label ID="lblLineas" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbLineas" class="form-control mt-1" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3 mt-4" id="pnlNiv2" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNiv2" runat="server" class=""></asp:Label>
                                <asp:DropDownList ID="cmbNiv2" class="form-control mt-1" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3 mt-4" id="pnlNiv3" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNiv3" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNiv3" class="form-control mt-1" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3 mt-4">
                            <div class="form-group" id="pnlNiv4" runat="server">
                                <asp:Label ID="lblNiv4" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNiv4" class="form-control mt-1" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3 mt-2">
                            <div class="form-group" id="pnlNiv5" runat="server">
                                <asp:Label ID="lblNiv5" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNiv5" class="form-control" runat="server" AutoComplete="Off" ></asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-12 mt-4 text-center">
                            <asp:LinkButton ID="btnConsultar" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                        </div>

                        <div class="col-2 mt-2">
                            <h5>Resultados</h5>
                        </div>
                        <div class="col-10 mt-2">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblMetas" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="" />
                                    <asp:BoundField DataField="name" HeaderText="" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSeleccionar" runat="server" data-placement="top"
                                                data-toggle="tooltip" CommandName="Editar"
                                                Style="display: inline-grid" title="Seleccionar Meta" class="btn btn-primary">                                           
                                            Ejecutar                                 
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:Panel>


            <asp:Panel ID="pnlInfoMetas" runat="server" class="card mb-4 py-3 border-bottom-info">

                <a class="card" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                    <div class="card-header" id="headingOne">
                        <div class="row">
                            <div class="col-2">
                                <h5 class="mb-0">
                                    <button class="btn" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                        Ver Meta <i class="fa fa-arrow-down ml-3"></i>
                                    </button>
                                </h5>
                            </div>
                            <div class="col-9">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>
                        </div>
                    </div>
                </a>

                <div class="collapse show" id="collapseExample">
                    <div class="card card-body">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-5">
                                    <h5>INFORMACION DE LA META</h5>
                                </div>
                                <div class="col-7">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>
                                <div class="col-6 mt-4">
                                    <div class="form-group">
                                        <label>Nombre / Descripción de la meta</label>
                                        <asp:TextBox ID="txtNombreMeta" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-6 mt-4">
                                    <div class="form-group">
                                        <label>Tipo de meta</label>
                                        <asp:DropDownList ID="cmbTipoMeta" class="form-control" runat="server" AutoComplete="Off" disabled></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>
                                <div class="col-4 text-center">
                                    <h5>Programación anual</h5>
                                </div>
                                <div class="col-4">
                                    <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                                </div>

                                <div class="col-4">
                                    <div class="form-group">
                                        <label>Linea base</label>
                                        <asp:TextBox ID="txtLineaBase" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblPriYear" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtPriYear" class="form-control mt-2" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblSegYear" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtSegYear" class="form-control mt-2" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblTercYear" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtTercYear" class="form-control mt-2" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCuartYear" runat="server"></asp:Label>
                                        <asp:TextBox ID="txtCuartYear" class="form-control mt-2" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-3"></div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblYearActual" runat="server"></asp:Label>
                                        <asp:TextBox TextMode="Number" ID="txtYearActual" class="form-control mt-2" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3">
                                    <div class="form-group">
                                        <label>Valor de progreso actual</label>
                                        <asp:TextBox TextMode="Number" ID="txtValorProgreso" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-3"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlActividades" runat="server" class="card mb-4 py-3 border-bottom-info">
                <div class="card-body">
                    <div class="row">
                        <div class="col-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-4 text-center">
                            <h5>Actividades desarrolladas</h5>
                        </div>
                        <div class="col-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-12">
                            <div class="form-group">
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtActividades" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="form-group">
                                <label>Valor físico</label>
                                <asp:TextBox ID="txtValorFisico" class="form-control" runat="server" AutoComplete="Off" onkeyup="validarNumero(this)"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-5">
                            <div class="form-group">
                                <label>Quien reporta</label>
                                <asp:DropDownList ID="cmbQuienReporta" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4 mt-4">
                            <asp:LinkButton ID="btnVisualizarHojaVida" runat="server" class="btn btn-primary">Visualizar hoja de vida</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="row">
                        <div class="col-12 mt-4 text-center">
                            <asp:LinkButton ID="btnGrabar" runat="server" class="btn btn-primary">Grabar y continuar</asp:LinkButton>
                        </div>
                    </div>
            </asp:Panel>

            <asp:Panel ID="pnlEvidencias" runat="server" class="card mb-4 py-3 border-bottom-info">
                <div class="card-body">
                    <div class="row">
                        <div class="col-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-4 text-center">
                            <h5>Evidencias</h5>
                        </div>
                        <div class="col-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>
                        <div class="col-12 mt-4 text-center">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mdlAdjArchivos">
                                Archivos planos (pdf, excel, word)
                            </button>
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mdlAdjImagenes">
                                Imagenes
                            </button>
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mdlLinks">
                                Links
                            </button>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 mt-4 text-center">
                        <asp:LinkButton ID="btnFinalizar" runat="server" class="btn btn-primary">Finalizar</asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
        <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>
    </div>


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

    <div class="modal fade" id="mdlAdjArchivos" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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
                        <div class="col-10">
                            <div class="form-group">
                                <label>Cargue el archivo</label>
                                <asp:FileUpload type="FileStream" AllowMultiple="true" ID="fuArchivo" runat="server" accept=".pdf, .xlsx, .xls, .docx, .txt" CssClass="btn btn-primary btn-icon-split" />
                                <input type="text" id="txtArchivo" runat="server" class="form-control" style="display: none;" />
                            </div>
                        </div>
                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblArchivos" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre del archivo" />
                                    <asp:BoundField DataField="ruta" HeaderText="Ruta" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" data-placement="top"
                                                data-toggle="tooltip" Height="30px" Width="30px" CommandName="Eliminar"
                                                Style="display: inline-grid" title="Eliminar adjunto" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-backspace"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="mdlAdjImagenes" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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
                        <div class="col-10">
                            <div class="form-group">
                                <label>Cargue la imagen</label>
                                <asp:FileUpload type="FileStream" ID="fuImagenes" runat="server" accept=".pdf, .xlxs, .word, .jpg" CssClass="btn btn-primary btn-icon-split" />
                                <input type="text" id="txtImagen" runat="server" class="form-control" style="display: none;" />
                            </div>
                        </div>
                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblImagenes" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre del archivo" />
                                    <asp:BoundField DataField="ruta" HeaderText="Ruta" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" data-placement="top"
                                                data-toggle="tooltip" Height="30px" Width="30px" CommandName="Eliminar"
                                                Style="display: inline-grid" title="Eliminar adjunto" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-backspace"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="mdlLinks" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
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
                            <label>Cargue la imagen</label>
                        </div>
                        <div class="col-12">
                            <div class="form-group">
                                <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtLinks" class="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
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


    <asp:Button ID="CargarArchivo" runat="server" class="d-none" />
    <asp:Button ID="CargarImagen" runat="server" class="d-none" />

    <script>
        $(window).on('load', function () {
            $('#3').addClass("MnuActive");
        });

        function abrirModal() {
            $(window).on('load', function () {
                $('#mdlVisualizador').modal('show');
            });
        };
        function abrirModalAdjutnosArch() {
            $(window).on('load', function () {
                $('#mdlAdjArchivos').modal('show');
            });
        };
        function abrirModalAdjutnosImg() {
            $(window).on('load', function () {
                $('#mdlAdjImagenes').modal('show');
            });
        };
        function abrirModalAdjutnosLink() {
            $(window).on('load', function () {
                $('#mdlLinks').modal('show');
            });
        };

        function validarNumero(campo) {
            var decimals;
            var num = campo.value;
            num += '';
            num = parseFloat(num.replace(/[^0-9]/g, ''));
            decimals = decimals || 0;
            if (isNaN(num) || num === 0) {
                campo.value = "";
            } else {
                campo.value = num;
            }
        };

        //document.getElementById('fake-file-button-browse').addEventListener('click', function () {
        //    document.getElementById('contenedor2_fuArchivo').click();
        //});

        document.getElementById('contenedor2_fuArchivo').addEventListener('change', function () {
            document.getElementById('contenedor2_txtArchivo').value = this.value;
            document.getElementById('contenedor2_CargarArchivo').click();
        });

        document.getElementById('contenedor2_fuImagenes').addEventListener('change', function () {
            document.getElementById('contenedor2_txtImagen').value = this.value;
            document.getElementById('contenedor2_CargarImagen').click();
        });

        window.onload = function () {
            var pos = window.name || 0;
            window.scrollTo(0, pos);
        }
        window.onunload = function () {
            window.name = self.pageYOffset || (document.documentElement.scrollTop + document.body.scrollTop);
        }
    </script>
</asp:Content>
