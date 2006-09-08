Imports Microsoft.VisualBasic

Public Class Dependency

    Private _GID As Integer
    Private _GidDepended As Integer
    Private _LevelDepended As Integer

    Public Sub New(ByVal gid As Integer, ByVal gidDepended As Integer, ByVal levelDepended As Integer)

        _GID = gid
        _GidDepended = gidDepended
        _LevelDepended = levelDepended

    End Sub

    Public ReadOnly Property GID() As Integer
        Get
            Return _GID
        End Get
    End Property

    Public ReadOnly Property GidDepended() As Integer
        Get
            Return _GidDepended
        End Get
    End Property

    Public ReadOnly Property LevelDepended() As Integer
        Get
            Return _LevelDepended
        End Get
    End Property
End Class
