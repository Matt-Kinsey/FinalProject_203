Option Explicit On
Option Strict On

Public Class Reservation
    Public ReadOnly Property ID As Integer
    Public Property DateStart As Date
    Public Property DateEnd As Date
    Public Property UserID As Integer
    Public ReadOnly Property EquipmentID As Integer
    Public Property Notes As String

    Public Sub New(ByVal id As Integer, ByVal date_start As Date, ByVal date_end As Date, ByVal user_id As Integer, ByVal equipment_id As Integer, ByVal notes As String)
        Me.ID = id
        DateStart = date_start
        DateEnd = date_end
        UserID = user_id
        EquipmentID = equipment_id
        Me.Notes = notes
    End Sub

    Public Sub New(ByRef res As Reservation)
        'Copy constructor
        Me.ID = res.ID
        DateStart = res.DateStart
        DateEnd = res.DateEnd
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
