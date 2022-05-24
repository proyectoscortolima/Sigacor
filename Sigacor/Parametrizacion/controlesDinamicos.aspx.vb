Public Class controlesDinamicos
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion
    Dim reportPac As New clReportPac
    Dim login As New clLogin

#Region "Init"

    Private Sub controlesDinamicos_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim dt As DataTable = ViewState("Quantity")
        Me.QuantityDinamicControls = dt
        If Not IsPostBack Then
            GenerateControls()
            lblError.Visible = False
        End If
    End Sub

#End Region

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

    Protected Overloads Overrides Sub CreateChildControls()
        If Page.IsPostBack Then GenerateControls()
    End Sub
    Private Sub controlNuevo(Optional nivel As String = "")
        Dim nuevoCmb As DropDownList = New DropDownList()
        DataT = parametrizacion.selectNiveles("1", nivel)
        If DataT.Rows.Count > 0 Then
            nuevoCmb.ID = "cmbNivel" + DataT(0)(3).ToString()
            nuevoCmb.CssClass = "form-control"
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataTextField = "name"
            nuevoCmb.DataValueField = "code"
            nuevoCmb.DataSource = DataT
            AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
            nuevoCmb.AutoPostBack = True
            nuevoCmb.DataBind()
            nuevoCmb.Items.Insert(0, New ListItem("---Seleccione---", ""))
            nuevoCmb.SelectedIndex = 0

            phDinamicControls.Controls.Add(New LiteralControl("<div class=""col-3 mt-2"">
                                                                   <div class=""form-group"">
                                                                       <label>" & DataT(0)(4) & "</label>"))

            phDinamicControls.Controls.Add(nuevoCmb)

            phDinamicControls.Controls.Add(New LiteralControl("</div>
                                                               </div>"))
        Else
            phDinamicControls.Controls.Add(New LiteralControl("error nivel no contiene contenido"))
        End If


    End Sub
    Private Sub GenerateControls()
        Dim Quantity As Integer = 0
        Dim i As Integer = 0

        Dim dataNiveles As DataTable = parametrizacion.selectLevels("1", "hierarchy")

        If QuantityDinamicControls.Rows.Count > 0 Then
            For Each row As DataRow In QuantityDinamicControls.Rows
                Dim nuevoCmb As DropDownList = New DropDownList()
                DataT = Nothing
                DataT = parametrizacion.selectNiveles("1", row("sublevel").ToString())
                If DataT.Rows.Count > 0 Then
                    nuevoCmb.ID = "cmbNivel" + row("level")
                    nuevoCmb.CssClass = "form-control"
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataTextField = "name"
                    nuevoCmb.DataValueField = "code"
                    nuevoCmb.DataSource = DataT
                    AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
                    nuevoCmb.AutoPostBack = True
                    nuevoCmb.DataBind()
                    nuevoCmb.Items.Insert(0, New ListItem("---Seleccione---", ""))
                    nuevoCmb.SelectedIndex = 0

                    phDinamicControls.Controls.Add(New LiteralControl("<div class=""col-3 mt-2"">
                                                                       <div class=""form-group"">
                                                                           <label>" & DataT(0)(4) & "</label>"))

                    phDinamicControls.Controls.Add(nuevoCmb)

                    phDinamicControls.Controls.Add(New LiteralControl("</div>
                                                                       </div>"))
                Else
                    phDinamicControls.Controls.Add(New LiteralControl("error nivel no contiene contenido"))
                End If
            Next
        Else
            Dim nuevoCmb As DropDownList = New DropDownList()
            Dim dt As New DataTable()
            dt.Columns.Add("level")
            dt.Columns.Add("sublevel")
            Dim row As DataRow = dt.NewRow()
            DataT = parametrizacion.selectNiveles("1")
            If DataT.Rows.Count > 0 Then
                row("level") = DataT(0)(3).ToString()
                nuevoCmb.ID = "cmbNivel" + DataT(0)(3).ToString()
                nuevoCmb.CssClass = "form-control"
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataTextField = "name"
                nuevoCmb.DataValueField = "code"
                nuevoCmb.DataSource = DataT
                AddHandler nuevoCmb.SelectedIndexChanged, AddressOf nuevoCmb_SelectedIndexChanged
                nuevoCmb.AutoPostBack = True
                nuevoCmb.DataBind()
                nuevoCmb.Items.Insert(0, New ListItem("---Seleccione---", ""))
                nuevoCmb.SelectedIndex = 0

                phDinamicControls.Controls.Add(New LiteralControl("<div class=""col-3 mt-2"">
                                                                   <div class=""form-group"">
                                                                       <label>" & DataT(0)(4) & "</label>"))

                phDinamicControls.Controls.Add(nuevoCmb)

                phDinamicControls.Controls.Add(New LiteralControl("</div>
                                                                   </div>"))
            Else
                phDinamicControls.Controls.Add(New LiteralControl("error nivel no contiene contenido"))
            End If
            dt.Rows.Add(row)
            QuantityDinamicControls = dt
        End If
    End Sub

    Private Sub nuevoCmb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DataT2 As New DataTable
        Dim nivelControl As DropDownList = DirectCast(sender, DropDownList)
        Dim row As DataRow = QuantityDinamicControls.NewRow()

        DataT2 = parametrizacion.selectNiveles("1", nivelControl.SelectedValue)
        If nivelControl IsNot Nothing Then
            If DataT2.Rows.Count > 0 Then
                row("level") = DataT2(0)(3)
            Else
                row("level") = String.Empty
            End If
            row("sublevel") = nivelControl.SelectedValue
            QuantityDinamicControls.Rows.Add(row)

            Dim result As DataRow() = QuantityDinamicControls.Select("level = '" & row("level") & "'")
            If result.Length >= 2 Then
                phDinamicControls.Controls.Clear()
                QuantityDinamicControls = Nothing
                GenerateControls()
            Else
                controlNuevo(nivelControl.SelectedValue)
            End If
        End If
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            Dim code, level_id As String

            If (QuantityDinamicControls.Rows.Count > 0) Then
                Dim lastRow As DataRow = QuantityDinamicControls.Rows(QuantityDinamicControls.Rows.Count - 1)

                code = lastRow("sublevel")
                level_id = lastRow("level")

                DataT = parametrizacion.selectContentsFiltro("1", code, level_id)
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

    Public Sub alerta(ByVal mensaje As String, ByVal subMensaje As String, ByVal tipo As String, Optional foco As String = "")
        Dim Script As String = "<script type='text/javascript'> swal({title:'" + mensaje.Replace("'", " | ") + "', text:'" + subMensaje.Replace("'", " | ") + "' , type:'" + tipo + "', confirmButtonText:'OK'})"
        If foco.Trim <> "" Then
            Script &= ".then((result) => {if (result.value) {document.getElementById('" + foco + "').focus();}});"
        End If
        Script &= " </script>"
        ScriptManager.RegisterStartupScript(Me, GetType(Page), "swal", Script, False)
    End Sub
End Class
