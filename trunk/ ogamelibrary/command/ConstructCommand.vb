Imports System.Math
Imports Microsoft.VisualBasic

Namespace Command

    Public Class ConstructCommand
        Inherits BuildingsCommand
        Implements IUpgradeCommand

        Private Const URI_FORMAT As String = "http://{{0}}/game/b_building.php?session={{1}}&bau={0}"
        Private Const PLANET_URI_FORMAT As String = URI_FORMAT & "&cp={{2}}"

        Private ReadOnly _GID As Gid
        Private ReadOnly _Level As Integer
        Private ReadOnly _RoboticsFactoryLevel As Integer
        Private ReadOnly _NaniteFactoryLevel As Integer

        Private ReadOnly _Dependencies As Dictionary(Of Ogame.Gid, Integer)

        Private ReadOnly _MetalCost As Integer
        Private ReadOnly _CrystalCost As Integer
        Private ReadOnly _DeuteriumCost As Integer

        Private ReadOnly _Time As TimeSpan

        Private ReadOnly _Validator As Planet.ValidateCommand

        Public Event Executing(ByVal cmd As CommandBase)

        Public Sub New(ByVal serverName As String, ByVal planetId As Integer, ByVal gid As Gid, ByVal currentLevel As Integer, ByVal roboticsFactoryLevel As Integer, ByVal naniteFactoryLevel As Integer, ByVal validator As Planet.ValidateCommand)

            MyBase.New(serverName, planetId)

            _GID = gid
            _Level = currentLevel
            _RoboticsFactoryLevel = roboticsFactoryLevel
            _NaniteFactoryLevel = naniteFactoryLevel

            _Dependencies = New Dictionary(Of Ogame.Gid, Integer)

            Select Case gid
                Case Ogame.Gid.MetalMine
                    _MetalCost = 60 * 1.5 ^ currentLevel
                    _CrystalCost = 15 * 1.5 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.CrystalMine
                    _MetalCost = 48 * 1.6 ^ currentLevel
                    _CrystalCost = 24 * 1.6 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.DeuteriumSynthesizer
                    _MetalCost = 225 * 1.5 ^ currentLevel
                    _CrystalCost = 75 * 1.5 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.SolarPlant
                    _MetalCost = 75 * 1.5 ^ currentLevel
                    _CrystalCost = 30 * 1.5 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.FusionReactor
                    _MetalCost = 900 * 1.8 ^ currentLevel
                    _CrystalCost = 360 * 1.8 ^ currentLevel
                    _DeuteriumCost = 180 * 1.8 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.DeuteriumSynthesizer, 5)
                Case Ogame.Gid.RoboticsFactory
                    _MetalCost = 400 * 2 ^ currentLevel
                    _CrystalCost = 120 * 2 ^ currentLevel
                    _DeuteriumCost = 200 * 2 ^ currentLevel
                Case Ogame.Gid.NaniteFactory
                    Throw New NotImplementedException("nanite factory cost formula")
                    _Dependencies.Add(Ogame.Gid.RoboticsFactory, 10)
                    _Dependencies.Add(Ogame.Gid.ComputerTechnology, 5)
                Case Ogame.Gid.Shipyard
                    _MetalCost = 400 * 2 ^ currentLevel
                    _CrystalCost = 200 * 2 ^ currentLevel
                    _DeuteriumCost = 100 * 2 ^ currentLevel
                    _Dependencies.Add(Ogame.Gid.RoboticsFactory, 2)
                Case Ogame.Gid.MetalStorage
                    _MetalCost = 2000 * 2 ^ currentLevel
                    _CrystalCost = 0
                    _DeuteriumCost = 0
                Case Ogame.Gid.CrystalStorage
                    _MetalCost = 2000 * 2 ^ currentLevel
                    _CrystalCost = 1000 * 2 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.DeuteriumTank
                    _MetalCost = 2000 * 2 ^ currentLevel
                    _CrystalCost = 2000 * 2 ^ currentLevel
                    _DeuteriumCost = 0
                Case Ogame.Gid.ResearchLab
                    _MetalCost = 200 * 2 ^ currentLevel
                    _CrystalCost = 400 * 2 ^ currentLevel
                    _DeuteriumCost = 200 * 2 ^ currentLevel
                Case Ogame.Gid.Terraformer
                    Throw New NotImplementedException("terror former cost formula")
                    _Dependencies.Add(Ogame.Gid.NaniteFactory, 1)
                    _Dependencies.Add(Ogame.Gid.EnergyTechnology, 12)
                Case Ogame.Gid.AllianceDepot
                    Throw New NotImplementedException("alliance depot cost formula")
                Case Ogame.Gid.MissileSilo
                    _MetalCost = 20000 * 2 ^ currentLevel
                    _CrystalCost = 20000 * 2 ^ currentLevel
                    _DeuteriumCost = 1000 * 2 ^ currentLevel
                Case Else
                    Throw New NotImplementedException(String.Format("construction cost formula - unexpected gid: {0}", gid))
            End Select

            Dim hour As Double = (_MetalCost + _CrystalCost) / 2500 / (roboticsFactoryLevel + 1) * 0.5 ^ naniteFactoryLevel
            'Dim minute As Double = (hour - Floor(hour)) * 60
            'Dim second As Double = (minute - Floor(minute)) * 60
            '_Time = New TimeSpan(Floor(hour), Floor(minute), Floor(second))
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

        Public ReadOnly Property RoboticsFactoryLevel() As Integer
            Get
                Return _RoboticsFactoryLevel
            End Get
        End Property

        Public ReadOnly Property NaniteFactoryLevel() As Integer
            Get
                Return _NaniteFactoryLevel
            End Get
        End Property
    End Class
End Namespace
