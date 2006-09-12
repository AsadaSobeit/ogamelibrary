Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace Page

    ''' <summary>
    ''' not implemented
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TechnologyPage
        Inherits PageBase

#Region "Regular Expression"

        '<tr><td class=l><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+)>(?<name>.*?)</a></th><td class=l>(?:<font color=#(?<flag>\w{6})>(?<parentname>.*?)[(]\D+(?<parentlevel>\d+)[)]\n</font><br>)*</td></tr>
        Const LIST_ITEM_PATTERN As String = "<tr><td class=l><a href=infos[.]php[?]session=\w{12}&gid=(?<gid>\d+)>(?<name>.*?)</a></th><td class=l>(?:<font color=#(?<flag>\w{6})>(?<parentname>.*?)[(]\D+(?<parentlevel>\d+)[)]\n</font><br>)*</td></tr>"

        Shared ReadOnly LIST_ITEM_REGEX As New Regex(LIST_ITEM_PATTERN, RegexOptions.Singleline)

#End Region

        'todo
        Private _DependencyList As New List(Of Dependency)()

        Public Sub New(ByVal html As String)

            MyBase.New(html)

        End Sub

        Public Overrides Function Parse() As Boolean

            Dim success = MyBase.Parse()
            If success Then
                Dim count As Integer = 0
                Dim m As Match = LIST_ITEM_REGEX.Match(Content)
                While m.Success
                    count += 1
                    'Dim gid = m.Groups("gid").Value
                    'Dim name = m.Groups("name").Value
                    m = m.NextMatch()
                End While
                If count = 0 Then
                    success = False
                End If
            End If

            Return success

        End Function
    End Class
End Namespace
