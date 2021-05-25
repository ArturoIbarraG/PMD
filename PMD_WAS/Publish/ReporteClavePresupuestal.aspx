<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.ReporteClavePresupuestal" Codebehind="ReporteClavePresupuestal.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updTareas" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn  btn-secondary" Text="Exportar Excel" OnClick="btnExportarExcel_Click" /><br />
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="table table-responsive">
                              <asp:GridView ID="grdClavePresupuestal" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="true" Width="100%">
                        </asp:GridView>
                        </div>
                      
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportarExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
