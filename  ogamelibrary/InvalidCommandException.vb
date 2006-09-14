Public Class InvalidCommandException
    Inherits OGameException

    Public Sub New(ByVal message As String, ByVal innerException As Exception)

        MyBase.New(message, InnerException)

    End Sub
End Class
