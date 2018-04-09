Option Explicit On
Option Strict On

Public Class EquipmentListForm



    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim form As New EquipmentManagementForm(Nothing)
        form.ShowDialog()

        RefreshItems()
    End Sub


    Private Sub EquipmentListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshItems()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lstEquipment.SelectedIndex = -1 Then
            errProvider.SetError(lstEquipment, "Please select an item to edit")
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment)

        Dim form As New EquipmentManagementForm(equip)
        form.ShowDialog()

        RefreshItems()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lstEquipment.SelectedIndex = -1 Then
            errProvider.SetError(lstEquipment, "Please select an item to delete")
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment)

        If MessageBox.Show("Are you sure you want to delete " & equip.Name & "?", "Confirm Deletion", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            DBUtilities.DeleteEquipment(equip)
        End If

        RefreshItems()
    End Sub

    Private Sub RefreshItems()
        errProvider.Clear()

        Dim list As List(Of Equipment) = DBUtilities.GetAllEquipment()
        lstEquipment.Items.Clear()

        For Each equip As Equipment In list
            lstEquipment.Items.Add(equip)
        Next
    End Sub
End Class