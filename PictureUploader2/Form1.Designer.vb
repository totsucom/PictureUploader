<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonQRCode = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonSync = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonOpenFolder = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonFileCut = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFileCopy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonImageCopy = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonRotateLeft = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonRotateRight = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButtonFileDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonInfo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemSelAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemCut = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCopyImage = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemRotLeft = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemRotRight = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItemDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RadioButtonDragFile = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDragImage = New System.Windows.Forms.RadioButton()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonQRCode, Me.ToolStripButtonSync, Me.ToolStripSeparator1, Me.ToolStripButtonOpenFolder, Me.ToolStripSeparator2, Me.ToolStripButtonFileCut, Me.ToolStripButtonFileCopy, Me.ToolStripButtonImageCopy, Me.ToolStripButtonRotateLeft, Me.ToolStripButtonRotateRight, Me.ToolStripSeparator3, Me.ToolStripButtonFileDelete, Me.ToolStripButtonInfo, Me.ToolStripSeparator6})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(459, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonQRCode
        '
        Me.ToolStripButtonQRCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonQRCode.Image = Global.PictureUploader2.My.Resources.Resources.qr_code
        Me.ToolStripButtonQRCode.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonQRCode.Name = "ToolStripButtonQRCode"
        Me.ToolStripButtonQRCode.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonQRCode.Text = "ToolStripButton1"
        Me.ToolStripButtonQRCode.ToolTipText = "QRコードを表示"
        '
        'ToolStripButtonSync
        '
        Me.ToolStripButtonSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonSync.Image = Global.PictureUploader2.My.Resources.Resources.refresh_arrow
        Me.ToolStripButtonSync.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSync.Name = "ToolStripButtonSync"
        Me.ToolStripButtonSync.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonSync.Text = "ToolStripButton2"
        Me.ToolStripButtonSync.ToolTipText = "リフレッシュ"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButtonOpenFolder
        '
        Me.ToolStripButtonOpenFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonOpenFolder.Image = Global.PictureUploader2.My.Resources.Resources.folder_white_shape
        Me.ToolStripButtonOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonOpenFolder.Name = "ToolStripButtonOpenFolder"
        Me.ToolStripButtonOpenFolder.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonOpenFolder.Text = "ToolStripButton3"
        Me.ToolStripButtonOpenFolder.ToolTipText = "エクスプローラーで開く"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButtonFileCut
        '
        Me.ToolStripButtonFileCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFileCut.Image = Global.PictureUploader2.My.Resources.Resources.cut
        Me.ToolStripButtonFileCut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFileCut.Name = "ToolStripButtonFileCut"
        Me.ToolStripButtonFileCut.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFileCut.Text = "ToolStripButton1"
        Me.ToolStripButtonFileCut.ToolTipText = "切り取り"
        '
        'ToolStripButtonFileCopy
        '
        Me.ToolStripButtonFileCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFileCopy.Image = Global.PictureUploader2.My.Resources.Resources.copy_document
        Me.ToolStripButtonFileCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFileCopy.Name = "ToolStripButtonFileCopy"
        Me.ToolStripButtonFileCopy.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFileCopy.Text = "ToolStripButton4"
        Me.ToolStripButtonFileCopy.ToolTipText = "ファイルコピー"
        '
        'ToolStripButtonImageCopy
        '
        Me.ToolStripButtonImageCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonImageCopy.Image = Global.PictureUploader2.My.Resources.Resources.copy
        Me.ToolStripButtonImageCopy.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonImageCopy.Name = "ToolStripButtonImageCopy"
        Me.ToolStripButtonImageCopy.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonImageCopy.Text = "ToolStripButton1"
        Me.ToolStripButtonImageCopy.ToolTipText = "イメージをコピー"
        '
        'ToolStripButtonRotateLeft
        '
        Me.ToolStripButtonRotateLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRotateLeft.Image = Global.PictureUploader2.My.Resources.Resources.rotate_left_circular_arrow_interface_symbol
        Me.ToolStripButtonRotateLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRotateLeft.Name = "ToolStripButtonRotateLeft"
        Me.ToolStripButtonRotateLeft.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRotateLeft.Text = "ToolStripButton1"
        Me.ToolStripButtonRotateLeft.ToolTipText = "反時計回りに90度回転"
        '
        'ToolStripButtonRotateRight
        '
        Me.ToolStripButtonRotateRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRotateRight.Image = Global.PictureUploader2.My.Resources.Resources.rotate_right_circular_arrow_interface_symbol
        Me.ToolStripButtonRotateRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRotateRight.Name = "ToolStripButtonRotateRight"
        Me.ToolStripButtonRotateRight.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRotateRight.Text = "ToolStripButton1"
        Me.ToolStripButtonRotateRight.ToolTipText = "時計回りに90度回転"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButtonFileDelete
        '
        Me.ToolStripButtonFileDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonFileDelete.Image = Global.PictureUploader2.My.Resources.Resources.trash
        Me.ToolStripButtonFileDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFileDelete.Name = "ToolStripButtonFileDelete"
        Me.ToolStripButtonFileDelete.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonFileDelete.Text = "ToolStripButton1"
        Me.ToolStripButtonFileDelete.ToolTipText = "ファイル削除"
        '
        'ToolStripButtonInfo
        '
        Me.ToolStripButtonInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonInfo.Image = Global.PictureUploader2.My.Resources.Resources.information_button
        Me.ToolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonInfo.Name = "ToolStripButtonInfo"
        Me.ToolStripButtonInfo.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonInfo.Text = "ToolStripButton1"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'ListView1
        '
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListView1.HideSelection = False
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 25)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(5)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(459, 408)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(128, 128)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 433)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(2, 0, 23, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(459, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(403, 17)
        Me.ToolStripStatusLabel1.Spring = True
        Me.ToolStripStatusLabel1.Text = "Ready"
        Me.ToolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(0, 17)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemSelAll, Me.ToolStripMenuItemOpen, Me.ToolStripMenuItemEdit, Me.ToolStripSeparator5, Me.ToolStripMenuItemCut, Me.ToolStripMenuItemCopy, Me.ToolStripMenuItemCopyImage, Me.ToolStripMenuItemRotLeft, Me.ToolStripMenuItemRotRight, Me.ToolStripSeparator4, Me.ToolStripMenuItemDelete})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(188, 214)
        '
        'ToolStripMenuItemSelAll
        '
        Me.ToolStripMenuItemSelAll.Name = "ToolStripMenuItemSelAll"
        Me.ToolStripMenuItemSelAll.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.ToolStripMenuItemSelAll.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemSelAll.Text = "全て選択"
        '
        'ToolStripMenuItemOpen
        '
        Me.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen"
        Me.ToolStripMenuItemOpen.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemOpen.Text = "開く"
        '
        'ToolStripMenuItemEdit
        '
        Me.ToolStripMenuItemEdit.Name = "ToolStripMenuItemEdit"
        Me.ToolStripMenuItemEdit.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemEdit.Text = "編集"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(184, 6)
        '
        'ToolStripMenuItemCut
        '
        Me.ToolStripMenuItemCut.Image = Global.PictureUploader2.My.Resources.Resources.cut
        Me.ToolStripMenuItemCut.Name = "ToolStripMenuItemCut"
        Me.ToolStripMenuItemCut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ToolStripMenuItemCut.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemCut.Text = "切り取り"
        '
        'ToolStripMenuItemCopy
        '
        Me.ToolStripMenuItemCopy.Image = Global.PictureUploader2.My.Resources.Resources.copy_document
        Me.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy"
        Me.ToolStripMenuItemCopy.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.ToolStripMenuItemCopy.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemCopy.Text = "ファイルコピー"
        '
        'ToolStripMenuItemCopyImage
        '
        Me.ToolStripMenuItemCopyImage.Image = Global.PictureUploader2.My.Resources.Resources.copy
        Me.ToolStripMenuItemCopyImage.Name = "ToolStripMenuItemCopyImage"
        Me.ToolStripMenuItemCopyImage.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.ToolStripMenuItemCopyImage.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemCopyImage.Text = "イメージをコピー"
        '
        'ToolStripMenuItemRotLeft
        '
        Me.ToolStripMenuItemRotLeft.Image = Global.PictureUploader2.My.Resources.Resources.rotate_left_circular_arrow_interface_symbol
        Me.ToolStripMenuItemRotLeft.Name = "ToolStripMenuItemRotLeft"
        Me.ToolStripMenuItemRotLeft.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemRotLeft.Text = "反時計回りに90度回転"
        '
        'ToolStripMenuItemRotRight
        '
        Me.ToolStripMenuItemRotRight.Image = Global.PictureUploader2.My.Resources.Resources.rotate_right_circular_arrow_interface_symbol
        Me.ToolStripMenuItemRotRight.Name = "ToolStripMenuItemRotRight"
        Me.ToolStripMenuItemRotRight.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemRotRight.Text = "時計回りに90度回転"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(184, 6)
        '
        'ToolStripMenuItemDelete
        '
        Me.ToolStripMenuItemDelete.Image = Global.PictureUploader2.My.Resources.Resources.trash
        Me.ToolStripMenuItemDelete.Name = "ToolStripMenuItemDelete"
        Me.ToolStripMenuItemDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.ToolStripMenuItemDelete.Size = New System.Drawing.Size(187, 22)
        Me.ToolStripMenuItemDelete.Text = "削除"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "ドラッグモードの選択"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.RadioButtonDragFile)
        Me.Panel1.Controls.Add(Me.RadioButtonDragImage)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(459, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(151, 455)
        Me.Panel1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 289)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 74)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "ファイル" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "写真ファイルを別のフォルダなどに移動やコピーする場合に使用"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Meiryo UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(140, 57)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "イメージ" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "エクセルなどに画像として貼り付ける場合に使用"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'RadioButtonDragFile
        '
        Me.RadioButtonDragFile.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonDragFile.AutoSize = True
        Me.RadioButtonDragFile.Image = Global.PictureUploader2.My.Resources.Resources.document
        Me.RadioButtonDragFile.Location = New System.Drawing.Point(39, 216)
        Me.RadioButtonDragFile.Name = "RadioButtonDragFile"
        Me.RadioButtonDragFile.Size = New System.Drawing.Size(70, 70)
        Me.RadioButtonDragFile.TabIndex = 5
        Me.RadioButtonDragFile.UseVisualStyleBackColor = True
        '
        'RadioButtonDragImage
        '
        Me.RadioButtonDragImage.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonDragImage.AutoSize = True
        Me.RadioButtonDragImage.Checked = True
        Me.RadioButtonDragImage.Image = Global.PictureUploader2.My.Resources.Resources.excel
        Me.RadioButtonDragImage.Location = New System.Drawing.Point(39, 60)
        Me.RadioButtonDragImage.Name = "RadioButtonDragImage"
        Me.RadioButtonDragImage.Size = New System.Drawing.Size(70, 70)
        Me.RadioButtonDragImage.TabIndex = 4
        Me.RadioButtonDragImage.TabStop = True
        Me.RadioButtonDragImage.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(610, 455)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Meiryo UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ListView1 As ListView
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents ToolStripButtonQRCode As ToolStripButton
    Friend WithEvents ToolStripButtonSync As ToolStripButton
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripButtonOpenFolder As ToolStripButton
    Friend WithEvents ToolStripButtonFileCopy As ToolStripButton
    Friend WithEvents ToolStripButtonFileCut As ToolStripButton
    Friend WithEvents ToolStripButtonFileDelete As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents ToolStripButtonImageCopy As ToolStripButton
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItemOpen As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCut As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCopy As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCopyImage As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemDelete As ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripButtonInfo As ToolStripButton
    Friend WithEvents ToolStripButtonRotateLeft As ToolStripButton
    Friend WithEvents ToolStripButtonRotateRight As ToolStripButton
    Friend WithEvents ToolStripMenuItemSelAll As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItemRotLeft As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemRotRight As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItemEdit As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RadioButtonDragFile As RadioButton
    Friend WithEvents RadioButtonDragImage As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
End Class
