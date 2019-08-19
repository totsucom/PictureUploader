<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSetting
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxImageDir = New System.Windows.Forms.TextBox()
        Me.ButtonSelectImageDir = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPortNo = New System.Windows.Forms.TextBox()
        Me.ButtonDefault = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(216, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "アップロードされた写真などを保存するフォルダ"
        '
        'TextBoxImageDir
        '
        Me.TextBoxImageDir.Location = New System.Drawing.Point(14, 46)
        Me.TextBoxImageDir.Name = "TextBoxImageDir"
        Me.TextBoxImageDir.Size = New System.Drawing.Size(194, 19)
        Me.TextBoxImageDir.TabIndex = 1
        '
        'ButtonSelectImageDir
        '
        Me.ButtonSelectImageDir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectImageDir.Location = New System.Drawing.Point(214, 44)
        Me.ButtonSelectImageDir.Name = "ButtonSelectImageDir"
        Me.ButtonSelectImageDir.Size = New System.Drawing.Size(28, 23)
        Me.ButtonSelectImageDir.TabIndex = 2
        Me.ButtonSelectImageDir.Text = "..."
        Me.ButtonSelectImageDir.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(184, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Webサーバーポート番号(上級者向け)"
        '
        'TextBoxPortNo
        '
        Me.TextBoxPortNo.Location = New System.Drawing.Point(14, 112)
        Me.TextBoxPortNo.Name = "TextBoxPortNo"
        Me.TextBoxPortNo.Size = New System.Drawing.Size(51, 19)
        Me.TextBoxPortNo.TabIndex = 4
        '
        'ButtonDefault
        '
        Me.ButtonDefault.Location = New System.Drawing.Point(66, 176)
        Me.ButtonDefault.Name = "ButtonDefault"
        Me.ButtonDefault.Size = New System.Drawing.Size(99, 23)
        Me.ButtonDefault.TabIndex = 5
        Me.ButtonDefault.Text = "デフォルトに戻す"
        Me.ButtonDefault.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(177, 176)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(65, 23)
        Me.ButtonOK.TabIndex = 6
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'FormSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(254, 217)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonDefault)
        Me.Controls.Add(Me.TextBoxPortNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ButtonSelectImageDir)
        Me.Controls.Add(Me.TextBoxImageDir)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "FormSetting"
        Me.Text = "PictureUploaderの設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxImageDir As TextBox
    Friend WithEvents ButtonSelectImageDir As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxPortNo As TextBox
    Friend WithEvents ButtonDefault As Button
    Friend WithEvents ButtonOK As Button
End Class
