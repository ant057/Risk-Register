<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RiskAssessment.aspx.cs" Inherits="RiskAssessment" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" language="javascript">
</script>
    <h2><b>Risk Assessment</b></h2>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="riskInsertLabel" runat="server"></asp:Label>


            </td>
        </tr>
        <tr>
            <td class="TitleBar">
                <asp:Label ID="Label11" runat="server" Text="Applicable Entities" CssClass="style1"></asp:Label>
                <asp:SqlDataSource ID="entitiesDS" runat="server" 
                    
        ProviderName="System.Data.SqlClient" SelectCommand="[dbo].[EntitySelectAll]" 
                    SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
    <asp:SqlDataSource ID="riskTypeDS" runat="server"  
        ProviderName="System.Data.SqlClient" SelectCommand="[dbo].[Risk_TypeSelectAll]" 
        SelectCommandType="StoredProcedure"></asp:SqlDataSource>


                <asp:Button ID="selectAllButton" runat="server" CausesValidation="False" 
                    onclick="selectAllButton_Click" Text="Select All" CssClass="flatbutton" 
                    Width="70px" />


            </td>
        </tr>
        <tr>
            <td class="style34">
                <asp:CheckBoxList ID="entityCheckBoxList" runat="server" 
                    DataSourceID="entitiesDS" DataTextField="Entity_Name" 
                    DataValueField="Entity_ID" RepeatDirection="Horizontal" 
                    Font-Size="Smaller">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <table class="style3" width="100%">
                    <tr>
                        <td class="TitleBar" colspan="2">
                            <asp:Label ID="Label17" runat="server" Text="Master Risk Details"></asp:Label>
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="SubTitle" rowspan="1" style="width: 25%">
                <asp:Label ID="Label12" runat="server" Text="Risk Type"></asp:Label>
                                            &nbsp;</td>
                        <td>
                <asp:DropDownList ID="ddlRiskType" runat="server" 
                                        DataSourceID="riskTypeDS" DataTextField="Risk_Type_Description" 
                                        DataValueField="Risk_Type_ID" 
                                onselectedindexchanged="ddlRiskType_SelectedIndexChanged" 
                                AutoPostBack="True">
                </asp:DropDownList>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                            <asp:Label ID="Label68" runat="server" Text="Risk Source"></asp:Label>
                                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlRiskSource" runat="server">
                            </asp:DropDownList>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                <asp:Label ID="Label16" runat="server" Text="Primary Owner"></asp:Label>
                                        </td>
                        <td>
                            <asp:DropDownList ID="primaryOwnerDD" runat="server">
                            </asp:DropDownList>
                                    </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                <asp:Label ID="Label15" runat="server" Text="Oversight Party"></asp:Label>
                                        </td>
                        <td>
                            <asp:DropDownList ID="oversightPartyDD" runat="server">
                            </asp:DropDownList>
                                    </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td class="TitleBar">
                <asp:Label ID="Label1" runat="server" Text="Risk Scenario"></asp:Label>
                                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style33">
                <asp:TextBox ID="textRiskScenario" runat="server" Height="150px" Width="450px" 
                    TextMode="MultiLine"></asp:TextBox>
                                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 311px">
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style11">
                &nbsp;</td>
            <td>
                <asp:Button ID="saveButton0" runat="server" OnClientClick="return confirm('Are you sure you wish to save this Risk Assessment? A risk for each entity selected will be created.');"
                onclick="saveButton_Click"  Text="Save" Width="75px" CssClass="flatbutton" />
                <asp:Button ID="cancelButton0" runat="server" onclick="cancelButton_Click" 
                    Text="Cancel" Width="94px" CausesValidation="False" 
                    CssClass="flatbutton" />
                                            </td>
        </tr>
    </table>
</asp:Content>

