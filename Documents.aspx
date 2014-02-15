<%@ Page Title="Documents" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Documents.aspx.cs" Inherits="Documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><b>Risk/Action Documents</b></h2>
    <asp:FileUpload ID="FileUpload" runat="server" />
    <br />
    <asp:Button ID="uploadButton" runat="server" onclick="uploadButton_Click" 
        Text="Upload" CssClass="flatbutton" />
    &nbsp;<asp:Label ID="sucesssFailLabel" runat="server"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="documentsGridView" runat="server" 
    AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
    DataKeyNames="Image_Id" DataSourceID="docsGridViewDS" 
    onselectedindexchanged="documentsGridView_SelectedIndexChanged"
    CssClass="gridview"
    Width="60%">
        <Columns>
            <asp:BoundField DataField="Image_Id" HeaderText="Image_Id" 
                SortExpression="Image_Id" InsertVisible="False" ReadOnly="True" />
            <asp:BoundField DataField="Added_Date" HeaderText="Added_Date" 
                SortExpression="Added_Date" DataFormatString="{0:d}" />
            <asp:BoundField DataField="Image_Name" HeaderText="Image_Name" 
                SortExpression="Image_Name" />
            <asp:BoundField DataField="Image_Description" HeaderText="Image_Description" 
                SortExpression="Image_Description" />
            <asp:BoundField DataField="Uploaded_By" HeaderText="Uploaded_By" 
                SortExpression="Uploaded_By" />
            <asp:TemplateField HeaderText="Delete?">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="deleteDocsButton" runat="server" 
        onclick="deleteDocsButton_Click" Text="Delete Seleted Documents" 
        Width="155px" 
        OnClientClick="return confirm('Are you sure you wish to DELETE the selected attachments?.');" 
        CssClass="flatbutton" />
<asp:SqlDataSource ID="docsGridViewDS" runat="server" 
    ProviderName="System.Data.SqlClient" 
    SelectCommand="[dbo].[DocumentsSelectByRiskId]" 
    SelectCommandType="StoredProcedure" DeleteCommand="[dbo].[DocumentsDelete]" 
        DeleteCommandType="StoredProcedure" 
        ConnectionString="Data Source=SAT-C7Q8XL1;Initial Catalog=RiskRegisterDevelopment;Integrated Security=True">
    <SelectParameters>
        <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
        <asp:QueryStringParameter Name="Risk_Id" QueryStringField="riskId" 
            Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>

