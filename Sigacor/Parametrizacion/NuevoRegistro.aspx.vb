Public Class NuevoRegistro
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
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
            pnlNiv2.Visible = False
            pnlNiv3.Visible = False
            pnlNiv4.Visible = False
            btnFiltro.Visible = False
            pnlNuevoJerarquia.Visible = False
            pnlNiv2Meta.Visible = False
            pnlNiv3Meta.Visible = False
            pnlNiv4Meta.Visible = False
            pnlMetaNuevo.Visible = False
            btnFiltroMeta.Visible = False

            DataT = Nothing
            DataT = users.selectUsuario
            If DataT.Rows.Count > 0 Then
                cmbResponsable.Items.Clear()
                cmbResponsable.DataTextField = "nombreEmp"
                cmbResponsable.DataValueField = "user_id"
                cmbResponsable.DataSource = DataT
                cmbResponsable.DataBind()
                cmbResponsable.Items.Insert(0, New ListItem("---Seleccione---", ""))

                cmbResponsableMdl.Items.Clear()
                cmbResponsableMdl.DataTextField = "nombreEmp"
                cmbResponsableMdl.DataValueField = "user_id"
                cmbResponsableMdl.DataSource = DataT
                cmbResponsableMdl.DataBind()
                cmbResponsableMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))

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
            cargarLineas()
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
    Private Sub cmbNiveles_SelectedIndexChsanged(sender As Object, e As EventArgs) Handles cmbNiveles.SelectedIndexChanged
        Try
            If cmbNiveles.SelectedValue = "1" Then
                pnlSubNivel.Visible = False
                cmbSubNivel.Items.Clear()
            Else
                DataT = Nothing
                DataT = parametrizacion.selectContents(lblPac.Text.Trim, CInt(cmbNiveles.SelectedValue.Trim) - 1, "", "")
                If DataT.Rows.Count > 0 Then
                    lblSubNivel.Text = "¿A que " & DataT(0)(4) & " pertenece?"
                    pnlSubNivel.Visible = True
                    cmbSubNivel.Items.Clear()
                    cmbSubNivel.DataTextField = "name"
                    cmbSubNivel.DataValueField = "code"
                    cmbSubNivel.DataSource = DataT
                    cmbSubNivel.DataBind()
                    cmbSubNivel.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    cmbSubNivel.Focus()
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

    Private Sub cmbLineas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineas.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbLineas.SelectedIndex = 0 Then
                cmbNiv2.Items.Clear()
                pnlNiv2.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbLineas.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv2.Items.Clear()
                    cmbNiv2.DataTextField = "name"
                    cmbNiv2.DataValueField = "code"
                    cmbNiv2.DataSource = DataT
                    cmbNiv2.DataBind()
                    cmbNiv2.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv2.Text = DataT(0)(2)
                    pnlNiv2.Visible = True
                Else
                    cmbNiv2.Items.Clear()
                    pnlNiv2.Visible = False
                    alerta("Advertencia", "No se han encontrado programas", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbLineasMeta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineasMeta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbLineasMeta.SelectedIndex = 0 Then
                cmbNiv2Meta.Items.Clear()
                pnlNiv2Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbLineasMeta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv2Meta.Items.Clear()
                    cmbNiv2Meta.DataTextField = "name"
                    cmbNiv2Meta.DataValueField = "code"
                    cmbNiv2Meta.DataSource = DataT
                    cmbNiv2Meta.DataBind()
                    cmbNiv2Meta.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv2Meta.Text = DataT(0)(2)
                    pnlNiv2Meta.Visible = True
                Else
                    cmbNiv2Meta.Items.Clear()
                    pnlNiv2Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado programas", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbNiv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv2.SelectedIndex = 0 Then
                cmbNiv3.Items.Clear()
                pnlNiv3.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv2.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv3.Items.Clear()
                    cmbNiv3.DataTextField = "name"
                    cmbNiv3.DataValueField = "code"
                    cmbNiv3.DataSource = DataT
                    cmbNiv3.DataBind()
                    cmbNiv3.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv3.Text = DataT(0)(2)
                    pnlNiv3.Visible = True
                Else
                    cmbNiv3.Items.Clear()
                    pnlNiv3.Visible = False
                    alerta("Advertencia", "No se han encontrado proyectos", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv2Meta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2Meta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv2Meta.SelectedIndex = 0 Then
                cmbNiv3Meta.Items.Clear()
                pnlNiv3Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv2Meta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv3Meta.Items.Clear()
                    cmbNiv3Meta.DataTextField = "name"
                    cmbNiv3Meta.DataValueField = "code"
                    cmbNiv3Meta.DataSource = DataT
                    cmbNiv3Meta.DataBind()
                    cmbNiv3Meta.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv3Meta.Text = DataT(0)(2)
                    pnlNiv3Meta.Visible = True
                Else
                    cmbNiv3Meta.Items.Clear()
                    pnlNiv3Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado proyectos", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv3.SelectedIndex = 0 Then
                cmbNiv4.Items.Clear()
                pnlNiv4.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv3.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv4.Items.Clear()
                    cmbNiv4.DataTextField = "name"
                    cmbNiv4.DataValueField = "code"
                    cmbNiv4.DataSource = DataT
                    cmbNiv4.DataBind()
                    cmbNiv4.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv4.Text = DataT(0)(2)
                    pnlNiv4.Visible = True
                Else
                    cmbNiv4.Items.Clear()
                    pnlNiv4.Visible = False
                    alerta("Advertencia", "No se han encontrado actividades", "info")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub cmbNiv3Meta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3Meta.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv3Meta.SelectedIndex = 0 Then
                cmbNiv4Meta.Items.Clear()
                pnlNiv4Meta.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim, cmbNiv3Meta.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv4Meta.Items.Clear()
                    cmbNiv4Meta.DataTextField = "name"
                    cmbNiv4Meta.DataValueField = "code"
                    cmbNiv4Meta.DataSource = DataT
                    cmbNiv4Meta.DataBind()
                    cmbNiv4Meta.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    lblNiv4Meta.Text = DataT(0)(2)
                    pnlNiv4Meta.Visible = True
                Else
                    cmbNiv4Meta.Items.Clear()
                    pnlNiv4Meta.Visible = False
                    alerta("Advertencia", "No se han encontrado actividades", "info")
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
                'linkBtnEliminar.CommandArgument = e.Row.Cells(0).Text.Trim

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
                'e.Row.Cells(4).Visible = False

                Dim linkBtnEditar, linkBtnEliminar, linkBtnConfirmar As New LinkButton
                linkBtnEditar = e.Row.FindControl("lnkEditNiv")
                'linkBtnEliminar = e.Row.FindControl("lnkEliNiv")
                linkBtnConfirmar = e.Row.FindControl("lnkConEdit")

                linkBtnEditar.CommandArgument = e.Row.Cells(0).Text.Trim
                'linkBtnEliminar.CommandArgument = e.Row.Cells(0).Text.Trim

                linkBtnConfirmar.Visible = False

            End If
            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.Cells(0).Visible = False
                e.Row.Cells(3).Visible = False
                'e.Row.Cells(4).Visible = False
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
                'linkBtn2 = e.Row.FindControl("lnkEliminarMeta")

                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
                'linkBtn2.CommandArgument = e.Row.Cells(0).Text.Trim

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
                    'linkBtnEliminar = tblNiveles.Rows(row.RowIndex).FindControl("lnkEliNiv")

                    'codigo = tblNiveles.Rows(row.RowIndex).FindControl("txtCodigo")
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        'linkBtnEliminar.Visible = False

                        'codigo.Text = row.Cells(1).Text.Trim
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(2).Text.Trim)

                        row.Cells(2).Visible = False
                        row.Cells(3).Visible = True

                        'row.Cells(3).Visible = False
                        'row.Cells(4).Visible = True


                    Else
                        linkBtnEditar.Visible = False
                        linkBtnEliminar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblNiveles.Rows
                    'codigo = tblNiveles.Rows(row.RowIndex).FindControl("txtCodigo")
                    nombre = tblNiveles.Rows(row.RowIndex).FindControl("txtNombre")

                    If row.Cells(3).Visible = True Then
                        'If codigo.Text = String.Empty Then
                        '    alerta("Advertencia", "Ingrese el código del nivel", "info")
                        '    Exit Sub
                        'End If
                        If nombre.Text = String.Empty Then
                            alerta("Advertencia", "Ingrese el nombre del nivel", "info")
                            Exit Sub
                        End If
                        parametrizacion.updateLevels(row.Cells(0).Text.Trim, nombre.Text.Trim, row.Cells(1).Text.Trim, "A")
                        'Fila = Nothing
                        'Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, row.Cells(1).Text.Trim, nombre.Text.Trim)
                        'If Fila Is Nothing Then

                        '    If row.Cells(1).Text.Trim <> codigo.Text.Trim Then
                        '        Fila = Nothing
                        '        Fila = parametrizacion.selectLevelsFila(lblPac.Text.Trim, codigo.Text.Trim)
                        '        If Fila IsNot Nothing Then
                        '            alerta("Advertencia", "El código del nivel, ya existe", "info")
                        '            Exit Sub
                        '        End If
                        '    Else

                        '    End If

                        'End If
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
            'Dim jerarquia, nombre, peso As TextBox
            Dim nombre, peso As TextBox

            If e.CommandName = "Editar" Then
                For Each row As GridViewRow In tblPlanAccion.Rows

                    linkBtnConfirmar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkConEditPlanAcc")
                    linkBtnEditar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEditPlanAcc")
                    'linkBtnEliminar = tblPlanAccion.Rows(row.RowIndex).FindControl("lnkEliPlanAcc")

                    'jerarquia = tblPlanAccion.Rows(row.RowIndex).FindControl("txtJerarquia")
                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If e.CommandArgument = row.Cells(0).Text Then

                        linkBtnEditar.Visible = False
                        linkBtnConfirmar.Visible = True
                        'linkBtnEliminar.Visible = False

                        'jerarquia.Text = row.Cells(2).Text.Trim
                        nombre.Text = HttpUtility.HtmlDecode(row.Cells(3).Text.Trim)
                        peso.Text = row.Cells(5).Text.Trim

                        row.Cells(3).Visible = False
                        row.Cells(4).Visible = True

                        row.Cells(5).Visible = False
                        row.Cells(6).Visible = True
                    Else
                        linkBtnEditar.Visible = False
                        'linkBtnEliminar.Visible = False
                    End If
                Next

            ElseIf e.CommandName = "Confirmar" Then

                For Each row As GridViewRow In tblPlanAccion.Rows
                    'jerarquia = tblPlanAccion.Rows(row.RowIndex).FindControl("txtJerarquia")
                    nombre = tblPlanAccion.Rows(row.RowIndex).FindControl("txtNombrePlanAcc")
                    peso = tblPlanAccion.Rows(row.RowIndex).FindControl("txtPeso")

                    If row.Cells(4).Visible = True Then
                        'If jerarquia.Text = String.Empty Then
                        '    alerta("Advertencia", "Ingrese el código de la jerarquia", "info")
                        '    Exit Sub
                        'End If
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
                Session("idPlanAcc") = e.CommandArgument
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
                    cmbSubActividadMetaMdl.SelectedValue = Fila("subactivity")
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
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region
    Private Sub btnSigPac_Click(sender As Object, e As EventArgs) Handles btnSigPac.Click
        Try
            If txtNomPac.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre el PAC", "info", "contenedor2_txtNomPac")
                Exit Sub
            End If
            If txtSlogan.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el slogan", "info", "contenedor2_txtSlogan")
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

            If parametrizacion.updatePac(txtNomPac.Text.Trim, txtSlogan.Text.Trim, txtYearInicial.Text.Trim,
                                         txtYearFinal.Text.Trim, txtCantYears.Text.Trim,
                                         "A", lblPac.Text.Trim) > 0 Then

                alerta("Se ha actualizado el pac correctamente", "PAC:  " & lblPac.Text.Trim, "success", "")
            Else
                parametrizacion.insertPac(txtNomPac.Text.Trim, txtSlogan.Text.Trim, txtYearInicial.Text.Trim,
                                          txtYearFinal.Text.Trim, txtCantYears.Text.Trim,
                                          "A")
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

            'cargarMetas(lblPac.Text.Trim, 1)
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

            If cmbNiveles.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione un nivel", "info", "contenedor2_cmbNiveles")
                Exit Sub
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

            Dim subNivel, name As String
            If cmbSubNivel.Items.Count > 0 Then
                subNivel = cmbSubNivel.SelectedValue.Trim
                name = cmbSubNivel.SelectedItem.ToString.Trim
            Else
                subNivel = String.Empty
            End If

            Dim code, array As String

            DataT = parametrizacion.selectContents(lblPac.Text.Trim, cmbNiveles.SelectedValue.Trim - 1, "", subNivel)
            If DataT.Rows.Count > 0 Then
                code = DataT(0)(3) & "." & txtCodigo.Text.Trim
                DataT(0)(9) = CStr(DataT(0)(9)).Substring(0, CStr(DataT(0)(9)).Length - 1)
                array = CStr(DataT(0)(9)) & ", " & cmbNiveles.SelectedItem.ToString & ":" & txtCodigo.Text.Trim & "]"
            Else
                code = txtCodigo.Text.Trim
                array = "[" & cmbNiveles.SelectedItem.ToString() & ":" & txtCodigo.Text.Trim & "]"
            End If
            DataT = Nothing
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

                btnConsultar_Click(Nothing, Nothing)
                'cargarPlanAccion(lblPac.Text.Trim)
                'cargarMetas(lblPac.Text.Trim, 1)
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
            If txtNombreMeta.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el nombre de la meta", "info", "contenedor2_txtNombreMeta")
                Exit Sub
            End If
            If cmbTipoMeta.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el tipo de meta", "info", "contenedor2_cmbTipoMeta")
                Exit Sub
            End If
            If cmbSubActividad.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione la subactividad la meta", "info", "contenedor2_cmbSubActividad")
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
            If cmbResponsable.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el responsable", "info", "contenedor2_cmbResponsable")
                Exit Sub
            End If
            If cmbAlimentador.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el alimentador", "info", "contenedor2_cmbAlimentador")
                Exit Sub
            End If

            If parametrizacion.insertGoals(lblPac.Text.Trim, txtNombreMeta.Text.Trim, cmbTipoMeta.SelectedValue,
                                           cmbSubActividad.SelectedValue, txtLineaBaseMeta.Text.Trim, txtPriYearMeta.Text.Trim,
                                           txtSegYearMeta.Text.Trim, txtTerYearMeta.Text.Trim, txtCuaYearMeta.Text.Trim,
                                           cmbResponsable.SelectedValue.Trim, cmbAlimentador.SelectedValue.Trim, "A") > 0 Then

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
            If cmbSubActividadMetaMdl.SelectedIndex = 0 Then
                alertaMdl("Advertencia", "Seleccione la subactividad la meta", "info", "mdlEditarMeta", "contenedor2_cmbSubActividadMetaMdl")
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
                                           cmbSubActividadMetaMdl.SelectedValue, txtLineaBaseMetaMdl.Text.Trim, txtPriYearMetaMdl.Text.Trim,
                                           txtSegYearMetaMdl.Text.Trim, txtTercYearMetaMdl.Text.Trim, txtCuartYearMetaMdl.Text.Trim,
                                           cmbResponsableMdl.SelectedValue.Trim, cmbAlimentadorMdl.SelectedValue.Trim) > 0 Then

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
            Dim code As String
            If cmbLineas.SelectedIndex > 0 Then
                If cmbNiv2.SelectedIndex > 0 Then
                    If cmbNiv3.SelectedIndex > 0 Then
                        If cmbNiv4.SelectedIndex > 0 Then
                            code = cmbNiv4.SelectedValue
                        Else
                            code = cmbNiv3.SelectedValue
                        End If
                    Else
                        code = cmbNiv2.SelectedValue
                    End If
                Else
                    code = cmbLineas.SelectedValue
                End If
            Else
                code = String.Empty
            End If

            DataT = parametrizacion.selectContentsFiltro(lblPac.Text.Trim, code)
            If DataT.Rows.Count > 0 Then
                tblPlanAccion.DataSource = DataT
                tblPlanAccion.DataBind()
                tblPlanAccion.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                alerta("No se han encontraron registros", "", "info")
                tblPlanAccion.DataSource = Nothing
                tblPlanAccion.DataBind()
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
            If cmbLineasMeta.SelectedIndex > 0 Then
                If cmbNiv2Meta.SelectedIndex > 0 Then
                    If cmbNiv3Meta.SelectedIndex > 0 Then
                        If cmbNiv4Meta.SelectedIndex > 0 Then
                            code = cmbNiv4Meta.SelectedValue
                        Else
                            code = cmbNiv3Meta.SelectedValue
                        End If
                    Else
                        code = cmbNiv2Meta.SelectedValue
                    End If
                Else
                    code = cmbLineasMeta.SelectedValue
                End If
            Else
                code = String.Empty
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
                    txtSlogan.Text = Fila("slogan")
                    txtYearInicial.Text = Fila("initial_year")
                    txtCantYears.Text = Fila("number_years")
                    txtYearFinal.Text = Fila("final_year")

                    btnActPac.Visible = True
                    btnSigPac.Visible = False

                    cargarNiveles(lblPac.Text.Trim)
                    'cargarPlanAccion(lblPac.Text.Trim)
                    cargarMetas(lblPac.Text.Trim, 1)
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
            DataT = parametrizacion.selectLevels(pac)
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
                    Else
                        codLevel = "0"
                    End If

                    DataT = Nothing
                    DataT = parametrizacion.selectContents(pac, codLevel, , )
                    If DataT.Rows.Count > 0 Then
                        cmbSubActividad.Items.Clear()
                        cmbSubActividad.DataTextField = "name"
                        cmbSubActividad.DataValueField = "code"
                        cmbSubActividad.DataSource = DataT
                        cmbSubActividad.DataBind()
                        cmbSubActividad.Items.Insert(0, New ListItem("---Seleccione---", ""))

                        cmbSubActividadMetaMdl.Items.Clear()
                        cmbSubActividadMetaMdl.DataTextField = "name"
                        cmbSubActividadMetaMdl.DataValueField = "code"
                        cmbSubActividadMetaMdl.DataSource = DataT
                        cmbSubActividadMetaMdl.DataBind()
                        cmbSubActividadMetaMdl.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    End If
                End If

                'DataT = Nothing
                'DataT = parametrizacion.selectGoals(pac)
                'If DataT.Rows.Count > 0 Then
                '    tblMetas.DataSource = DataT
                '    tblMetas.DataBind()
                '    tblMetas.UseAccessibleHeader = True
                '    tblMetas.HeaderRow.TableSection = TableRowSection.TableHeader
                'Else
                '    tblMetas.DataSource = Nothing
                '    tblMetas.DataBind()
                'End If
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
            parametrizacion.deleteLevels(Session("idNivel"), "I")
            Session("idNivel") = Nothing
            cargarNiveles(lblPac.Text.Trim)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarPlanAcc_Click(sender As Object, e As EventArgs) Handles eliminarPlanAcc.Click
        Try
            parametrizacion.deleteContents(Session("idPlanAcc"), "I")
            Session("idPlanAcc") = Nothing
            cargarPlanAccion(lblPac.Text.Trim)
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
    Public Sub cargarLineas()
        Try
            Fila = Nothing
            Fila = parametrizacion.selectPacActivo
            If Fila IsNot Nothing Then
                'pac.Text = Fila("id")
                DataT = Nothing
                DataT = parametrizacion.selectNiveles(lblPac.Text.Trim)
                If DataT.Rows.Count > 0 Then
                    cmbLineas.Items.Clear()
                    cmbLineas.DataTextField = "name"
                    cmbLineas.DataValueField = "code"
                    cmbLineas.DataSource = DataT
                    cmbLineas.DataBind()
                    cmbLineas.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    cmbLineasMeta.Items.Clear()
                    cmbLineasMeta.DataTextField = "name"
                    cmbLineasMeta.DataValueField = "code"
                    cmbLineasMeta.DataSource = DataT
                    cmbLineasMeta.DataBind()
                    cmbLineasMeta.Items.Insert(0, New ListItem("---Seleccione---", ""))
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub limpiarForm()
        Try
            lblPac.Text = String.Empty
            txtNomPac.Text = String.Empty
            txtSlogan.Text = String.Empty
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
        cmbSubActividad.SelectedIndex = 0
        txtLineaBaseMeta.Text = String.Empty
        txtPriYearMeta.Text = String.Empty
        txtSegYearMeta.Text = String.Empty
        txtTerYearMeta.Text = String.Empty
        txtCuaYearMeta.Text = String.Empty
        cmbResponsable.SelectedIndex = 0
        cmbAlimentador.SelectedIndex = 0
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



#End Region

End Class