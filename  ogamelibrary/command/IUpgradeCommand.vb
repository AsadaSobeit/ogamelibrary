Namespace Command

    Public Interface IUpgradeCommand

        ReadOnly Property Gid() As Gid

        ReadOnly Property Level() As Integer

        ReadOnly Property MetalCost() As Integer

        ReadOnly Property CrystalCost() As Integer

        ReadOnly Property DeuteriumCost() As Integer

        ReadOnly Property DependencyDictionary() As Dictionary(Of Gid, Integer)

        ReadOnly Property Time() As TimeSpan

        ReadOnly Property Ready() As Boolean

        Sub BeginExecute()

    End Interface
End Namespace
