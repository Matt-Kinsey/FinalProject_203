Option Strict On
Option Explicit On

Public Class SearchForm
    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpBegin.MinDate = Date.Now.AddDays(1)
        dtpEnd.MinDate = Date.Now.AddDays(8)

        cboUsers.Items.Clear()
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        For Each user As User In users
            cboUsers.Items.Add(user)
        Next
        PopulateEquipment()
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
        If cboUsers.SelectedIndex = -1 Or lstEquipment.SelectedIndex = -1 Then
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment)
        Dim user As User = CType(cboUsers.SelectedItem, User)
        Dim res As New Reservation(0, dtpBegin.Value, dtpEnd.Value, user.ID, equip.ID, "")

        DBUtilities.CreateReservation(res)
        PopulateEquipment()
    End Sub

    Public Sub PopulateEquipment()
        lstEquipment.Items.Clear()
        Dim equip As List(Of Equipment) = DBUtilities.GetAvailableEquipment(dtpBegin.Value, dtpEnd.Value)
        If (equip Is Nothing) Then
            Return
        End If
        For Each e As Equipment In equip
            lstEquipment.Items.Add(e)
        Next

    End Sub

    Private Sub dtpBegin_ValueChanged(sender As Object, e As EventArgs) Handles dtpBegin.ValueChanged, dtpEnd.ValueChanged
        PopulateEquipment()
        Dim ctrl As DateTimePicker = CType(sender, DateTimePicker)
        If (ctrl.Name = "dtpEnd") Then
            ctrl.Tag = "1"
        End If
        If Not dtpEnd.Tag Is "1" Then
            dtpEnd.Value = dtpBegin.Value.AddDays(7)
        End If
    End Sub
End Class
