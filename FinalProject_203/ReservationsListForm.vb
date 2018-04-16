Public Class ReservationsListForm
    Private Sub ReservationsListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim users As List(Of User) = DBUtilities.GetAllUsers()

        For Each user As User In users
            cboUsername.Items.Add(user)
        Next
        cboUsername.SelectedIndex = 0
    End Sub

    Private Sub cboUsername_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsername.SelectedIndexChanged
        lstReservations.Items.Clear()
        Dim reservations As List(Of Reservation) = DBUtilities.GetReservationsOfUser(CType(cboUsername.SelectedItem, User))
        For Each res As Reservation In reservations
            lstReservations.Items.Add(res)
        Next
    End Sub
End Class