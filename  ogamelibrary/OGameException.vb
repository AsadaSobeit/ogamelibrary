Public Class OGameException
    Inherits Exception

    Public Sub New(ByVal message As String)

        MyBase.New(message)

        My.Application.Log.WriteEntry(message)

    End Sub

    Public Sub New(ByVal message As String, ByVal innerException As Exception)

        MyBase.New(message, innerException)

        My.Application.Log.WriteEntry(message)
        My.Application.Log.WriteException(innerException)

    End Sub
End Class
