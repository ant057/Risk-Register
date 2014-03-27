<%@ Page Title="Risk Detail" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RiskDetail.aspx.cs" Inherits="RiskDetail" ValidateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script>

          $(document).ready(function () {
              $("#ctl00_ContentPlaceHolder1_completedDateTextBox").datepicker();
          });
          $(document).ready(function () {
              $("#ctl00_ContentPlaceHolder1_implementDateTextBox").datepicker();
          });
          
  </script>
    <script type="text/javascript" language="javascript">

        function definitions() {
            window.open('HtmlPage.html', 'Definitons', 'toolbar=0, height=500, width=800, resizable=1, scrollbars=1');
            window.focus();

        }
  </script>
    <div>
&nbsp;
    <h2><b>Risk Detail</b></h2>
        <asp:SqlDataSource ID="riskTypeDS" runat="server"  
            ProviderName="System.Data.SqlClient" SelectCommand="[dbo].[Risk_TypeSelectAll]" 
            SelectCommandType="StoredProcedure"></asp:SqlDataSource>


                                <asp:SqlDataSource ID="riskSourceDS" runat="server" 
        ProviderName="System.Data.SqlClient" 
        SelectCommand="[dbo].[Risk_SourceSelectAll]" 
        SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:Parameter Direction="ReturnValue" Name="RETURN_VALUE" Type="Int32" />
                                    </SelectParameters>
    </asp:SqlDataSource>
        <table class="DataTable">
            <tr>
                <td colspan="2" dir="rtl" style="width: 10%">
                                    <asp:Button ID="deleteRiskButton" runat="server" Text="Delete Risk" 
                                        OnClientClick="return confirm('Are you sure you wish to DELETE? This will delete the selected Risk / associated Action Plan / associated Documentation.');" 
                                        onclick="deleteRiskButton_Click" CssClass="flatbutton" />
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 10%">
                <asp:Button ID="editButton" runat="server" Text="Edit" Width="57px" 
                        onclick="editButton_Click" CssClass="flatbutton" />
                    <asp:Button ID="saveButton" runat="server" Text="Save" Visible="False" 
                        Width="79px" onclick="saveButton_Click" 
                        OnClientClick="return confirm('Are you sure you wish to Save?');" 
                        CssClass="flatbutton" />
                    <asp:Button ID="cancelButton" runat="server" OnClick="cancelButton_Click" 
                        Text="Cancel" Visible="False" CssClass="flatbutton" />
                                    <asp:Label ID="riskUpdateLabel" runat="server"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td class="TitleBar" colspan="2" style="width: 10%">
                                    <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Size="Large" 
                                        Text="Risk Entities"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                                    <asp:CheckBoxList ID="entityCheckBoxList" runat="server" Enabled="False" 
                                        ondatabinding="Page_Load" RepeatDirection="Horizontal" Font-Size="Smaller">
                                    </asp:CheckBoxList>
                    </td>
            </tr>
            <tr>
                <td class="TitleBar" colspan="2" style="width: 10%">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Large" 
                                        Text="Risk Details"></asp:Label>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label93" runat="server" Text="Risk ID"></asp:Label>


                                &nbsp;
                                    


                                    



                                    <asp:LinkButton ID="historyLinkBtn" runat="server" OnClick="historyLinkBtn_Click">History</asp:LinkButton>
                                    


                                    



                                </td>
                <td>
                                    <asp:Label ID="riskIdLabel" runat="server"></asp:Label>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label runat="server" Text="Entity"></asp:Label>


                                </td>
                <td>
                                    <asp:Label ID="entityLabel" runat="server"></asp:Label>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label5" runat="server" Text="Risk Type"></asp:Label>


                                </td>
                <td>
                                    <asp:DropDownList ID="ddlRiskType" runat="server" Enabled="False" 
                                        Height="23px" DataSourceID="riskTypeDS" 
                                        DataTextField="Risk_Type_Description" DataValueField="Risk_Type_ID" 
                                        onselectedindexchanged="ddlRiskType_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label27" runat="server" Text="Risk Source"></asp:Label>


                                </td>
                <td>
                                    <asp:DropDownList ID="ddlRiskSource" runat="server" 
                                        Enabled="False">
                                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label8" runat="server" Text="Risk Date"></asp:Label>
                                </td>
                <td>
                                    <asp:TextBox ID="riskDateTextBox" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label89" runat="server" Text="Risk Entered By"></asp:Label>
                                </td>
                <td>
                                    <asp:Label ID="userLabel" runat="server"></asp:Label>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label10" runat="server" Text="Risk Scenario"></asp:Label>
                                </td>
                <td>
                                    <asp:TextBox ID="riskScenarioTextBox" runat="server" Height="205px" 
                                        Width="640px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle">
                                    <asp:Label ID="Label29" runat="server" Text="Primary Owner"></asp:Label>
                </td>
                <td>
                                    <asp:DropDownList ID="ddlPrimaryOwner" runat="server" Enabled="False">
                                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="SubTitle">
                                    <asp:Label runat="server" Text="Oversight Party"></asp:Label>
                </td>
                <td>
                                    <asp:DropDownList ID="ddlOversightParty" runat="server" Enabled="False">
                                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="SubTitle">
                                    <asp:Label ID="Label96" runat="server" Text="Not Applicable"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="naCheckBox" runat="server" AutoPostBack="True" 
                        Enabled="False" oncheckedchanged="naCheckBox_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td class="TitleBar" colspan="2" style="width: 10%">
                                <asp:Label ID="Label95" runat="server" Font-Bold="True" Font-Size="Large" 
                                    Text="Inherent Risk Scoring:"></asp:Label>
                            &nbsp;<asp:Label ID="entityTitleLabel" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label14" runat="server" Text="Frequency"></asp:Label>
                                <asp:ImageButton ID="defImageButton1" runat="server" Height="17px" 
                                    ImageUrl="~/QuestionMark.jpg" Width="23px" onclientclick="definitions();" />
                                </td>
                <td>
                                    <asp:DropDownList ID="frequencyTextBox" runat="server" AutoPostBack="True" 
                                        Enabled="False" Height="22px" 
                                        onselectedindexchanged="frequencyTextBox_SelectedIndexChanged">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem Value="1">1 - Remote</asp:ListItem>
                                        <asp:ListItem Value="2">2 - Occasional</asp:ListItem>
                                        <asp:ListItem Value="3">3 - Probable</asp:ListItem>
                                        <asp:ListItem Value="4">4 - Frequent</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label15" runat="server" Text="Severity"></asp:Label>
                                <asp:ImageButton ID="defImageButton2" runat="server" Height="17px" 
                                    ImageUrl="~/QuestionMark.jpg" Width="23px" onclientclick="definitions();" />
                                </td>
                <td>
                                    <asp:DropDownList ID="severityTextBox" runat="server" 
                                        AutoPostBack="True" Enabled="False" Height="22px" 
                                        onselectedindexchanged="severityTextBox_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label9" runat="server" Text="Inherent Risk Score"></asp:Label>
                                </td>
                <td>
                                    <asp:TextBox ID="riskScoreTextBox" runat="server" ReadOnly="True" 
                                        Font-Bold="True"></asp:TextBox>
                                </td>
            </tr>
            <tr>
                <td class="TitleBar" colspan="2" style="width: 10%">
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Large" 
                                    Text="Controls / Residual Detail:"></asp:Label>
                            &nbsp;<asp:Label ID="entityTitleLabel0" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="Label16" runat="server" Text="Mitigating Controls / Actions"></asp:Label>
                            </td>
                <td>
                    <FTB:FreeTextBox id="mitigatingControlsFTB" runat="Server" 
                        AllowHtmlMode="False" ReadOnly="False" />
                            </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="Label17" runat="server" Text="Adjustment"></asp:Label>
                                <asp:ImageButton ID="defImageButton" runat="server" Height="17px" 
                                    ImageUrl="~/QuestionMark.jpg" Width="23px" 
                                    onclientclick="definitions();" />
                            </td>
                <td>
                                <asp:DropDownList ID="residualAdjustmentDDL" runat="server" AutoPostBack="True" 
                                    Enabled="False" 
                                    onselectedindexchanged="residualAdjustmentDDL_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="Label21" runat="server" Text="Residual Risk Score"></asp:Label>
                            </td>
                <td>
                                <asp:TextBox ID="residualScoreText" runat="server" ReadOnly="True" 
                                    Width="146px" Font-Bold="True"></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <td class="TitleBar" colspan="2" style="width: 10%">
                                <asp:Label runat="server" Font-Bold="True" Font-Size="Large" 
                                    Text="Action Details:"></asp:Label>
                            &nbsp;<asp:Label ID="entityTitleLabel1" runat="server"></asp:Label>
                            </td>
            </tr>
            <tr>
                <td style="width: 10%">
                <asp:Button ID="createActionButton" runat="server" Text="Create Action Plan" 
                    onclick="createActionButton_Click" 
                        OnClientClick="return confirm('Are you sure you wish to create an Action Plan?');" 
                        CssClass="flatbutton" Width="116px"/>
                    <asp:Button ID="actionHistoryButton" runat="server" Text="Action Plan History" 
                        Width="121px" CssClass="flatbutton" OnClick="actionHistoryButton_Click" />
                                        </td>
                <td>
                                &nbsp;</td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label runat="server" Text="Action Details"></asp:Label>
                                        </td>
                <td>
                                <asp:TextBox ID="actionDetailTextBox" runat="server" Height="130px" Width="640px" 
                                                TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                        </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                            <asp:Label ID="Label91" runat="server" Text="Action Created Date"></asp:Label>
                                        </td>
                <td>
                                            <asp:TextBox ID="actionCreatedTextBox" runat="server"></asp:TextBox>
                                        </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                            <asp:Label ID="Label92" runat="server" Text="Action Entered By"></asp:Label>
                                        </td>
                <td>
                                            <asp:Label ID="actionUserLabel" runat="server"></asp:Label>
                                        </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                    <asp:Label ID="Label28" runat="server" Text="Action Owner"></asp:Label>
                                </td>
                <td>
                                    <asp:DropDownList ID="ddlActionOwner" runat="server" Enabled="False">
                                    </asp:DropDownList>
                                </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="partyLabel" runat="server" Text="Follow Up Party"></asp:Label>
                                        </td>
                <td>
                                            <asp:DropDownList ID="ddlFollowUpParty" runat="server" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="implementLabel" runat="server" Text="Implementation Date"></asp:Label>
                                        </td>
                <td>
                                <input ID="implementDateTextBox" runat="server" readonly="readonly" />
                                        </td>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                <asp:Label ID="completeLabel" runat="server" Text="Completion Date"></asp:Label>
                                        </td>
                <td>
                    <input id="completedDateTextBox" runat="server" type="text" readonly="readonly"/>
            </tr>
            <tr>
                <td class="SubTitle" style="width: 10%">
                                            <asp:Label ID="Label26" runat="server" Text="Action Plan Status"></asp:Label>
                                        </td>
                <td>
                                            <asp:DropDownList ID="ddlActionStatus" runat="server" Height="23px" 
                                                Width="136px" Enabled="False">
                                                <asp:ListItem>Not Required</asp:ListItem>
                                                <asp:ListItem>Not Started</asp:ListItem>
                                                <asp:ListItem>In Process</asp:ListItem>
                                                <asp:ListItem>Completed</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 10%">
                <asp:Button ID="documentsButton" runat="server" Text="View Documents" 
                        onclick="documentsButton_Click" CssClass="flatbutton" />
                </td>
                <td>
                <asp:Button ID="excelButton" runat="server" Text="Excel Export" Width="95px" 
                        onclick="excelButton_Click" CssClass="flatbutton" />
                </td>
            </tr>
        </table>
</div>
</asp:Content>