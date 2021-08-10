Imports System.Data
Public Class clReportPac
    Public Function selectLineas(ByVal code As String, ByVal pac_id As String) As DataTable

        QRY = "select code, name from contents where pac_id = " & pac_id & " "

        If code <> String.Empty Then
            QRY &= "and code = '" & code & "'"
        Else
            QRY &= "and sublevel = '' "
        End If

        QRY &= "group by code, name"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectLineasFila(ByVal code As String, ByVal pac_id As String) As DataRow

        QRY = "select code, name from contents where pac_id = " & pac_id & " and state = 'A' "

        If code <> String.Empty Then
            QRY &= "and code = '" & code & "'"
        Else
            QRY &= "and sublevel = '' "
        End If

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectLineasFiltroGeneral(ByVal pac_id As String, ByVal level_id As String, Optional ByVal indicador As String = "") As DataTable

        If indicador = String.Empty Then
            QRY = "select c.sublevel from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and  c.state = 'A' group by c.sublevel"
        Else
            QRY = "select c.code as sublevel from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and  c.state = 'A' group by c.code"
        End If

        Return Data.OpenData(QRY)
    End Function


    Public Function selectContentsFiltroGeneral(ByVal pac_id As String, ByVal level_id As String, ByVal code As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and c.code like '" & code & "%' and c.state = 'A'   "

        Return Data.OpenData(QRY)
    End Function


    Public Function selectContentsFiltro(ByVal pac_id As String, ByVal code As String, ByVal level_id As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' and c.level_id = " & level_id & " order by c.code"

        Return Data.OpenData(QRY)
    End Function
    Public Function selectContentsReport(ByVal pac_id As String, ByVal code As String, ByVal sublevel As String) As DataRow

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' 
               and c.sublevel = '" & sublevel & "' order by c.code"

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectPalabraClave(ByVal name As String, ByVal pac_id As String) As DataTable

        QRY = "select c.level_id, l.name, c.code, c.name from contents c join levels l on c.level_id = l.hierarchy and 
               c.pac_id = l.pac_id where c.name like  '%" & name & "%' and c.pac_id=" & pac_id & "  and c.state = 'A' order by c.code"

        Return Data.OpenData(QRY)
    End Function


    Public Function selectPalabraClave(ByVal pac_id As String, ByVal code As String, ByVal name As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' and c.name like  '%" & name & "%' and c.state = 'A'   "

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal pac_id As String) As DataTable

        QRY = "select id, name, subactivity as sublevel from goals where pac_id = " & pac_id & " and state = 'A' "

        Return Data.OpenData(QRY)
    End Function

End Class
