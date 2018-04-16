Option Explicit On
Option Strict On

Public Class User
    Public ReadOnly Property ID As Integer
    Public ReadOnly Property UserName As String
    Public ReadOnly Property FullName As String
    Public ReadOnly Property SSN As Integer

    Public Sub New(ByVal pID As Integer, ByVal pUsername As String, ByVal pfullname As String, ByVal pSSN As Integer)
        ID = pID
        UserName = pUsername
        FullName = pfullname
        SSN = pSSN
    End Sub

    Public Sub New(ByRef u As User)
        'Copy constructor
        ID = u.ID
        UserName = u.UserName
        FullName = u.FullName
        SSN = u.SSN
    End Sub

    Public Overrides Function ToString() As String
        Return UserName
    End Function
End Class
