Imports System.Data
Public Class clLogin
    Public Function selectUsuario(ByVal usuario_bd As String, Optional ByVal consulta As String = "") As DataRow

        If consulta = String.Empty Then
            QRY = "select usuario_bd, clave, rol_id, address, movil, encriptado, description, " &
                  "concat(nombr_empld, ' ', aplld_empld) as nombreEmp from RHMHOJVI " &
                  "join profiles on usuario_bd = user_id join roles on name = rol_id " &
                  "where usuario_bd = '" & usuario_bd & "' "
        ElseIf consulta = "1" Then
            QRY = "select  usuario_bd from    RHMHOJVI where   usuario_bd = '" & usuario_bd & "'" &
                  "and codg_carg  not like  '99%'"
        ElseIf consulta = "2" Then
            QRY = "select  usuario_bd from    RHMHOJVI where   usuario_bd = '" & usuario_bd & "'" &
                  "and codg_carg  not like  '99%' and fech_retr >= '" & Now.ToString("yyyy-MM-dd") & "'"
        End If

        Return Data.OpenRow(QRY)
    End Function

    Public Function selectUsuario() As DataTable

        QRY = "select p.user_id, h.clave, p.rol_id, p.address, p.movil, h.encriptado, r.description, 
               concat(h.nombr_empld, ' ', h.aplld_empld) as nombreEmp from RHMHOJVI h join profiles p on 
               h.usuario_bd = p.user_id join roles r on r.name = p.rol_id where p.state = 'A' "

        Return Data.OpenData(QRY)
    End Function

    Public Function selectMenu(ByVal rol As String) As DataTable

        QRY = "select * from menu where state = 'A' and rol = '" & rol & "'"

        Return Data.OpenData(QRY)
    End Function

End Class
