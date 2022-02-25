Imports System.Data
Public Class Funciones
    Public Function states() As DataTable

        QRY = "select * from SCRESTDS"

        Return Data.OpenData(QRY)
    End Function

    Public Function goal_type() As DataTable

        QRY = "select * From SCDTIPMT Where state = 'A'"

        Return Data.OpenData(QRY)
    End Function

    Public Function periodicity() As DataTable

        QRY = "select * From SCDPERDCDD Where state = 'A' order by description"

        Return Data.OpenData(QRY)
    End Function



End Class
