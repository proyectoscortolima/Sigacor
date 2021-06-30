Imports System.IO

Public Class Seguimiento
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim fun As New Funciones

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("idMeta") <> String.Empty Then
                cargarMeta(Request.QueryString("idMeta"))
            End If
            'cargarMetaAgrupado()
            If Not IsPostBack Then
                pnlNiv2.Visible = False
                pnlNiv3.Visible = False
                pnlNiv4.Visible = False
                cargarLineas()

                Session("dataImagenes") = Nothing
                Session("dataAdjuntos") = Nothing

                lblError.Visible = False

                navMetas_Click(Nothing, Nothing)

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

                lblYearActual.Text = "Año actual(" & Date.Now.Year & ")"
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "SelectedIndexChanged"
    Private Sub cmbLineas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineas.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbLineas.SelectedIndex = 0 Then
                cmbNiv2.Items.Clear()
                pnlNiv2.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(pac.Text.Trim, cmbLineas.SelectedValue)
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

    Private Sub cmbNiv2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv2.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv2.SelectedIndex = 0 Then
                cmbNiv3.Items.Clear()
                pnlNiv3.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(pac.Text.Trim, cmbNiv2.SelectedValue)
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

    Private Sub cmbNiv3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNiv3.SelectedIndexChanged
        Try
            DataT = Nothing
            If cmbNiv3.SelectedIndex = 0 Then
                cmbNiv4.Items.Clear()
                pnlNiv4.Visible = False
            Else
                DataT = parametrizacion.selectNiveles(pac.Text.Trim, cmbNiv3.SelectedValue)
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

#End Region

#Region "TextChanged"
    Private Sub txtLinks_TextChanged(sender As Object, e As EventArgs) Handles txtLinks.TextChanged
        Try
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModalAdjutnosLink();", True)
            If Request.QueryString("idMeta") = String.Empty Then
                alerta("Advertencia", "Debe seleccionar la meta", "info", "")
                Exit Sub
            End If
            If idReport.Text = String.Empty Then
                alerta("Advertencia", "Debe guardar primero el reporte y despues adjuntar las evidencias", "info", "")
                Exit Sub
            End If
            parametrizacion.updateAdjuntosReport("array_link", txtLinks.Text.Trim, idReport.Text.Trim)
            alerta("Se han grabado los link correctamente", "", "success", "")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "RowDataBound"
    Private Sub tblArchivos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblArchivos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn As New LinkButton
                linkBtn = e.Row.FindControl("lnkEliminar")
                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblImagenes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblImagenes.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn As New LinkButton
                linkBtn = e.Row.FindControl("lnkEliminar")
                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblMetas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblMetas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn As New LinkButton
                linkBtn = e.Row.FindControl("lnkSeleccionar")
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
    Private Sub tblArchivos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblArchivos.RowCommand
        Try
            Dim i As Integer = 0
            Dim Index As Integer
            Dim arraySeguimiento, filePath As String
            Dim archivo As FileInfo
            Dim linkBtnEliminar As New LinkButton
            Dim dataArchivos As DataTable = Session("dataAdjuntos")

            ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModalAdjutnosArch();", True)

            If e.CommandName = "Eliminar" Then
                arraySeguimiento = "["
                For Each row As DataRow In dataArchivos.Rows
                    If e.CommandArgument = row("nombre") Then
                        filePath = Path.Combine(Request.PhysicalApplicationPath, row("ruta"))
                        Index = i
                    Else
                        arraySeguimiento &= row("ruta") & ", "
                    End If
                    i += 1
                Next

                If arraySeguimiento.Length > 1 Then
                    arraySeguimiento = arraySeguimiento.Substring(0, arraySeguimiento.Length - 2)
                End If
                arraySeguimiento &= "]"

                archivo = New FileInfo(filePath)
                If archivo.Exists Then
                    File.Delete(filePath)
                End If
                dataArchivos.Rows.RemoveAt(Index)

                Session("dataAdjuntos") = dataArchivos
                tblArchivos.DataSource = dataArchivos
                tblArchivos.DataBind()

                parametrizacion.updateAdjuntosReport("array_archivos", arraySeguimiento, idReport.Text.Trim)
                alerta("Se ha eliminado el archivo correctamente", "", "success")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblImagenes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblImagenes.RowCommand
        Try
            Dim i As Integer = 0
            Dim index As Integer = 0
            Dim imagen As FileInfo
            Dim arraySeguimiento, filePath As String
            Dim linkBtnEliminar As New LinkButton
            Dim dataImagenes As DataTable = Session("dataImagenes")

            ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModalAdjutnosImg();", True)

            If e.CommandName = "Eliminar" Then
                arraySeguimiento = "["
                For Each row As DataRow In dataImagenes.Rows
                    If e.CommandArgument = row("nombre") Then
                        filePath = Path.Combine(Request.PhysicalApplicationPath, row("ruta"))
                        index = i
                    Else
                        arraySeguimiento &= row("ruta") & ", "
                    End If
                    i += 1
                Next

                If arraySeguimiento.Length > 1 Then
                    arraySeguimiento = arraySeguimiento.Substring(0, arraySeguimiento.Length - 2)
                End If
                arraySeguimiento &= "]"

                imagen = New FileInfo(filePath)
                If imagen.Exists Then
                    File.Delete(filePath)
                End If

                dataImagenes.Rows.RemoveAt(index)

                Session("dataImagenes") = dataImagenes
                tblArchivos.DataSource = dataImagenes
                tblArchivos.DataBind()

                parametrizacion.updateAdjuntosReport("array_imagenes", arraySeguimiento, idReport.Text.Trim)
                alerta("Se ha eliminado el archivo correctamente", "", "success")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub tblMetas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblMetas.RowCommand
        meta.Text = e.CommandArgument.trim
        cargarMeta(e.CommandArgument.trim)
        navActividades_Click(Nothing, Nothing)
    End Sub

