Public Class DetallePac
    Inherits System.Web.UI.Page

    Dim reportPac As New clReportPac
    Dim parametrizacion As New clParametrizacion
    Dim login As New clLogin
    Dim fun As New Funciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.QueryString("meta") <> String.Empty And Request.QueryString("pac") <> String.Empty Then
                    Session("CodPac") = Request.QueryString("pac")
                    Session("CodMeta") = Request.QueryString("meta")
                    Response.Redirect("detallepac.aspx")
                End If
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

            lblPac.Text = Session("CodPac")
            lblCodMeta.Text = Session("CodMeta")

            If lblPac.Text <> String.Empty And lblCodMeta.Text <> String.Empty Then
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

                    Fila3 = parametrizacion.selectGoalsFila(lblCodMeta.Text.Trim)
                    If Fila3 IsNot Nothing Then
                        pnlDescripcionJerarquia.Controls.Add(New LiteralControl("<b>Meta: </b> " & Fila3("name") & ""))

                        lblMetaOneYear.Text = "Meta " & Fila("initial_year")
                        lblMetaTwoYear.Text = "Meta " & (CInt(Fila("initial_year")) + 1)
                        lblMetaThreeYear.Text = "Meta " & (CInt(Fila("final_year")) - 1)
                        lblMetaFourYear.Text = "Meta " & Fila("final_year")

                        Dim porcentajeOne, porcentajeTwo, porcentajeThree, porcentajeFour As Double

                        If Not IsDBNull(Fila3("progress_one_year")) Then
                            porcentajeOne = (CDbl(Fila3("progress_one_year")) / CDbl(Fila3("value_one_year"))) * 100
                            If porcentajeOne > 100 Then
                                porcentajeOne = 100
                            End If
                        End If
                        If Not IsDBNull(Fila3("progress_two_year")) Then
                            porcentajeTwo = (CDbl(Fila3("progress_two_year")) / CDbl(Fila3("value_two_year"))) * 100
                            If porcentajeTwo > 100 Then
                                porcentajeTwo = 100
                            End If
                        End If
                        If Not IsDBNull(Fila3("progress_three_year")) Then
                            porcentajeThree = (CDbl(Fila3("progress_three_year")) / CDbl(Fila3("value_three_year"))) * 100
                            If porcentajeThree > 100 Then
                                porcentajeThree = 100
                            End If
                        End If
                        If Not IsDBNull(Fila3("progress_four_year")) Then
                            porcentajeFour = (CDbl(Fila3("progress_four_year")) / CDbl(Fila3("value_four_year"))) * 100
                            If porcentajeFour > 100 Then
                                porcentajeFour = 100
                            End If
                        End If

                        progressbarEjecucionOne.Attributes.Add("style", "width: " & porcentajeOne & "%; aria-valuenow=""25""; aria-valuemin=""0""; aria-valuemax=""100""; ")
                        lblValorProgressOne.Text = porcentajeOne & "%"

                        progressbarEjecucionTwo.Attributes.Add("style", "width: " & porcentajeTwo & "%; aria-valuenow=""25""; aria-valuemin=""0""; aria-valuemax=""100""; ")
                        lblValorProgressTwo.Text = porcentajeTwo & "%"

                        progressbarEjecucionThree.Attributes.Add("style", "width: " & porcentajeThree & "%; aria-valuenow=""25""; aria-valuemin=""0""; aria-valuemax=""100""; ")
                        lblValorProgressThree.Text = porcentajeThree & "%"

                        progressbarEjecucionFour.Attributes.Add("style", "width: " & porcentajeFour & "%; aria-valuenow=""25""; aria-valuemin=""0""; aria-valuemax=""100""; ")
                        lblValorProgressFour.Text = porcentajeFour & "%"

                    End If

                    DataT = Nothing
                    DataT = parametrizacion.selectReport(lblCodMeta.Text.Trim)
                    If DataT.Rows.Count > 0 Then
                        i = 0
                        For Each row As DataRow In DataT.Rows

                            pnlAvances.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                            <a class=""card-report-2"" data-toggle=""collapse"" href=""#avc-" & i & """ role=""button"" aria-expanded=""false"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                <div class=""card-header-report"">
                                                                                    <div class=""row"">
                                                                                        <div class=""col-12"">
                                                                                            <h5 class=""mb-0"">
                                                                                                <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                                                                                                    " & row("fecha") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                                </button>
                                                                                            </h5>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class=""collapse"" id=""avc-" & i & """>
                                                                                    <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem; padding: 0rem;"">
                                                                                        <div class=""card-body"">
                                                                                            <div class=""row"">
                                                                                                <div class=""col-12 mt-2"">  
                                                                                                    " & row("fecha") & " - Consolida Información: " & login.selectEmpleados(row("who_report")) & ".  
                                                                                                    Carga Información: " & login.selectEmpleados(row("user_reg")) & vbCrLf & row("activities_developed") & "
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </a>
                                                                        </div>
                                                                        "))

                            i += 1
                        Next
                    Else
                        pnlAvances.Controls.Add(New LiteralControl("No se ha ejecutado ninguna actividad"))
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
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "$(window).on('load', function () {$('#mdlVisualizador').modal('show');});", True)
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