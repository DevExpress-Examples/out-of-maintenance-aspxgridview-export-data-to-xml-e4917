<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.14.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ProcessValueChanged(key, animalName, colourID) {
            var currentKey = "key" + key.toString();

            if (!clientHiddenField.Contains(currentKey)) {
                clientHiddenField.Add(currentKey, animalName + ";" + colourID);
            } else {
                clientHiddenField.Set(currentKey, animalName + ";" + colourID);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" KeyFieldName="ID" AutoGenerateColumns="False"
            SettingsBehavior-AllowDragDrop="False" ClientInstanceName="clientGridView">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="ID" VisibleIndex="0" Width="15px">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Animal" VisibleIndex="1">
                    <DataItemTemplate>
                        <dx:ASPxTextBox ID="tbWbsLevel" runat="server" Text='<%#Bind("Animal")%>' Size="6" OnDataBound="tbWbsLevel_Load">
                        </dx:ASPxTextBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="ColourID" Caption="Colour" VisibleIndex="2">
                    <DataItemTemplate>
                        <dx:ASPxComboBox ID="colourBox" runat="server" Value='<%#Bind("ColourID") %>' OnDataBound="colourBox_Load">
                            <Items>
                                <dx:listedititem text="Red" value="1" />
                                <dx:listedititem text="Gray" value="2" />
                                <dx:listedititem text="Brown" value="3" />
                                <dx:listedititem text="Black" value="4" />
                            </Items>
                        </dx:ASPxComboBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowDragDrop="False" />
            <SettingsPager PageSize="5">
            </SettingsPager>
        </dx:ASPxGridView>
        <dx:ASPxHiddenField runat="server" ID="hiddenField"
            ClientInstanceName="clientHiddenField">
        </dx:ASPxHiddenField>
        <br />
        <dx:ASPxButton ID="btnUpdate" runat="server" Text="Export"
            OnClick="btnUpdate_Click">
        </dx:ASPxButton>
    </form>
</body>
</html>
