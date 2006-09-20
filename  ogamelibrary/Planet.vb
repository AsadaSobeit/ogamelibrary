'Imports System.Math
Imports System.Web
Imports System.Collections.Generic

Public Class Planet

#Region "shared"

    Public Shared Function List(ByVal serverName As String, ByVal sessionId As String) As List(Of Planet)

        Dim planetList As List(Of Planet)

        Dim currentOverviewCommand As New Command.OverviewCommand(serverName)
        currentOverviewCommand.Execute(sessionId)
        Dim currentOverviewPage As Page.OverviewPage = currentOverviewCommand.Page
        With currentOverviewPage
            If .Parse() Then
                planetList = New List(Of Planet)()

                Dim ordinal As Integer = 0
                For Each planetId As Integer In .PlanetIdList
                    Dim planet As New Planet(serverName, planetId, ordinal)

                    If planetId = .CurrentPlanetId Then
                        planet.LoadOverview(currentOverviewPage)
                        'Else
                        'If Not planet.LoadOverviewPage(sessionId) Then
                        '    planetList = Nothing
                        '    Exit For
                        'End If
                        'asynchronous loading
                        'planet.BeginLoadOverviewPage()
                    End If

                    'If Not planet.LoadOtherPages(sessionId) Then
                    '    planetList = Nothing
                    '    Exit For
                    'End If
                    'asynchronous loading
                    'planet.BeginLoadOtherPages()

                    planetList.Add(planet)
                    ordinal += 1
                Next
            Else
                planetList = Nothing
            End If
        End With

        Return planetList

    End Function
#End Region

    Public Delegate Function ValidateCommand(ByVal metalCost As Integer, ByVal crystalCost As Integer, ByVal deuteriumCost As Integer, ByVal dependencies As Dictionary(Of Gid, Integer))

    ''' <summary>
    ''' for database
    ''' </summary>
    ''' <remarks></remarks>
    Private _EmpireId As Integer

#Region "signature variables"

    Private ReadOnly _ServerName As String
    Private ReadOnly _Id As String

    ''' <summary>
    ''' colony index: 0 - home planet, 1 - 1st colony, 2 - 2nd colony, ..., 9 - 9th colony
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _OrdinalNumber As Integer

#End Region

#Region "informational command variables"

    Private ReadOnly _OverviewCommand As Command.OverviewCommand
    Private ReadOnly _BuildingsCommand As Command.BuildingsCommand
    Private ReadOnly _ResourcesCommand As Command.ResourcesCommand
    Private ReadOnly _ResearchLabCommand As Command.ResearchLabCommand
    Private ReadOnly _FleetCommand As Command.FleetCommand
    Private ReadOnly _DefenseCommand As Command.DefenseCommand

#End Region

#Region "upgrade command variables"

    'Private _ConstructCommand As Command.ConstructCommand
    'Private _DestroyCommand As Command.DestroyCommand
    'Private _ResearchCommand As Command.ResearchCommand

    'Private _CancelConstructionCommand As Command.CancelConstructionCommand
    'Private _CancelResearchCommand As Command.CancelResearchCommand

    Private _UpgradeCommandDictionary As Dictionary(Of Gid, Command.IUpgradeCommand)

#End Region

#Region "overview page variables"

    Private _LocalTime As Date

    Private _SmallImageUri As String
    Private _Type As String

    Private _Metal As Integer
    Private _Crystal As Integer
    Private _Deuterium As Integer

    Private _Name As String
    Private _ServerTime As String
    'todo: event list
    Private _LargeImageUri As String
    Private _MaxFields As Integer
    Private _LowestTemperature As Integer
    Private _HighestTemperature As Integer
    Private _GalaxyIndex As Integer
    Private _SystemIndex As Integer
    Private _PlanetIndex As Integer

    Private _Points As Integer
    Private _Rank As Integer
    Private _EmpireCount As Integer

#End Region

