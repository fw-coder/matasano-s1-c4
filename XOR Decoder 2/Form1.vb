Public Class Form1

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ' Dim inputBytes As New List(Of String)()  ' this will be a list of bytes from the input textbox
 
        Dim commonPairs(0 To 27) As String
        Dim commonTriplets(0 To 6) As String
        commonPairs(0) = "TH"
        commonPairs(1) = "EA"
        commonPairs(2) = "OF"
        commonPairs(3) = "TO"
        commonPairs(4) = "IN"
        commonPairs(5) = "IT"
        commonPairs(6) = "IS"
        commonPairs(7) = "BE"
        commonPairs(8) = "AS"
        commonPairs(9) = "AT"
        commonPairs(10) = "SO"
        commonPairs(11) = "WE"
        commonPairs(12) = "HE"
        commonPairs(13) = "BY"
        commonPairs(14) = "OR"
        commonPairs(15) = "ON"
        commonPairs(16) = "DO"
        commonPairs(17) = "IF"
        commonPairs(18) = "ME"
        commonPairs(19) = "MY"
        commonPairs(20) = "UP"
        commonPairs(21) = "SS"
        commonPairs(22) = "EE"
        commonPairs(23) = "TT"
        commonPairs(24) = "FF"
        commonPairs(25) = "LL"
        commonPairs(26) = "MM"
        commonPairs(27) = "OO"
        commonTriplets(0) = "THE"
        commonTriplets(1) = "EST"
        commonTriplets(2) = "FOR"
        commonTriplets(3) = "AND"
        commonTriplets(4) = "HIS"
        commonTriplets(5) = "ENT"
        commonTriplets(6) = "THA"

        ' Insert a leading 0 if there is an odd number of characters in the input byte string
        ' Dim stringData1 As String = ListBox1.SelectedItem.ToString 'selected string in listbox

        ProgressBar2.Maximum = ListBox1.Items.Count - 1

        For l_index As Integer = 0 To ListBox1.Items.Count - 1
            Dim stringData1 As String = CStr(ListBox1.Items(l_index))
            Dim topScore1 As Integer = 0
            Dim topScore2 As Integer = 0
            Dim topScore3 As Integer = 0
            Dim topScoreResultString1 As String = String.Empty
            Dim topScoreResultString2 As String = String.Empty
            Dim topScoreResultString3 As String = String.Empty
            Dim topScoreResultKey1 As Integer = 0
            Dim topScoreResultKey2 As Integer = 0
            Dim topScoreResultKey3 As Integer = 0

            ProgressBar2.Value = l_index

            If stringData1.Length Mod 2 = 1 Then
                stringData1 = "0" & stringData1
            End If

            'initializing textBox2
            'TextBox3.Text = "Results:" & Environment.NewLine & Environment.NewLine

            For t As Byte = &H0 To &HFF
                Dim resultString As String = String.Empty ' use to hold string for frequency analysis
                Dim skipFreqAnalysis As Integer = 0 ' set to 1 if string/key combo has bad characters so freq analysis will be skipped

                ProgressBar1.Value = t

                'Dim testByte As String = Convert.ToString(t, 2)
                'While testByte.Length < 8
                '    testByte = "0" & testByte 'make sure testByte is an 8-bit binary string
                'End While
                ''''Dim testByte As Byte = Byte.Parse(t, Globalization.NumberStyles.HexNumber)
                Dim testByte As Byte = t


                ' ******TextBox3.Text = TextBox3.Text & "Key = " & t & Environment.NewLine


                For i As Integer = 0 To stringData1.Length - 1 Step 2
                    ' Convert each pair of characters to a byte
                    Dim currentByte1 As String = stringData1.Substring(i, 2) ' currentByte is two hex values
                    Dim value1 As Byte = Byte.Parse(currentByte1, Globalization.NumberStyles.HexNumber) 'value is hex byte
                    Dim tempByte As Byte = value1 Xor testByte

                    'Dim bin1 As String = Convert.ToString(value1, 2) 'conv value to binary
                    'While bin1.Length < 8
                    '    bin1 = "0" & bin1  'make sure bin is a 8-bit binary string
                    'End While

                    '' resultBin = bin1 XOR testByte each possible hex value 00 thru ff
                    'Dim resultBin As String

                    ''Performing XOR
                    ''Doing first bit out of the loop to initialize resultBin
                    'If bin1.Substring(0, 1) = testByte.Substring(0, 1) Then
                    '    resultBin = "0"
                    'Else
                    '    resultBin = "1"
                    'End If
                    ''Now doing the rest of the 8 bits
                    'For j As Integer = 1 To 7
                    '    Dim currentBit1 As String = bin1.Substring(j, 1)
                    '    Dim currentBit2 As String = testByte.Substring(j, 1)

                    '    If currentBit1 = currentBit2 Then
                    '        resultBin = resultBin & "0"
                    '    Else
                    '        resultBin = resultBin & "1"
                    '    End If
                    'Next

                    '' At this point, resultBin is a binary literal so I need to 
                    '' convert it to a binary number and then to a hex value
                    'Dim tempByte As Byte = 0
                    'For k As Integer = 7 To 0 Step -1
                    '    ' next line is explained at
                    '    ' http://www.acraigie.com/programming/bitstobytes.html 
                    '    tempByte = tempByte Or CByte(CInt(resultBin.Substring(k, 1)) << Math.Abs(k - 7))
                    'Next



                    ' ************************************************************************************
                    ' Experimenting with turbo-mode - exit loop if character is out of standard text range
                    ' If tempByte < &H20 Or tempByte > &H7E Then
                    If tempByte < &H0 Then
                        skipFreqAnalysis = 1
                        ' ******TextBox3.Text = TextBox3.Text & "**GARBAGE**"
                        Exit For
                    Else
                        Dim com As String = ChrW(CInt(tempByte))
                        ' ******TextBox3.Text = TextBox3.Text & com

                        ' build a string in resultString to use for freq analysis
                        If tempByte = 0 Then
                            resultString = resultString & " " ' if this is a null, add a space
                        ElseIf tempByte = &H5E Or tempByte = &HD Or tempByte = &HA Then
                            resultString = resultString & "~" ' if this is a ^, add a ~ so it doesn't mess up my Excel delimiting
                        Else
                            resultString = resultString & com ' if it is not a null, add the character
                        End If
                    End If

                Next
                ' ******TextBox3.Text = TextBox3.Text & Environment.NewLine & Environment.NewLine

                If skipFreqAnalysis = 0 Then


                    ' frequency analysis
                    ' do two loops: one for common pairs and one for common triplets
                    ' weigh common triplets slightly more
                    ' common pair is worth 4 pts
                    ' common triplet is worth 5 pts
                    '
                    ' available variables:
                    ' resultString is holding the string
                    ' resultScore for holding the score for this result
                    ' topScore for holding best score so far (if resultScore > topScore then overwrite it to textBox4)
                    ' commonPairs(0 to 27)
                    ' commonTriplets(0 to 6)
                    '
                    ' Search for every instance of every common pair/triplet
                    ' and update resultScore/topScore accordingly
                    Dim lngPosition1 As Long
                    Dim lngPosition2 As Long
                    Dim resultScore As Integer = 0
                    For lngPosition1 = LBound(commonPairs) To UBound(commonPairs)
                        Dim position As Integer = 1
                        Dim lastPosition As Integer = resultString.Length - 1
                        Do
                            Dim strFind As Integer = InStr(position, LCase$(resultString), LCase$(commonPairs(lngPosition1)))
                            If strFind = 0 Then
                                Exit Do
                            Else
                                resultScore = resultScore + 4
                                position = position + 1
                            End If
                        Loop While position < lastPosition
                    Next

                    For lngPosition2 = LBound(commonTriplets) To UBound(commonTriplets)
                        Dim position As Integer = 1
                        Dim lastPosition As Integer = resultString.Length - 1
                        Do
                            Dim strFind As Integer = InStr(position, LCase$(resultString), LCase$(commonTriplets(lngPosition2)))
                            If strFind = 0 Then
                                Exit Do
                            Else
                                resultScore = resultScore + 5
                                position = position + 1
                            End If
                        Loop While position < lastPosition
                    Next

                    If resultScore > topScore1 Then
                        topScore1 = resultScore
                        topScoreResultString1 = resultString
                        topScoreResultKey1 = t
                    ElseIf resultScore > topScore2 Then
                        topScore2 = resultScore
                        topScoreResultString2 = resultString
                        topScoreResultKey2 = t
                    ElseIf resultScore > topScore3 Then
                        topScore3 = resultScore
                        topScoreResultString3 = resultString
                        topScoreResultKey3 = t
                    End If

                End If
                If t = &HFF Then Exit For 'fix bug where system gives overflow after last loop where t is temporarily &hff+1
            Next
            If topScore1 > 0 Then TextBox4.Text = TextBox4.Text & Environment.NewLine & stringData1 & "^" & "code: " & "^" & topScoreResultString1 & "^" & " Key: " & "^" & topScoreResultKey1 & "^" & " Score: " & "^" & topScore1 & Environment.NewLine & stringData1 & "^" & "code: " & "^" & topScoreResultString2 & "^" & " Key: " & "^" & topScoreResultKey2 & "^" & " Score: " & "^" & topScore2 & Environment.NewLine & stringData1 & "^" & "code: " & "^" & topScoreResultString3 & "^" & " Key: " & "^" & topScoreResultKey3 & "^" & " Score: " & "^" & topScore3
        Next
    End Sub

    
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.Title = "Please Select a File"
        OpenFileDialog1.InitialDirectory = "C:temp"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Dim strm As System.IO.Stream

        strm = OpenFileDialog1.OpenFile()

        TextBox1.Text = OpenFileDialog1.FileName.ToString()

        If Not (strm Is Nothing) Then
            'insert code to read the file data
            strm.Close()
        End If

        ' Import strings from text file
        Dim R As New IO.StreamReader(OpenFileDialog1.FileName)
        Dim str As String() = R.ReadToEnd().Split(New String(vbLf)) ' Delimiter is vbLF (LineFeed)
        ListBox1.Items.AddRange(str) ' Put the strings from the imported file into the list box
        R.Close()

    End Sub
End Class
