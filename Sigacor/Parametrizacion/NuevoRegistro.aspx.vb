Public Class NuevoRegistro
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim report As New clReportPac
    Dim users As New clLogin
    Dim fun As New Funciones

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlPac.Visible = True
            pestaña(1)
            pnlSubNivel.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
            lblError.Visible = False
            btnActPac.Visible = False
            btnFiltro.Visible = False
            pnlNuevoJerarquia.Visible = False
            pnlMetaNuevo.Visible = False
            btnFiltroMeta.Visible = False

            pnlNvl1Reg.Visible = False
            pnlNvl2Reg.Visible = False
            pnlNvl3Reg.Visible = False
            pnlNvl4Reg.Visible = False
            pnlNvl5Reg.Visible = False

            Session("Actualizar") = "N"

            DataT = Nothing
            DataT = users.selectUsuario
            If DataT.Rows.Count > 0 Then
                cmbAlimentador.Items.Clear()
                cmbAlimentador.DataTextField = "nombreEmp"
                cmbAlimentador.DataValueField = "user_id"
                cmbAlimentador.DataSource = DataT
                cmbAlimentador.DataBind()
                cmbAlimentador.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbAlimentadorMdl.Items.Clear()
                cmbAlimentadorMdl.DataTextField = "nombreEmp"
                cmbAlimentadorMdl.DataValueField = "user_id"
                cmbAlimentadorMdl.DataSource = DataT
                cmbAlimentadorMdl.DataBind()
                cmbAlimentadorMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            DataT = Nothing
            DataT = parametrizacion.selectComdepndnc
            If DataT.Rows.Count > 0 Then
                cmbResponsable.Items.Clear()
                cmbResponsable.DataTextField = "nombr_depndnc"
                cmbResponsable.DataValueField = "codg_depndnc"
                cmbResponsable.DataSource = DataT
                cmbResponsable.DataBind()
                cmbResponsable.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbResponsableMdl.Items.Clear()
                cmbResponsableMdl.DataTextField = "nombr_depndnc"
                cmbResponsableMdl.DataValueField = "codg_depndnc"
                cmbResponsableMdl.DataSource = DataT
                cmbResponsableMdl.DataBind()
                cmbResponsableMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            DataT = Nothing
            DataT = fun.goal_type
            If DataT.Rows.Count > 0 Then
                cmbTipoMeta.Items.Clear()
                cmbTipoMeta.DataTextField = "description"
                cmbTipoMeta.DataValueField = "name"
                cmbTipoMeta.DataSource = DataT
                cmbTipoMeta.DataBind()
                cmbTipoMeta.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbTipoMetaMdl.Items.Clear()
                cmbTipoMetaMdl.DataTextField = "description"
                cmbTipoMetaMdl.DataValueField = "name"
                cmbTipoMetaMdl.DataSource = DataT
                cmbTipoMetaMdl.DataBind()
                cmbTipoMetaMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
            End If

            visualizarPac(Session("id_pac"))
            Session("id_pac") = Nothing
            Session("pac") = Nothing

        End If

        If tblNiveles.Rows.Count > 0 Then
            tblNiveles.UseAccessibleHeader = True
            tblNiveles.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        If tblPlanAccion.Rows.Count > 0 Then
            tblPlanAccion.UseAccessibleHeader = True
            tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
        If tblMetas.Rows.Count > 0 Then
            tblMetas.UseAccessibleHeader = True
            tblMetas.HeaderRow.TableSection = TableRowSection.TableHeader
        End If

    End Sub

#End Region

#Region "TextChanged"
    Private Sub txtYearInicial_TextChanged(sender As Object, e As EventArgs) Handles txtYearInicial.TextChanged
        calcularYearFinal()
        txtCantYears.Focus()
    End Sub

    Private Sub txtCantYears_TextChanged(sender As Object, e As EventArgs) Handles txtCantYears.TextChanged
        calcularYearFinal()
        txtYearFinal.Focus()
    End Sub

#End Region

#Region "SelectedIndexChanged"


    Private Sub cmbNiveles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiveles.SelectedIndexChanged
        Try

            If cmbNiveles.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el nivel que desea ingresar", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If

            cmbNvl1Reg.Items.Clear()
            pnlNvl1Reg.Visible = False
            cmbNvl2Reg.Items.Clear()
            pnlNvl2Reg.Visible = False
            cmbNvl3Reg.Items.Clear()
            pnlNvl3Reg.Visible = False
            cmbNvl4Reg.Items.Clear()
            pnlNvl4Reg.Visible = False
            cmbNvl5Reg.Items.Clear()
            pnlNvl5Reg.Visible = False

            If CInt(1 < cmbNiveles.SelectedValue) Then
                DataT = Nothing
                DataT = report.selectContentsFiltro(lblPac.Text.Trim, "", "1")
                If DataT.Rows.Count > 0 Then
                    lblNvl1Reg.Text = "¿A que " & DataT(0)(3) & " pertenece?"
                    pnlNvl1Reg.Visible = True

                    cmbNvl1Reg.Items.Clear()
                    cmbNvl1Reg.DataTextField = "name"
                    cmbNvl1Reg.DataValueField = "code"
                    cmbNvl1Reg.DataSource = DataT
                    cmbNvl1Reg.DataBind()
                    cmbNvl1Reg.Items.Insert(0, New ListItem("---Seleccione--", ""))
                    cmbNvl1Reg.Focus()
                Else
                    txtNombrePlanAcc.Focus()
                End If
            End If

            lblCodigo.Text = "Código de " & cmbNiveles.SelectedItem.ToString
            lblNombre.Text = "Nombre de " & cmbNiveles.SelectedItem.ToString
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl1Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl1Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(2 < cmbNiveles.SelectedValue) Then
                If cmbNvl1Reg.SelectedIndex = 0 Then
                    cmbNvl2Reg.Items.Clear()
                    pnlNvl2Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl1Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl2Reg.Items.Clear()
                        cmbNvl2Reg.DataTextField = "name"
                        cmbNvl2Reg.DataValueField = "code"
                        cmbNvl2Reg.DataSource = DataT
                        cmbNvl2Reg.DataBind()
                        cmbNvl2Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl2Reg.Text = DataT(0)(2)
                        pnlNvl2Reg.Visible = True
                        cmbNvl2Reg.Focus()
                    Else
                        cmbNvl2Reg.Items.Clear()
                        pnlNvl2Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado programas", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl2Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl2Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(3 < cmbNiveles.SelectedValue) Then
                If cmbNvl2Reg.SelectedIndex = 0 Then
                    cmbNvl3Reg.Items.Clear()
                    pnlNvl3Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl2Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl3Reg.Items.Clear()
                        cmbNvl3Reg.DataTextField = "name"
                        cmbNvl3Reg.DataValueField = "code"
                        cmbNvl3Reg.DataSource = DataT
                        cmbNvl3Reg.DataBind()
                        cmbNvl3Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl3Reg.Text = DataT(0)(2)
                        pnlNvl3Reg.Visible = True
                        cmbNvl3Reg.Focus()
                    Else
                        cmbNvl3Reg.Items.Clear()
                        pnlNvl3Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Proyectos", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNvl3Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl3Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(4 < cmbNiveles.SelectedValue) Then
                If cmbNvl3Reg.SelectedIndex = 0 Then
                    cmbNvl4Reg.Items.Clear()
                    pnlNvl4Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl3Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl4Reg.Items.Clear()
                        cmbNvl4Reg.DataTextField = "name"
                        cmbNvl4Reg.DataValueField = "code"
                        cmbNvl4Reg.DataSource = DataT
                        cmbNvl4Reg.DataBind()
                        cmbNvl4Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl4Reg.Text = DataT(0)(2)
                        pnlNvl4Reg.Visible = True
                        cmbNvl4Reg.Focus()
                    Else
                        cmbNvl4Reg.Items.Clear()
                        pnlNvl4Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Actividades", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNvl4Reg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNvl4Reg.SelectedIndexChanged
        Try

            DataT = Nothing
            If CInt(5 < cmbNiveles.SelectedValue) Then
                If cmbNvl4Reg.SelectedIndex = 0 Then
                    cmbNvl5Reg.Items.Clear()
                    pnlNvl5Reg.Visible = False
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNvl4Reg.SelectedValue)
                    If DataT.Rows.Count > 0 Then
                        cmbNvl5Reg.Items.Clear()
                        cmbNvl5Reg.DataTextField = "name"
                        cmbNvl5Reg.DataValueField = "code"
                        cmbNvl5Reg.DataSource = DataT
                        cmbNvl5Reg.DataBind()
                        cmbNvl5Reg.Items.Insert(0, New ListItem("---Seleccione---", ""))
                        lblNvl5Reg.Text = DataT(0)(2)
                        pnlNvl5Reg.Visible = True
                        cmbNvl5Reg.Focus()
                    Else
                        cmbNvl5Reg.Items.Clear()
                        pnlNvl5Reg.Visible = False
                        alerta("Advertencia", "No se han encontrado Actividades", "info")
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


