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
        Me.LinkLabelQRInfo = New System.Windows.Forms.LinkLabel()
        Me.ComboBoxIPAddresses = New System.Windows.Forms.ComboBox()
        Me.PictureBoxQR = New System.Windows.Forms.PictureBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenDir_ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Setting_ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LastLog_ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.PictureBoxQR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LinkLabelQRInfo
        '
        Me.LinkLabelQRInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LinkLabelQRInfo.Location = New System.Drawing.Point(0, 223)
        Me.LinkLabelQRInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.LinkLabelQRInfo.Name = "LinkLabelQRInfo"
        Me.LinkLabelQRInfo.Size = New System.Drawing.Size(200, 14)
        Me.LinkLabelQRInfo.TabIndex = 3
        Me.LinkLabelQRInfo.TabStop = True
        Me.LinkLabelQRInfo.Text = "LinkLabel1"
        Me.LinkLabelQRInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBoxIPAddresses
        '
        Me.ComboBoxIPAddresses.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboBoxIPAddresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxIPAddresses.FormattingEnabled = True
        Me.ComboBoxIPAddresses.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxIPAddresses.Margin = New System.Windows.Forms.Padding(0)
        Me.ComboBoxIPAddresses.Name = "ComboBoxIPAddresses"
        Me.ComboBoxIPAddresses.Size = New System.Drawing.Size(200, 20)
        Me.ComboBoxIPAddresses.TabIndex = 1
        '
        'PictureBoxQR
        '
        Me.PictureBoxQR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBoxQR.Location = New System.Drawing.Point(0, 20)
        Me.PictureBoxQR.Margin = New System.Windows.Forms.Padding(0)
        Me.PictureBoxQR.Name = "PictureBoxQR"
        Me.PictureBoxQR.Size = New System.Drawing.Size(200, 203)
        Me.PictureBoxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxQR.TabIndex = 4
        Me.PictureBoxQR.TabStop = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenDir_ToolStripMenuItem, Me.Setting_ToolStripMenuItem, Me.LastLog_ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.ShowImageMargin = False
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(156, 92)
        '
        'OpenDir_ToolStripMenuItem
        '
        Me.OpenDir_ToolStripMenuItem.Name = "OpenDir_ToolStripMenuItem"
        Me.OpenDir_ToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OpenDir_ToolStripMenuItem.Text = "フォルダを開く"
        '
        'Setting_ToolStripMenuItem
        '
        Me.Setting_ToolStripMenuItem.Name = "Setting_ToolStripMenuItem"
        Me.Setting_ToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.Setting_ToolStripMenuItem.Text = "設定"
        '
        'LastLog_ToolStripMenuItem
        '
        Me.LastLog_ToolStripMenuItem.Name = "LastLog_ToolStripMenuItem"
        Me.LastLog_ToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.LastLog_ToolStripMenuItem.Text = "最新の受信ログを見る"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(200, 237)
        Me.Controls.Add(Me.PictureBoxQR)
        Me.Controls.Add(Me.LinkLabelQRInfo)
        Me.Controls.Add(Me.ComboBoxIPAddresses)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.Text = "PictureUploader"
        CType(Me.PictureBoxQR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBoxIPAddresses As ComboBox
    Friend WithEvents LinkLabelQRInfo As LinkLabel
    Friend WithEvents PictureBoxQR As PictureBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents OpenDir_ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Setting_ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LastLog_ToolStripMenuItem As ToolStripMenuItem
End Class
