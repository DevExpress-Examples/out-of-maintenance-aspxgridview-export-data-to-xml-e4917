using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    private DataTable Data {
        get {
            DataTable data = (DataTable)Session["Data"];
            if (data == null) {
                data = new DataTable();
                data.Columns.Add("ID");
                data.Columns.Add("Animal");
                data.Columns.Add("ColourID");
                data.Rows.Add(new object[] { 1, "Fox", 1 });
                data.Rows.Add(new object[] { 2, "Wolf", 2 });
                data.Rows.Add(new object[] { 3, "Bear", 3 });
                data.Rows.Add(new object[] { 4, "Panther", 4 });
                data.Rows.Add(new object[] { 5, "Rat", 2 });
                data.Rows.Add(new object[] { 6, "Cat", 4 });
                Session["Data"] = data;
            }
            return data;
        }
    }

    protected void Page_Init(object sender, EventArgs e) {
        ASPxGridView1.DataSource = Data;
        ASPxGridView1.DataBind();
    }

    protected void btnUpdate_Click(object sender, EventArgs e) {
        for (int i = 0; i < Data.Rows.Count; i++) {
            string hfKey = "key" + Data.Rows[i]["ID"].ToString();
            if (hiddenField.Contains(hfKey)) {
                string[] pars = Convert.ToString(hiddenField[hfKey]).Split(';');
                Data.Rows[i]["Animal"] = pars[0];
                Data.Rows[i]["ColourID"] = pars[1];
            }
        }
        ASPxGridView1.DataBind();
        hiddenField.Clear();

        StartExport();
    }

    private void StartExport() {
        DataTable dt = ASPxGridView1.DataSource as DataTable;

        StringBuilder xmlDoc = new StringBuilder("<?xml version=\"1.1\" encoding='UTF-8' ?>\n");
        xmlDoc.AppendLine("<items>");

        foreach (DataRow dr in dt.Rows) {
            xmlDoc.AppendLine("\t<item>");
 
            foreach (DataColumn col in dr.Table.Columns) {
                xmlDoc.AppendLine("\t\t<" + col.ColumnName + ">" + dr[col].ToString() + "</" + col.ColumnName + ">");
            }

            xmlDoc.AppendLine("\t</item>");
        }

        xmlDoc.AppendLine("</items>");

        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xml");
        Response.Charset = "";
        Response.ContentType = "application/xml";
        Response.Write(xmlDoc);
        Response.Flush();
        Response.End();
    }

    protected void tbWbsLevel_Load(object sender, EventArgs e) {
        GridViewDataItemTemplateContainer c = ((ASPxTextBox)sender).NamingContainer
            as GridViewDataItemTemplateContainer;
        ((ASPxTextBox)sender).ClientInstanceName = "animalName" + c.KeyValue.ToString();
        ((ASPxTextBox)sender).ClientSideEvents.TextChanged = "function(s,e){ProcessValueChanged(" + c.KeyValue.ToString() + ",s.GetText(),animalColour" + c.KeyValue.ToString() + ".GetValue());}";

        string hfKey = "key" + c.KeyValue.ToString();
        if (hiddenField.Contains(hfKey)) {
            string[] pars = Convert.ToString(hiddenField[hfKey]).Split(';');
            ((ASPxTextBox)sender).Text = pars[0];
        }
    }

    protected void colourBox_Load(object sender, EventArgs e) {
        GridViewDataItemTemplateContainer c = ((ASPxComboBox)sender).NamingContainer
         as GridViewDataItemTemplateContainer;
        ((ASPxComboBox)sender).ClientInstanceName = "animalColour" + c.KeyValue.ToString();
        ((ASPxComboBox)sender).ClientSideEvents.SelectedIndexChanged = "function(s,e){ProcessValueChanged(" + c.KeyValue.ToString() + ",animalName" + c.KeyValue.ToString() + ".GetText(),s.GetValue().toString());}";
        string hfKey = "key" + c.KeyValue.ToString();
        if (hiddenField.Contains(hfKey)) {
            string[] pars = Convert.ToString(hiddenField[hfKey]).Split(';');
            ((ASPxComboBox)sender).Value = pars[1];
        }
    }
}