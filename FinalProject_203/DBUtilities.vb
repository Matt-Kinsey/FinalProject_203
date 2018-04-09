Option Explicit On
Option Strict On

Imports System.Data.SQLite

Public Class DBUtilities
    Private Const connect As String = "Data Source = ../../EquipmentBooking.db; Version = 3;"

    Private Shared con As New SQLiteConnection(connect)

    Public Shared Sub Query(ByVal q As String)
        con.Open()

        Using cmd As New SQLiteCommand(con)

            cmd.CommandText = q

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

    Public Shared Function GetAllEquipment() As List(Of Equipment)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Equipment)

        cmd.CommandText = "SELECT * FROM Equipment"

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Equipment(rdr.GetString(1), rdr.GetString(2)))
        End While


        con.Close()

        Return list
    End Function



End Class
