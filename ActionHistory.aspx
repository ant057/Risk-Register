<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ActionHistory.aspx.cs" Inherits="ActionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Action History</h2>

    <asp:GridView ID="GridView1" Width="100%" runat="server" AutoGenerateColumns="False" DataKeyNames="Action_History_ID" DataSourceID="historyDataSource" CssClass="gridview">
        <Columns>
            <asp:BoundField DataField="Action_Detail_Old" HeaderText="Action Details Old" SortExpression="Action_Detail_Old" />
            <asp:BoundField DataField="Action_Detail_New" HeaderText="Action Details New" SortExpression="Action_Detail_New" />
            <asp:BoundField DataField="Modified_By" HeaderText="Modified By" SortExpression="Modified_By" />
            <asp:BoundField DataField="Modified_Date" HeaderText="Modified Date" SortExpression="Modified_Date" DataFormatString="{0:d}" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="historyDataSource" runat="server" ProviderName="System.Data.SqlClient" SelectCommand="[SelectAllHistory]" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="Risk_ID" QueryStringField="riskId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>

