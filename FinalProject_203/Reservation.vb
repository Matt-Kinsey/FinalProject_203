Option Explicit On
Option Strict On

Public Class Reservation
    Public ReadOnly Property ID As Integer
    Public Property DateStart As Date
    Public Property DateEnd As Date
    Public Property UserID As String
    Public ReadOnly Property EquipmentID As Integer
    Public Property Notes As String
End Class
