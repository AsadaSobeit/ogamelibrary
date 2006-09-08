Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    Public Class DefensePage
        Inherits PageBase

#Region "Regular Expression"

        '<tr><td class=l><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+).*?><img border='0' src="(?<skinurl>.*?)gebaeude/\k<gid>[.]gif" align='top' width='120' height='120'></a></td><td class=l><a href=infos[.]php[?]session=\w{12}&gid=\k<gid>.*?>(?<name>.*?)</a>(?:</a> [(] ?(?<count>\d+) available ?[)])?<br>(?<description>.*?)<br>(?<requirement>.*?)<br>(?<timerequired>.*?)<br></th><td class=k >(?:<input type=text name=fmenge[[]\k<gid>[]] alt='\k<name>' size=6 maxlength=6 value=0 tabindex=1> )?.*?</td></tr>
        Const LIST_ITEM_PATTERN As String = "<tr><td class=l><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+).*?><img border='0' src=""(?<skinurl>.*?)gebaeude/\k<gid>[.]gif"" align='top' width='120' height='120'></a></td><td class=l><a href=infos[.]php[?]session=\w{12}&gid=\k<gid>.*?>(?<name>.*?)</a>(?:</a> [(] ?(?<count>\d+) available ?[)])?<br>(?<description>.*?)<br>(?<requirement>.*?)<br>(?<timerequired>.*?)<br></th><td class=k >(?:<input type=text name=fmenge[[]\k<gid>[]] alt='\k<name>' size=6 maxlength=6 value=0 tabindex=1> )?.*?</td></tr>"

        '\ng = (?<secondsElapsed>\d+);\ns = 0;\nhs = 0;\nof = 1;\nc = new Array[(](?:(?<secondsPerUnit>\d+),)+""[)];\nb = new Array[(](?:"(?<name>.*?)",)+""[)];\na = new Array[(](?:"(?<count>\d+)",)?""[)];\n
        Const PROGRESS_PATTERN As String = "\ng = (?<secondsElapsed>\d+);\ns = 0;\nhs = 0;\nof = 1;\nc = new Array[(](?:(?<secondsPerUnit>\d+),)+""""[)];\nb = new Array[(](?:""(?<name>.*?)"",)+""""[)];\na = new Array[(](?:""(?<count>\d+)"",)?""""[)];\n"

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)
        Shared ReadOnly PROGRESS_REGEX As New Regex(PROGRESS_PATTERN)
#End Region

        Private _UnitCountMap As Dictionary(Of Integer, Integer)

        ''' <summary>
        ''' todo
        ''' </summary>
        ''' <remarks></remarks>
        Private _Queue As ShipyardQueue

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim position As Integer = 0

            Dim success = MyBase.Parse()
            If success Then
                _UnitCountMap = New Dictionary(Of Integer, Integer)

                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    Dim gid As String = m.Groups("gid").Value
                    Dim count As String = m.Groups("count").Value
                    If count.Length > 0 Then
                        _UnitCountMap.Add(gid, count)
                    Else
                        _UnitCountMap.Add(gid, 0)
                    End If

                    position = m.Index + m.Length
                    m = m.NextMatch()
                End While
            End If

            If position > 0 Then
                Dim m As Match = PROGRESS_REGEX.Match(Content, position)
                If m.Success Then
                    Dim secondsElapsed As Integer = m.Groups("secondsElapsed").Value
                    Dim secPerUnitCaptures As CaptureCollection = m.Groups("secondsPerUnit").Captures
                    Dim nameCaptures As CaptureCollection = m.Groups("name").Captures
                    Dim countCaptures As CaptureCollection = m.Groups("count").Captures

                    Dim c As Integer = secPerUnitCaptures.Count
                    Dim startTime As Date = Now - New TimeSpan(0, 0, secondsElapsed)
                    Dim unitList As New List(Of ShipyardQueue.Unit)(c)
                    For i As Integer = 0 To c - 1
                        Dim secondsPerUnit As Integer = secPerUnitCaptures(i).Value
                        Dim name As String = nameCaptures(i).Value
                        Dim count As Integer = countCaptures(i).Value
                        Dim u As New ShipyardQueue.Unit(name, secondsPerUnit, count)
                        unitList.Add(u)
                    Next

                    _Queue = New ShipyardQueue(startTime, unitList)
                End If
            End If

            Return success

        End Function

        Public ReadOnly Property UnitCountMap() As Dictionary(Of Integer, Integer)
            Get
                Return _UnitCountMap
            End Get
        End Property
    End Class
End Namespace
