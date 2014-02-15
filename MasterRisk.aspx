<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MasterRisk.aspx.cs" Inherits="MasterRisk" %>



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
    <h2><b>Master Risk Detail</b></h2>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="riskInsertLabel" runat="server"></asp:Label>


            </td>
        </tr>
        <tr>
            <td class="TitleBar">
                <asp:Label ID="Label11" runat="server" Text="Applicable Entities" CssClass="style1"></asp:Label>
    <asp:SqlDataSource ID="riskTypeDS" runat="server"  
        ProviderName="System.Data.SqlClient" SelectCommand="[dbo].[Risk_TypeSelectAll]" 
        SelectCommandType="StoredProcedure"></asp:SqlDataSource>


            </td>
        </tr>
        <tr>
            <td class="style34">
                <asp:CheckBoxList ID="entityCheckBoxList" runat="server" RepeatDirection="Horizontal" 
                    Font-Size="Smaller" Enabled="False">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <table class="DataTable" width="100%">
                    <tr>
                        <td class="TitleBar" colspan="2">
                            <asp:Label ID="Label17" runat="server" Text="Master Risk Details"></asp:Label>
                        &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="SubTitle" style="width: 25%">
                <asp:Label ID="lab" runat="server" Text="Risk ID"></asp:Label>
                                            </td>
                        <td>
                            <asp:Label ID="riskIdLabel" runat="server"></asp:Label>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle" style="width: 25%">
                <asp:Label ID="lab0" runat="server" Text="Risk Date"></asp:Label>
                                            </td>
                        <td>
                            <asp:TextBox ID="dateTextBox" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle" style="width: 25%">
                <asp:Label ID="lab1" runat="server" Text="Risk Entered By"></asp:Label>
                                            </td>
                        <td>
                            <asp:Label ID="enteredByLabel" runat="server"></asp:Label>
                                        </td>
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
                                AutoPostBack="True" Enabled="False">
                </asp:DropDownList>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                            <asp:Label ID="Label68" runat="server" Text="Risk Source"></asp:Label>
                                            &nbsp;</td>
                        <td>
                            <asp:DropDownList ID="ddlRiskSource" runat="server" Enabled="False">
                            </asp:DropDownList>
                                        </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                <asp:Label ID="Label16" runat="server" Text="Primary Owner"></asp:Label>
                                        </td>
                        <td>
                            <asp:DropDownList ID="primaryOwnerDD" runat="server" Enabled="False">
                            </asp:DropDownList>
                                    </td>
                    </tr>
                    <tr>
                        <td class="SubTitle">
                <asp:Label ID="Label15" runat="server" Text="Oversight Party"></asp:Label>
                                        </td>
                        <td>
                            <asp:DropDownList ID="oversightPartyDD" runat="server" Enabled="False">
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
                    TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
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
                <asp:Button ID="updateButton" runat="server" OnClientClick="return confirm('Are you sure you wish to save this Risk Assessment? A risk for each entity selected will be created.');"
                onclick="saveButton_Click"  Text="Save" Width="75px" CssClass="flatbutton" 
                    Visible="False" />
                <asp:Button ID="editButton" runat="server" CausesValidation="False" 
                    CssClass="flatbutton" onclick="editButton_Click" 
                    onclientclick="return alert('Editing this record will affect the entities that share this risk.');" 
                    Text="Edit" Width="50px" />
                <asp:Button ID="cancelButton0" runat="server" onclick="cancelButton_Click" 
                    Text="Cancel" Width="94px" CausesValidation="False" 
                    CssClass="flatbutton" />
                                            </td>
        </tr>
    </table>
</asp:Content>

