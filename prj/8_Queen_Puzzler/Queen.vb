Public Class Queen

    Private myName As String
    Private myRow As Integer

    Public Sub New(ByVal Nm As String, ByVal rVal As Integer)
        myName = Nm
        myRow = rVal
    End Sub

    Public Property Name As String
        Get
            Return myName
        End Get
        Set(ByVal Value As String)
            myName = Value
        End Set
    End Property

    Public Property Row As Integer
        Get
            Return myRow
        End Get
        Set(ByVal Value As Integer)
            myRow = Value
        End Set
    End Property

End Class
