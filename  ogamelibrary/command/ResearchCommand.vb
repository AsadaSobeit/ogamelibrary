Imports Microsoft.VisualBasic

Namespace Command

    Public Class ResearchCommand
        Inherits ResearchLabCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/buildings.php?session={{1}}&mode=Forschung&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private ReadOnly _GID As Integer
        Private ReadOnly _Level As Integer

        Private ReadOnly _MetalCost As Integer
        Private ReadOnly _CrystalCost As Integer
        Private ReadOnly _DeuteriumCost As Integer

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Integer, ByVal level As Integer)

            MyBase.New(serverName, planetId)

            _GID = gid

            Select Case gid
                Case Ogame.Gid.EspionageTechnology
                    _MetalCost = 100 * 2 ^ level
                    _CrystalCost = 500 * 2 ^ level
                    _DeuteriumCost = 100 * 2 ^ level
                Case Ogame.Gid.ComputerTechnology
                    _MetalCost = 0
                    _CrystalCost = 200 * 2 ^ level
                    _DeuteriumCost = 300 * 2 ^ level
                Case Ogame.Gid.WeaponsTechnology
                    _MetalCost = 400 * 2 ^ level
                    _CrystalCost = 100 * 2 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.ShieldingTechnology
                    _MetalCost = 100 * 2 ^ level
                    _CrystalCost = 300 * 2 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.ArmourTechnology
                    _MetalCost = 500 * 2 ^ level
                    _CrystalCost = 0
                    _DeuteriumCost = 0
                Case Ogame.Gid.EnergyTechnology
                    _MetalCost = 400 * 2 ^ level
                    _CrystalCost = 0
                    _DeuteriumCost = 200 * 2 ^ level
                Case Ogame.Gid.HyperspaceTechnology
                    _MetalCost = 0
                    _CrystalCost = 2000 * 2 ^ level
                    _DeuteriumCost = 1000 * 2 ^ level
                Case Ogame.Gid.CombustionDrive
                    _MetalCost = 200 * 2 ^ level
                    _CrystalCost = 0
                    _DeuteriumCost = 300 * 2 ^ level
                Case Ogame.Gid.ImpulseDrive
                    _MetalCost = 1000 * 2 ^ level
                    _CrystalCost = 2000 * 2 ^ level
                    _DeuteriumCost = 300 * 2 ^ level
                Case Ogame.Gid.HyperspaceDrive
                    _MetalCost = 5000 * 2 ^ level
                    _CrystalCost = 10000 * 2 ^ level
                    _DeuteriumCost = 3000 * 2 ^ level
                Case Ogame.Gid.LaserTechnology
                    _MetalCost = 100 * 2 ^ level
                    _CrystalCost = 50 * 2 ^ level
                    _DeuteriumCost = 0
                Case Ogame.Gid.IonTechnology
                    _MetalCost = 500 * 2 ^ level
                    _CrystalCost = 150 * 2 ^ level
                    _DeuteriumCost = 50 * 2 ^ level
                Case Ogame.Gid.PlasmaTechnology
                    Throw New NotImplementedException("plasma technology cost formula")
                Case Ogame.Gid.IntergalacticResearchNetwork
                    Throw New NotImplementedException("intergalatic research cost formula")
                Case Ogame.Gid.GravitonTechnology
                    Throw New NotImplementedException("graviton technology cost formula")
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
