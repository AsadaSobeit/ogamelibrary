Imports Microsoft.VisualBasic

Namespace Command

    Public Class DestroyCommand
        Inherits ConstructCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/b_building.php?session={{1}}&abreissen=1&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private _GID As Integer

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Integer, ByVal level As Integer)

            MyBase.New(serverName, planetId, gid, level - 1)

            _GID = gid

        End Sub

        Protected Overrides ReadOnly Property PlanetUriFormat() As String
            Get
                Return String.Format(PLANET_URI_FORMAT, _GID)
            End Get
        End Property

        Protected Overrides ReadOnly Property UriFormat() As String
            Get
                Return String.Format(URI_FORMAT, _GID)
            End Get
        End Property
    End Class
End Namespace
