
'/**
' The module logging_Utils is used to maintain
' a persistent log of issues that arise during 
' program execution
'
' @author Joe Downey
' @version 1.0
'**/

Module loggingUtilities

    Dim myLogName As String

    Sub setLogName(ByVal logName As String)
        myLogName = logName
    End Sub

    Sub logTestCaseResult(ByVal result As String)

        Dim logMessage As String

        logMessage = result & " at " & Date.Now & vbCrLf

        Console.WriteLine(logMessage)
        My.Computer.FileSystem.WriteAllText(myLogName, logMessage, True)
    End Sub

End Module
