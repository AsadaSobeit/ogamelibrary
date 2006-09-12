Public Class InvalidCommandException
    Inherits Exception

    Public Sub New(ByVal message As String, ByVal innerException As Exception)

        MyBase.New(message, InnerException)

    End Sub
End Class