#End Region

#Region "Click"

#Region "Pestaña"

    Private Sub navMetas_Click(sender As Object, e As EventArgs) Handles navMetas.Click
        pnlMetas.Visible = True
        pnlActividades.Visible = False
        pnlInfoMetas.Visible = False
        pnlEvidencias.Visible = False
        pestaña(1)
    End Sub
    Private Sub navActividades_Click(sender As Object, e As EventArgs) Handles navActividades.Click
        If txtNombreMeta.Text = String.Empty Then
            alerta("Advertencia", "Debe seleccionar una meta", "info")
            navMetas_Click(Nothing, Nothing)
            Exit Sub
        End If
        pnlMetas.Visible = False
        pnlActividades.Visible = True
        pnlInfoMetas.Visible = True
        pnlEvidencias.Visible = False
        pestaña(2)
    End Sub

    Private Sub navEvidencias_Click(sender As Object, e As EventArgs) Handles navEvidencias.Click
        If idReport.Text = String.Empty Then
            alerta("Advertencia", "Debe grabar la información de las actividades desarrolladas", "info")
            navActividades_Click(Nothing, Nothing)
            Exit Sub
        End If
        pnlMetas.Visible = False
        pnlActividades.Visible = False
        pnlInfoMetas.Visible = False
        pnlEvidencias.Visible = True
        pestaña(3)
    End Sub

