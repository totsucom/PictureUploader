Imports System.Net
Imports System.Text
Imports ZXing

Public Class Form1
    Private Ini As ClsIni

    Private thread1 As System.Threading.Thread
    Private arg As New WorkerArg

    'Dim port As Integer
    'Dim imageDir As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'iniファイルから設定を読み出す
        Ini = New ClsIni(Application.StartupPath & "\settings.ini")
        arg.imageDir = Ini.GetProfileString("Directory", "Image", FormSetting.GetDefaultImageDir())
        arg.port = Integer.Parse(Ini.GetProfileString("Network", "Port", FormSetting.DEFAULT_PORT_NO.ToString()))

        'PIN
        Dim r As New Random(Integer.Parse(Date.Now.ToString("HHmmss")))
        arg.pin = (r.Next() + 10000).ToString()

        '自分のIPアドレスを列挙、コンボボックスに設定
        Dim ar As IpAddressInfo() = GetIPAddresses()
        For Each ii As IpAddressInfo In ar
            ComboBoxIPAddresses.Items.Add(ii)
        Next
        If ComboBoxIPAddresses.Items.Count = 0 Then
            LinkLabelQRInfo.ForeColor = Color.Red
            LinkLabelQRInfo.Text = "このPCのネットワークアドレスが見つかりません"
            Exit Sub
        End If

        '最初のIPアドレスを選択
        ComboBoxIPAddresses.SelectedIndex = 0

        'スレッドに渡すためのパラメータを準備
        arg.arIpAddressInfo = ar
        arg.stat = WorkerArg.STATUS.TERMINATED

        'ウェブサーバースレッドを開始
        thread1 = New System.Threading.Thread(AddressOf ThreadMain1)
        thread1.Start()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If TerminateRequestToThread1() Then
            Debug.Print("ウェブサーバースレッドは終了しました")
        End If
    End Sub

    'ウェブサーバースレッドに終了要求を送信
    Private Function TerminateRequestToThread1()
        SyncLock arg
            If arg.stat <> WorkerArg.STATUS.RUNNING Then Return True

            '終了フラグをセット
            arg.inst = WorkerArg.INSTRUCTION.TERMINATE
        End SyncLock

        'ダミーリクエストを送信し、スレッド終了を促す
        Dim httpReq As HttpWebRequest = HttpWebRequest.Create("http://localhost:" & arg.port.ToString & "/")
        httpReq.Method = "GET"
        httpReq.Timeout = 100
        Try
            Dim httpRes As HttpWebResponse = httpReq.GetResponse()
            httpRes.Close()
        Catch ex As Exception
        End Try

        Threading.Thread.Sleep(100)
        SyncLock arg
            'うまく終了できた
            If arg.stat <> WorkerArg.STATUS.RUNNING Then Return True
        End SyncLock

        '強制終了
        thread1.Abort()
        Return False
    End Function

    Public Class IpAddressInfo
        Public address As String
        Public nwkclass As Integer ' = 0:クラスA 1:クラスB 2:クラスC 3:クラスD 4:クラスE
        Public Sub New(address As String, nwkclass As Integer)
            Me.address = address
            Me.nwkclass = nwkclass
        End Sub
        Public Overrides Function ToString() As String
            Return "アドレス:" & address & " クラス:" & Chr(Asc("A"c) + nwkclass)
        End Function
    End Class

    'ローカルPCのIPv4アドレスを取得し、ネットワーククラスの狭い順(ClassE->A)に並べて返す
    Private Shared Function GetIPAddresses() As IpAddressInfo()
        Dim ar As New List(Of IpAddressInfo)
        Dim ipentry As IPHostEntry = Dns.GetHostEntry(Dns.GetHostName())

        For Each ip As IPAddress In ipentry.AddressList

            'IPv4アドレスのみ
            If ip.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then

                'アドレスの最上位ビットからビット0が見つかるまで数える。i=0..4
                Dim adr As String = ip.ToString
                Dim firstAddress = Integer.Parse(adr.Substring(0, adr.IndexOf("."c)))
                Dim bit As Integer = 128
                Dim i As Integer = 0
                While (firstAddress And bit) <> 0
                    bit >>= 1
                    i += 1
                    If i = 4 Then Exit While
                End While

                'i = 0:クラスA 1:クラスB 2:クラスC 3:クラスD 4:クラスE

                'arに追加する。狭いネットワーク優先(つまりクラスEが最優先)
                Dim added As Boolean = False
                For n As Integer = 0 To ar.Count - 1
                    If i > ar(n).nwkclass Then
                        ar.Insert(n, New IpAddressInfo(adr, i))
                        added = True
                        Exit For
                    End If
                Next
                If Not added Then
                    ar.Add(New IpAddressInfo(adr, i))
                End If
            End If
        Next
        Return ar.ToArray()
    End Function

    'バイト配列版のIndexOf()
    Private Shared Function IndexOf(ByRef byteArray As Byte(), ByRef targetBytes As Byte(), Optional startIndex As Integer = 0) As Integer
        Do
            Dim byteIndex As Integer = Array.IndexOf(byteArray, targetBytes(0), startIndex) '最初の一文字を検索する
            If byteIndex < 0 OrElse byteIndex + targetBytes.Length > byteArray.Length Then
                Return -1 '最初の文字が見つからなかった場合、またはStartIndexの位置がバイト配列の最後まで到達した場合は処理終了
            End If

            Dim different As Boolean = False
            For i As Integer = 1 To targetBytes.Length - 1 '残りの文字（2文字目以降）も検索する
                If byteArray(byteIndex + i) <> targetBytes(i) Then
                    different = True '合致しなかった
                    Exit For
                End If
            Next

            If Not different Then
                Return byteIndex '見つけた
            End If

            startIndex = byteIndex + 1 'ヒットしなかった場合は、検索開始位置を1バイトずらして再度検索
        Loop
    End Function

    'バイト配列のindexから1行読んでUTF8として文字列変換する。indexは次の行の先頭を指す
    Private Shared Function GetLine(ByRef byteArray As Byte(), ByRef index As Integer) As String
        Static eol As Byte() = {&HD, &HA}

        Dim nextIndex As Integer = index
        Dim endLineIndex As Integer = IndexOf(byteArray, eol, nextIndex)

        If endLineIndex < 0 Then
            '改行コードが見つからなかったのでindex以降を文字列に変換
            Dim s As String = Encoding.UTF8.GetString(byteArray, index, byteArray.Length - index)
            index = -1 '次の行は無いので-1
            Return s
        Else
            '改行コードが見つかったのでindex～改行コード前までを文字列に変換
            Dim s As String = Encoding.UTF8.GetString(byteArray, index, endLineIndex - index)
            index = endLineIndex + eol.Length
            Return s
        End If
    End Function

    'IPアドレスのコンボボックスが選択されるとQRコードでアップロードページのURLを表示
    Private Sub ComboBoxIPAddresses_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxIPAddresses.SelectedIndexChanged
        If ComboBoxIPAddresses.SelectedIndex < 0 Then
            PictureBoxQR.Image = Nothing
            LinkLabelQRInfo.Text = Nothing
            Exit Sub
        End If

        'QRコード作成の準備
        Dim qrcode = New BarcodeWriter
        With qrcode
            .Format = BarcodeFormat.QR_CODE
            qrcode.Options = New ZXing.QrCode.QrCodeEncodingOptions
            With qrcode.Options
                .Height = 160
                .Width = 160
                .Margin = 4
            End With
        End With

        'QRコード画像を生成、表示
        Dim ii As IpAddressInfo = ComboBoxIPAddresses.SelectedItem
        Dim url As String
        SyncLock arg
            url = "http://" & ii.address & ":"c & arg.port.ToString() & "/?PIN=" & arg.pin
        End SyncLock
        PictureBoxQR.Image = qrcode.Write(url)

        With LinkLabelQRInfo
            .ForeColor = Color.Black
            .Text = ii.address
            .Tag = url
        End With
    End Sub

    Public Class WorkerArg
        Public Enum STATUS
            RUNNING
            TERMINATED
            ERROR_TERMINATED
        End Enum
        Public Enum INSTRUCTION
            RUN
            TERMINATE
        End Enum
        Public arIpAddressInfo As IpAddressInfo()
        Public port As Integer
        Public imageDir As String
        Public stat As STATUS
        Public inst As INSTRUCTION
        Public lastLog As String
        Public logUpdated As Boolean
        Public pin As String
    End Class

    'ウェブサーバースレッド
    Private Sub ThreadMain1()

        '初期設定
        Dim listener As HttpListener = New HttpListener()
        SyncLock arg
            arg.stat = WorkerArg.STATUS.RUNNING

            Dim url As String = "http://localhost:" & arg.port.ToString() & "/"c
            listener.Prefixes.Add(url)
            Debug.Print("Listen " & url)

            '取得したIPv4アドレスへのアクセスを監視に追加する
            For Each ii As IpAddressInfo In arg.arIpAddressInfo
                url = "http://" & ii.address & ":"c & arg.port.ToString() & "/"c
                listener.Prefixes.Add(url)
                Debug.Print("Listen " & url)
            Next

            arg.lastLog = ""
            arg.logUpdated = False
        End SyncLock

        '権限エラーを回避するのにマニフェストが必要 level="requireAdministrator"
        '実行PC以外からアクセスするのにWindowsDefenderFirewallにて受信規則 TCP ポート8005を開放すべし

        listener.Start()

        Do
            '下記関数で受信待ちのためブロックされる
            Dim context As HttpListenerContext = listener.GetContext()

            SyncLock arg
                '終了指示を受け付ける
                If arg.inst = WorkerArg.INSTRUCTION.TERMINATE Then Exit Do
            End SyncLock

            Dim sbLog As New Text.StringBuilder
            With sbLog
                .Append("[Connected] ")
                .Append(Date.Now.ToLongTimeString())
                .Append(vbNewLine)
                .Append("From ")
                .Append(context.Request.RemoteEndPoint.ToString())
                .Append(vbNewLine)
                .Append("To   ")
                .Append(context.Request.LocalEndPoint.ToString())
                .Append(vbNewLine)
                .Append("URI  ")
                .Append(context.Request.Url.ToString())
                .Append(vbNewLine)
            End With
            Debug.Print("**Connected " & Date.Now.ToLongTimeString)
            Debug.Print("From " & context.Request.RemoteEndPoint.ToString())
            Debug.Print("To   " & context.Request.LocalEndPoint.ToString())
            Debug.Print("URI  " & context.Request.Url.ToString)

            Dim res As HttpListenerResponse
            Dim content As Byte()

            Dim from As String = context.Request.LocalEndPoint.ToString()
            Dim uri As String = context.Request.Url.ToString()
            Dim i As Integer = uri.IndexOf(from)
            Dim path As String = ""
            If i >= 0 Then
                path = uri.Substring(i + from.Length)
                Debug.Print("PATH: " & path)
                If path = "/favicon.ico" Then
                    'faviconは無いので404を返す
                    res = context.Response
                    res.StatusCode = 404
                    content = Encoding.UTF8.GetBytes("<!DOCTYPE html><html><body>404 Not found</body></html>")
                    res.OutputStream.Write(content, 0, content.Length)
                    res.Close()
                    Debug.Print("404 Not found")
                    Continue Do
                End If
            End If

            sbLog.Append("[Request Header]")
            sbLog.Append(vbNewLine)
            Debug.Print("[Request Header]")


            Dim content_length As Integer = -1
            Dim boundaryText As String = ""

            For Each nv In context.Request.Headers
                With sbLog
                    .Append(nv.ToString())
                    .Append(": ")
                    .Append(context.Request.Headers.Item(nv))
                    .Append(vbNewLine)
                End With
                Debug.Print("> " & nv.ToString() & ": " & context.Request.Headers.Item(nv))

                Dim value As String = context.Request.Headers.Item(nv)
                Select Case nv.ToString.ToUpper
                    Case "CONTENT-LENGTH"
                        content_length = Integer.Parse(value)
                    Case "CONTENT-TYPE"
                        If value.ToUpper.IndexOf("MULTIPART") >= 0 Then
                            i = value.ToUpper.IndexOf("BOUNDARY")
                            If i >= 0 Then
                                i = value.IndexOf("="c, i)
                                If i >= 0 Then
                                    boundaryText = value.Substring(i + 1).Trim
                                End If
                            End If
                        End If
                End Select
            Next

            Dim htmlMessage As String = ""
            If content_length > 0 AndAlso boundaryText.Length > 0 Then
                '画像がアップロードされたはず

                Dim bs(content_length - 1) As Byte
                Dim count As Integer = 0
                Dim err = False
                Do
                    Try
                        count += context.Request.InputStream.Read(bs, count, content_length - count)
                    Catch ex As Exception
                        With sbLog
                            .Append("Read ")
                            .Append(count.ToString())
                            .Append("/")
                            .Append(content_length.ToString())
                            .Append("bytes")
                            .Append(vbNewLine)
                            .Append("Exception occured")
                            .Append(vbNewLine)
                            .Append(ex.Message)
                            .Append(vbNewLine)
                        End With
                        Debug.Print("Read error - " & ex.Message)
                        err = True
                        Exit Do
                    End Try
                Loop While count < content_length

                If Not err Then
                    With sbLog
                        .Append("Read ")
                        .Append(count.ToString())
                        .Append("/")
                        .Append(content_length.ToString())
                        .Append("bytes")
                        .Append(vbNewLine)
                    End With
                End If

                If Not err AndAlso ProcessUplodedData(bs, boundaryText, sbLog) Then
                    htmlMessage = "<div style=""color:blue"">アップロードしました</div>"
                Else
                    htmlMessage = "<div style=""color:red"">アップロードできませんでした</div>"
                    err = True
                End If

                If Not err Then
                    sbLog.Append("Result: Success")
                    sbLog.Append(vbNewLine)
                Else
                    sbLog.Append("Result: Fail")
                    sbLog.Append(vbNewLine)
                End If
                SyncLock arg
                    arg.lastLog = sbLog.ToString()
                    arg.logUpdated = True
                End SyncLock
            End If

            res = context.Response
            res.StatusCode = 200
            SyncLock arg
                If path = "/?PIN=" & arg.pin Then
                    content = Encoding.UTF8.GetBytes(
                    "<!DOCTYPE html><html><head><meta charset=""utf-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1"">" &
                    "<title>PictureUploader</title></head>" &
                    "<body><h1>PictureUploader</h1>" & htmlMessage &
                    "<form action=""/?PIN=" & arg.pin & """ method=""post"" enctype=""multipart/form-data"">" &
                    "<p><input type=""file"" name=""upfile"" id=""upfile"" accept=""image/*"" /></p>" &
                    "<p><input type=""submit"" name=""save"" value=""アップロード"" /></p></form></body></html>")
                Else
                    content = Encoding.UTF8.GetBytes(
                    "<!DOCTYPE html><html><head><meta charset=""utf-8""><meta name=""viewport"" content=""width=device-width,initial-scale=1"">" &
                    "<title>PictureUploader</title></head>" &
                    "<body><h1>PictureUploader</h1>このアドレスは無効です<br>ＱＲコードを再スキャンしてください</body></html>")
                End If
            End SyncLock
            res.OutputStream.Write(content, 0, content.Length)
            res.Close()

        Loop

        SyncLock arg
            arg.stat = WorkerArg.STATUS.TERMINATED
        End SyncLock
    End Sub

    'アップロードされたMULTI-PARTを処理する
    Private Function ProcessUplodedData(ByRef bs As Byte(), boundaryText As String, ByRef sbLog As Text.StringBuilder) As Boolean

#If DEBUG Then
        'デバッグ用にファイルに保存
        System.IO.File.WriteAllBytes("received_content.binary", bs)
#End If

        sbLog.Append("[MULTI-PART]")
        sbLog.Append(vbNewLine)

        'boundaryの前に--を付加する
        Dim boundary As Byte() = Encoding.UTF8.GetBytes("--" & boundaryText)
        Dim startIndex As Integer = IndexOf(bs, boundary, 0)

        If startIndex < 0 Then
            '致命的エラー、最初のboundaryが見つからない
            Debug.Print("boundaryがひとつも見つからないw")
            sbLog.Append("Error. Boundary not found")
            sbLog.Append(vbNewLine)
            Return False
        End If

        Dim boundaryCount As Integer = 0
        Dim last As Boolean = False
        Do 'boundaryループ
            startIndex += boundary.Length
            If startIndex + 1 < bs.Length AndAlso bs(startIndex) = 13 AndAlso bs(startIndex + 1) = 10 Then
                'boundary直後の\r\nをスキップ
                startIndex += 2
            End If

            Dim endIndex As Integer = IndexOf(bs, boundary, startIndex)
            If endIndex < 0 Then
                '致命的エラー、最後ののboundaryが見つからない
                Debug.Print("最後のboundaryが見つからないw")
                sbLog.Append("Error. Last boundary not found")
                sbLog.Append(vbNewLine)
                Return False
            End If

            Dim i As Integer = endIndex + boundary.Length
            If i + 2 < bs.Length AndAlso bs(i) = &H2D AndAlso bs(i + 1) = &H2D Then '&H2D = Asc('-')
                '最後のboundaryが見つかった
                last = True
            End If

            'startIndex .. endIndex-1 までについて処理
            boundaryCount += 1
            Debug.Print("[Boundary " & boundaryCount.ToString() & "] index=" & startIndex.ToString() & " length=" & (endIndex - startIndex).ToString())

            Dim content_disposition As New Hashtable
            Dim content_type As String = ""
            Dim binarySize As Integer = 0

            Dim lineIndex As Integer = startIndex
            While lineIndex < endIndex 'boundary内の式を処理するループ
                Dim line As String = GetLine(bs, lineIndex) '処理後lineIndexは次の行の先頭を指す

                i = line.IndexOf(":"c)
                If i >= 1 Then
                    Select Case line.Substring(0, i).Trim.ToUpper
                        Case "CONTENT-DISPOSITION"
                            ';で分割し、式を抽出しcontent_dispositionに格納する。式の左側は大文字に変換
                            Static separator As Char() = {";"c}
                            For Each exp As String In line.Substring(i + 1).Split(separator)
                                Dim j As Integer = exp.IndexOf("="c)
                                If j < 0 Then
                                    '式に=が無い
                                    content_disposition.Add(exp.Trim.ToUpper, "")
                                Else
                                    '式に=が含まれる
                                    Dim w As String = exp.Substring(j + 1).Trim()
                                    If w.Length >= 2 AndAlso w(0) = """"c AndAlso w.Last = """"c Then
                                        '値が""で囲まれていたら取り除く
                                        w = w.Substring(1, w.Length - 2)
                                    End If
                                    content_disposition.Add(exp.Substring(0, j).Trim.ToUpper, w)
                                End If
                            Next
                        Case "CONTENT-TYPE"
                            content_type = line.Substring(i + 1).Trim
                    End Select
                End If

                If line.Length = 0 Then
                    '空行の後にバイナリデータが含まれる
                    binarySize = endIndex - lineIndex
                    If binarySize > 0 Then
                        Debug.Print("<begin binary data> " & binarySize.ToString() & "bytes")
                    End If
                    Exit While
                End If

                Debug.Print("> " & line)
                sbLog.Append(line)
                sbLog.Append(vbNewLine)
            End While

            If binarySize > 0 AndAlso content_disposition("NAME") = "upfile" Then
                'name="upfile"はアップロードされたファイルである

                Dim imageDir As String
                SyncLock arg
                    imageDir = arg.imageDir
                End SyncLock

                '1つ処理したら完了
                Return ExtractPicture(content_disposition, content_type, bs, lineIndex, binarySize, imageDir, sbLog)
            End If

            startIndex = endIndex
        Loop Until last
        Return False
    End Function

    Private Function ExtractPicture(ByRef content_disposition As Hashtable, content_type As String,
                                    ByRef bs As Byte(), startIndex As Integer, binarySize As Integer,
                                    imageDir As String, ByRef sbLog As Text.StringBuilder) As Boolean

        If Not IO.Directory.Exists(imageDir) Then
            '画像保存用のディレクトリを準備する
            With sbLog
                .Append("Create directory: ")
                .Append(imageDir)
                .Append(vbNewLine)
            End With
            Try
                IO.Directory.CreateDirectory(imageDir)
                Debug.Print("Directory created - " & imageDir)
            Catch ex As Exception
                Debug.Print("Directory create error - " & ex.Message)
                With sbLog
                    .Append("Exception occured")
                    .Append(vbNewLine)
                    .Append(ex.Message)
                    .Append(vbNewLine)
                End With
                Return False
            End Try
        End If

        Dim originalName As String = content_disposition("FILENAME")
        If originalName.Length = 0 Then
            'ファイル名が設定されていない
            Debug.Print("FILENAME not defined")
            sbLog.Append("Error. FILENAME not defined")
            sbLog.Append(vbNewLine)
            Return False
        End If

        'ファイル名から拡張子を取り除く(念のため)
        Dim ext As String = System.IO.Path.GetExtension(originalName)
        originalName = originalName.Substring(0, originalName.Length - ext.Length)

        'そしてContent-Typeの拡張子を使う
        Select Case content_type.ToUpper
            Case "IMAGE/JPEG"
                ext = ".jpg"
            Case "IMAGE/PNG"
                ext = ".png"
            Case "IMAGE/GIF"
                ext = ".gif"
            Case "IMAGE/BMP"
                ext = ".bmp"
            Case Else
                Debug.Print("Unknown Contet-Type=" & content_type)
                sbLog.Append("Error. Unknown Contet-Type: ")
                sbLog.Append(content_type)
                sbLog.Append(vbNewLine)
                Return False
        End Select

        Dim basePath As String = imageDir & "/" & originalName

        Dim path As String = basePath & ext
        Dim fileIndex As Integer = 1
        While IO.File.Exists(path)
            '重複している場合は名前を変更する
            fileIndex += 1
            path = basePath & "_" & fileIndex.ToString() & ext
        End While

        With sbLog
            .Append("Save to file: ")
            .Append(path)
            .Append(vbNewLine)
        End With

        Try
            Dim fs As New System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write)
            fs.Write(bs, startIndex, binarySize)
            fs.Close()
        Catch ex As Exception
            Debug.Print("File save error - " & ex.Message)
            With sbLog
                .Append("Exception occured")
                .Append(vbNewLine)
                .Append(ex.Message)
                .Append(vbNewLine)
            End With
            Return False
        End Try

        Return True
    End Function

    Private Sub LinkLabelQRInfo_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelQRInfo.LinkClicked
        If LinkLabelQRInfo.ForeColor = Color.Black Then
            Try
                Process.Start(LinkLabelQRInfo.Tag)
            Catch ex As Exception
                MsgBox("例外が発生しました" & vbNewLine & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "PictureUploader")
            End Try
        End If
    End Sub

    Private Sub PictureBoxQR_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBoxQR.MouseDown
        If e.Button And MouseButtons.Right Then
            ContextMenuStrip1.Show(PictureBoxQR.PointToScreen(e.Location))
        End If
    End Sub

    Private Sub OpenDir_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenDir_ToolStripMenuItem.Click
        Dim imageDir As String
        SyncLock arg
            imageDir = arg.imageDir
        End SyncLock

        If Not IO.Directory.Exists(imageDir) Then
            MsgBox("フォルダが存在しません。初めて実行した場合はアップロード後にフォルダが生成されます。", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "PictureUploader")
        Else
            Try
                Process.Start(imageDir)
            Catch ex As Exception
                MsgBox("例外が発生しました" & vbNewLine & ex.Message, MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "PictureUploader")
            End Try
        End If
    End Sub

    Private Sub Setting_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Setting_ToolStripMenuItem.Click
        Dim imageDir As String
        Dim port As Integer
        SyncLock arg
            imageDir = arg.imageDir
            port = arg.port
        End SyncLock

        Dim dlg As New FormSetting(imageDir, port)
        If dlg.ShowDialog() = DialogResult.OK AndAlso dlg.Modified Then

            Ini.WriteProfileString("Directory", "Image", dlg.ImageDir)
            Ini.WriteProfileString("Network", "Port", dlg.PortNo.ToString())
            MsgBox("設定を有効にするため、今すぐアプリケーションを再起動します", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "PictureUploader")
            Application.Restart()
        End If
    End Sub

    Private Sub LastLog_ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LastLog_ToolStripMenuItem.Click
        Dim dlg As New FormLog(arg)
        dlg.ShowDialog()
    End Sub
End Class
