
Partial Class _Default
    Inherits System.Web.UI.Page
    Public Shared list As New ArrayList
    Public Shared count As Integer = 0
    Public Shared listlength As Integer = 0
    Public Shared Answered As New ArrayList
    Public Shared timePassed As Integer = 0
    Public Shared appPath As String
    Public Shared TableEnable As Boolean
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            Dim path As String = HttpContext.Current.Request.ApplicationPath
            appPath = HttpContext.Current.Request.MapPath(path)
            Dim FileNum As Integer = FreeFile()
            FileOpen(FileNum, appPath + "test.txt", OpenMode.Input)
            Dim j As Integer = 0
            Do Until EOF(FileNum)
                list.Add(LineInput(FileNum))
                listlength = listlength + 1
            Loop
            System.IO.File.WriteAllText(appPath + "result.txt", "")
            FileClose(FileNum)
            Create_UI()
        Else
            If (count >= list.Count) Then
                form1.Visible = False
            Else
                Create_UI()
            End If


        End If
    End Sub

    Private Sub Create_UI()

        Table1.Rows.Clear()
        Dim choices As Integer
        Dim line As String
        line = TryCast(list.Item(count), String)
        line = line.Substring(1)
        Dim endIndex As Integer
        Dim Qtype, alignment As String
        TableEnable = True
        Table1.Enabled = TableEnable
        For i = 0 To 3 Step 1
            endIndex = line.IndexOf("|")
            Dim substring As String = line.Substring(0, endIndex)
            If i = 0 Then
                Qtype = substring
            ElseIf i = 1 Then
                alignment = substring
            ElseIf i = 2 Then
                choices = Convert.ToInt32(substring.Substring(3))
            ElseIf i = 3 Then
                If String.Compare(substring, "Off") = 0 Then
                    Label2.Text = ": ∞"
                    Label1.Visible = True
                Else
                    line = line.Substring(endIndex + 1)
                    If String.Compare(line.Substring(0, endIndex), "Timer0") = 0 Then
                        TableEnable = False
                    ElseIf String.Compare(line.Substring(0, endIndex), "Timer1") = 0 Then
                        TableEnable = True
                    End If
                    endIndex = line.IndexOf("|")
                    Dim sec As Integer = Convert.ToInt32(substring) - timePassed
                    Label2.Text = ": " + Convert.ToString(sec)

                    Timer1.Enabled = True
                End If
            End If
            line = line.Substring(endIndex + 1)
        Next
        endIndex = line.IndexOf("\")
        Label1.Text = line.Substring(0, endIndex)
        line = line.Substring(endIndex + 3)
        Dim rw As New TableRow()
        If Answered.Contains(count) Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
        For i = 0 To choices - 1 Step 1
            endIndex = line.IndexOf("\")
            Dim cel As New TableCell()

            If String.Compare(Qtype, "ButtonT") = 0 Then
                Dim dynItem = New Button()
                dynItem.ID = "but" + Convert.ToString(i)
                If (i <> choices - 1) Then
                    dynItem.Text = line.Substring(0, endIndex)
                Else
                    dynItem.Text = line.Substring(0, line.Length - 2)
                End If
                AddHandler dynItem.Click, AddressOf But_click
                If Button2.Enabled = True Then
                    Dim Lines() As String = System.IO.File.ReadAllLines(appPath + "result.txt")
                    If String.Compare(Lines(count).Substring(1), dynItem.Text) = 0 Then
                        dynItem.BackColor = Drawing.Color.Red
                        Table1.Enabled = TableEnable
                    End If
                End If
                cel.Controls.Add(dynItem)


            ElseIf String.Compare(Qtype, "RadioT") = 0 Then

                Dim dynItem As New RadioButton
                dynItem.ID = "Rbut" + Convert.ToString(i)
                If (i <> choices - 1) Then
                    dynItem.Text = line.Substring(0, endIndex)
                Else
                    dynItem.Text = line.Substring(0, line.Length - 2)
                End If
                dynItem.AutoPostBack = True
                AddHandler dynItem.CheckedChanged, AddressOf But_click
                If Button2.Enabled = True Then
                    Dim Lines() As String = System.IO.File.ReadAllLines(appPath + "result.txt")
                    If String.Compare(Lines(count).Substring(1), dynItem.Text) = 0 Then
                        dynItem.ForeColor = Drawing.Color.Red
                        Table1.Enabled = TableEnable
                    End If
                End If
                cel.Controls.Add(dynItem)


            ElseIf String.Compare(Qtype, "ButtonI") = 0 Then
                Dim dynItem As New ImageButton
                dynItem.ID = " Image " + Convert.ToString(i)
                Dim label2 As New Label
                If (i <> choices - 1) Then
                    If line.Substring(1, endIndex).StartsWith("http://") Or line.Substring(1, endIndex).StartsWith("https://") Then
                        dynItem.ImageUrl = line.Substring(1, endIndex)
                    Else
                        dynItem.ImageUrl = "~/" + line.Substring(1, endIndex - 1)
                    End If

                Else
                    If line.Substring(1, line.Length - 2).StartsWith("http://") Or line.Substring(1, line.Length - 2).StartsWith("https://") Then
                        dynItem.ImageUrl = line.Substring(1, line.Length - 2)
                    Else
                        dynItem.ImageUrl = "~/" + line.Substring(1, line.Length - 2)
                    End If
                End If
                dynItem.Height = 80
                dynItem.Width = 80
                AddHandler dynItem.Click, AddressOf But_click
                If Button2.Enabled = True Then
                    Dim Lines() As String = System.IO.File.ReadAllLines(appPath + "result.txt")
                    If String.Compare(Lines(count).Substring(1), dynItem.ID) = 0 Then
                        cel.BorderColor = Drawing.Color.Red
                        cel.BorderStyle = BorderStyle.Double
                        Table1.Enabled = TableEnable

                    End If
                End If
                cel.Controls.Add(dynItem)
            End If

            rw.Cells.Add(cel)
            Table1.Rows.Add(rw)
            If String.Compare(alignment, "Horizontially") = 0 Then
                rw = New TableRow
            End If
            line = line.Substring(endIndex + 3)
        Next
    End Sub

    Protected Sub But_click(ByVal sender As Object, ByVal e As System.EventArgs)
        timePassed = 0
        Timer1.Enabled = False
        Answered.Add(count)
        Dim Lines() As String = System.IO.File.ReadAllLines(appPath + "result.txt")
        If (Lines.Length > count) Then
            If TypeOf sender Is ImageButton Then
                Lines(count) = Convert.ToString(count) + sender.ID
            Else
                Lines(count) = Convert.ToString(count) + sender.Text
            End If
            System.IO.File.WriteAllLines(appPath + "result.txt", Lines)
        Else
            Dim file As System.IO.StreamWriter
            file = My.Computer.FileSystem.OpenTextFileWriter(appPath + "result.txt", True)
            If TypeOf sender Is ImageButton Then
                file.WriteLine(Convert.ToString(count) + sender.ID)
            Else
                file.WriteLine(Convert.ToString(count) + sender.Text)
            End If

            file.Close()

        End If
        count = count + 1
        If (count >= listlength) Then
            form1.Visible = False
        Else
            Create_UI()
        End If
        Button1.Enabled = True

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        count = count - 1
        timePassed = 0
        Timer1.Enabled = False
        If (count >= 0) Then
            If (count = 0) Then Button1.Enabled = False
        End If
        Create_UI()

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        count = count + 1
        timePassed = 0
        Timer1.Enabled = False
        If (count >= list.Count) Then
            form1.Visible = False
        Else
            Create_UI()
        End If
        Button1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        timePassed = timePassed + 1
        If Convert.ToInt32(Label2.Text.Substring(2)) = 0 Then
            timePassed = 0
            count = count + 1
            Timer1.Enabled = False
            Button1.Enabled = True
            Dim Lines() As String = System.IO.File.ReadAllLines(appPath + "result.txt")
            If (Lines.Length <= count) Then
                Dim file As System.IO.StreamWriter
                file = My.Computer.FileSystem.OpenTextFileWriter(appPath + "result.txt", True)
                file.WriteLine(Convert.ToString(count) + "None")
                file.Close()
            End If
            If (count >= list.Count) Then
                form1.Visible = False
            Else
                Create_UI()
            End If


        End If


    End Sub
End Class
