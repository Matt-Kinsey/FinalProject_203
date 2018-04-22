Option Strict On
Option Explicit On

Public Class ReservationsListForm
    Private Sub ReservationsListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        'Populate the combobox of Users
        For Each user As User In users
            cboUsername.Items.Add(user)
        Next
        cboUsername.SelectedIndex = 0 'Select the first user because it doesn't hurt anything
    End Sub

    Private Sub ReservationsListForm_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        SearchForm.PopulateEquipment() 'Refresh the main search form equipment list to reflect available equipment
    End Sub

    Private Sub cboUsername_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsername.SelectedIndexChanged
        RefreshItems() 'Refresh the list for the new selected user
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lstReservations.SelectedIndex = -1 Then 'Make sure a reservation is selected
            errProvider.SetError(lstReservations, "Please select an item to edit")
            Return
        End If

        Dim res As Reservation = CType(lstReservations.SelectedItem, Reservation) 'Get the reservation object from the listbox

        Dim form As New ReservationManagementForm(res)
        form.ShowDialog()
        'Pass of the editing to the mangement form and then refresh the list
        RefreshItems()
    End Sub

    ''' <summary>
    ''' Refresh the list of reservations
    ''' </summary>
    Private Sub RefreshItems()
        lstReservations.Items.Clear()
        Dim reservations As List(Of Reservation) = DBUtilities.GetReservationsOfUser(CType(cboUsername.SelectedItem, User))
        For Each res As Reservation In reservations
            lstReservations.Items.Add(res)
        Next
        errProvider.Clear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lstReservations.SelectedIndex = -1 Then 'Make sure a reservation is selected
            errProvider.SetError(lstReservations, "Please select an item to edit")
            Return
        End If

        Dim res As Reservation = CType(lstReservations.SelectedItem, Reservation) 'Get the reservation object from the listbox

        DBUtilities.DeleteReservation(res) 'Delete the reservation
        'Refresh the list to reflect the delete
        RefreshItems()
    End Sub
End Class