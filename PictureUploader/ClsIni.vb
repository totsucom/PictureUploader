Public Class ClsIni
    'プロファイル文字列取得
    Private Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (
       ByVal lpApplicationName As String,
       ByVal lpKeyName As String,
       ByVal lpDefault As String,
       ByVal lpReturnedString As System.Text.StringBuilder,
       ByVal nSize As UInt32,
       ByVal lpFileName As String) As UInt32

    'プロファイル文字列書込み
    Private Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (
        ByVal lpAppName As String,
        ByVal lpKeyName As String,
        ByVal lpString As String,
        ByVal lpFileName As String) As Integer

    Private strIniFileName As String = ""

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="strIniFile">INIファイル名(フルパス)</param>
    Sub New(ByVal strIniFile As String)
        Me.strIniFileName = strIniFile  'ファイル名退避
    End Sub

    ''' <summary>
    ''' プロファイル文字列取得
    ''' </summary>
    ''' <param name="strAppName">アプリケーション文字列</param>
    ''' <param name="strKeyName">キー文字列</param>
    ''' <param name="strDefault">デフォルト文字列</param>
    ''' <returns>プロファイル文字列</returns>
    Public Function GetProfileString(ByVal strAppName As String,
                                     ByVal strKeyName As String,
                                     ByVal strDefault As String) As String
        Try
            Dim strWork As System.Text.StringBuilder = New System.Text.StringBuilder(1024)
            Dim intRet As Integer = GetPrivateProfileString(strAppName, strKeyName,
                                                                       strDefault, strWork,
                                                                       strWork.Capacity - 1, strIniFileName)
            If intRet > 0 Then
                'エスケープ文字を解除して返す
                Return ResetEscape(strWork.ToString())
            Else
                Return strDefault
            End If
        Catch ex As Exception
            Return strDefault
        End Try
    End Function

    ''' <summary>
    ''' プロファイル文字列設定
    ''' </summary>
    ''' <param name="strAppName">アプリケーション文字列</param>
    ''' <param name="strKeyName">キー文字列</param>
    ''' <param name="strSet">設定文字列</param>
    ''' <returns>True:正常, False:エラー</returns>
    Public Function WriteProfileString(ByVal strAppName As String,
                                       ByVal strKeyName As String,
                                       ByVal strSet As String) As Boolean
        Try
            'エスケープ文字変換
            Dim strCnv As String = SetEscape(strSet)
            Dim intRet As Integer = WritePrivateProfileString(strAppName, strKeyName, strCnv, strIniFileName)
            If intRet > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' エスケープ文字変換
    ''' </summary>
    ''' <param name="strSet">設定文字列</param>
    ''' <returns>変換後文字列</returns>
    Private Function SetEscape(ByVal strSet As String) As String
        Dim strEscape As String = ";#=:"
        Dim strRet As String = strSet
        Try
            For i = 0 To strEscape.Length - 1
                Dim str As String = strEscape.Substring(i, 1)
                strRet = strRet.Replace(str, "\" & str)
            Next
            Return strRet
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' エスケープ文字解除
    ''' </summary>
    ''' <param name="strSet">設定文字列</param>
    ''' <returns>変換後文字列</returns>
    Private Function ResetEscape(ByVal strSet As String) As String
        Dim strEscape As String = ";#=:"
        Dim strRet As String = strSet
        Try
            For i = 0 To strEscape.Length - 1
                Dim str As String = strEscape.Substring(i, 1)
                strRet = strRet.Replace("\" & str, str)
            Next
            Return strRet
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
