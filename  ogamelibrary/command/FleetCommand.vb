Imports Microsoft.VisualBasic

Namespace Command

    Public Class FleetCommand
        Inherits CommandBase

        Private Const URI_FORMAT As String = "http://{0}/game/flotten1.php?session={1}&mode=Flotte"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={2}"

        Private _Page As Page.FleetPage

        Public Event Complete(ByVal page As Page.FleetPage)

        Public Sub New(ByVal serverName As String, ByVal planetId As String)

            MyBase.New(serverName, planetId)

        End Sub

        Protected Overrides ReadOnly Property PlanetUriFormat() As String
            Get
                Return PLANET_URI_FORMAT
            End Get
        End Property

        Protected Overrides ReadOnly Property UriFormat() As String
            Get
                Return URI_FORMAT
            End Get
        End Property

        Protected Overrides Sub SetPageContent(ByVal html As String)

            _Page = New Page.FleetPage(html)

            RaiseEvent Complete(_Page)

        End Sub

        Public ReadOnly Property Page() As Page.FleetPage
            Get
                Return _Page
            End Get
        End Property
    End Class
End Namespace
