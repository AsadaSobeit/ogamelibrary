Imports Microsoft.VisualBasic

Namespace Command

    Public Class DestructCommand
        Inherits CommandBase
        Implements IBuildingCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/b_building.php?session={{1}}&abreissen=1&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private _GID As Integer

        Private _Page As Page.TechLevelPage

        Public Sub New(ByVal serverName As String, ByVal gid As Integer)

            MyBase.New(serverName)

            _GID = gid

        End Sub

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Integer)

            MyBase.New(serverName, planetId)

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

        Protected Overrides Sub SetPageContent(ByVal html As String)

            _Page = New Page.TechLevelPage(html)

        End Sub

        Public ReadOnly Property Page() As Page.TechLevelPage Implements IBuildingCommand.Page
            Get
                Return _Page
            End Get
        End Property
    End Class
End Namespace
