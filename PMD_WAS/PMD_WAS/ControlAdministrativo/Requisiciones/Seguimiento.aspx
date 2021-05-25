<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="Seguimiento.aspx.vb" Inherits="PMD_WAS.Seguimiento1" %>

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
                    <div class="col-12">
                        <asp:GridView ID="grdSolicitudes" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>

                                <asp:BoundField HeaderText="Núm. solicitud" DataField="folio" />
                                <asp:BoundField HeaderText="Almacen" DataField="almacen" />

                                <asp:TemplateField HeaderText="Total materiales">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkTotalMateriales" runat="server" CssClass="btn btn-link" Text='<%# String.Format("{0:n2}", Eval("material")) %>' OnCommand="lnkTotalMateriales_Command" CommandName="materiales" CommandArgument='<%# Eval("id") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("total")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Empleado" DataField="usuarioSolicitud" />
                                <asp:TemplateField HeaderText="Fecha solicitud">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecha" runat="server" Text='<%# String.Format("{0:dd/MMMM/yyyy HH:mm tt}", Eval("fechaSolicitud")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estatus">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEstatus" runat="server" Text='<%# Eval("estatus") %>' CssClass="btn btn-link" CommandName="estatus" CommandArgument='<%# Eval("id") %>' OnCommand="lnkTotalMateriales_Command"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkImprimirFolio" runat="server" CssClass="btn btn-link" Text="Imprimir documento" OnCommand="lnkTotalMateriales_Command" CommandName="imprimir" CommandArgument='<%# Eval("id") %>' />
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
        function muestraModalMateriales() {
            $('#modalListaMateriales').modal('show');
        }

        function muestraModalInfoEstatus() {
            $('#modalInfoEstatus').modal('show');
        }

          function openModalIFrame(url) {
            console.log(url);
            document.getElementById('iframeModalURL').src = (url + '?i=' + new Date().getTime());
            $('#modalIframeURL').modal('show');
        }
    </script>
</asp:Content>
