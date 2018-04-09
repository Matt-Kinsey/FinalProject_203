Option Explicit On
Option Strict On

Public Class Equipment

    Public Property Name As String
    Public Property Description As String

    Public Sub New(ByVal pName As String, ByVal pDescription As String)
        Name = pName
        Description = pDescription
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
