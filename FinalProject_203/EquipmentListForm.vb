Public Class EquipmentListForm
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        EquipmentManagementForm.ShowDialog()
    End Sub

    Private Sub EquipmentListForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim list As List(Of Equipment) = DBUtilities.GetAllEquipment()

        For Each equip As Equipment In list
            lstEquipment.Items.Add(equip)
        Next
    End Sub
End Class