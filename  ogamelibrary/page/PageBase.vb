Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Text

Namespace Page

    Public MustInherit Class PageBase

#Region "shared"

        Protected Shared Function CIntGerman(ByVal number As String) As Integer

            number = number.Replace(".", "")
            number = number.Replace(",", ".")
            CIntGerman = Val(number)

        End Function

        Public Shared Function ConvertUTF8toUnicode(ByVal utf8String As String) As String

            'this line not works well
            Dim utf8Bytes As Byte() = Encoding.UTF8.GetBytes(utf8String)

            Dim unicodeBytes As Byte() = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes)
            Dim unicodeString As String = Encoding.Unicode.GetString(unicodeBytes)

            Return unicodeString

        End Function
#End Region

#Region "Regular Expression"

        '(?:.*?)
        Protected Const NONSENSE_PATTERN As String = "(?:.*?)"

        '(?<=<td align="center" width="85">)(?:<font color=#ff0000>)?(?<delta>(?:-|\d|\.)+)(?:</font>)?/(?<supply>(?:\d|\.)+)(?=</td>)
        Const POWER_PATTERN As String = "(?<=<td align=""center"" width=""85"">)(?:<font color=#ff0000>)?(?<delta>(?:-|\d|\.)+)(?:</font>)?/(?<generation>(?:\d|\.)+)(?=</td>)"

        '(?:(?<=<td align="center" width="85">(?:<font color='#ff0000'>)?))(?:\d|\.)+(?=(?:</font>)?</td>)
        Const RESOURCE_PATTERN As String = "(?:(?<=<td align=""center"" width=""85"">(?:<font color='#ff0000'>)?))(?:\d|\.)+(?=(?:</font>)?</td>)"

        '(?:<option value="/game/\w+[.]php[?]session=\w{12}&cp=(?<planetid>\d+)[^"]*" (?<selected>(?:selected)?)>.*?</option>\s*)+
        Const PLANETID_PATTERN As String = "(?:<option value=""/game/\w+[.]php[?]session=\w{12}&cp=(?<planetid>\d+)[^""]*"" (?<selected>(?:selected)?)>.*?</option>\s*)+"

        '<img src="(?<smallimage>[^"]*/planeten/small/s_(?<planettype>[^"]+?).jpg?)" width="50" height="50">
        Const SMALL_IMAGE_PATTERN As String = "<img src=""(?<smallimage>[^""]*/planeten/small/s_(?<planettype>[^""]+?).jpg?)"" width=""50"" height=""50"">"

        Const GENERAL_INFO_PATTERN As String = SMALL_IMAGE_PATTERN & NONSENSE_PATTERN & _
                PLANETID_PATTERN & NONSENSE_PATTERN & _
                "(?<metal>" & RESOURCE_PATTERN & ")" & NONSENSE_PATTERN & _
                "(?<crystal>" & RESOURCE_PATTERN & ")" & NONSENSE_PATTERN & _
                "(?<deuterium>" & RESOURCE_PATTERN & ")" & NONSENSE_PATTERN & _
                POWER_PATTERN

        '<script>setTimeout[(]window[.]parent[.]location='[^']+',1500[)];</script>.*?<br>
        Const TIME_OUT_PATTERN As String = "<script>setTimeout[(]window[.]parent[.]location='[^']+',1500[)];</script>.*?<br>"

        Shared ReadOnly GENERAL_INFO_REGEX As New Regex(GENERAL_INFO_PATTERN, RegexOptions.Singleline)
        Shared ReadOnly TIME_OUT_REGEX As New Regex(TIME_OUT_PATTERN)

#End Region

        Private _PlanetType As String
        Private _SmallImageUri As String

        Private _PlanetIdList As List(Of String)
        Private _CurrentPlanetId As String

        Private _Metal As Integer
        Private _Crystal As Integer
        Private _Deuterium As Integer

        Private _Content As String

        Public Sub New(ByVal html As String)

            _Content = html

        End Sub

        Public Overridable Function Parse() As Boolean

            Dim success As Boolean = True

            Dim m As Match = GENERAL_INFO_REGEX.Match(_Content)
            If m.Success Then
                _SmallImageUri = m.Groups("smallimage").Value
                _PlanetType = m.Groups("planettype").Value

                _PlanetIdList = New List(Of String)()
                With m.Groups("planetid")
                    For Each c As Capture In .Captures
                        _PlanetIdList.Add(c.Value)
                    Next
                End With
                With m.Groups("selected")
                    Dim index As Integer = 0
                    For Each c As Capture In .Captures
                        If c.Length > 0 Then
                            _CurrentPlanetId = _PlanetIdList(index)
                            Exit For
                        End If
                        index += 1
                    Next
                End With

                _Metal = CIntGerman(m.Groups("metal").Value)
                _Crystal = CIntGerman(m.Groups("crystal").Value)
                _Deuterium = CIntGerman(m.Groups("deuterium").Value)
            ElseIf TIME_OUT_REGEX.IsMatch(_Content) Then
                Throw New InvalidSessionException()
            Else
                success = False
            End If

            Return success

        End Function

        Public ReadOnly Property PlanetType() As String
            Get
                Return _PlanetType
            End Get
        End Property

        Public ReadOnly Property SmallImageUri() As String
            Get
                Return _SmallImageUri
            End Get
        End Property

        Public ReadOnly Property PlanetIdList() As List(Of String)
            Get
                Return _PlanetIdList
            End Get
        End Property

        Public ReadOnly Property CurrentPlanetId() As String
            Get
                Return _CurrentPlanetId
            End Get
        End Property

        Public ReadOnly Property Metal() As Integer
            Get
                Return _Metal
            End Get
        End Property

        Public ReadOnly Property Crystal() As Integer
            Get
                Return _Crystal
            End Get
        End Property

        Public ReadOnly Property Deuterium() As Integer
            Get
                Return _Deuterium
            End Get
        End Property

        Public ReadOnly Property Content() As String
            Get
                Return _Content
            End Get
        End Property
    End Class
End Namespace
