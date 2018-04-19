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

    Public Shared Function GetAllUsers() As List(Of User)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of User)

        cmd.CommandText = "SELECT * FROM User"

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3)))
        End While

        con.Close()

        Return list
    End Function

    Public Shared Function GetReservationsOfUser(ByRef user As User) As List(Of Reservation)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Reservation)

        cmd.CommandText = "SELECT * FROM Reservation WHERE userid = @UserID"

        cmd.Parameters.AddWithValue("@UserID", user.ID)

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Reservation(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5)))
        End While

        con.Close()

        Return list
    End Function

    Public Shared Function GetReservationsOfEquipment(ByRef equip As Equipment) As List(Of Reservation)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Reservation)

        cmd.CommandText = "SELECT * FROM Reservation WHERE equipmentid = @EquipID"

        cmd.Parameters.AddWithValue("@EquipID", equip.ID)

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Reservation(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5)))
        End While

        con.Close()

        Return list
    End Function

    Public Shared Function GetAllReservations() As List(Of Reservation)
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Reservation)

        cmd.CommandText = "SELECT * FROM Reservation"

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Reservation(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5)))
        End While

        con.Close()

        Return list
    End Function

    Public Shared Function CreateReservation(ByRef res As Reservation) As Boolean
        Dim equip As List(Of Equipment) = GetAvailableEquipment(res.DateStart, res.DateEnd)
        Dim isAvailable = False
        For Each e As Equipment In equip 'Make sure equipment is available
            If e.ID = res.EquipmentID Then
                isAvailable = True
                Exit For
            End If
        Next
        If Not isAvailable Then
            Return False
        End If
        If res.DateStart.CompareTo(res.DateEnd) > 0 Then 'If start > end
            Return False
        End If

        con.Open()

        Dim sql As String = "INSERT INTO Reservation(date_start, date_end, userid, equipmentid, notes) VALUES(@DateStart, @DateEnd, @UserID, @EquipmentID, @Notes)"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@DateStart", res.DateStart)
        cmd.Parameters.AddWithValue("@DateEnd", res.DateEnd)
        cmd.Parameters.AddWithValue("@UserID", res.UserID)
        cmd.Parameters.AddWithValue("@EquipmentID", res.EquipmentID)
        cmd.Parameters.AddWithValue("@Notes", res.Notes)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

    Public Shared Function GetAvailableEquipment(ByVal date_start As Date, ByVal date_end As Date) As List(Of Equipment)
        If date_start.CompareTo(date_end) > 0 Then
            Return Nothing
        End If

        Dim equip As List(Of Equipment) = GetAllEquipment()
        Dim available As New List(Of Equipment)

        date_start = Date.Parse(date_start.ToShortDateString)
        date_end = Date.Parse(date_end.ToShortDateString)
        For Each e As Equipment In equip
            Dim reservations = GetReservationsOfEquipment(e)
            Dim avail As Boolean = True
            For Each res As Reservation In reservations

                If DateBetween(date_start, res.DateStart, res.DateEnd) Or DateBetween(date_end, res.DateStart, res.DateEnd) Then 'Start or end is between
                    avail = False
                ElseIf date_start.CompareTo(res.DateStart) < 0 And date_end.CompareTo(res.DateEnd) > 0 Then
                    avail = False
                End If
            Next
            If avail Then
                available.Add(e)
            End If
        Next
        Return available
    End Function

    Public Shared Function GetUserByID(ByVal userid As Integer) As User
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim u As User

        cmd.CommandText = "SELECT * FROM User WHERE id = @UserID"
        cmd.Parameters.AddWithValue("@UserID", userid)

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            u = New User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3))
        End While

        con.Close()

        Return u
    End Function

    Public Shared Function GetEquipmentByID(ByVal equipid As Integer) As Equipment
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim equip As Equipment

        cmd.CommandText = "SELECT * FROM Equipment WHERE id = @EquipID"
        cmd.Parameters.AddWithValue("@EquipID", equipid)

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            equip = New Equipment(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(0))
        End While

        con.Close()

        Return equip
    End Function

    Private Shared Function DateBetween(ByVal test_date As Date, ByVal start_date As Date, end_date As Date) As Boolean
        Return test_date >= start_date And test_date <= end_date
    End Function

    ''' <summary>
    ''' Deletes a provided reservation from the database
    ''' </summary>
    ''' <param name="res">The reservation to delete</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function DeleteReservation(ByRef res As Reservation) As Boolean
        con.Open()

        Dim sql As String = "DELETE FROM Reservation WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@ID", res.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

End Class
