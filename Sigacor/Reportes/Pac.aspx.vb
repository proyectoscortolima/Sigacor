﻿Public Class Pac
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim reportPac As New clReportPac

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Visible = False
                lblPac.Visible = False
                pnlNiv6.Visible = False
                DataT = Nothing
                DataT = parametrizacion.selectPacTodos()
                If DataT.Rows.Count > 0 Then
                    cmbPac.Items.Clear()
                    cmbPac.DataTextField = "name"
                    cmbPac.DataValueField = "id"
                    cmbPac.DataSource = DataT
                    cmbPac.DataBind()
                    cmbPac.Items.Insert(0, New ListItem("Todos", ""))
                    cmbPac.SelectedIndex = 1
                    cmbPac_SelectedIndexChanged(Nothing, Nothing)
                End If

                Dim dt As DataTable = ViewState("Quantity")
                Me.QuantityDinamicControls = dt
                GenerateControls()
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
            Dim delimitadores() As String = {"."}
            Dim vectoraux() As String
            Dim contador = 0

            Dim dataNiveles As DataTable = parametrizacion.selectLevels(cmbPac.SelectedValue, "hierarchy")

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
                                                          "))

            DataT = Nothing
            If cmbNivel.SelectedValue = "1" Then
                DataT = reportPac.selectLineasFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue, "S")
            ElseIf cmbNivel.SelectedValue = dataNiveles.Rows.Count + 1 Then
                DataT = reportPac.selectGoals(cmbPac.SelectedValue)
                If DataT.Rows.Count > 0 Then
                    Dim vectorHistorial(DataT.Rows.Count - 1) As String
                    For Each row As DataRow In DataT.Rows
                        vectoraux = row("sublevel").Split(delimitadores, StringSplitOptions.None)
                        vectorHistorial(contador) = CStr(vectoraux(0))
                        contador += 1
                    Next

                    vectoraux = DeleteArrayRepetitions(vectorHistorial, True)
                    DataT = reportPac.selectLineasFiltroGeneralMetas(cmbPac.SelectedValue, vectoraux)
                End If
            Else
                DataT = reportPac.selectLineasFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue)
            End If

            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    Fila = Nothing
                    Fila = reportPac.selectLineasFila(row("code"), cmbPac.SelectedValue)
                    If Fila IsNot Nothing Then
                        pnlResultados.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                       <a class=""card-report-2"" data-toggle=""collapse"" href=""#rpt-" & Fila("code") & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"">
                                                                           <div class=""card-header-report"" id=""headingOne"">
                                                                               <div class=""row"" style=""justify-content: center;"">
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
                                                                       <div class=""collapse show"" id=""rpt-" & Fila("code") & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))
                    End If



                    Dim code, botonRedireccionar As String
                    Dim valueProgress As Integer
                    If cmbNivel.SelectedValue = dataNiveles.Rows.Count + 1 Then
                        dataT2 = reportPac.selectGoalsFiltroGeneral(cmbPac.SelectedValue, Fila("code"))
                    Else
                        dataT2 = reportPac.selectContentsFiltroGeneral(cmbPac.SelectedValue, cmbNivel.SelectedValue, Fila("code"))
                    End If
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

                            If cmbNivel.SelectedValue = dataNiveles.Rows.Count + 1 Then
                                botonRedireccionar = "<a href=""detallepac.aspx?meta=" & row2("id") & "&pac=" & row2("pac_id") & """>Leer más</a>"
                            Else
                                botonRedireccionar = String.Empty
                            End If

                            i = 0
                            jerarquia = String.Empty
                            subLevel = String.Empty

                            If Not IsDBNull(row2("value_progress")) Then
                                If row2("value_progress") <> String.Empty Then
                                    valueProgress = row2("value_progress")
                                Else
                                    valueProgress = 0
                                End If
                            Else
                                valueProgress = 0
                            End If

                            If arrayCode <> String.Empty Then
                                pnlResultados.Controls.Add(New LiteralControl("<div class=""col-2"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"" style=""justify-content: center;"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""" & cargarImagenConveciones(valueProgress) & """ width=""70""/>
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
                                                                                                                </div>                                                                                                                 
                                                                                                           </div>                                                                                                                 
                                                                                                       </div>                                                                                                    
                                                                                                   </div> 
                                                                                                   " & botonRedireccionar & "                  
                                                                                               </div>                                                                                                
                                                                                           </div>                                                                                           
                                                                                       </div>                                                                                                              
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                            Else
                                If row("code") = row2("code") Then
                                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-2"">
                                                                                <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none; border:none !important; "">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"" style=""justify-content: center;"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""" & cargarImagenConveciones(valueProgress) & """ width=""70""/>
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
                                                                                                               </div>                                                                                                                                                                                                                        
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div> 
                                                                                                   " & botonRedireccionar & "                 
                                                                                               </div>
                                                                                           </div>                                                                                              
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
            Else
                pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado datos </label>"))
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

            Dim dataNiveles As DataTable = parametrizacion.selectLevels(cmbPac.SelectedValue, "hierarchy")

            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info", "cmbPac")
                Exit Sub
            End If

            pnlResultados.Controls.Clear()
            pnlResultados.Controls.Add(New LiteralControl("<div Class=""col-xs-12 col-md-12"">
                                                               <div class=""card-report"">
                                                                   <div class=""card-body"">
                                                                       <div class=""row"">                                                                          
                                                          "))

            Dim code, level_id As String
            If (QuantityDinamicControls.Rows.Count > 0) Then
                Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 1)
                If IsDBNull(lastRow("sublevel")) Then
                    code = String.Empty
                Else
                    code = lastRow("sublevel")
                End If
                If IsDBNull(lastRow("level")) Then
                    level_id = String.Empty
                Else
                    level_id = lastRow("level")
                End If
            End If

            Dim control As DropDownList
            Dim linea As String = String.Empty

            control = TryCast(phDinamicControls.FindControl("cmbNivel-" & QuantityDinamicControls(0)(1)), DropDownList)
            If control IsNot Nothing Then
                linea = control.SelectedValue
            End If

            DataT = Nothing
            DataT = reportPac.selectLineas(linea, cmbPac.SelectedValue)
            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                       <a class=""card-report-2"" data-toggle=""collapse"" href=""#rpt-" & row("code") & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"">
                                                                           <div class=""card-header-report"" id=""headingOne"">
                                                                               <div class=""row"" style=""justify-content: center;"">
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
                                                                       <div class=""collapse show"" id=""rpt-" & row("code") & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))

                    If level_id = dataNiveles.Rows.Count + 1 Then
                        dataT2 = reportPac.selectGoalsFiltroGeneral(cmbPac.SelectedValue, code)
                    Else
                        dataT2 = reportPac.selectContentsFiltro(cmbPac.SelectedValue, code, level_id)
                    End If

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
                                pnlResultados.Controls.Add(New LiteralControl("<div class=""col-2"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"" style=""justify-content: center;"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""70""/>
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
                                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-2"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"" style=""justify-content: center;"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""70""/>
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
            Else
                pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado jerarquia</label>"))
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
    Private Sub btnConsultarPalabraClave_Click(sender As Object, e As EventArgs) Handles btnConsultarPalabraClave.Click
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
            If txtPalabraClave.Text = String.Empty Then
                alerta("Advertencia", "Ingrese una palabra clave", "info", "txtPalabraClave")
                Exit Sub
            End If

            pnlResultados.Controls.Clear()
            pnlResultados.Controls.Add(New LiteralControl("<div Class=""col-xs-12 col-md-12"">
                                                               <div class=""card-report"">
                                                                   <div class=""card-body"">
                                                                       <div class=""row"">"))

            Dim codeLinea, nombreLinea, arrayCodeLinea, lineaGlobal As String
            Dim contador As Integer = 0
            DataT = Nothing
            DataT = reportPac.selectPalabraClave(txtPalabraClave.Text.Trim, cmbPac.SelectedValue)
            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows

                    codeLinea = CStr(row("code")).Replace(".", "")
                    arrayCodeLinea = codeLinea.ToCharArray
                    Fila = reportPac.selectLineasFila(arrayCodeLinea(0), cmbPac.SelectedValue)

                    If Fila IsNot Nothing Then
                        If contador = 0 Then
                            lineaGlobal = Fila("code")
                        End If
                        codeLinea = Fila("code")
                        nombreLinea = Fila("name")
                    End If

                    If lineaGlobal <> codeLinea Or contador = 0 Then
                        lineaGlobal = codeLinea
                        pnlResultados.Controls.Add(New LiteralControl("<div class=""col-12 mt-2""> 
                                                                       <a class=""card-report-2"" data-toggle=""collapse"" href=""#rpt-" & codeLinea & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"">
                                                                           <div class=""card-header-report"" id=""headingOne"">
                                                                               <div class=""row"">
                                                                                   <div class=""col-12"">
                                                                                       <h5 class=""mb-0"">
                                                                                           <button class=""btn"" data-toggle=""collapse"" data-target=""#collapseOne"" aria-expanded=""True"" aria-controls=""collapseOne"">
                                                                                               " & codeLinea & " - " & nombreLinea & " <i class=""fa fa-arrow-down ml-3""></i>
                                                                                           </button>
                                                                                       </h5>
                                                                                   </div>
                                                                               </div>
                                                                           </div>
                                                                       </a>
                                                                   </div>
                                                                   <div class=""col-12"">
                                                                       <div class=""collapse show"" id=""rpt-" & codeLinea & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))



                        Dim code As String
                        dataT2 = reportPac.selectPalabraClave(cmbPac.SelectedValue, codeLinea, txtPalabraClave.Text.Trim)
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
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""70""/>
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
                                                                                               <img src=""../Componentes/img/nvl1.svg"" width=""70""/>
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
                    End If
                    contador += 1
                Next
            Else
                pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado datos </label>"))
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

#End Region

#Region "SelectedIndexChanged"
    Private Sub cmbPac_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPac.SelectedIndexChanged
        Try
            cargarNiveles()
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "Metodos - Funciones"
    Public Function DeleteArrayRepetitions(ByVal strArray1() As String, ByVal Sorted As Boolean) As Array
        Dim strArray2(0) As String
        Dim Count As Integer = 0
        Dim Count2 As Integer = 0
        For Each Element In strArray1
            Dim Last As Integer = Array.LastIndexOf(strArray1, Element)
            If Count = Last Then
                ReDim Preserve strArray2(Count2)
                strArray2(Count2) = Element
                Count2 += 1
            End If
            Count += 1
        Next
        If Sorted = True Then Array.Sort(strArray2)
        Return strArray2
    End Function

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
                cmbNivel.Items.Insert(DataT.Rows.Count + 1, New ListItem("Metas", DataT.Rows.Count + 1))
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

    Protected Sub chkConvenciones_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dataT2 As DataTable
            Dim jerarquia, subLevel, script, botonRedireccionar As String
            Dim i As Integer = 0 
            Dim i2 As Integer = 0
            Dim array() As Char
            Dim delimitadores() As String = {"."}
            Dim vectoraux() As String
            Dim arrayCode() As String
            Dim contador = 0

            If cmbPac.SelectedIndex = 0 Then
                alerta("Advertencia", "Seleccione el periodo", "info", "cmbPac")
                Exit Sub
            End If

            pnlResultados.Controls.Clear()
            pnlResultados.Controls.Add(New LiteralControl("<div Class=""col-xs-12 col-md-12"">
                                                               <div class=""card-report"">
                                                                   <div class=""card-body"">
                                                                       <div class=""row"">                                                                         
                                                          "))

            DataT = Nothing

            DataT = reportPac.selectGoals(True, cmbPac.SelectedValue, chkNoProgramado.Checked, chkEjecMenos25.Checked, chkEjec25Al49.Checked,
                                          chkEjec50Al74.Checked, chkEjec75Al99.Checked, chkEjecMas100.Checked)
            If DataT.Rows.Count > 0 Then
                Dim vectorHistorial(DataT.Rows.Count - 1) As String
                For Each row As DataRow In DataT.Rows
                    vectoraux = row("sublevel").Split(delimitadores, StringSplitOptions.None)
                    vectorHistorial(contador) = CStr(vectoraux(0))
                    contador += 1
                Next

                vectoraux = DeleteArrayRepetitions(vectorHistorial, True)
            End If
            If DataT.Rows.Count > 0 Then
                DataT = reportPac.selectLineasFiltroGeneralMetas(cmbPac.SelectedValue, vectoraux)
            End If
            If DataT.Rows.Count > 0 Then
                For Each row As DataRow In DataT.Rows
                    Fila = Nothing
                    Fila = reportPac.selectLineasFila(row("code"), cmbPac.SelectedValue)
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
                                                                       <div class=""collapse show"" id=""rpt-" & Fila("code") & """>
                                                                           <div class=""card-report card-body mb-3"" style=""margin-top: 0.4rem;"">
                                                                               <div class=""card-body"">
                                                                                   <div class=""row"">

                                                                   "))
                    End If




                    dataT2 = reportPac.selectGoals(False, cmbPac.SelectedValue, chkNoProgramado.Checked, chkEjecMenos25.Checked, chkEjec25Al49.Checked,
                                          chkEjec50Al74.Checked, chkEjec75Al99.Checked, chkEjecMas100.Checked, Fila("code"))
                    If dataT2.Rows.Count > 0 Then
                        For Each row2 As DataRow In dataT2.Rows
                            arrayCode = row2("code").Split(delimitadores, StringSplitOptions.None)

                            script = String.Empty
                            If arrayCode IsNot Nothing Then
                                For Each valor In arrayCode
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

                            botonRedireccionar = "<a href=""detallepac.aspx?meta=" & row2("id") & "&pac=" & row2("pac_id") & """>Leer más</a>"
                            i = 0
                            jerarquia = String.Empty
                            subLevel = String.Empty

                            If arrayCode IsNot Nothing Then
                                pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                               <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none;"">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""" & cargarImagenConveciones(row2("value_progress")) & """ width=""70""/>
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
                                                                                                                </div>                                                                                                                 
                                                                                                           </div>                                                                                                                 
                                                                                                       </div>                                                                                                    
                                                                                                   </div> 
                                                                                                   " & botonRedireccionar & "                  
                                                                                               </div>                                                                                                
                                                                                           </div>                                                                                           
                                                                                       </div>                                                                                                              
                                                                                   </div>
                                                                               </a>                                                                                                                                                           
                                                                           </div>
                                                                           "))
                            Else
                                If row("code") = row2("code") Then
                                    pnlResultados.Controls.Add(New LiteralControl("<div class=""col-3"">
                                                                                <a class=""card-report-2"" data-toggle=""collapse"" href=""#rptSub-" & i2 & """ role=""button"" aria-expanded=""False"" aria-controls=""collapseExample"" style=""text-decoration: none; border:none !important; "">
                                                                                   <div class=""card-header-report"" id=""headingOne"">
                                                                                       <div class=""row"">
                                                                                           <div class=""col-12 text-center"">
                                                                                               <img src=""" & cargarImagenConveciones(row2("value_progress")) & """ width=""70""/>
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
                                                                                                               </div>                                                                                                                                                                                                                        
                                                                                                           </div>
                                                                                                       </div>
                                                                                                   </div> 
                                                                                                   " & botonRedireccionar & "                 
                                                                                               </div>
                                                                                           </div>                                                                                              
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
            Else
                pnlResultados.Controls.Add(New LiteralControl("<label>No se han encontrado datos </label>"))
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

