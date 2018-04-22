Option Explicit On
Option Strict On

Public Class Reservation
    Public ReadOnly Property ID As Integer 'ID of reservation from database
    Public Property DateStart As Date 'Start date of the reservation
    Public Property DateEnd As Date 'End date of the reservation
    Public Property UserID As Integer 'UserID from the reservation
    Public ReadOnly Property EquipmentID As Integer 'EquipmentID of the reserved equipment, readonly because equipmentid can't be edited (Make a new reservation)
    Public Property Notes As String 'Notes for the reservation

    ''' <summary>
    ''' Basic constructor for all fields
    ''' </summary>
    ''' <param name="id">ID from the database</param>
    ''' <param name="date_start">Start date of reservation</param>
    ''' <param name="date_end">End date of the reservation</param>
    ''' <param name="user_id">User ID creating the reservation</param>
    ''' <param name="equipment_id">Equipment ID being reserved</param>
    ''' <param name="notes">Notes from the reservatuin</param>
    Public Sub New(ByVal id As Integer, ByVal date_start As Date, ByVal date_end As Date, ByVal user_id As Integer, ByVal equipment_id As Integer, ByVal notes As String)
        Me.ID = id
        DateStart = Date.Parse(date_start.ToShortDateString) 'Strip time data from date
        DateEnd = Date.Parse(date_end.ToShortDateString) 'Strip time data from date
        UserID = user_id
        EquipmentID = equipment_id
        Me.Notes = notes
    End Sub

    ''' <summary>
    ''' Cloning constructor
    ''' </summary>
    ''' <param name="res">Reservation object to clone</param>
    Public Sub New(ByRef res As Reservation)

        Me.ID = res.ID
        DateStart = Date.Parse(res.DateStart.ToShortDateString) 'Strip time data from date
        DateEnd = Date.Parse(res.DateEnd.ToShortDateString) 'Strip time data from date
        UserID = res.UserID
        EquipmentID = res.EquipmentID
        Me.Notes = res.Notes
    End Sub

    Public Overrides Function ToString() As String
        Dim user As User = DBUtilities.GetUserByID(UserID)
        Dim equip As Equipment = DBUtilities.GetEquipmentByID(EquipmentID)

        Return user.UserName & ": " & equip.Name & vbTab & DateStart.ToShortDateString & " - " & DateEnd.ToShortDateString
    End Function
End Class
