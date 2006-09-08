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
                        'todo: asynchronous loading
                        If Not planet.LoadOverviewPage(sessionId) Then
                            planetList = Nothing
                            Exit For
                        End If
                    End If

                    'todo: asynchronous loading
                    If Not planet.LoadOtherPages(sessionId) Then
                        planetList = Nothing
                        Exit For
                    End If

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

    Private _ServerName As String
    Private _Id As String

    ''' <summary>
    ''' indicates: 0 - home planet, 1 - 1st colony, 2 - 2nd colony, ..., 9 - 9th colony
    ''' </summary>
    ''' <remarks></remarks>
    Private _OrdinalNumber As Integer

    Private _Metal As Integer
    Private _Crystal As Integer
    Private _Deuterium As Integer

    Private _MaxFields As Integer
    Private _LowestTemperature As Integer
    Private _HighestTemperature As Integer
    Private _GalaxyIndex As Integer
    Private _SystemIndex As Integer
    Private _PlanetIndex As Integer
    Private _Name As String
    Private _ServerTime As String
    Private _LocalTime As DateTime

    Private _Points As Integer
    Private _Rank As Integer
    Private _EmpireCount As Integer

    Private _OverviewCommandList As List(Of Command.CommandBase)
    Private _ConstructionCommandList As List(Of Command.CommandBase)
    Private _ResourcesCommandList As List(Of Command.CommandBase)
    Private _ResearchCommandList As List(Of Command.CommandBase)
    Private _ShipyardCommandList As List(Of Command.CommandBase)
    Private _FleetCommandList As List(Of Command.CommandBase)
    Private _TechnologyCommandList As List(Of Command.CommandBase)
    'Private _GalaxyCommandList As List(Of Command.Command)
    Private _DefenseCommandList As List(Of Command.CommandBase)

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

    'galaxy view
    Private _PlanetType As Integer
    Private _PlanetActivity As String
    Private _HasMoon As Boolean
    Private _DebrisMetal As Integer
    Private _DebrisCrystal As Integer
    Private _UserName As String
    Private _UserStatus As String
    Private _UserAlliance As String

    Private Sub New(ByVal serverName As String, ByVal id As String, ByVal ordinal As Integer)

        _ServerName = serverName
        _Id = id
        _OrdinalNumber = ordinal

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
        _PlanetActivity = planetActivity
        _HasMoon = hasMoon
        _DebrisMetal = debrisMetal
        _DebrisCrystal = debrisCrystal
        _UserName = username
        _UserStatus = userStatus
        _UserAlliance = userAlliance

    End Sub

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
            Return New Dictionary(Of Integer, Integer)(_BuildingLevelMap)
        End Get
    End Property

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Integer, Integer)
        Get
            Return New Dictionary(Of Integer, Integer)(_ResearchLevelMap)
        End Get
    End Property

    Public ReadOnly Property PlanetaryDefenseMap() As Dictionary(Of Integer, Integer)
        Get
            Return New Dictionary(Of Integer, Integer)(_PlanetaryDefenseMap)
        End Get
    End Property

    Public ReadOnly Property StationaryFleetMap() As Dictionary(Of Integer, Integer)
        Get
            Return New Dictionary(Of Integer, Integer)(_StationaryFleetMap)
        End Get
    End Property

    Public ReadOnly Property Type() As Integer
        Get
            Return _PlanetType
        End Get
    End Property

    Public ReadOnly Property Activity() As String
        Get
            Return _PlanetActivity
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

    Private Sub LoadOverview(ByVal page As Page.OverviewPage)

        _LocalTime = Now

        With page
            _Metal = .Metal
            _Crystal = .Crystal
            _Deuterium = .Deuterium

            _Name = .PlanetName
            _ServerTime = .ServerTime
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
        With New Command.ConstructionCenterCommand(_ServerName, _Id)
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
End Class
