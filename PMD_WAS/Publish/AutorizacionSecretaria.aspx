<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="AutorizacionSecretaria.aspx.vb" Inherits="PMD_WAS.AutorizacionSecretaria" %>

<%@ Register Src="~/UserControl/ucPresupuestoDireccionGrafico.ascx" TagName="ucPresupuesto" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updAutorizaciones" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdAutorizaciones" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Secretaria" DataField="Nombr_secretaria" />
                                <asp:BoundField HeaderText="Direccion" DataField="Nombr_direccion" />
                                <asp:BoundField HeaderText="Presupuesto asignado" DataField="Presupuesto" DataFormatString="{0:c2}" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField HeaderText="Capturado" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetalleCapturado" runat="server" Text='<%# String.Format("{0:c2}", Eval("Capturado")) %>' OnCommand="lnkPresupuesto_Command" CommandName="detalle" CommandArgument='<%# String.Format("{0}|{1}", Eval("IdDireccion"), Eval("Nombr_direccion")) %>' CssClass="btn btn-link"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estatus" ItemStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAutorizarPresupuesto" runat="server" Text="Autorizar" Visible='<%# Not Eval("Autorizado") %>' OnCommand="lnkPresupuesto_Command" CssClass="btn btn-link" CommandName="autorizar" CommandArgument='<%# String.Format("{0}|{1}|{2}|{3}", Eval("IdSecretaria"), Eval("IdDireccion"), Eval("Nombr_direccion"), Eval("Capturado")) %>'></asp:LinkButton>
                                        <asp:Label ID="lblInfoAutorizacion" runat="server" Text='<%# Eval("InfoAutorizacion") %>' Visible='<%# Eval("Autorizado") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div id="modalDetallePresupuesto" class="modal fade modal-big" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h2 class="modal-title">Detalle presupuesto</h2>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <uc1:ucPresupuesto ID="ucDetallePresupuesto" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-secondary right" data-dismiss="modal">Aceptar</a>
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField ID="hdnIdSecretariaSeleccionada" runat="server" />
            <asp:HiddenField ID="hdnIdDireccionSeleccionada" runat="server" />
            <asp:HiddenField ID="hdnTotalPresupuesto" runat="server" />
            <asp:HiddenField ID="hdnNombreDireccionSeleccionada" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalAutorizaPresupuesto" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Autorizació de presupuesto</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updAutorizacion" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-8">
                                        <h6 class="subtitle">
                                            <asp:Label ID="lblInfoPresupuesto" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                    <div class="col-4">
                                        <h6 class="subtitle">
                                            <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="Comentarios (opcional)" Rows="5"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnGuardarAutorizacion" runat="server" CssClass="btn btn-primary" Text="Guardar autorización" OnClick="btnGuardarAutorizacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function muestraModalDetallePresupuesto() {
            $('#modalDetallePresupuesto').modal('show');
        }

        function muestraModalAutorizacion() {
            $('#modalAutorizaPresupuesto').modal('show');
        }

        function ocultaModalAutorizacion() {
            $('#modalAutorizaPresupuesto').modal('hide');
        }
    </script>
</asp:Content>


