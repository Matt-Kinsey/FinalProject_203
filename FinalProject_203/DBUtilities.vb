Option Explicit On
Option Strict On

Imports System.Data.SQLite

Public Class DBUtilities
    Private Const connect As String = "Data Source = ../../EquipmentBooking.db; Version = 3;"

    Private Shared con As New SQLiteConnection(connect)

    Public Shared Function GetAllEquipment() As List(Of Equipment)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Equipment)

        cmd.CommandText = "SELECT * FROM Equipment"

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Equipment(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(0)))
        End While


        con.Close()

        Return list
    End Function

    ''' <summary>
    ''' Adds equipment to the database
    ''' </summary>
    ''' <param name="equip">The Equipment object to add</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function AddEquipment(ByRef equip As Equipment) As Boolean
        con.Open()

        Dim sql As String = "INSERT INTO Equipment(Name, Description) VALUES(@Name, @Description)"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@Name", equip.Name)
        cmd.Parameters.AddWithValue("@Description", equip.Description)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

    ''' <summary>
    ''' Edits an equipment in the database
    ''' </summary>
    ''' <param name="equip">The Equipment object to edit</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function EditEquipment(ByRef equip As Equipment) As Boolean
        con.Open()

        Dim sql As String = "UPDATE Equipment SET Name = @Name, Description = @Description WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@Name", equip.Name)
        cmd.Parameters.AddWithValue("@Description", equip.Description)
        cmd.Parameters.AddWithValue("@ID", equip.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

    ''' <summary>
    ''' Deletes an equipment from the database
    ''' </summary>
    ''' <param name="equip">The Equipment object to delete</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function DeleteEquipment(ByRef equip As Equipment) As Boolean
        con.Open()

        Dim sql As String = "DELETE FROM Equipment WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@ID", equip.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function



End Class
