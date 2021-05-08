Imports System.Deployment.Application

Public Class FormInfo
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://www.flaticon.com/authors/dave-gandy")
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        System.Diagnostics.Process.Start("http://fontawesome.io")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://www.flaticon.com/")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        System.Diagnostics.Process.Start("https://www.flaticon.com/")
    End Sub

    Private Sub LinkLabel6_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel6.LinkClicked
        System.Diagnostics.Process.Start("https://www.freepik.com")
    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        System.Diagnostics.Process.Start("https://www.flaticon.com/")
    End Sub

    Private Sub FormInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If ApplicationDeployment.IsNetworkDeployed Then
            Dim ver = ApplicationDeployment.CurrentDeployment.CurrentVersion
            Label1.Text = "PiPデスクトップ " & ver.ToString
        Else
            Label1.Text = "PiPデスクトップ 未公開バージョン"
        End If
    End Sub
End Class