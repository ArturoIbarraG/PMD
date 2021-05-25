<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="CotizacionRequisicion.aspx.vb" Inherits="PMD_WAS.CotizacionRequisicion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/ucAgregaComentarioCotizacion.ascx" TagName="ucComentarios" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucVistaCotizacion.ascx" TagName="ucVistaCotizacion" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .modal h6 {
            display: inline-block;
        }
    </style>
    <asp:UpdatePanel ID="updRequisiciones" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnClaveSubmisionID" runat="server" />
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h5>
                            <asp:Label ID="lblInformacionRequisicion" runat="server" Style="font-size: 1.2rem;" Text="Solicitudes correspondientes al mes de <b>Enero</b> del <b>2021</b> de la <b>Administración 2018-2021</b>"></asp:Label>
                        </h5>
                        <asp:HiddenField ID="hdnAdmon" runat="server" />
                        <asp:HiddenField ID="hdnAnio" runat="server" />
                        <asp:HiddenField ID="hdnMes" runat="server" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaría/Instituto:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Direcciones:</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Actividad:</h6>
                        <asp:DropDownList ID="ddlActividad" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged" TabIndex="3">
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Sub Actividad:</h6>
                        <asp:DropDownList ID="ddlSubActividad" runat="server" TabIndex="3" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlSubActividad_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div id="pnlAgregarCotizacion" class="container-fluid" runat="server" visible="false">
                <div class="row">
                    <div class="col-12 text-right">
                        <a class="btn btn-primary" onclick="javascript:abreMensajeAutorizarCotizacion();">Proponer</a>
                        <a class="btn btn-primary" onclick="javascript:abreModalComplementar();">Complementar</a>
                        <a class="btn btn-primary" onclick="javascript:abreModalRechazar();">Rechazar</a>
                        <a class="btn btn-primary" onclick="javascript:abreModalHabilitarSolicitudRequisicion();">Habilitar solicitud requisicion</a>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <h6>
                            <asp:CheckBox ID="chkTodos" runat="server" Text="Seleccionar todas las cotizaciones" OnCheckedChanged="chkTodos_CheckedChanged" AutoPostBack="true" /></h6>
                        <div class="table table-responsive">
                            <asp:HiddenField ID="hdnIdCotizacionEliminar" runat="server" />
                            <asp:GridView ID="grdCotizacion" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                            <asp:CheckBox ID="chkSeleccionar" runat="server" CssClass="checked-solicitud" Checked='<%# Eval("seleccionado") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnProductos" runat="server" CssClass="btn btn-link" Style="font-size: 20px; font-weight: 600; padding: 0; margin: 0;" ToolTip="Ver productos" Text="+" OnCommand="btn_Command" CommandName="productos" CommandArgument='<%# Eval("id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Folio" DataField="folio" />
                                    <asp:BoundField HeaderText="Tipo" DataField="tipo" />
                                    <asp:BoundField HeaderText="Productos" DataField="productos" />
                                    <asp:BoundField HeaderText="Fecha solicitud" DataField="fechaEnvio" DataFormatString="{0:dd/MMMM/yyyy}" />
                                    <asp:BoundField HeaderText="Usuario solicitud" DataField="usuarioEnvio" />
                                    <asp:BoundField HeaderText="Estatus" DataField="estatus" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnAgregarDocumentoCotizaicon" runat="server" OnCommand="btn_Command" CommandName="documentos" CommandArgument='<%# Eval("id") %>' Visible='<%# Not Eval("estatus") = "Documento seleccionado" %>' CssClass="btn btn-link" Text="Agregar documentos" />
                                            <asp:Button ID="btnDocumentoSeleccionado" runat="server" OnCommand="btn_Command" CommandName="documentoSeleccionado" Visible='<%# Eval("estatus") = "Documento seleccionado" %>' CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Ver documento elegido" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0" HeaderStyle-Width="0">
                                        <ItemTemplate>
                                            <tr>
                                                <td></td>
                                                <td colspan="8">
                                                    <asp:GridView ID="grdProductos" runat="server" CssClass="table table-striped table-small table-detail" Width="100%" Visible="false" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Producto" DataField="producto" />
                                                            <asp:BoundField HeaderText="Descripcion" DataField="comentarios" />
                                                            <asp:BoundField HeaderText="Justificacion" DataField="justificacion" />
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnVer" runat="server" OnCommand="btn_Command" CommandName="ver" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Ver" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnAgregarComentario" runat="server" OnCommand="btn_Command" CommandName="comentarios" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Comentarios" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <h6>No hay cotizaciones agregadas</h6>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <h6>No hay cotizaciones agregadas</h6>
                                            </div>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Button ID="btnAutorizarCotizaciones" runat="server" style="visibility:hidden;" Width="0" Height="0" OnClick="btnAutorizarCotizacion_Click" />
                            <asp:Button ID="btnHabilitarSolicitudRequisiciones" runat="server" style="visibility:hidden;" Width="0" Height="0" OnClick="btnHabilitarSolicitudRequisiciones_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalNuevaCotizacion" class="modal modal-big fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información de la Cotización</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <uc2:ucVistaCotizacion ID="ucCotizacion" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div id="modalRechazarSolicitud" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Rechazar requisicion</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updRechazarSolicitud" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6>Favor de seleccionar el motivo del rechazo</h6>
                                        <asp:DropDownList ID="ddlMotivoRechazo" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6>Comentarios</h6>
                                        <asp:TextBox ID="txtComentariosRechazo" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4"></asp:TextBox>
                                    </div>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnRechazarCotizacion" runat="server" CssClass="btn btn-primary" Text="Rechazar" OnClick="btnRechazarCotizacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalComplementarSolicitud" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Complementar cotizacion</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6>Favor proporcione informacion para que el encargado sepa la informacion a complementar</h6>
                                        <asp:TextBox ID="txtComentariosComplementear" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnComplementar" runat="server" CssClass="btn btn-primary" Text="Complementar" OnClick="btnComplementar_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalAceptaCotizacion" class="modal modal-big fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Proponer Documentos de la Cotización</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updArchivosCotizacion" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnIdCotizacion" runat="server" />
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Sube los documentos de cotizacion para el producto.</h6>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <ajaxToolkit:AsyncFileUpload ID="afUploadCotizacion" runat="server" ThrobberID="Throbber" OnClientUploadComplete="uploadComplete" OnUploadedComplete="afUploadCotizacion_UploadedComplete" UploaderStyle="Traditional" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Button ID="btnRecargaArchivos" runat="server" OnClick="btnRecargaArchivos_Click" Style="visibility: hidden;" Height="0" Width="0" />
                                        <asp:GridView ID="grdDocumentosCotizacion" runat="server" CssClass="table table-bordered table-hover table-small" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Archivo" DataField="Nombre" />
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnRutaArchivo" runat="server" Value='<%# Eval("URL") %>' />
                                                        <a runat="server" class="btn btn-link" visible='<%# If(Eval("URL").ToString() = "", False, True) %>' onclick="javascript:openModalIFrame(this,event);">Ver</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnqQuitarArchivo" runat="server" CssClass="btn btn-link" CommandName="quitar" CommandArgument='<%# Eval("Id") %>' Text="Quitar" OnCommand="lnqQuitarArchivo_Command"></asp:LinkButton>
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
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarDocumentosCotizacion" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarDocumentosCotizacion_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalIframeURL" class="modal modal-big fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <iframe id="iframeModalURL" width="100%" height="600px"></iframe>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-primary right" data-dismiss="modal">Cerrar</a>
                </div>
            </div>
        </div>
    </div>

    <uc1:ucComentarios ID="ucComentariosCotizacion" runat="server" />

    <script type="text/javascript">
        function muestraModalCotizacion() {
            $('#modalNuevaCotizacion').modal('show');
        }

        function ocultaModalCotizacion() {
            $('#modalNuevaCotizacion').modal('hide');
        }

        function uploadError(sender) {
            clearContents();
        }

        function openModalIFrame(object, sender) {
            var url = window.location.protocol + '//' + window.location.host + '/PlaneacionFinanciera/' + $(object).parent().find('input[type="hidden"]').val();
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function openModalIFrame(object, sender) {
            var url = window.location.protocol + '//' + window.location.host + '/PlaneacionFinanciera/' + $(object).parent().find('input[type="hidden"]').val();
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function openPDFIFrame(url) {
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function abreModalRechazar() {
            if ($('.checked-solicitud input:checked').length > 0)
                $('#modalRechazarSolicitud').modal('show');
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una cotizacion.',
                    title: 'Favor de validar'
                });
            }
        }

        function abreModalComplementar() {
            if ($('.checked-solicitud input:checked').length > 0)
                $('#modalComplementarSolicitud').modal('show');
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una cotizacion.',
                    title: 'Favor de validar'
                });
            }
        }

        function ocultaModalComplementar() {
            $('#modalComplementarSolicitud').modal('hide');
        }

        function abreModalAceptaCotizacion() {
            $('#modalAceptaCotizacion').modal('show');
        }

        function cierraModalAceptaCotizacion() {
            $('#modalAceptaCotizacion').modal('hide');
        }

        function uploadComplete(sender) {
            clearContents();
            document.getElementById('<%= btnRecargaArchivos.ClientID %>').click();
        }

        function clearContents() {
            var span = $get("<%= afUploadCotizacion.ClientID%>");
            var txts = span.getElementsByTagName("input");
            for (var i = 0; i < txts.length; i++) {
                if (txts[i].type == "text") {
                    txts[i].value = "";
                }
            }
        }

        function abreModalHabilitarSolicitudRequisicion() {
            if ($('.checked-solicitud input:checked').length > 0) {
                document.getElementById('<%= btnHabilitarSolicitudRequisiciones.ClientID %>').click();
            }
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una cotizacion.',
                    title: 'Favor de validar'
                });
            }
        }

        function ocultaModalRechazo() {
            $('#modalRechazarSolicitud').modal('hide');
        }

        function abreMensajeAutorizarCotizacion() {
            if ($('.checked-solicitud input:checked').length > 0) {
                document.getElementById('<%= btnAutorizarCotizaciones.ClientID %>').click();
            }
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una cotizacion.',
                    title: 'Favor de validar'
                });
            }
        }
    </script>
</asp:Content>
