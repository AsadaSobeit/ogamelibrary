Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    ''' <summary>
    ''' 适用于建筑和研究两个页面
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TechLevelPage
        Inherits PageBase

#Region "Regular Expression"

        'match construction or research in progress
        '<tr>(?:<td class=l><a href=infos[.]php[?]session=\w{12}&gid=\d+(?:&PHPSESSID=\w{32})?>.*?</a></td>)?<td class=l><a href=infos[.]php[?]session=(?<session>\w{12})&gid=(?<gid>\d+)(?:&PHPSESSID=\w{32})?>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[k]>(?:<script src="lang/en/cnt[.]js" type="text/javascript"> </script><div id="bxx" class="z"></div><SCRIPT language=JavaScript>\s+pp="(?<seconds>\d+)";\s+pk="\k<gid>";\s+pl="(?<planetid>\d+)";\s+ps="\k<session>";\s+t[(][)];\s+</script>\s+|</td>| - |.*?ss=(?<seconds>\d+).*?)</td></tr>
        Const LIST_ITEM_PATTERN1 As String = "<tr>(?:<td class=l><a href=infos[.]php[?]session=\w{12}&gid=\d+(?:&PHPSESSID=\w{32})?>.*?</a></td>)?<td class=l><a href=infos[.]php[?]session=(?<session>\w{12})&gid=(?<gid>\d+)(?:&PHPSESSID=\w{32})?>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[k]>(?:<script src=""lang/en/cnt[.]js"" type=""text/javascript""> </script><div id=""bxx"" class=""z""></div><SCRIPT language=JavaScript>\s+pp=""(?<seconds>\d+)"";\s+pk=""\k<gid>"";\s+pl=""(?<planetid>\d+)"";\s+ps=""\k<session>"";\s+t[(][)];\s+</script>\s+|</td>| - |.*?ss=(?<seconds>\d+).*?)</td></tr>"

        'match construction or research idle
        '<tr>(?:<td class=l><a href=infos[.]php[?]session=\w{12}&gid=\d+(?:&PHPSESSID=\w{32})?>.*?</a></td>)?<td class=l><a href=infos[.]php[?]session=(?<session>\w{12})&gid=(?<gid>\d+)(?:&PHPSESSID=\w{32})?>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[l]>.*?</td></tr>
        Const LIST_ITEM_PATTERN2 As String = "<tr>(?:<td class=l><a href=infos[.]php[?]session=\w{12}&gid=\d+(?:&PHPSESSID=\w{32})?>.*?</a></td>)?<td class=l><a href=infos[.]php[?]session=(?<session>\w{12})&gid=(?<gid>\d+)(?:&PHPSESSID=\w{32})?>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[l]>.*?</td></tr>"

        Const LIST_ITEM_PATTERN As String = LIST_ITEM_PATTERN1 & "|" & LIST_ITEM_PATTERN2

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)

#End Region

        Private _LevelMap As Dictionary(Of Gid, Integer)
        Private _TimeLeft As TimeSpan
        Private _SecondsLeft As Integer

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success = MyBase.Parse()
            If success Then
                _LevelMap = New Dictionary(Of Gid, Integer)()
                _TimeLeft = New TimeSpan(-1)
                _SecondsLeft = -1

                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    Dim gid As String = m.Groups("gid").Value
                    Dim value As String = m.Groups("level").Value
                    Dim seconds As String = m.Groups("seconds").Value

                    If value.Length > 0 Then
                        _LevelMap.Add(gid, value)
                    Else
                        _LevelMap.Add(gid, 0)
                    End If

                    If seconds.Length > 0 Then
                        _TimeLeft = New TimeSpan(0, 0, seconds)
                        _SecondsLeft = Integer.Parse(seconds)
                    End If
                    m = m.NextMatch()
                End While
            End If

            Return success

        End Function

        Public ReadOnly Property LevelMap() As Dictionary(Of Gid, Integer)
            Get
                Return _LevelMap
            End Get
        End Property

        Public ReadOnly Property TimeLeft() As TimeSpan
            Get
                Return _TimeLeft
            End Get
        End Property

        Public ReadOnly Property SecondsLeft() As Integer
            Get
                Return _SecondsLeft
            End Get
        End Property
    End Class
End Namespace
