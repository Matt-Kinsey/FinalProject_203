Option Explicit On
Option Strict On

Public Class EquipmentManagementForm

    Private equip As Equipment
    Private adding As Boolean = False

    ''' <summary>
    ''' Constructor for form
    ''' </summary>
    ''' <param name="pEquip">The equipment object to edit, OR Nothing for adding equipment</param>
    Public Sub New(ByRef pEquip As Equipment)
        InitializeComponent()

        If pEquip Is Nothing Then
            adding = True
            Return 'Rest is "Else"
        End If

        equip = pEquip

        txtEquipmentName.Text = equip.Name
        txtDescription.Text = equip.Description

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If adding Then
            equip = New Equipment(txtEquipmentName.Text, txtDescription.Text) 'Create the object

            DBUtilities.AddEquipment(equip) 'Add to db
        Else 'We are editing
            equip.Name = txtEquipmentName.Text
            equip.Description = txtDescription.Text

            DBUtilities.EditEquipment(equip)
        End If


        Me.Close() 'Make sure we close the form
    End Sub
End Class