#Region "other page variables"

    ''' <summary>
    ''' 建筑等级
    ''' </summary>
    ''' <remarks></remarks>
    Private _BuildingLevelMap As Dictionary(Of Gid, Integer)

    ''' <summary>
    ''' 生产比率
    ''' </summary>
    ''' <remarks></remarks>
    Private _ProductionMap As Dictionary(Of Gid, Integer)

    ''' <summary>
    ''' 科研等级
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResearchLevelMap As Dictionary(Of Gid, Integer)

    ''' <summary>
    ''' 行星防御规模
    ''' </summary>
    ''' <remarks></remarks>
    Private _PlanetaryDefenseMap As Dictionary(Of Gid, Integer)

    ''' <summary>
    ''' 驻留舰队规模
    ''' </summary>
    ''' <remarks></remarks>
    Private _StationaryFleetMap As Dictionary(Of Gid, Integer)

    ''' <summary>
    ''' obsolete
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConstructionTimeLeft As TimeSpan

    ''' <summary>
    ''' obsolete
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResearchTimeLeft As TimeSpan

    Private _ConstructionSecondsLeft As Integer

    Private _ResearchSecondsLeft As Integer

#End Region

#Region "galaxy view variables"

    Private _Activity As String
    Private _HasMoon As Boolean
    Private _DebrisMetal As Integer
    Private _DebrisCrystal As Integer
    Private _UserName As String
    Private _UserStatus As String
    Private _UserAlliance As String

#End Region

#Region "events"

    ''' <summary>
    ''' update research levels
    ''' </summary>
    ''' <param name="levelMap"></param>
    ''' <remarks></remarks>
    Public Event ResearchLabUpdated(ByVal levelMap As Dictionary(Of Gid, Integer))

    Public Event EnqueueCommand(ByVal cmd As Command.CommandBase)

    Public Event Changed()

#End Region

#Region "construtors"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="serverName">server address</param>
    ''' <param name="id">planet id</param>
    ''' <param name="ordinal">colony index: 0 - home planet, 1 - 1st colony, 2 - 2nd colony, ..., 9 - 9th colony</param>
    ''' <remarks></remarks>
    Private Sub New(ByVal serverName As String, ByVal id As String, ByVal ordinal As Integer)

        _ServerName = serverName
        _Id = id
        _OrdinalNumber = ordinal

        _OverviewCommand = New Command.OverviewCommand(serverName, id)
        _BuildingsCommand = New Command.BuildingsCommand(serverName, id)
        _ResourcesCommand = New Command.ResourcesCommand(serverName, id)
        _ResearchLabCommand = New Command.ResearchLabCommand(serverName, id)
        _FleetCommand = New Command.FleetCommand(serverName, id)
        _DefenseCommand = New Command.DefenseCommand(serverName, id)

        AddHandler _OverviewCommand.Complete, AddressOf OverviewCommand_Complete
        AddHandler _BuildingsCommand.Complete, AddressOf BuildingsCommand_Complete
        AddHandler _ResourcesCommand.Complete, AddressOf ResourcesCommand_Complete
        AddHandler _ResearchLabCommand.Complete, AddressOf ResearchLabCommand_Complete
        AddHandler _FleetCommand.Complete, AddressOf FleetCommand_Complete
        AddHandler _DefenseCommand.Complete, AddressOf DefenseCommand_Complete

        _UpgradeCommandDictionary = New Dictionary(Of Gid, Command.IUpgradeCommand)

    End Sub

    ''' <summary>
    ''' used in galaxy view
    ''' </summary>
    ''' <param name="galaxyIndex"></param>
    ''' <param name="systemIndex"></param>
    ''' <param name="planetIndex"></param>
    ''' <param name="planetName"></param>
    ''' <param name="planetActivity"></param>
    ''' <param name="hasMoon"></param>
    ''' <param name="debrisMetal"></param>
    ''' <param name="debrisCrystal"></param>
    ''' <param name="username"></param>
    ''' <param name="userStatus"></param>
    ''' <param name="userAlliance"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal galaxyIndex As Integer, ByVal systemIndex As Integer, ByVal planetIndex As Integer, ByVal planetName As String, ByVal planetActivity As String, ByVal hasMoon As Boolean, ByVal debrisMetal As Integer, ByVal debrisCrystal As Integer, ByVal username As String, ByVal userStatus As String, ByVal userAlliance As String)

        _GalaxyIndex = galaxyIndex
        _SystemIndex = systemIndex
        _PlanetIndex = planetIndex
        _Name = planetName
        _Activity = planetActivity
        _HasMoon = hasMoon
        _DebrisMetal = debrisMetal
        _DebrisCrystal = debrisCrystal
        _UserName = username
        _UserStatus = userStatus
        _UserAlliance = userAlliance

    End Sub
#End Region

