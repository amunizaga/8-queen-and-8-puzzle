Module nPuzzleUtilities

    Dim board() As Integer
    Dim goal() As Integer
    Private spaceIndex As Integer = 0
    Public boardNumber As Integer = 0
    Private inverseOfPreviousAction As Integer = -1

    Enum moves
        left = 0
        right = 1
        up = 2
        down = 3
    End Enum

    Function SolveNPuzzle()
        Dim nextAction As Integer
        While Not GoalSatisfied()
            Delay(2)
            nextAction = SelectNextAction()
            SetInverseOfPreviousAction(nextAction)
            MovePuzzlePiece(nextAction)
        End While

        Delay(30)

        Return 0
    End Function
    Sub SetInverseOfPreviousAction(ByVal action As Integer)
        Select Case action
            Case moves.left
                inverseOfPreviousAction = moves.right
            Case moves.right
                inverseOfPreviousAction = moves.left
            Case moves.up
                inverseOfPreviousAction = moves.down
            Case moves.down
                inverseOfPreviousAction = moves.up
        End Select
    End Sub
    Function GoalSatisfied()
        For i = 0 To board.Length - 1
            If board(i) <> goal(i) Then
                Return False
            End If
        Next
        Return True
    End Function

    Sub InitializeNPuzzle(ByVal initialState As String, ByVal goalState As String)
        board = New Integer(initialState.Length) {}
        goal = New Integer(initialState.Length) {}
        For i = 0 To (initialState.Length - 1)
            board(i) = Integer.Parse(initialState.Substring(i, 1))
            TestSetup.updateNPuzzleGrid(i, board(i))
            If board(i) = 0 Then
                spaceIndex = i
            End If
            goal(i) = Integer.Parse(goalState.Substring(i, 1))
        Next i
    End Sub

    Function SelectNextAction()
        Dim betterResults() As Integer = New Integer() {}
        Dim unchangedResults() As Integer = New Integer() {}
        Dim worseResults() As Integer = New Integer() {}

        ' evaluate each of up to 3 possible moves
        For i = 0 To 3
            If i <> inverseOfPreviousAction And isMoveValid(i) Then
                Select Case EvaluateMove(GetIndexOfPieceToMove(i))
                    Case 1 ' moves a piece into its goal position
                        ReDim Preserve betterResults(0 To UBound(betterResults) + 1)
                        betterResults(UBound(betterResults)) = i
                    Case 0 ' moves piece neither in or out of goal position
                        ReDim Preserve unchangedResults(0 To UBound(unchangedResults) + 1)
                        unchangedResults(UBound(unchangedResults)) = i
                    Case -1 ' moves piece out of goal position
                        ReDim Preserve worseResults(0 To UBound(worseResults) + 1)
                        worseResults(UBound(worseResults)) = i
                End Select
            End If
        Next

        If betterResults.Length > 0 Then
            If betterResults.Length = 1 Then
                'return the only uphill result
                Return betterResults(0)
            End If
            Return betterResults(Random(0, betterResults.Length - 1))
        ElseIf unchangedResults.Length > 0 Then
            If unchangedResults.Length = 1 Then
                'return the only uphill result
                Return unchangedResults(0)
            End If
            Return unchangedResults(Random(0, unchangedResults.Length - 1))
        ElseIf worseResults.Length > 1 Then
            Return worseResults(Random(0, worseResults.Length - 1))
        End If

        Return worseResults(0)

    End Function

    Function EvaluateMove(ByVal index As Integer)
        ' result of 1 means the piece moved will end up in its goal state
        ' result of -1 means the piece moved will end up out of its goal state
        ' result of 0 does neither
        If board(index) = goal(spaceIndex) Then
            Return 1
        ElseIf board(index) = goal(index) Then
            Return -1
        End If
        Return 0
    End Function

    Function GetIndexOfPieceToMove(ByVal direction As Integer)
        Dim index As Integer
        Select Case direction
            Case 0
                index = spaceIndex - 1
            Case 1
                index = spaceIndex + 1
            Case 2
                index = spaceIndex - 3
            Case 3
                index = spaceIndex + 3
        End Select
        Return index
    End Function

    Function isMoveValid(ByVal direction As Integer)
        Select Case direction
            Case 0 'move space left
                Return spaceIndex Mod 3 <> 0
            Case 1 'move space right
                Return spaceIndex Mod 3 <> 2
            Case 2 'move space up
                Return spaceIndex > 2
            Case 3 'move space down
                Return spaceIndex < 6
        End Select
        Return False
    End Function

    Sub MovePuzzlePiece(ByVal direction As String)
        Select Case direction
            Case 0 'move space left
                SwapPieces(spaceIndex - 1)
                spaceIndex = spaceIndex - 1
            Case 1 'move space right
                SwapPieces(spaceIndex + 1)
                spaceIndex = spaceIndex + 1
            Case 2 'move space up
                SwapPieces(spaceIndex - 3)
                spaceIndex = spaceIndex - 3
            Case 3 'move space down
                SwapPieces(spaceIndex + 3)
                spaceIndex = spaceIndex + 3
        End Select

        TestSetup.tb_CMC.Text += 1
    End Sub

    Sub SwapPieces(ByVal pieceIndex As Integer)
        board(spaceIndex) = board(pieceIndex)
        board(pieceIndex) = 0
        TestSetup.updateNPuzzleGrid(spaceIndex, board(spaceIndex))
        TestSetup.updateNPuzzleGrid(pieceIndex, board(pieceIndex))
    End Sub

End Module
