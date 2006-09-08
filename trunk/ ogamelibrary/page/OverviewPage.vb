Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    Public Class OverviewPage
        Inherits PageBase

#Region "regular expressions"

        Const CURRENT_PLANET_PATTERN As String = "&pl=(?<currentplanet>\d+)"

        '(?<=&quot;).*?(?=&quot;)
        Const PLANET_NAME_PATTERN As String = "&quot;(?<planetname>.+?)&quot;"

        '(?<=[(]).*?(?=[)])
        'Const PLAYER_NAME_PATTERN As String = "(?<=[(]).*?(?=[)])"

        '(?<weekday>Sat|Sun|Mon|Tue|Web|Thu|Fri) (?<month>Sep|Oct|Nov|Dec|Jan|Feb|Mat|Apr|May|Jun|Jul|Aug) (?<day>\d{1,2}) (?<time>\d{1,2}:\d\d:\d\d)
        Const SERVER_TIME_PATTERN As String = "(?<servertime>(?<weekday>Sat|Sun|Mon|Tue|Web|Thu|Fri) (?<month>Sep|Oct|Nov|Dec|Jan|Feb|Mat|Apr|May|Jun|Jul|Aug) (?<day>\d{1,2}) (?<time>\d{1,2}:\d\d:\d\d))"

        '[(]\s?<a title="[^"]*">\s?\d+ </a> / <a title="[^"]*">(\d+)\s</a>\s.*?[)]
        Const MAX_FIELDS_PATTERN As String = "[(]\s?<a title=""[^""]*"">\s?(?<developedfields>\d+) </a> / <a title=""[^""]*"">(?<maxfields>\d+)\s</a>\s.*?[)]"

        '(-?\d+)&deg;C .+ (-?\d+)\s?&deg;C
        Const TEMPERATURE_PATTERN As String = "(?<lowesttemperature>-?\d+)&deg;C .+ (?<highesttemperature>-?\d+)\s?&deg;C"

        '<th colspan=3>(\d+):(\d+):(\d+)</th>
        Const POSITION_PATTERN As String = "<th colspan=3>(?<galaxyindex>\d+):(?<systemindex>\d+):(?<planetindex>\d+)</th>"

        '(?:.*? <a href='stat[.]php[?]session=\w{12}&start=\d+'>(\d+)</a> .*? (\d+))
        Const RANKING_PATTERN_SUB1 As String = "(?<empirecount>\d+)\D+?<a href='stat[.]php[?][^']+'>(?<rank>\d+)</a>.*?"
        Const RANKING_PATTERN_SUB2 As String = "<a href='stat[.]php[?][^']+'>(?<rank>\d+)</a>\D+(?<empirecount>\d+)"
        '<th colspan=3>(\d+)\D*[(](?:\D+(\d+)\D+?<a href='stat[.]php[?]session=\w{12}&start=\d+'>(\d+)</a>.*?)|(?:\D+<a href='stat[.]php[?]session=\w{12}&start=\d+'>(\d+)</a>\D+(\d+))[)]</th>
        Const RANKING_PATTERN As String = "<th colspan=3>(?<points>\d+)\D*[(]\D+(?:(?:" & RANKING_PATTERN_SUB1 & ")|(?:" & RANKING_PATTERN_SUB2 & "))[)]</th>"

        Const OVERVIEW_PATTERN As String = PLANET_NAME_PATTERN & NONSENSE_PATTERN & _
                SERVER_TIME_PATTERN & NONSENSE_PATTERN & _
                MAX_FIELDS_PATTERN & NONSENSE_PATTERN & _
                TEMPERATURE_PATTERN & NONSENSE_PATTERN & _
                POSITION_PATTERN & NONSENSE_PATTERN & _
                RANKING_PATTERN

        '"(?<player>" & PLAYER_NAME_PATTERN & ")" & NONSENSE_PATTERN & _

        Shared ReadOnly RE As New Regex(OVERVIEW_PATTERN, RegexOptions.Singleline)

#End Region

        Private _PlanetName As String
        Private _ServerTime As String
        Private _MaxFields As Integer
        Private _LowestTemperature As Integer
        Private _HighestTemperature As Integer
        Private _GalaxyIndex As Integer
        Private _SystemIndex As Integer
        Private _PlanetIndex As Integer
        Private _Points As Integer
        Private _Rank As Integer
        Private _EmpireCount As Integer

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success As Boolean = MyBase.Parse()
            If success Then
                Dim m As Match = RE.Match(Content)
                If m.Success Then
                    _PlanetName = m.Groups("planetname").Value
                    _ServerTime = m.Groups("servertime").Value
                    _MaxFields = m.Groups("maxfields").Value
                    _LowestTemperature = m.Groups("lowesttemperature").Value
                    _HighestTemperature = m.Groups("highesttemperature").Value
                    _GalaxyIndex = m.Groups("galaxyindex").Value
                    _SystemIndex = m.Groups("systemindex").Value
                    _PlanetIndex = m.Groups("planetindex").Value
                    _Points = m.Groups("points").Value
                    _Rank = m.Groups("rank").Value
                    _EmpireCount = m.Groups("empirecount").Value
                Else
                    success = False
                End If
            End If

            Return success

        End Function

        Public ReadOnly Property EmpireCount() As Integer
            Get
                Return _EmpireCount
            End Get
        End Property

        Public ReadOnly Property Rank() As Integer
            Get
                Return _Rank
            End Get
        End Property

        Public ReadOnly Property Points() As Integer
            Get
                Return _Points
            End Get
        End Property

        Public ReadOnly Property PlanetIndex() As Integer
            Get
                Return _PlanetIndex
            End Get
        End Property

        Public ReadOnly Property SystemIndex() As Integer
            Get
                Return _SystemIndex
            End Get
        End Property

        Public ReadOnly Property GalaxyIndex() As Integer
            Get
                Return _GalaxyIndex
            End Get
        End Property

        Public ReadOnly Property PlanetName() As String
            Get
                Return _PlanetName
            End Get
        End Property

        Public ReadOnly Property ServerTime() As String
            Get
                Return _ServerTime
            End Get
        End Property

        Public ReadOnly Property MaxFields() As Integer
            Get
                Return _MaxFields
            End Get
        End Property

        Public ReadOnly Property LowestTemperature() As Integer
            Get
                Return _LowestTemperature
            End Get
        End Property

        Public ReadOnly Property HighestTemperature() As Integer
            Get
                Return _HighestTemperature
            End Get
        End Property
    End Class
End Namespace
