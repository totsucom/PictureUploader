Imports System.Collections.Specialized
Imports System.IO
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Web

Public Class Form1

    Const MSGBOX_TITLE = "PiPデスクトップ"

    'フォルダで扱うファイルの拡張子
    Private ReadOnly extensions = {".PNG", ".JPEG", ".JPG", ".GIF"}

    'サムネイルのサイズ
    Const TWIDTH = 128
    Const THEIGHT = 128

    Private watcher As FileSystemWatcher

    'Delegate Sub RebuildThumbnailDelegate(ByVal FullPath As String)
    'Private vRebuildThumbnailDelegate As New RebuildThumbnailDelegate(AddressOf RebuildThumbnail)

    Delegate Sub CreateThumbnailDelegate(ByVal FullPath As String)
    Private vCreateThumbnailDelegate As New CreateThumbnailDelegate(AddressOf CreateThumbnail)

    Delegate Sub RemoveThumbnailDelegate(ByVal FullPath As String)
    Private vRemoveThumbnailDelegate As New RemoveThumbnailDelegate(AddressOf RemoveThumbnail)

    Delegate Sub RenameThumbnailDelegate(ByVal OldFullPath As String, ByVal NewFullPath As String)
    Private vRenameThumbnailDelegate As New RenameThumbnailDelegate(AddressOf RenameThumbnail)



    'PC,ユーザー固有文字列をBASE64エンコードしたもの
    'BASE64デコードして、utf8エンコードして、":"以降を取り出せばユーザー名文字列になる
    Private pcid As String = ""

    '16バイトの16進数文字列(32文字)
    Private key1 As String = ""

    Private pictureDirectory As String = ""

    'ネットワーク（サーバー）監視スレッド
    Private networkWorkerThread As Threading.Thread
    Private checkNetworkDataNow As Boolean = False
    Private isNetworkThreadRunning As Boolean = False
    Private reqTerminateNetworkThread As Boolean = False

    Delegate Sub SetToolStripStatusLabel1Delegate(ByVal Value As String)
    Private ToolStripStatusLabel1Delegate As New SetToolStripStatusLabel1Delegate(AddressOf SetToolStripStatusLabel1)


    Private sdata As New ShareData


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'If My.Settings.MyName.Length = 0 Then
        '    Dim myName As String

        '    Do
        '        myName = InputBox("このパソコンの名称を入力してください。" & vbNewLine & "20文字以下のわかりやすい名前で。", "PiPデスクトップ", My.User.Name, Me.Location.X, Me.Location.Y)
        '        If myName.Trim.Length = 0 Then
        '            Application.Exit()
        '            Exit Sub
        '        End If
        '        If myName.Trim.Length <= 20 Then Exit Do
        '    Loop
        '    My.Settings.MyName = myName.Trim
        '    My.Settings.Save()
        'End If

        My.User.InitializeWithWindowsUser() 'My.User.Nameが空文字を返す対処
        Me.Text = "PiPデスクトップ - " & My.User.Name
        Debug.Print("UserName=" & My.User.Name)

        If Not etc.GetMyInfo(pcid, key1) Then
            MsgBox("キーの生成エラー", vbAbort, MSGBOX_TITLE)
            Application.Exit()
            Exit Sub
        End If
        Debug.Print("pcid=" & pcid)
        Debug.Print("key1=" & key1)

        'マイドキュメント直下にディレクトリを設定する
        'Dim path As String = IO.Path.Combine(My.Application.Info.DirectoryPath, "downloaded")
        Dim path As String = IO.Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), "PipDownloaded")
        Try
            IO.Directory.CreateDirectory(path)
        Catch ex As Exception
            MsgBox("ディレクトリを作成できませんでした" & vbNewLine & path, vbAbort, MSGBOX_TITLE)
            Application.Exit()
            Exit Sub
        End Try
        pictureDirectory = path

        SetFolder(pictureDirectory)

        'folderWatchWorkerThread = New Threading.Thread(New Threading.ThreadStart(AddressOf FolderWatchThread))
        'folderWatchWorkerThread.IsBackground = True
        'folderWatchWorkerThread.Start()


        watcher = New FileSystemWatcher(pictureDirectory)
        AddHandler watcher.Changed, AddressOf OnChanged
        AddHandler watcher.Created, AddressOf OnCreated
        AddHandler watcher.Deleted, AddressOf OnDeleted
        AddHandler watcher.Renamed, AddressOf OnRenamed
        watcher.Filter = "*.*"
        watcher.IncludeSubdirectories = False
        watcher.EnableRaisingEvents = True


        networkWorkerThread = New Threading.Thread(New Threading.ThreadStart(AddressOf NetworkThread))
        networkWorkerThread.IsBackground = True
        networkWorkerThread.Start()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        If watcher IsNot Nothing Then
            watcher.Dispose()
            watcher = Nothing
        End If

        If networkWorkerThread IsNot Nothing Then
            reqTerminateNetworkThread = True
            While isNetworkThreadRunning
                Threading.Thread.Sleep(50)
            End While
            networkWorkerThread.Abort()
        End If
    End Sub


    'QRコードアイコンがクリックされた
    Private Sub ToolStripButtonQRCode_Click(sender As Object, e As EventArgs) Handles ToolStripButtonQRCode.Click
        Dim dlg As New FormQRCode(etc.CreateQrCodeParam(pcid, key1), Me.Location)
        dlg.Show()
    End Sub

    'リロードボタンがクリックされた
    Private Sub ToolStripButtonSync_Click(sender As Object, e As EventArgs) Handles ToolStripButtonSync.Click
        SetFolder(pictureDirectory)
        checkNetworkDataNow = True
    End Sub


    Shared Function dummy() As Boolean
        Return False ' このメソッドの内容は何でもよい
    End Function

    Private Sub SetFolder(dirPath As String)
        ListView1.BeginUpdate()
        ListView1.Items.Clear()

        For Each filePath In IO.Directory.GetFiles(dirPath, "*.*", System.IO.SearchOption.TopDirectoryOnly)
            BuildThumbnail(filePath)
        Next

        ListView1.EndUpdate()
    End Sub

    ''サムネイルの再構築
    'Private Sub RebuildThumbnail(FullPath As String)
    '    Dim name As String = IO.Path.GetFileName(FullPath)
    '    For Each item As ListViewItem In ListView1.Items
    '        If item.Text = name Then
    '            Dim fs = New IO.FileStream(FullPath, IO.FileMode.Open, IO.FileAccess.Read)
    '            Dim img As Bitmap = Image.FromStream(fs).GetThumbnailImage(TWIDTH, THEIGHT, New Image.GetThumbnailImageAbort(AddressOf dummy), IntPtr.Zero)
    '            fs.Close()
    '            ImageList1.Images(item.ImageIndex) = img
    '            img.Dispose()
    '            ListView1.Refresh()
    '            Exit For
    '        End If
    '    Next
    'End Sub
    Private Sub OnChanged(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        If e.ChangeType <> WatcherChangeTypes.Changed Then Exit Sub

        Debug.Print("OnChanged:" & e.FullPath)
        Me.Invoke(vCreateThumbnailDelegate, New Object() {e.FullPath})
        Dim value As String = $"Created: {e.FullPath}"
        Console.WriteLine(value)
    End Sub

    'サムネイルの追加または更新
    Private Sub BuildThumbnail(FullPath As String)
        Dim ext As String = IO.Path.GetExtension(FullPath).ToUpper
        Dim name As String = IO.Path.GetFileName(FullPath)
        If Array.IndexOf(extensions, ext) >= 0 Then
            Dim fs As IO.FileStream = Nothing
            Dim src As Image = Nothing
            Dim img As Bitmap = Nothing
            Try
                fs = New IO.FileStream(FullPath, IO.FileMode.Open, IO.FileAccess.Read)
                src = Image.FromStream(fs)
                img = src.GetThumbnailImage(TWIDTH, THEIGHT, New Image.GetThumbnailImageAbort(AddressOf dummy), IntPtr.Zero)
                fs.Close()

                Dim lvi As ListViewItem = Nothing
                Do
                    If lvi Is Nothing Then
                        lvi = ListView1.FindItemWithText(name)
                    Else
                        lvi = ListView1.FindItemWithText(name, False, lvi.Index + 1)
                    End If
                    If lvi Is Nothing Then Exit Do
                    If lvi.Text = name Then Exit Do
                Loop
                If lvi Is Nothing Then
                    ImageList1.Images.Add(img)
                    lvi = New ListViewItem(name, ImageList1.Images.Count - 1)
                    lvi.Tag = New FileInfo(src.Size, New IO.FileInfo(FullPath).Length)
                    ListView1.Items.Insert(0, lvi)

                    SyncLock sdata
                        If sdata.DownloadedFile <> "" AndAlso IO.Path.GetFileName(sdata.DownloadedFile) = name Then
                            '新規ダウンロードは自動選択（しないことにした）
                            'lvi.Selected = True
                            'ListView1.EnsureVisible(lvi.Index)
                            sdata.DownloadedFile = ""
                        End If
                    End SyncLock
                Else
                    ImageList1.Images(lvi.ImageIndex) = img
                    lvi.Tag = New FileInfo(src.Size, New IO.FileInfo(FullPath).Length)
                    ListView1.RedrawItems(lvi.Index, lvi.Index, False)
                End If

                src.Dispose()
                img.Dispose()
            Catch ex As Exception
            Finally
                If fs IsNot Nothing Then fs.Close()
                If img IsNot Nothing Then img.Dispose()
            End Try
        End If
    End Sub
    Private Async Sub CreateThumbnail(FullPath As String)
        Await Task.Delay(1000) 'ファイルロック回避
        BuildThumbnail(FullPath)
    End Sub
    Private Sub OnCreated(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        Debug.Print("OnCreated:" & e.FullPath)
        Me.Invoke(vCreateThumbnailDelegate, New Object() {e.FullPath})
        Dim value As String = $"Created: {e.FullPath}"
        Console.WriteLine(value)
    End Sub

    'サムネイルの削除
    Private Sub RemoveThumbnail(FullPath As String)
        Dim name As String = IO.Path.GetFileName(FullPath)
        For i As Integer = 0 To ListView1.Items.Count - 1
            If ListView1.Items(i).Text = name Then
                ListView1.Items.RemoveAt(i)
                Exit For
            End If
        Next
    End Sub
    Private Sub OnDeleted(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        Debug.Print("OnDeleted:" & e.FullPath)
        Me.Invoke(vRemoveThumbnailDelegate, New Object() {e.FullPath})
        Console.WriteLine($"Deleted: {e.FullPath}")
    End Sub

    'サムネイルのリネーム
    Private Sub RenameThumbnail(OldFullPath As String, NewFullPath As String)
        Dim name As String = IO.Path.GetFileName(OldFullPath)
        For Each item As ListViewItem In ListView1.Items
            If item.Text = name Then
                item.Text = IO.Path.GetFileName(NewFullPath)
                Exit For
            End If
        Next
    End Sub
    Private Sub OnRenamed(ByVal sender As Object, ByVal e As RenamedEventArgs)
        Me.Invoke(vRenameThumbnailDelegate, New Object() {e.OldFullPath, e.FullPath})
        Debug.Print("OnRenamed:" & e.FullPath)
        Console.WriteLine($"Renamed:")
        Console.WriteLine($"    Old: {e.OldFullPath}")
        Console.WriteLine($"    New: {e.FullPath}")
    End Sub


    'ステータステキストの設定（Delegateするメソッド）
    Private Sub SetToolStripStatusLabel1(ByVal Value As String)
        ToolStripStatusLabel1.Text = Value
    End Sub

    Private Function LoadFromNetwork(ByRef hasErr As Boolean, ByRef hasNext As Boolean, ByRef path As String) As String
        hasErr = False
        hasNext = False
        path = ""

        'サーバーに問い合わせ
        Dim url = "http://mae8bit.php.xdomain.jp/get.php?usr="
        url &= HttpUtility.UrlEncode(pcid) '自身のID
        url &= "&pw="
        url &= password.forWebServer 'Webサーバー側のPHPコードとの通信で使う固定の共通パスワード

        Dim webreq As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(url), System.Net.HttpWebRequest)
        Dim code As HttpStatusCode
        Dim source As String = ""
        Dim webres As System.Net.HttpWebResponse = Nothing
        Dim st As System.IO.Stream = Nothing
        Dim sr As System.IO.StreamReader = Nothing
        Try
            '参考 https://dobon.net/vb/dotnet/internet/webrequest.html

            webres = DirectCast(webreq.GetResponse(), System.Net.HttpWebResponse)
            code = webres.StatusCode
            If code = HttpStatusCode.OK Then
                st = webres.GetResponseStream()
                sr = New System.IO.StreamReader(st, System.Text.Encoding.ASCII)
                source = sr.ReadToEnd()
            End If
            Debug.Print(Date.Now.ToString("HH:mm") & code.ToString)
        Catch ex As Exception
            Debug.Print(ex.Message)
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " サーバーに接続できませんでした(例外)"
        Finally
            If sr IsNot Nothing Then sr.Close()
            If st IsNot Nothing Then st.Close()
            If webres IsNot Nothing Then webres.Close()
        End Try

        If code = HttpStatusCode.NoContent Then
            'データは無い
            Return ""
        End If

        If code <> HttpStatusCode.OK Then
            '想定外のエラー
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " サーバーに接続できませんでした(" & Int(code).ToString & ")"
        End If

        If source.Length <= (2 + 12 + 32) Then
            'データ長のエラー
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " データエラー(1)"
        End If

        '次のデータの有無
        Dim s As String = source.Substring(0, 2)
        If s <> "00" AndAlso s <> "01" Then
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " データエラー(2)"
        End If
        hasNext = (s = "01")

        Dim img As Image = Nothing
        Dim ext As String = ""
        Dim i As Integer
        Try
            'アップロード日時
            s = source.Substring(2, 12)
            Dim uploadedTime As DateTime = New DateTime(
                Integer.Parse(s.Substring(0, 2)) + 2000,
                Integer.Parse(s.Substring(2, 2)),
                Integer.Parse(s.Substring(4, 2)),
                Integer.Parse(s.Substring(6, 2)),
                Integer.Parse(s.Substring(8, 2)),
                Integer.Parse(s.Substring(10, 2)))

            Dim k2 As Byte() = etc.Hex2Array(source.Substring(14, 32))
            For i = 0 To k2.Length - 1
                k2(i) = ((k2(i) And 3) << 6) Or ((k2(i) >> 2) And &H3F)
            Next

            Dim bs As Byte() = etc.Decrypt(System.Convert.FromBase64String(source.Substring(46)), etc.Hex2Array(key1), k2)
            img = Image.FromStream(New IO.MemoryStream(bs))

            Debug.Print("6")

            If img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp) Then
                ext = ".bmp"
            ElseIf img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif) Then
                ext = ".gif"
            ElseIf img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg) Then
                ext = ".jpg"
            ElseIf img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png) Then
                ext = ".png"
            End If

            If ext = "" Then
                If img IsNot Nothing Then img.Dispose()
                hasErr = True
                Return Date.Now.ToString("HH:mm") & " 未対応の画像形式"
            End If
        Catch ex As Exception
            If img IsNot Nothing Then img.Dispose()
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " データ変換エラー"
        End Try

        i = 0
        Dim fileName As String
        Do
            fileName = s & IIf(i = 0, "", "(" & i.ToString & ")") & ext
            path = IO.Path.Combine(pictureDirectory, fileName)
            If Not IO.File.Exists(path) Then Exit Do
            i += 1
        Loop
        Try
            SyncLock sdata
                sdata.DownloadedFile = path
            End SyncLock
            img.Save(path)
        Catch ex As Exception
            hasErr = True
            Return Date.Now.ToString("HH:mm") & " ファイル保存エラー"
        End Try
        img.Dispose()

        Return Date.Now.ToString("HH:mm") & " " & fileName
    End Function

    Private Sub NetworkThread()
        isNetworkThreadRunning = True

        Dim cdNextCheck As Integer = 30 '3sec

        While Not reqTerminateNetworkThread
            Threading.Thread.Sleep(100)
            cdNextCheck -= 1
            If Not checkNetworkDataNow AndAlso cdNextCheck > 0 Then Continue While

            Me.Invoke(ToolStripStatusLabel1Delegate, New Object() {"サーバーを確認しています..."})
            Debug.Print("processing")
            Dim hasErr As Boolean = False
            Dim hasNext As Boolean = False
            Dim path As String = ""
            Dim statusText As String = LoadFromNetwork(hasErr, hasNext, path)
            Debug.Print("end")

            If reqTerminateNetworkThread Then Exit While
            If statusText.Length > 0 Then
                Me.Invoke(ToolStripStatusLabel1Delegate, New Object() {statusText})
            Else
                Me.Invoke(ToolStripStatusLabel1Delegate, New Object() {"Ready"})
            End If

            checkNetworkDataNow = False
            cdNextCheck = IIf(hasNext, 30, 150) '3sec / 15sec
        End While

        isNetworkThreadRunning = False
    End Sub

    Private Sub ToolStripButtonOpenFolder_Click(sender As Object, e As EventArgs) Handles ToolStripButtonOpenFolder.Click
        Process.Start("EXPLORER.EXE", pictureDirectory)
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count <> 1 Then Exit Sub
        Process.Start(IO.Path.Combine({pictureDirectory, ListView1.SelectedItems(0).Text}))
    End Sub

    Private Sub ToolStripButtonFileCut_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFileCut.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub
        Dim files As New List(Of String)
        For Each item As ListViewItem In ListView1.SelectedItems
            files.Add(IO.Path.Combine({pictureDirectory, item.Text}))
        Next
        Dim data As IDataObject = New DataObject(DataFormats.FileDrop, files.ToArray)

        'DragDropEffects.Moveを設定する（DragDropEffects.Move は 2）
        Dim bs As Byte() = New Byte() {CByte(DragDropEffects.Move), 0, 0, 0}
        Dim ms As System.IO.MemoryStream = New System.IO.MemoryStream(bs)
        data.SetData("Preferred DropEffect", ms)
        Clipboard.SetDataObject(data)
    End Sub

    Private Sub ToolStripButtonFileCopy_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFileCopy.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub
        Dim files As New System.Collections.Specialized.StringCollection()
        For Each item As ListViewItem In ListView1.SelectedItems
            files.Add(IO.Path.Combine({pictureDirectory, item.Text}))
        Next
        Clipboard.SetFileDropList(files)
    End Sub

    Private Sub ToolStripButtonRotateLeft_Click(sender As Object, e As EventArgs) Handles ToolStripButtonRotateLeft.Click
        Try
            For Each item As ListViewItem In ListView1.SelectedItems
                Dim path As String = IO.Path.Combine({pictureDirectory, item.Text})
                Dim fs = New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
                Dim img As Bitmap = Image.FromStream(fs)
                Dim imageSize As Long = fs.Length
                fs.Close()
                img.RotateFlip(RotateFlipType.Rotate270FlipNone)

                If Not SaveJpegFile(img, path, imageSize) Then
                    MsgBox("ファイルに保存できませんでした", vbExclamation, MSGBOX_TITLE)
                End If
                img.Dispose()
            Next
        Catch ex As Exception
            MsgBox("回転処理中にエラーが発生しました", vbExclamation, MSGBOX_TITLE)
        End Try
    End Sub

    Private Sub ToolStripButtonRotateRight_Click(sender As Object, e As EventArgs) Handles ToolStripButtonRotateRight.Click
        Try
            For Each item As ListViewItem In ListView1.SelectedItems
                Dim path As String = IO.Path.Combine({pictureDirectory, item.Text})
                Dim fs = New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
                Dim img As Bitmap = Image.FromStream(fs)
                Dim imageSize As Long = fs.Length
                fs.Close()
                img.RotateFlip(RotateFlipType.Rotate90FlipNone)

                If Not SaveJpegFile(img, path, imageSize) Then
                    MsgBox("ファイルに保存できませんでした", vbExclamation, MSGBOX_TITLE)
                End If
                img.Dispose()
            Next
        Catch ex As Exception
            MsgBox("回転処理中にエラーが発生しました", vbExclamation, MSGBOX_TITLE)
        End Try
    End Sub

    'PiPアンドロイドの品質設定 70,80,90%のうち、targetSizeに最も近い品質設定でファイルに保存する
    Private Shared Function SaveJpegFile(ByRef img As Image, path As String, targetSize As Long) As Boolean

        '品質80%と、70または90%の２パターンでメモリに書き出す
        Dim quality() As Integer = {80, 0}
        Dim ms(1) As MemoryStream
        Dim i As Integer
        For i = 0 To 1
            ms(i) = New MemoryStream()
            Dim ici As Imaging.ImageCodecInfo = GetEncoderInfo(Imaging.ImageFormat.Jpeg)
            Dim eps As New Imaging.EncoderParameters(1)
            Dim ep As New Imaging.EncoderParameter(Imaging.Encoder.Quality, CLng(quality(i)))
            eps.Param(0) = ep
            img.Save(ms(i), ici, eps)
            If i = 0 Then quality(1) = IIf(targetSize < ms.Length, 90, 70)
        Next

        'サイズの近い品質インデックスを得る
        i = IIf(Math.Pow(targetSize - ms(0).Length, 2) < Math.Pow(targetSize - ms(1).Length, 2), 0, 1)
        Debug.Print("Jpeg品質は" & quality(i).ToString & "%が選択されました")

        Try
            Using fs As New FileStream(path, FileMode.Create, FileAccess.Write)
                ms(i).WriteTo(fs)
            End Using
        Catch ex As Exception
            ms(0).Close()
            ms(1).Close()
            Return False
        End Try

        ms(0).Close()
        ms(1).Close()
        Return True
    End Function

    Private Shared Function GetEncoderInfo(f As Imaging.ImageFormat) As System.Drawing.Imaging.ImageCodecInfo
        Dim encs As Imaging.ImageCodecInfo() =
        Imaging.ImageCodecInfo.GetImageEncoders()
        Dim enc As Imaging.ImageCodecInfo
        For Each enc In encs
            If enc.FormatID = f.Guid Then
                Return enc
            End If
        Next
        Return Nothing
    End Function

    Private Sub ToolStripButtonFileDelete_Click(sender As Object, e As EventArgs) Handles ToolStripButtonFileDelete.Click
        If ListView1.SelectedItems.Count = 0 Then Exit Sub
        If ListView1.SelectedItems.Count = 1 Then
            If MsgBox("本当に削除しますか？ " & ListView1.SelectedItems(0).Text, vbQuestion Or vbOKCancel, MSGBOX_TITLE) <> vbOK Then Exit Sub
        Else
            If MsgBox("本当に削除しますか？ " & ListView1.SelectedItems.Count.ToString & "ファイル", vbQuestion Or vbOKCancel, MSGBOX_TITLE) <> vbOK Then Exit Sub
        End If
        For Each item As ListViewItem In ListView1.SelectedItems
            IO.File.Delete(IO.Path.Combine({pictureDirectory, item.Text}))
        Next
    End Sub

    Private Sub ToolStripButtonImageCopy_Click(sender As Object, e As EventArgs) Handles ToolStripButtonImageCopy.Click
        If ListView1.SelectedItems.Count <> 1 Then Exit Sub
        Dim path = IO.Path.Combine({pictureDirectory, ListView1.SelectedItems(0).Text})
        Dim fs As IO.FileStream = Nothing
        Dim img As Image = Nothing
        Try
            fs = New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
            img = Image.FromStream(fs)

            'イメージのコピーは容量を食うので半分のサイズのビットマップにした
            Dim canvas As New Bitmap(CInt(img.Width / 2), CInt(img.Height / 2))
            Dim g As Graphics = Graphics.FromImage(canvas)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(img, 0, 0, canvas.Width, canvas.Height)

            Clipboard.SetImage(canvas)

            g.Dispose()
            canvas.Dispose()
            img.Dispose()
        Catch ex As Exception
        Finally
            If fs IsNot Nothing Then fs.Close()
            If img IsNot Nothing Then img.Dispose()
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim n = ListView1.SelectedItems.Count
        ToolStripButtonFileCut.Enabled = (n > 0)
        ToolStripButtonFileCopy.Enabled = (n > 0)
        ToolStripButtonImageCopy.Enabled = (n = 1)
        ToolStripButtonRotateLeft.Enabled = (n > 0)
        ToolStripButtonRotateRight.Enabled = (n > 0)
        ToolStripButtonFileDelete.Enabled = (n > 0)

        Dim s As String
        If n = 1 Then
            Dim fi As FileInfo = ListView1.SelectedItems(0).Tag
            s = fi.ImageSize.Width & "×" & fi.ImageSize.Height & "   " & (fi.FileSize / 1000).ToString("#,0") & "KB"
        ElseIf n > 1 Then
            Dim sz As Long = 0
            For Each lvi As ListViewItem In ListView1.SelectedItems
                Dim fi As FileInfo = lvi.Tag
                sz += fi.FileSize
            Next
            s = ListView1.SelectedItems.Count & "ファイル  " & (sz / 1000).ToString("#,0") & "KB"
        Else
            s = ""
        End If
        ToolStripStatusLabel2.Text = s
    End Sub

    Private Sub ListView1_MouseClick(sender As Object, e As MouseEventArgs) Handles ListView1.MouseClick
        If e.Button <> MouseButtons.Right Then Exit Sub
        ContextMenuStrip1.Show(Me.PointToScreen(e.Location))
    End Sub

    Private Sub ToolStripMenuItemSelAll_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSelAll.Click
        For Each item As ListViewItem In ListView1.Items
            item.Selected = True
        Next
    End Sub

    Private Sub ToolStripMenuItemOpen_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemOpen.Click
        For Each item As ListViewItem In ListView1.SelectedItems
            Process.Start(IO.Path.Combine({pictureDirectory, item.Text}))
        Next
    End Sub

    Private Sub ToolStripMenuItemEdit_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemEdit.Click
        For Each item As ListViewItem In ListView1.SelectedItems
            Process.Start("mspaint.exe", IO.Path.Combine({pictureDirectory, item.Text}))
        Next
    End Sub

    Private Sub ToolStripMenuItemCut_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemCut.Click
        ToolStripButtonFileCut_Click(sender, e)
    End Sub

    Private Sub ToolStripMenuItemCopy_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemCopy.Click
        ToolStripButtonFileCopy_Click(sender, e)
    End Sub

    Private Sub ToolStripMenuItemCopyImage_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemCopyImage.Click
        ToolStripButtonImageCopy_Click(sender, e)
    End Sub

    Private Sub ToolStripMenuItemRotLeft_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemRotLeft.Click
        ToolStripButtonRotateLeft.PerformClick()
    End Sub

    Private Sub ToolStripMenuItemRotRight_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemRotRight.Click
        ToolStripButtonRotateRight.PerformClick()
    End Sub

    Private Sub ToolStripMenuItemDelete_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemDelete.Click
        ToolStripButtonFileDelete_Click(sender, e)
    End Sub

    Private Sub ToolStripButtonInfo_Click(sender As Object, e As EventArgs) Handles ToolStripButtonInfo.Click
        Dim dlg As New FormInfo
        dlg.ShowDialog()
        dlg.Dispose()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control Then
            Select Case e.KeyCode
                Case Keys.A
                    For Each item As ListViewItem In ListView1.Items
                        item.Selected = True
                    Next
                    e.Handled = True
                Case Keys.X
                    ToolStripButtonFileCut.PerformClick()
                    e.Handled = True
                Case Keys.C
                    ToolStripButtonFileCopy.PerformClick()
                    e.Handled = True
                Case Keys.I
                    ToolStripButtonImageCopy.PerformClick()
                    e.Handled = True
            End Select
        Else
            Select Case e.KeyData
                Case Keys.F5
                    ToolStripButtonSync.PerformClick()
                    e.Handled = True
                Case Keys.Delete
                    ToolStripButtonFileDelete.PerformClick()
                    e.Handled = True
                Case Keys.Enter
                    ToolStripMenuItemOpen.PerformClick()
                    e.Handled = True
            End Select
        End If
    End Sub

    Private doDrag As Boolean = False

    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove
        If doDrag Then Exit Sub
        If e.Button <> MouseButtons.Left OrElse ListView1.SelectedItems.Count = 0 Then
            doDrag = False
            Exit Sub
        End If

        If RadioButtonDragFile.Checked Then
            'ファイルをドラッグ

            Dim ar As New List(Of String)
            For Each item As ListViewItem In ListView1.SelectedItems
                ar.Add(IO.Path.Combine({pictureDirectory, item.Text}))
            Next
            Dim dde As DragDropEffects = ListView1.DoDragDrop(New DataObject(DataFormats.FileDrop, ar.ToArray()), DragDropEffects.All)

            doDrag = True
        Else
            If ListView1.SelectedItems.Count <> 1 Then
                doDrag = False
                Exit Sub
            End If

            'イメージをドラッグ

            Dim path = IO.Path.Combine({pictureDirectory, ListView1.SelectedItems(0).Text})
            Dim fs As IO.FileStream = Nothing
            Dim img As Image = Nothing
            Try
                fs = New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
                img = Image.FromStream(fs)

                'イメージのコピーは容量を食うので半分のサイズのビットマップにした
                Dim canvas As New Bitmap(CInt(img.Width / 2), CInt(img.Height / 2))
                Dim g As Graphics = Graphics.FromImage(canvas)
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.DrawImage(img, 0, 0, canvas.Width, canvas.Height)

                Dim dde As DragDropEffects = ListView1.DoDragDrop(New DataObject(DataFormats.Bitmap, canvas), DragDropEffects.Copy)

                g.Dispose()
                canvas.Dispose()
                img.Dispose()
            Catch ex As Exception
            Finally
                If fs IsNot Nothing Then fs.Close()
                If img IsNot Nothing Then img.Dispose()
            End Try

        End If
    End Sub

    Private Sub ListView1_MouseLeave(sender As Object, e As EventArgs) Handles ListView1.MouseLeave
        doDrag = False
    End Sub



End Class

Class FileInfo
    Public ImageSize As Size
    Public FileSize As Long
    Public Sub New(imageSize As Size, fileSize As Long)
        Me.ImageSize = imageSize
        Me.FileSize = fileSize
    End Sub
End Class

Class ShareData
    Public DownloadedFile As String
End Class