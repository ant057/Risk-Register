<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RiskHistory.aspx.cs" Inherits="RiskHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 18px;
        }
        .auto-style3 {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><b><asp:Label Text = "Risk History" ID="eLabel" runat="server"></asp:Label></b></h2>
    <table class="auto-style1">
        <tr>
            <td width="20%">&nbsp;</td>
            <td>
                <asp:Label runat="server" Text="Risk ID : "></asp:Label>
                <asp:Label ID="riskLabel" runat="server"></asp:Label>
&nbsp;<asp:Label ID="Label20" runat="server" Text="Entity : "></asp:Label>
                <asp:Label ID="entityLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="auto-style3">
                <asp:Label ID="Label1" runat="server" Text="Risk Log"></asp:Label>
            </td>
            <td class="auto-style3">
                <asp:Label ID="Label3" runat="server" Text="Risk Scenario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%">
                <asp:ListBox ID="riskLogListBox" runat="server" Height="300px" Width="75%"></asp:ListBox>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" Width="35%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Details"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <table class="auto-style1">
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label4" runat="server" Text="Primary Owner:"></asp:Label>
                                <asp:Label ID="primaryOwnerLabel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Oversight Party:"></asp:Label>
                                <asp:Label ID="oversightPartyLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label8" runat="server" Text="Frequency:"></asp:Label>
                                <asp:Label ID="frequencyLabel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Severity:"></asp:Label>
                                <asp:Label ID="severityLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label12" runat="server" Text="Inherent Risk Score:"></asp:Label>
                                &nbsp;<asp:Label ID="inherentRiskScoreLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label14" runat="server" Text="Mitigating Controls:"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style2" colspan="2" width="30%">
                                <asp:Label ID="mitigatingLabel" runat="server"></asp:Label>
                            </td>
                            <td class="auto-style2"></td>
                        </tr>
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label16" runat="server" Text="Adjustment:"></asp:Label>
                                <asp:Label ID="adjustmentLabel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label18" runat="server" Text="Residual Risk Score:"></asp:Label>
                                <asp:Label ID="residualLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="30%">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

