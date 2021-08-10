Public Class DetallePac
    Inherits System.Web.UI.Page

    Dim reportPac As New clReportPac
    Dim parametrizacion As New clParametrizacion

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            DataT = Nothing
            Dim Fila2 As DataRow
            Dim Datat2 As New DataTable
            Dim valor, valor2, subLevel As String
            Dim i As Integer = 0
            Dim delimitadores() As String = {"."}
            Dim vectoraux() As String

            valor = "1.1.1.1.1"
            vectoraux = valor.Split(delimitadores, StringSplitOptions.None)

            Fila2 = Nothing
            Datat2.Columns.Add("code")
            Datat2.Columns.Add("name")
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
                Fila = reportPac.selectContentsReport("17", valor2, subLevel)
                If Fila IsNot Nothing Then
                    Fila2("code") = Fila("code")
                    Fila2("name") = Fila("name")
                    Datat2.Rows.Add(Fila2)
                End If

                i += 1
            Next

            tblJerarquia.DataSource = Datat2
            tblJerarquia.DataBind()


        Catch ex As Exception

        End Try
    End Sub

End Class