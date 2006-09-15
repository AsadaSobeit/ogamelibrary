Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    Public Class ResourcesPage
        Inherits PageBase

#Region "Regular Expression"

        '<select name="last(?<gid>\d+)" size="1">(?:\s*<option value="(?<percentage>\d{0,2}0)" (?<selected>(?:selected)?)?>\k<percentage>%</option>){11}\s*</select>
        Const LIST_ITEM_PATTERN As String = "<select name=""last(?<gid>\d+)"" size=""1"">(?:\s*<option value=""(?<percentage>\d{0,2}0)"" (?<selected>(?:selected)?)?>\k<percentage>%</option>){11}\s*</select>"

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)

#End Region

        Private _PercentageMap As Dictionary(Of Gid, Integer)

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success = MyBase.Parse()
            If success Then
                _PercentageMap = New Dictionary(Of Gid, Integer)()

                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    Dim gid As String = m.Groups("gid").Value

                    Dim percentage As Integer
                    Dim index As Integer = 0
                    With m.Groups("selected")
                        For Each c As Capture In .Captures
                            If c.Length > 0 Then
                                Exit For
                            End If
                            index += 1
                        Next
                    End With
                    With m.Groups("percentage")
                        percentage = .Captures(index).Value
                    End With

                    _PercentageMap(gid) = percentage

                    m = m.NextMatch()
                End While
            End If

            Return success

        End Function

        Public ReadOnly Property PercentageMap() As Dictionary(Of Gid, Integer)
            Get
                Return _PercentageMap
            End Get
        End Property
    End Class
End Namespace
