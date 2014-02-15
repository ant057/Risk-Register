<%@ Page Title="Risk Register" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

    function gvrowtoggle(row) {
        try {
            row_num = row; //row to be hidden
            ctl_row = row - 1; //row where show/hide button was clicked
            rows = document.getElementById("<%= riskDetailGridView.ClientID %>").rows;
            rowElement = rows[ctl_row]; //elements in row where show/hide button was clicked
            img = rowElement.cells[0].firstChild; //the show/hide button
            document.getElementById("<%=updateLabel.ClientID %>").value = rows.toString();
            if (rows[row_num].className != "hidden") //if the row is not currently hidden 
            //(default)...
            {
                rows[row_num].className = "hidden"; //hide the row
                img.src = "../Images/closes.jpg"; //change the image for the show/hide button
            }
            else {
                rows[row_num].className = ""; //set the css class of the row to default 
                //(to make it visible)
                img.src = "../Images/details.jpg"; //change the image for the show/hide button
            }
        }
        catch (ex) 
        { alert(ex) }
    }

</script> 
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
        <asp:Label ID="Label4" runat="server" Text="Sort By:"></asp:Label>
        <asp:DropDownList ID="ddlSort1" runat="server">
            <asp:ListItem>RiskID</asp:ListItem>
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
            <asp:Label ID="sort2Label" runat="server" Text="Sort By:" Visible="False"></asp:Label>
            <asp:DropDownList ID="ddlSort2" runat="server" Visible="False">
                <asp:ListItem>RiskID</asp:ListItem>
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
            <asp:Label ID="sort3Label" runat="server" Text="Sort By:" Visible="False"></asp:Label>
            <asp:DropDownList ID="ddlSort3" runat="server" Visible="False">
                <asp:ListItem>RiskID</asp:ListItem>
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
    <br />
        <asp:GridView ID="riskDetailGridView" runat="server" Width="100%"
            AllowSorting="True" AutoGenerateColumns="False" 
            onselectedindexchanged="riskDetailGridView_SelectedIndexChanged" 
            onrowdatabound="riskDetailGridView_RowDataBound" 
            onsorting="riskDetailGridView_Sorting"  CssClass="gridview" 
            DataSourceID="SQLOverLimitList">
            <AlternatingRowStyle BackColor="Silver" />
            <RowStyle Font-Bold="true" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                         <%--This is a placeholder for the details GridView--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MasterRiskID" HeaderText="MasterRiskID" SortExpression="MasterRiskID" />
                <asp:BoundField DataField="RiskType" HeaderText="RiskType" SortExpression="RiskType" />
                <asp:BoundField DataField="RiskSource" HeaderText="RiskSource" SortExpression="RiskSource" />
                <asp:BoundField DataField="RiskScenario" HeaderText="RiskScenario" SortExpression="RiskScenario" />
                <asp:BoundField DataField="PrimaryOwner" HeaderText="PrimaryOwner" SortExpression="PrimaryOwner" />
                <asp:BoundField DataField="OversightParty" HeaderText="OversightParty" SortExpression="OversightParty" />
                <asp:BoundField DataField="ModifiedUser" HeaderText="ModifiedUser" SortExpression="ModifiedUser" />
                <asp:BoundField DataField="ModifiedDate" HeaderText="ModifiedDate" SortExpression="ModifiedDate" />
            </Columns>
            <EmptyDataTemplate>
                No Data to display
            </EmptyDataTemplate>
        </asp:GridView>

    </div>
           

        <asp:SqlDataSource ID="SQLOverLimitList" runat="server" 
          ConnectionString="<%$ ConnectionStrings:RiskRegister %>" 
          SelectCommand="[dbo].[RiskDetailAll]" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource> 

        <asp:SqlDataSource ID="SQLOverLimitDetail" runat="server"
          ConnectionString="<%$ ConnectionStrings:RiskRegister %>"
          SelectCommand="[dbo].[RiskDetail]" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource> 

</asp:Content>

