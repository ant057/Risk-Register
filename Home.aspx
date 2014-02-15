<%@ Page Title="Risk Register" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h2><b><asp:Label Text = "Risk Dashboard" ID="eLabel" runat="server"></asp:Label></b></h2>
        </div>

    <div>
    <br />
        <asp:Label ID="Label5" runat="server" Text="Select Entity: "></asp:Label>
        <asp:DropDownList ID="entityDDL" runat="server" AutoPostBack="True" 
            onselectedindexchanged="entityDDL_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Group By:"></asp:Label>
        <asp:DropDownList ID="ddlSort1" runat="server">
            <asp:ListItem>RiskID</asp:ListItem>
            <asp:ListItem>MasterRiskID</asp:ListItem>
            <asp:ListItem>RiskType</asp:ListItem>
            <asp:ListItem>RiskSource</asp:ListItem>
            <asp:ListItem>RiskScenario</asp:ListItem>
            <asp:ListItem>PrimaryOwner</asp:ListItem>
            <asp:ListItem>InherentRiskScore</asp:ListItem>
            <asp:ListItem>ResidualRiskScore</asp:ListItem>
            <asp:ListItem>ModifiedDate</asp:ListItem>
            <asp:ListItem>HasActionPlan</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="addFilterButton" runat="server" OnClick="addFilterButton_Click" 
            Text="Add" CssClass="flatbutton" />
        &nbsp;<asp:Button ID="removeFilterButton" runat="server" 
            onclick="removeFilterButton_Click" Text="Remove" Width="52px" 
            CssClass="flatbutton" />
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="sort2Label" runat="server" Text="Group By:" Visible="False"></asp:Label>
            <asp:DropDownList ID="ddlSort2" runat="server" Visible="False">
                <asp:ListItem>RiskID</asp:ListItem>
                <asp:ListItem>MasterRiskID</asp:ListItem>
                <asp:ListItem>RiskType</asp:ListItem>
                <asp:ListItem>RiskSource</asp:ListItem>
                <asp:ListItem>RiskScenario</asp:ListItem>
                <asp:ListItem>PrimaryOwner</asp:ListItem>
                <asp:ListItem>InherentRiskScore</asp:ListItem>
                <asp:ListItem>ResidualRiskScore</asp:ListItem>
                <asp:ListItem>ModifiedDate</asp:ListItem>
                <asp:ListItem>HasActionPlan</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="sort3Label" runat="server" Text="Group By:" Visible="False"></asp:Label>
            <asp:DropDownList ID="ddlSort3" runat="server" Visible="False">
                <asp:ListItem>RiskID</asp:ListItem>
                <asp:ListItem>MasterRiskID</asp:ListItem>
                <asp:ListItem>RiskType</asp:ListItem>
                <asp:ListItem>RiskSource</asp:ListItem>
                <asp:ListItem>RiskScenario</asp:ListItem>
                <asp:ListItem>PrimaryOwner</asp:ListItem>
                <asp:ListItem>InherentRiskScore</asp:ListItem>
                <asp:ListItem>ResidualRiskScore</asp:ListItem>
                <asp:ListItem>ModifiedDate</asp:ListItem>
                <asp:ListItem>HasActionPlan</asp:ListItem>
            </asp:DropDownList>
        </asp:Panel>
        <br />
        <asp:Button ID="sortButton" runat="server" OnClick="sortButton_Click" 
            style="height: 26px" Text="Sort" CssClass="flatbutton" />
        <br />
        <asp:Label ID="updateLabel" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Quick Search"></asp:Label>
        <asp:TextBox ID="searchCriteriaText" runat="server"></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" CssClass="flatbutton" Text="Go" 
            onclick="searchButton_Click" />
    <br />
        <asp:GridView ID="riskDetailGridView" runat="server" Width="100%"
            AllowSorting="True" AutoGenerateColumns="True" 
            onselectedindexchanged="riskDetailGridView_SelectedIndexChanged" 
            AutoGenerateSelectButton="True" 
            onrowdatabound="riskDetailGridView_RowDataBound" 
            onsorting="riskDetailGridView_Sorting"  CssClass="gridview" 
            Font-Names="Arial" >
            <AlternatingRowStyle BackColor="Silver" />
            <RowStyle Font-Bold="true" />
        </asp:GridView>
    </div>
</asp:Content>

