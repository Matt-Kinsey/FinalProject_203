Option Strict On
Option Explicit On

Public Class SearchForm
    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpBegin.MinDate = Date.Now.AddDays(1)
        dtpEnd.MinDate = Date.Now.AddDays(8)

        Dim equipment As List(Of Equipment) = DBUtilities.GetAllEquipment()
        lstEquipment.Items.Clear()

        For Each equip As Equipment In equipment
            lstEquipment.Items.Add(equip)
        Next

        cboUsers.Items.Clear()
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        For Each user As User In users
            cboUsers.Items.Add(user)
        Next

    End Sub

    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click
        Dim form As New EquipmentListForm
        form.ShowDialog()
    End Sub

    Private Sub ListToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem1.Click
        Dim form As New ReservationsListForm
        form.ShowDialog()
    End Sub

    Private Sub btnReserve_Click(sender As Object, e As EventArgs) Handles btnReserve.Click
        If Not dtpBegin.Value > Date.Now Then
            Return
        End If
        If Not dtpEnd.Value > dtpBegin.Value Then
            Return
        End If
        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment)
        Dim user As User = CType(cboUsers.SelectedItem, User)
        Dim res As New Reservation(0, dtpBegin.Value, dtpEnd.Value, user.ID, equip.ID, "")

        DBUtilities.CreateReservation(res)
    End Sub
End Class
