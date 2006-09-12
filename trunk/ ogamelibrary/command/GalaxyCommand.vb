Imports Microsoft.VisualBasic

Namespace Command

    Public Class GalaxyCommand
        Inherits CommandBase

        Private Const URI_FORMAT As String = "http://{{0}}/game/galaxy.php?session={{1}}&galaxy={0}&system={1}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private _GalaxyIndex As Integer
        Private _SystemIndex As Integer

        Private _Page As Page.GalaxyPage

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal galaxyIndex As Integer, ByVal systemIndex As Integer)

            MyBase.New(serverName, planetId)

            _GalaxyIndex = galaxyIndex
            _SystemIndex = systemIndex

        End Sub

        Protected Overrides ReadOnly Property PlanetUriFormat() As String
            Get
                Return String.Format(PLANET_URI_FORMAT, _GalaxyIndex, _SystemIndex)
            End Get
        End Property

        Protected Overrides ReadOnly Property UriFormat() As String
            Get
                Return String.Format(URI_FORMAT, _GalaxyIndex, _SystemIndex)
            End Get
        End Property

        Public ReadOnly Property NextGalaxy() As GalaxyCommand
            Get
                Return New GalaxyCommand(ServerName, PlanetId, _GalaxyIndex + 1, _SystemIndex)
            End Get
        End Property

        Public ReadOnly Property PrevGalaxy() As GalaxyCommand
            Get
                Return New GalaxyCommand(ServerName, PlanetId, _GalaxyIndex - 1, _SystemIndex)
            End Get
        End Property

        Public ReadOnly Property NextSystem() As GalaxyCommand
            Get
                Return New GalaxyCommand(ServerName, PlanetId, _GalaxyIndex, _SystemIndex + 1)
            End Get
        End Property

        Public ReadOnly Property PrevSystem() As GalaxyCommand
            Get
                Return New GalaxyCommand(ServerName, PlanetId, _GalaxyIndex, _SystemIndex - 1)
            End Get
        End Property

        Protected Overrides Sub SetPageContent(ByVal html As String)

            _Page = New Page.GalaxyPage(html)

        End Sub

        Public ReadOnly Property Page() As Page.GalaxyPage
            Get
                Return _Page
            End Get
        End Property
    End Class
End Namespace
