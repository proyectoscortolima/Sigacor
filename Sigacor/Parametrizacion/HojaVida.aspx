<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HojaVida.aspx.vb" Inherits="Sigacor.HojaVida" MasterPageFile="~/Principal.Master" Culture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>

    <div class="container">
        <div class="row">
            <div class="col-md-4 col-xs-12"></div>
            <div class="col-md-4 col-xs-12">
                <nav class="nav nav-pills nav-justified">
                    <div class="nav-link text-center">
                        <h4>HOJA DE VIDA</h4>
                    </div>
                </nav>
            </div>
            <div class="col-md-4 col-xs-12"></div>
        </div>

        <br />
        <br />
        <asp:Panel ID="pnlInfoHojaVida" runat="server" CssClass="row">
            <div class="col-2">
                <h4>Metas</h4>
            </div>
            <div class="col-7">
                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
            </div>
            <div class="col-3 text-right">
                <div class="btn-group" role="group" aria-label="Basic example">
                    <span class="icon text-white-50" style="display: inline-block; padding: .375rem 0.75rem; background-color: #c9a811;">
                        <i class="fas fa-filter"></i>
                    </span>
                    <asp:DropDownList ID="cmbFiltrar" class="btn btn-amarillo dropdown-toggle" runat="server">
                        <asp:ListItem>Filtrar por </asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-12 mt-4" style="overflow-x: auto; overflow-y: auto;">
                <asp:GridView ID="tblMetass" runat="server" CssClass="sem table" Width="100%" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="id" />
                        <asp:BoundField DataField="type_goal" HeaderText="Tipo Meta" />
                        <asp:BoundField DataField="subactivity" HeaderText="Sub Activ." />
                        <asp:BoundField DataField="name" HeaderText="Nombre" />
                        <asp:BoundField DataField="line_base" HeaderText="Linea Base" />
                        <asp:BoundField DataField="" HeaderText="Hoja de Vida" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkMeta" runat="server" data-placement="top"
                                    data-toggle="tooltip" Height="30px" Width="30px" CommandName="Seleccionar"
                                    Style="display: inline-grid" title="Seleccionar meta" class="btn btn-success btn-circle">                                            
                                                <i class="fas fa-edit"></i>                                        
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlHojaVida" runat="server" class="card mb-4 py-3 border-bottom-info" Style="box-shadow: 4px 4px 8px #bdbdbd;">
            <asp:Label ID="lblCodMeta" runat="server" Text="Label"></asp:Label>
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label>Sigla de la hoja de vida</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtSiglaHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Descripción de la hoja de vida</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtDescripHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Definición de la hoja de vida</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtDefinHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Método de medición</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtMetodoMedic" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Formulas de la hoja de vida</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtFormulaHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Variables de la hoja de vida</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtVariablesHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Observaciones</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtObservHojaVida" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Desag. Geográfica</label>
                            <asp:TextBox TextMode="MultiLine" Rows="5" ID="txtGeografica" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6">
                        <div class="form-group">
                            <label>Periodicidad</label>
                            <asp:DropDownList ID="cmbPeriodicidad" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-12 text-center mt-1">
                        <asp:LinkButton ID="btnGrabar" runat="server" class="btn btn-primary">Grabar</asp:LinkButton>
                        <asp:LinkButton ID="btnActualizar" runat="server" class="btn btn-primary">Actualizar</asp:LinkButton>
                        <asp:LinkButton ID="btnLimpiar" runat="server" class="btn btn-primary">Limpiar</asp:LinkButton>
                        <asp:LinkButton ID="btnAtras" runat="server" class="btn btn-primary">Atras</asp:LinkButton>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>
    </div>


    <script>
        $(window).on('load', function () {            
            $('#2').addClass("MnuActive");
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
