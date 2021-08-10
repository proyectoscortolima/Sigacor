Imports System.Data
Public Class clParametrizacion

#Region "Pac"

    Public Function insertPac(ByVal name As String, ByVal slogan As String, ByVal initial_year As String,
                              ByVal final_year As String, ByVal number_years As String, ByVal state As String) As Integer

        QRY = "insert into pac(name, slogan, initial_year, final_year, number_years, state) values (" &
              "'" & name & "', '" & slogan & "', " & initial_year & ", " & final_year & ", " &
              "" & number_years & ", '" & state & "') "

        Return Data.Execute(QRY)
    End Function

    Public Function updatePac(ByVal name As String, ByVal slogan As String, ByVal initial_year As String,
                              ByVal final_year As String, ByVal number_years As String, ByVal state As String,
                              ByVal id As String) As Integer

        QRY = "update pac set name = '" & name & "', slogan = '" & slogan & "', initial_year = " & initial_year & ", 
               final_year = " & final_year & ", number_years = " & number_years & ", state = '" & state & "'
               where id = " & id & ""

        Return Data.Execute(QRY)
    End Function

    Public Function updatePac(ByVal id As String) As Integer

        QRY = "update pac set state = 'I' where id = " & id & ""

        Return Data.Execute(QRY)
    End Function

    Public Function consecutivoPac() As Integer

        Dim row As DataRow

        QRY = "select id from pac order by id desc"

        row = Data.OpenRow(QRY)
        If row IsNot Nothing Then
            consecutivoPac = row("id")
        End If

    End Function

    Public Function selectPac() As DataTable

        QRY = "select id, name, slogan, initial_year, final_year, number_years, state from pac where state = 'A' 
               order by id"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectPacTodos() As DataTable

        QRY = "select id, name, slogan, initial_year, final_year, number_years, state from pac where state <> 'I' 
               order by id desc"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectPac(ByVal id As String) As DataRow

        QRY = "select id, name, slogan, initial_year, final_year, number_years, state from pac where id = " & id

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectPacActivo() As DataRow

        QRY = "select id, name, slogan, initial_year, final_year, number_years, state from pac where state = 'A'"

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectPacYear(ByVal final_year As String) As DataRow

        QRY = "select id from pac where state = 'A' and final_year = " & final_year

        Return Data.OpenRow(QRY)
    End Function

    Public Function updateStatePac(ByVal id As String) As Integer

        QRY = "update pac set state = 'C' where id = " & id

        Return Data.Execute(QRY)
    End Function

#End Region

#Region "Levels"
    Public Function selectLevels(ByVal pac_id As String, Optional order As String = "") As DataTable

        QRY = "select id, name, pac_id, hierarchy, state from levels where state = 'A' and pac_id = " & pac_id

        If order <> String.Empty Then
            QRY &= " order by " & order
        End If

        Return Data.OpenData(QRY)
    End Function

    Public Function selectLevelsFila(ByVal pac_id As String, ByVal hierarchy As String, Optional ByVal name As String = "") As DataRow

        QRY = "select id, name, pac_id, hierarchy, state from levels where pac_id = " & pac_id & " and hierarchy = '" & hierarchy & "'"

        If name <> String.Empty Then
            QRY &= "and name = '" & name & "'"
        End If

        QRY &= "and state = 'A' "

        Return Data.OpenRow(QRY)
    End Function

    Public Function insertLevels(ByVal name As String, ByVal pac_id As String, ByVal hierarchy As String, ByVal state As String) As Integer

        QRY = "insert into levels(name, pac_id, hierarchy,  state) values (" &
              "'" & name & "', " & pac_id & ", " & hierarchy & ", '" & state & "') "

        Return Data.Execute(QRY)
    End Function

    Public Function updateLevels(ByVal id As String, name As String, ByVal hierarchy As String, ByVal state As String) As Integer

        QRY = "update levels set name = '" & name & "', hierarchy = '" & hierarchy & "', state = '" & state & "' where id = " & id & ""

        Return Data.Execute(QRY)
    End Function

    Public Function deleteLevels(ByVal id As String, ByVal state As String) As Integer

        QRY = "update levels set  state = '" & state & "' where id = " & id & ""

        Return Data.Execute(QRY)
    End Function

    Public Function selectNiveles(ByVal pac_id As String, Optional ByVal sublevel As String = "") As DataTable

        QRY = "select c.code, c.name, l.name,  l.hierarchy, l.name from contents c join levels l on c.pac_id = l.pac_id and c.level_id = l.hierarchy where 
               c.state = 'A' and  c.pac_id = " & pac_id & " and c.sublevel = '" & sublevel & "'"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoalsXsubactivity(ByVal pac_id As String, ByVal subactivity As String) As DataTable

        QRY = "select id, CONCAT(subactivity, ' - ', name) name from goals where state = 'A' and subactivity like '" & subactivity & "%' and pac_id = " & pac_id

        Return Data.OpenData(QRY)
    End Function

