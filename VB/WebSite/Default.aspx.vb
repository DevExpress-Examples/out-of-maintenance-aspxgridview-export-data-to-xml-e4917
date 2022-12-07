Imports Microsoft.VisualBasic
Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private ReadOnly Property Data() As DataTable
		Get
'INSTANT VB NOTE: The local variable data was renamed since Visual Basic will not allow local variables with the same name as their enclosing function or property:
			Dim data_Renamed As DataTable = CType(Session("Data"), DataTable)
			If data_Renamed Is Nothing Then
				data_Renamed = New DataTable()
				data_Renamed.Columns.Add("ID")
				data_Renamed.Columns.Add("Animal")
				data_Renamed.Columns.Add("ColourID")
				data_Renamed.Rows.Add(New Object() { 1, "Fox", 1 })
				data_Renamed.Rows.Add(New Object() { 2, "Wolf", 2 })
				data_Renamed.Rows.Add(New Object() { 3, "Bear", 3 })
				data_Renamed.Rows.Add(New Object() { 4, "Panther", 4 })
				data_Renamed.Rows.Add(New Object() { 5, "Rat", 2 })
				data_Renamed.Rows.Add(New Object() { 6, "Cat", 4 })
				Session("Data") = data_Renamed
			End If
			Return data_Renamed
		End Get
	End Property

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		ASPxGridView1.DataSource = Data
		ASPxGridView1.DataBind()
	End Sub

	Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs)
		For i As Integer = 0 To Data.Rows.Count - 1
			Dim hfKey As String = "key" & Data.Rows(i)("ID").ToString()
			If hiddenField.Contains(hfKey) Then
				Dim pars() As String = Convert.ToString(hiddenField(hfKey)).Split(";"c)
				Data.Rows(i)("Animal") = pars(0)
				Data.Rows(i)("ColourID") = pars(1)
			End If
		Next i
		ASPxGridView1.DataBind()
		hiddenField.Clear()

		StartExport()
	End Sub

	Private Sub StartExport()
		Dim dt As DataTable = TryCast(ASPxGridView1.DataSource, DataTable)

		Dim xmlDoc As New StringBuilder("<?xml version=""1.1"" encoding='UTF-8' ?>" & Constants.vbLf)
		xmlDoc.AppendLine("<items>")

		For Each dr As DataRow In dt.Rows
			xmlDoc.AppendLine(Constants.vbTab & "<item>")

			For Each col As DataColumn In dr.Table.Columns
				xmlDoc.AppendLine(Constants.vbTab + Constants.vbTab & "<" & col.ColumnName & ">" & dr(col).ToString() & "</" & col.ColumnName & ">")
			Next col

			xmlDoc.AppendLine(Constants.vbTab & "</item>")
		Next dr

		xmlDoc.AppendLine("</items>")

		Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xml")
		Response.Charset = ""
		Response.ContentType = "application/xml"
		Response.Write(xmlDoc)
		Response.Flush()
		Response.End()
	End Sub

	Protected Sub tbWbsLevel_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim c As GridViewDataItemTemplateContainer = TryCast((CType(sender, ASPxTextBox)).NamingContainer, GridViewDataItemTemplateContainer)
		CType(sender, ASPxTextBox).ClientInstanceName = "animalName" & c.KeyValue.ToString()
		CType(sender, ASPxTextBox).ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" & c.KeyValue.ToString() & ",s.GetText(),animalColour" & c.KeyValue.ToString() & ".GetValue());}"

		Dim hfKey As String = "key" & c.KeyValue.ToString()
		If hiddenField.Contains(hfKey) Then
			Dim pars() As String = Convert.ToString(hiddenField(hfKey)).Split(";"c)
			CType(sender, ASPxTextBox).Text = pars(0)
		End If
	End Sub

	Protected Sub colourBox_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim c As GridViewDataItemTemplateContainer = TryCast((CType(sender, ASPxComboBox)).NamingContainer, GridViewDataItemTemplateContainer)
		CType(sender, ASPxComboBox).ClientInstanceName = "animalColour" & c.KeyValue.ToString()
		CType(sender, ASPxComboBox).ClientSideEvents.SelectedIndexChanged = "function(s,e){ProcessValueChanged(" & c.KeyValue.ToString() & ",animalName" & c.KeyValue.ToString() & ".GetText(),s.GetValue().toString());}"
		Dim hfKey As String = "key" & c.KeyValue.ToString()
		If hiddenField.Contains(hfKey) Then
			Dim pars() As String = Convert.ToString(hiddenField(hfKey)).Split(";"c)
			CType(sender, ASPxComboBox).Value = pars(1)
		End If
	End Sub
End Class