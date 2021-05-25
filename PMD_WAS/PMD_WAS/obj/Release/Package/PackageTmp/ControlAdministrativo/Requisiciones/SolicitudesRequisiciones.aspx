﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="SolicitudesRequisiciones.aspx.vb" Inherits="PMD_WAS.SolicitudesRequisiciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updRequisiciones" runat="server">
        <ContentTemplate>
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
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-right">
                        <a class="btn btn-primary" onclick="javascript:abreModalRechazar();">Rechazar</a>
                        <a class="btn btn-primary" onclick="javascript:abreModalAutorizacion();">Autorizar</a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6>
                            <asp:CheckBox ID="chkTodos" runat="server" Text="Seleccionar todas las solicitudes" OnCheckedChanged="chkTodos_CheckedChanged" AutoPostBack="true" /></h6>
                        <asp:GridView ID="grdSolicitudes" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" CssClass="checked-solicitud" Checked='<%# Eval("seleccionado") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Núm. solicitud" DataField="folio" />
                                <asp:BoundField HeaderText="Almacen" DataField="almacen" />

                                <asp:TemplateField HeaderText="Total materiales">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTotalMateriales" runat="server" CssClass="btn btn-link" Text='<%# String.Format("{0:n2}", Eval("material")) %>' OnCommand="lnkTotalMateriales_Command" CommandName="materiales" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("total")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Estatus" DataField="estatus" />
                                <asp:BoundField HeaderText="Empleado" DataField="usuarioSolicitud" />
                                <asp:TemplateField HeaderText="Fecha solicitud">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecha" runat="server" Text='<%# String.Format("{0:dd/MMMM/yyyy HH:mm tt}", Eval("fechaSolicitud")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h3>No hay información disponible</h3>
                                        </div>
                                    </div>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalAutorizarSolicitud" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Autorizar requisicion</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEnviar" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6>Favor de proporcionar el Proveedor al cual se asignará la requisición</h6>
                                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnEnviaOrdenesSurtido" runat="server" CssClass="btn btn-primary" Text="Autorizar" OnClick="btnEnviaOrdenesSurtido_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

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
                                        <asp:Button ID="btnRechazarSolicitud" runat="server" CssClass="btn btn-primary" Text="Rechazar" OnClick="btnRechazarSolicitud_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalListaMateriales" class="modal modal-big fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Listado de materiales de la Orden de Surtido</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updmateriales" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="grdListaMateriales" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Material" DataField="material" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("cantidad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Unidad" DataField="unidad" />
                                                <asp:TemplateField HeaderText="Precio unitario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# String.Format("{0:c2}", Eval("precioUnitario")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IVA">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIVA" runat="server" Text='<%# String.Format("{0} %", Eval("iva")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("total")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Label ID="lblTotalMateriales" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-primary right" data-dismiss="modal">Cerrar</a>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function muestraModalMateriales() {
            $('#modalListaMateriales').modal('show');
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

        function abreModalAutorizacion() {
            if ($('.checked-solicitud input:checked').length > 0)
                $('#modalAutorizarSolicitud').modal('show');
            else {
                swal({
                    type: 'info',
                    text: 'Favor de seleccionar al menos una solicitud para poder ser autorizada.',
                    title: 'Favor de validar'
                });
            }
        }

        function ocultaModalAutorizacion() {
            $('#modalAutorizarSolicitud').modal('hide');
        }

         function ocultaModalRechazo() {
            $('#modalRechazarSolicitud').modal('hide');
        }
    </script>
</asp:Content>