#End Region

#Region "RowDataBound"
    Private Sub tblPlanAccion_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblPlanAccion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False

                Dim linkBtnEditar, linkBtnEliminar, linkBtnConfirmar As New LinkButton
                linkBtnEditar = e.Row.FindControl("lnkEditPlanAcc")
                linkBtnEliminar = e.Row.FindControl("lnkEliPlanAcc")
                linkBtnConfirmar = e.Row.FindControl("lnkConEditPlanAcc")
                linkBtnEditar.CommandArgument = e.Row.Cells(0).Text.Trim
                linkBtnEliminar.CommandArgument = e.Row.Cells(2).Text.Trim
                linkBtnConfirmar.Visible = False

            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(4).Visible = False
                e.Row.Cells(6).Visible = False                
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblNiveles_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblNiveles.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).Visible = False

                Dim linkBtnEditar, linkBtnEliminar, linkBtnConfirmar As New LinkButton
                linkBtnEditar = e.Row.FindControl("lnkEditNiv")
                linkBtnConfirmar = e.Row.FindControl("lnkConEdit")
                linkBtnEliminar = e.Row.FindControl("lnkEliminar")

                linkBtnEditar.CommandArgument = e.Row.Cells(0).Text.Trim
                linkBtnEliminar.CommandArgument = e.Row.Cells(0).Text.Trim
                linkBtnConfirmar.Visible = False

                Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, "hierarchy desc")
                If Fila IsNot Nothing Then
                    If Fila("id") = e.Row.Cells(0).Text.Trim Then
                        linkBtnEliminar.Visible = True
                    Else
                        linkBtnEliminar.Visible = False
                    End If
                End If
                End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub tblMetas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblMetas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn, linkBtn2 As New LinkButton
                linkBtn = e.Row.FindControl("lnkEditarMeta")
                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
                e.Row.Cells(0).Visible = False
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "RowCommand"
    Private Sub tblNiveles_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblNiveles.RowCommand
        Try

            Dim linkBtnConfirmar, linkBtnEditar, linkBtnEliminar As New LinkButton
            Dim nombre As TextBox

            If e.CommandName = "Editar" Then
                For Each row As GridViewRow In tblNiveles.Rows

                    linkBtnConfirmar = tblNiveles.Rows(row.RowIndex).FindControl("lnkConEdit")
                    linkBtnEditar = tblNiveles.Rows(row.RowIndex).FindControl("lnkEditNiv")
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(2).Text.Trim)

                        row.Cells(2).Visible = False
                        row.Cells(3).Visible = True
                    Else
                        linkBtnEditar.Visible = False
                        linkBtnEliminar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblNiveles.Rows
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If row.Cells(3).Visible = True Then
                        If nombre.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el nombre del nivel", "info")
                            Exit Sub
                        End If
                        parametrizacion.updateLevels(row.Cells(0).Text.Trim, nombre.Text.Trim, row.Cells(1).Text.Trim, "A")
                    End If
                Next

                cargarNiveles(lblPac.Text.Trim)
                alerta("Se ha actualizado el nivel correctamente", "", "success")

            ElseIf e.CommandName = "Eliminar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaNivel", "AlertaEliminacionNivel();", True)
                Session("idNivel") = e.CommandArgument
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblPlanAccion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblPlanAccion.RowCommand
        Try

            Dim linkBtnConfirmar, linkBtnEditar, linkBtnEliminar As New LinkButton
            Dim nombre, peso As TextBox

            If e.CommandName = "Editar" Then
                For Each row As GridViewRow In tblPlanAccion.Rows

                    linkBtnConfirmar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkConEditPlanAcc")
                    linkBtnEditar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEditPlanAcc")
                    linkBtnEliminar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEliPlanAcc")

                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(3).Text.Trim)
                        peso.Text = row.Cells(5).Text.Trim

                        row.Cells(3).Visible = False
                        row.Cells(4).Visible = True

                        row.Cells(5).Visible = False
                        row.Cells(6).Visible = True
                    Else
                        linkBtnEditar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblPlanAccion.Rows
                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If row.Cells(4).Visible = True Then
                        If nombre.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el nombre del plan de acción cuatrienal", "info")
                            Exit Sub
                        End If
                        If peso.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el peso del plan de acción cuatrienal", "info")
                            Exit Sub
                        End If
                        parametrizacion.updateContents(row.Cells(0).Text.Trim, row.Cells(2).Text.Trim, nombre.Text.Trim, peso.Text.Trim)
                        Exit For
                    End If
                Next

                'cargarPlanAccion(lblPac.Text.Trim)
                btnConsultar_Click(Nothing, Nothing)
                alerta("Se ha actualizado el plan de acción cuatrienal correctamente", "", "success")

            ElseIf e.CommandName = "Eliminar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaPlanAcc", "AlertaEliminacionPlanAcc();", True)
                Session("CodePlanAcc") = e.CommandArgument
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblMetas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblMetas.RowCommand
        Try
            lblIdMeta.Text = e.CommandArgument

            If e.CommandName = "Editar" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModal();", True)
                Fila = parametrizacion.selectGoals(lblPac.Text.Trim, lblIdMeta.Text)
                If Fila IsNot Nothing Then
                    txtNombreMetaMdl.Text = Fila("name")
                    cmbTipoMetaMdl.SelectedValue = Fila("type_goal")
                    cmbNivelMetaMdl.SelectedValue = Fila("subactivity")
                    txtLineaBaseMetaMdl.Text = Fila("line_base")
                    txtPriYearMetaMdl.Text = Fila("value_one_year")
                    txtSegYearMetaMdl.Text = Fila("value_two_year")
                    txtTercYearMetaMdl.Text = Fila("value_three_year")
                    txtCuartYearMetaMdl.Text = Fila("value_four_year")
                    cmbResponsableMdl.SelectedValue = Fila("responsable_id")
                    cmbAlimentadorMdl.SelectedValue = Fila("feeder_id")
                End If

            ElseIf e.CommandName = "Eliminar" Then
                parametrizacion.updateStateGoals(lblIdMeta.Text.Trim)
                btnConsultarMeta_Click(Nothing, Nothing)
                'cargarMetas(lblPac.Text.Trim, 0)
                alerta("Se ha eliminado la meta correctamente", "", "success", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Click"

#Region "Pestañas"
    Private Sub btnPac_Click(sender As Object, e As EventArgs) Handles btnPac.Click
        Try
            pnlPac.Visible = True
            pestaña(1)
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnNiveles_Click(sender As Object, e As EventArgs) Handles btnNiveles.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnPlanAccion_Click(sender As Object, e As EventArgs) Handles btnPlanAccion.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnMetas_Click(sender As Object, e As EventArgs) Handles btnMetas.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El pac no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If

            pestaña(4)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            pnlMetas.Visible = True
            cargarMetas(lblPac.Text.Trim, 1)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region
    Private Sub btnSigPac_Click(sender As Object, e As EventArgs) Handles btnSigPac.Click
        Try
            If Session("Actualizar") = "N" Then
                DataT = Nothing
                DataT = parametrizacion.selectPac
                If DataT.Rows.Count > 0 Then
                    alerta("Advertencia", "No se puede grabar el pac, se encuentra un pac activo", "info", "")
                    Exit Sub
                End If
                DataT = Nothing
                DataT = parametrizacion.selectPacPeriodo(txtYearInicial.Text.Trim, txtYearFinal.Text.Trim)
                If DataT.Rows.Count > 0 Then
                    alerta("Advertencia", "No se puede grabar el pac, el periodo ya fue cerrado", "info", "")
                    Exit Sub
                End If
            End If

            Session("Actualizar") = "N"

            If txtNomPac.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre el PAC", "info", "contenedor2_txtNomPac")
                Exit Sub
            End If
            If txtYearInicial.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el año inicial", "info", "contenedor2_txtYearInicial")
                Exit Sub
            End If
            If txtCantYears.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad de años", "info", "contenedor2_txtCantYears")
                Exit Sub
            End If

            If parametrizacion.updatePac(txtNomPac.Text.Trim, txtYearInicial.Text.Trim,
                                         txtYearFinal.Text.Trim, txtCantYears.Text.Trim,
                                         "A", lblPac.Text.Trim) > 0 Then

                alerta("Se ha actualizado el pac correctamente", "PAC:  " & lblPac.Text.Trim, "success", "")
            Else
                parametrizacion.insertPac(txtNomPac.Text.Trim, txtYearInicial.Text.Trim,
                                          txtYearFinal.Text.Trim, txtCantYears.Text.Trim, "A")
                lblPac.Text = parametrizacion.consecutivoPac
                alerta("Se ha creado el PAC correctamente", "Pac:  " & lblPac.Text.Trim, "success", "")
            End If

            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnActPac_Click(sender As Object, e As EventArgs) Handles btnActPac.Click
        Session("Actualizar") = "S"
        btnSigPac_Click(Nothing, Nothing)
    End Sub

    Private Sub btnSigNiveles_Click(sender As Object, e As EventArgs) Handles btnSigNiveles.Click
        Try
            cargarNiveles(lblPac.Text.Trim)

            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasNiveles_Click(sender As Object, e As EventArgs) Handles btnAtrasNiveles.Click
        Try
            btnPac_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasPlanAcc_Click(sender As Object, e As EventArgs) Handles btnAtrasPlanAcc.Click
        Try
            pestaña(2)
            pnlPac.Visible = False
            pnlNiveles.Visible = True
            pnlPlanAccion.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtrasMetas_Click(sender As Object, e As EventArgs) Handles btnAtrasMetas.Click
        Try
            pestaña(3)
            pnlPac.Visible = False
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = True
            pnlMetas.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnSigPlanAcc_Click(sender As Object, e As EventArgs) Handles btnSigPlanAcc.Click
        Try
            If lblPac.Text = String.Empty Then
                alerta("Advertencia", "El PAC no esta registrado", "info", "")
                btnPac_Click(Nothing, Nothing)
                Exit Sub
            End If
            If tblPlanAccion.Rows.Count = 0 Then
                alerta("Advertencia", "Ingrese la parametrización de plan de acción cuatrienal", "info", "")
                Exit Sub
            End If

            btnMetas_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim hierarchy As Integer = 1
            If txtNombreNiv.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre del nivel", "info", "contenedor2_txtNombreNiv")
                Exit Sub
            End If

            DataT = Nothing
            DataT = parametrizacion.selectLevels(lblPac.Text.Trim, "hierarchy desc")
            If DataT.Rows.Count > 0 Then
                hierarchy = CInt(DataT(0)(3)) + 1
            End If

            If parametrizacion.insertLevels(txtNombreNiv.Text.Trim, lblPac.Text.Trim, hierarchy, "A") > 0 Then
                cargarNiveles(lblPac.Text.Trim)
                txtNombreNiv.Text = String.Empty
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAgregarPlanAcc_Click(sender As Object, e As EventArgs) Handles btnAgregarPlanAcc.Click
        Try
            Dim code, array As String
            Dim subNivel, name As String
            If cmbNiveles.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione un nivel", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If

            If pnlNvl1Reg.Visible = True And cmbNvl1Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl1Reg.Text, "info", "contenedor2_cmbNvl1Reg")
                Exit Sub
            Else
                If cmbNvl1Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl1Reg.SelectedValue
                End If
            End If
            If pnlNvl2Reg.Visible = True And cmbNvl2Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el " & lblNvl2Reg.Text, "info", "contenedor2_cmbNvl2Reg")
                Exit Sub
            Else
                If cmbNvl2Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl2Reg.SelectedValue
                End If
            End If
            If pnlNvl3Reg.Visible = True And cmbNvl3Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el " & lblNvl3Reg.Text, "info", "contenedor2_cmbNvl3Reg")
                Exit Sub
            Else
                If cmbNvl3Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl3Reg.SelectedValue
                End If
            End If
            If pnlNvl4Reg.Visible = True And cmbNvl4Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl4Reg.Text, "info", "contenedor2_cmbNvl4Reg")
                Exit Sub
            Else
                If cmbNvl4Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl4Reg.SelectedValue
                End If
            End If
            If pnlNvl5Reg.Visible = True And cmbNvl5Reg.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la " & lblNvl5Reg.Text, "info", "contenedor2_cmbNvl5Reg")
                Exit Sub
            Else
                If cmbNvl5Reg.SelectedIndex > 0 Then
                    subNivel = cmbNvl5Reg.SelectedValue
                End If
            End If

            If txtCodigo.Text = String.Empty Then
                alerta("Advertencia", "Ingrese un codigo código", "info", "contenedor2_cmbNiveles")
                Exit Sub
            End If
            If txtNombrePlanAcc.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre", "info", "contenedor2_txtNombrePlanAcc")
                Exit Sub
            End If
            If txtPesoPlanAcc.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el peso", "info", "contenedor2_txtPesoPlanAcc")
                Exit Sub
            End If

            If subNivel <> String.Empty Then
                code = subNivel & "." & txtCodigo.Text.Trim
            Else
                code = txtCodigo.Text.Trim
            End If

            DataT = parametrizacion.selectContents(lblPac.Text.Trim, code)
            If DataT.Rows.Count > 0 Then
                alerta("Advertencia", "Jerarquia " & code & " ya existe", "info")
                Exit Sub
            End If


            If parametrizacion.insertContents(lblPac.Text.Trim, cmbNiveles.SelectedValue, code, cmbNiveles.SelectedItem.ToString.Trim, subNivel,
                                           txtNombrePlanAcc.Text.Trim, txtPesoPlanAcc.Text.Trim, "A", array) > 0 Then

                txtCodigo.Text = String.Empty
                txtNombrePlanAcc.Text = String.Empty
                txtPesoPlanAcc.Text = String.Empty
                limiarFiltroRegistro()
                cmbNiveles_SelectedIndexChanged(Nothing, Nothing)
                alerta("Se ha creado el item correctamente", "", "success")
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnGrabarMetas_Click(sender As Object, e As EventArgs) Handles btnGrabarMetas.Click
        Try
            Dim lastRow As DataRow
            Dim code As String
            If txtNombreMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre de la meta", "info", "contenedor2_txtNombreMeta")
                Exit Sub
            End If
            If cmbTipoMeta.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el tipo de meta", "info", "contenedor2_cmbTipoMeta")
                Exit Sub
            End If

            If (QuantityDinamicControlsMetaReg.Rows.Count > 0) Then
                lastRow = QuantityDinamicControlsMetaReg.Rows(QuantityDinamicControlsMetaReg.Rows.Count - 1)
                If lastRow IsNot Nothing Then
                    Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, "hierarchy desc")
                    If Fila IsNot Nothing And CInt(lastRow("level")) > CInt(Fila("hierarchy")) Then
                        code = lastRow("sublevel")
                    Else
                        alerta("Advertencia", "Seleccione la jerarquía completa", "info", "")
                        Exit Sub
                    End If
                End If
            End If

            If txtPesoMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el peso de la meta", "info", "contenedor2_txtPesoMeta")
                Exit Sub
            End If

            If txtLineaBaseMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la linea base", "info", "contenedor2_txtLineaBaseMeta")
                Exit Sub
            End If

            If txtPriYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el primer año", "info", "contenedor2_txtPriYearMeta")
                Exit Sub
            End If
            If txtSegYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el segundo año", "info", "contenedor2_txtSegYearMeta")
                Exit Sub
            End If
            If txtTerYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el tercer año", "info", "contenedor2_txtTerYearMeta")
                Exit Sub
            End If
            If txtCuaYearMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el cuarto año", "info", "contenedor2_txtCuaYearMeta")
                Exit Sub
            End If

            If txtPriPresupuesto.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el " & lblPriPresupuesto.Text.Trim, "info", "contenedor2_txtPriPresupuesto")
                Exit Sub
            End If
            If txtSegPresupuesto.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el " & lblSegPresupuesto.Text.Trim, "info", "contenedor2_txtSegPresupuesto")
                Exit Sub
            End If
            If txtTerPresupuesto.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el " & lblTerPresupuesto.Text.Trim, "info", "contenedor2_txtTerPresupuesto")
                Exit Sub
            End If
            If txtCuarPresupuesto.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la cantidad para el " & lblCuarPresupuesto.Text.Trim, "info", "contenedor2_txtCuarPresupuesto")
                Exit Sub
            End If

            If cmbResponsable.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el responsable", "info", "contenedor2_cmbResponsable")
                Exit Sub
            End If
            If cmbAlimentador.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el alimentador", "info", "contenedor2_cmbAlimentador")
                Exit Sub
            End If

            If parametrizacion.insertGoals(lblPac.Text.Trim, txtNombreMeta.Text.Trim, cmbTipoMeta.SelectedValue,
                                           code, txtLineaBaseMeta.Text.Trim, txtPriYearMeta.Text.Trim,
                                           txtSegYearMeta.Text.Trim, txtTerYearMeta.Text.Trim, txtCuaYearMeta.Text.Trim,
                                           cmbResponsable.SelectedValue, cmbAlimentador.SelectedValue, "A", txtPesoMeta.Text.Trim,
                                           EliminarFormato(txtPriPresupuesto.Text.Trim), EliminarFormato(txtSegPresupuesto.Text.Trim),
                                           EliminarFormato(txtTerPresupuesto.Text.Trim), EliminarFormato(txtCuarPresupuesto.Text.Trim)) > 0 Then

                alerta("Se ha creado correctamente la meta", "", "success", "")
                limpiarMetas()
            Else
                alerta("Advertencia", "Se genero un error al grabar", "error", "")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnActualizarMeta_Click(sender As Object, e As EventArgs) Handles btnActualizarMeta.Click
        Try
            If txtNombreMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese el nombre de la meta", "info", "mdlEditarMeta", "contenedor2_txtNombreMetaMdl")
                Exit Sub
            End If
            If cmbTipoMetaMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el tipo de meta", "info", "mdlEditarMeta", "contenedor2_cmbTipoMetaMdl")
                Exit Sub
            End If
            If cmbNivelMetaMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione la el nivel del la meta", "info", "mdlEditarMeta", "contenedor2_cmbNivelMetaMdl")
                Exit Sub
            End If
            If txtLineaBaseMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la linea base", "info", "mdlEditarMeta", "contenedor2_txtLineaBaseMetaMdl")
                Exit Sub
            End If
            If txtPriYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el primer año", "info", "mdlEditarMeta", "contenedor2_txtPriYearMetaMdl")
                Exit Sub
            End If
            If txtSegYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el segundo año", "info", "mdlEditarMeta", "contenedor2_txtSegYearMetaMdl")
                Exit Sub
            End If
            If txtTercYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el tercer año", "info", "mdlEditarMeta", "contenedor2_txtTercYearMetaMdl")
                Exit Sub
            End If
            If txtCuartYearMetaMdl.Text = String.Empty Then
                alertaMdl("Advertencia", "Ingrese la cantidad para el cuarto año", "info", "mdlEditarMeta", "contenedor2_txtPCuartYearMetaMdl")
                Exit Sub
            End If
            If cmbResponsableMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el responsable", "info", "contenedor2_cmbResponsableMdl")
                Exit Sub
            End If
            If cmbAlimentadorMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione el alimentador", "info", "contenedor2_cmbAlimentadorMdl")
                Exit Sub
            End If

            If parametrizacion.updateGoals(lblIdMeta.Text.Trim, txtNombreMetaMdl.Text.Trim, cmbTipoMetaMdl.SelectedValue,
                                           cmbNivelMetaMdl.SelectedValue, txtLineaBaseMetaMdl.Text.Trim, txtPriYearMetaMdl.Text.Trim,
                                           txtSegYearMetaMdl.Text.Trim, txtTercYearMetaMdl.Text.Trim, txtCuartYearMetaMdl.Text.Trim,
                                           cmbResponsableMdl.SelectedValue, cmbAlimentadorMdl.SelectedValue) > 0 Then

                alerta("Se ha actualizado correctamente la meta", "", "success", "")
                limpiarMetas()
            Else
                alerta("Advertencia", "Se genero un error al actualizar", "error", "")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            DataT = Nothing
            Dim code, level_id As String

            If (QuantityDinamicControls.Rows.Count > 0) Then
                Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 1)
                If lastRow IsNot Nothing Then
                    If Not IsDBNull(lastRow("level")) Then
                        level_id = lastRow("level")
                    Else
                        level_id = ""
                    End If
                    If Not IsDBNull(lastRow("sublevel")) Then
                        code = lastRow("sublevel")
                    Else
                        code = ""
                    End If
                End If

                DataT = parametrizacion.selectContentsFiltro(lblPac.Text.Trim, code, level_id)
                If DataT.Rows.Count > 0 Then
                    tblPlanAccion.DataSource = DataT
                    tblPlanAccion.DataBind()
                    tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    alerta("No se han encontraron registros", "", "info")
                    tblPlanAccion.DataSource = Nothing
                    tblPlanAccion.DataBind()
                End If
            Else
                alerta("Debe seleccionar algun item", "", "info")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnConsultarMeta_Click(sender As Object, e As EventArgs) Handles btnConsultarMeta.Click
        Try
            DataT = Nothing
            Dim code As String

            If (QuantityDinamicControlsMeta.Rows.Count > 0) Then
                Dim lastRow As DataRow = QuantityDinamicControlsMeta.Rows(QuantityDinamicControlsMeta.Rows.Count - 1)
                If IsDBNull(lastRow("sublevel")) Then
                    code = String.Empty
                Else
                    code = lastRow("sublevel")
                End If

                DataT = parametrizacion.selectGoalsFiltro(lblPac.Text.Trim, code)
                If DataT.Rows.Count > 0 Then
                    tblMetas.DataSource = DataT
                    tblMetas.DataBind()
                    tblMetas.HeaderRow.TableSection = TableRowSection.TableHeader
                Else
                    alerta("No se han encontraron registros", "", "info")
                    tblMetas.DataSource = Nothing
                    tblMetas.DataBind()
                End If
            Else
                alerta("Debe seleccionar algun item", "", "info")
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Try
            pnlNuevoJerarquia.Visible = True
            btnNuevo.Visible = False
            pnlFiltro.Visible = False
            btnFiltro.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnNuevoMeta_Click(sender As Object, e As EventArgs) Handles btnNuevoMeta.Click
        Try
            pnlMetaNuevo.Visible = True
            btnNuevoMeta.Visible = False
            pnlFiltroMeta.Visible = False
            btnFiltroMeta.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            pnlNuevoJerarquia.Visible = False
            btnNuevo.Visible = True
            pnlFiltro.Visible = True
            btnFiltro.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnCancelarMeta_Click(sender As Object, e As EventArgs) Handles btnCancelarMeta.Click
        Try
            pnlMetaNuevo.Visible = False
            btnNuevoMeta.Visible = True
            pnlFiltroMeta.Visible = True
            btnFiltroMeta.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnFiltro_Click(sender As Object, e As EventArgs) Handles btnFiltro.Click
        Try
            pnlNuevoJerarquia.Visible = False
            btnNuevo.Visible = True
            pnlFiltro.Visible = True
            btnFiltro.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnFiltroMeta_Click(sender As Object, e As EventArgs) Handles btnFiltroMeta.Click
        Try
            pnlMetaNuevo.Visible = False
            btnNuevoMeta.Visible = True
            pnlFiltroMeta.Visible = True
            btnFiltroMeta.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Metodos - Funciones"
    Public Sub visualizarPac(ByVal pac As String)
        Try
            If pac <> String.Empty Then
                Fila = Nothing
                Fila = parametrizacion.selectPac(pac)
                If Fila IsNot Nothing Then
                    lblTituloForm.Text = "EDITAR PAC"
                    lblPac.Text = Fila("id")
                    txtNomPac.Text = Fila("name")
                    txtYearInicial.Text = Fila("initial_year")
                    txtCantYears.Text = Fila("number_years")
                    txtYearFinal.Text = Fila("final_year")

                    btnActPac.Visible = True
                    btnSigPac.Visible = False

                    cargarNiveles(lblPac.Text.Trim)
                    cargarMetas(lblPac.Text.Trim, 1)
                    cargarLabelPresupuesto()
                End If
            Else
                lblTituloForm.Text = "CREACIÓN DEL PAC"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub cargarNiveles(ByVal pac As String)
        Try
            DataT = parametrizacion.selectLevels(pac, "hierarchy")
            If DataT.Rows.Count > 0 Then
                tblNiveles.DataSource = DataT
                tblNiveles.DataBind()
                tblNiveles.UseAccessibleHeader = True
                tblNiveles.HeaderRow.TableSection = TableRowSection.TableHeader

                Session("dtNiveles") = DataT
                cmbNiveles.Items.Clear()
                cmbNiveles.DataTextField = "name"
                cmbNiveles.DataValueField = "hierarchy"
                cmbNiveles.DataSource = DataT
                cmbNiveles.DataBind()
                cmbNiveles.Items.Insert(0, New ListItem("---Seleccione---", ""))
            Else
                tblNiveles.DataSource = Nothing
                tblNiveles.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarPlanAccion(ByVal pac As String)
        Try

            DataT = Nothing
            DataT = parametrizacion.selectContents(pac)
            If DataT.Rows.Count > 0 Then
                tblPlanAccion.DataSource = DataT
                tblPlanAccion.DataBind()
                tblPlanAccion.UseAccessibleHeader = True
                tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblPlanAccion.DataSource = Nothing
                tblPlanAccion.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarMetas(ByVal pac As String, ByVal proceso As Integer)
        Try
            If pac <> String.Empty Then
                If proceso = 1 Then
                    Dim codLevel As String
                    DataT = Nothing
                    DataT = parametrizacion.selectLevels(pac, "hierarchy desc")
                    If DataT.Rows.Count > 0 Then
                        codLevel = DataT(0)(3)
                        lblNivelMetaMdl.Text = DataT(0)(1)
                    Else
                        codLevel = "0"
                        lblNivelMetaMdl.Text = "No hay niveles"
                    End If

                    DataT = Nothing
                    DataT = parametrizacion.selectContents(pac, codLevel, , )
                    If DataT.Rows.Count > 0 Then
                        cmbNivelMetaMdl.Items.Clear()
                        cmbNivelMetaMdl.DataTextField = "name"
                        cmbNivelMetaMdl.DataValueField = "code"
                        cmbNivelMetaMdl.DataSource = DataT
                        cmbNivelMetaMdl.DataBind()
                        cmbNivelMetaMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    End If
                End If
            Else
                tblMetas.DataSource = Nothing
                tblMetas.DataBind()
            End If

            lblIdMeta.Text = String.Empty
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarNivel_Click(sender As Object, e As EventArgs) Handles eliminarNivel.Click
        Try
            DataT = Nothing
            parametrizacion.deleteLevels(lblPac.Text.Trim, Session("idNivel"), "I")
            DataT = parametrizacion.selectContents(lblPac.Text.Trim, Session("idNivel"), "", "")
            parametrizacion.deleteContentsXLevels(lblPac.Text.Trim, Session("idNivel"))
            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    parametrizacion.deleteGoals(lblPac.Text.Trim, row("code"))
                Next
            End If

            Session("idNivel") = Nothing
            cargarNiveles(lblPac.Text.Trim)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarPlanAcc_Click(sender As Object, e As EventArgs) Handles eliminarPlanAcc.Click
        Try
            parametrizacion.deleteContents(lblPac.Text.Trim, Session("CodePlanAcc"))
            Session("idPlanAcc") = Nothing
            btnConsultar_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub alerta(ByVal mensaje As String, ByVal subMensaje As String, ByVal tipo As String, Optional foco As String = "")
        Dim Script As String = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})"
        If foco.Trim <> "" Then
            Script &= ".then((result) => {if (result.value) {document.getElementById('" + foco + "').focus();}});"
        End If
        Script &= " </script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "swal", Script, False)
    End Sub

    Public Sub alertaMdl(ByVal mensaje As String, ByVal subMensaje As String, ByVal tipo As String, ByVal modal As String, Optional foco As String = "")
        Dim Script As String = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})"
        If foco.Trim <> "" Then
            Script &= ".then((result) => {if (result.value) {document.getElementById('" + foco + "').focus();}});"
        End If
        Script &= " $(window).on('load', function () {
                        $('#" & modal & "').modal('show');
                    });"
        Script &= " </script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "swal", Script, False)
    End Sub

    Sub pestaña(index As Integer)
        btnPac.Attributes.Add("class", "")
        btnNiveles.Attributes.Add("class", "")
        btnPlanAccion.Attributes.Add("class", "")
        btnMetas.Attributes.Add("class", "")

        Select Case index
            Case 1
                btnPac.Attributes.Add("class", "nav-link active")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar información general"
            Case 2
                btnNiveles.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar niveles"
            Case 3
                btnPlanAccion.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnMetas.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar contenido"
            Case 4
                btnMetas.Attributes.Add("class", "nav-link active")
                btnPac.Attributes.Add("class", "nav-link")
                btnNiveles.Attributes.Add("class", "nav-link")
                btnPlanAccion.Attributes.Add("class", "nav-link")
                lblSubTitulo.Text = "Editar metas"
        End Select
    End Sub

    Public Sub limpiarForm()
        Try
            lblPac.Text = String.Empty
            txtNomPac.Text = String.Empty
            txtYearInicial.Text = String.Empty
            txtCantYears.Text = String.Empty
            txtYearFinal.Text = String.Empty

            txtNombreNiv.Text = String.Empty
            tblNiveles.DataSource = Nothing
            tblNiveles.DataBind()

            cmbNiveles.Items.Clear()
            cmbSubNivel.Items.Clear()
            txtNombrePlanAcc.Text = String.Empty
            txtPesoPlanAcc.Text = String.Empty
            tblPlanAccion.DataSource = Nothing
            tblPlanAccion.DataBind()

            pnlPac.Visible = True
            pestaña(1)
            pnlNiveles.Visible = False
            pnlPlanAccion.Visible = False
            lblError.Text = String.Empty
            lblError.Visible = False

            pnlSubNivel.Visible = False

            limpiarMetas()

            Session("niveles") = Nothing
            Session("pac") = Nothing
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub limpiarMetas()
        lblIdMeta.Text = String.Empty
        txtNombreMeta.Text = String.Empty
        cmbTipoMeta.SelectedIndex = 0
        txtLineaBaseMeta.Text = String.Empty
        txtPriYearMeta.Text = String.Empty
        txtSegYearMeta.Text = String.Empty
        txtTerYearMeta.Text = String.Empty
        txtCuaYearMeta.Text = String.Empty
        cmbResponsable.SelectedIndex = 0
        cmbAlimentador.SelectedIndex = 0
        QuantityDinamicControlsMetaReg = Nothing
        phDinamicControlsMetaReg.Controls.Clear()
        GenerateControlsMetaReg()
        'cargarMetas(lblPac.Text.Trim, 0)
    End Sub

    Public Sub calcularYearFinal()
        Try
            Dim yearInicial As Integer = 0
            Dim cantidadYears As Integer = 0

            If txtYearInicial.Text <> String.Empty Then yearInicial = CInt(txtYearInicial.Text.Trim)
            If txtCantYears.Text <> String.Empty Then cantidadYears = CInt(txtCantYears.Text.Trim)

            txtYearFinal.Text = yearInicial + (cantidadYears - 1)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarLabelPresupuesto()
        Dim yearInit As Integer = CInt(txtYearInicial.Text.Trim)
        Dim yearFinish As Integer = CInt(txtYearFinal.Text.Trim)

        lblPriPresupuesto.Text = "Presupuesto " & yearInit
        lblSegPresupuesto.Text = "Presupuesto " & yearInit + 1
        lblTerPresupuesto.Text = "Presupuesto " & yearFinish - 1
        lblCuarPresupuesto.Text = "Presupuesto " & yearFinish

    End Sub
    Public Sub limiarFiltroRegistro()
        pnlNvl1Reg.Visible = False
        pnlNvl2Reg.Visible = False
        pnlNvl3Reg.Visible = False
        pnlNvl4Reg.Visible = False
        pnlNvl5Reg.Visible = False
        cmbNvl1Reg.Items.Clear()
        cmbNvl2Reg.Items.Clear()
        cmbNvl3Reg.Items.Clear()
        cmbNvl4Reg.Items.Clear()
        cmbNvl5Reg.Items.Clear()
        QuantityDinamicControls = Nothing
        QuantityDinamicControlsMeta = Nothing
        phDinamicControls.Controls.Clear()
        phDinamicControlsMeta.Controls.Clear()
    End Sub

    Public Function EliminarFormato(ByVal numeroFormateado As String) As String
        Dim pattern As String = "[\$\,\ .]"
        Dim replacement As String = String.Empty
        Dim regex As New System.Text.RegularExpressions.Regex(pattern)

        Return regex.Replace(numeroFormateado, replacement)
    End Function


