Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text

Public Class ViewSubmissionsForm
    Private submissions As New List(Of Submission)()
    Private currentIndex As Integer = 0

    Private txtName As TextBox
    Private txtEmail As TextBox
    Private txtPhone As TextBox
    Private txtGithubLink As TextBox
    Private txtStopwatchTime As TextBox

    Private btnEditName As Button
    Private btnEditEmail As Button
    Private btnEditPhone As Button
    Private btnEditGithubLink As Button
    Private btnEditStopwatchTime As Button

    Private btnPrevious As Button
    Private btnNext As Button
    Private btnDelete As Button
    Private btnUpdate As Button

    Public Sub New()
        InitializeComponent()
        InitializeAsync()
    End Sub

    Private Async Sub InitializeAsync()
        Try
            Await LoadSubmissions()
            If submissions.Count > 0 Then
                DisplaySubmission()
            Else
                MessageBox.Show("No Submissions Available. Create New Submissions to View Submissions.")
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show($"Failed to initialize! Exception: {ex.Message}")
        End Try
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Hirdesh Khandelwal, Slidely Task 2 - View Submissions"
        Me.Width = 600
        Me.Height = 600
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.LightGray
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' Name
        Dim lblName As New Label()
        lblName.Text = "Name"
        lblName.Location = New Point(20, 20)
        lblName.Size = New Size(200, 20)
        lblName.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(lblName)

        txtName = New TextBox()
        txtName.Location = New Point(20, 50)
        txtName.Size = New Size(400, 30)
        txtName.Font = New Font("Arial", 10)
        txtName.ReadOnly = True
        Me.Controls.Add(txtName)

        btnEditName = New Button()
        btnEditName.Text = "Edit"
        btnEditName.Location = New Point(430, 50)
        btnEditName.Size = New Size(100, 30)
        btnEditName.Font = New Font("Arial", 10)
        AddHandler btnEditName.Click, Sub(sender, e) ToggleEditMode(txtName, btnEditName)
        Me.Controls.Add(btnEditName)

        ' Email
        Dim lblEmail As New Label()
        lblEmail.Text = "Email"
        lblEmail.Location = New Point(20, 90)
        lblEmail.Size = New Size(200, 20)
        lblEmail.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(lblEmail)

        txtEmail = New TextBox()
        txtEmail.Location = New Point(20, 120)
        txtEmail.Size = New Size(400, 30)
        txtEmail.Font = New Font("Arial", 10)
        txtEmail.ReadOnly = True
        Me.Controls.Add(txtEmail)

        btnEditEmail = New Button()
        btnEditEmail.Text = "Edit"
        btnEditEmail.Location = New Point(430, 120)
        btnEditEmail.Size = New Size(100, 30)
        btnEditEmail.Font = New Font("Arial", 10)
        AddHandler btnEditEmail.Click, Sub(sender, e) ToggleEditMode(txtEmail, btnEditEmail)
        Me.Controls.Add(btnEditEmail)

        ' Phone
        Dim lblPhone As New Label()
        lblPhone.Text = "Phone Number"
        lblPhone.Location = New Point(20, 160)
        lblPhone.Size = New Size(200, 20)
        lblPhone.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(lblPhone)

        txtPhone = New TextBox()
        txtPhone.Location = New Point(20, 190)
        txtPhone.Size = New Size(400, 30)
        txtPhone.Font = New Font("Arial", 10)
        txtPhone.ReadOnly = True
        Me.Controls.Add(txtPhone)

        btnEditPhone = New Button()
        btnEditPhone.Text = "Edit"
        btnEditPhone.Location = New Point(430, 190)
        btnEditPhone.Size = New Size(100, 30)
        btnEditPhone.Font = New Font("Arial", 10)
        AddHandler btnEditPhone.Click, Sub(sender, e) ToggleEditMode(txtPhone, btnEditPhone)
        Me.Controls.Add(btnEditPhone)

        ' GitHub Link
        Dim lblGithubLink As New Label()
        lblGithubLink.Text = "Github Link For Task 2"
        lblGithubLink.Location = New Point(20, 230)
        lblGithubLink.Size = New Size(200, 20)
        lblGithubLink.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(lblGithubLink)

        txtGithubLink = New TextBox()
        txtGithubLink.Location = New Point(20, 260)
        txtGithubLink.Size = New Size(400, 30)
        txtGithubLink.Font = New Font("Arial", 10)
        txtGithubLink.ReadOnly = True
        Me.Controls.Add(txtGithubLink)

        btnEditGithubLink = New Button()
        btnEditGithubLink.Text = "Edit"
        btnEditGithubLink.Location = New Point(430, 260)
        btnEditGithubLink.Size = New Size(100, 30)
        btnEditGithubLink.Font = New Font("Arial", 10)
        AddHandler btnEditGithubLink.Click, Sub(sender, e) ToggleEditMode(txtGithubLink, btnEditGithubLink)
        Me.Controls.Add(btnEditGithubLink)

        ' Stopwatch Time
        Dim lblStopwatchTime As New Label()
        lblStopwatchTime.Text = "Stopwatch Time"
        lblStopwatchTime.Location = New Point(20, 300)
        lblStopwatchTime.Size = New Size(200, 20)
        lblStopwatchTime.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(lblStopwatchTime)

        txtStopwatchTime = New TextBox()
        txtStopwatchTime.Location = New Point(20, 330)
        txtStopwatchTime.Size = New Size(400, 30)
        txtStopwatchTime.Font = New Font("Arial", 10)
        txtStopwatchTime.ReadOnly = True
        Me.Controls.Add(txtStopwatchTime)

        btnEditStopwatchTime = New Button()
        btnEditStopwatchTime.Text = "Edit"
        btnEditStopwatchTime.Location = New Point(430, 330)
        btnEditStopwatchTime.Size = New Size(100, 30)
        btnEditStopwatchTime.Font = New Font("Arial", 10)
        AddHandler btnEditStopwatchTime.Click, Sub(sender, e) ToggleEditMode(txtStopwatchTime, btnEditStopwatchTime)
        Me.Controls.Add(btnEditStopwatchTime)

        ' Previous button
        btnPrevious = New Button()
        btnPrevious.Text = "PREVIOUS (CTRL + P)"
        btnPrevious.Size = New Size(200, 50)
        btnPrevious.Location = New Point(50, 400)
        btnPrevious.Font = New Font("Arial", 12, FontStyle.Bold)
        btnPrevious.BackColor = Color.White
        btnPrevious.ForeColor = Color.DarkBlue
        AddHandler btnPrevious.Click, AddressOf BtnPrevious_Click
        Me.Controls.Add(btnPrevious)

        ' Next button
        btnNext = New Button()
        btnNext.Text = "NEXT (CTRL + N)"
        btnNext.Size = New Size(200, 50)
        btnNext.Location = New Point(300, 400)
        btnNext.Font = New Font("Arial", 12, FontStyle.Bold)
        btnNext.BackColor = Color.White
        btnNext.ForeColor = Color.DarkBlue
        AddHandler btnNext.Click, AddressOf BtnNext_Click
        Me.Controls.Add(btnNext)

        ' Delete button
        btnDelete = New Button()
        btnDelete.Text = "DELETE (CTRL + D)"
        btnDelete.Size = New Size(200, 50)
        btnDelete.Location = New Point(50, 470)
        btnDelete.Font = New Font("Arial", 12, FontStyle.Bold)
        btnDelete.BackColor = Color.Red
        btnDelete.ForeColor = Color.White
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        Me.Controls.Add(btnDelete)

        ' Update button
        btnUpdate = New Button()
        btnUpdate.Text = "UPDATE (CTRL + U)"
        btnUpdate.Size = New Size(200, 50)
        btnUpdate.Location = New Point(300, 470)
        btnUpdate.Font = New Font("Arial", 12, FontStyle.Bold)
        btnUpdate.BackColor = Color.LightGreen
        btnUpdate.ForeColor = Color.DarkGreen
        AddHandler btnUpdate.Click, AddressOf BtnUpdate_Click
        Me.Controls.Add(btnUpdate)
    End Sub

    Private Async Function LoadSubmissions() As Task
        Try
            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/read")
                Debug.WriteLine($"Response -> {response}")
                If response.IsSuccessStatusCode Then
                    Dim json As String = Await response.Content.ReadAsStringAsync()
                    Debug.WriteLine($"Received JSON -> {json}")
                    submissions = JsonConvert.DeserializeObject(Of List(Of Submission))(json)
                    Debug.WriteLine($"Number of submissions loaded: {submissions.Count}")
                Else
                    MessageBox.Show("Failed to load submissions.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to get Submissions! Exception: {ex.Message}")
        End Try
    End Function

    Private Sub DisplaySubmission()
        If submissions IsNot Nothing AndAlso submissions.Count > 0 Then
            Dim submission = submissions(currentIndex)
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGithubLink.Text = submission.GithubLink
            txtStopwatchTime.Text = submission.StopwatchTime
        End If
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        Else
            MessageBox.Show("No Previous Submissions Available.")
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        Else
            MessageBox.Show("No Next Submissions Available.")
        End If
    End Sub

    Private Async Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Are you sure you want to delete this submission?", "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                Using client As New HttpClient()
                    Dim response = Await client.DeleteAsync($"http://localhost:3000/delete?index={currentIndex}")
                    If response.IsSuccessStatusCode Then
                        MessageBox.Show("Submission deleted successfully.")
                        submissions.RemoveAt(currentIndex)
                        If submissions.Count > 0 AndAlso currentIndex >= submissions.Count Then
                            currentIndex = submissions.Count - 1
                            DisplaySubmission()
                        ElseIf submissions.Count <= 0 Then
                            MessageBox.Show("No Submissions Available.")
                            Me.Close()
                        End If
                    Else
                        MessageBox.Show("Failed to delete submission.")
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show($"Failed to delete submission! Exception: {ex.Message}")
            End Try
        End If
    End Sub

    Private Async Sub BtnUpdate_Click(sender As Object, e As EventArgs)
        Dim updatedSubmission As New Submission With {
            .Name = txtName.Text,
            .Email = txtEmail.Text,
            .Phone = txtPhone.Text,
            .GithubLink = txtGithubLink.Text,
            .StopwatchTime = txtStopwatchTime.Text
        }

        Try
            Using client As New HttpClient()
                Dim jsonContent = JsonConvert.SerializeObject(New With {.index = currentIndex, .name = updatedSubmission.Name, .email = updatedSubmission.Email, .phone = updatedSubmission.Phone, .github_link = updatedSubmission.GithubLink, .stopwatch_time = updatedSubmission.StopwatchTime})
                Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

                Dim response = Await client.PutAsync("http://localhost:3000/edit", content)
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission updated successfully.")
                    submissions(currentIndex) = updatedSubmission
                    ToggleReadOnly(txtName, btnEditName)
                    ToggleReadOnly(txtEmail, btnEditEmail)
                    ToggleReadOnly(txtPhone, btnEditPhone)
                    ToggleReadOnly(txtGithubLink, btnEditGithubLink)
                Else
                    MessageBox.Show("Failed to update submission.")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Failed to update submission! Exception: {ex.Message}")
        End Try
    End Sub

    Private Sub ToggleEditMode(txtBox As TextBox, btn As Button)
        txtBox.ReadOnly = Not txtBox.ReadOnly
        btn.Text = If(txtBox.ReadOnly, "Edit", "Save")
    End Sub

    Private Sub ToggleReadOnly(txtBox As TextBox, btn As Button)
        txtBox.ReadOnly = True
        btn.Text = "Edit"
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.P) Then
            BtnPrevious_Click(Nothing, Nothing)
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            BtnNext_Click(Nothing, Nothing)
            Return True
        ElseIf keyData = (Keys.Control Or Keys.D) Then
            BtnDelete_Click(Nothing, Nothing)
            Return True
        ElseIf keyData = (Keys.Control Or Keys.U) Then
            BtnUpdate_Click(Nothing, Nothing)
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class