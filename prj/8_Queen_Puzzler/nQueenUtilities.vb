﻿Module nQueenUtilities

    Public currentCycleCount As Integer = 0

    Dim myList(7) As Queen

    Public Sub clearData()
        currentCycleCount = 0
        For i = 0 To myList.Length - 1
            myList(i) = New Queen("A", 1)
        Next
    End Sub

    Sub SetupQueenslist(ByVal posString As String)
        Dim myQ As Queen
        Dim myQueenName As String = "A"
        For i = 0 To (posString.Length - 1)
            myQ = New Queen(myQueenName, posString.Substring(i, 1))
            myList(i) = myQ
            myQueenName = Chr(Asc(myQueenName) + 1)
        Next i
    End Sub

    Function SolveNQueen() As String
        Dim h As Integer = 0
        Dim myReturnString(7) As Char
        Dim LastTotalH As Integer = 0
        Dim TotalH As Integer = 0
        While (True)
            TotalH = 0

            For i = 0 To (myList.Length - 1)
                h = computeQueensHNumber(myList(i))
                If (h > 0) Then
                    Dim prevRow = myList(i).Row
                    MoveQueenInColumn(myList(i))
                    If prevRow <> myList(i).Row Then
                        TestSetup.tb_CMC.Text += 1
                    End If
                End If
            Next i

            'now that we have looped through the whole board, update our solution to be displayed
            For i = 0 To (myList.Length - 1)
                myReturnString(i) = myList(i).Row.ToString
            Next i

            TestSetup.updateChessGrid(myReturnString)
            If TestSetup.moveDelayEnabled Then
                fileParsingUtilities.Delay(1)
            End If

            For i = 0 To (myList.Length - 1)
                TotalH += computeQueensHNumber(myList(i))
                'MsgBox("Column " & i & " h val: " & computeQueensHNumber(myList(i)))
            Next i

            TotalH = (TotalH / 2) 'remove the 2-way duplication
            TestSetup.tb_CurrentH.Text = TotalH
            'MsgBox("End of Round's Total H: " & TotalH)
            If TotalH = 0 Then '  Or TotalH = LastTotalH
                Exit While
            Else
                LastTotalH = TotalH
            End If
        End While
        'MsgBox("Solved!")
        Return myReturnString
    End Function

    Function computeQueensHNumber(ByVal Queen1 As Queen) As Integer
        Dim returnHNumber As Integer = 0

        For i = 0 To (myList.Length - 1) ' 7 here should eventually be changed to n
            If computeCommonRowOrDiagonal(Queen1, myList(i)) <> 0 Then
                returnHNumber += 1
            End If
        Next i
        'MsgBox("H for Queen " & Queen1.Name & " is: " & returnHNumber)

        Return returnHNumber
    End Function

    Function computeCommonRowOrDiagonal(ByVal Queen1 As Queen, ByVal Queen2 As Queen) As Integer
        If (Queen1.Name <> Queen2.Name) Then
            If (Queen1.Row = Queen2.Row) Then
                Return 1 ' we have a common row
            ElseIf (Math.Abs(Asc(Queen1.Name) - Asc(Queen2.Name)) = Math.Abs(Queen1.Row - Queen2.Row)) Then
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

        Dim myOriginalH As Integer = computeQueensHNumber(myQueen)

        Dim myRand = Random(0, 1000)
        If myRand > currentCycleCount Then

            Dim mynewRand = Random(myQueen.Row + 1, 8)
            'MsgBox("Sending Queen " & myQueen.Name & " to row " & mynewRand)
            myQueen.Row = mynewRand
            Dim myNewH As Integer = computeQueensHNumber(myQueen)
            If myNewH >= myOriginalH Then
                mynewRand = Random(1, myQueen.Row - 1)
                myQueen.Row = mynewRand
                myNewH = computeQueensHNumber(myQueen)
                If myNewH > myOriginalH Then
                    myQueen.Row = originalRow
                End If
            End If
        Else
            myQueen.Row += 1

            If myQueen.Row >= 9 Then
                myQueen.Row = 1
            End If

            Dim myNewH As Integer = computeQueensHNumber(myQueen)

            If myNewH >= myOriginalH Then
                myQueen.Row -= 2
                If myQueen.Row <= 0 Then
                    myQueen.Row = 8
                End If
                myNewH = computeQueensHNumber(myQueen)
                If myNewH > myOriginalH Then
                    myQueen.Row = originalRow
                End If
            End If
        End If
        currentCycleCount += 1

    End Sub

    Function Random(ByVal Lowerbound As Long, ByVal Upperbound As Long)
        Randomize()
        Random = CInt(Rnd() * (Upperbound - Lowerbound)) + Lowerbound
    End Function

    Function WeightedRandom(ByVal Lowerbound As Long, ByVal Upperbound As Long)
        Dim temp As Single
        Randomize()
        temp = Rnd()
        temp = temp * temp
        WeightedRandom = CInt(temp * (Upperbound - Lowerbound)) + Lowerbound
    End Function

End Module