#End Region

#Region "Init Filtro Dinamicos"

    Protected Overloads Overrides Sub CreateChildControls()
        If Page.IsPostBack Then
            GenerateControls()
            GenerateControlsMeta()
            GenerateControlsMetaReg()
        End If
    End Sub

    Private Property QuantityDinamicControls() As DataTable
        Get
            If ViewState("Quantity") Is Nothing Then
                ViewState("Quantity") = New DataTable()
            End If
            Return DirectCast(ViewState("Quantity"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("Quantity") = value
        End Set
    End Property

    Private Property QuantityDinamicControlsMeta() As DataTable
        Get
            If ViewState("QuantityMeta") Is Nothing Then
                ViewState("QuantityMeta") = New DataTable()
            End If
            Return DirectCast(ViewState("QuantityMeta"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("QuantityMeta") = value
        End Set
    End Property

    Private Property QuantityDinamicControlsMetaReg() As DataTable
        Get
            If ViewState("QuantityMetaReg") Is Nothing Then
                ViewState("QuantityMetaReg") = New DataTable()
            End If
            Return DirectCast(ViewState("QuantityMetaReg"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            ViewState("QuantityMetaReg") = value
        End Set
    End Property

    Private Sub controlesDinamicos_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsPostBack Then
            Dim dt As DataTable = ViewState("Quantity")
            Dim dt2 As DataTable = ViewState("QuantityMeta")
            Dim dt3 As DataTable = ViewState("QuantityMetaReg")
            Me.QuantityDinamicControls = dt
            Me.QuantityDinamicControlsMeta = dt2
            Me.QuantityDinamicControlsMetaReg = dt3
            GenerateControls()
            GenerateControlsMeta()
            GenerateControlsMetaReg()
        End If
    End Sub


#End Region

#Region "Filtro Dinamico Plan Accion"

    Private Sub controlNuevo(Optional nivel As String = "")
        Dim nuevoCmb As DropDownList = New DropDownList()
        Dim nuevoPanel As Panel = New Panel()

        DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, nivel)
        If DataT.Rows.Count > 0 Then
            nuevoCmb.ID = "cmbNivel-" + DataT(0)(4).ToString()
            nuevoCmb.CssClass = "form-control"
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataTextField = "codeName"
            nuevoCmb.DataValueField = "code"
            nuevoCmb.DataSource = DataT
            AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataBind()
            nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
            nuevoCmb.SelectedIndex = 0

            nuevoPanel.ID = "pnl-" + DataT(0)(4).ToString()
            nuevoPanel.CssClass = "col-3"
            nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
            nuevoPanel.Controls.Add(nuevoCmb)
            nuevoPanel.Controls.Add(New LiteralControl("</div>"))

            phDinamicControls.Controls.Add(nuevoPanel)

        Else
            Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 2)
            Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, CInt(lastRow("level")) + 1, "")
            If Fila IsNot Nothing Then
                alerta("El nivel " & Fila("name") & " no contiene contenido", "", "info")
            End If
        End If


    End Sub
    Private Sub GenerateControls()
        Dim Quantity As Integer = 0
        Dim i As Integer = 0
        phDinamicControls.Controls.Clear()
        If QuantityDinamicControls.Rows.Count > 0 Then
            For Each row As DataRow In QuantityDinamicControls.Rows
                Dim nuevoCmb As DropDownList = New DropDownList()
                Dim nuevoPanel As Panel = New Panel()
                DataT = Nothing

                If row("level").ToString() = "1" Then
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, row("sublevel").ToString())
                End If
                If DataT.Rows.Count > 0 Then
                    nuevoCmb.ID = "cmbNivel-" + row("level")
                    nuevoCmb.CssClass = "form-control"
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataTextField = "codeName"
                    nuevoCmb.DataValueField = "code"
                    nuevoCmb.DataSource = DataT
                    AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataBind()
                    nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                    nuevoCmb.SelectedIndex = 0

                    nuevoPanel.ID = "pnl-" + DataT(0)(4).ToString()
                    nuevoPanel.CssClass = "col-3"
                    nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
                    nuevoPanel.Controls.Add(nuevoCmb)
                    nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                    phDinamicControls.Controls.Add(nuevoPanel)
                End If
            Next
        Else
            Dim nuevoCmb As DropDownList = New DropDownList()
            Dim nuevoPanel As Panel = New Panel()
            Dim dt As New DataTable()
            dt.Columns.Add("idcontrol")
            dt.Columns.Add("level")
            dt.Columns.Add("sublevel")
            Dim row As DataRow = dt.NewRow()
            DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
            If DataT.Rows.Count > 0 Then
                row("level") = DataT(0)(4).ToString()
                row("idcontrol") = "cmbNivel-" + DataT(0)(4).ToString()
                nuevoCmb.ID = "cmbNivel-" + DataT(0)(4).ToString()
                nuevoCmb.CssClass = "form-control"
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataTextField = "codeName"
                nuevoCmb.DataValueField = "code"
                nuevoCmb.DataSource = DataT
                AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataBind()
                nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                nuevoCmb.SelectedIndex = 0

                nuevoPanel.ID = "pnl-" + DataT(0)(4).ToString()
                nuevoPanel.CssClass = "col-3"
                nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
                nuevoPanel.Controls.Add(nuevoCmb)
                nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                phDinamicControls.Controls.Add(nuevoPanel)
            End If
            dt.Rows.Add(row)
            QuantityDinamicControls = dt
        End If
    End Sub


    Private Sub nuevoCmb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DataT2 As New DataTable
        Dim nivelControl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As DataRow = QuantityDinamicControls.NewRow()
        Dim delimitadores() As String = {"-"}
        Dim vectoraux() As String
        Dim i As Integer = 1

        vectoraux = nivelControl.ID.Split(delimitadores, StringSplitOptions.None)

        If nivelControl.SelectedValue <> String.Empty Then
            DataT2 = parametrizacion.selectNiveles(lblPac.Text.Trim, nivelControl.SelectedValue)

            If DataT2.Rows.Count > 0 Then
                row("level") = DataT2(0)(4)
            Else
                If QuantityDinamicControls.Rows.Count > 1 Then
                    Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 1)
                    row("level") = CInt(lastRow("level") + 1)
                Else
                    row("level") = String.Empty
                End If

            End If
            row("sublevel") = nivelControl.SelectedValue
            row("idcontrol") = "cmbNivel-" + row("level").ToString()

            Dim result As DataRow() = QuantityDinamicControls.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltro(nivelControl)
            End If
            QuantityDinamicControls.Rows.Add(row)
            controlNuevo(nivelControl.SelectedValue)
        Else
            Dim result As DataRow() = QuantityDinamicControls.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltro(nivelControl)
            End If
        End If
    End Sub

    Public Sub actualizarValoresFiltro(ByVal control As DropDownList)
        Dim indice As Integer = -1
        Dim i As Integer = 1
        Dim vectorNvl() As String
        Dim vectoraux() As String
        Dim indicesRowsEliminar As New List(Of Integer)
        Dim delimitadores() As String = {"-"}

        vectoraux = control.ID.Split(delimitadores, StringSplitOptions.None)
        For Each rowFilter As DataRow In QuantityDinamicControls.Rows
            vectorNvl = rowFilter("idcontrol").Split(delimitadores, StringSplitOptions.None)
            indice += 1
            If rowFilter("level") = vectoraux(1) Then
                rowFilter("sublevel") = control.SelectedValue
            ElseIf rowFilter("level") > vectoraux(1) Then
                indicesRowsEliminar.Add(indice)
                Dim controlPanel As Panel = TryCast(phDinamicControls.FindControl("pnl-" & vectorNvl(1)), Panel)
                phDinamicControls.Controls.Remove(controlPanel)
            End If
        Next

        For x As Int32 = indicesRowsEliminar.Count - 1 To 0 Step -1
            Dim indiceEliminar As Integer = indicesRowsEliminar.Item(x)
            QuantityDinamicControls.Rows.RemoveAt(indiceEliminar)
        Next

        For Each rowFilter As DataRow In QuantityDinamicControls.Rows
            If i > 1 Then
                Dim controlCmb As DropDownList = TryCast(phDinamicControls.FindControl("cmbNivel-" & CInt(rowFilter("level")) - 1 & ""), DropDownList)
                If controlCmb IsNot Nothing Then
                    rowFilter("sublevel") = controlCmb.SelectedValue
                End If
            End If
            i += 1
        Next
    End Sub

#End Region

#Region "Fltro Dinamico Metas"
    Private Sub controlNuevoMeta(Optional nivel As String = "")
        Dim nuevoCmb As DropDownList = New DropDownList()
        Dim nuevoPanel As Panel = New Panel()

        DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, nivel)
        If DataT.Rows.Count > 0 Then
            nuevoCmb.ID = "cmbNivelMeta-" + DataT(0)(4).ToString()
            nuevoCmb.CssClass = "form-control"
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataTextField = "codeName"
            nuevoCmb.DataValueField = "code"
            nuevoCmb.DataSource = DataT
            AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMeta_SelectedIndexChanged
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataBind()
            nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
            nuevoCmb.SelectedIndex = 0

            nuevoPanel.ID = "pnlMeta-" + DataT(0)(4).ToString()
            nuevoPanel.CssClass = "col-3"
            nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
            nuevoPanel.Controls.Add(nuevoCmb)
            nuevoPanel.Controls.Add(New LiteralControl("</div>"))

            phDinamicControlsMeta.Controls.Add(nuevoPanel)

        Else
            Dim lastRow As DataRow = QuantityDinamicControlsMeta.Rows(QuantityDinamicControlsMeta.Rows.Count - 2)
            Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, CInt(lastRow("level")) + 1, "")
            If Fila IsNot Nothing Then
                alerta("El nivel " & Fila("name") & " no contiene contenido", "", "info")
            End If
        End If


    End Sub
    Private Sub GenerateControlsMeta()
        Dim Quantity As Integer = 0
        Dim i As Integer = 0
        phDinamicControlsMeta.Controls.Clear()
        If QuantityDinamicControlsMeta.Rows.Count > 0 Then
            For Each row As DataRow In QuantityDinamicControlsMeta.Rows
                Dim nuevoCmb As DropDownList = New DropDownList()
                Dim nuevoPanel As Panel = New Panel()
                DataT = Nothing

                If row("level").ToString() = "1" Then
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, row("sublevel").ToString())
                End If
                If DataT.Rows.Count > 0 Then
                    nuevoCmb.ID = "cmbNivelMeta-" + row("level")
                    nuevoCmb.CssClass = "form-control"
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataTextField = "codeName"
                    nuevoCmb.DataValueField = "code"
                    nuevoCmb.DataSource = DataT
                    AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMeta_SelectedIndexChanged
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataBind()
                    nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                    nuevoCmb.SelectedIndex = 0

                    nuevoPanel.ID = "pnlMeta-" + DataT(0)(4).ToString()
                    nuevoPanel.CssClass = "col-3"
                    nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
                    nuevoPanel.Controls.Add(nuevoCmb)
                    nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                    phDinamicControlsMeta.Controls.Add(nuevoPanel)
                End If
            Next
        Else
            Dim nuevoCmb As DropDownList = New DropDownList()
            Dim nuevoPanel As Panel = New Panel()
            Dim dt As New DataTable()
            dt.Columns.Add("idcontrol")
            dt.Columns.Add("level")
            dt.Columns.Add("sublevel")
            Dim row As DataRow = dt.NewRow()
            DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
            If DataT.Rows.Count > 0 Then
                row("level") = DataT(0)(4).ToString()
                row("idcontrol") = "cmbNivelMeta-" + DataT(0)(4).ToString()
                nuevoCmb.ID = "cmbNivelMeta-" + DataT(0)(4).ToString()
                nuevoCmb.CssClass = "form-control"
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataTextField = "codeName"
                nuevoCmb.DataValueField = "code"
                nuevoCmb.DataSource = DataT
                AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMeta_SelectedIndexChanged
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataBind()
                nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                nuevoCmb.SelectedIndex = 0

                nuevoPanel.ID = "pnlMeta-" + DataT(0)(4).ToString()
                nuevoPanel.CssClass = "col-3"
                nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
                nuevoPanel.Controls.Add(nuevoCmb)
                nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                phDinamicControlsMeta.Controls.Add(nuevoPanel)
            End If
            dt.Rows.Add(row)
            QuantityDinamicControlsMeta = dt
        End If
    End Sub


    Private Sub nuevoCmbMeta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DataT2 As New DataTable
        Dim nivelControl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As DataRow = QuantityDinamicControlsMeta.NewRow()
        Dim delimitadores() As String = {"-"}
        Dim vectoraux() As String
        Dim i As Integer = 1

        vectoraux = nivelControl.ID.Split(delimitadores, StringSplitOptions.None)

        If nivelControl.SelectedValue <> String.Empty Then
            DataT2 = parametrizacion.selectNiveles(lblPac.Text.Trim, nivelControl.SelectedValue)

            If DataT2.Rows.Count > 0 Then
                row("level") = DataT2(0)(4)
            Else
                If QuantityDinamicControlsMeta.Rows.Count > 1 Then
                    Dim lastRow As DataRow = QuantityDinamicControlsMeta.Rows(QuantityDinamicControlsMeta.Rows.Count - 1)
                    row("level") = CInt(lastRow("level") + 1)
                Else
                    row("level") = String.Empty
                End If

            End If
            row("sublevel") = nivelControl.SelectedValue
            row("idcontrol") = "cmbNivelMeta-" + row("level").ToString()

            Dim result As DataRow() = QuantityDinamicControlsMeta.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltroMeta(nivelControl)
            End If
            QuantityDinamicControlsMeta.Rows.Add(row)
            controlNuevoMeta(nivelControl.SelectedValue)
        Else
            Dim result As DataRow() = QuantityDinamicControlsMeta.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltroMeta(nivelControl)
            End If
        End If
    End Sub

    Public Sub actualizarValoresFiltroMeta(ByVal control As DropDownList)
        Dim indice As Integer = -1
        Dim i As Integer = 1
        Dim vectorNvl() As String
        Dim vectoraux() As String
        Dim indicesRowsEliminar As New List(Of Integer)
        Dim delimitadores() As String = {"-"}

        vectoraux = control.ID.Split(delimitadores, StringSplitOptions.None)
        For Each rowFilter As DataRow In QuantityDinamicControlsMeta.Rows
            vectorNvl = rowFilter("idcontrol").Split(delimitadores, StringSplitOptions.None)
            indice += 1
            If rowFilter("level") = vectoraux(1) Then
                rowFilter("sublevel") = control.SelectedValue
            ElseIf rowFilter("level") > vectoraux(1) Then
                indicesRowsEliminar.Add(indice)
                Dim controlPanel As Panel = TryCast(phDinamicControlsMeta.FindControl("pnlMeta-" & vectorNvl(1)), Panel)
                phDinamicControlsMeta.Controls.Remove(controlPanel)
            End If
        Next
        For x As Int32 = indicesRowsEliminar.Count - 1 To 0 Step -1
            Dim indiceEliminar As Integer = indicesRowsEliminar.Item(x)
            QuantityDinamicControlsMeta.Rows.RemoveAt(indiceEliminar)
        Next

        For Each rowFilter As DataRow In QuantityDinamicControlsMeta.Rows
            If i > 1 Then
                Dim controlCmb As DropDownList = TryCast(phDinamicControlsMeta.FindControl("cmbNivelMeta-" & CInt(rowFilter("level")) - 1 & ""), DropDownList)
                If controlCmb IsNot Nothing Then
                    rowFilter("sublevel") = controlCmb.SelectedValue
                End If
            End If
            i += 1
        Next
    End Sub

