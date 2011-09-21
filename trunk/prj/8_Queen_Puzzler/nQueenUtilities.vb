Module nQueenUtilities



    Dim myList(7) As Queen

    Sub SetupQueenslist(ByVal posString As String)
        Dim myQ As Queen
        For i = 0 To (posString.Length - 1)
            myQ = New Queen(i, posString.Substring(i, 1))
            myList(i) = myQ
        Next i
    End Sub

    Function SolveNQueen() As String
        Dim h As Integer = 0
        Dim myReturnString(7) As Char

        For i = 0 To (myList.Length - 1)
            h = computeQueensHNumber(myList(i))
            If (h > 0) Then
                MoveQueenInColumn(myList(i))
            End If
        Next i

        'now that we have looped through the whole board, update our solution to be displayed
        For i = 0 To (myList.Length - 1)
            myReturnString(i) = myList(i).Row.ToString
        Next i
        Return myReturnString
    End Function

    Function computeQueensHNumber(ByVal Queen1 As Queen) As Integer
        Dim returnHNumber As Integer = 0

        For i = 0 To (myList.Length - 1) ' 7 here should eventually be changed to n
            If computeCommonRowOrDiagonal(Queen1, myList(i)) <> 0 Then
                returnHNumber += 1
            End If
        Next i

        Return returnHNumber
    End Function

    Function computeCommonRowOrDiagonal(ByVal Queen1 As Queen, ByVal Queen2 As Queen) As Integer
        If (Queen1.Name <> Queen2.Name) Then
            If (Queen1.Row = Queen2.Row) Then
                Return 1 ' we have a common row
            ElseIf (Math.Abs(Queen1.Row - Queen2.Row) Mod 9 = 0) Or
                    (Math.Abs(Queen1.Row - Queen2.Row) Mod 7 = 0) Then
                Return 2 ' we have a common diagonal
            Else
                Return 0 ' we have nothing in common!
            End If
        Else
            Return 0
        End If
    End Function

    Sub MoveQueenInColumn(ByVal myQueen As Queen)

        Dim originalRow As Integer = myQueen.Row

        myQueen.Row += 1

        If myQueen.Row = 9 Then
            myQueen.Row = 1
        End If

        'TODO: get the name of the queen in column i here

        ' for to 7, the length of a column, put the queen in each spot. 
        '   then, calculate our H number.  if its lower, leave the queen there. if it isn't, go to the next spot
        ' if we've gone through all 8 spots, break!

    End Sub


End Module
