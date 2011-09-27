Imports System.IO

Module fileParsingUtilities

    Dim piecePositions As String
    Dim puzzleType As String
    Dim TestCaseQL(0 To 0) As String
    Dim TestCaseNPL(0 To 0) As String
    Dim TestCaseNPS(0 To 0) As String
    Dim TestCaseOS(0 To 0) As Integer
    Dim myNumTC As Integer = 0

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
        Dim myReader As StreamReader
        For Each file In TestSetup.ofd_TestCase.FileNames
            Try
                myReader = New System.IO.StreamReader(file)
            Catch ex As Exception
                MsgBox("Could not Open test case file!")
                Exit Sub
            End Try
            'Dim  As StreamReader = New StreamReader(myFileStream)
            Dim numTestCases As Integer = 0
            While getSingleTestCase(myReader)
                'MsgBox("got a test case!")
            End While
        Next file
    End Sub
    Function getSingleTestCase(ByVal myreader As StreamReader) As Integer
        puzzleType = If(TestSetup.cb_Queen.Checked, "Q", "P")

        piecePositions = getNonCommentLine(myreader)
        If piecePositions = "END" Then
            Return 0
        End If

        If puzzleType = "Q" Then ' parse as 8 queens puzzle
            If piecePositions.Length > 8 Then
                MsgBox("You probably loaded a puzzle file. try again!")
                Return 0
            End If
            ReDim Preserve TestCaseQL(0 To myNumTC + 1)
            TestCaseQL(myNumTC) = piecePositions
            'MsgBox("added a test case! It is number " & myNumTC & " and has data: " & piecePositions)
        ElseIf puzzleType = "P" Then ' parse as 8 puzzle

            If piecePositions.Length <= 8 Then
                MsgBox("You probably loaded a queen file. try again!")
                Return 0
            End If
            ' 8 puzzle initial layout
            ReDim Preserve TestCaseNPL(0 To myNumTC + 1)
            TestCaseNPL(myNumTC) = piecePositions

            ' 8 puzzle goal state
            ReDim Preserve TestCaseNPS(0 To myNumTC + 1)
            TestCaseNPS(myNumTC) = getNonCommentLine(myreader)
        End If

        Dim optimalSolution = getNonCommentLine(myreader)

        If optimalSolution = "END" Then
            Return 0
        End If

        ReDim Preserve TestCaseOS(0 To myNumTC + 1)
        TestCaseOS(myNumTC) = optimalSolution

        myNumTC += 1
        Return 1
    End Function

    Sub StartTests(ByVal By)
        puzzleType = If(TestSetup.cb_Queen.Checked, "Q", "P")
        If puzzleType = "Q" Then
            StartNQueensTest()
        ElseIf puzzleType = "P" Then
            StartNPuzzleTest()
        End If
    End Sub

    Sub StartNPuzzleTest()
        For i = 0 To (TestCaseNPL.Length - 2)
            InitializeNPuzzle(TestCaseNPL(i), TestCaseNPS(i))

            TestSetup.tb_N.Text = TestCaseNPL(i).Length
            TestSetup.tb_OMC.Text = TestCaseOS(0)
            TestSetup.tb_CMC.Text = 0

            SolveNPuzzle()
        Next i

    End Sub
    Sub StartNQueensTest()

        For i = 0 To (TestCaseQL.Length - 2)
            'MsgBox(TestCaseQL(i) & "and there will be " & (TestCaseQL.Length - 1) & " test cases")
            SetupQueenslist(TestCaseQL(i))
            TestSetup.updateChessGrid(TestCaseQL(i))
            Delay(2)

            TestSetup.tb_N.Text = TestCaseQL(i).Length
            TestSetup.tb_OMC.Text = TestCaseOS(i)
            TestSetup.tb_CMC.Text = 0

            piecePositions = SolveNQueen()

            TestSetup.updateChessGrid(piecePositions)
            'MsgBox("Solved Puzzle " & i & " out of " & (TestCaseQL.Length - 2))
        Next i

    End Sub

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

    Public Sub clearData()
        ReDim Preserve TestCaseQL(0 To 0)
        ReDim Preserve TestCaseOS(0 To 0)
        myNumTC = 0
    End Sub

End Module
