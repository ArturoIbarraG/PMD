<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucVistaCotizacion.ascx.vb" Inherits="PMD_WAS.ucVistaCotizacion" %>
<style type="text/css">
    #contentInfoCotizacion h6 {
        display: inline-block;
    }
</style>
<asp:UpdatePanel ID="updNuevaCotizacion" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdnIdCotizacionDetalle" runat="server" Value="0" />
        <div class="container-fluid" id="contentInfoCotizacion">
            <div class="row">
                <div class="col-12">
                    <h4 class="subtitle">
                        <asp:Label ID="lblInformacionCotizacion" runat="server"></asp:Label>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <h5 class="subtitle">Información del producto</h5>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-md-6">
                    <h6 class="subtitle">Tipo:</h6>
                    <asp:Label ID="lblTipoCotizacion" runat="server"></asp:Label>
                </div>
                <div class="col-12 col-md-6">
                    <h6 id="tituloNombre" runat="server" class="subtitle">Nombre del producto:</h6>
                    <asp:Label ID="lblNombreProducto" runat="server"></asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlInfoProducto" runat="server">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Cantidad:</h6>
                        <asp:Label ID="lblCantidad" runat="server"></asp:Label>
                    </div>

                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Unidad:</h6>
                        <asp:Label ID="lblUnidad" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlInfoServicio" runat="server" Visible="false">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Fecha inicio:</h6>
                        <asp:Label ID="lblFechaInicio" runat="server"></asp:Label>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Fecha terminación:</h6>
                        <asp:Label ID="lblFechaTerminacion" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="subtitle">Vigencia:</h6>
                        <asp:Label ID="lblVigencia" runat="server"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-12 col-md-6">
                    <h6 class="subtitle">Lugar de entrega</h6>
                    <asp:Label ID="lblLugarEntrega" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <h6 class="subtitle">Justificacion:</h6>
                    <asp:Label ID="lblJustificacion" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <h6 class="subtitle">Comentarios:</h6>
                    <asp:Label ID="lblComentarios" runat="server"></asp:Label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-12">
                    <h5 class="subtitle">Especificaciones:</h5>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <div class="table table-responsive">
                        <asp:GridView ID="grdEspecificaciones" runat="server" CssClass="table table-bordered table-hover table-small" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Tipo" DataField="Nombre" />
                                <asp:BoundField HeaderText="Especificacion" DataField="Especificacion" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h6>No hay especificaciones agregadas</h6>
                                        </div>
                                    </div>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-12">
                    <h5 class="subtitle">Archivos </h5>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="grdArchivosCotizacion" runat="server" CssClass="table table-bordered table-hover table-small" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Archivo" DataField="Nombre" />
                            <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnRutaArchivo" runat="server" Value='<%# Eval("URL") %>' />
                                    <a runat="server" class="btn btn-link" visible='<%# If(Eval("URL").ToString() = "", False, True) %>' onclick="javascript:openModalIFrameVista(this,event);">Ver</a>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6>No hay archivos agregados</h6>
                                    </div>
                                </div>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-12 text-right">
                    <a data-dismiss="modal" class="btn btn-primary">Cerrar</a>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function openModalIFrameVista(object, sender) {
        var url = window.location.protocol + '//' + window.location.host + '/PlaneacionFinanciera/' + $(object).parent().find('input[type="hidden"]').val();
        alert(url);
        document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
        $('#modalIframeURL').modal('show');
    }
</script>
