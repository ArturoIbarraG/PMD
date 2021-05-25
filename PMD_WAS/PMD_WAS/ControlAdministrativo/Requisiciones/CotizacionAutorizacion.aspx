<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="CotizacionAutorizacion.aspx.vb" Inherits="PMD_WAS.CotizacionAutorizacion" %>

<%@ Register Src="~/UserControl/ucVistaCotizacion.ascx" TagName="ucCotizacion" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucAgregaComentarioCotizacion.ascx" TagName="ucComentarios" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                        <asp:Button ID="btnAutorizarCotizacion" runat="server" CssClass="btn btn-primary" Text="Autorizar cotizacion" OnClick="btnAutorizarCotizacion_Click" />
                        <a class="btn btn-primary" onclick="javascript:abreModalRechazar();">Rechazar</a>
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
                                        <asp:TemplateField HeaderText="Estatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstatus" runat="server" Style="padding: .375rem .75rem;" Text='<%# Eval("estatus") %>' Visible='<%# Eval("estatus") <> "Rechazada" And Eval("estatus") <> "Complementar"  %>'></asp:Label>
                                            <asp:Button ID="btnEstatus" runat="server" CssClass="btn btn-link" Text='<%# Eval("estatus") %>' Visible='<%# Eval("estatus") = "Rechazada" Or Eval("estatus") = "Complementar"  %>' OnCommand="btn_Command" CommandName="muestraComentario" CommandArgument='<%# Eval("id") %>' />
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
                        </div>
                    </div>
                </div>
            </div>

            <div id="modalEstatusCotizacion" class="modal modal-small fade" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Información de la Cotización</h5>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblInfoEstatusCotizacion" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <a class="btn btn-primary right" data-dismiss="modal">Aceptar</a>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
   
    <div id="modalRechazarSolicitud" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Autorizar requisicion</h2>
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

    <uc1:ucComentarios ID="ucComentariosCotizacion" runat="server" />
    <div id="modalInfoCotizacion" class="modal modal-big fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información de la Cotización</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <uc2:ucCotizacion ID="ucCotizacion" runat="server" />
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
    <script type="text/javascript">
        function muestraModalEstatusCotizacion() {
            $('#modalEstatusCotizacion').modal('show');
        }

        function openModalIFrame(url) {
            console.log(url);
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function abreModalRechazar() {
            if ($('.checked-solicitud input:checked').length > 0)
                $('#modalRechazarSolicitud').modal('show');
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una solicitud para poder ser rechazada.',
                    title: 'Favor de validar'
                });
            }
        }

        function ocultaModalRechazo() {
            $('#modalRechazarSolicitud').modal('hide');
        }
        function openPDFIFrame(url) {
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function muestraModalInfoCotizacion() {
            $('#modalInfoCotizacion').modal('show');
        }
    </script>
</asp:Content>
