<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.Configuraciones" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Title="Configuracion" Codebehind="Configuraciones.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        select {
            max-width: 100%;
            width: 350px;
        }

        input {
            max-width: 100%;
        }

        .item {
            padding: 7px;
        }

        .tabla-pmd > .item:nth-child(2n+1):not(:first-child) {
            background-color: #EEE;
        }

        .table {
            max-width: 100%;
        }

        .row-header {
            border-bottom: 2px solid rgba(200,200,200,0.5);
            padding: 7px;
            margin-bottom: 7px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updConfiguracion" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h2>Correos</h2>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-8">
                        <span style="font-size: medium;">Correo Alcalde:</span>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtCorreoAlcalde" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-8">
                        <span style="font-size: medium;">Correo Auditoria Interna:</span>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtAuditoriaInterna" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-8">
                        <span style="font-size: medium;">Correo Auditoria Externa:</span>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtAuditoriaExterna" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <h2>Eventos</h2>
                    </div>
                </div><br />
                <div class="row">
                    <div class="col-8">
                         <span style="font-size: medium;">Días de espera para solicitar evento:</span>
                    </div>
                    <div class="col-4">
                        <asp:TextBox ID="txtDiasEvento" runat="server" Width="100%"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row row-header">
                    <div class="col-8">
                        <b>ETAPA</b>
                    </div>
                    <div class="col-4">
                        <b>DURACIÓN</b>
                    </div>
                </div>
                <asp:Repeater ID="rptEtapaEventos" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-8">
                                <span><%# Eval("etapa") %></span>
                            </div>
                            <div class="col-4">
                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                <asp:TextBox ID="txtHoras" runat="server" Width="80%" Text='<%# Eval("horas") %>' Style="display: inline-block;"></asp:TextBox>
                                <p style="display: inline-block;">horas.</p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="row">
                    <div class="col-8"></div>
                    <div class="col-4" style="border-top: 1px solid #AAA;">
                        <h2>
                            <asp:Label ID="lblTotalHoras" runat="server" Text="100 horas."></asp:Label></h2>
                    </div>
                </div>
                <hr />
                <div class="row">

                    <div class="col-12 text-right">
                        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-secondary" Text="Guardar" OnClick="btnActualizar_Click" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
