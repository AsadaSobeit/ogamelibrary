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
                    Else
                        'If Not planet.LoadOverviewPage(sessionId) Then
                        '    planetList = Nothing
                        '    Exit For
                        'End If
                        'asynchronous loading
                        planet.BeginLoadOverviewPage()
                    End If

                    'If Not planet.LoadOtherPages(sessionId) Then
                    '    planetList = Nothing
                    '    Exit For
                    'End If
                    'asynchronous loading
                    planet.BeginLoadOtherPages()

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

#Region "informational command variables"

    Private ReadOnly _OverviewCommand As Command.OverviewCommand
    Private ReadOnly _ConstructionCenterCommand As Command.BuildingsCommand
    Private ReadOnly _ResourcesCommand As Command.ResourcesCommand
    Private ReadOnly _ResearchLabCommand As Command.ResearchLabCommand
    Private ReadOnly _FleetCommand As Command.FleetCommand
    Private ReadOnly _DefenseCommand As Command.DefenseCommand

#End Region

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
    Private _BuildingLevelMap As Dictionary(Of Integer, Integer)

    ''' <summary>
    ''' 科研等级
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResearchLevelMap As Dictionary(Of Integer, Integer)

    ''' <summary>
    ''' 行星防御规模
    ''' </summary>
    ''' <remarks></remarks>
    Private _PlanetaryDefenseMap As Dictionary(Of Integer, Integer)

    ''' <summary>
    ''' 驻留舰队规模
    ''' </summary>
    ''' <remarks></remarks>
    Private _StationaryFleetMap As Dictionary(Of Integer, Integer)

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
    Public Event ResearchLabUpdated(ByVal levelMap As Dictionary(Of Integer, Integer))

    Public Event EnqueueCommand(ByVal cmd As Command.CommandBase)

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
        _ConstructionCenterCommand = New Command.BuildingsCommand(serverName, id)
        _ResourcesCommand = New Command.ResourcesCommand(serverName, id)
        _ResearchLabCommand = New Command.ResearchLabCommand(serverName, id)
        _FleetCommand = New Command.FleetCommand(serverName, id)
        _DefenseCommand = New Command.DefenseCommand(serverName, id)

        AddHandler _OverviewCommand.Complete, AddressOf OverviewCommand_Complete
        AddHandler _ConstructionCenterCommand.Complete, AddressOf ConstructionCenterCommand_Complete
        AddHandler _ResourcesCommand.Complete, AddressOf ResourcesCommand_Complete
        AddHandler _ResearchLabCommand.Complete, AddressOf ResearchLabCommand_Complete
        AddHandler _FleetCommand.Complete, AddressOf FleetCommand_Complete
        AddHandler _DefenseCommand.Complete, AddressOf DefenseCommand_Complete

    End Sub

    ''' <summary>
    ''' 
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

    Public ReadOnly Property BuildingLevelMap() As Dictionary(Of Integer, Integer)
        Get
            Return _BuildingLevelMap
        End Get
    End Property

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Integer, Integer)
        Get
            Return _ResearchLevelMap
        End Get
    End Property

    Public ReadOnly Property PlanetaryDefenseMap() As Dictionary(Of Integer, Integer)
        Get
            Return _PlanetaryDefenseMap
        End Get
    End Property

    Public ReadOnly Property StationaryFleetMap() As Dictionary(Of Integer, Integer)
        Get
            Return _StationaryFleetMap
        End Get
    End Property

    Public ReadOnly Property LargeImageUri() As String
        Get
            Return _LargeImageUri
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

    Public Sub BeginLoadOverviewPage()

        RaiseEvent EnqueueCommand(_OverviewCommand)

    End Sub

    Public Sub BeginLoadOtherPages()

        RaiseEvent EnqueueCommand(_ConstructionCenterCommand)
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

#Region "command completion event handlers"

    Private Sub OverviewCommand_Complete(ByVal page As Page.OverviewPage)

        If page.Parse() Then
            LoadOverview(page)
        End If
    End Sub

    Private Sub ConstructionCenterCommand_Complete(ByVal page As Page.TechLevelPage)

        If page.Parse() Then
            _BuildingLevelMap = page.LevelMap
        End If
    End Sub

    Private Sub ResourcesCommand_Complete(ByVal page As Page.ResourcesPage)

        'todo: production percentage

    End Sub

    Private Sub ResearchLabCommand_Complete(ByVal page As Page.TechLevelPage)

        If page.Parse() Then
            _ResearchLevelMap = page.LevelMap
            RaiseEvent ResearchLabUpdated(_ResearchLevelMap)
        End If
    End Sub

    Private Sub FleetCommand_Complete(ByVal page As Page.FleetPage)

        If page.Parse() Then
            _StationaryFleetMap = page.UnitCountMap
        End If
    End Sub

    Private Sub DefenseCommand_Complete(ByVal page As Page.DefensePage)

        If page.Parse() Then
            _PlanetaryDefenseMap = page.UnitCountMap

            'todo: shipyard queue
        End If
    End Sub
#End Region
End Class
