﻿Imports System.IO

Module fileParsingUtilities

    Dim piecePositions As String

    Function getQueenInitialPositions(ByVal InitialLocationStringArray As String()) As Integer()
        Dim i As Integer
        Dim returnArray As Integer() = {}
        For i = 0 To i < InitialLocationStringArray.Length
            returnArray(i) = convertAlgebraicNotationToNumeralPosition(InitialLocationStringArray(i))

        Next i

        Return returnArray
    End Function

    Function convertAlgebraicNotationToNumeralPosition(ByVal AN As String) As Integer
        Select Case AN
            Case "A1"
                Return 1
            Case "A2"
                Return 2
            Case "A3"
                Return 3
            Case "A4"
                Return 4
            Case "A5"
                Return 5
            Case "A6"
                Return 6
            Case "A7"
                Return 7
            Case "A8"
                Return 8
            Case "B1"
                Return 9
            Case "B2"
                Return 10
            Case "B3"
                Return 11
            Case "B4"
                Return 12
            Case "B5"
                Return 13
            Case "B6"
                Return 14
            Case "B7"
                Return 15
            Case "B8"
                Return 16
            Case "C1"
                Return 17
            Case "C2"
                Return 18
            Case "C3"
                Return 19
            Case "C4"
                Return 20
            Case "C5"
                Return 21
            Case "C6"
                Return 22
            Case "C7"
                Return 23
            Case "C8"
                Return 24
            Case "D1"
                Return 25
            Case "D2"
                Return 26
            Case "D3"
                Return 27
            Case "D4"
                Return 28
            Case "D5"
                Return 29
            Case "D6"
                Return 30
            Case "D7"
                Return 31
            Case "D8"
                Return 32
            Case "E1"
                Return 33
            Case "E2"
                Return 34
            Case "E3"
                Return 35
            Case "E4"
                Return 36
            Case "E5"
                Return 37
            Case "E6"
                Return 38
            Case "E7"
                Return 39
            Case "E8"
                Return 40
            Case "F1"
                Return 41
            Case "F2"
                Return 42
            Case "F3"
                Return 43
            Case "F4"
                Return 44
            Case "F5"
                Return 45
            Case "F6"
                Return 46
            Case "F7"
                Return 47
            Case "F8"
                Return 48
            Case "G1"
                Return 49
            Case "G2"
                Return 50
            Case "G3"
                Return 51
            Case "G4"
                Return 52
            Case "G5"
                Return 53
            Case "G6"
                Return 54
            Case "G7"
                Return 55
            Case "G8"
                Return 56
            Case "H1"
                Return 57
            Case "H2"
                Return 58
            Case "H3"
                Return 59
            Case "H4"
                Return 60
            Case "H5"
                Return 61
            Case "H6"
                Return 62
            Case "H7"
                Return 63
            Case "H8"
                Return 64
            Case Else
                Return 0
        End Select
    End Function

    Sub getTestCases()
        Dim myFileStream As Stream = TestSetup.ofd_TestCase.OpenFile
        Dim myReader As StreamReader = New StreamReader(myFileStream)
        While getSingleTestCase(myReader)
            Delay(3)
            'MsgBox("after delay!")
        End While
    End Sub
    Function getSingleTestCase(ByVal myreader As StreamReader) As Integer

        piecePositions = getNonCommentLine(myreader)
        If piecePositions = "END" Then
            Return 0
        End If

        SetupQueenslist(piecePositions)

        TestSetup.updateChessGrid(piecePositions)
        Delay(3)

        piecePositions = SolveNQueen()

        TestSetup.updateChessGrid(piecePositions)

        Dim optimalSolution = getNonCommentLine(myreader)
        If optimalSolution = "END" Then
            Return 0
        End If
        Return 1
    End Function

    Function getNonCommentLine(ByVal myreader As StreamReader) As String
        Dim myReturnString As String = ""

        Dim myLine As String
        myLine = myreader.ReadLine
        Try
            While (myLine.Substring(0, 1) = "#")
                myLine = myreader.ReadLine
            End While
            myReturnString = myLine
        Catch ex As NullReferenceException
            myReturnString = "END"
        End Try

        Return myReturnString
    End Function

    Function getPositionString() As String
        Return piecePositions
    End Function


    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents() ' Allow windows messages to be processed
        Loop

    End Sub

End Module
