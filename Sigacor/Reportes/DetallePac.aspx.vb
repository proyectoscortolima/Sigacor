Public Class DetallePac
    Inherits System.Web.UI.Page

    Dim reportPac As New clReportPac
    Dim parametrizacion As New clParametrizacion
    Dim login As New clLogin

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.QueryString("meta") <> String.Empty And Request.QueryString("pac") <> String.Empty Then
                    Session("CodPac") = Request.QueryString("pac")
                    Session("CodMeta") = Request.QueryString("meta")
                    Response.Redirect("detallepac.aspx")
                End If

                lblPac.Text = Session("CodPac")
                lblCodMeta.Text = Session("CodMeta")

                DataT = Nothing
                Dim Fila2, Fila3 As DataRow
                Dim Datat2 As New DataTable
                Dim valor, valor2, subLevel As String
                Dim i As Integer = 0
                Dim delimitadores() As String = {"."}
                Dim vectoraux() As String

                Fila = Nothing
                Fila = parametrizacion.selectGoalsFila(lblCodMeta.Text.Trim)
                If Fila IsNot Nothing Then
                    valor = Fila("subactivity")
                End If

                vectoraux = valor.Split(delimitadores, StringSplitOptions.None)

                Fila2 = Nothing
                Datat2.Columns.Add("code")
                Datat2.Columns.Add("name")
                Datat2.Columns.Add("name_level")
                For Each row As String In vectoraux
                    If i = 0 Then
                        valor2 = row
                        subLevel = String.Empty
                    Else
                        valor2 &= "." & row
                        subLevel = valor2
                        subLevel = Mid(subLevel, 1, Len(subLevel) - 2)
                    End If
                    Fila2 = Datat2.NewRow()


                    Fila = Nothing
                    Fila = reportPac.selectContentsReport(lblPac.Text.Trim, valor2, subLevel)
                    If Fila IsNot Nothing Then
                        Fila2("code") = Fila("code")
                        Fila2("name") = Fila("name")
                        Fila2("name_level") = Fila("name_level")
                        Datat2.Rows.Add(Fila2)
                    End If

                    i += 1
                Next

                pnlDescripcionJerarquia.Controls.Clear()
                Fila = Nothing
                Fila = parametrizacion.selectPac(lblPac.Text.Trim)
                If Fila IsNot Nothing Then
                    lblNomPac.Text = Fila("name")
                    pnlDescripcionJerarquia.Controls.Add(New LiteralControl("<b>Periodo: </b> " & Fila("initial_year") & " - " & Fila("final_year") & " "))
                    For Each row As DataRow In Datat2.Rows
                        pnlDescripcionJerarquia.Controls.Add(New LiteralControl("<b>" & row("name_level") & ":</b> " & row("name") & " "))
                    Next

                    Dim year_initial, index As Integer
                    Fila3 = parametrizacion.selectGoalsFila(lblCodMeta.Text.Trim)
                    If Fila3 IsNot Nothing Then
                        pnlDescripcionJerarquia.Controls.Add(New LiteralControl("<b>Meta: </b> " & Fila3("name") & ""))
                        lblLineaBase.Text = Fila3("line_base")

                        year_initial = CInt(Fila("initial_year"))
                        For i2 As Integer = 1 To CInt(Fila("number_years"))
                            If year_initial = CInt(Date.Now.ToString("yyyy")) Then
                                index = i2
                            End If
                            year_initial += 1
                        Next
                        Select Case index
                            Case 1
                                lblMeta.Text = Fila3("value_one_year")
                            Case 2
                                lblMeta.Text = Fila3("value_two_year")
                            Case 3
                                lblMeta.Text = Fila3("value_three_year")
                            Case 4
                                lblMeta.Text = Fila3("value_four_year")
                        End Select



                        progressbarEjecucion.Attributes.Add("style", "width: " & Fila3("value_progress") & "%; aria-valuenow=""25""; aria-valuemin=""0""; aria-valuemax=""100""; ")
                        lblValorProgress.Text = Fila3("value_progress") & "%"
                    End If

                    DataT = Nothing
                    DataT = parametrizacion.selectReport(lblCodMeta.Text.Trim)
                    If DataT.Rows.Count > 0 Then
                        i = 0
                        For Each row As DataRow In DataT.Rows
                            If i > 0 Then
                                txtAvances.Text &= vbCrLf & "---------------------------------------"
                            End If
                            txtAvances.Text &= row("fecha") & ": " & "Consolida Información: " & login.selectEmpleados(row("who_report")) & ". Carga Información: " &
                                              login.selectEmpleados(row("user_reg")) & vbCrLf & row("activities_developed")
                        Next
                    Else
                        txtAvances.Text = "No se ha ejecutado ninguna actividad"
                    End If

                End If

            End If


        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnVisualizarHojaVida_Click(sender As Object, e As EventArgs) Handles btnVisualizarHojaVida.Click
        Try
            If lblCodMeta.Text <> String.Empty Then
                Fila = Nothing
                Fila = parametrizacion.selectCurriculum(lblCodMeta.Text)
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
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModal();", True)
                Else
                    alerta("Advertencia", "La meta no tiene la hoja de vida grabada", "info")
                End If
            Else
                alerta("Advertencia", "Debe seleccionar una meta", "info")
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

End Class