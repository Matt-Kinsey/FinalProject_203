Option Explicit On
Option Strict On

Imports System.Data.SQLite

Public Class DBUtilities
    Private Const connect As String = "URI=file:EquipmentBooking.db"

    Private Shared con As New SQLiteConnection(connect)

    Public Shared Sub Query(ByVal q As String)
        con.Open()

        Using cmd As New SQLiteCommand(con)

            cmd.CommandText = "SELECT * FROM Equipment LIMIT 5"

            Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

            Using rdr
                While (rdr.Read())
                    Console.WriteLine(rdr.GetInt32(0) & " " _
                        & rdr.GetString(1) & " " & rdr.GetInt32(2))
                End While
            End Using
        End Using

        con.Close()
    End Sub


End Class
