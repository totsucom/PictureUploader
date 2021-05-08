Imports System.Management
Imports System.Security.Cryptography

Public Class etc
    Private Shared r As System.Random = Nothing

    'コンピューター情報を用いるため、pcid,key1は同一パソコンであればパソコン毎に異な
    Public Shared Function GetMyInfo(ByRef pcid As String, ByRef key1 As String) As Boolean
        Dim drive = "PHYSICALDRIVE0"
        Dim theSearcher = New ManagementObjectSearcher($"SELECT * FROM Win32_DiskDrive WHERE DeviceID like '%{drive}'")
        Dim s As String = ""
        For Each currentObject As ManagementObject In theSearcher.[Get]()
            'Console.WriteLine(currentObject("DeviceID"))
            'Console.WriteLine(currentObject("SerialNumber"))
            s = currentObject("SerialNumber")
            Exit For
        Next
        If s = "" Then Return False 'エラー
        s &= ":" & My.User.Name
        pcid = System.Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(s))
        If pcid.Length > 255 Then Return False 'エラー

        Dim sha256 As SHA256 = New SHA256CryptoServiceProvider()
        Dim hashValue As Byte() = sha256.ComputeHash(Text.Encoding.UTF8.GetBytes(pcid))
        hashValue = sha256.ComputeHash(hashValue)
        If hashValue.Length < 16 Then Return False

        Dim sb As New Text.StringBuilder
        For Each b As Byte In hashValue
            sb.Append(b.ToString("X2"))
        Next

        key1 = sb.ToString().Substring(0, 32)
        Return True
    End Function

    Public Shared Function CreateQrCodeParam(pcid As String, key1 As String) As String
        Dim sb As New Text.StringBuilder
        sb.Append("FA2E")           '識別子1
        sb.Append(key1)             'Key1 32chars
        sb.Append("3A")             '識別子2
        sb.Append(pcid.Length.ToString("X2")) 'MyIdの長さ
        sb.Append(pcid)             'このPCのID
        sb.Append("D5")             '識別子3
        Return sb.ToString()
    End Function

    Public Shared Function Hex2Array(ByRef input As String) As Byte()
        Dim result As Byte() = New Byte(input.Length / 2 - 1) {}
        Dim cur As Integer = 0
        Dim i As Integer = 0
        Try
            While i < input.Length
                Dim w As String = input.Substring(i, 2)
                result(cur) = Convert.ToByte(w, 16)
                cur += 1
                i = i + 2
            End While
        Catch ex As Exception
            Return Nothing
        End Try
        Return result
    End Function

    Public Shared Function Decrypt(src As Byte(), key1 As Byte(), iv As Byte()) As Byte()
        ' AES暗号化サービスプロバイダ
        Dim aes As New AesCryptoServiceProvider()
        aes.BlockSize = 128
        aes.KeySize = 128
        aes.IV = iv
        aes.Key = key1
        aes.Mode = CipherMode.CBC
        aes.Padding = PaddingMode.PKCS7

        ' 複号化する
        Using dec As ICryptoTransform = aes.CreateDecryptor()
            Return dec.TransformFinalBlock(src, 0, src.Length)
        End Using
    End Function

    Public Shared Function WaitForUnlock(FullPath As String) As Boolean
        Dim finfo As New System.IO.FileInfo(FullPath)
        Dim stream As System.IO.Stream = Nothing
        Dim lp As Integer = 0
        While True
            Try
                stream = finfo.Open(IO.FileMode.Open, IO.FileAccess.Read)
                Exit While

            Catch ex As IO.IOException
                ' ロック解除待ち
                lp += 1
                If lp > 10 Then Return False ' これだと10秒でタイムアウト
                System.Threading.Thread.Sleep(1000) ' 1秒待つ

            Finally
                If Not stream Is Nothing Then stream.Close()
            End Try
        End While
        Return True
    End Function

End Class
