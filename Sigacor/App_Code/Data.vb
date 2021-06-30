Imports System.Data.SqlClient
Imports System.Data

Public Class Data

    ''' <summary>
    ''' Realiza consultas que devuelven datos Table
    ''' </summary>
    ''' <param name="QRY">Cadena de entrada para devolver un TABLE</param>
    ''' <returns>Table con los datos de la consulta</returns>
    Public Shared Function OpenData(QRY As String) As DataTable
        Dim dt As New DataTable
        Dim da As SqlDataAdapter
        Dim conexion As New Conexion
        Try
            Dim con As SqlConnection = conexion.conectar()
            Dim Comando2 = New SqlCommand(QRY, con)
            da = New SqlDataAdapter(Comando2)
            da.Fill(dt)
            conexion.desconectar()
        Catch ex As Exception
        End Try
        Return dt
    End Function

    ''' <summary>
    ''' Realiza consultas que devuelven datos Row
    ''' </summary>
    ''' <param name="QRY">Cadena de entrada para devolver un ROW</param>
    ''' <returns>DAtaRow con los datos de la consulta</returns>
    Public Shared Function OpenRow(QRY As String) As DataRow
        Dim da As SqlDataAdapter
        Dim dt As New DataTable
        Dim dr As DataRow = Nothing
        Dim conexion As New Conexion
        Dim con As SqlConnection = conexion.conectar()
        Dim Comando2 = New SqlCommand(QRY, con)
        da = New SqlDataAdapter(Comando2)
        da.Fill(dt)
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
        Else
            dr = Nothing
        End If
        conexion.desconectar()
        Return dr
    End Function


    ''' <summary>
    ''' Ejecuta una instruccion sea Insert o Update
    ''' </summary>
    ''' <param name="QRY">Cadena de entrada para realizar INSERT o UPDATE</param>
    ''' <returns>Retorna un 0 si no ejecuta la cadena y un 1 si fue exitoso.</returns>
    Public Shared Function Execute(QRY As String) As Integer
        Dim resul As Integer = 0
        Dim conexion As New Conexion
        Try
            Dim con As SqlConnection = conexion.conectar()
            Dim cmd = New SqlCommand(QRY, con)
            resul = cmd.ExecuteNonQuery()
            conexion.desconectar()
        Catch ex As Exception
        End Try
        Return resul
    End Function

End Class


