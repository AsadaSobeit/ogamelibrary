Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.ComponentModel

<DataObject(True)> _
Public Class Empire

    ''' <summary>
    ''' primary key in empire database table
    ''' </summary>
    ''' <remarks></remarks>
    Private _Id As Integer

    Private ReadOnly _ServerName As String
    Private ReadOnly _Username As String
    Private ReadOnly _Password As String

    ''' <summary>
    ''' 科研等级
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _ResearchLevelMap As Dictionary(Of Gid, Integer)

    Private _SessionId As String

    ''' <summary>
    ''' 行星列表
    ''' </summary>
    ''' <remarks></remarks>
    Private _PlanetList As List(Of Planet)

    '''' <summary>
    '''' 行星字典
    '''' </summary>
    '''' <remarks></remarks>
    Private _PlanetDictionary As Dictionary(Of String, Planet)

    Private _PlanetViewList As List(Of PlanetView)
    Private _PlanetViewDictionary As Dictionary(Of String, PlanetView)

    Private _PlanetCount As Integer
    Private _ServerTime As String
    Private _LocalTime As Date

    Private _Points As Integer
    Private _Rank As Integer
    Private _EmpireCount As Integer

    Private _Email As String

    ''' <summary>
    ''' 命令队列
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _CommandQueue As Queue(Of Command.CommandBase)

    Public Event Online(ByVal sender As Empire)

    Friend Sub New(ByVal serverName As String, ByVal username As String, ByVal password As String)

        _ServerName = serverName
        _Username = username
        _Password = password

        _ResearchLevelMap = New Dictionary(Of Gid, Integer)()

        _CommandQueue = New Queue(Of Command.CommandBase)()

    End Sub

    Public Sub Login()

        _SessionId = Command.CommandBase.Login(_ServerName, _Username, _Password)

        RaiseEvent Online(Me)

    End Sub

    Public Sub Download()

        Login()

        '_PlanetList = Planet.List("ogame441.de", "ebdbafca33e2")
        _PlanetList = Planet.List(_ServerName, _SessionId)
        _PlanetDictionary = New Dictionary(Of String, Planet)
        _PlanetViewList = New List(Of PlanetView)
        _PlanetViewDictionary = New Dictionary(Of String, PlanetView)

        _PlanetCount = _PlanetList.Count

        For Each p As Planet In _PlanetList
            With p
                If .LocalTime > _LocalTime Then
                    _LocalTime = .LocalTime

                    _ServerTime = .ServerTime
                    _Points = .Points
                    _Rank = .Rank
                    _EmpireCount = .EmpireCount
                End If

                AddHandler .ResearchLabUpdated, AddressOf ResearchLabUpdatedEventHandler
                AddHandler .EnqueueCommand, AddressOf EnqueueCommandEventHandler

                .BeginLoadOverviewPage()
                .BeginLoadOtherPages()

                _PlanetDictionary.Add(p.Id, p)
            End With

            Dim v As New PlanetView(p)
            _PlanetViewList.Add(v)
            _PlanetViewDictionary.Add(p.Id, v)
        Next

    End Sub

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function ListPlanetViews() As ReadOnlyCollection(Of PlanetView)

        Return New ReadOnlyCollection(Of PlanetView)(_PlanetViewList)

    End Function

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function GetPlanetView(ByVal planetId As String) As PlanetView

        Dim v As PlanetView = _PlanetViewDictionary(planetId)
        v.LocalTime = Now

        Return v

    End Function

    Public Function GetPlanet(ByVal planetId As String) As Planet

        Return _PlanetDictionary(planetId)

    End Function

    Public Function GetUrl() As String

        Return String.Format("http://{0}/game/index.php?session={1}", _ServerName, _SessionId)

    End Function

#Region "properties"

    Public ReadOnly Property ServerName() As String
        Get
            Return _ServerName
        End Get
    End Property

    Public ReadOnly Property Username() As String
        Get
            Return _Username
        End Get
    End Property

    Public ReadOnly Property Password() As String
        Get
            Return _Password
        End Get
    End Property

    Public ReadOnly Property SessionId() As String
        Get
            Return _SessionId
        End Get
    End Property

    Public ReadOnly Property PlanetCount() As Integer
        Get
            Return _PlanetCount
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

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Gid, Integer)
        Get
            Return _ResearchLevelMap
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

    Public ReadOnly Property Email() As String
        Get
            Return _Email
        End Get
    End Property

    Public ReadOnly Property CommandQueue() As Queue(Of Command.CommandBase)
        Get
            Return _CommandQueue
        End Get
    End Property

    ''' <summary>
    ''' todo: not implemented
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private ReadOnly Property AvailableCommandList() As List(Of Command.CommandBase)
        Get
            Return Nothing
        End Get
    End Property
#End Region

#Region "event handlers"

    Private Sub ResearchLabUpdatedEventHandler(ByVal levelMap As Dictionary(Of Gid, Integer))

        With levelMap
            For Each gid As Integer In .Keys
                Dim level As Integer = .Item(gid)
                _ResearchLevelMap(gid) = level
            Next
        End With
    End Sub

    Private Sub EnqueueCommandEventHandler(ByVal cmd As Command.CommandBase)

        _CommandQueue.Enqueue(cmd)

    End Sub
#End Region
End Class
