Imports System.Security.Cryptography

Public Class Login
    Inherits System.Web.UI.Page

    Dim login As New clLogin
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtUsuario.Focus()
            Session("CodUsuario") = Nothing
            Session("NomUsuario") = Nothing
            Session("Rol") = Nothing
            Session("NomRol") = Nothing
        End If

    End Sub

#Region "Click"
    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Dim password As String
        Dim fila2 As DataRow
        If txtUsuario.Text = String.Empty Then
            alerta("Advertencia", "Ingrese el código de usuario", "info", "txtUsuario")
            Exit Sub
        End If
        If txtPassword.Text = String.Empty Then
            alerta("Advertencia", "Ingrese la contraseña", "info", "txtPassword")
            Exit Sub
        End If
        Fila = login.selectUsuario(txtUsuario.Text.Trim)
        If Fila IsNot Nothing Then
            If Not IsDBNull(Fila("encriptado")) Then
                If Fila("encriptado") = "1" Then
                    password = Encriptar(txtPassword.Text.Trim)
                Else
                    password = txtPassword.Text.Trim
                End If
            End If
            If password <> Fila("clave").ToString.Trim Then
                alerta("Advertencia", "La contraseña es incorrecta", "warning", "txtPassword")
                Exit Sub
            End If
            fila2 = login.selectUsuario(txtUsuario.Text.Trim, "1")
            If fila2 Is Nothing Then
                alerta("Advertencia", "El usuario se encuentra retirado", "info", "")
                Exit Sub
            End If
            'fila2 = Nothing
            'fila2 = login.selectUsuario(txtUsuario.Text.Trim, "2")
            'If fila2 Is Nothing Then
            '    alerta("Advertencia", "El usuario tiene el contrato vencido", "info", "")
            '    Exit Sub
            'End If
        Else
            alerta("Advertencia", "El usuario no se encuentra registrado", "error", "")
            Exit Sub
        End If

        Session("CodUsuario") = txtUsuario.Text
        Session("NomUsuario") = Fila("nombreEmp")
        Session("Rol") = Fila("rol_id")
        Session("NomRol") = Fila("description")

        Response.Redirect("Parametrizacion/Parametrizacion.aspx")
    End Sub

#End Region

#Region "Metodos - Funciones"

    Public Function Encriptar(ByVal Input As String) As String

        Dim sha As New SHA1CryptoServiceProvider ' declara proveedor SHA1CryptoServiceProvider
        Dim bytesToHash() As Byte '   and here is a byte variable

        bytesToHash = System.Text.Encoding.ASCII.GetBytes(Input.Trim) ' converte el password en codigo ascii

        bytesToHash = sha.ComputeHash(bytesToHash) ' empieza el proceso de encriptacion

        Dim encPassword As String = ""

        For Each b As Byte In bytesToHash
            encPassword += b.ToString("x2")
        Next

        Return encPassword ' cadena encriptada

    End Function

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