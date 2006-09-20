Public Class Server

    Private Const MINIMUM_INTERVAL As Integer = 1000
    Private Const LONGEST_THINKING As Integer = 5000

    Private Shared ReadOnly TIME_SLICE As New TimeSpan(0, 0, 30)

    Private ReadOnly _Name As String

    Private ReadOnly _EmpireQueue As Queue(Of String)
    Private ReadOnly _EmpireDictionary As Dictionary(Of String, Empire)

    Private _Online As Boolean

    Private WithEvents _CommandTimer As Timers.Timer
    Private _CurrentEmpire As Empire
    Private _CurrentUsername As String
    Private _StartTime As Date

    Friend Sub New(ByVal name As String)

        _Name = name
        _EmpireQueue = New Queue(Of String)()
        _EmpireDictionary = New Dictionary(Of String, Empire)()

        _Online = False

        _CommandTimer = New Timers.Timer(MINIMUM_INTERVAL)
        _CommandTimer.AutoReset = False

    End Sub

    Public ReadOnly Property Online() As Boolean
        Get
            Return _Online
        End Get
    End Property

#Region "empire collection"

    Public Function ContainsEmpire(ByVal username As String) As Boolean

        Return _EmpireDictionary.ContainsKey(username)

    End Function

    Public Function AddEmpire(ByVal username As String, ByVal password As String) As Empire

        Dim e As New Empire(_Name, username, password)
        AddHandler e.Online, AddressOf ServerOnlineEventHandler

        e.Download()

        _EmpireDictionary.Add(username, e)
        _EmpireQueue.Enqueue(username)

        If Not _CommandTimer.Enabled Then
            _CommandTimer.Enabled = True
        End If

        Return e

    End Function

    Public ReadOnly Property Empire(ByVal username As String) As Empire
        Get
            Return _EmpireDictionary(username)
        End Get
    End Property
#End Region

#Region "event handlers"

    Private Sub ServerOnlineEventHandler(ByVal sender As Empire)

        RemoveHandler sender.Online, AddressOf ServerOnlineEventHandler

        _Online = True

    End Sub

    Private Sub _CommandTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles _CommandTimer.Elapsed

        '0 - free, 1 - busy (which is skipped)
        Static syncPoint As Integer = 0
        Try
            'Dim sync As Integer = Threading.Interlocked.CompareExchange(syncPoint, 1, 0)
            If Threading.Interlocked.CompareExchange(syncPoint, 1, 0) = 0 Then
                Debug.Print(Now & " [" & Threading.Thread.CurrentThread.Name & "] entering timed procedure")
                If _EmpireQueue.Count > 0 Then
                    If _CurrentEmpire Is Nothing OrElse _CurrentEmpire.CommandQueue.Count = 0 OrElse Now - _StartTime > TIME_SLICE Then
                        Do
                            Dim username As String = _EmpireQueue.Dequeue()
                            _EmpireQueue.Enqueue(username)

                            _CurrentEmpire = _EmpireDictionary(username)
                            _StartTime = Now

                            If username = _CurrentUsername Then
                                Exit Do
                            End If
                            _CurrentUsername = username

                            If _CurrentEmpire.CommandQueue.Count > 0 Then
                                Exit Do
                            End If
                        Loop
                    End If

                    'todo: execute commands
                    With _CurrentEmpire
                        If .CommandQueue.Count > 0 Then
                            Dim cmd As Command.CommandBase = .CommandQueue.Peek()
                            If cmd.State = Command.CommandBase.CommandState.Failed Then
                                'retry
                                cmd.Execute(.SessionId)
                                If cmd.State = Command.CommandBase.CommandState.Done Then
                                    .CommandQueue.Dequeue()
                                ElseIf TypeOf cmd.LastException Is InvalidSessionException Then
                                    .Login()
                                    'retry
                                Else
                                    'skip
                                    While .CommandQueue.Count > 0
                                        cmd = .CommandQueue.Dequeue()
                                        cmd.Skip()
                                    End While
                                End If
                            Else
                                cmd.Execute(.SessionId)
                                If cmd.State = Command.CommandBase.CommandState.Done Then
                                    .CommandQueue.Dequeue()
                                ElseIf TypeOf cmd.LastException Is InvalidSessionException Then
                                    .Login()
                                    'retry
                                ElseIf TypeOf cmd.LastException Is InvalidCommandException Then
                                    'retry
                                    Debug.Print(cmd.ToString())
                                Else
                                    'skip
                                    While .CommandQueue.Count > 0
                                        cmd = .CommandQueue.Dequeue()
                                        cmd.Skip()
                                    End While
                                End If
                            End If
                        End If
                    End With

                    _CommandTimer.Interval = RandomInterval()
                    _CommandTimer.Enabled = True
                    'Else
                    '    _CommandTimer.Enabled = False
                End If
                Debug.Print(Now & " [" & Threading.Thread.CurrentThread.Name & "] exiting timed procedure")
            Else
                Debug.Print(Now & " [" & Threading.Thread.CurrentThread.Name & "] skipping timed procedure")
                _CommandTimer.Interval = MINIMUM_INTERVAL
                _CommandTimer.Enabled = True
            End If
        Finally
            syncPoint = 0
        End Try
    End Sub

    Private Function RandomInterval() As Double

        'Randomize()

        'Return MINIMUM_INTERVAL + LONGEST_THINKING * Rnd()
        Return MINIMUM_INTERVAL + LONGEST_THINKING * New Random().NextDouble()

    End Function
#End Region
End Class
