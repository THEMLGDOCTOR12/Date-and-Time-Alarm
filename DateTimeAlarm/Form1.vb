'Clock Icon provided by Icons8 - https://icons8.com/icons/set/clock'
Imports System.Xml
Imports System.Data
Public Class Form1
    Dim N As String = 0
    Dim DateTest As Boolean = False
    Dim TimeTest As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim filelocation As String = My.Application.Info.DirectoryPath & "\Alarms.xml"
        Dim CurrentDate As String = DateAndTime.Today
        Dim CurrentTime As String = TimeOfDay.ToString("h:mm tt")

        Dim ds As New DataSet

        Dim doc As New System.Xml.XmlDocument()
        doc.Load(filelocation)

        For Each data As System.Xml.XmlElement In doc.DocumentElement.ChildNodes

            For Each field As System.Xml.XmlElement In data.ChildNodes
                'MessageBox.Show(field.Name & " : " & field.InnerText)
                If field.InnerText = CurrentDate Then
                    DateTest = True
                End If
                If field.InnerText <= CurrentTime Then
                    TimeTest = True
                End If
            Next
            If DateTest = True And TimeTest = True Then
                MsgBox("Alarm")
            End If
        Next
        DateTest = False
        TimeTest = False
        ds.ReadXml(filelocation)
        dgvOutput.DataSource = ds.Tables(0)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim CurrentDate As String = DateAndTime.Today
        Dim CurrentTime As String = TimeOfDay.ToString("h:mm tt")
        Dim SetDate As String = dtpDate.Value
        Dim SetTime As String = txtTime.Text
        Dim SetName As String = txtEvent.Text
        Dim filelocation As String = My.Application.Info.DirectoryPath & "\Alarms.xml"
        Dim tempfile As String = My.Application.Info.DirectoryPath & "\Temp.xml"
        Dim ds As New DataSet

        Dim doc As New System.Xml.XmlDocument()
        doc.Load(filelocation)

        For Each data As System.Xml.XmlElement In doc.DocumentElement.ChildNodes

            For Each field As System.Xml.XmlElement In data.ChildNodes
                'MessageBox.Show(field.Name & " : " & field.InnerText)
                If field.InnerText = CurrentDate Then
                    DateTest = True
                End If
                If field.InnerText <= CurrentTime Then
                    TimeTest = True
                End If
            Next
            If DateTest = True And TimeTest = True Then
                MsgBox("Alarm")
            End If
        Next
        DateTest = False
        TimeTest = False

        'SetDate <= CurrentDate Then
        'MsgBox("Set Date Reached")
        'Else
        'MsgBox("Set Date Not Reached")
        'End If
        If System.IO.File.Exists(filelocation) Then
            Dim writer As New XmlTextWriter(tempfile, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)

            writer.Formatting = Formatting.Indented
            writer.Indentation = 2

            writer.WriteStartElement("Alarm")
            CreateXmlRec(SetName, SetDate, SetTime, writer)
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
            Dim x1 As New DataSet
            x1.ReadXml(filelocation)
            Dim x2 As New DataSet
            x2.ReadXml(tempfile)

            x1.Merge(x2)
            x1.WriteXml(filelocation)
        Else
            Dim writer As New XmlTextWriter(filelocation, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(True)

            writer.Formatting = Formatting.Indented
            writer.Indentation = 2

            writer.WriteStartElement("Alarm")
            CreateXmlRec(SetName, SetDate, SetTime, writer)
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        End If
        ds.ReadXml(filelocation)
        dgvOutput.DataSource = ds.Tables(0)
    End Sub
    Private Function CreateXmlRec(ByVal IncomingName As String, ByVal IncomingDate As String, ByVal IncomingTime As String, ByVal writer As XmlTextWriter)
        writer.WriteStartElement("Alarm")

        writer.WriteStartElement("Name")
        writer.WriteString(IncomingName)
        writer.WriteEndElement()

        writer.WriteStartElement("Date")
        writer.WriteString(IncomingDate)
        writer.WriteEndElement()

        writer.WriteStartElement("Time")
        writer.WriteString(IncomingTime)
        writer.WriteEndElement()

        writer.WriteEndElement()
    End Function
End Class
