<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RiskHistory.aspx.cs" Inherits="RiskHistory" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            height: 17px;
        }
        .auto-style4 {
            height: 304px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script language="javascript" type="text/javascript">

        function GetDetails(entityRiskLogId) {
            //WebService.SelectEntityRiskLog(entityRiskLogId, OnRequestComplete, OnError); //GetEntityRiskHistory
            WebService.GetEntityRiskHistory(entityRiskLogId, OnRequestComplete, OnError);
        }

        function OnRequestComplete(result) {

            var primaryOwner = document.getElementById("ctl00_ContentPlaceHolder1_primaryOwnerLabel");
            var oversightParty = document.getElementById("ctl00_ContentPlaceHolder1_oversightPartyLabel");
            var mitigating = document.getElementById("ctl00_ContentPlaceHolder1_mitigatingLabel");
            var frequency = document.getElementById("ctl00_ContentPlaceHolder1_frequencyLabel");
            var severity = document.getElementById("ctl00_ContentPlaceHolder1_severityLabel");
            var inherentRisk = document.getElementById("ctl00_ContentPlaceHolder1_inherentRiskScoreLabel");
            var adjustment = document.getElementById("ctl00_ContentPlaceHolder1_adjustmentLabel");
            var residualRisk = document.getElementById("ctl00_ContentPlaceHolder1_residualLabel");

            primaryOwner.innerHTML = "";
            oversightParty.innerHTML = "";
            mitigating.innerHTML = "";
            frequency.innerHTML = "";
            severity.innerHTML = "";
            inherentRisk.innerHTML = "";
            adjustment.innerHTML = "";
            mitigating.innerHTML = "";
            residualRisk.innerHTML = "";

            primaryOwner.innerHTML = result.PrimaryOwner;
            oversightParty.innerHTML = result.OversightParty;
            mitigating.innerHTML = result.MitigatingControls;
            frequency.innerHTML = result.Frequency;
            severity.innerHTML = result.Severity;
            inherentRisk.innerHTML = result.InherentRiskScore;
            adjustment.innerHTML = result.Adjustment;
            residualRisk.innerHTML = result.ResidualRiskScore;

        }

        //function OnRequestComplete(result) {

        //    var primaryOwner = document.getElementById("ctl00_ContentPlaceHolder1_primaryOwnerLabel");
        //    var oversightParty = document.getElementById("ctl00_ContentPlaceHolder1_oversightPartyLabel");
        //    var mitigating = document.getElementById("ctl00_ContentPlaceHolder1_mitigatingLabel");
        //    var frequency = document.getElementById("ctl00_ContentPlaceHolder1_frequencyLabel");
        //    var severity = document.getElementById("ctl00_ContentPlaceHolder1_severityLabel");
        //    var inherentRisk = document.getElementById("ctl00_ContentPlaceHolder1_inherentRiskScoreLabel");
        //    var adjustment = document.getElementById("ctl00_ContentPlaceHolder1_adjustmentLabel");
        //    var residualRisk = document.getElementById("ctl00_ContentPlaceHolder1_residualLabel");

        //    primaryOwner.innerHTML = "";
        //    oversightParty.innerHTML = "";
        //    mitigating.innerHTML = "";
        //    frequency.innerHTML = "";
        //    severity.innerHTML = "";
        //    inherentRisk.innerHTML = "";
        //    adjustment.innerHTML = "";
        //    mitigating.innerHTML = "";
        //    residualRisk.innerHTML = "";

        //    //primaryOwner.innerHTML = result.rows[0].Primary_Owner_Id;
        //    //oversightParty.innerHTML = result.OversightPartyId;
        //    //mitigating.innerHTML = result.MitigatingControls;
        //    //frequency.innerHTML = result.Frequency;
        //    //severity.innerHTML = result.Severity;
        //    //inherentRisk.innerHTML = result.InherentRiskScore;
        //    //adjustment.innerHTML = result.Adjustment;
        //    //residualRisk.innerHTML = result.ResidualRiskScore;

        //}

        function OnError(result) {
            var lbl = document.getElementById("ctl00_ContentPlaceHolder1_primaryOwnerLabel");
            lbl.innerHTML = "<b>" + result.get_message() + "</b>";
        }

    </script>
    <h2><b><asp:Label Text = "Risk History" ID="eLabel" runat="server"></asp:Label></b></h2>
    <table class="auto-style1">
        <tr>
            <td width="20%">
                <asp:Label runat="server" Text="Risk ID : " Font-Bold="True" Font-Names="Verdana"></asp:Label>
                <asp:Label ID="riskLabel" runat="server"></asp:Label>
            </td>
            <td width="25px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="20%">
                <asp:Label ID="Label20" runat="server" Text="Entity : " Font-Bold="True" Font-Names="Verdana"></asp:Label>
                <asp:Label ID="entityLabel" runat="server"></asp:Label>
            </td>
            <td width="25px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td width="20%" class="TitleBar">
                <asp:Label ID="Label1" runat="server" Text="Risk Log"></asp:Label>
            </td>
            <td width="25px">
                &nbsp;</td>
            <td class="TitleBar">
                <asp:Label ID="Label21" runat="server" Text="Risk Scenario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" rowspan="2">
                <asp:ListBox ID="riskLogListBox" runat="server" Height="275px" Width="100%"
                    onchange="GetDetails(this.value);"></asp:ListBox>
            </td>
            <td width="25px">
                &nbsp;
            </td>
            <td>
                <asp:Label ID="scenarioLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="25px">
                &nbsp;</td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="275px">
                    <table frame="border" style="border-style: groove; table-layout: auto; border-spacing: inherit; list-style-type: none;">
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label4" runat="server" Text="Primary Owner:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="primaryOwnerLabel" runat="server"></asp:Label>
                            </td>
                            <td width="40%">
                                <asp:Label ID="Label6" runat="server" Text="Oversight Party:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="oversightPartyLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label8" runat="server" Text="Frequency:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="frequencyLabel" runat="server"></asp:Label>
                            </td>
                            <td width="40%">
                                <asp:Label ID="Label10" runat="server" Text="Severity:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="severityLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label12" runat="server" Text="Inherent Risk Score:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                &nbsp;<asp:Label ID="inherentRiskScoreLabel" runat="server"></asp:Label>
                            </td>
                            <td width="40%">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label14" runat="server" Text="Mitigating Controls:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                            </td>
                            <td width="40%">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px" colspan="2">
                                <asp:Label ID="mitigatingLabel" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label16" runat="server" Text="Adjustment:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="adjustmentLabel" runat="server"></asp:Label>
                            </td>
                            <td width="40%">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="400px">
                                <asp:Label ID="Label18" runat="server" Text="Residual Risk Score:" Font-Bold="True" Font-Names="Verdana" ForeColor="#006699"></asp:Label>
                                <asp:Label ID="residualLabel" runat="server"></asp:Label>
                            </td>
                            <td width="40%">
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td width="20%">

            </td>
            <td width="25px">
                
                &nbsp;</td>
            <td>
                
            </td>
        </tr>
        <tr>
            <td width="20%">&nbsp;</td>
            <td width="25px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
<%--        <tr>
            <td width="20%">&nbsp;</td>
            <td>
                <table width="100%">
                    <caption>
                        addsaasdas
                    </caption>
                    <thead>
                        <tr>
                            <td>fuck shit</td>
                            <td></td>
                        </tr>
                    </thead>
                    <tr>
                        <td>

                        </td>
                        <td>

                        </td>
                        <td>

                        </td>
                        <td>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
    </table>
</asp:Content>

