Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Text

Namespace Command

    Public MustInherit Class CommandBase

#Region "shared"

        Private Const SESSION_ID_PATTERN As String = "(?<=/game/index[.]php[?]session=)\w{12}"
        Private Shared ReadOnly SESSION_ID_REGEX As New Regex(SESSION_ID_PATTERN, RegexOptions.Singleline)

        'http://ogame424.de/game/reg/login2.php?timestamp=0&v=2&login=rblade&pass=rathelerwh
        Private Const LOGIN_URI_FORMAT As String = "http://{0}/game/reg/login2.php?timestamp=0&v=2&login={1}&pass={2}"

        Private Const LOGOUT_URI_FORMAT As String = "http://{0}/game/logout.php?session={1}"

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="serverName"></param>
        ''' <param name="username"></param>
        ''' <param name="password"></param>
        ''' <returns>session id, or Nothing if login failed.</returns>
        ''' <remarks></remarks>
        Public Shared Function Login(ByVal serverName As String, ByVal username As String, ByVal password As String) As String

            Dim session As String

            Dim uriStr As String = String.Format(LOGIN_URI_FORMAT, serverName, username, password)
            'Dim loginUri As Uri = New Uri(uriStr)
            'Dim loginUri As Uri
            'With New ogameDataSetTableAdapters.CommandTableAdapter()
            '    Dim loginCmdTable As ogameDataSet.CommandDataTable = .GetCommand("login", ServerId)
            '    Dim cmdStr As String = loginCmdTable(0).Text
            '    Dim paramList As String() = loginCmdTable(0).Parameters.Split(",")
            '    cmdStr = cmdStr.Replace(paramList(0), ServerId)
            '    cmdStr = cmdStr.Replace(paramList(1), Username)
            '    cmdStr = cmdStr.Replace(paramList(2), Password)
            '    loginUri = New Uri(cmdStr)
            'End With

            'Dim loginRequest As WebRequest = WebRequest.CreateDefault(loginUri)
            'Dim loginResponse As WebResponse = loginRequest.GetResponse()
            'Me._Session = loginResponse.ResponseUri.Query
            'Dim str As String = New IO.StreamReader(loginResponse.GetResponseStream()).ReadToEnd()
            'Dim LoginWebClient As New WebClient()
            'Dim relayPage As String = LoginWebClient.DownloadString(loginUri)
            Dim relayPage As String = Post(uriStr)
            Dim m As Match = SESSION_ID_REGEX.Match(relayPage)
            If m.Success Then
                session = m.Value
            Else
                'login failed
                session = Nothing
            End If

            Return session

        End Function

        Public Shared Function Logout(ByVal serverName As String, ByVal session As String) As String

            Dim uriStr As String = String.Format(LOGOUT_URI_FORMAT, serverName, session)
            Dim page As String = Post(uriStr)

            Return page

        End Function

        ''' <summary>
        ''' todo: simulate a web browser
        ''' </summary>
        ''' <param name="uriStr"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Post(ByVal uriStr As String) As String

            Dim html As String

            Try
                Dim uri As New Uri(uriStr)
                Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
                With request
                    .Timeout = 10000
                    .Method = WebRequestMethods.Http.Post
                    .UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.6) Gecko/20060728 Firefox/1.5.0.6"
                End With

                Dim response As HttpWebResponse = request.GetResponse()

                Dim sr As New StreamReader(response.GetResponseStream())
                html = sr.ReadToEnd()
                'Catch ex As WebException
                '    If ex.Response Is Nothing Then
                '        Throw New InvalidCommandException(uri, ex)
                '    End If

                '    Dim statusCode As HttpStatusCode = CType(ex.Response, HttpWebResponse).StatusCode
            Catch ex As Exception
                Throw New InvalidCommandException(uriStr, ex)
            End Try

            Return html

        End Function
#End Region

        Private ReadOnly _ServerName As String

        'http parameter cp. If it is set to Nothing, planetId refers to home planet.
        Private ReadOnly _PlanetId As String

        Public Sub New(ByVal serverName As String)

            _ServerName = serverName
            _PlanetId = Nothing

        End Sub

        Public Sub New(ByVal serverName As String, ByVal planetId As String)

            _ServerName = serverName
            _PlanetId = planetId

        End Sub

        Public ReadOnly Property ServerName() As String
            Get
                Return _ServerName
            End Get
        End Property

        Public ReadOnly Property PlanetId() As String
            Get
                Return _PlanetId
            End Get
        End Property

        Public Sub Execute(ByVal sessionId As String)

            'Dim wb As New Net.WebClient()
            'Dim uri As Uri = GetUri(session)

            'Dim utf8Bytes As Byte() = wb.DownloadData(uri)
            'Dim unicodeBytes As Byte() = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes)
            'Dim unicodeString As String = Encoding.Unicode.GetString(unicodeBytes)

            'SetPageContent(unicodeString)
            Dim uriStr As String = GetUri(sessionId)

            Dim html As String = Post(uriStr)

            SetPageContent(html)

        End Sub

        Public Function GetUri(ByVal session As String) As String

            If _PlanetId Is Nothing Then
                GetUri = String.Format(UriFormat, _ServerName, session)
            Else
                GetUri = String.Format(PlanetUriFormat, _ServerName, session, _PlanetId)
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns>{0} is server name, {1} is session id</returns>
        ''' <remarks></remarks>
        Protected MustOverride ReadOnly Property UriFormat() As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <value></value>
        ''' <returns>{0} is server name, {1} is session id, {2} is current planet</returns>
        ''' <remarks></remarks>
        Protected MustOverride ReadOnly Property PlanetUriFormat() As String

        Protected MustOverride Sub SetPageContent(ByVal html As String)

        'Private ReadOnly Property Build(ByVal cmd As String, ByVal list As List(Of fmenge)) As String
        '    Get
        '        Dim sb As New StringBuilder(cmd)
        '        For Each item As fmenge In list
        '            sb.Append(item)
        '        Next

        '        Return sb.ToString()

        '    End Get
        'End Property
    End Class

    'Public Class fmenge

    '    Private _gid As String
    '    Private _number As Integer

    '    Public Sub New(ByVal gid As String, ByVal number As Integer)

    '        _gid = gid
    '        _number = number

    '    End Sub

    '    Public Overrides Function ToString() As String

    '        Static str As String = "&fmenge[" & _gid & "]=" & _number
    '        Return str

    '    End Function
    'End Class
End Namespace
