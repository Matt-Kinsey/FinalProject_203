Option Strict On
Option Explicit On

Public Class SearchForm
    Private Sub SearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpBegin.MinDate = Date.Now.AddDays(1) 'Start date is tomorrow
        dtpEnd.MinDate = Date.Now.AddDays(8) 'End date is a week from tomorrow

        'Populate the combobox of users
        cboUsers.Items.Clear()
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        For Each user As User In users
            cboUsers.Items.Add(user)
        Next
        PopulateEquipment() 'Refresh list of available equipment
    End Sub

    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click
        'Equipment List menu strip item
        Dim form As New EquipmentListForm
        form.ShowDialog()
    End Sub

    Private Sub ListToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem1.Click
        'Reservation List menu strip item
        Dim form As New ReservationsListForm
        form.ShowDialog()
    End Sub

    Private Sub btnReserve_Click(sender As Object, e As EventArgs) Handles btnReserve.Click
        'Input validation
        If Not dtpBegin.Value > Date.Now Then
            errProvider.SetError(dtpBegin, "Start date cannot be in the past")
            Return
        End If
        If Not dtpEnd.Value > dtpBegin.Value Then
            errProvider.SetError(dtpEnd, "End date cannot be before start date")
            Return
        End If
        If cboUsers.SelectedIndex = -1 Then
            MessageBox.Show("Select a user") 'Cannot set errProvider on menu strip item
            Return
        ElseIf lstEquipment.SelectedIndex = -1 Then
            errProvider.SetError(lstEquipment, "Select equipment")
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment) 'Get equipment object from listbox
        Dim user As User = CType(cboUsers.SelectedItem, User) 'Get user objet from combobox
        Dim res As New Reservation(0, dtpBegin.Value, dtpEnd.Value, user.ID, equip.ID, "") 'Create the reservation object

        'Add the reservation to the database and refresh list
        DBUtilities.CreateReservation(res)
        PopulateEquipment()
    End Sub

    ''' <summary>
    ''' Refresh the list of available equipment (and clear error provider)
    ''' </summary>
    Public Sub PopulateEquipment()
        lstEquipment.Items.Clear()
        Dim equip As List(Of Equipment) = DBUtilities.GetAvailableEquipment(dtpBegin.Value, dtpEnd.Value)
        If (equip Is Nothing) Then
            Return
        End If
        For Each e As Equipment In equip
            lstEquipment.Items.Add(e)
        Next
        errProvider.Clear() 'Make sure the errors are cleared (This function gets called almost always)
    End Sub

    Private Sub dtpBegin_ValueChanged(sender As Object, e As EventArgs) Handles dtpBegin.ValueChanged, dtpEnd.ValueChanged

        Dim ctrl As DateTimePicker = CType(sender, DateTimePicker)
        If (ctrl.Name = "dtpEnd") Then
            ctrl.Tag = "1" ' 1 = User changed
        End If
        If Not dtpEnd.Tag Is "1" Then 'If the user hasn't changed the end date, keep it at a week after start date
            dtpEnd.Value = dtpBegin.Value.AddDays(7)
        End If
        PopulateEquipment() 'Refresh the list when dates are changed
    End Sub
End Class
