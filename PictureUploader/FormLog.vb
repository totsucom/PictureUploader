Public Class FormLog
    Private _arg As Form1.WorkerArg

    Sub New(ByRef arg As Form1.WorkerArg)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _arg = arg
    End Sub

    Private Sub FormLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SyncLock _arg
            TextBox1.Text = _arg.lastLog
            _arg.logUpdated = False
        End SyncLock
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SyncLock _arg
            TextBox1.Text = _arg.lastLog
            _arg.logUpdated = False
        End SyncLock
        Button1.Enabled = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Not Button1.Enabled Then
            SyncLock _arg
                Button1.Enabled = _arg.logUpdated
            End SyncLock
        End If
    End Sub
End Class