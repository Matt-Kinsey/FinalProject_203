Option Explicit On
Option Strict On

Public Class User
    Public ReadOnly Property ID As Integer 'ID from the database
    Public ReadOnly Property UserName As String 'Username of the user
    Public ReadOnly Property FullName As String 'Full name of the user
    Public ReadOnly Property SSN As Integer 'SSN of the user

    ''' <summary>
    ''' Basic constructor for all fields
    ''' </summary>
    ''' <param name="pID">ID from the database</param>
    ''' <param name="pUsername">Username of the user</param>
    ''' <param name="pfullname">Full name of the user</param>
    ''' <param name="pSSN">SSN of the user</param>
    Public Sub New(ByVal pID As Integer, ByVal pUsername As String, ByVal pfullname As String, ByVal pSSN As Integer)
        ID = pID
        UserName = pUsername
        FullName = pfullname
        SSN = pSSN
    End Sub

    ''' <summary>
    ''' Cloning constructor
    ''' </summary>
    ''' <param name="u">User object to clone</param>
    Public Sub New(ByRef u As User)
        ID = u.ID
        UserName = u.UserName
        FullName = u.FullName
        SSN = u.SSN
    End Sub

    Public Overrides Function ToString() As String
        Return UserName
    End Function
End Class
