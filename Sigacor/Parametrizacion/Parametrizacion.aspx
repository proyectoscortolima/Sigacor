<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Parametrizacion.aspx.vb" Inherits="Sigacor.Parametrizacion" MasterPageFile="~/Principal.Master" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">

    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>    

    <div class="container">
        <div class="row">
            <div class="col-5">
                <h3>PARAMETRIZACIÓN DEL PAC</h3>
            </div>
            <div class="col-7">
                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
            </div>
        </div>

        <div class="row">
            <div class="col-12 text-right">
                <a class="btn btn-success btn-icon-split" href="NuevoRegistro.aspx">
                    <span class="icon text-white-50">
                        <i class="fas fa-plus"></i>
                    </span>
                    <span class="text">Nuevo PAC</span>
                </a>
                <div class="btn-group" role="group" aria-label="Basic example">
                    <span class="icon text-white-50" style="display: inline-block; padding: .375rem 0.75rem; background-color: #c9a811;">
                        <i class="fas fa-filter"></i>
                    </span>
                    <asp:DropDownList ID="cmbFiltrar" class="btn btn-amarillo dropdown-toggle" runat="server">
                        <asp:ListItem>Filtrar por </asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="row mt-4">
                    <div class="col-12 text-left" style="overflow-x: auto; overflow-y: auto;">
                        <asp:GridView ID="tblPac" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Código" />
                                <asp:BoundField DataField="name" HeaderText="Nombre" />
                                <asp:BoundField DataField="initial_year" HeaderText="Año Inicial" />
                                <asp:BoundField DataField="final_year" HeaderText="Año Final" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" data-placement="top"
                                            data-toggle="tooltip" Height="30px" Width="30px" CommandName="Editar"
                                            Style="display: inline-grid" title="Editar PAC" class="btn btn-success btn-circle">                                           
                                            <i class="fas fa-edit"></i>                                        
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkVisualizar" runat="server" data-placement="top"
                                            data-toggle="tooltip" Height="30px" Width="30px" CommandName="Visualizar"
                                            Style="display: inline-grid" title="Ver PAC" class="btn btn-success btn-circle">                                            
                                                <i class="fas fa-eye"></i>                                                                                    
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="lnkEliminar" runat="server" data-placement="top"
                                            data-toggle="tooltip" Height="30px" Width="30px" CommandName="Eliminar"
                                            Style="display: inline-grid" title="Eliminar PAC" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-backspace"></i>
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>
            </div>
        </div>
    </div>

    <div class="modal fade" id="mdlVisualizador" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="col-12 mb-2 border-bottom-info">
                        <label class="lblModal">PAC</label>
                    </div>
                    <div class="row mt-4 mb-2">
                        <div class="col-6">
                            <label class="lblModal">N° PAC</label>
                        </div>
                        <div class="col-6">
                            <label class="lblModal">Nombre del PAC</label>
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblPac" runat="server"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblNomPac" runat="server"></asp:Label>
                        </div>
                        <div class="col-4">
                            <label class="lblModal">Año Inicial</label>
                        </div>
                        <div class="col-4">
                            <label class="lblModal">Cantidad de años</label>
                        </div>
                        <div class="col-4"></div>
                        <div class="col-4">
                            <asp:Label ID="lblYearIni" runat="server"></asp:Label>
                        </div>
                        <div class="col-4">
                            <asp:Label ID="lblCantYears" runat="server"></asp:Label>
                        </div>

                        <div class="col-12 mt-4 mb-2 border-bottom-info">
                            <label class="lblModal">Niveles</label>
                        </div>

                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblNivParam" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="hierarchy" HeaderText="Código" />
                                    <asp:BoundField DataField="name" HeaderText="Nombre" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="col-12 mt-4 mb-2 border-bottom-info">
                            <label class="lblModal">Plan de acción cuatrienal</label>
                        </div>

                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblPlanAccParam" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="level_id" HeaderText="Nivel" />
                                    <asp:BoundField DataField="code" HeaderText="Jerarquia" />
                                    <asp:BoundField DataField="name" HeaderText="Nombre" />
                                    <asp:BoundField DataField="weigth" HeaderText="Peso" />
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

    <asp:Button ID="eliminarPac" runat="server" CssClass="d-none" />

    <style>
        .espacioBtnAlerta{
            margin-right: 40px;
        }
    </style>

    <script>
        $(window).on('load', function () {
            $('#1').addClass("MnuActive");
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

        // Link to open the dialog
        $("#dialog-link").click(function (event) {
            $("#dialog").dialog("open");
            event.preventDefault();
        });
    </script>
</asp:Content>

