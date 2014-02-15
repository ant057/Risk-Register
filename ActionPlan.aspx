<%@ Page Title="Action Plan" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ActionPlan.aspx.cs" Inherits="ActionPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .auto-style1 {
            height: 146px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
            $(document).ready(function () {
                $("#ContentPlaceHolder1_completionDateTextBox").datepicker();
            });
            $(document).ready(function () {
                $("#ContentPlaceHolder1_implementDateTextBox").datepicker();
            });
    </script>
    <script language="javascript" type="text/javascript">

        function validate() {
            var estimateDate = document.getElementById("<%=completionDateTextBox.ClientID%>");
            var completionDate = document.getElementById("<%=implementDateTextBox.ClientID%>");
            if (estimateDate < completionDate) {
                alert("Completion date cannot be before implementation date.");
            }
        }
    </script>
    <h2><b>Action Plan</b></h2>
    <table class="TableLabel" width="100%">
        <tr>
            <td class="style9">
                            <asp:Label ID="actionSaveLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <table class="style8">
                    <tr>
                        <td class="style10">
                            <asp:Label ID="Label1" runat="server" Text="Risk ID:"></asp:Label>
                        </td>
                        <td class="style39">
                            &nbsp;<asp:Label ID="riskIdLabel" runat="server"></asp:Label>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TitleBar">
                <asp:Label ID="Label2" runat="server" Text="Risk Scenario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style12">
                <asp:TextBox ID="riskScenarioTextBox" runat="server" Height="150px" ReadOnly="True" 
                    Width="450px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="TitleBar">
                <asp:Label ID="Label3" runat="server" Text="Action Plan:"></asp:Label>
                <asp:Label ID="entityTitleLab" runat="server"></asp:Label>
            &nbsp;<asp:RequiredFieldValidator ID="actionDetailValidator" runat="server" 
                    ControlToValidate="actionDetailTextBox" 
                    ErrorMessage="Must include action details" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <asp:TextBox ID="actionDetailTextBox" runat="server" Height="155px" 
                    Width="450px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style9">
                <table class="style3">
                    <tr>
                        <td class="TitleBar" colspan="2">
                            <asp:Label ID="Label10" runat="server" Text="Action Plan Details"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1" colspan="1">
                            <table class="DataTable" width="100%">
                                <tr>
                                    <td class="SubTitle" width="20%">
                            <asp:Label ID="Label4" runat="server" Text="Primary Owner"></asp:Label>
                                    </td>
                                    <td>
                            <asp:DropDownList ID="ddlPrimaryOwner" runat="server">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubTitle" width="20%">
                            <asp:Label ID="Label5" runat="server" Text="Follow Up Party"></asp:Label>
                                    </td>
                                    <td>
                            <asp:DropDownList ID="ddlFollowUpParty" runat="server">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubTitle" width="20%">
                                        <asp:Label ID="Label9" runat="server" Text="Action Plan Status"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlActionStatus" runat="server" 
                                            Width="136px">
                                            <asp:ListItem>Not Required</asp:ListItem>
                                            <asp:ListItem>Not Started</asp:ListItem>
                                            <asp:ListItem>In Process</asp:ListItem>
                                            <asp:ListItem>Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubTitle" width="20%">
                            <asp:Label ID="Label6" runat="server" Text="Action Plan Implementation Date"></asp:Label>
                                    </td>
                                    <td>
                            <input ID="implementDateTextBox" runat="server" readonly="readonly"
                                width="200px">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="SubTitle" width="20%">
                            <asp:Label ID="Label8" runat="server" Text="Estimated Completion Date"></asp:Label>
                                    </td>
                                    <td>
                            <input ID="completionDateTextBox" runat="server" readonly="readonly"
                                width="200px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="auto-style1">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="saveButton" runat="server" onclick="saveButton_Click" OnClientClick="return confirm('Are you sure you wish to save this Action Plan?');"
                                Text="Save Action Plan" Width="102px" CssClass="flatbutton" />
                            <asp:Button ID="cancelButton" runat="server" CssClass="flatbutton" 
                                onclick="cancelButton_Click" Text="Cancel" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

