Public Class TestSetup

    Public moveDelayEnabled As Boolean = True
    Dim images() As System.Drawing.Bitmap = New System.Drawing.Bitmap() {
        My.Resources.puzBlank,
        My.Resources.puz1,
        My.Resources.puz2,
        My.Resources.puz3,
        My.Resources.puz4,
        My.Resources.puz5,
        My.Resources.puz6,
        My.Resources.puz7,
        My.Resources.puz8
    }



    ' here, we can put anything we want to be enabled upon startup
    Private Sub TestSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clb_OptionsList.SetItemChecked(0, True)
    End Sub

    Private Sub btn_OpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OpenFile.Click
        ofd_TestCase.ShowDialog()


        ' if cb_Queen is checked get 8 Queens tests else 8 Puzzle tests
        fileParsingUtilities.getTestCases(If(cb_Queen.Checked, "Q", "P"))

        btn_StartTest.Enabled = True
    End Sub

    Private Sub btn_CloseWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_CloseWindow.Click
        Me.Dispose()
        Me.Close()
        End
    End Sub

    Private Sub btn_StartTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_StartTest.Click     
        If clb_OptionsList.CheckedItems.Count > 0 Then
            btn_StartTest.Enabled = False
            resetValues()
            DisableInput()
            StartTests(If(cb_Queen.Checked, "Q", "P"))
            enableInput()
            fileParsingUtilities.clearData()
            nQueenUtilities.clearData()
        Else
            MsgBox("Make sure you have at least 1 option checked")
        End If
    End Sub

    Public Sub updateChessGrid(ByVal newPositionString As String)
        Select Case newPositionString.Substring(0, 1)
            Case 1
                pb_chessA1.Image = My.Resources.whiteChessQueenBlack
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 2
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.whiteChessQueenRed
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 3
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.whiteChessQueenBlack
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 4
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.whiteChessQueenRed
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 5
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.whiteChessQueenBlack
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 6
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.whiteChessQueenRed
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 7
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.whiteChessQueenBlack
                pb_chessA8.Image = My.Resources.redChessSquare
            Case 8
                pb_chessA1.Image = My.Resources.blackChessSquare
                pb_chessA2.Image = My.Resources.redChessSquare
                pb_chessA3.Image = My.Resources.blackChessSquare
                pb_chessA4.Image = My.Resources.redChessSquare
                pb_chessA5.Image = My.Resources.blackChessSquare
                pb_chessA6.Image = My.Resources.redChessSquare
                pb_chessA7.Image = My.Resources.blackChessSquare
                pb_chessA8.Image = My.Resources.whiteChessQueenRed
        End Select
        Select Case newPositionString.Substring(1, 1)
            Case 1
                pb_chessB1.Image = My.Resources.whiteChessQueenRed
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 2
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.whiteChessQueenBlack
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 3
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.whiteChessQueenRed
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 4
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.whiteChessQueenBlack
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 5
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.whiteChessQueenRed
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 6
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.whiteChessQueenBlack
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 7
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.whiteChessQueenRed
                pb_chessB8.Image = My.Resources.blackChessSquare
            Case 8
                pb_chessB1.Image = My.Resources.redChessSquare
                pb_chessB2.Image = My.Resources.blackChessSquare
                pb_chessB3.Image = My.Resources.redChessSquare
                pb_chessB4.Image = My.Resources.blackChessSquare
                pb_chessB5.Image = My.Resources.redChessSquare
                pb_chessB6.Image = My.Resources.blackChessSquare
                pb_chessB7.Image = My.Resources.redChessSquare
                pb_chessB8.Image = My.Resources.whiteChessQueenBlack
        End Select
        Select Case newPositionString.Substring(2, 1)
            Case 1
                pb_chessC1.Image = My.Resources.whiteChessQueenBlack
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 2
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.whiteChessQueenRed
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 3
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.whiteChessQueenBlack
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 4
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.whiteChessQueenRed
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 5
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.whiteChessQueenBlack
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 6
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.whiteChessQueenRed
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 7
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.whiteChessQueenBlack
                pb_chessC8.Image = My.Resources.redChessSquare
            Case 8
                pb_chessC1.Image = My.Resources.blackChessSquare
                pb_chessC2.Image = My.Resources.redChessSquare
                pb_chessC3.Image = My.Resources.blackChessSquare
                pb_chessC4.Image = My.Resources.redChessSquare
                pb_chessC5.Image = My.Resources.blackChessSquare
                pb_chessC6.Image = My.Resources.redChessSquare
                pb_chessC7.Image = My.Resources.blackChessSquare
                pb_chessC8.Image = My.Resources.whiteChessQueenRed
        End Select
        Select Case newPositionString.Substring(3, 1)
            Case 1
                pb_chessD1.Image = My.Resources.whiteChessQueenRed
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 2
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.whiteChessQueenBlack
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 3
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.whiteChessQueenRed
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 4
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.whiteChessQueenBlack
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 5
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.whiteChessQueenRed
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 6
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.whiteChessQueenBlack
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 7
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.whiteChessQueenRed
                pb_chessD8.Image = My.Resources.blackChessSquare
            Case 8
                pb_chessD1.Image = My.Resources.redChessSquare
                pb_chessD2.Image = My.Resources.blackChessSquare
                pb_chessD3.Image = My.Resources.redChessSquare
                pb_chessD4.Image = My.Resources.blackChessSquare
                pb_chessD5.Image = My.Resources.redChessSquare
                pb_chessD6.Image = My.Resources.blackChessSquare
                pb_chessD7.Image = My.Resources.redChessSquare
                pb_chessD8.Image = My.Resources.whiteChessQueenBlack
        End Select
        Select Case newPositionString.Substring(4, 1)
            Case 1
                pb_chessE1.Image = My.Resources.whiteChessQueenBlack
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 2
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.whiteChessQueenRed
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 3
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.whiteChessQueenBlack
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 4
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.whiteChessQueenRed
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 5
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.whiteChessQueenBlack
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 6
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.whiteChessQueenRed
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 7
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.whiteChessQueenBlack
                pb_chessE8.Image = My.Resources.redChessSquare
            Case 8
                pb_chessE1.Image = My.Resources.blackChessSquare
                pb_chessE2.Image = My.Resources.redChessSquare
                pb_chessE3.Image = My.Resources.blackChessSquare
                pb_chessE4.Image = My.Resources.redChessSquare
                pb_chessE5.Image = My.Resources.blackChessSquare
                pb_chessE6.Image = My.Resources.redChessSquare
                pb_chessE7.Image = My.Resources.blackChessSquare
                pb_chessE8.Image = My.Resources.whiteChessQueenRed
        End Select
        Select Case newPositionString.Substring(5, 1)
            Case 1
                pb_chessF1.Image = My.Resources.whiteChessQueenRed
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 2
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.whiteChessQueenBlack
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 3
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.whiteChessQueenRed
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 4
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.whiteChessQueenBlack
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 5
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.whiteChessQueenRed
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 6
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.whiteChessQueenBlack
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 7
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.whiteChessQueenRed
                pb_chessF8.Image = My.Resources.blackChessSquare
            Case 8
                pb_chessF1.Image = My.Resources.redChessSquare
                pb_chessF2.Image = My.Resources.blackChessSquare
                pb_chessF3.Image = My.Resources.redChessSquare
                pb_chessF4.Image = My.Resources.blackChessSquare
                pb_chessF5.Image = My.Resources.redChessSquare
                pb_chessF6.Image = My.Resources.blackChessSquare
                pb_chessF7.Image = My.Resources.redChessSquare
                pb_chessF8.Image = My.Resources.whiteChessQueenBlack
        End Select
        Select Case newPositionString.Substring(6, 1)
            Case 1
                pb_chessG1.Image = My.Resources.whiteChessQueenBlack
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 2
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.whiteChessQueenRed
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 3
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.whiteChessQueenBlack
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 4
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.whiteChessQueenRed
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 5
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.whiteChessQueenBlack
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 6
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.whiteChessQueenRed
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 7
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.whiteChessQueenBlack
                pb_chessG8.Image = My.Resources.redChessSquare
            Case 8
                pb_chessG1.Image = My.Resources.blackChessSquare
                pb_chessG2.Image = My.Resources.redChessSquare
                pb_chessG3.Image = My.Resources.blackChessSquare
                pb_chessG4.Image = My.Resources.redChessSquare
                pb_chessG5.Image = My.Resources.blackChessSquare
                pb_chessG6.Image = My.Resources.redChessSquare
                pb_chessG7.Image = My.Resources.blackChessSquare
                pb_chessG8.Image = My.Resources.whiteChessQueenRed
        End Select
        Select Case newPositionString.Substring(7, 1)
            Case 1
                pb_chessH1.Image = My.Resources.whiteChessQueenRed
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 2
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.whiteChessQueenBlack
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 3
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.whiteChessQueenRed
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 4
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.whiteChessQueenBlack
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 5
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.whiteChessQueenRed
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 6
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.whiteChessQueenBlack
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 7
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.whiteChessQueenRed
                pb_chessH8.Image = My.Resources.blackChessSquare
            Case 8
                pb_chessH1.Image = My.Resources.redChessSquare
                pb_chessH2.Image = My.Resources.blackChessSquare
                pb_chessH3.Image = My.Resources.redChessSquare
                pb_chessH4.Image = My.Resources.blackChessSquare
                pb_chessH5.Image = My.Resources.redChessSquare
                pb_chessH6.Image = My.Resources.blackChessSquare
                pb_chessH7.Image = My.Resources.redChessSquare
                pb_chessH8.Image = My.Resources.whiteChessQueenBlack
        End Select
    End Sub

    Private Sub cb_Queen_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Queen.CheckedChanged
        If cb_Queen.Checked Then
            gb_ChessBoard.Visible = True
            tb_PuzzleType.Text = "Queen"
            If cb_Puzzle.Checked Then
                cb_Puzzle.Checked = False
            End If
        Else
            gb_ChessBoard.Visible = False
            tb_PuzzleType.Text = ""
        End If
    End Sub

    Private Sub cb_Puzzle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_Puzzle.CheckedChanged
        If cb_Puzzle.Checked Then
            gb_Puz.Visible = True
            tb_PuzzleType.Text = "Puzzle"
            If cb_Queen.Checked Then
                cb_Queen.Checked = False
            End If
        Else
            gb_Puz.Visible = False
            tb_PuzzleType.Text = ""
        End If
    End Sub

    Sub DisableInput()
        gb_LoadTestCase.Enabled = False
    End Sub

    Sub enableInput()
        gb_LoadTestCase.Enabled = True
    End Sub

    Sub resetValues()
        tb_CMC.Text = 0
        tb_CurrentH.Text = 0
        tb_N.Text = 0
        tb_OMC.Text = 0
    End Sub

    Private Sub cb_MoveDelay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_MoveDelay.CheckedChanged
        If cb_MoveDelay.Checked Then
            moveDelayEnabled = True
        Else
            movedelayEnabled = False
        End If
    End Sub

    Public Sub updateNPuzzleGrid(ByVal index As Integer, ByVal puzzlePiece As Integer)
        Select Case index
            Case 0
                pb_puzzle1.Image = images(puzzlePiece)
            Case 1
                pb_puzzle2.Image = images(puzzlePiece)
            Case 2
                pb_puzzle3.Image = images(puzzlePiece)
            Case 3
                pb_puzzle4.Image = images(puzzlePiece)
            Case 4
                pb_puzzle5.Image = images(puzzlePiece)
            Case 5
                pb_puzzle6.Image = images(puzzlePiece)
            Case 6
                pb_puzzle7.Image = images(puzzlePiece)
            Case 7
                pb_puzzle8.Image = images(puzzlePiece)
            Case 8
                pb_puzzle9.Image = images(puzzlePiece)
        End Select
    End Sub

    Private Sub PictureBox70_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pb_puzzle4.Click
        pb_chessH1.Image = My.Resources.redChessSquare
    End Sub
End Class
