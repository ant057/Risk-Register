<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AggregatedScores.aspx.cs" Inherits="AggregatedScores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2><b><asp:Label Text = "Aggregate Scoring" ID="eLabel" runat="server"></asp:Label></b></h2>
    <asp:GridView ID="AggregateScores" runat="server" CssClass="gridview" Width="50%">
    </asp:GridView>
</asp:Content>

