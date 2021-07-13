Public Class Pac
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim reportPac As New clReportPac

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Visible = False
                lblPac.Visible = False
                pnlNiv2.Visible = False
                pnlNiv3.Visible = False
                pnlNiv4.Visible = False
                DataT = Nothing
                DataT = parametrizacion.selectPacTodos()
                If DataT.Rows.Count > 0 Then
                    cmbPac.Items.Clear()
                    cmbPac.DataTextField = "name"
                    cmbPac.DataValueField = "id"
                    cmbPac.DataSource = DataT
                    cmbPac.DataBind()
                    cmbPac.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    cmbPac.SelectedIndex = 1
                    cmbPac_SelectedIndexChanged(Nothing, Nothing)
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Click"
    Private Sub btnConsultarGeneral_Click(sender As Object, e As EventArgs) Handles btnConsultarGeneral.Click
        Try
            Dim dataT2 As DataTable
            Dim arrayCode, jerarquia, subLevel, script As String
            Dim i As Integer = 0
            Dim i2 As Integer = 0
            Dim array() As Char

            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info", "cmbPac")
                Exit Sub
            End If
            If cmbNivel.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione un nivel", "info", "cmbNivel")
                Exit Sub
            End If

            pnlResultados.Controls.Clear()
            pnlResultados.Controls.Add(New LiteralControl("<div Class=""col-xs-12 col-md-12"">
                                                               <div class=""card-report"">
                                                                   <div class=""card-body"">
                                                                       <div class=""row"">
                                                                           <div class=""col-xs-6 col-md-2"">
                                                                               <h3>Resultados</h3>
                                                                           </div>
                                                                           <div class=""col-xs-6 col-md-10"">
                                                                               <hr style=""border-top: 3px solid rgba(0, 0, 0, .1);"" />
                                                                           </div>                                                                        
                                                          "))


            DataT = Nothing
            If cmbNivel.SelectedValue = "1" Then
                DataT = reportPac.selectLineasFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue, "S")
            Else
                DataT = reportPac.selectLineasFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue)
            End If

            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    Fila = Nothing
                    Fila = reportPac.selectLineasFila(Mid(CStr(row("sublevel")), 1, 1), cmbPac.SelectedValue)
                    If Fila IsNot Nothing Then
                        pnlResultados.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                       <a class=""card-report-2"" data-toggle=""collapse"" href=""#rpt-" & Fila("code") & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"">
                                                                           <div class=""card-header-report"" id=""headingOne"">
                                                                               <div class=""row"">
                                                                                   <div class=""col-12"">
                                                                                       <h5 class=""mb-0"">
                                                                                           <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                               " & Fila("code") & " - " & Fila("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                           </button>
                                                                                       </h5>
                                                                                   </div>
                                                                               </div>
                                                                           </div>
                                                                       </a>
                                                                   </div>
                                                                   <div class=""col-12"">
                                                                       <div class=""collapse"" id=""rpt-" & Fila("code") & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))
                    End If



                    Dim code As String
                    'dataT2 = parametrizacion.selectGoalsFiltro(cmbPac.SelectedValue, row("code"))
                    dataT2 = reportPac.selectContentsFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue, Fila("code"))
                    If dataT2.Rows.Count > 0 Then
                        For Each row2 As DataRow In dataT2.Rows
                            arrayCode = CStr(row2("code")).Replace(".", "")
                            array = arrayCode.ToCharArray

                            script = String.Empty
                            If arrayCode <> String.Empty Then
                                For Each valor In array
                                    If i = 0 Then
                                        jerarquia = valor
                                        subLevel = String.Empty
                                    Else
                                        jerarquia &= "." & valor
                                        subLevel = jerarquia
                                        subLevel = Mid(subLevel, 1, Len(subLevel) - 2)
                                    End If
                                    Fila = Nothing
                                    Fila = reportPac.selectContentsReport(cmbPac.SelectedValue, jerarquia, subLevel)
                                    If Fila IsNot Nothing Then
                                        script &= "<b>" & Fila("name_level") & ": </b>" & Fila("name") & " <br/>"
                                    End If
                                    i += 1
                                Next
                            Else
                                If row("code") = row2("code") Then
                                    script &= "<b>" & row2("name_level") & ": </b>" & row2("name") & " <br/>"
                                End If

                            End If

                            i = 0
                            jerarquia = String.Empty
                            subLevel = String.Empty

                            If arrayCode <> String.Empty Then
                                pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""150""/>
                                                                                               <h5 class=""mb-0"">
                                                                                                   <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                                       <b>" & row2("name_level") & ": </b>" & row2("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                                   </button>
                                                                                               </h5>
                                                                                           </div>
                                                                                           <div class=""col-12"">
                                                                                               <div class=""collapse"" id=""rptSub-" & i2 & """>
                                                                                                   <div class=""card-report mb-3"" style=""margin-top: 0.4rem;"">
                                                                                                       <div class=""card-body"">
                                                                                                           <div class=""row""> 
                                                                                                                <div class=""col-12"">
                                                                                                                    " & script & "
                                                                                                                    <br/>
                                                                                                                    <label>Ver más</label>
                                                                                                                </div> 
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div>                                                                        
                                                                                               </div>
                                                                                           </div><br/>
                                                                                       </div>
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                            Else
                                If row("code") = row2("code") Then
                                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""150""/>
                                                                                               <h5 class=""mb-0"">
                                                                                                   <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                                       <b>" & row2("name_level") & ": </b> " & row2("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                                   </button>
                                                                                               </h5>
                                                                                           </div>
                                                                                           <div class=""col-12"">
                                                                                               <div class=""collapse"" id=""rptSub-" & i2 & """>
                                                                                                   <div class=""card-report mb-3"" style=""margin-top: 0.4rem;"">
                                                                                                       <div class=""card-body"">
                                                                                                           <div class=""row""> 
                                                                                                               <div class=""col-12"">
                                                                                                                    " & script & "
                                                                                                                    <br/>
                                                                                                                    <label>Ver más</label>
                                                                                                               </div>                                                                                                                                                                                                                        
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div>                                                                        
                                                                                               </div>
                                                                                           </div><br/>
                                                                                       </div>
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                                End If
                            End If


                            i2 += 1
                        Next
                    Else
                        pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado datos </label>"))
                    End If

                    pnlResultados.Controls.Add(New LiteralControl("</div>
                                                                       </div>
                                                                           </div>                                                                        
                                                                               </div>
                                                                                   </div><br/>"))
                Next
            End If
            pnlResultados.Controls.Add(New LiteralControl("</div>
                                                               </div>
                                                                   </div>
                                                                       </div>"))
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            Dim dataT2 As DataTable
            Dim arrayCode, jerarquia, subLevel, script As String
            Dim i As Integer = 0
            Dim i2 As Integer = 0
            Dim array() As Char

            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info", "cmbPac")
                Exit Sub
            End If

            pnlResultados.Controls.Clear()
            pnlResultados.Controls.Add(New LiteralControl("<div Class=""col-xs-12 col-md-12"">
                                                               <div class=""card-report"">
                                                                   <div class=""card-body"">
                                                                       <div class=""row"">
                                                                           <div class=""col-xs-6 col-md-2"">
                                                                               <h3>Resultados</h3>
                                                                           </div>
                                                                           <div class=""col-xs-6 col-md-10"">
                                                                               <hr style=""border-top: 3px solid rgba(0, 0, 0, .1);"" />
                                                                           </div>                                                                        
                                                          "))

            Dim code, level_id As String
            If cmbLineas.SelectedIndex > 0 Then
                level_id = "1"
                If cmbNiv2.SelectedIndex > 0 Then
                    level_id = "2"
                    If cmbNiv3.SelectedIndex > 0 Then
                        level_id = "3"
                        If cmbNiv4.SelectedIndex > 0 Then
                            level_id = "4"
                            code = cmbNiv4.SelectedValue
                        Else
                            code = cmbNiv3.SelectedValue
                            level_id = "4"
                        End If
                    Else
                        level_id = "3"
                        code = cmbNiv2.SelectedValue
                    End If
                Else
                    level_id = "2"
                    code = cmbLineas.SelectedValue
                End If
            Else
                level_id = "1"
                code = String.Empty
            End If
            DataT = Nothing
            DataT = reportPac.selectLineas(cmbLineas.SelectedValue, cmbPac.SelectedValue)
            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                       <a class=""card-report-2"" data-toggle=""collapse"" href=""#rpt-" & row("code") & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"">
                                                                           <div class=""card-header-report"" id=""headingOne"">
                                                                               <div class=""row"">
                                                                                   <div class=""col-12"">
                                                                                       <h5 class=""mb-0"">
                                                                                           <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                               " & row("code") & " - " & row("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                           </button>
                                                                                       </h5>
                                                                                   </div>
                                                                               </div>
                                                                           </div>
                                                                       </a>
                                                                   </div>
                                                                   <div class=""col-12"">
                                                                       <div class=""collapse"" id=""rpt-" & row("code") & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))

                    'dataT2 = parametrizacion.selectGoalsFiltro(cmbPac.SelectedValue, row("code"))
                    dataT2 = reportPac.selectContentsFiltro(cmbPac.SelectedValue, code, level_id)
                    If dataT2.Rows.Count > 0 Then
                        For Each row2 As DataRow In dataT2.Rows
                            arrayCode = code.Replace(".", "")
                            array = arrayCode.ToCharArray

                            script = String.Empty
                            If arrayCode <> String.Empty Then
                                For Each valor In array
                                    If i = 0 Then
                                        jerarquia = valor
                                        subLevel = String.Empty
                                    Else
                                        jerarquia &= "." & valor
                                        subLevel = jerarquia
                                        subLevel = Mid(subLevel, 1, Len(subLevel) - 2)
                                    End If
                                    Fila = Nothing
                                    Fila = reportPac.selectContentsReport(cmbPac.SelectedValue, jerarquia, subLevel)
                                    If Fila IsNot Nothing Then
                                        script &= "<b>" & Fila("name_level") & ": </b>" & Fila("name") & " <br/>"
                                    End If
                                    i += 1
                                Next
                            Else
                                If row("code") = row2("code") Then
                                    script &= "<b>" & row2("name_level") & ": </b>" & row2("name") & " <br/>"
                                End If

                            End If

                                i = 0
                            jerarquia = String.Empty
                            subLevel = String.Empty

                            If arrayCode <> String.Empty Then
                                pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""150""/>
                                                                                               <h5 class=""mb-0"">
                                                                                                   <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                                       <b>" & row2("name_level") & ": </b>" & row2("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                                   </button>
                                                                                               </h5>
                                                                                           </div>
                                                                                           <div class=""col-12"">
                                                                                               <div class=""collapse"" id=""rptSub-" & i2 & """>
                                                                                                   <div class=""card-report mb-3"" style=""margin-top: 0.4rem;"">
                                                                                                       <div class=""card-body"">
                                                                                                           <div class=""row""> 
                                                                                                                <div class=""col-12"">
                                                                                                                    " & script & "
                                                                                                                    <br/>
                                                                                                                    <label>Ver más</label>
                                                                                                                </div> 
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div>                                                                        
                                                                                               </div>
                                                                                           </div><br/>
                                                                                       </div>
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                            Else
                                If row("code") = row2("code") Then
                                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""150""/>
                                                                                               <h5 class=""mb-0"">
                                                                                                   <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                                       <b>" & row2("name_level") & ": </b> " & row2("name") & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                                   </button>
                                                                                               </h5>
                                                                                           </div>
                                                                                           <div class=""col-12"">
                                                                                               <div class=""collapse"" id=""rptSub-" & i2 & """>
                                                                                                   <div class=""card-report mb-3"" style=""margin-top: 0.4rem;"">
                                                                                                       <div class=""card-body"">
                                                                                                           <div class=""row""> 
                                                                                                               <div class=""col-12"">
                                                                                                                    " & script & "
                                                                                                                    <br/>
                                                                                                                    <label>Ver más</label>
                                                                                                               </div>                                                                                                                                                                                                                        
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div>                                                                        
                                                                                               </div>
                                                                                           </div><br/>
                                                                                       </div>
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                                End If
                            End If


                            i2 += 1
                        Next
                    Else
                        pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado jerarquia</label>"))
                    End If

                    pnlResultados.Controls.Add(New LiteralControl("</div>
                                                                       </div>
                                                                           </div>                                                                        
                                                                               </div>
                                                                                   </div><br/>"))
                Next
            End If
            pnlResultados.Controls.Add(New LiteralControl("</div>
                                                               </div>
                                                                   </div>
                                                                       </div>"))
            'pnlResultados.Controls.Add(New LiteralControl("<li class=""nav-item"">"))
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "SelectedIndexChanged"
    Private Sub cmbPac_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPac.SelectedIndexChanged
        Try
            cargarLineas()
            cargarNiveles()
            cmbLineas.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub cmbLineas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineas.SelectedIndexChanged
        Try
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Card", "document.getElementById('fltAvanzado').className='show'", True)
            DataT = Nothing
            If cmbLineas.SelectedIndex = 0 Then
                cmbNiv2.Items.Clear()
                pnlNiv2.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue, cmbLineas.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv2.Items.Clear()
                    cmbNiv2.DataTextField = "name"
                    cmbNiv2.DataValueField = "code"
                    cmbNiv2.DataSource = DataT
                    cmbNiv2.DataBind()
                    cmbNiv2.Items.Insert(0, New ListItem("Todos", ""))
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

    Private Sub cmbNiv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2.SelectedIndexChanged
        Try
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Card", "document.getElementById('fltAvanzado').className='show'", True)
            DataT = Nothing
            If cmbNiv2.SelectedIndex = 0 Then
                cmbNiv3.Items.Clear()
                pnlNiv3.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue, cmbNiv2.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv3.Items.Clear()
                    cmbNiv3.DataTextField = "name"
                    cmbNiv3.DataValueField = "code"
                    cmbNiv3.DataSource = DataT
                    cmbNiv3.DataBind()
                    cmbNiv3.Items.Insert(0, New ListItem("Todos", ""))
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

    Private Sub cmbNiv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3.SelectedIndexChanged
        Try
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "Card", "document.getElementById('fltAvanzado').className='show'", True)
            DataT = Nothing
            If cmbNiv3.SelectedIndex = 0 Then
                cmbNiv4.Items.Clear()
                pnlNiv4.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue, cmbNiv3.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    cmbNiv4.Items.Clear()
                    cmbNiv4.DataTextField = "name"
                    cmbNiv4.DataValueField = "code"
                    cmbNiv4.DataSource = DataT
                    cmbNiv4.DataBind()
                    cmbNiv4.Items.Insert(0, New ListItem("Todos", ""))
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
#End Region

#Region "Metodos - Funciones"
    Public Sub cargarLineas()
        Try
            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info")
                Exit Sub
            End If
            DataT = Nothing
            DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue)
            If DataT.Rows.Count > 0 Then
                cmbLineas.Items.Clear()
                cmbLineas.DataTextField = "name"
                cmbLineas.DataValueField = "code"
                cmbLineas.DataSource = DataT
                cmbLineas.DataBind()
                cmbLineas.Items.Insert(0, New ListItem("---Seleccione---", ""))
            Else
                cmbLineas.Items.Clear()
            End If


        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub cargarNiveles()
        Try
            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info")
                Exit Sub
            End If
            DataT = Nothing
            DataT = parametrizacion.selectLevels(cmbPac.SelectedValue, "hierarchy")
            If DataT.Rows.Count > 0 Then
                cmbNivel.Items.Clear()
                cmbNivel.DataTextField = "name"
                cmbNivel.DataValueField = "hierarchy"
                cmbNivel.DataSource = DataT
                cmbNivel.DataBind()
                cmbNivel.Items.Insert(0, New ListItem("-Selecione un nivel-", ""))
            Else
                cmbNivel.Items.Clear()
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