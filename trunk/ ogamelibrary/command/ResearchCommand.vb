Imports Microsoft.VisualBasic

Namespace Command

    Public Class ResearchCommand
        Inherits ResearchLabCommand
        Implements IUpgradeCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/buildings.php?session={{1}}&mode=Forschung&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private ReadOnly _GID As Gid
        Private ReadOnly _Level As Integer
        Private ReadOnly _ResearchLabLevel As Integer

        Private ReadOnly _Dependencies As Dictionary(Of Ogame.Gid, Integer)

        Private ReadOnly _MetalCost As Integer
        Private ReadOnly _CrystalCost As Integer
        Private ReadOnly _DeuteriumCost As Integer

        Private ReadOnly _Time As TimeSpan

        Private ReadOnly _Validator As Planet.ValidateCommand

        Public Event Executing(ByVal cmd As CommandBase)

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Gid, ByVal currentLevel As Integer, ByVal researchLabLevel As Integer, ByVal validator As Planet.ValidateCommand)

            MyBase.New(serverName, planetId)

            _GID = gid
            _Level = currentLevel
            _ResearchLabLevel = researchLabLevel

            _Dependencies = New Dictionary(Of Ogame.Gid, Integer)

            Select Case gid
                Case Ogame.Gid.EspionageTechnology
                    _MetalCost = 200 * 2 ^ currentLevel
                    _CrystalCost = 1000 * 2 ^ currentLevel
                    _DeuteriumCost = 100 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 3)
                Case Ogame.Gid.ComputerTechnology
                    _MetalCost = 0
                    _CrystalCost = 400 * 2 ^ currentLevel
                    _DeuteriumCost = 600 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 1)
                Case Ogame.Gid.WeaponsTechnology
                    _MetalCost = 800 * 2 ^ currentLevel
                    _CrystalCost = 200 * 2 ^ currentLevel
                    _DeuteriumCost = 0
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 4)
                Case Ogame.Gid.ShieldingTechnology
                    _MetalCost = 200 * 2 ^ currentLevel
                    _CrystalCost = 600 * 2 ^ currentLevel
                    _DeuteriumCost = 0
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 3)
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 6)
                Case Ogame.Gid.ArmourTechnology
                    _MetalCost = 1000 * 2 ^ currentLevel
                    _CrystalCost = 0
                    _DeuteriumCost = 0
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 2)
                Case Ogame.Gid.EnergyTechnology
                    _MetalCost = 0
                    _CrystalCost = 800 * 2 ^ currentLevel
                    _DeuteriumCost = 400 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 1)
                Case Ogame.Gid.HyperspaceTechnology
                    _MetalCost = 0
                    _CrystalCost = 4000 * 2 ^ currentLevel
                    _DeuteriumCost = 2000 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 5)
                    _Dependencies.Add(Ogame.Gid.ShieldingTechnology, 5)
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 7)
                Case Ogame.Gid.CombustionDrive
                    _MetalCost = 400 * 2 ^ currentLevel
                    _CrystalCost = 0
                    _DeuteriumCost = 600 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 1)
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 1)
                Case Ogame.Gid.ImpulseDrive
                    _MetalCost = 2000 * 2 ^ currentLevel
                    _CrystalCost = 4000 * 2 ^ currentLevel
                    _DeuteriumCost = 600 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 1)
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 2)
                Case Ogame.Gid.HyperspaceDrive
                    _MetalCost = 10000 * 2 ^ currentLevel
                    _CrystalCost = 20000 * 2 ^ currentLevel
                    _DeuteriumCost = 6000 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.HyperspaceTechnology, 3)
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 7)
                Case Ogame.Gid.LaserTechnology
                    _MetalCost = 200 * 2 ^ currentLevel
                    _CrystalCost = 100 * 2 ^ currentLevel
                    _DeuteriumCost = 0
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 1)
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 2)
                Case Ogame.Gid.IonTechnology
                    _MetalCost = 1000 * 2 ^ currentLevel
                    _CrystalCost = 300 * 2 ^ currentLevel
                    _DeuteriumCost = 100 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 4)
                    _Dependencies.Add(Ogame.Gid.LaserTechnology, 5)
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 4)
                Case Ogame.Gid.PlasmaTechnology
                    Throw New NotImplementedException("plasma technology cost formula")
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 4)
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 8)
                    _Dependencies.Add(Ogame.Gid.LaserTechnology, 10)
                    _Dependencies.Add(Ogame.Gid.IonTechnology, 5)
                Case Ogame.Gid.IntergalacticResearchNetwork
                    Throw New NotImplementedException("intergalatic research cost formula")
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 10)
                    _Dependencies.Add(Ogame.Gid.ComputerTechnology, 8)
                    _Dependencies.Add(Ogame.Gid.HyperspaceTechnology, 8)
                Case Ogame.Gid.GravitonTechnology
                    Throw New NotImplementedException("graviton technology cost formula")
                    _Dependencies.Add(Ogame.Gid.ResearchLab, 12)
                Case Else
                    Throw New NotImplementedException(String.Format("construction cost formula - unexpected gid: {0}", gid))
            End Select

            Dim hour As Double = (_MetalCost + _CrystalCost) / 1000 / (researchLabLevel + 1)
            _Time = New TimeSpan(0, 0, 3600 * hour)

            _Validator = validator

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

        Public ReadOnly Property Gid() As Gid Implements IUpgradeCommand.Gid
            Get
                Return _GID
            End Get
        End Property

        Public ReadOnly Property Level() As Integer Implements IUpgradeCommand.Level
            Get
                Return _Level
            End Get
        End Property

        Public ReadOnly Property DependencyDictionary() As Dictionary(Of Ogame.Gid, Integer) Implements IUpgradeCommand.DependencyDictionary
            Get
                Return _Dependencies
            End Get
        End Property

        Public ReadOnly Property MetalCost() As Integer Implements IUpgradeCommand.MetalCost
            Get
                Return _MetalCost
            End Get
        End Property

        Public ReadOnly Property CrystalCost() As Integer Implements IUpgradeCommand.CrystalCost
            Get
                Return _CrystalCost
            End Get
        End Property

        Public ReadOnly Property DeuteriumCost() As Integer Implements IUpgradeCommand.DeuteriumCost
            Get
                Return _DeuteriumCost
            End Get
        End Property

        Public ReadOnly Property Time() As TimeSpan Implements IUpgradeCommand.Time
            Get
                Return _Time
            End Get
        End Property

        Public ReadOnly Property Ready() As Boolean Implements IUpgradeCommand.Ready
            Get
                Return _Validator.Invoke(_MetalCost, _CrystalCost, _DeuteriumCost, _Dependencies)
            End Get
        End Property

        Public Sub BeginExecute() Implements IUpgradeCommand.BeginExecute

            RaiseEvent Executing(Me)

        End Sub

        Public ReadOnly Property ResearchLabLevel() As Integer
            Get
                Return _ResearchLabLevel
            End Get
        End Property
    End Class
End Namespace
