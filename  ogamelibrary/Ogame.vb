Public Class Ogame

    Private ReadOnly MAX_TIME_SLICE As New TimeSpan(0, 0, 30)
    Private ReadOnly SERVER_QUEUE As New Dictionary(Of Integer, Server1)

    Public Sub IssueCommand(ByVal cmd)

        'Dim server As Server = Nothing
        'If SERVER_QUEUE.TryGetValue(serverId, server) = False Then
        '    server = New Server(serverId)
        '    SERVER_QUEUE.Add(serverId, server)
        'End If

        'Dim sb As New System.Text.StringBuilder("http://ogame" & serverId & ".de/" & file & ".php?")
        'For Each key As String In parameters.Keys
        '    Dim value As String = parameters(key)
        '    sb.Append("&" & key & "=" & value)
        'Next
        'Dim cmd As String = sb.ToString()

        'server.EmpireIssueCommand(handler, cmd, username, password)

    End Sub

End Class
