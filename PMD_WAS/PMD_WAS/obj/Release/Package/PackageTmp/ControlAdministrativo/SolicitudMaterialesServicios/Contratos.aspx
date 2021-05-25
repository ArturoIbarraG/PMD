<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="Contratos.aspx.vb" Inherits="PMD_WAS.Contratos" %>

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
            <div id="pnlAltaSolicitud" class="container-fluid" runat="server" visible="false">
                <div class="row">
                    <div class="col-4">
                        <h6 class="subtitle">Contrato:</h6>
                        <asp:DropDownList ID="ddlContrato" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlContrato_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-4">
                        <h6 class="subtitle">Requerimiento:</h6>
                        <asp:DropDownList ID="ddlRequerimiento" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                    <div class="col-2">
                        <h6 class="subtitle">Cantidad:</h6>
                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                    </div>
                    <div class="col-2 text-right">
                        <h6 class="subtitle" style="visibility: hidden;">Requerimiento:</h6>
                        <asp:Button ID="btnAgregarRequerimiento" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregarRequerimiento_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <h5 style="font-weight: 100;">Informacion de presupuesto para el <b>contrato</b> y <b>mes</b> seleccionados.</h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="border text-center">
                            <h6>PRESUPUESTO AUTORIZADO</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoAutorizado" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="border text-center">
                            <h6>PRESUPUESTO UTILIZADO</h6>
                          
                                <a class="btn btn-link" data-target="#modalPresupuestoUtilizado" data-toggle="modal">
                                     <h4> <asp:Label ID="lblPresupuestoUtilizado" runat="server"></asp:Label> </h4>
                                </a>
                           
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="border text-center">
                            <h6>PRESUPUESTO DISPONIBLE</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoDisponible" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="col-12 text-right">
                        <a class="btn btn-primary" data-toggle="modal" data-target="#modalSeleccionaAlmacen">SOLICITAR ORDEN DE SURTIDO</a>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:LinqDataSource ID="lnqRequisiciones" runat="server" OnSelecting="lnqRequisisciones_Selecting"></asp:LinqDataSource>
                        <asp:GridView ID="grdRequisiciones" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEliminar" runat="server" Text="Quitar" CssClass="btn btn-link" OnCommand="lnkEliminar_Command" CommandArgument='<%# Eval("IdRequisicion") %>' CommandName="quitar"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contrato" ItemStyle-Width="70%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContrato" runat="server" Text='<%# Eval("Contrato") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnIdReq" runat="server" Value='<%# Eval("IdRequisicion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Requerimiento" DataField="Requerimiento" />
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control text-right" Text='<%# Eval("Cantidad") %>' onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div id="modalPresupuestoUtilizado" class="modal fade modal-big" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h2 class="modal-title">Información de presupuesto utilizado</h2>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdOrdenesSurtido" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Núm. orden abastecimiento" DataField="id" />
                                                    <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />

                                                    <asp:TemplateField HeaderText="Contrato">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContrato" runat="server" Text='<%# String.Format("{0} - {1}", Eval("codigoContrato"), Eval("contrato")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Material" DataField="requirimiento" />
                                                    <asp:TemplateField HeaderText="Cantidad materiales">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIVA" runat="server" Text='<%# String.Format("{0:n0}", Eval("cantidad")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("total")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Estatus" DataField="estatus" />
                                                    <asp:BoundField HeaderText="Empleado" DataField="usuarioEnviado" />
                                                    <asp:TemplateField HeaderText="Fecha solicitud">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecha" runat="server" Text='<%# String.Format("{0:dd/MMMM/yyyy HH:mm tt}", Eval("fechaEnviado")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <h5>No hay información que mostrar</h5>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <h5>
                                            <asp:Label ID="lblTotalUtilizado" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <a class="btn btn-primary" data-dismiss="modal">Aceptar</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalSeleccionaAlmacen" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Almacen</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEnviar" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6>Favor de seleccionar el Almacen al cual se enviará la Orden de Abastecimiento</h6>
                                        <asp:DropDownList ID="ddlAlmacen" runat="server" CssClass="form-control select-basic-simple" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnEnviaOrdenesSurtido" runat="server" CssClass="btn btn-primary" Text="Solicitar" OnClick="btnEnviaOrdenesSurtido_Click" />
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

        function beginReq(sender, args) {

        }

        function endReq(sender, args) {
            aplicaEfectos();
        }

        $(document).ready(function () {
            aplicaEfectos();
        });

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

        function ocultaModalContratos() {
            $('#modalSeleccionaAlmacen').modal('hide');
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
    </script>
</asp:Content>

