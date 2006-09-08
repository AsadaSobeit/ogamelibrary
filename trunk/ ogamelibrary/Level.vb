Imports Microsoft.VisualBasic

''' <summary>
''' todo: not used
''' </summary>
''' <remarks></remarks>
Public Class Level

    Private _GID As Integer
    Private _Value As Integer

    Public Sub New(ByVal gid As Integer, ByVal value As Integer)

        _GID = gid
        _Value = value

    End Sub

    Public ReadOnly Property GID() As Integer
        Get
            Return _GID
        End Get
    End Property

    Public ReadOnly Property Value() As Integer
        Get
            Return _Value
        End Get
    End Property
End Class
