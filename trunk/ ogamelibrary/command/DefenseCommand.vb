Imports Microsoft.VisualBasic

Namespace Command

    Public Class DefenseCommand
        Inherits CommandBase
        Implements IDefenseCommand

        Private Const URI_FORMAT As String = "http://{0}/game/buildings.php?session={1}&mode=Verteidigung"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={2}"

        Private _Page As Page.DefensePage

        Public Sub New(ByVal serverName As String)

            MyBase.New(serverName)

        End Sub

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer)

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

            _Page = New Page.DefensePage(html)

        End Sub

        Public ReadOnly Property Page() As Page.DefensePage Implements IDefenseCommand.Page
            Get
                Return _Page
            End Get
        End Property
    End Class
End Namespace
