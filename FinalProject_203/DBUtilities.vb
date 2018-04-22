Option Explicit On
Option Strict On

Imports System.Data.SQLite

Public Class DBUtilities
    Private Const connect As String = "Data Source = ../../EquipmentBooking.db; Version = 3;" 'Connection string for the database

    Private Shared con As New SQLiteConnection(connect) 'Connection object used by all methods

    ''' <summary>
    ''' Gets a list of all equipment in the database
    ''' </summary>
    ''' <returns>A List(Of Equipment) that contains every piece of equipment in the database</returns>
    Public Shared Function GetAllEquipment() As List(Of Equipment)
        con.Open() 'Open connection to the database

        Dim cmd As New SQLiteCommand(con)
        Dim list As New List(Of Equipment) 'Return list

        cmd.CommandText = "SELECT * FROM Equipment"

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            list.Add(New Equipment(rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(0))) 'Add each equipment to the list
        End While


        con.Close() 'Close database connection

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

        cmd.Parameters.AddWithValue("@Name", equip.Name) 'Add our parameters so we don't get SQL Injected
        cmd.Parameters.AddWithValue("@Description", equip.Description)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0 'Count = the number of rows affected (1 is good, 0 is bad)
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

        cmd.Parameters.AddWithValue("@Name", equip.Name) 'Add our parameters so we don't get SQL Injected
        cmd.Parameters.AddWithValue("@Description", equip.Description)
        cmd.Parameters.AddWithValue("@ID", equip.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0 'Count = the number of rows affected (1 is good, 0 is bad)
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

        cmd.Parameters.AddWithValue("@ID", equip.ID) 'Add our parameters so we don't get SQL Injected

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0 'Count = the number of rows affected (1 is good, 0 is bad)
    End Function

    ''' <summary>
    ''' Gets a list of all users in the database
    ''' </summary>
    ''' <returns>A List(Of User) that contains every user in the database</returns>
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

    ''' <summary>
    ''' Gets a list of reservations belonging to a particular user
    ''' </summary>
    ''' <param name="user">The User object whose reservations we're getting</param>
    ''' <returns>A List(Of Reservation) of all reservations made by the specified user</returns>
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

    ''' <summary>
    ''' Gets a list of reservations of a particular piece of equipment
    ''' </summary>
    ''' <param name="equip">The Equipment object whose reservations of we want to get</param>
    ''' <returns>A List(Of Reservation) of all reservations of the specified equipment</returns>
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

    ''' <summary>
    ''' Get a single reservation by its Id value
    ''' </summary>
    ''' <param name="id">The ID of the reservation to be retrieved</param>
    ''' <returns>A Reservation object matching the id specified, or Nothing if not found</returns>
    Public Shared Function GetReservationByID(ByVal id As Integer) As Reservation
        con.Open()

        Dim cmd As New SQLiteCommand(con)
        Dim res As Reservation

        cmd.CommandText = "SELECT * FROM Reservation WHERE Id = @ID"

        cmd.Parameters.AddWithValue("@ID", id)

        Dim rdr As SQLiteDataReader = cmd.ExecuteReader()

        While (rdr.Read())
            res = New Reservation(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetDateTime(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5))
        End While

        con.Close()

        Return res
    End Function

    ''' <summary>
    ''' Gets a list of all reservations in the database
    ''' </summary>
    ''' <returns>A List(Of Reservation) that contains reservation in the database</returns>
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

    ''' <summary>
    ''' Adds a reservation to the database
    ''' </summary>
    ''' <param name="res">The Reservation object to be added</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function CreateReservation(ByRef res As Reservation) As Boolean

        Dim equip As List(Of Equipment) = GetAvailableEquipment(res.DateStart, res.DateEnd) 'Get available equipment to check dates
        Dim isAvailable = False 'Flag to be used in loop
        For Each e As Equipment In equip 'Make sure equipment is available
            If e.ID = res.EquipmentID Then 'We found the equipment we are reserving in AVAILABLE equipment list
                isAvailable = True 'Now we know the equipment is available to be reserved
                Exit For
            End If
        Next
        If Not isAvailable Then
            Return False 'If we didn't find the equipment in the list
        End If
        If res.DateStart.CompareTo(res.DateEnd) > 0 Then 'If start > end
            Return False 'Check if the start date is after the end date
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

    ''' <summary>
    ''' Gets a list of available equipment (not reserved) for the given date span
    ''' </summary>
    ''' <param name="date_start">The starting date of the search</param>
    ''' <param name="date_end">The ending date of the search</param>
    ''' <returns>a List(Of Equipment) of equipment that are not reserved at any point in the date span</returns>
    Public Shared Function GetAvailableEquipment(ByVal date_start As Date, ByVal date_end As Date) As List(Of Equipment)
        date_start = Date.Parse(date_start.ToShortDateString) 'Strip time data from the dates
        date_end = Date.Parse(date_end.ToShortDateString) 'Strip time data from the dates

        If date_start.CompareTo(date_end) > 0 Then 'If the start date is after the end date
            Return Nothing
        End If

        Dim equip As List(Of Equipment) = GetAllEquipment() 'Get a lsit of all equipment to search through
        Dim available As New List(Of Equipment) 'Our return list


        For Each e As Equipment In equip 'Go through each piece of equipment
            Dim reservations = GetReservationsOfEquipment(e) 'Get the reservations for the equipment we're on
            Dim avail As Boolean = True 'Flag for the loop
            For Each res As Reservation In reservations 'Go through each reservation

                If DateBetween(date_start, res.DateStart, res.DateEnd) Or DateBetween(date_end, res.DateStart, res.DateEnd) Then 'Start or end is between the dates
                    avail = False
                ElseIf date_start.CompareTo(res.DateStart) < 0 And date_end.CompareTo(res.DateEnd) > 0 Then 'The start date is before the reservation start date and the end date is after the reservation end date
                    avail = False '(This reservation is completely within the date span)
                End If
            Next
            If avail Then
                available.Add(e) 'Add available equipment to the list
            End If
        Next
        Return available
    End Function

    ''' <summary>
    ''' Gets a User object by their ID (Primarily used for getting the username from a Reservation)
    ''' </summary>
    ''' <param name="userid">ID of the user</param>
    ''' <returns>The User object that corresponds to the given ID in the database, or Nothing if not found</returns>
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

    ''' <summary>
    ''' Gets a piece of equipment by its ID in the database
    ''' </summary>
    ''' <param name="equipid">ID of the equipment in the database</param>
    ''' <returns>The equipment object that matches the given ID, or Nothing if not found</returns>
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

    ''' <summary>
    ''' Used to check if the given test date is between the start date and the end date
    ''' </summary>
    ''' <param name="test_date">The date we're checking</param>
    ''' <param name="start_date">The start date of the date span we're checking</param>
    ''' <param name="end_date">The end date of the date span we're checking</param>
    ''' <returns>True if the test date is between, false if isn't</returns>
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

    ''' <summary>
    ''' Edits a reservation in the database
    ''' </summary>
    ''' <param name="res">The Reservation object to be edited (Edited fields already changed, matched by ID)</param>
    ''' <returns>True on success, false on failure</returns>
    Public Shared Function EditReservation(ByRef res As Reservation) As Boolean
        If res.DateStart.CompareTo(res.DateEnd) > 0 Then 'If start > end
            Return False
        End If

        Dim oldRes As Reservation = GetReservationByID(res.ID) 'Make sure we save the old dates in case input validation says no

        'We need to set the date far back so the available equipment check doesn't get tripped by this reservation
        'If we don't do this, the check later will fail on the reservation itself
        If Not ResetReservationDate(res) Then 'If we can't reset the date
            Return False
        End If

        'Same process as AddEquipment
        Dim equip As List(Of Equipment) = GetAvailableEquipment(res.DateStart, res.DateEnd) 'Get not reserved equipment during the date span
        Dim isAvailable = False 'Flag for loop
        For Each e As Equipment In equip 'Make sure equipment is available
            If e.ID = res.EquipmentID Then 'If we find our equipment in the available equipment list, we know it's good
                isAvailable = True
                Exit For
            End If
        Next
        If Not isAvailable Then 'If the new date span doesn't work
            SetReservationDate(oldRes) 'Make sure we fix the date before we error out
            Return False
        End If

        con.Open()

        Dim sql As String = "UPDATE Reservation SET date_start = @DateStart, date_end = @DateEnd, userid = @UserID, notes = @Notes WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@DateStart", res.DateStart)
        cmd.Parameters.AddWithValue("@DateEnd", res.DateEnd)
        cmd.Parameters.AddWithValue("@UserID", res.UserID)
        cmd.Parameters.AddWithValue("@Notes", res.Notes)
        cmd.Parameters.AddWithValue("@ID", res.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0

    End Function

    ''' <summary>
    ''' Sets reservation date in database to epoch
    ''' </summary>
    ''' <param name="res">Reservation to change</param>
    ''' <returns>True on success, false on failure</returns>
    Private Shared Function ResetReservationDate(ByRef res As Reservation) As Boolean
        con.Open()

        Dim sql As String = "UPDATE Reservation SET date_start = @DateStart, date_end = @DateEnd WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@DateStart", New Date(1970, 1, 1))
        cmd.Parameters.AddWithValue("@DateEnd", New Date(1970, 1, 2))
        cmd.Parameters.AddWithValue("@ID", res.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

    ''' <summary>
    ''' Sets reservation date in database to the one in the object. To be used after ResetReservationDate()
    ''' </summary>
    ''' <param name="res">Reservation to change</param>
    ''' <returns>True on success, false on failure</returns>
    Private Shared Function SetReservationDate(ByRef res As Reservation) As Boolean
        con.Open()

        Dim sql As String = "UPDATE Reservation SET date_start = @DateStart, date_end = @DateEnd WHERE ID = @ID"

        Dim cmd As New SQLiteCommand(sql, con)

        Dim count As Integer

        cmd.Parameters.AddWithValue("@DateStart", res.DateStart)
        cmd.Parameters.AddWithValue("@DateEnd", res.DateEnd)
        cmd.Parameters.AddWithValue("@ID", res.ID)

        count = cmd.ExecuteNonQuery()

        con.Close()

        Return count > 0
    End Function

End Class
