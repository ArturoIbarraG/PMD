<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="OrdenesSurtido.aspx.vb" Inherits="PMD_WAS.OrdenesSurtido" %>

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
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="grdSolicitudes" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Núm. orden abastecimiento" DataField="folio" />
                            <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />

                            <asp:TemplateField HeaderText="Cantidad materiales" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMateriales" runat="server" CssClass="btn btn-link" Text='<%# String.Format("{0:n0}", Eval("cantidadMateriales")) %>' OnCommand="lnkImprimeFormato_Command" CommandName="materiales" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnIdOS" runat="server" Value='<%# Eval("id") %>' />
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("totalCostoMateriales")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Empleado" DataField="usuarioEnviado" />
                            <asp:TemplateField HeaderText="Fecha solicitud">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# String.Format("{0:dd/MMMM/yyyy HH:mm tt}", Eval("fechaEnviado")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Estatus">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEstatus" runat="server" CssClass="btn btn-link" Text='<%# Eval("estatus") %>' CommandArgument='<%# Eval("id") %>' CommandName="estatus" OnCommand="lnkImprimeFormato_Command"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkImprimeFormato" runat="server" Text="Imprimir documento" CssClass="btn btn-link" CommandArgument='<%# Eval("id") %>' CommandName="imprimir" OnCommand="lnkImprimeFormato_Command"></asp:LinkButton>
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
        </ContentTemplate>
    </asp:UpdatePanel>

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
                                                <asp:BoundField HeaderText="Material" DataField="requirimiento" />
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

    <div id="modalInfoEstatus" class="modal modal-medium fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Listado de materiales de la Orden de Surtido</h5>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updInfoEstatus" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblInfoEstatus" runat="server"></asp:Label>
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

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {

        }

        function endReq(sender, args) {
            aplicaEfectos();
        }

        $(document).ready(function () {
            aplicaEfectos();
        });

        function muestraModalMateriales() {
            $('#modalListaMateriales').modal('show');
        }


        function aplicaEfectos() {
            $('input[id*="txtCantidad"]').on('keydown', function (e) {
                var btn = $(this).parent().parent().find('.btn-agrega');

                console.log(btn.attr('name'));
                var keyCode = e.keyCode || e.which;

                if ((keyCode === 9) || keyCode === 13) {
                    e.preventDefault();
                    setTimeout(function () {
                        document.getElementById(btn.attr('id')).click();
                    }, 200);
                }
            });

            $('select[id*="ddlRequisicion"]').first().focus();
        }

        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }

        function openModalIFrame(url) {
            console.log(url);
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }

        function muestraMensajeError(error) {
            console.log(error);
            alert(error);
        }

        function muestraModalInfoEstatus() {
            $('#modalInfoEstatus').modal('show');
        }
    </script>
</asp:Content>