#Region "properites"

    Public ReadOnly Property ServerName() As String
        Get
            Return _ServerName
        End Get
    End Property

    Public ReadOnly Property Id() As String
        Get
            Return _Id
        End Get
    End Property

    Public ReadOnly Property OrdinalNumber() As Integer
        Get
            Return _OrdinalNumber
        End Get
    End Property

    Public ReadOnly Property SmallImageUri() As String
        Get
            Return _SmallImageUri
        End Get
    End Property

    Public ReadOnly Property Type() As String
        Get
            Return _Type
        End Get
    End Property

    Public ReadOnly Property Metal() As Integer
        Get
            Return _Metal
        End Get
    End Property

    Public ReadOnly Property Crystal() As Integer
        Get
            Return _Crystal
        End Get
    End Property

    Public ReadOnly Property Deuterium() As Integer
        Get
            Return _Deuterium
        End Get
    End Property

    Public ReadOnly Property MaxFields() As Integer
        Get
            Return _MaxFields
        End Get
    End Property

    Public ReadOnly Property LowestTemperature() As Integer
        Get
            Return _LowestTemperature
        End Get
    End Property

    Public ReadOnly Property HighestTemperature() As Integer
        Get
            Return _HighestTemperature
        End Get
    End Property

    Public ReadOnly Property GalaxyIndex() As Integer
        Get
            Return _GalaxyIndex
        End Get
    End Property

    Public ReadOnly Property SystemIndex() As Integer
        Get
            Return _SystemIndex
        End Get
    End Property

    Public ReadOnly Property PlanetIndex() As Integer
        Get
            Return _PlanetIndex
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property ServerTime() As String
        Get
            Return _ServerTime
        End Get
    End Property

    Public ReadOnly Property LocalTime() As Date
        Get
            Return _LocalTime
        End Get
    End Property

    Public ReadOnly Property Points() As Integer
        Get
            Return _Points
        End Get
    End Property

    Public ReadOnly Property Rank() As Integer
        Get
            Return _Rank
        End Get
    End Property

    Public ReadOnly Property EmpireCount() As Integer
        Get
            Return _EmpireCount
        End Get
    End Property

    Public ReadOnly Property BuildingLevelMap() As Dictionary(Of Gid, Integer)
        Get
            Return _BuildingLevelMap
        End Get
    End Property

    Public ReadOnly Property ProductionMap() As Dictionary(Of Gid, Integer)
        Get
            Return _ProductionMap
        End Get
    End Property

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Gid, Integer)
        Get
            Return _ResearchLevelMap
        End Get
    End Property

    Public ReadOnly Property PlanetaryDefenseMap() As Dictionary(Of Gid, Integer)
        Get
            Return _PlanetaryDefenseMap
        End Get
    End Property

    Public ReadOnly Property StationaryFleetMap() As Dictionary(Of Gid, Integer)
        Get
            Return _StationaryFleetMap
        End Get
    End Property

    Public ReadOnly Property ConstructionTimeLeft() As TimeSpan
        Get
            Return _ConstructionTimeLeft
        End Get
    End Property

    Public ReadOnly Property ResearchTimeLeft() As TimeSpan
        Get
            Return _ResearchTimeLeft
        End Get
    End Property

    Public ReadOnly Property ConstructionSecondsLeft() As Integer
        Get
            Return _ConstructionSecondsLeft
        End Get
    End Property

    Public ReadOnly Property ResearchSecondsLeft() As Integer
        Get
            Return _ResearchSecondsLeft
        End Get
    End Property

    Public ReadOnly Property LargeImageUri() As String
        Get
            Return _LargeImageUri
        End Get
    End Property

    Public ReadOnly Property UpgradeCommandMap() As Dictionary(Of Gid, Command.IUpgradeCommand)
        Get
            Return _UpgradeCommandDictionary
        End Get
    End Property

    Public ReadOnly Property Activity() As String
        Get
            Return _Activity
        End Get
    End Property

    Public ReadOnly Property HasMoon() As Boolean
        Get
            Return _HasMoon
        End Get
    End Property

    Public ReadOnly Property DebrisMetal() As Integer
        Get
            Return _DebrisMetal
        End Get
    End Property

    Public ReadOnly Property DebrisCrystal() As Integer
        Get
            Return _DebrisCrystal
        End Get
    End Property

    Public ReadOnly Property Username() As String
        Get
            Return _UserName
        End Get
    End Property

    Public ReadOnly Property UserStatus() As String
        Get
            Return _UserStatus
        End Get
    End Property

    Public ReadOnly Property UserAlliance() As String
        Get
            Return _UserAlliance
        End Get
    End Property