#End Region

#Region "Contents"
    Public Function selectContents(ByVal pac_id As String, ByVal level_id As String, Optional ByVal sublevel As String = "",
                                   Optional ByVal code As String = "") As DataTable

        QRY = "select * from contents where pac_id = " & pac_id & " and level_id = '" & level_id & "' and state = 'A' "

        If sublevel <> String.Empty Then
            QRY &= "and sublevel = '" & sublevel & "' "
        End If

        If code <> String.Empty Then
            QRY &= "and code = '" & code & "' "
        End If

        QRY &= "order by code"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectContents(ByVal pac_id As String, ByVal code As String) As DataTable

        QRY = "select * from contents where pac_id = " & pac_id & " and code = '" & code & "' and state = 'A'"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectContentsFiltro(ByVal pac_id As String, ByVal code As String, ByVal level_id As String) As DataTable

        QRY = "select * from contents where pac_id = " & pac_id & " and state = 'A' and code like '" & code & "%' and 
               level_id = " & level_id & " order by code"

        Return Data.OpenData(QRY)
    End Function
    Public Function selectContentsFiltroReg(ByVal pac_id As String, ByVal code As String, ByVal level_id As String) As DataTable

        QRY = "select code, name, from contents where pac_id = " & pac_id & " and state = 'A' and code like '" & code & "%' and 
               level_id = " & level_id & " order by code"

        Return Data.OpenData(QRY)
    End Function
    Public Function selectContents(ByVal pac_id As String) As DataTable

        QRY = "select * from contents where pac_id = " & pac_id & " and state = 'A' order by code"

        Return Data.OpenData(QRY)
    End Function
    Public Function insertContents(ByVal pac_id As String, ByVal level_id As String, ByVal code As String,
                               ByVal name_level As String, ByVal sublevel As String, ByVal name As String,
                               ByVal weigth As String, ByVal state As String, ByVal array As String) As Integer

        QRY = "insert into contents (pac_id, level_id, code, name_level, sublevel, name, weigth, state, array) values ( " &
              "" & pac_id & ", " & level_id & ", '" & code & "', '" & name_level & "', '" & sublevel & "', " &
              "'" & name & "', " & weigth & ",  '" & state & "', '" & array & "') "

        Return Data.Execute(QRY)
    End Function

    Public Function updateContents(ByVal id As String, code As String, ByVal name As String, ByVal weigth As String) As Integer

        QRY = "update contents set code = '" & code & "', name = '" & name & "', weigth = '" & weigth & "' where id = '" & id & "' "

        Return Data.Execute(QRY)
    End Function
    Public Function deleteContents(ByVal id As String, ByVal state As String) As Integer

        QRY = "update contents set state = '" & state & "' where id = '" & id & "' "

        Return Data.Execute(QRY)
    End Function

#End Region

#Region "Goals"
    Public Function insertGoals(ByVal pac_id As String, ByVal name As String, ByVal type_goal As String,
                                ByVal subactivity As String, ByVal line_base As String, ByVal value_one_year As String,
                                ByVal value_two_year As String, ByVal value_three_year As String, ByVal value_four_year As String,
                                ByVal responsable_id As String, ByVal feeder_id As String, ByVal state As String) As Integer

        QRY = "insert into goals (pac_id, name, type_goal, subactivity, line_base, value_one_year, value_two_year,
               value_three_year, value_four_year, responsable_id, feeder_id, state, value_progress) values ( " & pac_id & ", 
               '" & name & "', '" & type_goal & "', '" & subactivity & "', " & line_base & ", " & value_one_year & ", 
               " & value_two_year & ", " & value_three_year & ",  " & value_four_year & ", '" & responsable_id & "',
               '" & feeder_id & "', '" & state & "', 0) "

        Return Data.Execute(QRY)
    End Function

    Public Function updateGoals(ByVal id As String, ByVal name As String, ByVal type_goal As String,
                                ByVal subactivity As String, ByVal line_base As String, ByVal value_one_year As String,
                                ByVal value_two_year As String, ByVal value_three_year As String, ByVal value_four_year As String,
                                ByVal responsable_id As String, ByVal feeder_id As String) As Integer

        QRY = "update goals  set name = '" & name & "', type_goal = '" & type_goal & "', subactivity = '" & subactivity & "',
               line_base = " & line_base & ", value_one_year =  " & value_one_year & ",  value_two_year = " & value_two_year & ",
               value_three_year = " & value_three_year & ", value_four_year =  " & value_four_year & ", 
               responsable_id = '" & responsable_id & "', feeder_id = '" & feeder_id & "' where  id = " & id

        Return Data.Execute(QRY)
    End Function

    Public Function updateStateGoals(ByVal id As String) As Integer

        QRY = "update goals  set state = 'I' where  id = " & id

        Return Data.Execute(QRY)
    End Function

    Public Function selectGoals() As DataTable

        QRY = "select * from goals where state = 'A' order by id"

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal pac_id As String) As DataTable

        QRY = "select * from goals where pac_id = " & pac_id & " and state = 'A' "

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoals(ByVal pac_id As String, ByVal id As String) As DataRow

        QRY = "select * from goals where pac_id = " & pac_id & " and id = " & id & " and state = 'A' "

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectGoalsFiltro(ByVal pac_id As String, ByVal subactivity As String) As DataTable

        QRY = "select * from goals where state = 'A' and subactivity like '" & subactivity & "%' and pac_id = " & pac_id

        Return Data.OpenData(QRY)
    End Function

    Public Function selectGoalsFila(ByVal id As String) As DataRow

        QRY = "select * from goals where  id = " & id & " and state = 'A' "

        Return Data.OpenRow(QRY)
    End Function
    Public Function selectGoalsData(ByVal id As String) As DataTable

        QRY = "select * from goals where  id = " & id & " and state = 'A' "

        Return Data.OpenData(QRY)
    End Function

    Public Function updateValue_progress(ByVal id As String, ByVal value_progress As String) As DataRow

        QRY = "update goals set value_progress = (value_progress + " & value_progress & ") where id = " & id

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectComdepndnc() As DataTable

        QRY = "select codg_depndnc, nombr_depndnc from COMDEPNDNC order by nombr_depndnc"

        Return Data.OpenData(QRY)
    End Function

