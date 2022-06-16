Imports System.Data
Public Class clReportPac
    Public Function selectLineas(ByVal code As String, ByVal pac_id As String) As DataTable

        QRY = "select code, name from SCRCONTND where pac_id = " & pac_id & " "

        If code <> String.Empty Then
            QRY &= "and code = '" & code & "'"
        Else
            QRY &= "and sublevel = '' "
        End If

        QRY &= "group by code, name"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectLineasFila(ByVal code As String, ByVal pac_id As String) As DataRow

        QRY = "select code, name from SCRCONTND where pac_id = " & pac_id & " and state = 'A' "

        If code <> String.Empty Then
            QRY &= "and code = '" & code & "'"
        Else
            QRY &= "and sublevel = '' "
        End If

        Return Data.OpenRow(QRY)
    End Function
    Public Function selectLineasFiltroGeneralMetas(ByVal pac_id As String, ByVal arrayLineas As String()) As DataTable
        Dim codLineas As String
        Dim i As Integer = 0
        For Each row In arrayLineas
            codLineas &= "'" & arrayLineas(i) & "',"
            i += 1
        Next
        codLineas = Mid(codLineas, 1, Len(codLineas) - 1)
        QRY = "select * from SCRCONTND where pac_id = " & pac_id & " and code IN (" & codLineas & ")"

        Return Data.OpenData(QRY)
    End Function
    Public Function selectLineasFiltroGeneral(ByVal pac_id As String, ByVal level_id As String, Optional ByVal indicador As String = "") As DataTable

        If indicador = String.Empty Then
            QRY = "select c.sublevel, c.sublevel as code from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and  c.state = 'A' group by c.sublevel"
        Else
            QRY = "select c.code from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and  c.state = 'A' group by c.code"
        End If

        Return Data.OpenData(QRY)
    End Function


    Public Function selectContentsFiltroGeneral(ByVal pac_id As String, ByVal level_id As String, ByVal code As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level, c.pac_id, '' as value_progress from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and l.hierarchy = " & level_id & " and c.code like '" & code & "%' and c.state = 'A'   "

        Return Data.OpenData(QRY)
    End Function


    Public Function selectContentsFiltro(ByVal pac_id As String, ByVal code As String, ByVal level_id As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' and c.level_id = " & level_id & " order by c.code"

        Return Data.OpenData(QRY)
    End Function
    Public Function selectContentsReport(ByVal pac_id As String, ByVal code As String, ByVal sublevel As String) As DataRow

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' 
               and c.sublevel = '" & sublevel & "' order by c.code"

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectPalabraClave(ByVal name As String, ByVal pac_id As String) As DataTable

        QRY = "select c.level_id, l.name, c.code, c.name from SCRCONTND c join SCRNIVLS l on c.level_id = l.hierarchy and 
               c.pac_id = l.pac_id where c.name like  '%" & name & "%' and c.pac_id=" & pac_id & "  and c.state = 'A' order by c.code"

        Return Data.OpenData(QRY)
    End Function


    Public Function selectPalabraClave(ByVal pac_id As String, ByVal code As String, ByVal name As String) As DataTable

        QRY = "select c.code, c.name, c.sublevel, l.name name_level from SCRCONTND c join SCRNIVLS l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where
               c.pac_id = " & pac_id & " and c.code like '" & code & "%' and c.name like  '%" & name & "%' and c.state = 'A'   "

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal pac_id As String) As DataTable

        QRY = "select id, name, subactivity as sublevel, progress_one_year, progress_two_year, 
               progress_three_year, progress_four_year, value_one_year,value_two_year, value_three_year, value_four_year 
               from SCRMET where pac_id = " & pac_id & " and state = 'A' order by  subactivity "

        Return Data.OpenData(QRY)
    End Function
    Public Function selectGoalsFiltroGeneral(ByVal pac_id As String, ByVal subactivity As String) As DataTable

        QRY = "select id, name, subactivity as code, 'Metas' as name_level, pac_id, value_progress, progress_one_year, progress_two_year, 
               progress_three_year, progress_four_year, value_one_year,value_two_year, value_three_year, value_four_year 
               from SCRMET where pac_id = " & pac_id & " and 
               subactivity like '" & subactivity & "%' and state = 'A' order by  subactivity"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal pac_id As String, ByVal subactivity As String) As DataTable

        QRY = "select id, name, subactivity as code from SCRMET where pac_id = " & pac_id & " and 
               subactivity = '" & subactivity & "' and state = 'A' order by subactivity "

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal campos As Boolean, ByVal pac_id As String, ByVal noProgramado As Boolean, ByVal ejecMenos25 As Boolean, ByVal ejec25Al49 As Boolean,
                                ByVal ejec50Al74 As Boolean, ByVal ejec75Al99 As Boolean, ByVal ejecMas100 As Boolean, ByVal campoYear As String, Optional subactivity As String = "") As DataTable
        If campos Then
            QRY = "select id, name, subactivity as sublevel, value_progress from SCRMET where pac_id = " & pac_id & " and state = 'A' and ("
        Else
            QRY = "select id, name, subactivity as code, 'Metas' as name_level, pac_id from SCRMET where pac_id = " & pac_id & " and state = 'A' and subactivity like '" & subactivity & "%' and ("
        End If

        If noProgramado Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) = 0 "
        End If
        If ejecMenos25 Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) < 25 "
        End If
        If ejec25Al49 Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) BETWEEN 25 and 49 "
        End If
        If ejec50Al74 Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) BETWEEN 50 and 74 "
        End If
        If ejec75Al99 Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) BETWEEN 75 and 99 "
        End If
        If ejecMas100 Then
            QRY &= " or CAST(((progress_" & campoYear & "_year / value_" & campoYear & "_year) * 100) AS INT) >= 100 "
        End If

        QRY &= " )"

        Return Data.OpenData(QRY.Replace("( or", "("))
    End Function

    Public Function selectGoalsXId(ByVal id As String) As DataTable

        QRY = "select id, name, subactivity as code, 'Metas' as name_level, pac_id from SCRMET where id = " & id

        Return Data.OpenData(QRY)
    End Function

End Class
