Module nPuzzleUtilities

    Dim board() As Integer
    Dim goal() As Integer
    Public spaceIndex As Integer = 0
    Public boardNumber As Integer = 0
    Public moveCount As Integer = 0
    Enum moves
        left = 0
        right = 1
        up = 2
        down = 3
    End Enum
    Dim spaceVal As Integer

    Function SolveNPuzzle()
        Delay(2)
        nPuzzleUtilities.MovePuzzlePiece(moves.left)
        Delay(2)
        nPuzzleUtilities.MovePuzzlePiece(moves.up)
        Delay(2)
        nPuzzleUtilities.MovePuzzlePiece(moves.right)
        Delay(2)
        nPuzzleUtilities.MovePuzzlePiece(moves.down)
        Delay(2)
        nPuzzleUtilities.MovePuzzlePiece(moves.down)
        Delay(2)
        Return 0
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

    Sub MovePuzzlePiece(ByVal direction As String)
        Dim spaceVal As Integer = spaceIndex
        Select Case direction
            Case 0 'move space left
                If spaceIndex Mod 3 <> 0 Then
                    SwapPieces(spaceIndex - 1)
                    spaceIndex = spaceIndex - 1
                End If
            Case 1 'move space right
                If spaceIndex Mod 3 <> 2 Then
                    SwapPieces(spaceIndex + 1)
                    spaceIndex = spaceIndex + 1
                End If
            Case 2 'move space up
                If spaceIndex > 2 Then
                    SwapPieces(spaceIndex - 3)
                    spaceIndex = spaceIndex - 3
                End If
            Case 3 'move space down
                If spaceIndex < 6 Then
                    SwapPieces(spaceIndex + 3)
                    spaceIndex = spaceIndex + 3
                End If
        End Select

        If spaceVal <> spaceIndex Then
            TestSetup.tb_CMC.Text += 1
        End If
    End Sub

    Sub SwapPieces(ByVal pieceIndex As Integer)
        board(spaceIndex) = board(pieceIndex)
        board(pieceIndex) = 0
        TestSetup.updateNPuzzleGrid(spaceIndex, board(spaceIndex))
        TestSetup.updateNPuzzleGrid(pieceIndex, board(pieceIndex))
    End Sub

End Module
