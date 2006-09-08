Public Class ShipyardQueue

    Public ReadOnly StartTime As Date

    Private ReadOnly _MemberList As List(Of Unit)

    Public Sub New(ByVal startTime As Date, ByVal memberList As List(Of Unit))

        Me.StartTime = startTime
        _MemberList = memberList

    End Sub

    Public ReadOnly Property UnitList() As List(Of Unit)
        Get
            Return _MemberList.GetRange(0, _MemberList.Count)
        End Get
    End Property

    Public Class Unit

        Public ReadOnly Name As String
        Public ReadOnly SecondsPerUnit As Integer
        Public ReadOnly UnitCount As Integer

        Public Sub New(ByVal name As String, ByVal secondsPerUnit As Integer, ByVal unitCount As Integer)

            Me.Name = name
            Me.SecondsPerUnit = secondsPerUnit
            Me.UnitCount = unitCount

        End Sub
    End Class
End Class
