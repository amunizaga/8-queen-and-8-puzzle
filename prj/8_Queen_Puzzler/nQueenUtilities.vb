Module nQueenUtilities

    Public currentCycleCount As Integer = 0

    Dim myNumStates As Integer = 0

    Dim myList(7) As Queen

    Dim myPosStateArray(7)() As Queen

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
        myNumStates = 0
        Dim myReturnString(7) As Char
        Dim newReturnString(7) As Char
        Dim LastTotalH As Integer = 0
        Dim TotalH As Integer = 0
        Dim savedStates As Integer = 0
        Dim moveCounter As Integer = 0
        currentCycleCount = 0

        'this is the simulated annealing section
        If TestSetup.clb_OptionsList.GetItemChecked(0) Or TestSetup.clb_OptionsList.GetItemChecked(1) Then
            While (True)
                TotalH = 0

                For i = 0 To (myList.Length - 1)
                    h = computeQueensHNumber(myList(i))
                    If (h > 0) Then
                        Dim prevRow = myList(i).Row
                        MoveQueenInColumn(myList(i))
                        If prevRow <> myList(i).Row Then
                            moveCounter += Math.Abs(prevRow - myList(i).Row)

                        End If
                    End If
                Next i

                'now that we have looped through the whole board, update our solution to be displayed
                For i = 0 To (myList.Length - 1)
                    myReturnString(i) = myList(i).Row.ToString
                Next i

                For i = 0 To (myList.Length - 1)
                    TotalH += computeQueensHNumber(myList(i))
                    'MsgBox("Column " & i & " h val: " & computeQueensHNumber(myList(i)))
                Next i

                TotalH = (TotalH / 2) 'remove the 2-way duplication
                If TotalH = 0 Or currentCycleCount >= 4000 Then
                    Exit While
                Else
                    LastTotalH = TotalH
                End If

                If TestSetup.moveDelayEnabled Then
                    TestSetup.updateChessGrid(myReturnString)
                    TestSetup.tb_CurrentH.Text = TotalH
                    TestSetup.tb_CMC.Text = moveCounter
                    fileParsingUtilities.Delay(1)
                End If



            End While
        End If

        'here lies genetic algorithm operations
        If TestSetup.clb_OptionsList.GetItemChecked(2) Then

            TestSetup.DataGridView1.Visible = True

            'first, set up a society of 8 "starter" states. these will be totally random
            'but remember, theres no sense in moving a piece if its already at its h=0 state

            createFirstGeneration()

            populateDataGridView()

            'now we have a generation, lets loop until its perfect

            While (True)

                Dim firstRand = Random(0, 7) 'index to pull first parent from
                Dim secondRand = Random(0, 7) 'index to pull second parent from

                'we want to make sure the 2 parents aren't the same one (no asexual reproduction)
                While (secondRand = firstRand)
                    secondRand = Random(0, 7)
                End While

                Dim firstParent() As Queen = myPosStateArray(firstRand)
                Dim secondParent() As Queen = myPosStateArray(secondRand)

                'now create the child by putting togehter a random substring of genes from each parent
                Dim onlyChild() As Queen = reproduce(firstParent, secondParent)

                Dim mutatorProbability = Random(0, 1) 'a one in two chance of a mutation
                If mutatorProbability = 0 Then
                    mutateQueenList(onlyChild)
                End If

                'now calculate the child's value to this society
                Dim ChildsTotalH As Integer = 0
                For k = 0 To 7
                    ChildsTotalH += computeQueensHNumber(onlyChild(k))
                    newReturnString(k) = onlyChild(k).Row.ToString
                Next

                'now that we have our child's total H number, we will compare it to 
                'the h numbers in the list of states we currently have. if it is better
                'than any of them, well put in in its place and stop searching
                For l = 0 To 7
                    Dim compareTotalH As Integer = 0
                    For k = 0 To 7
                        compareTotalH += computeQueensHNumber(myPosStateArray(l)(k))
                    Next

                    'save the child into society if its better than what we have, throwing away its predecessor.
                    If (ChildsTotalH <= compareTotalH) Then
                        If l = 0 Then
                            'if we are looking at the first location in the array, then it is likely the best. put it right in.
                            myPosStateArray(l) = onlyChild
                            TestSetup.DataGridView1.Rows(l).Cells(1).Value = newReturnString(0) & newReturnString(1) & newReturnString(2) & newReturnString(3) & newReturnString(4) & newReturnString(5) & newReturnString(6) & newReturnString(7)
                            TestSetup.DataGridView1.Rows(l).Cells(2).Value = ChildsTotalH
                            If (ChildsTotalH = 0) Then
                                Exit While
                            End If
                            Exit For
                        ElseIf queenArrayEquals(onlyChild, myPosStateArray(l - 1)) = False Then
                            'at other spots in the array, we want to make sure we aren't putting in repeated data.
                            myPosStateArray(l) = onlyChild
                            TestSetup.DataGridView1.Rows(l).Cells(1).Value = newReturnString(0) & newReturnString(1) & newReturnString(2) & newReturnString(3) & newReturnString(4) & newReturnString(5) & newReturnString(6) & newReturnString(7)
                            TestSetup.DataGridView1.Rows(l).Cells(2).Value = ChildsTotalH
                            If (ChildsTotalH = 0) Then
                                Exit While
                            End If
                            Exit For
                        End If
                    End If

                    If TestSetup.moveDelayEnabled Then
                        TestSetup.updateChessGrid(newReturnString)
                        TestSetup.tb_CurrentH.Text = ChildsTotalH
                        fileParsingUtilities.Delay(1)
                    End If

                Next

                'MsgBox("pausing!")
                'fileParsingUtilities.Delay(1)

            End While
        End If
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
        If myRand > currentCycleCount And TestSetup.clb_OptionsList.GetItemChecked(1) Then

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
            If myQueen.Row >= 9 Then
                myQueen.Row = 1
            End If
            If myQueen.Row <= 0 Then
                myQueen.Row = 8
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

    Sub randomlyMoveQueenInColumn(ByVal myQueen As Queen)

        Dim mynewRand = Random(1, 8)

        myQueen.Row = mynewRand

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

    Function reproduce(ByVal dad As Queen(), ByVal mom As Queen()) As Queen()

        Dim myDadString(7) As Char
        Dim myMomString(7) As Char
        Dim myKidString(7) As Char

        Dim child(dad.Length - 1) As Queen

        Dim geneticRandomIndex = Random(1, 7)
        For i = 0 To geneticRandomIndex
            child(i) = dad(i)
        Next
        For i = geneticRandomIndex To dad.Length - 1
            child(i) = mom(i)
        Next

        For i = 0 To (myDadString.Length - 1)
            myDadString(i) = dad(i).Row.ToString
        Next i

        For i = 0 To (myMomString.Length - 1)
            myMomString(i) = mom(i).Row.ToString
        Next i

        For i = 0 To (myKidString.Length - 1)
            myKidString(i) = child(i).Row.ToString
        Next i

        'MsgBox("Dad: " & myDadString & " Mom: " & myMomString & " child: " & myKidString)

        Return child
    End Function

    Sub mutateQueenList(ByVal listToMutate As Queen())
        Dim indexForMutation = Random(0, listToMutate.Length - 1)
        Dim otherIndexForMutation = Random(0, listToMutate.Length - 1)
        While otherIndexForMutation = indexForMutation
            otherIndexForMutation = Random(0, listToMutate.Length - 1)
        End While
        randomlyMoveQueenInColumn(listToMutate(indexForMutation))
        randomlyMoveQueenInColumn(listToMutate(otherIndexForMutation))

    End Sub

    Function queenArrayEquals(ByVal queen1 As Queen(), ByVal queen2 As Queen()) As Boolean
        Dim x As Boolean = True
        For i = LBound(queen1) To UBound(queen1)
            If (queen1(i).Row <> queen2(i).Row) Or (queen1(i).Name <> queen2(i).Name) Then
                x = False
                Exit For
            End If
        Next i
        If x = True Then
            Return True
        Else
            Return False
        End If
    End Function


    Sub queenOptimal(ByVal queen As String)
        Dim queenSolutions(92) As String
        Dim i As Integer
        Dim j As Integer
        Dim optimalSolution As Integer
        Dim temp As Integer
        Dim q1 As Queen
        Dim q2 As Queen
        Dim qList(7) As Queen

        queenSolutions = {"17582463", "17468253", "16837425", "15863724", "61528374", "41582736", "51842736", "31758246", "51468273", "71386425",
                          "51863724", "41586372", "26174835", "53172864", "83162574", "46152837", "57142863", "63184275", "53168247", "63185247",
                          "48136275", "84136275", "48157263", "47185263", "64158273", "63175824", "73168524", "57138642", "25713864", "28613574",
                          "62713584", "72418536", "82417536", "52814736", "62714853", "52617483", "35714286", "64718253", "58417263", "36815724",
                          "75316824", "64713528", "58413627", "36418572", "36814752", "57413862", "26831475", "25741863", "27581463", "72631485",
                          "82531746", "42861357", "42751863", "36271485", "35281746", "68241753", "38471625", "35841726", "63741825", "63571428",
                          "63581427", "48531726", "47531682", "46831752", "24683175", "42586137", "42857136", "37285146", "36258174", "36275184",
                          "57263148", "57263184", "74258136", "74286135", "57248136", "36824175", "73825164", "47526138", "46827135", "53847162",
                          "27368514", "42736815", "52468317", "37286415", "64285713", "63724815", "63728514", "47382516", "42736851", "52473861",
                          "35286471", "36428571"}


        optimalSolution = 0

        For i = 0 To 91
            Dim myQueenName As String = "A"
            For j = 0 To (queenSolutions(i).Length - 1)
                q2 = New Queen(myQueenName, queenSolutions(i).Substring(j, 1))
                qList(j) = q2
                myQueenName = Chr(Asc(myQueenName) + 1)
            Next j
            temp = 0
            For k = 0 To 7
                temp += Math.Abs(myList(k).Row - qList(k).Row)
            Next k
            If (temp < optimalSolution Or i = 0) Then
                TestSetup.myBestGoalState = queenSolutions(i)
                optimalSolution = temp
            End If
        Next
        TestSetup.tb_calc_omc.Text = optimalSolution
    End Sub

    Sub createFirstGeneration()
        Dim h As Integer = 0
        myNumStates = 0
        Dim myReturnString(7) As Char
        myNumStates = 0
        While (myNumStates < 8)

            For i = 0 To (myList.Length - 1)
                h = computeQueensHNumber(myList(i))
                If (h > 0) Then
                    Dim prevRow = myList(i).Row
                    randomlyMoveQueenInColumn(myList(i))
                End If
            Next i

            For i = 0 To (myList.Length - 1)
                myReturnString(i) = myList(i).Row.ToString
            Next i

            Dim myNewList As Queen() = myList

            For i = 0 To myList.Length - 1
                ReDim Preserve myPosStateArray(0 To myNumStates)(0 To i)
                myPosStateArray(myNumStates)(i) = New Queen(myList(i).Name, myList(i).Row)
            Next

            myNumStates += 1
        End While
    End Sub

    Sub populateDataGridView()

        Dim TotalH As Integer
        Dim newReturnString(7) As Char

        TestSetup.DataGridView1.Rows.Add(8)
        For i = 0 To 7 ' search through each creature in our society
            'now we have the information for this generation, lets present it in the data grid view
            TestSetup.DataGridView1.Rows(i).Cells(0).Value = i

            TotalH = 0

            For k = 0 To 7

                TotalH += computeQueensHNumber(myPosStateArray(i)(k))
                newReturnString(k) = myPosStateArray(i)(k).Row.ToString
            Next
            TestSetup.DataGridView1.Rows(i).Cells(1).Value = newReturnString(0) & newReturnString(1) & newReturnString(2) & newReturnString(3) & newReturnString(4) & newReturnString(5) & newReturnString(6) & newReturnString(7)
            TestSetup.DataGridView1.Rows(i).Cells(2).Value = TotalH

        Next
    End Sub
End Module
