Imports ZXing

Public Class FormQRCode
    Const QR_MARGIN = 10
    Const QR_SIZE = (320 + QR_MARGIN * 2)

    Private _url As String
    Private _location As Point
    Private _isCursorShowing As Boolean = True
    Private _cusror As Cursor

    Sub New(url As String, location As Point)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        _url = url
        _location = location
    End Sub

    Private Sub FormQRCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Location = _location

        Dim frame As Size = Me.Size - Me.ClientSize
        Me.Size = New Size(frame.Width + QR_SIZE, frame.Height + QR_SIZE)

        'QRコード作成
        Dim qrcode = New BarcodeWriter
        With qrcode
            .Format = BarcodeFormat.QR_CODE
            qrcode.Options = New ZXing.QrCode.QrCodeEncodingOptions
            With qrcode.Options
                .Height = QR_SIZE
                .Width = QR_SIZE
                .Margin = QR_MARGIN
            End With
        End With
        PictureBox1.Image = qrcode.Write(_url)

        Dim bmp = New Bitmap(1, 1)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.FillRectangle(Brushes.Black, g.VisibleClipBounds)
        g.Dispose()
        bmp.MakeTransparent()
        Dim handle As IntPtr = bmp.GetHicon()
        Dim ico As Icon = Icon.FromHandle(handle)
        _cusror = New Cursor(ico.Handle)
    End Sub

    Private Sub FormQRCode_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        Me.Dispose()
    End Sub

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        ShowCursor()
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        ShowCursor()
        Timer1.Start()
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        ShowCursor()
    End Sub

    Private Sub FormQRCode_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        ShowCursor()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        HideCursor()
    End Sub

    Private Sub HideCursor()
        If _isCursorShowing Then
            Me.Cursor = _cusror
            _isCursorShowing = False
        End If
    End Sub

    Private Sub ShowCursor()
        If Not _isCursorShowing Then
            Me.Cursor = Cursors.Default
            _isCursorShowing = True
        End If
    End Sub


End Class