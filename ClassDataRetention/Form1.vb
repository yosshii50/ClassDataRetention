Public Class Form1

    '複数のオブジェクトを保存
    '   →読み込みでエラー

    '保存時に無かった項目が読み込み時に存在
    '   →問題なし

    '保存時にあった項目が読み込み時になし
    '   →問題なし


    '下記のWebページを参考に作成
    'https://dobon.net/vb/dotnet/file/xmlserializer.html

    Public Class SampleClass1
        Public NumberDa As Integer
        Public Message1 As String
        Public Message2() As String
        Public Message3() As String
    End Class
    Public Class SampleClass
        Public NumberData As Integer
        Public Message1 As String
        Public Message2(5) As String
        'Public Message3 As String
        Public OrgCls() As SampleClass1
    End Class

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        '保存先のファイル名
        Dim fileName As String = "sample.xml"

        '保存するクラス(SampleClass)のインスタンスを作成
        Dim obj As New SampleClass()
        obj.Message1 = "テストです。"
        'obj.Message3 = "333"
        obj.NumberData = 123
        ReDim obj.OrgCls(3)
        obj.OrgCls(0) = New SampleClass1
        obj.OrgCls(0).Message1 = "aaaaaaa"
        obj.OrgCls(1) = New SampleClass1

        'XmlSerializerオブジェクトを作成
        'オブジェクトの型を指定する
        Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(SampleClass))

        '書き込むファイルを開く（UTF-8 BOM無し）
        Dim sw As New System.IO.StreamWriter(fileName, False, New System.Text.UTF8Encoding(False))

        'シリアル化し、XMLファイルに保存する
        serializer.Serialize(sw, obj)

        'シリアル化し、XMLファイルに保存する
        'serializer.Serialize(sw, obj)

        'ファイルを閉じる
        sw.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        '保存元のファイル名
        Dim fileName As String = "sample.xml"

        'XmlSerializerオブジェクトを作成
        Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(SampleClass))

        '読み込むファイルを開く
        Dim sr As New System.IO.StreamReader(fileName, New System.Text.UTF8Encoding(False))

        'XMLファイルから読み込み、逆シリアル化する
        Dim obj As SampleClass = DirectCast(serializer.Deserialize(sr), SampleClass)

        'ファイルを閉じる
        sr.Close()

        MsgBox(obj.Message1) '"テストです。"


    End Sub
End Class
