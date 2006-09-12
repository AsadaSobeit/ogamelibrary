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
    Private ReadOnly _ResearchLevelMap As Dictionary(Of Integer, Integer)

    Private _SessionId As String

    ''' <summary>
    ''' 行星列表
    ''' </summary>
    ''' <remarks></remarks>
    Private _PlanetList As List(Of Planet)

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

        _ResearchLevelMap = New Dictionary(Of Integer, Integer)()

        _CommandQueue = New Queue(Of Command.CommandBase)()

    End Sub

    Public Sub Login()

        _SessionId = Command.CommandBase.Login(_ServerName, _Username, _Password)

        RaiseEvent Online(Me)

    End Sub

    Public Sub Download()

        Login()

        _PlanetList = Planet.List(_ServerName, _SessionId)
        '_PlanetList = Planet.List("ogame441.de", "ebdbafca33e2")

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
            End With
        Next

    End Sub

    <DataObjectMethod(DataObjectMethodType.Select, True)> _
    Public Function ListPlanets() As ReadOnlyCollection(Of Planet)

        Return New ReadOnlyCollection(Of Planet)(_PlanetList)

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

    Public ReadOnly Property ResearchLevelMap() As Dictionary(Of Integer, Integer)
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

    Private Sub ResearchLabUpdatedEventHandler(ByVal levelMap As Dictionary(Of Integer, Integer))

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