#End Region

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

            DataT = parametrizacion.selectGoalsXsubactivity(pac.Text.Trim, code)
            If DataT.Rows.Count > 0 Then
                tblMetas.DataSource = DataT
                tblMetas.DataBind()

            Else
                alerta("No se encontraron metas", "", "info")
                tblMetas.DataSource = Nothing
                tblMetas.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnVisualizarHojaVida_Click(sender As Object, e As EventArgs) Handles btnVisualizarHojaVida.Click
        Try
            If Request.QueryString("idMeta") <> String.Empty Then
                Fila = Nothing
                Fila = parametrizacion.selectCurriculum(Request.QueryString("idMeta"))
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

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Try
            Dim idMeta As String
            If idReport.Text <> String.Empty Then
                alerta("Advertencia", "Información antigua, limpie el formulario", "info", "ontenedor2_btnLimpiar")
                Exit Sub
            End If
            If meta.Text = String.Empty Then
                alerta("Advertencia", "Seleccione la meta para continuar el proceso", "info")
                Exit Sub
            End If
            If txtYearActual.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el valor del año actual", "info", "contenedor2_txtYearActual")
                Exit Sub
            End If
            If txtValorProgreso.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el valor del progreso actual", "info", "contenedor2_txtValorProgreso")
                Exit Sub
            End If
            If txtActividades.Text = String.Empty Then
                alerta("Advertencia", "Ingrese las actividades desarrolladas", "info", "contenedor2_txtActividades")
                Exit Sub
            End If
            If txtValorFisico.Text = String.Empty Then
                alerta("Advertencia", "Ingrese el valor físico", "info", "contenedor2_txtValorFisico")
                Exit Sub
            End If

            idReport.Text = parametrizacion.insertReport(meta.Text.Trim, txtYearActual.Text.Trim, txtValorFisico.Text.Trim,
                                                         txtActividades.Text.Trim, Date.Now.ToString("yyyy-MM-dd"), "A")
            parametrizacion.updateValue_progress(meta.Text.Trim, txtValorFisico.Text.Trim)
            alerta("Se ha grabado el seguimiento correctamente", "Ya puedes agregar las evidencias", "success")
            navEvidencias_Click(Nothing, Nothing)
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        Try
            Response.Redirect("Seguimiento.aspx")
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub CargarArchivo_Click(sender As Object, e As EventArgs) Handles CargarArchivo.Click
        Try
            Dim fileName, filePat, pathCorto, arraySeguimiento As String

            ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModalAdjutnosArch();", True)
            If meta.Text.Trim = String.Empty Then
                alerta("Advertencia", "Debe seleccionar la meta", "info", "")
                Exit Sub
            End If

            If idReport.Text = String.Empty Then
                alerta("Advertencia", "Debe guardar primero el reporte y despues adjuntar las evidencias", "info", "")
                Exit Sub
            End If

            If fuArchivo.HasFile Then
                For Each uploadedFile As HttpPostedFile In fuArchivo.PostedFiles

                    If (uploadedFile.ContentLength > 2000000) Then
                        alerta("El archivo no puede ser mayor a 2 MB", "", "error", "")
                        Exit Sub
                    End If

                    Dim fileExt As String = System.IO.Path.GetExtension(uploadedFile.FileName)

                    If fileExt = ".pdf" Or fileExt = ".xlsx" Or fileExt = ".xls" Or fileExt = ".docx" Or fileExt = ".txt" Or
                       fileExt = ".PDF" Or fileExt = ".XLSX" Or fileExt = ".XLS" Or fileExt = ".DOCX" Or fileExt = ".TXT" Then

                        filePat = Path.Combine(Request.PhysicalApplicationPath, "Evidencias\")
                        pathCorto &= "Evidencias\"
                        If (Directory.Exists(filePat & meta.Text.Trim & "-" & Session("pac"))) Then
                            If (Directory.Exists(filePat & meta.Text.Trim & "-" & Session("pac") & "\" & "txt")) Then
                            Else
                                Directory.CreateDirectory(Path.Combine(filePat, "txt"))
                            End If
                        Else
                            Directory.CreateDirectory(Path.Combine(filePat, meta.Text.Trim & "-" & Session("pac")))
                            Directory.CreateDirectory(Path.Combine(filePat & meta.Text.Trim & "-" & Session("pac"), "txt"))
                        End If

                        filePat &= meta.Text.Trim & "-" & Session("pac") & "\txt\"
                        pathCorto &= meta.Text.Trim & "-" & Session("pac") & "\txt\"

                        If (Directory.Exists(filePat & idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy"))) Then
                        Else
                            Directory.CreateDirectory(Path.Combine(filePat, idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy")))
                        End If

                        filePat &= idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy") & "\"
                        pathCorto &= idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy") & "\"

                        fileName = Path.GetFileName(Server.MapPath(uploadedFile.FileName))

                        fuArchivo.PostedFile.SaveAs(filePat & fileName)

                        Dim dataAdjuntos As New DataTable
                        Dim row As DataRow

                        dataAdjuntos.Columns.Add("nombre")
                        dataAdjuntos.Columns.Add("ruta")
                        dataAdjuntos.Columns.Add("")


                        If Session("dataAdjuntos") IsNot Nothing Then dataAdjuntos = Session("dataAdjuntos")

                        row = dataAdjuntos.NewRow()
                        row("nombre") = fileName
                        row("ruta") = pathCorto & fileName
                        dataAdjuntos.Rows.Add(row)

                        Session("dataAdjuntos") = dataAdjuntos
                        tblArchivos.DataSource = dataAdjuntos
                        tblArchivos.DataBind()

                        arraySeguimiento = "["
                        For Each row2 As DataRow In dataAdjuntos.Rows
                            arraySeguimiento &= row("ruta") & ", "
                        Next
                        arraySeguimiento = arraySeguimiento.Substring(0, arraySeguimiento.Length - 2)
                        arraySeguimiento &= "]"

                        parametrizacion.updateAdjuntosReport("array_archivos", arraySeguimiento, idReport.Text.Trim)
                        alerta("Se ha cargado el archivo correctamente", "", "success", "")
                    Else
                        alerta("Archivo no Permitido", "", "warning", "")
                        Exit Sub
                    End If
                Next
            Else
                alerta("Advertencia", "No ha seleccionado ningún archivo", "error", "")
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub CaragrImagen_Click(sender As Object, e As EventArgs) Handles CaragrImagen.Click
        Try
            Dim fileName, filePat, pathCorto, arraySeguimiento As String

            ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModalAdjutnosImg();", True)
            If meta.Text.Trim = String.Empty Then
                alerta("Debe seleccionar la meta", "", "info", "")
                Exit Sub
            End If
            If idReport.Text = String.Empty Then
                alerta("Advertencia", "Debe guardar primero el reporte y despues adjuntar las evidencias", "info", "")
                Exit Sub
            End If

            If fuImagenes.HasFile Then
                For Each uploadedFile As HttpPostedFile In fuArchivo.PostedFiles

                    If (uploadedFile.ContentLength > 2000000) Then
                        alerta("El archivo no puede ser mayor a 2 MB", "", "error", "")
                        Exit Sub
                    End If

                    Dim fileExt As String = System.IO.Path.GetExtension(uploadedFile.FileName)

                    If fileExt = ".img" Or fileExt = ".png" Or fileExt = ".jpg" Or fileExt = ".jpge" Or fileExt = ".svg" Or
                       fileExt = ".IMG" Or fileExt = ".PNG" Or fileExt = ".JPG" Or fileExt = ".JPGE" Or fileExt = ".SVG" Then
                        filePat = Path.Combine(Request.PhysicalApplicationPath, "Evidencias\")
                        pathCorto &= "Evidencias\"
                        If (Directory.Exists(filePat & meta.Text.Trim & "-" & Session("pac"))) Then
                            If (Directory.Exists(filePat & meta.Text.Trim & "-" & Session("pac") & "\" & "img")) Then
                            Else
                                Directory.CreateDirectory(Path.Combine(filePat & meta.Text.Trim & "-" & Session("pac"), "img"))
                            End If
                        Else
                            Directory.CreateDirectory(Path.Combine(filePat, meta.Text.Trim & "-" & Session("pac")))
                            Directory.CreateDirectory(Path.Combine(filePat & meta.Text.Trim & "-" & Session("pac"), "img"))
                        End If

                        filePat &= meta.Text.Trim & "-" & Session("pac") & "\img\"
                        pathCorto &= meta.Text.Trim & "-" & Session("pac") & "\img\"

                        If (Directory.Exists(filePat & idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy"))) Then
                        Else
                            Directory.CreateDirectory(Path.Combine(filePat, idReport.Text.Trim & "-" & Date.Now.ToString("dd-MM-yyyy")))
                        End If


                        fileName = Path.GetFileName(Server.MapPath(uploadedFile.FileName))

                        fuImagenes.PostedFile.SaveAs(filePat & fileName)

                        Dim dataImagenes As New DataTable
                        Dim row As DataRow

                        dataImagenes.Columns.Add("nombre")
                        dataImagenes.Columns.Add("ruta")
                        dataImagenes.Columns.Add("")


                        If Session("dataImagenes") IsNot Nothing Then dataImagenes = Session("dataImagenes")

                        row = dataImagenes.NewRow()
                        row("nombre") = fileName
                        row("ruta") = pathCorto & fileName
                        dataImagenes.Rows.Add(row)

                        Session("dataImagenes") = dataImagenes
                        tblImagenes.DataSource = dataImagenes
                        tblImagenes.DataBind()

                        arraySeguimiento = "["
                        For Each row2 As DataRow In dataImagenes.Rows
                            arraySeguimiento &= row("ruta") & ", "
                        Next
                        arraySeguimiento = arraySeguimiento.Substring(0, arraySeguimiento.Length - 2)
                        arraySeguimiento &= "]"

                        parametrizacion.updateAdjuntosReport("array_imagenes", arraySeguimiento, idReport.Text.Trim)
                        alerta("Se ha cargado la imagenes correctamente", "", "success", "")
                    Else
                        alerta("Archivo no Permitido", "", "warning", "")
                        Exit Sub
                    End If
                Next
            Else
                alerta("Advertencia", "No ha seleccionado ningún archivo", "error", "")
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
            Fila = Nothing
            Fila = parametrizacion.selectPacActivo
            If Fila IsNot Nothing Then
                pac.Text = Fila("id")
                DataT = Nothing
                DataT = parametrizacion.selectNiveles(pac.Text.Trim)
                If DataT.Rows.Count > 0 Then
                    cmbLineas.Items.Clear()
                    cmbLineas.DataTextField = "name"
                    cmbLineas.DataValueField = "code"
                    cmbLineas.DataSource = DataT
                    cmbLineas.DataBind()
                    cmbLineas.Items.Insert(0, New ListItem("---Seleccione---", ""))
                End If
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub


    Public Sub cargarMeta(ByVal idMeta As String)
        Try
            Fila = Nothing
            Fila = parametrizacion.selectGoalsFila(idMeta)
            If Fila IsNot Nothing Then
                DataT = Nothing
                DataT = fun.goal_type
                If DataT.Rows.Count > 0 Then
                    cmbTipoMeta.Items.Clear()
                    cmbTipoMeta.DataTextField = "description"
                    cmbTipoMeta.DataValueField = "name"
                    cmbTipoMeta.DataSource = DataT
                    cmbTipoMeta.DataBind()
                End If
                Session("pac") = Fila("pac_id")
                txtNombreMeta.Text = Fila("name")
                cmbTipoMeta.SelectedValue = Fila("type_goal")
                txtLineaBase.Text = Fila("line_base")
                txtPriYear.Text = Fila("value_one_year")
                txtSegYear.Text = Fila("value_two_year")
                txtTercYear.Text = Fila("value_three_year")
                txtCuartYear.Text = Fila("value_four_year")
                txtValorProgreso.Text = Fila("value_progress")
                Dim year_initial, index As Integer
                Fila = Nothing
                Fila = parametrizacion.selectPac(Session("pac"))
                If Fila IsNot Nothing Then
                    year_initial = CInt(Fila("initial_year"))
                    For i As Integer = 1 To CInt(Fila("number_years"))
                        If year_initial = CInt(Date.Now.ToString("yyyy")) Then
                            index = i
                        End If
                        year_initial += 1
                    Next
                    If index = 1 Then txtYearActual.Text = txtPriYear.Text
                    If index = 2 Then txtYearActual.Text = txtSegYear.Text
                    If index = 3 Then txtYearActual.Text = txtTercYear.Text
                    If index = 4 Then txtYearActual.Text = txtCuartYear.Text
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Sub pestaña(index As Integer)
        navMetas.Attributes.Add("class", "")
        navActividades.Attributes.Add("class", "")
        navEvidencias.Attributes.Add("class", "")

        Select Case index
            Case 1
                navMetas.Attributes.Add("class", "nav-link active")
                navActividades.Attributes.Add("class", "nav-link")
                navEvidencias.Attributes.Add("class", "nav-link")
            Case 2
                navActividades.Attributes.Add("class", "nav-link active")
                navMetas.Attributes.Add("class", "nav-link")
                navEvidencias.Attributes.Add("class", "nav-link ")
            Case 3
                navEvidencias.Attributes.Add("class", "nav-link active")
                navMetas.Attributes.Add("class", "nav-link")
                navActividades.Attributes.Add("class", "nav-link")
        End Select
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
