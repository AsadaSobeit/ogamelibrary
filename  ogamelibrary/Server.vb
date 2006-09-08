Public Class Server

#Region "shared"

    Private Shared ReadOnly MAX_TIME_SLICE As New TimeSpan(0, 0, 30)
    Private Shared ReadOnly SERVER_POOL As New Dictionary(Of Integer, Server)

    Public Shared Sub IssueCommand(ByVal handler As Command.Executed, ByVal file As String, ByVal parameters As Dictionary(Of String, String), ByVal serverId As String, ByVal username As String, ByVal password As String)

        Dim server As Server = Nothing
        If SERVER_POOL.TryGetValue(serverId, server) = False Then
            server = New Server(serverId)
            SERVER_POOL.Add(serverId, server)
        End If

        Dim sb As New System.Text.StringBuilder("http://ogame" & serverId & ".de/" & file & ".php?")
        For Each key As String In parameters.Keys
            Dim value As String = parameters(key)
            sb.Append("&" & key & "=" & value)
        Next
        Dim cmd As String = sb.ToString()

        server.EmpireIssueCommand(handler, cmd, username, password)

    End Sub
#End Region

#Region "instance"

    Private id As Integer

    Private recentUsername As String
    Private recentSession As String = Nothing
    Private recentEmpire As Empire = Nothing

    Private WithEvents tmr As New Timers.Timer(5000.0#)
    Private startTime As DateTime = Nothing

    Private ReadOnly empireQueue As New Queue(Of Empire)
    Private ReadOnly empireDictionary As New Dictionary(Of String, Empire)

    Private Sub EmpireIssueCommand(ByVal handler As Command.Executed, ByVal command As String, ByVal username As String, ByVal password As String)

        Dim empire As Empire = Nothing
        If empireDictionary.TryGetValue(username, empire) = False Then
            empire = New Empire(username, password)
            empireDictionary.Add(username, empire)
            empireQueue.Enqueue(empire)
        End If

        empire.IssueCommand(handler, command)

    End Sub

    'Public Shared Function GetInstance(ByVal id As Integer) As Server

    '    Dim svr As Server = Nothing
    '    If SERVER_DICTIONARY.TryGetValue(id, svr) = False Then
    '        svr = New Server(id)
    '        SERVER_DICTIONARY.Add(id, svr)
    '    End If

    '    Return svr

    'End Function

    Private Sub New(ByVal id As Integer)

        Me.id = id

    End Sub

    Private Sub tmr_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmr.Elapsed

        'todo
        If recentEmpire Is Nothing Then

        End If
        If empireQueue.Count > 0 Then
            Dim empire As Empire = empireQueue.Dequeue()

            empireQueue.Enqueue(Empire)
        Else
            tmr.Stop()
        End If


        'todo: execute

        'Dim html As String = ""
        'handler.Invoke(html)

    End Sub

    Public Class Command

        ''' <summary>
        ''' notify the command source
        ''' </summary>
        ''' <param name="html">response page</param>
        ''' <remarks></remarks>
        Public Delegate Sub Executed(ByVal html As String)

        Private handler As Executed
        Private command As String

        Public Sub New(ByVal handler As Executed, ByVal command As String)

            Me.handler = handler
            Me.command = command

        End Sub
    End Class

    Public Class Empire

        Private _Username As String
        Private _Password As String

        Private _CommandQueue As New Queue(Of Command)

        Public Sub New(ByVal username As String, ByVal password As String)

            _Username = username
            _Password = password

        End Sub

        Public Sub IssueCommand(ByVal handler As Command.Executed, ByVal command As String)

            'todo enqueue
            Dim cmd As New Command(handler, command)
            _CommandQueue.Enqueue(cmd)

        End Sub

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
    End Class
#End Region
End Class
