<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="Cotizaciones.aspx.vb" Inherits="PMD_WAS.Cotizaciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/ucAgregaComentarioCotizacion.ascx" TagName="ucComentarios" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucVistaCotizacion.ascx" TagName="ucVistaCotizacion" TagPrefix="uc2" %>

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
                        <asp:Button ID="btnSolicitarAutorizacion" runat="server" CssClass="btn btn-primary" Text="Solicitar autorización" OnClick="btnSolicitarAutorizacion_Click" />
                        <asp:Button ID="btnAgregarCotizacion" runat="server" CssClass="btn btn-primary" Text="Agregar cotizacion" OnClick="btnAgregarCotizacion_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <h6>
                            <asp:CheckBox ID="chkTodos" runat="server" Text="Seleccionar todas las cotizaciones" OnCheckedChanged="chkTodos_CheckedChanged" AutoPostBack="true" /></h6>
                        <div class="table table-responsive">
                            <asp:HiddenField ID="hdnIdCotizacionEliminar" runat="server" />
                            <asp:Button ID="btnEliminarCotizacion" runat="server" OnClick="btnEliminarCotizacion_Click" Style="visibility: hidden" Height="0" Width="0" />
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
                                            <asp:Label ID="lblEstatus" runat="server" Style="padding: .375rem .75rem;" Text='<%# Eval("estatus") %>' Visible='<%# Not Eval("estatus").ToString.ToLower().Contains("rechazada") And Eval("estatus") <> "Complementar"  %>'></asp:Label>
                                            <asp:Button ID="btnEstatus" runat="server" CssClass="btn btn-link" Text='<%# Eval("estatus") %>' Visible='<%# Eval("estatus").ToString.ToLower().Contains("rechazada") Or Eval("estatus") = "Complementar"  %>' OnCommand="btn_Command" CommandName="muestraComentario" CommandArgument='<%# Eval("id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDocumentosCotizacion" runat="server" CssClass="btn btn-link" Text="Documentos" OnCommand="btn_Command" CommandArgument='<%# Eval("id") %>' CommandName="documentos" Visible='<%# Eval("estatus").ToString.ToLower().Contains("autorizada") %>' />
                                            <asp:Button ID="btnAgregarProducto" runat="server" CssClass="btn btn-link" Text="Agregar producto" OnCommand="btn_Command" CommandArgument='<%# Eval("id") %>' CommandName="agregar" Visible='<%# Eval("estatus") = "Borrador" Or Eval("estatus") = "Complementar"  %>' />
                                            <asp:Button ID="btnDocumentoSeleccionado" runat="server" OnCommand="btn_Command" CommandName="documentoSeleccionado" Visible='<%# Eval("estatus") = "Documento seleccionado" %>' CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Documento seleccionado" />
                                            <asp:Button ID="btnHabilitarRequisicion" runat="server" OnCommand="btn_Command" CommandName="habilitarRequisicion" Visible='<%# Eval("estatus") = "Solicitud de Requisicion Habilitada" %>' CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Crear requisicion" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="btn btn-link" OnCommand="btn_Command" CommandArgument='<%# Eval("id") %>' CommandName="quitar" Visible='<%# Eval("estatus") = "Borrador" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0" HeaderStyle-Width="0">
                                        <ItemTemplate>
                                            <tr>
                                                <td></td>
                                                <td colspan="9">
                                                    <asp:GridView ID="grdProductos" runat="server" CssClass="table table-striped table-small table-detail" Width="100%" Visible="false" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Producto" DataField="producto" />
                                                            <asp:BoundField HeaderText="Descripcion" DataField="comentarios" />
                                                            <asp:BoundField HeaderText="Justificacion" DataField="justificacion" />
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnVer" runat="server" OnCommand="btn_Command" CommandName="ver" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Ver" Visible='<%# Eval("estatus") <> "Borrador" And Eval("estatus") <> "Complementar"  %>' />
                                                                    <asp:Button ID="btnEditar" runat="server" OnCommand="btn_Command" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-link" Text="Editar" Visible='<%# Eval("estatus") = "Borrador" Or Eval("estatus") = "Complementar"  %>' />
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

    <!--    MODAL DE DOCUMENTOS -->
    <div id="modalVisualizacionDocumentos" class="modal modal-full fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información de la Cotización</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updDocumentosCotizacion" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Repeater ID="rptDocumentos" runat="server">
                                            <ItemTemplate>
                                                <div class="col-12 col-sm-6 col-md-4">
                                                    <asp:CheckBox ID="chkDocumentos" runat="server" Text="Seleccionar como cotizacion valida." CssClass="chkSeleccionarDocumento" onchange="javascript:validaCheckSeleccionado(this,event);" />
                                                    <asp:HiddenField ID="hdnIdCotizacion" runat="server" Value='<%# Eval("IdCotizacion") %>' />
                                                    <asp:HiddenField ID="hdnIdDocumento" runat="server" Value='<%# Eval("id") %>' />
                                                    <iframe src='<%# String.Format("https://admin.sanicolas.gob.mx/PlaneacionFinanciera/{0}", Eval("rutaArchivo")) %>' width="100%" style="height: 75vh !important"></iframe>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarSeleccionDocumento" runat="server" CssClass="btn btn-primary" Text="Guardar seleccion" OnClick="btnGuardarSeleccionDocumento_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalNuevaCotizacion" class="modal modal-big fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información de la Cotización</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updNuevaCotizacion" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnIdCotizacion" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnIdCotizacionDetalle" runat="server" Value="0" />
                            <div class="container-fluid">
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
                                        <h6 class="subtitle">Tipo de cotización:</h6>
                                        <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="form-control select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoProducto_SelectedIndexChanged">
                                            <asp:ListItem Text="Producto" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Servicio" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 id="tituloNombre" runat="server" class="subtitle">Nombre del producto:</h6>
                                        <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                                <asp:Panel ID="pnlInfoProducto" runat="server">
                                    <div class="row">
                                        <div class="col-12 col-md-6">
                                            <h6 class="subtitle">Cantidad:</h6>
                                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="col-12 col-md-6">
                                            <h6 class="subtitle">Unidad:</h6>
                                            <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlInfoServicio" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-12 col-md-4">
                                            <h6 class="subtitle">Fecha inicio:</h6>
                                            <asp:TextBox ID="txtFechaInicio" runat="server" Width="100%" CssClass="form-control" onkeypress="javascript:return nocaptura(event)" TabIndex="8"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server" TargetControlID="txtFechaInicio"
                                                TodaysDateFormat="dd-MM-yyyy" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-12 col-md-4">
                                            <h6 class="subtitle">Fecha terminación:</h6>
                                            <asp:TextBox ID="txtFechaTerminacion" runat="server" Width="100%" CssClass="form-control" onkeypress="javascript:return nocaptura(event)" TabIndex="8"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFechaTerminacion_CalendarExtender" runat="server" TargetControlID="txtFechaTerminacion"
                                                TodaysDateFormat="dd-MM-yyyy" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                        </div>
                                        <div class="col-12 col-md-4">
                                            <h6 class="subtitle">Vigencia:</h6>
                                            <asp:TextBox ID="txtVigencia" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Lugar de entrega</h6>
                                        <asp:DropDownList ID="ddlAlmacen" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Justificacion:</h6>
                                        <asp:TextBox ID="txtJustificacion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Comentarios:</h6>
                                        <asp:TextBox ID="txtDescripcionProducto" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <h5 class="subtitle">Especificaciones: <small>(Puedes agregar especificaciones para facilitar el trabajo del Departamento de Adquisiciones)</small></h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-3">
                                        <p class="simple">Tipo especificación:</p>
                                        <asp:DropDownList ID="ddlTipoEspecificacion" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-md-7">
                                        <p class="simple">Especificación</p>
                                        <asp:TextBox ID="txtEspecificacion" runat="server" CssClass="form-control" TextMode="MultiLine" Width="100%" Rows="2"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-2 text-right">
                                        <p class="simple" style="visibility: hidden;">Especificacion</p>
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregar_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="grdEspecificaciones" runat="server" CssClass="table table-bordered table-hover table-small" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Tipo" DataField="Nombre" />
                                                    <asp:BoundField HeaderText="Especificacion" DataField="Especificacion" />
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminar" runat="server" Text="Quitar" CssClass="btn btn-link" OnCommand="lnkEliminar_Command" CommandArgument='<%# Eval("Id") %>' CommandName="quitar"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                        <h5 class="subtitle">Archivos <small>(Puedes subir cualquier tipo de archivo que ayude a encontrar el producto con facilidad.)</small></h5>
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
                                        <asp:GridView ID="grdArchivosCotizacion" runat="server" CssClass="table table-bordered table-hover table-small" AutoGenerateColumns="false">
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
                                        <a data-dismiss="modal" class="btn btn-primary">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarCotizacion" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarCotizacion_Click" />
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
                    <uc2:ucVistaCotizacion ID="ucCotizacion" runat="server" />
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

        function muestraModalCotizacion() {
            $('#modalNuevaCotizacion').modal('show');
        }

        function ocultaModalCotizacion() {
            $('#modalNuevaCotizacion').modal('hide');
        }

        function uploadComplete(sender) {
            clearContents();
            document.getElementById('<%= btnRecargaArchivos.ClientID %>').click();
        }

        function uploadError(sender) {
            clearContents();
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

        function muestraAlertaEliminarCotizacion() {
            swal({
                title: 'Confirmar acción',
                text: 'La cotización será eliminada, ¿desesa continuar?',
                type: 'info',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }, function (isConfirm) {
                if (isConfirm) {
                    document.getElementById('<%= btnEliminarCotizacion.ClientID %>').click();
                }
            });
        }
        function openModalIFrame(object, sender) {
            var url = window.location.protocol + '//' + window.location.host + '/PlaneacionFinanciera/' + $(object).parent().find('input[type="hidden"]').val();
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function muestraModalInfoCotizacion() {
            $('#modalInfoCotizacion').modal('show');
        }

        function muestraModalDocumentos() {
            $('#modalVisualizacionDocumentos').modal('show');
        }

        function ocultaModalDocumentos() {
                $('#modalVisualizacionDocumentos').modal('hide');
        }

        function validaCheckSeleccionado(o, e) {
            $('.chkSeleccionarDocumento input').prop('checked', false);
            $(o).find('input').prop('checked', true);
        }

         function openPDFIFrame(url) {
            document.getElementById('iframeModalURL').src = ('/PlaneacionFinanciera/' + url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }
    </script>

</asp:Content>
