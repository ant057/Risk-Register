<%@ Page Title="Master Dashboard" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MasterDash.aspx.cs" Inherits="MasterDash" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
function switchViews(obj)
    {
        var div = document.getElementById(obj);
        var img = document.getElementById('img' + obj);
        
        if (div.style.display == "none") {
            div.style.display = "inline";
            img.src = "Images/closes.jpg";
            img.alt = "Close to view other risks";
        }
        else {
            div.style.display = "none";
            img.src = "Images/details.jpg";
            img.alt = "Expand to show entity risks";
        }
    }
</script>
    <div>
        &nbsp;
        <h2><b><asp:Label Text = "Risk Dashboard" ID="eLabel" runat="server"></asp:Label></b></h2>
        </div>

    <div>
        <br />
        &nbsp;<asp:Label ID="Label7" runat="server" Text="Risk Register :" 
            Font-Bold="True"></asp:Label>
        <asp:RadioButtonList ID="dashButtonList" runat="server" AutoPostBack="True" 
            onselectedindexchanged="dashButtonList_SelectedIndexChanged" 
            RepeatDirection="Horizontal">
            <asp:ListItem>Group (Corporate)</asp:ListItem>
            <asp:ListItem Selected="True">Business Unit</asp:ListItem>
            <asp:ListItem>Group &amp; Consolidated</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="updateLabel" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Quick Search"></asp:Label>
        <asp:TextBox ID="searchCriteriaText" runat="server" ></asp:TextBox>
        <asp:Button ID="searchButton" runat="server" CssClass="flatbutton" Text="Go" 
            onclick="searchButton_Click" />
        <asp:Button ID="Button1" runat="server" PostBackUrl="~/RiskAssessment.aspx" Text="New Risk" CssClass="flatbutton" />
    <br />
        <table width="100%">
            <tr>
                <td>

                </td>
                <td width="30%">

&nbsp;<asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Select Register :"></asp:Label>

                    <asp:DropDownList ID="entityExportddl" runat="server">
                    </asp:DropDownList>
&nbsp;<asp:Button ID="Button2" runat="server" CssClass="flatbutton" OnClick="Button2_Click" Text="Export" />

                </td>
            </tr>
        </table>
        <%--        this begins the expandable grid view--%>
        <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="RiskID"
                DataSourceID="SqlDataSource1" OnRowDataBound="gv_RowDataBound"
                AllowPaging="False" CssClass="gridview" >
            <AlternatingRowStyle BackColor="Silver" />
            <RowStyle Font-Bold="False" />
           <Columns>
              <asp:TemplateField>
                        <ItemTemplate>
                            <a href="javascript:switchViews('div<%# Eval("RiskID") %>');">
                                <img id="imgdiv<%# Eval("RiskID") %>" alt="Click to show/hide orders" border="0" src="Images/details.jpg" />
                            </a>
                        </ItemTemplate>
               </asp:TemplateField> 
               <asp:BoundField DataField="RiskID" HeaderText="RiskID" SortExpression="RiskID" ShowHeader="False" />
               <asp:HyperLinkField DataNavigateUrlFields="RiskID" DataNavigateUrlFormatString="MasterRisk.aspx?riskId={0}"
                    DataTextField="RiskID" HeaderText="RiskID">
                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    <HeaderStyle Width="40px" />
                </asp:HyperLinkField>     
               <asp:BoundField DataField="RiskType" HeaderText="RiskType" SortExpression="RiskType" />
                <asp:BoundField DataField="RiskSource" HeaderText="RiskSource" SortExpression="RiskSource" />
                <asp:BoundField DataField="RiskScenario" HeaderText="RiskScenario" SortExpression="RiskScenario" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="true" />
<%--                <asp:BoundField DataField="PrimaryOwner" HeaderText="PrimaryOwner" SortExpression="PrimaryOwner" />
                <asp:BoundField DataField="OversightParty" HeaderText="OversightParty" SortExpression="OversightParty" />--%>
                <asp:TemplateField>
                        <ItemTemplate>
                            </td></tr>
                            <tr>
                                <td colspan="100%">
                                    <div id="div<%# Eval("RiskID") %>" style="display:none;position:relative;left:25px;" >
                                        <asp:GridView ID="GridView2" runat="server" Width="80%"
                                            AutoGenerateColumns="false" DataKeyNames="EntityRiskID"
                                            EmptyDataText="No entities for this risk." 
                                            CssClass="gridview" >
                                            <AlternatingRowStyle BackColor="Silver" />
                                            <RowStyle Font-Bold="true" />
                                            <Columns>
                                                <asp:HyperLinkField DataNavigateUrlFields="EntityRiskID" DataNavigateUrlFormatString="RiskDetail.aspx?entityRiskId={0}"
                                                    DataTextField="EntityRiskID" HeaderText="EntityRiskID">
                                                    <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                    <HeaderStyle Width="40px" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="Entity" HeaderText="Entity" HtmlEncode="False"/>
                                                <asp:BoundField DataField="Frequency" HeaderText="Frequency" HtmlEncode="False" />
                                                <asp:BoundField DataField="Severity" HeaderText="Severity" />
                                                <asp:BoundField DataField="InherentRiskScore" HeaderText="InherentRiskScore" />
                                                <asp:BoundField DataField="ResidualRiskScore" HeaderText="ResidualRiskScore" />
                                                <asp:BoundField DataField="HasActionPlan" HeaderText="HasActionPlan" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                 </asp:TemplateField>
           </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=Antdawg\SQLExpress;Initial Catalog=RiskRegister;Integrated Security=True"
                SelectCommand="EXEC [dbo].[MasterRiskAll] 'Business Unit'" SelectCommandType="Text">
        </asp:SqlDataSource>


    </div>
</asp:Content>

