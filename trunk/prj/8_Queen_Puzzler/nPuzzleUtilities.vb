Module nPuzzleUtilities

    Dim board() As Integer
    Dim goal() As Integer
    Dim moveString As String = ""

    Private spaceIndex As Integer = 0
    Public boardNumber As Integer = 0
    Private currentH As Integer = 0
    Private inverseOfPreviousAction As Integer = -1
    Private delayTime As Integer = 1
    Private moveCount As Integer = 0
    Private maxMoveCount As Integer = 4000

    Dim t As Double = 1000
    Dim deltaE As Double = 0.0
    Enum moves
        left = 0
        right = 1
        up = 2
        down = 3
    End Enum

    Function SolveNPuzzle()
        'CheckForCycle()
        Dim nextAction As Integer
        Dim actionStr As String = ""
        Delay(2)

        ' calculate and display the h value of the initial state
        currentH = HeuristicFunction(-1)
        TestSetup.tb_CurrentH.Text = currentH

        While Not GoalSatisfied() And moveCount < maxMoveCount
            If TestSetup.moveDelayEnabled Then
                Delay(delayTime)
            End If

            If TestSetup.clb_OptionsList.GetItemChecked(0) Then
                nextAction = SelectActionWithStandardHillClimbing()
            ElseIf TestSetup.clb_OptionsList.GetItemChecked(1) Then
                nextAction = SelectActionWithFirstChoiceHillClimbing()
            ElseIf TestSetup.clb_OptionsList.GetItemChecked(2) Then
                nextAction = SelectActionWithSimulatedAnnealing()
            ElseIf TestSetup.clb_OptionsList.GetItemChecked(3) Then
                nextAction = SelectAction()
            End If
            If (IsStuck(nextAction)) Then
                Exit While
            End If
            moveString = String.Concat(moveString, nextAction.ToString())
            SetInverseOfPreviousAction(nextAction)
            MovePuzzlePiece(nextAction)
        End While

        TestSetup.tb_calc_omc.Text = T
        ' let the user view the board
        Delay(2)

        ' get ready for next test
        moveString = ""
        moveCount = 0

        t = 1000

        Return 0
    End Function

    Function IsStuck(ByVal nextAction As Integer)
        If nextAction = -1 Then
            MsgBox("No better states to move to!")
            Return True
            'ElseIf Not TestSetup.clb_OptionsList.GetItemChecked(3) And CheckForCycle() Then
            '    MsgBox("Caught in an infinite loop!")
            '    Return True
        End If
        Return False
    End Function

    Function CheckForCycle()
        Dim cycle1 As String = ""
        Dim cycle2 As String = ""

        ' not enough moves for a cycle
        If moveString.Length < 16 Then
            Return False
        End If

        ' look for a cycle of 6 moves
        cycle1 = moveString.Substring(0, 6)
        cycle2 = moveString.Substring(6, 6)

        If cycle1 = cycle2 Then
            Return True
        End If

        ' look for a cycle of 8 moves
        cycle1 = moveString.Substring(0, 8)
        cycle2 = moveString.Substring(8, 8)

        If cycle1 = cycle2 Then
            Return True
        End If

        'truncate the first character to make room for the next move
        moveString = moveString.Substring(1, 15)

        Return False
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
        board = New Integer(initialState.Length - 1) {}
        goal = New Integer(initialState.Length - 1) {}
        For i = 0 To (initialState.Length - 1)
            board(i) = Integer.Parse(initialState.Substring(i, 1))
            TestSetup.updateNPuzzleGrid(i, board(i))
            If board(i) = 0 Then
                spaceIndex = i
            End If
            goal(i) = Integer.Parse(goalState.Substring(i, 1))
        Next i
    End Sub

    Function SelectAction()
        Dim betterResult As Integer = -1
        Dim unchangedResults() As Integer = New Integer() {}
        Dim worseResults() As Integer = New Integer() {}

        ' evaluate each of up to 3 possible moves
        For i = 0 To 3
            If i <> inverseOfPreviousAction And isMoveValid(i) Then
                Select Case EvaluateMove(GetIndexOfPieceToMove(i))
                    Case 1 ' moves a piece into its goal position
                        betterResult = i
                    Case 0 ' moves piece neither in or out of goal position
                        ReDim Preserve unchangedResults(0 To UBound(unchangedResults) + 1)
                        unchangedResults(UBound(unchangedResults)) = i
                    Case -1 ' moves piece out of goal position
                        ReDim Preserve worseResults(0 To UBound(worseResults) + 1)
                        worseResults(UBound(worseResults)) = i
                End Select
            End If
        Next

        If betterResult > -1 Then
            Return betterResult
        ElseIf unchangedResults.Length > 0 Then
            If unchangedResults.Length = 1 Then
                'return the only uphill result
                Return unchangedResults(0)
            End If
            Return unchangedResults(Random(0, unchangedResults.Length - 1))
            'Dim bestUnchanged As Integer = -1

        ElseIf worseResults.Length > 1 Then
            Return worseResults(Random(0, worseResults.Length - 1))
        End If

        Return worseResults(0)

    End Function
    Function SelectActionWithStandardHillClimbing()
        Dim bestIndex As Integer = 0
        Dim h As Integer = -1
        Dim unchangedResults() As Integer = New Integer() {}
        Dim results() As Integer = {19, 19, 19, 19}

        ' evaluate each of up to 3 possible moves
        For i = 0 To 3
            ' don't move back to where we were or consider invalid moves (i.e. left in the leftmost column)
            If i <> inverseOfPreviousAction And isMoveValid(i) Then
                results(i) = HeuristicFunction(GetIndexOfPieceToMove(i))
                If i > 0 Then
                    ' here we store the heuristic values for each move
                    If results(i) < results(bestIndex) Then
                        bestIndex = i
                    End If
                End If
            End If
        Next

        ' NOTE: Gets stuck almost immediately with test against current H value so comment it out
        '' return the best uphill move
        'If results(bestIndex) < currentH Then
        '    Return bestIndex
        'End If

        '' uh oh, we're stuck!
        'Return -1


        ' if multiple equal best moves were found, the first one found is returned
        Return bestIndex

    End Function

    Function SelectActionWithFirstChoiceHillClimbing()
        Dim bestIndex As Integer = -1
        Dim h As Integer = -1
        Dim possibleMoveChoices As New List(Of Integer)

        ' evaluate each of up to 3 possible moves
        For i = 0 To 3
            ' don't move back to where we were or consider invalid moves (i.e. left in the leftmost column)
            If i <> inverseOfPreviousAction And isMoveValid(i) Then
                possibleMoveChoices.Add(i)
            End If
        Next

        ' randomly select a move, check if its h value is better than the current state, if so make that move
        Dim randomIndex As Integer
        While possibleMoveChoices.Count > 0
            randomIndex = Random(0, possibleMoveChoices.Count - 1)
            If HeuristicFunction(GetIndexOfPieceToMove(possibleMoveChoices(randomIndex))) > currentH Then
                Return possibleMoveChoices.Item(randomIndex)
            End If
            ' the h value of this choice was not better than currentH, so remove it to prevent infinite looping
            possibleMoveChoices.RemoveAt(randomIndex)
        End While

        ' NOTE: Gets stuck almost immediately with test against current H value so comment it out
        '' return the best uphill move
        'If results(bestIndex) < currentH Then
        '    Return bestIndex
        'End If

        ' none of the neighbors had a better h value, uh oh, we're stuck!
        Return -1

    End Function

    Function SelectActionWithSimulatedAnnealing()
        Dim h As Integer = -1
        Dim possibleMoveChoices As New List(Of Integer)

        ' evaluate each of up to 3 possible moves
        For i = 0 To 3
            ' don't move back to where we were or consider invalid moves (i.e. left in the leftmost column)
            If i <> inverseOfPreviousAction And isMoveValid(i) Then
                possibleMoveChoices.Add(i)
            End If
        Next
        Dim num As Integer = 0
        Dim probability As Double = 0
        Dim randomIndex As Integer

        While True

            'If t <= 0 Then
            '    Return -1 ' to return the current position would mean we are stuck so just return -1 and be done
            'End If

            randomIndex = Random(0, possibleMoveChoices.Count - 1)
            h = HeuristicFunction(GetIndexOfPieceToMove(possibleMoveChoices(randomIndex)))
            deltaE = h - currentH

            If deltaE >= 0 Then
                Exit While
            Else
                num = Random(0, 1000)
                'Math.Pow(Math.E, deltaE / t)
                If num < t Then
                    t -= 0.5
                    Exit While
                End If
            End If

        End While

        currentH = h
        Return possibleMoveChoices.Item(randomIndex)

    End Function

    Function HeuristicFunction(ByVal pieceIndex As Integer)

        ' make a temporary copy of the board state
        Dim tempBoard() As Integer = New Integer(board.Length - 1) {}
        For i = 0 To board.Length - 1
            tempBoard(i) = board(i)
        Next

        ' if not checking the h of the initial state
        If pieceIndex > -1 Then
            ' we need to swap the pieces to get to the new state
            tempBoard(spaceIndex) = tempBoard(pieceIndex)
            tempBoard(pieceIndex) = 0
        End If

        ' generate the heuristic values
        Dim row As Integer = 0
        Dim col As Integer = 0
        Dim rowCount As Integer = 0
        Dim colCount As Integer = 0
        Dim h As Integer = 0
        For i = 0 To tempBoard.Length - 1
            If i < 3 Then
                row = 0
            ElseIf i > 2 And i < 6 Then
                row = 1
            Else
                row = 2
            End If
            col = i Mod 3

            h += GetRowValue(row, tempBoard(i)) + GetColValue(col, tempBoard(i))
        Next
        Return h
    End Function
    ' Return 1 if piece is in correct row or 0 if not
    Function GetRowValue(ByVal row As Integer, ByVal number As Integer)
        Dim gNum As Integer = 0
        For i = row * 3 To row * 3 + 2
            ' check if piece is in the goal position
            gNum = goal(i)
            If number = gNum Then
                Return 1
            End If
        Next
        Return 0
    End Function
    ' Return 0 if piece is in column or 1 if not
    Function GetColValue(ByVal col As Integer, ByVal number As Integer)
        For i = 0 To goal.Length - 1
            Select Case col
                Case 0
                    If i Mod 3 = 0 And number = goal(i) Then
                        Return 1
                    End If
                Case 1
                    If i Mod 3 = 1 And number = goal(i) Then
                        Return 1
                    End If
                Case 2
                    If i Mod 3 = 2 And number = goal(i) Then
                        Return 1
                    End If
            End Select
        Next
        Return 0
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
        moveCount += 1

        TestSetup.tb_CMC.Text = moveCount
    End Sub

    Sub SwapPieces(ByVal pieceIndex As Integer)
        board(spaceIndex) = board(pieceIndex)
        board(pieceIndex) = 0
        TestSetup.updateNPuzzleGrid(spaceIndex, board(spaceIndex))
        TestSetup.updateNPuzzleGrid(pieceIndex, board(pieceIndex))
    End Sub

End Module
