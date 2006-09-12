Imports Microsoft.VisualBasic

Namespace Command

    Public Class ConstructCommand
        Inherits BuildingsCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/b_building.php?session={{1}}&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private ReadOnly _GID As Integer
        Private ReadOnly _Level As Integer

        Private ReadOnly _MetalCost As Integer
        Private ReadOnly _CrystalCost As Integer
        Private ReadOnly _DeuteriumCost As Integer

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Integer, ByVal level As Integer)

            MyBase.New(serverName, planetId)

            _GID = gid
            _Level = level

            Select Case gid
                Case Ogame.Gid.MetalMine
                    _MetalCost = 40 * 1.5 ^ level
                    _CrystalCost = 10 * 1.5 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.CrystalMine
                    _MetalCost = 30 * 1.6 ^ level
                    _CrystalCost = 15 * 1.6 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.DeuteriumSynthesizer
                    _MetalCost = 150 * 1.5 ^ level
                    _CrystalCost = 50 * 1.5 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.SolarPlant
                    _MetalCost = 50 * 1.5 ^ level
                    _CrystalCost = 20 * 1.5 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.FusionReactor
                    _MetalCost = 500 * 1.8 ^ level
                    _CrystalCost = 200 * 1.8 ^ level
                    _DeuteriumCost = 100 * 1.8 ^ level
                Case Ogame.Gid.RoboticsFactory
                    _MetalCost = 200 * 2 ^ level
                    _CrystalCost = 60 * 2 ^ level
                    _DeuteriumCost = 100 * 2 ^ level
                Case Ogame.Gid.NaniteFactory
                    Throw New NotImplementedException("nanite factory cost formula")
                Case Ogame.Gid.Shipyard
                    _MetalCost = 200 * 2 ^ level
                    _CrystalCost = 100 * 2 ^ level
                    _DeuteriumCost = 50 * 2 ^ level
                Case Ogame.Gid.MetalStorage
                    _MetalCost = 1000 * 2 ^ level
                    _CrystalCost = 0
                    _DeuteriumCost = 0
                Case Ogame.Gid.CrystalStorage
                    _MetalCost = 1000 * 2 ^ level
                    _CrystalCost = 500 * 2 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.DeuteriumTank
                    _MetalCost = 1000 * 2 ^ level
                    _CrystalCost = 1000 * 2 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.ResearchLab
                    _MetalCost = 100 * 2 ^ level
                    _CrystalCost = 200 * 2 ^ level
                    _DeuteriumCost = 100 * 2 ^ level
                Case Ogame.Gid.Terraformer
                    Throw New NotImplementedException("terror former cost formula")
                Case Ogame.Gid.AllianceDepot
                    Throw New NotImplementedException("alliance depot cost formula")
                Case Ogame.Gid.MissileSilo
                    _MetalCost = 20000 * 2 ^ level
                    _CrystalCost = 20000 * 2 ^ level
                    _DeuteriumCost = 1000 * 2 ^ level
                Case Else
                    Throw New NotImplementedException(String.Format("construction cost formula - unexpected gid: {0}", gid))
            End Select
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

        Public ReadOnly Property Gid() As Integer
            Get
                Return _GID
            End Get
        End Property

        Public ReadOnly Property Level() As Integer
            Get
                Return _Level
            End Get
        End Property

        Public ReadOnly Property MetalCost() As Integer
            Get
                Return _MetalCost
            End Get
        End Property

        Public ReadOnly Property CrystalCost() As Integer
            Get
                Return _CrystalCost
            End Get
        End Property

        Public ReadOnly Property DeuteriumCost() As Integer
            Get
                Return _DeuteriumCost
            End Get
        End Property
    End Class
End Namespace
