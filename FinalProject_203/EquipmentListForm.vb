Option Explicit On
Option Strict On

Public Class EquipmentListForm



    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Open the management form to take care of adding
        Dim form As New EquipmentManagementForm(Nothing)
        form.ShowDialog()
        'Refresh the list after so the new equipment shows up
        RefreshItems()
    End Sub


    Private Sub EquipmentListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshItems() 'Make sure the list is correct
    End Sub

    Private Sub EquipmentListForm_Closing(sender As Object, e As EventArgs) Handles MyBase.Closing
        SearchForm.PopulateEquipment() 'Make sure we refresh the list of equipment of on the main search page to reflect any changes made
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lstEquipment.SelectedIndex = -1 Then 'Make sure a piece of equipment is selected
            errProvider.SetError(lstEquipment, "Please select an item to edit")
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment) 'Get the Equipment object from the list box
        'Open the management form to take care of editing
        Dim form As New EquipmentManagementForm(equip)
        form.ShowDialog()
        'Refresh the list after so the new equipment shows up
        RefreshItems()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If lstEquipment.SelectedIndex = -1 Then 'Make sure a piece of equipment is selected
            errProvider.SetError(lstEquipment, "Please select an item to delete")
            Return
        End If

        Dim equip As Equipment = CType(lstEquipment.SelectedItem, Equipment) 'Get the Equipment object from the list box

        If MessageBox.Show("Are you sure you want to delete " & equip.Name & "?", "Confirm Deletion", MessageBoxButtons.YesNo) = DialogResult.Yes Then 'Confirm the deletion
            DBUtilities.DeleteEquipment(equip) 'Delete the equipment
        End If
        'Refresh the list after so the new equipment shows up
        RefreshItems()
    End Sub

    ''' <summary>
    ''' Refreshes the list of equipment
    ''' </summary>
    Private Sub RefreshItems()
        errProvider.Clear()

        Dim list As List(Of Equipment) = DBUtilities.GetAllEquipment()
        lstEquipment.Items.Clear()

        For Each equip As Equipment In list
            lstEquipment.Items.Add(equip)
        Next
        errProvider.Clear()
    End Sub
End Class