<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.Eventos2015_CatalogoOrdenServicio" Codebehind="CatalogoOrdenServicio.aspx.vb" %>


<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_requerimiento.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <style type="text/css">
        .container-border {
            border: 1px solid #AAA;
            padding: 10px;
        }

        .labelIndicador {
            min-height: 50px;
            height: auto;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }

        h5 {
            font-size: 1.4em;
            margin-bottom: 6px;
        }

        #MainContent_txtFechaInsta_CalendarExtender_container {
            z-index: 9999;
        }
    </style>
    <asp:UpdatePanel ID="updRequisicionesGrid" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-5">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-3"></div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnAgregarOrdenServicio" runat="server" CssClass="btn btn-secondary" Text="Agregar orden de servicio" OnClick="btnAgregarOrdenServicio_Click" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblIdOrdenServicio" runat="server" Visible="false"></asp:Label>
                        <asp:GridView ID="grdOrdenServicio" runat="server" Width="100%" CssClass="table table-bordered table-hover" CellPadding="4" DataKeyNames="id_os" AllowPaging="true" OnPageIndexChanging="grdOrdenServicio_PageIndexChanging"
                            GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                            
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="id_os" />
                                <asp:BoundField HeaderText="Requisición" DataField="ordenServicio" />
                                <asp:BoundField HeaderText="Clave de Gastos" DataField="claveGastos" />
                                <asp:BoundField HeaderText="Costo unitario" DataField="costo" DataFormatString="{0:c2}" />
                                <asp:BoundField HeaderText="IVA" DataField="IVA" DataFormatString="{0} %" />
                                <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:c2}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" ForeColor="Blue" Text="EDITAR" CssClass="btn btn-link " CommandName="editar" OnCommand="btn_Command" CommandArgument='<%# Eval("id_os") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" ForeColor="Red" Text="ELIMINAR" CssClass="btn btn-link  " CommandName="eliminar" OnCommand="btn_Command" CommandArgument='<%# Eval("id_os") %>' />
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


    <div id="modalAgregarOrdenServicio" class="modal fade modal-small" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Orden Servicio</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updOrdenServicio" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Nombre</h6>
                                        <asp:TextBox ID="txtNombreOS" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Clave Gastos</h6>
                                        <asp:DropDownList ID="ddlClaveGastos" runat="server" Width="100%" CssClass="select-basic-simple form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Costo unitario</h6>
                                        <asp:TextBox ID="txtCostoUnitario" runat="server" ClientIDMode="Static" CssClass="form-control" onblur="javascript:recalculaTotal(this,event);" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">IVA</h6>
                                        <asp:DropDownList ID="ddlIVA" runat="server" CssClass="select-basic-simple form-control" ClientIDMode="Static" Width="100%">
                                            <asp:ListItem Value="16" Text="16 %"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Total</h6>
                                        <b>
                                            <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static"></asp:Label></b>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-secondary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardaOrdenServicio" runat="server" CssClass="btn btn-secondary" Text="Guardar" OnClick="btnGuardaOrdenServicio_Click" />
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

        function abreModalOrdenServicio() {
            $('#modalAgregarOrdenServicio').modal('show');
        }

        function ocultaModalOrdenServicio() {
            $('#modalAgregarOrdenServicio').modal('hide');
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