#End Region

#Region "Fltro Dinamico Metas Registro"
    Private Sub controlNuevoMetaReg(Optional nivel As String = "")
        Dim nuevoCmb As DropDownList = New DropDownList()
        Dim nuevoPanel As Panel = New Panel()

        DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, nivel)
        If DataT.Rows.Count > 0 Then
            nuevoCmb.ID = "cmbNivelMetaReg-" + DataT(0)(4).ToString()
            nuevoCmb.CssClass = "form-control"
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataTextField = "codeName"
            nuevoCmb.DataValueField = "code"
            nuevoCmb.DataSource = DataT
            AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMetaReg_SelectedIndexChanged
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataBind()
            nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
            nuevoCmb.SelectedIndex = 0

            nuevoPanel.ID = "pnlMetaReg-" + DataT(0)(4).ToString()
            nuevoPanel.CssClass = "col-3"
            nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
            nuevoPanel.Controls.Add(nuevoCmb)
            nuevoPanel.Controls.Add(New LiteralControl("</div>"))

            phDinamicControlsMetaReg.Controls.Add(nuevoPanel)

        Else
            Dim lastRow As DataRow = QuantityDinamicControlsMetaReg.Rows(QuantityDinamicControlsMetaReg.Rows.Count - 2)
            Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, CInt(lastRow("level")) + 1, "")
            If Fila IsNot Nothing Then
                alerta("El nivel " & Fila("name") & " no contiene contenido", "", "info")
            End If
        End If


    End Sub
    Private Sub GenerateControlsMetaReg()
        Dim Quantity As Integer = 0
        Dim i As Integer = 0
        phDinamicControlsMetaReg.Controls.Clear()
        If QuantityDinamicControlsMetaReg.Rows.Count > 0 Then
            For Each row As DataRow In QuantityDinamicControlsMetaReg.Rows
                Dim nuevoCmb As DropDownList = New DropDownList()
                Dim nuevoPanel As Panel = New Panel()

                DataT = Nothing

                If row("level").ToString() = "1" Then
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
                Else
                    DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, row("sublevel").ToString())
                End If
                If DataT.Rows.Count > 0 Then
                    nuevoCmb.ID = "cmbNivelMetaReg-" + row("level")
                    nuevoCmb.CssClass = "form-control"
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataTextField = "codeName"
                    nuevoCmb.DataValueField = "code"
                    nuevoCmb.DataSource = DataT
                    AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMetaReg_SelectedIndexChanged
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataBind()
                    nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                    nuevoCmb.SelectedIndex = 0

                    nuevoPanel.ID = "pnlMetaReg-" + row("level")
                    nuevoPanel.CssClass = "col-3"
                    nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                                <label>" & DataT(0)(5).ToString() & "</label>"))
                    nuevoPanel.Controls.Add(nuevoCmb)
                    nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                    phDinamicControlsMetaReg.Controls.Add(nuevoPanel)
                End If
            Next
        Else
            Dim nuevoCmb As DropDownList = New DropDownList()
            Dim nuevoPanel As Panel = New Panel()
            Dim dt As New DataTable()
            dt.Columns.Add("idcontrol")
            dt.Columns.Add("level")
            dt.Columns.Add("sublevel")
            Dim row As DataRow = dt.NewRow()
            DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
            If DataT.Rows.Count > 0 Then
                row("level") = DataT(0)(4).ToString()
                row("idcontrol") = "cmbNivelMetaReg-" + DataT(0)(4).ToString()
                nuevoCmb.ID = "cmbNivelMetaReg-" + DataT(0)(4).ToString()
                nuevoCmb.CssClass = "form-control"
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataTextField = "codeName"
                nuevoCmb.DataValueField = "code"
                nuevoCmb.DataSource = DataT
                AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmbMetaReg_SelectedIndexChanged
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataBind()
                nuevoCmb.Items.Insert(0, New ListItem("Todos", ""))
                nuevoCmb.SelectedIndex = 0
                nuevoPanel.ID = "pnlMetaReg-" + row("level")
                nuevoPanel.CssClass = "col-3"
                nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                                <label>" & DataT(0)(5).ToString() & "</label>"))
                nuevoPanel.Controls.Add(nuevoCmb)
                nuevoPanel.Controls.Add(New LiteralControl("</div>"))

                phDinamicControlsMetaReg.Controls.Add(nuevoPanel)
            End If
            dt.Rows.Add(row)
            QuantityDinamicControlsMetaReg = dt
        End If
    End Sub


    Private Sub nuevoCmbMetaReg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DataT2 As New DataTable
        Dim nivelControl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As DataRow = QuantityDinamicControlsMetaReg.NewRow()
        Dim delimitadores() As String = {"-"}
        Dim vectoraux() As String
        Dim i As Integer = 1

        vectoraux = nivelControl.ID.Split(delimitadores, StringSplitOptions.None)

        If nivelControl.SelectedValue <> String.Empty Then
            DataT2 = parametrizacion.selectNiveles(lblPac.Text.Trim, nivelControl.SelectedValue)

            If DataT2.Rows.Count > 0 Then
                row("level") = DataT2(0)(4)
            Else
                If QuantityDinamicControlsMetaReg.Rows.Count > 1 Then
                    Dim lastRow As DataRow = QuantityDinamicControlsMetaReg.Rows(QuantityDinamicControlsMetaReg.Rows.Count - 1)
                    row("level") = CInt(lastRow("level") + 1)
                Else
                    row("level") = String.Empty
                End If

            End If
            row("sublevel") = nivelControl.SelectedValue
            row("idcontrol") = "cmbNivelMetaReg-" + row("level").ToString()

            Dim result As DataRow() = QuantityDinamicControlsMetaReg.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltroMetaReg(nivelControl)
            End If
            QuantityDinamicControlsMetaReg.Rows.Add(row)
            controlNuevoMetaReg(nivelControl.SelectedValue)
        Else
            Dim result As DataRow() = QuantityDinamicControlsMetaReg.Select("level = '" & vectoraux(1) & "'")
            If result.Length >= 1 Then
                actualizarValoresFiltroMetaReg(nivelControl)
            End If
        End If
    End Sub

    Public Sub actualizarValoresFiltroMetaReg(ByVal control As DropDownList)
        Dim indice As Integer = -1
        Dim i As Integer = 1
        Dim vectorNvl() As String
        Dim vectoraux() As String
        Dim indicesRowsEliminar As New List(Of Integer)
        Dim delimitadores() As String = {"-"}

        vectoraux = control.ID.Split(delimitadores, StringSplitOptions.None)
        For Each rowFilter As DataRow In QuantityDinamicControlsMetaReg.Rows
            vectorNvl = rowFilter("idcontrol").Split(delimitadores, StringSplitOptions.None)
            indice += 1
            If rowFilter("level") = vectoraux(1) Then
                rowFilter("sublevel") = control.SelectedValue
            ElseIf rowFilter("level") > vectoraux(1) Then
                indicesRowsEliminar.Add(indice)
                Dim controlPanel As Panel = TryCast(phDinamicControlsMetaReg.FindControl("pnlMetaReg-" & vectorNvl(1)), Panel)
                phDinamicControlsMetaReg.Controls.Remove(controlPanel)
            End If
        Next
        For x As Int32 = indicesRowsEliminar.Count - 1 To 0 Step -1
            Dim indiceEliminar As Integer = indicesRowsEliminar.Item(x)
            QuantityDinamicControlsMetaReg.Rows.RemoveAt(indiceEliminar)
        Next

        For Each rowFilter As DataRow In QuantityDinamicControlsMetaReg.Rows
            If i > 1 Then
                Dim controlCmb As DropDownList = TryCast(phDinamicControlsMetaReg.FindControl("cmbNivelMetaReg-" & CInt(rowFilter("level")) - 1 & ""), DropDownList)
                If controlCmb IsNot Nothing Then
                    rowFilter("sublevel") = controlCmb.SelectedValue
                End If
            End If
            i += 1
        Next
    End Sub

    Private Sub txtPriPresupuesto_TextChanged(sender As Object, e As EventArgs) Handles txtPriPresupuesto.TextChanged
        calcularPresupuesto()
        txtSegPresupuesto.Focus()
    End Sub

    Private Sub txtSegPresupuesto_TextChanged(sender As Object, e As EventArgs) Handles txtSegPresupuesto.TextChanged
        calcularPresupuesto()
        txtTerPresupuesto.Focus()
    End Sub

    Private Sub txtTerPresupuesto_TextChanged(sender As Object, e As EventArgs) Handles txtTerPresupuesto.TextChanged
        calcularPresupuesto()
        txtCuarPresupuesto.Focus()
    End Sub

    Private Sub txtCuarPresupuesto_TextChanged(sender As Object, e As EventArgs) Handles txtCuarPresupuesto.TextChanged
        calcularPresupuesto()
        txtAcumulado.Focus()
    End Sub

#End Region

    Public Sub calcularPresupuesto()
        Dim priPresupuesto, segPresupuesto, terPresupuesto, cuarPresupuesto, total As Decimal
        If txtPriPresupuesto.Text <> String.Empty Then priPresupuesto = EliminarFormato(txtPriPresupuesto.Text.Trim) Else priPresupuesto = 0
        If txtSegPresupuesto.Text <> String.Empty Then segPresupuesto = EliminarFormato(txtSegPresupuesto.Text.Trim) Else segPresupuesto = 0
        If txtTerPresupuesto.Text <> String.Empty Then terPresupuesto = EliminarFormato(txtTerPresupuesto.Text.Trim) Else terPresupuesto = 0
        If txtCuarPresupuesto.Text <> String.Empty Then cuarPresupuesto = EliminarFormato(txtCuarPresupuesto.Text.Trim) Else cuarPresupuesto = 0
        total = priPresupuesto + segPresupuesto + terPresupuesto + cuarPresupuesto
        txtAcumulado.Text = total
    End Sub


End Class