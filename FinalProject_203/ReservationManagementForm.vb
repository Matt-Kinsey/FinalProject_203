Option Strict On
Option Explicit On

Public Class ReservationManagementForm

    Private res As Reservation

    Public Sub New(ByRef r As Reservation)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        res = r
    End Sub

    Private Sub ReservationManagementForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Fill in the combobox with all the users
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        For Each user As User In users
            cboUsername.Items.Add(user)
        Next

        Dim equip As Equipment = DBUtilities.GetEquipmentByID(res.EquipmentID) 'Get the equipment object (So we can show the name of the equipment)
        'Fill in controls with Reservation information
        dtpBegin.Value = res.DateStart
        dtpEnd.Value = res.DateEnd
        txtNotes.Text = res.Notes
        txtEquipment.Text = equip.ToString()
        For Each user In cboUsername.Items
            If CType(user, User).ID = res.UserID Then
                cboUsername.SelectedItem = user 'Make sure we select the correct user for the reservation
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'Do some input validation before we proceed
        If dtpBegin.Value = Nothing Then
            errProvider.SetError(dtpBegin, "Select a date")
            Return
        ElseIf dtpEnd.Value = Nothing Then
            errProvider.SetError(dtpEnd, "Select a date")
            Return
        ElseIf cboUsername.SelectedIndex = -1 Then
            errProvider.SetError(cboUsername, "Select a user")
            Return
        End If

        res.DateStart = Date.Parse(dtpBegin.Value.ToShortDateString) 'Strip time data from the date
        res.DateEnd = Date.Parse(dtpEnd.Value.ToShortDateString) 'Strip time data from the date
        res.UserID = CType(cboUsername.SelectedItem, User).ID 'Get the ID from the User object
        res.Notes = txtNotes.Text

        DBUtilities.EditReservation(res)
        Me.Close() 'Close the form after
    End Sub
End Class