Imports System.Data.SqlClient
Public Class Conexion
    Dim cone As New SqlConnection(ConfigurationManager.ConnectionStrings("conexion").ConnectionString)


    ''' <summary>
    ''' se conecta a la base de datos
    ''' </summary>
    ''' <returns>Conexion a la base de datos.</returns>
    Public Function conectar() As SqlConnection
        Try
            cone.Open()
            Console.WriteLine("Se conecto a la bases de datos sin problemas")
            Return cone
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try

    End Function

    ''' <summary>
    ''' Se desconecta de la base de datos
    ''' </summary>
    Public Sub desconectar()
        cone.Close()
        cone.Dispose()
    End Sub

End Class

