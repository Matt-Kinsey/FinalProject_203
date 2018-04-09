Option Strict On
Option Explicit On

Public Class SearchForm
    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpBegin.MinDate = Date.Now
        dtpEnd.MinDate = Date.Now
    End Sub

    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click
        Dim form As New EquipmentListForm
        form.ShowDialog()
    End Sub
End Class
