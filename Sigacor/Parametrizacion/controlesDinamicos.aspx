<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="controlesDinamicos.aspx.vb" Inherits="Sigacor.controlesDinamicos" Culture="en-US" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sigacor</title>


    <link href="Componentes/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />
    <link href="../Componentes/css/sb-admin-2.css" rel="stylesheet" />
    <script src="../Componentes/sweetAlert2/sweetalert2.all.min.js"></script>
    <%--<script src="Componentes/vendor/jquery/jquery.min.js"></script>--%>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>

</head>
<body>
    <form id="form1" runat="server">

        <div class="row">
            <div class="col-2">
                <h5>Filtro</h5>
            </div>
            <div class="col-10">
                <hr style="border-top: 3px solid rgba(0, 0, 0, .1);" />
            </div>
            <br />
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
                            </ItemTemplate>
                            <ItemStyle Width="20%" VerticalAlign="Middle" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>


        <asp:Label ID="lblError" runat="server" Text="lblError" Style="color: red;"></asp:Label>
    </form>
    <script src="../Componentes/vendor/bootstrap/js/bootstrap.bundle.js"></script>
    <script src="../Componentes/js/sb-admin-2.min.js"></script>
</body>
</html>
