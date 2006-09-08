Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    Public Class GalaxyPage
        Inherits PageBase

#Region "regular expressions"

        Const GALAXY_INDEX_PATTERN As String = "(?<=<td class=""l""><input type=""text"" name=""galaxy"" value="")\d+(?="" size=""5"" maxlength=""3"" tabindex=""1"">)"
        Const SYSTEM_INDEX_PATTERN As String = "(?<=<td class=""l""><input type=""text"" name=""system"" value="")\d+(?="" size=""5"" maxlength=""3"" tabindex=""2"">)"
        Const PLANET_INFO_PATTERN As String = "    <tr>\n    <th width=""30"">\n      <a href=""#"" (?: tabindex=""\d+"" )?>(?<planetindex>\d+)</a>\n(?:<a href=""#"" title=""\D+(?<debrismetal>\d+)\D+(?<debriscrystal>\d+)""></a>)?    </th>\n    <th width=""30"">\n.*?</th>\n<th width=""130"" style='white-space: nowrap;'>\n(?<planetname>.*?)(?:&nbsp;[(](?<activity>[*]|\d+\D+)[)])?</th>\n<th width=""30"" style='white-space: nowrap;'>\n.*?</th> \n    <th width=""30"">\n.*?</th>\n    <th width=""150""> \n\n (?:    <a style=""cursor:pointer"" onmouseover="".*?""><span class=""(?<userstatus>.*?)"">(?<username>.*?)</span></a>)?\n.*?</th>\n<th width=""80""> \n(?:<a style=""cursor:pointer"" onmouseover="".*?"">\n\n   (?<useralliance>\w+) </a>)?.*?</th>\n<th width=""125"" style='white-space: nowrap;'>\n.*?</th>\n</tr>\n"

        Shared ReadOnly GI_RE As New Regex(GALAXY_INDEX_PATTERN)
        Shared ReadOnly SI_RE As New Regex(SYSTEM_INDEX_PATTERN)
        Shared ReadOnly RE As New Regex(PLANET_INFO_PATTERN, RegexOptions.Singleline)

#End Region

        Private _GalaxyIndex As Integer
        Private _SystemIndex As Integer
        Private _PlanetList As List(Of Planet)

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success As Boolean = True
            Dim startat As Integer = 0

            If success Then
                Dim m As Match = GI_RE.Match(Content, startat)
                If m.Success Then
                    _GalaxyIndex = m.Value
                    startat = m.Index + m.Length
                Else
                    success = False
                End If
            End If

            If success Then
                Dim m As Match = SI_RE.Match(Content, startat)
                If m.Success Then
                    _SystemIndex = m.Value
                    startat = m.Index + m.Length
                Else
                    success = False
                End If
            End If

            If success Then
                Dim list As List(Of Planet) = New List(Of Planet)()

                Dim m As Match = RE.Match(Content, startat)
                While m.Success
                    Dim planetIndex As Integer = m.Groups("planetindex").Value
                    Dim planetName As String = m.Groups("planetname").Value
                    Dim planetActivity As String = m.Groups("activity").Value
                    Dim hasMoon As Boolean = False 'todo
                    Dim debrisMetal As Integer = Val(m.Groups("debrismetal").Value)
                    Dim debrisCrystal As Integer = Val(m.Groups("debriscrystal").Value)
                    Dim username As String = m.Groups("username").Value
                    Dim userStatus As String = m.Groups("userstatus").Value
                    Dim userAlliance As String = m.Groups("useralliance").Value
                    Dim p As New Planet(_GalaxyIndex, _SystemIndex, planetIndex, planetName, planetActivity, hasMoon, debrisMetal, debrisCrystal, username, userStatus, userAlliance)
                    list.Add(p)
                    m = m.NextMatch()
                End While
                If list.Count > 0 Then
                    _PlanetList = list
                Else
                    success = False
                End If
            End If

            Return success

        End Function

        Public ReadOnly Property PlanetList() As List(Of Planet)
            Get
                Return _PlanetList
            End Get
        End Property

        Public ReadOnly Property GalaxyIndex() As Integer
            Get
                Return _GalaxyIndex
            End Get
        End Property

        Public ReadOnly Property SystemIndex() As Integer
            Get
                Return _SystemIndex
            End Get
        End Property
    End Class
End Namespace
