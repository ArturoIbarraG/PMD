<%@ Page Language="VB" MasterPageFile="~/MasterGlobal.master" AutoEventWireup="false" Inherits="PMD_WAS.CatalogoRequerimiento" Codebehind="CatalogoRequerimiento.aspx.vb" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_requerimiento.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
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

      <asp:UpdatePanel ID="updRequerimientosGrid" runat="server">
        <ContentTemplate>
            <div class="container-fluid">

                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Catálogo de Requerimientos"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </div>

                </div>
                <div class="row">
                    <div class="col-5">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-3"></div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnAgregarRequerimientos" runat="server" CssClass="btn  btn-primary" Text="Agregar requerimiento" OnClick="btnAgregarRequerimientos_Click" /><br />
                        <br />
                    </div>
                </div>
                <hr />
                
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblIdRequerimiento" runat="server" Visible="false"></asp:Label>
                        <asp:GridView ID="grdRequerimientos" runat="server" Width="100%" Height="90px" CellPadding="4" DataKeyNames="id_req" AllowPaging="true" OnPageIndexChanging="grdRequerimientos_PageIndexChanging"
                            ForeColor="#333333" GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Height="40px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle ForeColor="#333333" BackColor="#F7F6F3" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="id_req" />
                                <asp:BoundField HeaderText="Requerimiento" DataField="requerimiento" />
                                <asp:BoundField HeaderText="Clave de Gastos" DataField="claveGastos" />
                                <asp:BoundField HeaderText="Costo unitario" DataField="costo" DataFormatString="{0:c2}" />
                                <asp:BoundField HeaderText="IVA" DataField="IVA" DataFormatString="{0} %" />
                                <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:c2}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" ForeColor="Blue" Text="EDITAR" CssClass="btn btn-link " CommandName="editar" OnCommand="btn_Command" CommandArgument='<%# Eval("id_req") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" ForeColor="Red" Text="ELIMINAR" CssClass="btn btn-link  " CommandName="eliminar" OnCommand="btn_Command" CommandArgument='<%# Eval("id_req") %>' />
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


    <div id="modalAgregarRequerimiento" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Requerimiento</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updRequerimiento" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-2">
                                        <p>Nombre</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="txtNombreRequerimiento" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-2">
                                        <p>Clave Gastos</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlClaveGastos" runat="server" Width="100%" CssClass="select-basic-simple form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-2">
                                        <p>Costo unitario</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:TextBox ID="txtCostoUnitario" runat="server" ClientIDMode="Static" CssClass="form-control" onblur="javascript:recalculaTotal(this,event);" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-2">
                                        <p>IVA</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlIVA" runat="server" CssClass="form-control" ClientIDMode="Static">
                                             <asp:ListItem Value="0" Text="0 %"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16 %"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-2">
                                        <p>Total</p>
                                    </div>
                                    <div class="col-8">
                                        <b>
                                            <asp:Label ID="lblTotal" runat="server" ClientIDMode="Static"></asp:Label></b>
                                    </div>
                                </div>
                                <br />
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardaRequerimiento" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardaRequerimiento_Click" />
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

        function abreModalRequerimiento() {
            $('#modalAgregarRequerimiento').modal('show');
        }

        function ocultaModalRequerimiento() {
            $('#modalAgregarRequerimiento').modal('hide');
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