#End Region

    ''' <summary>
    ''' general information
    ''' </summary>
    ''' <param name="page"></param>
    ''' <remarks></remarks>
    Private Sub LoadOverview(ByVal page As Page.OverviewPage)

        _LocalTime = Now

        With page
            _SmallImageUri = .SmallImageUri
            _Type = .PlanetType

            _Metal = .Metal
            _Crystal = .Crystal
            _Deuterium = .Deuterium

            _Name = .PlanetName
            _UserName = .UserName
            _ServerTime = .ServerTime
            'todo: event list
            _LargeImageUri = .LargeImageUri
            _MaxFields = .MaxFields
            _LowestTemperature = .LowestTemperature
            _HighestTemperature = .HighestTemperature
            _GalaxyIndex = .GalaxyIndex
            _SystemIndex = .SystemIndex
            _PlanetIndex = .PlanetIndex

            _Points = .Points
            _Rank = .Rank
            _EmpireCount = .EmpireCount
        End With
    End Sub

    Private Sub AddConstructionCommand(ByVal gid As Gid, ByVal roboticsFactoryLevel As Integer, ByVal naniteFactoryLevel As Integer)

        Dim level As Integer = GetBuildingLevel(gid)
        Dim cmd As Command.ConstructCommand = New Command.ConstructCommand(_ServerName, _Id, gid, level, roboticsFactoryLevel, naniteFactoryLevel, AddressOf ValidateCommandEventHandler)
        AddHandler cmd.Complete, AddressOf BuildingsCommand_Complete
        AddHandler cmd.Executing, AddressOf ExecutingEventHandler

        _UpgradeCommandDictionary.Add(gid, cmd)

    End Sub

    Private Sub AddResearchCommand(ByVal gid As Gid, ByVal researchLabLevel As Integer)

        Dim level As Integer = GetBuildingLevel(gid)
        Dim cmd As Command.ResearchCommand = New Command.ResearchCommand(_ServerName, _Id, gid, level, researchLabLevel, AddressOf ValidateCommandEventHandler)
        AddHandler cmd.Complete, AddressOf ResearchLabCommand_Complete
        AddHandler cmd.Executing, AddressOf ExecutingEventHandler

        _UpgradeCommandDictionary.Add(gid, cmd)

    End Sub

    Private Function GetBuildingLevel(ByVal gid As Gid) As Integer

        Dim level As Integer

        If _BuildingLevelMap.ContainsKey(gid) Then
            level = _BuildingLevelMap(gid)
        Else
            level = 0
        End If

        Return level

    End Function

    Private Function GetResearchLevel(ByVal gid As Gid) As Integer

        Dim level As Integer

        If _ResearchLevelMap.ContainsKey(gid) Then
            level = _ResearchLevelMap(gid)
        Else
            level = 0
        End If

        Return level

    End Function

    Private Function ValidateLevel(ByVal levels As Dictionary(Of Gid, Integer), ByVal level As KeyValuePair(Of Gid, Integer)) As Boolean

        Return levels.ContainsKey(level.Key) AndAlso levels(level.Key) >= level.Value

    End Function

#Region "debug functions"

    Private Function LoadOverviewPage(ByVal session As String) As Boolean

        Dim success As Boolean = True

        Dim overviewPage As Page.OverviewPage
        Dim overviewCommand As Command.OverviewCommand = New Command.OverviewCommand(_ServerName, _Id)
        overviewCommand.Execute(session)
        overviewPage = overviewCommand.Page
        If overviewPage.Parse() Then
            LoadOverview(overviewPage)
        Else
            success = False
        End If

        Return success

    End Function

    Private Function LoadOtherPages(ByVal session As String) As Boolean

        Dim success = True

        'buildings
        With New Command.BuildingsCommand(_ServerName, _Id)
            .Execute(session)
            With .Page
                If .Parse() Then
                    _BuildingLevelMap = .LevelMap
                Else
                    success = False
                End If
            End With
        End With

        'todo: resources

        'research
        With New Command.ResearchLabCommand(_ServerName, _Id)
            .Execute(session)
            With .Page
                If .Parse() Then
                    _ResearchLevelMap = .LevelMap
                Else
                    success = False
                End If
            End With
        End With

        'shipyard: not used

        'todo: fleet
        With New Command.FleetCommand(_ServerName, _Id)
            .Execute(session)
            With .Page
                If .Parse() Then
                    _StationaryFleetMap = .UnitCountMap
                Else
                    success = False
                End If
            End With
        End With

        'technology: not used

        'galaxy: not used

        'defense
        With New Command.DefenseCommand(_ServerName, _Id)
            .Execute(session)
            With .Page
                If .Parse() Then
                    _PlanetaryDefenseMap = .UnitCountMap

                    'todo: shipyard queue
                Else
                    success = False
                End If
            End With
        End With

        Return success

    End Function
#End Region

#Region "command completion event handlers"

    Private Sub OverviewCommand_Complete(ByVal page As Page.OverviewPage)

        If page.Parse() Then
            LoadOverview(page)
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub BuildingsCommand_Complete(ByVal page As Page.TechLevelPage)

        If page.Parse() Then
            _BuildingLevelMap = page.LevelMap
            _ConstructionTimeLeft = page.TimeLeft
            _ConstructionSecondsLeft = page.SecondsLeft

            'begin: load construction commands

            Dim roboticsFactoryLevel As Integer = GetBuildingLevel(Gid.RoboticsFactory)
            Dim naniteFactoryLevel As Integer = GetBuildingLevel(Gid.NaniteFactory)

            AddConstructionCommand(Gid.MetalMine, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.CrystalMine, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.DeuteriumSynthesizer, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.SolarPlant, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.FusionReactor, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.RoboticsFactory, roboticsFactoryLevel, naniteFactoryLevel)
            'AddConstructionCommand(Gid.NaniteFactory, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.Shipyard, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.MetalStorage, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.CrystalStorage, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.DeuteriumTank, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.ResearchLab, roboticsFactoryLevel, naniteFactoryLevel)
            'AddConstructionCommand(Gid.Terraformer, roboticsFactoryLevel, naniteFactoryLevel)
            'AddConstructionCommand(Gid.AllianceDepot, roboticsFactoryLevel, naniteFactoryLevel)
            AddConstructionCommand(Gid.MissileSilo, roboticsFactoryLevel, naniteFactoryLevel)

            'end: load construction commands

            RaiseEvent Changed()
        End If
    End Sub

    Private Sub ResourcesCommand_Complete(ByVal page As Page.ResourcesPage)

        If page.Parse() Then
            _ProductionMap = page.PercentageMap
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub ResearchLabCommand_Complete(ByVal page As Page.TechLevelPage)

        If page.Parse() Then
            _ResearchLevelMap = page.LevelMap
            _ResearchTimeLeft = page.TimeLeft
            _ResearchSecondsLeft = page.SecondsLeft

            'begin: load research commands

            Dim researchLabLevel As Integer = GetBuildingLevel(Gid.ResearchLab)

            AddResearchCommand(Gid.EspionageTechnology, researchLabLevel)
            AddResearchCommand(Gid.ComputerTechnology, researchLabLevel)
            AddResearchCommand(Gid.WeaponsTechnology, researchLabLevel)
            AddResearchCommand(Gid.ShieldingTechnology, researchLabLevel)
            AddResearchCommand(Gid.ArmourTechnology, researchLabLevel)
            AddResearchCommand(Gid.EnergyTechnology, researchLabLevel)
            AddResearchCommand(Gid.HyperspaceTechnology, researchLabLevel)
            AddResearchCommand(Gid.CombustionDrive, researchLabLevel)
            AddResearchCommand(Gid.ImpulseDrive, researchLabLevel)
            AddResearchCommand(Gid.HyperspaceDrive, researchLabLevel)
            AddResearchCommand(Gid.LaserTechnology, researchLabLevel)
            AddResearchCommand(Gid.IonTechnology, researchLabLevel)
            'AddResearchCommand(Gid.PlasmaTechnology, researchLabLevel)
            'AddResearchCommand(Gid.IntergalacticResearchNetwork, researchLabLevel)
            'AddResearchCommand(Gid.GravitonTechnology, researchLabLevel)

            'end: load research commands

            RaiseEvent ResearchLabUpdated(_ResearchLevelMap)
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub FleetCommand_Complete(ByVal page As Page.FleetPage)

        If page.Parse() Then
            _StationaryFleetMap = page.UnitCountMap
            RaiseEvent Changed()
        End If
    End Sub

    Private Sub DefenseCommand_Complete(ByVal page As Page.DefensePage)

        If page.Parse() Then
            _PlanetaryDefenseMap = page.UnitCountMap

            'todo: shipyard queue

            RaiseEvent Changed()
        End If
    End Sub

    Private Function ValidateCommandEventHandler(ByVal metalCost As Integer, ByVal crystalCost As Integer, ByVal deuteriumCost As Integer, ByVal dependencies As Dictionary(Of Gid, Integer))

        Dim valid As Boolean = True

        If metalCost > _Metal OrElse crystalCost > _Crystal OrElse deuteriumCost > _Deuterium Then
            valid = False
        Else
            For Each level As KeyValuePair(Of Gid, Integer) In dependencies
                If Not ValidateLevel(_BuildingLevelMap, level) AndAlso Not ValidateLevel(_ResearchLevelMap, level) Then
                    valid = False
                    Exit For
                End If
            Next
        End If

        Return valid

    End Function

    Private Sub ExecutingEventHandler(ByVal cmd As Command.CommandBase)

        RaiseEvent EnqueueCommand(cmd)

    End Sub
#End Region

    Public Sub BeginLoadOverviewPage()

        RaiseEvent EnqueueCommand(_OverviewCommand)

    End Sub

    Public Sub BeginLoadOtherPages()

        RaiseEvent EnqueueCommand(_BuildingsCommand)
        RaiseEvent EnqueueCommand(_ResourcesCommand)
        RaiseEvent EnqueueCommand(_ResearchLabCommand)
        RaiseEvent EnqueueCommand(_FleetCommand)
        RaiseEvent EnqueueCommand(_DefenseCommand)
        'With _CommandQueue
        '    .Enqueue(_ConstructionCenterCommand)
        '    .Enqueue(_ResourcesCommand)
        '    .Enqueue(_ResearchLabCommand)
        '    .Enqueue(_FleetCommand)
        '    .Enqueue(_DefenseCommand)
        'End With
    End Sub

    ''' <summary>
    ''' not used
    ''' </summary>
    ''' <param name="gid"></param>
    ''' <returns></returns>
    ''' <remarks>Returns Nothing before buildings page downloaded.</remarks>
    Public Function CreateConstructCommand(ByVal gid As Gid) As Command.ConstructCommand

        Dim cmd As Command.ConstructCommand

        If _BuildingLevelMap IsNot Nothing Then
            Dim currentLevel As Integer = GetBuildingLevel(gid)
            Dim roboticsFactoryLevel As Integer = GetBuildingLevel(Ogame.Gid.RoboticsFactory)
            Dim naniteFactoryLevel As Integer = GetBuildingLevel(Ogame.Gid.NaniteFactory)

            cmd = New Command.ConstructCommand(_ServerName, _Id, gid, currentLevel, roboticsFactoryLevel, naniteFactoryLevel, AddressOf ValidateCommandEventHandler)
            AddHandler cmd.Complete, AddressOf BuildingsCommand_Complete
            AddHandler cmd.Executing, AddressOf ExecutingEventHandler
        Else
            cmd = Nothing
        End If

        Return cmd

    End Function

    ''' <summary>
    ''' not used
    ''' </summary>
    ''' <param name="gid"></param>
    ''' <returns></returns>
    ''' <remarks>Returns Nothing before buildings page downloaded, or building level is 0.</remarks>
    Public Function CreateDestroyCommand(ByVal gid As Gid) As Command.DestroyCommand

        Dim cmd As Command.DestroyCommand

        If _BuildingLevelMap IsNot Nothing Then
            Dim currentLevel As Integer = GetBuildingLevel(gid)
            If currentLevel > 0 Then
                Dim roboticsFactoryLevel As Integer = GetBuildingLevel(Ogame.Gid.RoboticsFactory)
                Dim naniteFactoryLevel As Integer = GetBuildingLevel(Ogame.Gid.NaniteFactory)

                cmd = New Command.DestroyCommand(_ServerName, _Id, gid, currentLevel, roboticsFactoryLevel, naniteFactoryLevel, AddressOf ValidateCommandEventHandler)
                AddHandler cmd.Complete, AddressOf BuildingsCommand_Complete
            Else
                cmd = Nothing
            End If
        Else
            cmd = Nothing
        End If

        Return cmd

    End Function

    ''' <summary>
    ''' not used
    ''' </summary>
    ''' <param name="gid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateCancelConstructCommand(ByVal gid As Gid) As Command.CancelConstructionCommand

        Dim cmd As Command.CancelConstructionCommand

        If _BuildingLevelMap IsNot Nothing Then
            cmd = New Command.CancelConstructionCommand(_ServerName, _Id, gid)
            AddHandler cmd.Complete, AddressOf BuildingsCommand_Complete
        Else
            cmd = Nothing
        End If

        Return cmd

    End Function
End Class
