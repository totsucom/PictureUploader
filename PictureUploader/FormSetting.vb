Public Class FormSetting

    Public Const DEFAULT_IMAGE_DIR = "\image"
    Public Const DEFAULT_PORT_NO = 8005

    Public Shared Function GetDefaultImageDir() As String
        Return System.Windows.Forms.Application.StartupPath & DEFAULT_IMAGE_DIR
    End Function


    Private _imageDir As String
    Private _portNo As Integer
    Private _modified As Boolean

    Public ReadOnly Property ImageDir As String
        Get
            Return _imageDir
        End Get
    End Property

    Public ReadOnly Property PortNo As Integer
        Get
            Return _portNo
        End Get
    End Property

    Public ReadOnly Property Modified As Boolean
        Get
            Return _modified
        End Get
    End Property

    Sub New(imageDir As String, portNo As Integer)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _imageDir = imageDir
        _portNo = portNo
        _modified = False
    End Sub

    Private Sub FormSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxImageDir.Text = ImageDir
        TextBoxPortNo.Text = PortNo.ToString()
    End Sub

    Private Sub ButtonSelectImageDir_Click(sender As Object, e As EventArgs) Handles ButtonSelectImageDir.Click
        Dim fbd As New FolderBrowserDialog
        fbd.Description = "フォルダを指定してください"

        '初期フォルダを設定
        If IO.Directory.Exists(TextBoxImageDir.Text) Then
            fbd.SelectedPath = TextBoxImageDir.Text
        Else
            fbd.SelectedPath = ImageDir
        End If

        If fbd.ShowDialog(Me) = DialogResult.OK Then
            TextBoxImageDir.Text = fbd.SelectedPath
        End If
    End Sub

    Private Sub ButtonDefault_Click(sender As Object, e As EventArgs) Handles ButtonDefault.Click
        TextBoxImageDir.Text = GetDefaultImageDir()
        TextBoxPortNo.Text = DEFAULT_PORT_NO.ToString()
    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        If TextBoxImageDir.Text.Trim.Length = 0 Then
            MsgBox("フォルダを指定してください", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        If ImageDir <> TextBoxImageDir.Text Then
            If Not IO.Directory.Exists(TextBoxImageDir.Text) Then
                Dim res = MsgBox("フォルダは存在しません。作成しますか？", MsgBoxStyle.YesNoCancel Or MsgBoxStyle.Question)
                If res = MsgBoxResult.Cancel Then Exit Sub
                If res = MsgBoxResult.Yes Then
                    Try
                        IO.Directory.CreateDirectory(TextBoxImageDir.Text)
                    Catch ex As Exception
                        MsgBox("フォルダを作成できませんでした" & vbNewLine & vbNewLine & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                        Exit Sub
                    End Try
                End If
                _imageDir = TextBoxImageDir.Text
                _modified = True
            End If
        End If

        Dim p As Integer
        If Not Integer.TryParse(TextBoxPortNo.Text, p) Then
            MsgBox("ポート番号は1-65535の整数でなければなりません", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        If p < 1 OrElse p > 65535 Then
            MsgBox("ポート番号は1-65535の整数でなければなりません", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Exit Sub
        End If

        If p <> PortNo Then
            _portNo = p
            _modified = True
        End If

        DialogResult = DialogResult.OK
    End Sub
End Class