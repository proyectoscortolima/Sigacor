Public Class HojaVida
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim fun As New Funciones

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblCodMeta.Visible = False
                lblError.Visible = False
                pnlHojaVida.Visible = False
                cargarMetas()
                btnActualizar.Visible = False

                DataT = Nothing
                DataT = fun.periodicity()
                If DataT.Rows.Count > 0 Then
                    cmbPeriodicidad.Items.Clear()
                    cmbPeriodicidad.DataTextField = "description"
                    cmbPeriodicidad.DataValueField = "name"
                    cmbPeriodicidad.DataSource = DataT
                    cmbPeriodicidad.DataBind()
                    cmbPeriodicidad.Items.Insert(0, New ListItem("---Seleccione---", ""))
                End If
            End If

            If tblMetass.Rows.Count > 0 Then
                tblMetass.UseAccessibleHeader = True
                tblMetass.HeaderRow.TableSection = TableRowSection.TableHeader
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
#End Region

#Region "RowDataBound"
    Private Sub tblMetass_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblMetass.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn As New LinkButton
                linkBtn = e.Row.FindControl("lnkMeta")

                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
                e.Row.Cells(0).Visible = False
                Fila = Nothing
                Fila = parametrizacion.selectCurriculum(e.Row.Cells(0).Text.Trim)
                If Fila IsNot Nothing Then
                    e.Row.Cells(5).Text = "S"
                Else
                    e.Row.Cells(5).Text = "N"
                End If
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
    Private Sub tblMetass_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblMetass.RowCommand
        Try
            pnlInfoHojaVida.Visible = False
            pnlHojaVida.Visible = True
            lblCodMeta.Text = e.CommandArgument.trim
            cargarHojavida(lblCodMeta.Text.Trim)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Click"
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try

            If txtSiglaHojaVida.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la sigla de la hoja de vida", "info", "contenedor2_txtSiglaHojaVida")
                Exit Sub
            End If
            If txtDefinHojaVida.Text = String.Empty Then
                alerta("Advertencia", "Ingrese la definación de la hoja de vida", "info", "contenedor2_txtDefinHojaVida")
                Exit Sub
            End If
            If txtMetodoMedic.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el método de la hoja de vida", "info", "contenedor2_txtMetodoMedic")
                Exit Sub
            End If
            If txtFormulaHojaVida.Text = String.Empty Then
                alerta("Advertencia", "Ingrese las formulas de la hoja de vida", "info", "contenedor2_txtFormulaHojaVida")
                Exit Sub
            End If
            If txtVariablesHojaVida.Text = String.Empty Then
                alerta("Advertencia", "Ingrese las variables de la hoja de vida", "info", "contenedor2_txtVariablesHojaVida")
                Exit Sub
            End If
            If txtObservHojaVida.Text = String.Empty Then
                alerta("Advertencia", "Ingrese las observaciones de la hoja de vida", "info", "contenedor2_txtObservHojaVida")
                Exit Sub
            End If
            If txtGeografica.Text = String.Empty Then
                alerta("Advertencia", "Ingrese desag. Geográfica de la hoja de vida", "info", "contenedor2_txtGeografica")
                Exit Sub
            End If
            If cmbPeriodicidad.SelectedIndex = 0 Then
                alerta("Advertencia", "Ingrese la periodicidad de la hoja de vida", "info", "contenedor2_cmbPeriodicidad")
                Exit Sub
            End If

            If parametrizacion.updateCurriculum(lblCodMeta.Text.Trim, txtSiglaHojaVida.Text.Trim, txtDescripHojaVida.Text.Trim, txtDefinHojaVida.Text.Trim,
                                                txtMetodoMedic.Text.Trim, txtFormulaHojaVida.Text.Trim, txtVariablesHojaVida.Text.Trim,
                                                txtObservHojaVida.Text.Trim, txtGeografica.Text.Trim, cmbPeriodicidad.SelectedValue.Trim) > 0 Then
                alerta("Se ha actualizado correctamente la hoja de vida", "", "success")
            Else
                parametrizacion.insertCurriculum(lblCodMeta.Text.Trim, txtSiglaHojaVida.Text.Trim, txtDescripHojaVida.Text.Trim, txtDefinHojaVida.Text.Trim,
                                                 txtMetodoMedic.Text.Trim, txtFormulaHojaVida.Text.Trim, txtVariablesHojaVida.Text.Trim,
                                                 txtObservHojaVida.Text.Trim, txtGeografica.Text.Trim, cmbPeriodicidad.SelectedValue.Trim, "A")
                alerta("Se ha grabado correctamente la hoja de vida", "", "success")
            End If

            btnLimpiar_Click(Nothing, Nothing)

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        btnGrabar_Click(Nothing, Nothing)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        Try
            txtSiglaHojaVida.Text = String.Empty
            txtDescripHojaVida.Text = String.Empty
            txtDefinHojaVida.Text = String.Empty
            txtMetodoMedic.Text = String.Empty
            txtFormulaHojaVida.Text = String.Empty
            txtVariablesHojaVida.Text = String.Empty
            txtObservHojaVida.Text = String.Empty
            txtGeografica.Text = String.Empty
            cmbPeriodicidad.SelectedIndex = 0
            btnActualizar.Visible = False
            btnGrabar.Visible = True
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As EventArgs) Handles btnAtras.Click
        Try
            lblCodMeta.Text = String.Empty
            pnlInfoHojaVida.Visible = True
            pnlHojaVida.Visible = False
            cargarMetas()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Metodos - Funciones"
    Public Sub cargarHojavida(ByVal codMeta As String)
        Try
            Fila = Nothing
            Fila = parametrizacion.selectCurriculum(codMeta)
            If Fila IsNot Nothing Then
                txtSiglaHojaVida.Text = Fila("initials")
                txtDescripHojaVida.Text = Fila("description")
                txtDefinHojaVida.Text = Fila("definition")
                txtMetodoMedic.Text = Fila("method")
                txtFormulaHojaVida.Text = Fila("formulas")
                txtVariablesHojaVida.Text = Fila("variables")
                txtObservHojaVida.Text = Fila("observations")
                txtGeografica.Text = Fila("geographic")
                cmbPeriodicidad.SelectedValue = Fila("periodicity")
                btnActualizar.Visible = True
                btnGrabar.Visible = False
            Else
                txtSiglaHojaVida.Text = String.Empty
                txtDescripHojaVida.Text = String.Empty
                txtDefinHojaVida.Text = String.Empty
                txtMetodoMedic.Text = String.Empty
                txtFormulaHojaVida.Text = String.Empty
                txtVariablesHojaVida.Text = String.Empty
                txtObservHojaVida.Text = String.Empty
                txtGeografica.Text = String.Empty
                cmbPeriodicidad.SelectedIndex = 0
                btnActualizar.Visible = False
                btnGrabar.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub cargarMetas()
        Try
            DataT = Nothing
            DataT = parametrizacion.selectGoals()
            If DataT.Rows.Count > 0 Then
                tblMetass.DataSource = DataT
                tblMetass.DataBind()
                tblMetass.UseAccessibleHeader = True
                tblMetass.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblMetass.DataSource = Nothing
                tblMetass.DataBind()
            End If
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


#End Region

End Class