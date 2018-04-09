Option Explicit On
Option Strict On

Public Class Equipment

    Public Property Name As String
    Public Property Description As String
    Public Property ID As Integer

    Public Sub New(ByVal pName As String, ByVal pDescription As String)
        Name = pName
        Description = pDescription
    End Sub

    Public Sub New(ByVal pName As String, ByVal pDescription As String, ByVal pID As Integer)
        Name = pName
        Description = pDescription
        ID = pID
    End Sub

    Public Sub New(ByRef equip As Equipment)
        'Cloning constructor
        Name = equip.Name
        Description = equip.Description
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