#End Region


    Public Function cargarImagenConveciones(ByVal valueProgress As Integer) As String

        Dim pathIcono As String

        If valueProgress = 0 Then
            pathIcono = "../Componentes/img/nvl1.svg"
        ElseIf valueProgress <= 24 Then
            pathIcono = "../Componentes/img/nvl2.svg"
        ElseIf valueProgress >= 25 And valueProgress <= 49 Then
            pathIcono = "../Componentes/img/nvl3.svg"
        ElseIf valueProgress >= 50 And valueProgress <= 74 Then
            pathIcono = "../Componentes/img/nvl4.svg"
        ElseIf valueProgress >= 75 And valueProgress <= 99 Then
            pathIcono = "../Componentes/img/nvl5.svg"
        ElseIf valueProgress >= 100 Then
            pathIcono = "../Componentes/img/nvl6.svg"
        End If

        Return pathIcono
    End Function


#Region "Init Filtro Dinamicos"
    Protected Overloads Overrides Sub CreateChildControls()
        If Page.IsPostBack Then
            GenerateControls()
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

#End Region

#Region "Fltro Dinamico"

    Private Sub controlNuevo(Optional nivel As String = "")
        Dim nuevoCmb As DropDownList = New DropDownList()
        Dim nuevoPanel As Panel = New Panel()

        DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue, nivel)
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
            nuevoPanel.CssClass = "col-12"
            nuevoPanel.Controls.Add(New LiteralControl("<div class=""form-group"">
                                                        <label>" & DataT(0)(5).ToString() & "</label>"))
            nuevoPanel.Controls.Add(nuevoCmb)
            nuevoPanel.Controls.Add(New LiteralControl("</div>"))

            phDinamicControls.Controls.Add(nuevoPanel)

        Else
            Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 2)
            Fila = parametrizacion.selectLevelsFila(cmbPac.SelectedValue, CInt(lastRow("level")) + 1, "")
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
                    DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue)
                Else
                    DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue, row("sublevel").ToString())
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
                    nuevoPanel.CssClass = "col-12"
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
            DataT = parametrizacion.selectNiveles(cmbPac.SelectedValue)
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
                nuevoPanel.CssClass = "col-12"
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
            DataT2 = parametrizacion.selectNiveles(cmbPac.SelectedValue, nivelControl.SelectedValue)

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
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "Card", "document.getElementById('fltAvanzado').className='show'; document.getElementById('filtro').className='show';", True)
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

End Class