<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.AsignaPresupuestoDireccion" Codebehind="AsignaPresupuestoDireccion.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updAsignaPresupuesto" runat="server">
        <ContentTemplate>
              <asp:HiddenField ID="hdnPresupuestoSecretaria" runat="server" />
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
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secreataria:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-8">
                        <h5>
                            <asp:Label ID="lblPresupuestoSecretaria" runat="server"></asp:Label>
                        </h5>
                    </div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnGuardarPresupuesto" runat="server" CssClass="btn btn-primary" Text="Guardar presupuesto" OnClick="btnGuardarPresupuesto_Click" />
                    </div>
                </div>
                <hr />
                <asp:Repeater ID="rptPresupuestoDireccion" runat="server">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-8">
                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Direccion") %>'></asp:Label>
                            </div>
                            <div class="col-4">
                                <asp:HiddenField ID="hdnPresupuesto" runat="server" Value='<%# Eval("IdDireccion") %>' />
                                <asp:TextBox ID="txtPresupuesto" runat="server" CssClass="form-control money text-right" MaxLength="12" Text='<%# Eval("Presupuesto") %>'></asp:TextBox>
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