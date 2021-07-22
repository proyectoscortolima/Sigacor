Public Class Parametrizacion
    Inherits System.Web.UI.Page

    Dim parametrizacion As New clParametrizacion

#Region "Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblError.Visible = False
                actualizarStatePac()
                cargarPac()
            End If

            If tblPac.Rows.Count > 0 Then
                tblPac.HeaderRow.TableSection = TableRowSection.TableHeader
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try

    End Sub

#End Region

#Region "RowDataBound"
    Private Sub tblPac_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles tblPac.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim linkBtn, linkBtn2, linkBtn3 As New LinkButton
                linkBtn = e.Row.FindControl("lnkEditar")
                linkBtn2 = e.Row.FindControl("lnkVisualizar")
                linkBtn3 = e.Row.FindControl("lnkEliminar")

                linkBtn.CommandArgument = e.Row.Cells(0).Text.Trim
                linkBtn2.CommandArgument = e.Row.Cells(0).Text.Trim
                linkBtn3.CommandArgument = e.Row.Cells(0).Text.Trim

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

#End Region

#Region "RowCommand"
    Private Sub tblPac_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles tblPac.RowCommand
        Try

            If e.CommandName = "Editar" Then
                Session("id_pac") = e.CommandArgument
                Response.Redirect("NuevoRegistro.aspx")

            ElseIf e.CommandName = "Visualizar" Then
                visualizarPac(e.CommandArgument)
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "modal", "abrirModal();", True)
            ElseIf e.CommandName = "Eliminar" Then
                lblPac.Text = e.CommandArgument
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaSN", "AlertaSN();", True)

            End If

        Catch ex As Exception

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
                    lblPac.Text = Fila("id")
                    lblNompac.Text = Fila("name")
                    lblSlogan.Text = Fila("slogan")
                    lblYearIni.Text = Fila("initial_year")
                    lblCantYears.Text = Fila("number_years")

                    cargarNiveles(lblPac.Text.Trim)
                    cargarPlanAccion(lblPac.Text.Trim)

                End If
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
                tblNivParam.DataSource = DataT
                tblNivParam.DataBind()
                tblNivParam.UseAccessibleHeader = True
                tblNivParam.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblNivParam.DataSource = Nothing
                tblNivParam.DataBind()
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
                tblPlanAccParam.DataSource = DataT
                tblPlanAccParam.DataBind()
                tblPlanAccParam.UseAccessibleHeader = True
                tblPlanAccParam.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblPlanAccParam.DataSource = Nothing
                tblPlanAccParam.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub
    Public Sub cargarPac()
        Try
            DataT = Nothing
            DataT = parametrizacion.selectPac
            If DataT.Rows.Count > 0 Then
                tblPac.DataSource = DataT
                tblPac.DataBind()
                tblPac.HeaderRow.TableSection = TableRowSection.TableHeader
            Else
                tblPac.DataSource = Nothing
                tblPac.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Public Sub actualizarStatePac()
        Try
            Fila = parametrizacion.selectPacYear(Now.Year)
            If Fila IsNot Nothing Then
                parametrizacion.updateStatePac(Fila("id"))
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.Visible = True
        End Try
    End Sub

    Private Sub eliminarPac_Click(sender As Object, e As EventArgs) Handles eliminarPac.Click
        Try
            parametrizacion.updatePac(lblPac.Text.Trim)
            cargarPac()
            lblPac.Text = String.Empty
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