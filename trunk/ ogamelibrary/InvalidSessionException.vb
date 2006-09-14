Public Class InvalidSessionException
    Inherits OGameException

    Public Const MY_MESSAGE As String = "Invalid Session"

    Public Sub New()

        MyBase.New(MY_MESSAGE)

    End Sub
End Class
