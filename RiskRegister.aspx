<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RiskRegister.aspx.cs" Inherits="RiskRegister" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style3
    {
        width: 100%;
    }
        .style4
        {
            width: 80%;
        }
        .style5
        {
            width: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><b>Risk Register</b></h2>
    <table class="style3">
    <tr>
        <td>
            <table class="style3">
                <tr>
                    <td>
            <asp:SqlDataSource ID="riskRegDS" runat="server" 
                            ProviderName="System.Data.SqlClient" SelectCommandType="StoredProcedure" 
                            SelectCommand="[dbo].[RiskRegister]">
          <%--      <SelectParameters>
                    <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                </SelectParameters>--%>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="entitiesDS" runat="server" 
                            ProviderName="System.Data.SqlClient" 
                            SelectCommand="[dbo].[RiskRegisterEntityChilds]" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter Name="Risk_ID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
                </table>
        </td>
        <td class="style4">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
        <td class="style5">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
                        <asp:Label ID="sort1Label" runat="server" Text="Sort By:"></asp:Label>
&nbsp;<asp:DropDownList ID="ddlSort1" runat="server">
            <asp:ListItem>RiskType</asp:ListItem>
            <asp:ListItem>RiskSource</asp:ListItem>
            <asp:ListItem>RiskScenario</asp:ListItem>
            <asp:ListItem>InherentRiskScore</asp:ListItem>
            <asp:ListItem>ControlsDetails</asp:ListItem>
            <asp:ListItem>ResidualRiskScore</asp:ListItem>
            <asp:ListItem>Entity</asp:ListItem>
        </asp:DropDownList>
                        <asp:Button ID="addFilterButton" runat="server" OnClick="addFilterButton_Click" 
                            Text="Add" CssClass="flatbutton" />
                        &nbsp;<asp:Button ID="removeFilterButton" runat="server" 
                onclick="removeFilterButton_Click" Text="Remove" Width="52px" CssClass="flatbutton" />
            <asp:Panel ID="Panel1" runat="server">
                <asp:Label ID="sort2Label" runat="server" 
    Text="Sort By:" Visible="False"></asp:Label>
                &nbsp;<asp:DropDownList ID="ddlSort2" runat="server" Visible="False">
                    <asp:ListItem>RiskType</asp:ListItem>
                    <asp:ListItem>RiskSource</asp:ListItem>
                    <asp:ListItem>RiskScenario</asp:ListItem>
                    <asp:ListItem>InherentRiskScore</asp:ListItem>
                    <asp:ListItem>ControlsDetails</asp:ListItem>
                    <asp:ListItem>ResidualRiskScore</asp:ListItem>
                    <asp:ListItem>Entity</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="sort3Label" runat="server" Text="Sort By:" Visible="False"></asp:Label>
                &nbsp;<asp:DropDownList ID="ddlSort3" runat="server" Visible="False">
                    <asp:ListItem>RiskType</asp:ListItem>
                    <asp:ListItem>RiskSource</asp:ListItem>
                    <asp:ListItem>RiskScenario</asp:ListItem>
                    <asp:ListItem>InherentRiskScore</asp:ListItem>
                    <asp:ListItem>ControlsDetails</asp:ListItem>
                    <asp:ListItem>ResidualRiskScore</asp:ListItem>
                    <asp:ListItem>Entity</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Button ID="sortButton" runat="server" OnClick="sortButton_Click" 
                    Text="Sort" CssClass="flatbutton" />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:GridView ID="reportGridView" runat="server" 
                style="margin-top: 0px" AutoGenerateColumns="False" DataKeyNames="RiskID" 
                DataSourceID="riskRegDS" onrowdatabound="reportGridView_RowDataBound" 
                AllowSorting="True" CssClass="gridview" Font-Names="Arial">
                <AlternatingRowStyle BackColor="Silver" />
                <Columns>
                <asp:TemplateField HeaderText="RiskType" SortExpression="RiskType">
                    <ItemTemplate>
                    <b><%# Eval("RiskType") %></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="RiskSource" SortExpression="RiskSource">
                    <ItemTemplate>
                    <b><%# Eval("RiskSource") %></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="RiskScenario" SortExpression="RiskScenario">
                    <ItemTemplate>
                    <b><%# Eval("RiskScenario") %></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="InherentRiskScore" SortExpression="InherentRiskScore">
                    <ItemTemplate>
                    <b><%# Eval("InherentRiskScore")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="ControlsDetails" SortExpression="ControlsDetails">
                    <ItemTemplate>
                    <b><%# Eval("ControlsDetails")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText ="ActionPlanDetails" SortExpression="ActionPlanDetails">
                    <ItemTemplate>
                    <b><%# Eval("ActionPlanDetails")%></b>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText ="ResidualRiskScore" SortExpression="ResidualRiskScore">
                    <ItemTemplate>
                    <b><%# Eval("ResidualRiskScore")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText ="Entity" SortExpression="Entity">
                    <ItemTemplate>
                    <b><%# Eval("Entity")%></b>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText ="Entities" SortExpression="Entities">
                    <ItemTemplate>
                        <asp:DataList ID="listViewChild" runat="server">
                        <ItemTemplate>
                                <b><%# Eval("Entity")%></b>
                                </ItemTemplate>
                        </asp:DataList>          
                    </ItemTemplate>
                </asp:TemplateField>--%>

                </Columns>
         </asp:GridView>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Button ID="excelButton" runat="server" onclick="excelButton_Click" 
                Text="Export to Excel" CssClass="flatbutton" />
        </td>
    </tr>
</table>
</asp:Content>

