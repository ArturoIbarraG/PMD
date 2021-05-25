<%@ Page Title="Bienvendido" Language="VB" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" AutoEventWireup="false" Inherits="PMD_WAS.Bienvenido" CodeBehind="Bienvenido.aspx.vb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        h4 {
            padding-bottom:0px !important;
        }

        .container-body {
            background-color:transparent !important;
            border: none !important;
        }

        .container-body .container-fluid {
            margin:0px !important;
            padding:0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-4">
                <h4>
                    
                </h4>
            </div>
            <div class="col-4">
               
            </div>
            <div class="col4">

            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Panel ID="pnlContainer" runat="server"></asp:Panel>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="updBienvenido" runat="server">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin-top: 2%; margin-bottom: 6%; font-family: Times New Roman,Times,serif;">

        <br />

        <%-- <div style="text-align: center;">
            <h4 style="font-size: x-large;">
                Liga del AQS&nbsp;
            </h4>
            <a href="http://162.198.100.7/AQS/index.php" style="font-family: arial, sans-serif;
                font-size: 20px; font-style: normal; font-variant: normal; font-weight: normal;
                letter-spacing: normal; line-height: normal; orphans: auto; text-align: left;
                text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px;
                -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255);">http://162.198.100.7/AQS/index.php
            </a>
         
        </div>--%>
    </div>
</asp:Content>
