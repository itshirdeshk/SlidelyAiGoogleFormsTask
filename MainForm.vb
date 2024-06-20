Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Hirdesh Khandelwal, Slidely Task 2 - Slidely Form App"
        Me.Width = 800
        Me.Height = 600
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.LightBlue
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False

        ' Create "View Submissions" button
        Dim btnViewSubmissions As New Button()
        btnViewSubmissions.Text = "View Submissions (CTRL + V)"
        btnViewSubmissions.Size = New Size(300, 50)
        btnViewSubmissions.Font = New Font("Arial", 12, FontStyle.Bold)
        btnViewSubmissions.Location = New Point(300, 200)
        btnViewSubmissions.BackColor = Color.White
        btnViewSubmissions.ForeColor = Color.DarkBlue
        AddHandler btnViewSubmissions.Click, AddressOf BtnViewSubmissions_Click
        Me.Controls.Add(btnViewSubmissions)

        ' Create "Create Submission" button
        Dim btnCreateSubmission As New Button()
        btnCreateSubmission.Text = "Create New Submission (CTRL + N)"
        btnCreateSubmission.Size = New Size(300, 50)
        btnCreateSubmission.Font = New Font("Arial", 12, FontStyle.Bold)
        btnCreateSubmission.Location = New Point(300, 300)
        btnCreateSubmission.BackColor = Color.White
        btnCreateSubmission.ForeColor = Color.DarkBlue
        AddHandler btnCreateSubmission.Click, AddressOf BtnCreateSubmission_Click
        Me.Controls.Add(btnCreateSubmission)
    End Sub

    Private Sub BtnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub BtnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = (Keys.Control Or Keys.V) Then
            BtnViewSubmissions_Click(Nothing, Nothing)
            Return True
        ElseIf keyData = (Keys.Control Or Keys.N) Then
            BtnCreateSubmission_Click(Nothing, Nothing)
            Return True
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function
End Class
