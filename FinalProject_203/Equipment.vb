Option Explicit On
Option Strict On

Public Class Equipment

    Public Property Name As String 'Name of the equipment
    Public Property Description As String 'Description of the equipment
    Public ReadOnly Property ID As Integer 'ID from the database

    ''' <summary>
    ''' Basic constructor for all (not readonly) fields, used primarly for AddEquipment Equipment object creation
    ''' </summary>
    ''' <param name="pName">Name of the equipment</param>
    ''' <param name="pDescription">Description of the equipment</param>
    Public Sub New(ByVal pName As String, ByVal pDescription As String)
        Name = pName
        Description = pDescription
    End Sub

    ''' <summary>
    ''' Basic constructor for all fields, used for making Equipment objects from the Database
    ''' </summary>
    ''' <param name="pName">Name of the equipment</param>
    ''' <param name="pDescription">Description of the equipment</param>
    ''' <param name="pID">ID of the equipment from the database</param>
    Public Sub New(ByVal pName As String, ByVal pDescription As String, ByVal pID As Integer)
        Name = pName
        Description = pDescription
        ID = pID
    End Sub

    ''' <summary>
    ''' Cloning constructor
    ''' </summary>
    ''' <param name="equip">The equipment object to clone</param>
    Public Sub New(ByRef equip As Equipment)

        Name = equip.Name
        Description = equip.Description
    End Sub

    Public Overrides Function ToString() As String
        Return Name
    End Function
End Class
