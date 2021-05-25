<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.AsignaPresupuestoSecretaria" Codebehind="AsignaPresupuestoSecretaria.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updAsignaPresupuesto" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnPresupuestoAnual" runat="server" />
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-8">
                        <h5>
                            <asp:Label ID="lblPresupuestoAnual" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnGuardarPresupuesto" runat="server" CssClass="btn btn-primary" Text="Guardar presupuesto" OnClick="btnGuardarPresupuesto_Click" />
                    </div>
                </div>
                <hr />
                <asp:Repeater ID="rptPresupuestoSecretaria" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-8">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Secretaria") %>'></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:HiddenField ID="hdnPresupuesto" runat="server" Value='<%# Eval("IdSecretaria") %>' />
                                <asp:TextBox ID="txtPresupuesto" runat="server" CssClass="form-control money text-right" MaxLength="10" Text='<%# Eval("Presupuesto") %>' onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {
            //
            $(".money").mask("#,##0", {reverse: true});
        }
    </script>
</asp:Content>
