<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AggregatedScores.aspx.cs" Inherits="AggregatedScores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
        function ShowGrid() {
            var gridView = document.getElementById("ctl00_ContentPlaceHolder1_editGridView");

            if (gridView.className == "invisible") {
                gridView.className = "";
            }
            else {
                gridView.className = "invisible";
            }
        }
    </script>
    <h2><b><asp:Label Text = "Aggregate Scoring" ID="eLabel" runat="server"></asp:Label></b></h2>
    <asp:CheckBox ID="editCheckBox" runat="server" Text="Edit Entity Capital" onclick="ShowGrid();" />
    <br />
&nbsp;<asp:GridView ID="AggregateScores" runat="server" CssClass="gridview" Width="50%">
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiskRegister %>" ProviderName="<%$ ConnectionStrings:RiskRegister.ProviderName %>" SelectCommand="dbo.Entity_CapitalSelectAll" SelectCommandType="StoredProcedure" UpdateCommand="[dbo].[Entity_CapitalUpdate]" UpdateCommandType="StoredProcedure">
    </asp:SqlDataSource>
    <asp:GridView ID="editGridView" CssClass="invisible" runat="server" AutoGenerateColumns="False" AutoGenerateEditButton="True" DataKeyNames="Entity_Name" DataSourceID="SqlDataSource1" EnableModelValidation="True">
        <Columns>
            <asp:BoundField DataField="Entity_Name" HeaderText="Entity_Name" SortExpression="Entity_Name" />
            <asp:BoundField DataField="Capital" HeaderText="Capital" SortExpression="Capital" />
        </Columns>
    </asp:GridView>
</asp:Content>

