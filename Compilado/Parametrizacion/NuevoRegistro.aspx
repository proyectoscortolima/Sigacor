<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NuevoRegistro.aspx.vb" Inherits="Sigacor.NuevoRegistro" MasterPageFile="~/Principal.Master" Culture="en-US" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contenedor1" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contenedor2" runat="server">
    <script src="../Componentes/vendor/jquery/jquery.min.js"></script>
    <div class="container">

        <div class="row">
            <div class="col-4">
                <asp:Label ID="lblTituloForm" runat="server" class="h3"></asp:Label>
            </div>
            <div class="col-8">
                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
            </div>
        </div>


        <nav class="nav nav-pills nav-justified mt-3" style="height: 60px; border-radius: 10px; box-shadow: 4px 4px 8px #bdbdbd;">
            <asp:LinkButton ID="btnPac" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">1</span>
                PAC</asp:LinkButton>
            <asp:LinkButton ID="btnNiveles" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">2</span>
                Niveles</asp:LinkButton>
            <asp:LinkButton ID="btnPlanAccion" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">3</span>
                Plan acción cuatrienal</asp:LinkButton>
            <asp:LinkButton ID="btnMetas" runat="server" class="nav-link">
                <span class="btn btn-primary bg-white text-black-50 btn-circle" style="margin-right: 10px;">4</span>
                Metas</asp:LinkButton>

        </nav>
        <br />
        <asp:Label ID="lblSubTitulo" runat="server" class="h4"></asp:Label>
        <asp:Panel ID="pnlPac" CssClass="mt-2" runat="server">
            <asp:Label ID="lblPac" runat="server" CssClass="d-none"></asp:Label>
            <div class="card mb-4 py-3 border-bottom-info" style="box-shadow: 4px 4px 8px #bdbdbd;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label>Nombre del PAC</label>
                                <asp:TextBox ID="txtNomPac" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Año Inicial</label>
                                <asp:TextBox TextMode="Number" ID="txtYearInicial" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Cantidad de Años</label>
                                <asp:TextBox TextMode="Number" ID="txtCantYears" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <label>Año Final</label>
                                <asp:TextBox ID="txtYearFinal" class="form-control" runat="server" AutoComplete="Off" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-12 text-center">
                            <asp:LinkButton ID="btnSigPac" runat="server" class="btn btn-primary">Grabar PAC</asp:LinkButton>
                            <asp:LinkButton ID="btnActPac" runat="server" class="btn btn-primary">Actualizar PAC</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlNiveles" CssClass="mt-2" runat="server">
            <div class="card mb-4 py-3 border-bottom-info" style="box-shadow: 4px 4px 8px #bdbdbd;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-6">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtNombreNiv" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group mt-4">
                                <asp:LinkButton ID="btnAgregar" runat="server" class="btn btn-primary">Agregar</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                            <asp:GridView ID="tblNiveles" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False" Style="cursor: pointer;">
                                <Columns>
                                    <asp:BoundField DataField="id" />
                                    <asp:BoundField DataField="hierarchy" HeaderText="Código" />
                                    <asp:BoundField DataField="name" HeaderText="Nombre" />
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNombre" class="form-control" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEditNiv" runat="server" data-placement="top"
                                                data-toggle="tooltip" Height="30px" Width="30px" CommandName="Editar"
                                                Style="display: inline-grid" title="Editar niveles" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-edit"></i>                                        
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkConEdit" runat="server" data-placement="top"
                                                data-toggle="tooltip" Height="30px" Width="30px" CommandName="Confirmar"
                                                Style="display: inline-grid" title="Confirmar" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-check"></i>                                   
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" data-placement="top"
                                                data-toggle="tooltip" Height="30px" Width="30px" CommandName="Eliminar"
                                                Style="display: inline-grid" title="Confirmar" class="btn btn-warning btn-circle">                                            
                                            <i class="fa fa-times"></i>                                   
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="col-12 text-center mt-3">
                            <asp:LinkButton ID="btnAtrasNiveles" runat="server" class="btn btn-primary">Atrás</asp:LinkButton>
                            <asp:LinkButton ID="btnSigNiveles" runat="server" class="btn btn-primary">Siguiente</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlPlanAccion" CssClass="mt-2" runat="server">
            <div class="card mb-4 py-3 border-bottom-info" style="box-shadow: 4px 4px 8px #bdbdbd;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-12 text-left">
                            <asp:LinkButton ID="btnNuevo" runat="server" class="btn btn-primary">Nuevo registro</asp:LinkButton>
                        </div>
                    </div>
                    <asp:Panel ID="pnlNuevoJerarquia" CssClass="row mt-4" runat="server">
                        <div class="col-4">
                            <div class="form-group">
                                <label>¿Que nivel desea ingresar?</label>
                                <asp:DropDownList ID="cmbNiveles" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-4 mt-2" id="pnlNvl1Reg" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNvl1Reg" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNvl1Reg" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4 mt-2" id="pnlNvl2Reg" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNvl2Reg" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNvl2Reg" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4 mt-2" id="pnlNvl3Reg" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNvl3Reg" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNvl3Reg" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4 mt-2" id="pnlNvl4Reg" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNvl4Reg" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNvl4Reg" class="form-control" runat="server" AutoComplete="Off" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4 mt-2" id="pnlNvl5Reg" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblNvl5Reg" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNvl5Reg" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-4" id="pnlSubNivel" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblSubNivel" runat="server" CssClass="labelAccionCua"></asp:Label>
                                <asp:DropDownList ID="cmbSubNivel" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <asp:Label ID="lblCodigo" runat="server" CssClass="labelAccionCua">Código</asp:Label>
                                <asp:TextBox TextMode="Number" ID="txtCodigo" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="form-group">
                                <asp:Label ID="lblNombre" runat="server" CssClass="labelAccionCua">Nombre</asp:Label>
                                <asp:TextBox ID="txtNombrePlanAcc" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Peso</label>
                                <asp:TextBox TextMode="Number" ID="txtPesoPlanAcc" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group my-4">
                                <asp:LinkButton ID="btnAgregarPlanAcc" runat="server" class="btn btn-primary">Grabar</asp:LinkButton>
                                <asp:LinkButton ID="btnCancelar" runat="server" class="btn btn-primary">Cancelar</asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>
                    <br />
                    <div class="row">
                        <div class="col-12 text-left">
                            <asp:LinkButton ID="btnFiltro" runat="server" class="btn btn-primary">Aplicar filtro</asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnlFiltro" runat="server">
                        <div class="row">
                            <div class="col-2">
                                <h5>Filtro</h5>
                            </div>
                            <div class="col-10">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>
                        </div>

                        <div class="row">
                            <asp:PlaceHolder ID="phDinamicControls" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="row">
                            <div class="col-12 mt-2 text-center">
                                <asp:LinkButton ID="btnConsultar" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-2 mt-2">
                                <h5>Resultados</h5>
                            </div>
                            <div class="col-10 mt-2">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>

                            <div class="col-12" style="overflow-x: auto; overflow-y: auto;">
                                <asp:GridView ID="tblPlanAccion" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="id" />
                                        <asp:BoundField DataField="level_id" HeaderText="Nivel" />
                                        <asp:BoundField DataField="code" HeaderText="Jerarquia" />
                                        <%-- <asp:TemplateField HeaderText="Jerarquia">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtJerarquia" class="form-control" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:BoundField DataField="name" HeaderText="Nombre" />
                                        <asp:TemplateField HeaderText="Nombre">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNombrePlanAcc" class="form-control" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="weigth" HeaderText="Peso" />
                                        <asp:TemplateField HeaderText="Peso">
                                            <ItemTemplate>
                                                <asp:TextBox TextMode="Number" ID="txtPeso" class="form-control" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditPlanAcc" runat="server" data-placement="top"
                                                    data-toggle="tooltip" Height="30px" Width="30px" CommandName="Editar"
                                                    Style="display: inline-grid" title="Editar plan de acción" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-edit"></i>                                        
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkConEditPlanAcc" runat="server" data-placement="top"
                                                    data-toggle="tooltip" Height="30px" Width="30px" CommandName="Confirmar"
                                                    Style="display: inline-grid" title="Confirmar" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-check"></i>                                   
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="lnkEliPlanAcc" runat="server" data-placement="top"
                                                    data-toggle="tooltip" Height="30px" Width="30px" CommandName="Eliminar"
                                                    Style="display: inline-grid" title="Eliminar plan de acción" class="btn btn-warning btn-circle">                                            
                                            <i class="fas fa-backspace"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </asp:Panel>
                    <br />
                    <div class="row">
                        <div class="col-12 text-center">
                            <asp:LinkButton ID="btnAtrasPlanAcc" runat="server" class="btn btn-primary">Atrás</asp:LinkButton>
                            <asp:LinkButton ID="btnSigPlanAcc" runat="server" class="btn btn-primary">Siguiente</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlMetas" CssClass="mt-2" runat="server">

            <div class="card mb-4 py-3 border-bottom-info" style="box-shadow: 4px 4px 8px #bdbdbd;">
                <div class="card-body">
                    <asp:Label ID="lblIdMeta" runat="server" CssClass="d-none"></asp:Label>
                    <div class="row">
                        <div class="col-12 text-left">
                            <asp:LinkButton ID="btnNuevoMeta" runat="server" class="btn btn-primary">Nuevo registro</asp:LinkButton>
                        </div>
                    </div>
                    <asp:Panel ID="pnlMetaNuevo" CssClass="row mt-4" runat="server">
                        <div class="col-12">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtNombreMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tipo de meta</label>
                                <asp:DropDownList ID="cmbTipoMeta" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>
                        <asp:PlaceHolder ID="phDinamicControlsMetaReg" runat="server"></asp:PlaceHolder>


                        <div class="col-2">
                            <div class="form-group">
                                <label>Linea Base</label>
                                <asp:TextBox TextMode="Number" ID="txtLineaBaseMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-1">
                            <div class="form-group">
                                <label>Peso</label>
                                <asp:TextBox TextMode="Number" ID="txtPesoMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-3 mt-4">
                                <h4>Metas Anuales</h4>
                            </div>
                            <div class="col-9 mt-4">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Primer año</label>
                                    <asp:TextBox TextMode="Number" ID="txtPriYearMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Segundo año</label>
                                    <asp:TextBox TextMode="Number" ID="txtSegYearMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Tercer año</label>
                                    <asp:TextBox TextMode="Number" ID="txtTerYearMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Cuarto año</label>
                                    <asp:TextBox TextMode="Number" ID="txtCuaYearMeta" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-3 mt-4">
                                <h4>Metas financieras</h4>
                            </div>
                            <div class="col-9 mt-4">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblPriPresupuesto" runat="server" Text=""></asp:Label>                                    
                                    <asp:TextBox ID="txtPriPresupuesto" class="form-control mt-2" runat="server" AutoComplete="Off" onkeyup="validarDinero(this)" onclick="validarNumero(this)" onblur="validarDinero(this)" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblSegPresupuesto" runat="server" Text=""></asp:Label>                                    
                                    <asp:TextBox ID="txtSegPresupuesto" class="form-control mt-2" runat="server" AutoComplete="Off" onkeyup="validarDinero(this)" onclick="validarNumero(this)" onblur="validarDinero(this)" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblTerPresupuesto" runat="server" Text=""></asp:Label>     
                                    <asp:TextBox ID="txtTerPresupuesto" class="form-control mt-2" runat="server" AutoComplete="Off" onkeyup="validarDinero(this)" onclick="validarNumero(this)" onblur="validarDinero(this)" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <asp:Label ID="lblCuarPresupuesto" runat="server" Text=""></asp:Label>     
                                    <asp:TextBox ID="txtCuarPresupuesto" class="form-control mt-2" runat="server" AutoComplete="Off" onkeyup="validarDinero(this)" onclick="validarNumero(this)" onblur="validarDinero(this)" AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label>Acumulado presupuestal</label>
                                    <asp:TextBox ID="txtAcumulado" class="form-control" runat="server" AutoComplete="Off" onkeyup="validarDinero(this)" onclick="validarNumero(this)" onblur="validarDinero(this)" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-3 mt-4">
                            <h4>Responsables</h4>
                        </div>
                        <div class="col-9 mt-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>

                        <div class="col-6 mt-3">
                            <div class="form-group">
                                <label>Responsable</label>
                                <asp:DropDownList ID="cmbResponsable" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-6 mt-3">
                            <div class="form-group">
                                <label>Alimentador del PAC</label>
                                <asp:DropDownList ID="cmbAlimentador" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-12 text-center mt-1">
                            <asp:LinkButton ID="btnGrabarMetas" runat="server" class="btn btn-primary">Grabar Meta</asp:LinkButton>
                            <asp:LinkButton ID="btnCancelarMeta" runat="server" class="btn btn-primary">Cancelar</asp:LinkButton>
                        </div>

                    </asp:Panel>
                    <br />
                    <div class="row">
                        <div class="col-12 text-left">
                            <asp:LinkButton ID="btnFiltroMeta" runat="server" class="btn btn-primary">Aplicar filtro</asp:LinkButton>
                        </div>
                    </div>
                    <br />

                    <asp:Panel ID="pnlFiltroMeta" runat="server">

                        <div class="row">
                            <div class="col-2">
                                <h5>Filtro</h5>
                            </div>
                            <div class="col-10">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>
                        </div>

                        <div class="row">
                            <asp:PlaceHolder ID="phDinamicControlsMeta" runat="server"></asp:PlaceHolder>
                        </div>
                        <div class="row">
                            <div class="col-12 mt-2 text-center">
                                <asp:LinkButton ID="btnConsultarMeta" runat="server" class="btn btn-primary">Consultar</asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-2 mt-2">
                                <h5>Resultados</h5>
                            </div>
                            <div class="col-10 mt-2">
                                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                            </div>

                            <div class="col-12 mt-4" style="overflow-x: auto; overflow-y: auto;">
                                <asp:GridView ID="tblMetas" runat="server" CssClass="table" Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="id" />
                                        <asp:BoundField DataField="type_goal" HeaderText="Tipo Meta" />
                                        <asp:BoundField DataField="subactivity" HeaderText="Sub Activ." />
                                        <asp:BoundField DataField="name" HeaderText="Nombre" />
                                        <asp:BoundField DataField="line_base" HeaderText="Linea Base" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200px" HeaderText="Acciones">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditarMeta" runat="server" data-placement="top"
                                                    data-toggle="tooltip" Height="30px" Width="30px" CommandName="Editar"
                                                    Style="display: inline-grid" title="Editar meta" class="btn btn-success btn-circle">                                            
                                            <i class="fas fa-edit"></i>                                        
                                                </asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:Panel>

                    <br />
                    <div class="row">
                        <div class="col-12 text-center mt-1">
                            <asp:LinkButton ID="btnAtrasMetas" runat="server" class="btn btn-primary">Atras</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>
        <asp:LinkButton ID="eliminarNivel" runat="server" CssClass="d-none"></asp:LinkButton>
        <asp:LinkButton ID="eliminarPlanAcc" runat="server" CssClass="d-none"></asp:LinkButton>
    </div>

    <div class="modal fade" id="mdlEditarMeta" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Editar Meta</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-12">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtNombreMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <label>Tipo de meta</label>
                                <asp:DropDownList ID="cmbTipoMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-6">
                            <div class="form-group">
                                <asp:Label ID="lblNivelMetaMdl" runat="server"></asp:Label>
                                <asp:DropDownList ID="cmbNivelMetaMdl" class="form-control mt-2" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Linea Base</label>
                                <asp:TextBox TextMode="Number" ID="txtLineaBaseMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Primer año</label>
                                <asp:TextBox TextMode="Number" ID="txtPriYearMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Segundo año</label>
                                <asp:TextBox TextMode="Number" ID="txtSegYearMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Tercer año</label>
                                <asp:TextBox TextMode="Number" ID="txtTercYearMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-2">
                            <div class="form-group">
                                <label>Cuarto año</label>
                                <asp:TextBox TextMode="Number" ID="txtCuartYearMetaMdl" class="form-control" runat="server" AutoComplete="Off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-3 mt-4">
                            <h4>Responsables</h4>
                        </div>
                        <div class="col-9 mt-4">
                            <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
                        </div>

                        <div class="col-6 mt-3">
                            <div class="form-group">
                                <label>Responsable</label>
                                <asp:DropDownList ID="cmbResponsableMdl" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-6 mt-3">
                            <div class="form-group">
                                <label>Alimentador del PAC</label>
                                <asp:DropDownList ID="cmbAlimentadorMdl" class="form-control" runat="server" AutoComplete="Off"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-12 text-center mt-1 mb-3">
                            <asp:LinkButton ID="btnActualizarMeta" runat="server" class="btn btn-primary">Actualizar Meta</asp:LinkButton>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .labelAccionCua {
            display: inline-block;
            margin-bottom: .5rem;
        }

        .espacioBtnAlerta {
            margin-right: 40px;
        }

        .btnAmarillo {
            background-color: #e5cd5d;
            border-color: #F6DB5E;
            color: white;
        }
    </style>

    <script type="text/javascript">
        $(window).on('load', function () {
            $('#1').addClass("MnuActive");
        });

        function abrirModal() {
            $(window).on('load', function () {
                $('#mdlEditarMeta').modal('show');
            });
        };

        function AlertaEliminacionNivel() {
            const swalWithBootstrapButtons = swal.mixin({
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger espacioBtnAlerta',
                buttonsStyling: false,
            })
            swalWithBootstrapButtons({
                title: 'Verificación',
                text: "¿Desea eliminar el nivel?",
                type: 'info',
                showCancelButton: true,
                confirmButtonText: 'Si',
                cancelButtonText: 'No',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    document.getElementById('contenedor2_eliminarNivel').click();
                } else if (
                    // Read more about handling dismissals
                    result.dismiss === swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons(
                        'Has cancelado la eliminación del nivel',
                        '',
                        'error'
                    )
                }
            })
        };

        function AlertaEliminacionPlanAcc() {
            const swalWithBootstrapButtons = swal.mixin({
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger espacioBtnAlerta',
                buttonsStyling: false,
            })
            swalWithBootstrapButtons({
                title: 'Verificación',
                text: "¿Desea eliminar el plan acción cuatrienal?",
                type: 'info',
                showCancelButton: true,
                confirmButtonText: 'Si',
                cancelButtonText: 'No',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    document.getElementById('contenedor2_eliminarPlanAcc').click();
                } else if (
                    // Read more about handling dismissals
                    result.dismiss === swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons(
                        'Has cancelado la eliminación del plan de acción cuatrienal',
                        '',
                        'error'
                    )
                }
            })
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
        function validarDinero(campo) {
            var tar = campo.value;
            var v = '$' + number_format(tar);
            campo.value = v;
        };
        function number_format(amount, decimals) {
            amount += ''; // por si pasan un numero en vez de un string
            amount = parseFloat(amount.replace(/[^0-9\.\-]/g, '')); // elimino cualquier cosa que no sea numero o punto
            decimals = decimals || 0; // por si la variable no fue fue pasada
            // si no es un numero o es igual a cero retorno el mismo cero
            if (isNaN(amount) || amount === 0)
                return parseFloat(0).toFixed(decimals);
            // si es mayor o menor que cero retorno el valor formateado como numero
            amount = '' + amount.toFixed(decimals);
            var amount_parts = amount.split('.'),
                regexp = /(\d+)(\d{3})/;
            while (regexp.test(amount_parts[0]))
                amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');
            return amount_parts.join('.');
        };



        window.onload = function () {
            var pos = window.name || 0;
            window.scrollTo(0, pos);
        }
        window.onunload = function () {
            window.name = self.pageYOffset || (document.documentElement.scrollTop + document.body.scrollTop);
        }
    </script>
</asp:Content>

