Imports System.Collections.ObjectModel
Imports System.Collections.Generic
Imports System.Net
Imports System.Web

Public Class Empire

    ''' <summary>
    ''' not used
    ''' </summary>
    ''' <remarks></remarks>
    Private WithEvents LoginWebClient As WebClient

    ''' <summary>
    ''' primary key in empire database table
    ''' </summary>
    ''' <remarks></remarks>
    Private _Id As Integer

    Private _ServerName As String
    Private _Username As String
    Private _Password As String

    Private _SessionId As String

    ''' <summary>
    ''' 行星列表
    ''' </summary>
    ''' <remarks></remarks>
    Private _PlanetList As List(Of Planet)

    Private _PlanetCount As Integer
    Private _ServerTime As String
    Private _LocalTime As DateTime

    ''' <summary>
    ''' 科研等级
    ''' </summary>
    ''' <remarks></remarks>
    Private _ResearchLevelMap As Dictionary(Of Integer, Integer)

    Private _Points As Integer
    Private _Rank As Integer
    Private _EmpireCount As Integer

    Private _Email As String

    ''' <summary>
    ''' 命令队列
    ''' </summary>
    ''' <remarks></remarks>
    Private _CommandQueue As Queue(Of Command.CommandBase)

    Public Sub New(ByVal serverName As String, ByVal username As String, ByVal password As String)

        _ServerName = serverName
        _Username = username
        _Password = password

    End Sub

    Public Sub Login()

        _SessionId = Command.CommandBase.Login(_ServerName, _Username, _Password)

    End Sub

    Public Sub Download()

        Login()

        _PlanetList = Planet.List(_ServerName, _SessionId)

        _PlanetCount = _PlanetList.Count

        With _PlanetList(0)
            _ServerTime = .ServerTime
            _LocalTime = .LocalTime
        End With

        _ResearchLevelMap = New Dictionary(Of Integer, Integer)
        For Each p As Planet In _PlanetList
            With p.ResearchLevelMap
                For Each gid As Integer In .Keys
                    Dim level As Integer = .Item(gid)
                    _ResearchLevelMap(gid) = level
                Next
            End With

            _Points = p.Points
            _Rank = p.Rank
            _EmpireCount = p.EmpireCount
        Next

    End Sub

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

    Public ReadOnly Property PlanetList() As ReadOnlyCollection(Of Planet)
        Get
            Return New ReadOnlyCollection(Of Planet)(_PlanetList)
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
            Return New Dictionary(Of Integer, Integer)(_ResearchLevelMap)
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

    Public ReadOnly Property CommandQueue() As Queue(Of Command.CommandBase)
        Get
            Return _CommandQueue
        End Get
    End Property

    Private Sub LoginWebClient_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs) Handles LoginWebClient.DownloadStringCompleted

    End Sub
End Class
