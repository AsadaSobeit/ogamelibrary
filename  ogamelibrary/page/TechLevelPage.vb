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

        '(?:<tr><td class=l>(?<a><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+).*?>).*?</td><td class=l>\k<a>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[lk]>.*?</td></tr>(?:</tr>)*)
        Const LIST_ITEM_PATTERN As String = "(?:<tr><td class=l>(?<a><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+).*?>).*?</td><td class=l>\k<a>.*?</a>(?:</a> [(]\D+(?<level>\d+) ?[)])?<br>.*?(?:</td>)?<td class=[lk]>.*?</td></tr>(?:</tr>)*)"

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)

#End Region

        Private _LevelMap As Dictionary(Of Gid, Integer)

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success = MyBase.Parse()
            If success Then
                _LevelMap = New Dictionary(Of Gid, Integer)()

                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    Dim gid As String = m.Groups("gid").Value
                    Dim value As String = m.Groups("level").Value
                    If value.Length > 0 Then
                        _LevelMap.Add(gid, value)
                    Else
                        _LevelMap.Add(gid, 0)
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
    End Class
End Namespace
