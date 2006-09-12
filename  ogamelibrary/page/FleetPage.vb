Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    ''' <summary>
    ''' 适用于驻留舰队
    ''' todo: 解析作业舰队
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FleetPage
        Inherits PageBase

#Region "Regular Expression"

        'name="maxship(?<gid>\d+)" value="(?<number>\d+)"
        Const LIST_ITEM_PATTERN As String = "name=""maxship(?<gid>\d+)"" value=""(?<count>\d+)"""

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)

#End Region

        Private _UnitCountMap As Dictionary(Of Integer, Integer)

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success = MyBase.Parse()
            If success Then
                _UnitCountMap = New Dictionary(Of Integer, Integer)()

                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    Dim gid As String = m.Groups("gid").Value
                    Dim count As String = m.Groups("count").Value
                    _UnitCountMap.Add(gid, count)

                    m = m.NextMatch()
                End While
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
