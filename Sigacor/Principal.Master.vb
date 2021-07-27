Public Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("CodUsuario") Is Nothing Then
            Response.Redirect("../Login.aspx")
        End If
        lblNomUsuario.Text = Session("NomUsuario")
        pnlMenu.Controls.Clear()
        Dim Menu As New Sigacor.clLogin
        Dim DataT1 As New DataTable()
        Dim DataT2 As New DataTable()
        DataT1 = Menu.selectMenu(Session("Rol"))
        If DataT1 IsNot Nothing Then
            For Each row As DataRow In DataT1.Rows
                pnlMenu.Controls.Add(New LiteralControl("<li class=""nav-item"">"))
                If Not IsDBNull(row("route")) Then
                    pnlMenu.Controls.Add(New LiteralControl("<a Class=""nav-link nav-link-Curvo"" id=""" & row("orden") & """ href=" & row("route") & ">
                                                            <i Class=""" & row("icon") & """></i>
                                                            <span> " & row("name").ToString() & "</span></a>"))
                Else
                    pnlMenu.Controls.Add(New LiteralControl("<a class=""nav-link nav-link-Curvo collapsed"" href=""#"" data-toggle=""collapse"" data-target=""#collapseTwo""
                                                             aria-expanded=""True"" aria-controls=""collapseTwo"">
                                                                <i class=" & row("icon") & "></i>
                                                                <span>" & row("name") & " </span>
                                                             </a>
                                                             <div id=""collapseTwo"" class=""collapse"" aria-labelledby=""headingTwo"" data-parent=""#accordionSidebar"">
                                                             <div class=""bg-white py-2 collapse-inner rounded"">"))
                    'DataT2 = Menu.selectSubMenu(row("MNNOMBRE"), Session("CodRol"))
                    DataT2 = Nothing
                    If DataT2 IsNot Nothing Then
                        For Each rowSub As DataRow In DataT2.Rows
                            pnlMenu.Controls.Add(New LiteralControl("<a class=""collapse-item"" href=" & rowSub("SMRUTA") & "> " & rowSub("SMNOMBRE") & "></a>"))
                        Next
                    End If
                End If
                pnlMenu.Controls.Add(New LiteralControl("</li>"))
            Next
        End If


    End Sub

End Class