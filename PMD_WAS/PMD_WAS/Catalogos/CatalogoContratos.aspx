<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="CatalogoContratos.aspx.vb" Inherits="PMD_WAS.CatalogoContratos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updRequerimientosGrid" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-5">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-3"></div>
                    <div class="col-4 text-right">
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregarContrato" runat="server" CssClass="btn btn-primary" Text="Agregar contrato" OnClick="btnAgregarContrato_Click" />
                    </div>
                </div>
                <hr />

                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblIdRequerimiento" runat="server" Visible="false"></asp:Label>
                        <asp:GridView ID="grdRequerimientos" runat="server" Width="100%" CssClass="table table-bordered table-hover table-small" CellPadding="4" DataKeyNames="codigo_contrato" AllowPaging="true" OnPageIndexChanging="grdRequerimientos_PageIndexChanging"
                            GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />
                                <asp:BoundField HeaderText="Código contrato" DataField="codigo_contrato" />
                                <asp:BoundField HeaderText="Nombre" DataField="nombre_contrato" />
                                <asp:BoundField HeaderText="Clave de Gastos" DataField="clave_gastos" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server" ForeColor="Blue" Text="Editar" CssClass="btn btn-link " CommandName="ver" OnCommand="btn_Command" CommandArgument='<%# Eval("codigo_contrato") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" ForeColor="Red" Text="ELIMINAR" CssClass="btn btn-link  " CommandName="eliminar" OnCommand="btn_Command" CommandArgument='<%# Eval("codigo_contrato") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h5>No hay información disponible</h5>
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

    <div id="modalInformacionContrato" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Información contrato</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updNuevoContrato" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <h6 class="subtitle">Proveedor</h6>
                                        <asp:TextBox ID="txtProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <h6 class="subtitle">Clave de gastos</h6>
                                        <asp:DropDownList ID="ddlClaveGastos" runat="server" Width="100%" CssClass="select-basic-simple form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <h6 class="subtitle">Código contrato</h6>
                                        <asp:TextBox ID="txtCodigoContrato" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <h6 class="subtitle">Nombre</h6>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <h5 class="subtitle">Productos del contrato</h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <p>Nombre producto</p>
                                        <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Unidad</p>
                                        <asp:TextBox ID="txtUnidad" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Costo unitario</p>
                                        <asp:TextBox ID="txtCostoUnitario" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <p>IVA</p>
                                        <asp:TextBox ID="txtIVA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>IEPS</p>
                                        <asp:TextBox ID="txtIEPS" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Total</p>
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnAgregarProductp" runat="server" CssClass="btn btn-secondary" Text="Agregar" OnClick="btnAgregarProductp_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <br />
                                        <asp:GridView ID="grdMaterialesContrato" runat="server" Width="100%" CssClass="table table-bordered table-hover" CellPadding="4" DataKeyNames="id" AllowPaging="true"
                                            GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Nombre" DataField="requerimiento" />
                                                <asp:BoundField HeaderText="Unidad" DataField="unidad" />
                                                <asp:BoundField HeaderText="Costo unitario" DataField="costoUnitario" />
                                                <asp:BoundField HeaderText="IVA" DataField="iva" DataFormatString="{0:n2} %" />
                                                <asp:BoundField HeaderText="IEPS" DataField="ieps" DataFormatString="{0:c2}" />
                                                <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:c2}" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-link" Text="Quitar" OnCommand="btn_Command" CommandName="quitar" CommandArgument='<%# Eval("id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <h6>No hay productos agregados</h6>
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
                                        <asp:Button ID="btnGuardarContrato" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarContrato_Click" />
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
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        $(document).ready(function () {

            $('.select-basic-simple').select2();
        });

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {

            $('.select-basic-simple').select2();
        }

        function muestraModalInfoContrato() {
            $('#modalInformacionContrato').modal('show');
        }

        function ocultaModalInfoContrato() {
            $('#modalInformacionContrato').modal('hide');
        }

        function recalculaTotal(o, e) {
            var iva = $('#ddlIVA option:selected').val();
            var costo = $('#txtCostoUnitario').val();

            var total = 0;
            if (iva > 0)
                total = costo * (1 * (1 + (iva / 100)));
            else
                total = costo;

            $('#lblTotal').text('$ ' + Number(total).toFixed(2).replace(/./g, function (c, i, a) {
                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
            }));
        }
    </script>
</asp:Content>
