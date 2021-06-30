Imports System.Data
Public Class Funciones
    Public Function states() As DataTable

        QRY = "select * from states"

        Return Data.OpenData(QRY)
    End Function

    Public Function goal_type() As DataTable

        QRY = "select * From goal_type Where state = 'A'"

        Return Data.OpenData(QRY)
    End Function

    Public Function periodicity() As DataTable

        QRY = "select * From periodicity Where state = 'A' order by description"

        Return Data.OpenData(QRY)
    End Function



End Class