#End Region

#Region "Curriculum"

    Public Function insertCurriculum(ByVal goal_id As String, ByVal initials As String, ByVal description As String,
                                     ByVal definition As String, ByVal method As String, ByVal formulas As String,
                                     ByVal variables As String, ByVal observations As String, ByVal geographic As String,
                                     ByVal periodicity As String, ByVal state As String) As Integer

        QRY = "insert into curriculum (goal_id, initials, description, definition, method, formulas, variables,
               observations, geographic, periodicity, state) values ( " & goal_id & ", 
               '" & initials & "', '" & description & "', '" & definition & "', '" & method & "', '" & formulas & "', 
               '" & variables & "', '" & observations & "',  '" & geographic & "', '" & periodicity & "',
               '" & state & "') "

        Return Data.Execute(QRY)
    End Function

    Public Function updateCurriculum(ByVal goal_id As String, ByVal initials As String, ByVal description As String,
                                     ByVal definition As String, ByVal method As String, ByVal formulas As String,
                                     ByVal variables As String, ByVal observations As String, ByVal geographic As String,
                                     ByVal periodicity As String) As Integer

        QRY = "update curriculum set initials = '" & initials & "', description = '" & description & "', definition = '" & definition & "',
               method = '" & method & "', formulas =  '" & formulas & "',  variables = '" & variables & "',
               observations = '" & observations & "', geographic =  '" & geographic & "', 
               periodicity = '" & periodicity & "' where goal_id = " & goal_id

        Return Data.Execute(QRY)
    End Function

    Public Function updateStateCurriculum(ByVal id As String) As Integer

        QRY = "update curriculum set state = 'I' where  id = " & id

        Return Data.Execute(QRY)
    End Function

    Public Function selectCurriculum(ByVal goal_id As String) As DataRow

        QRY = "select * from curriculum where goal_id = " & goal_id & " and state = 'A' "

        Return Data.OpenRow(QRY)
    End Function

#End Region

#Region "report"
    Public Function insertReport(ByVal id_goal As String, ByVal year_current As String, ByVal value_progress As String,
                                 ByVal activities_developed As String, ByVal date_reg As String, ByVal state As String,
                                 ByVal user_reg As String, ByVal who_report As String) As Integer
        Dim row As DataRow

        QRY = "insert into report(goal_id, year_current, value_progress, activities_developed, 
               date_reg, state, user_reg, who_report) values (" & id_goal & "," & year_current & ",
               " & value_progress & ", '" & activities_developed & "', '" & date_reg & "', '" & state & "',
               '" & user_reg & "', '" & who_report & "')"

        Data.Execute(QRY)

        row = Data.OpenRow("select id from report order by id desc")
        If row IsNot Nothing Then
            insertReport = CInt(row("id"))
        End If

        Return insertReport
    End Function

    Public Function updateAdjuntosReport(ByVal campo As String, ByVal valor As String, ByVal id As String) As Integer

        QRY = "update report set " & campo & " = '" & valor & "' where id =" & id

        Return Data.Execute(QRY)
    End Function

#End Region

End Class
