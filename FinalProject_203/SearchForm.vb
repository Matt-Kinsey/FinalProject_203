Option Strict On
Option Explicit On

Public Class SearchForm
    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpBegin.MinDate = Date.Now
        dtpEnd.MinDate = Date.Now
    End Sub
End Class
