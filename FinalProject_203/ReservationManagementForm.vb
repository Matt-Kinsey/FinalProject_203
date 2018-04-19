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
        Dim users As List(Of User) = DBUtilities.GetAllUsers()
        For Each user As User In users
            cboUsername.Items.Add(user)
        Next
        Dim equip As Equipment = DBUtilities.GetEquipmentByID(res.EquipmentID)

        dtpBegin.Value = res.DateStart
        dtpEnd.Value = res.DateEnd
        txtNotes.Text = res.Notes
        txtEquipment.Text = equip.ToString()
        For Each user In cboUsername.Items
            If CType(user, User).ID = res.UserID Then
                cboUsername.SelectedItem = user
            End If
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If dtpBegin.Value = Nothing Or dtpEnd.Value = Nothing Or cboUsername.SelectedIndex = -1 Then
            Return
        End If

        res.DateStart = Date.Parse(dtpBegin.Value.ToShortDateString)
        res.DateEnd = Date.Parse(dtpEnd.Value.ToShortDateString)
        res.UserID = CType(cboUsername.SelectedItem, User).ID
        res.Notes = txtNotes.Text

        DBUtilities.EditReservation(res)
        Me.Close()
    End Sub
End Class