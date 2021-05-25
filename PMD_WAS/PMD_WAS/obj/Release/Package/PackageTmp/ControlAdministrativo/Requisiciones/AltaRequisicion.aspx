<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="AltaRequisicion.aspx.vb" Inherits="PMD_WAS.AltaRequisicion" %>

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
                    <div class="col-12">
                        <h5>Informacion de presupuesto en el mes seleccionado</h5>
                    </div>
                </div>
                <div class="row">
                    <div class="col-3">
                        <div class="border text-center">
                            <h6>PRESUPUESTO AUTORIZADO</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoAutorizado" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="border text-center">
                            <h6>PRESUPUESTO PRE COMPROMETIDO</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoReservado" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="border text-center">
                            <h6>PRESUPUESTO COMPROMETIDO</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoComprometido" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="border text-center">
                            <h6>PRESUPUESTO DISPONIBLE</h6>
                            <h4>
                                <asp:Label ID="lblPresupuestoDisponible" runat="server"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12 text-right">
                        <a class="btn btn-primary" data-toggle="modal" data-target="#modalSeleccionaAlmacen">ENVIAR LA REQUISICION
                        </a>

                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:LinqDataSource ID="lnqRequisiciones" runat="server" OnSelecting="lnqRequisisciones_Selecting"></asp:LinqDataSource>
                        <asp:GridView ID="grdRequisiciones" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="grdRequisiciones_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEliminar" runat="server" Text="Quitar" CssClass="btn btn-link" OnCommand="lnkEliminar_Command" CommandArgument='<%# Eval("IdRequisicion") %>' CommandName="quitar"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requisicion" ItemStyle-Width="80%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlRequisicion" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnIdReq" runat="server" Value='<%# Eval("IdRequisicion") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control text-right" Text='<%# Eval("Cantidad") %>' onkeypress="javascript:return solonumeros(event)"></asp:TextBox>
                                        <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR REQUISICION" CssClass="btn-agrega" Style="visibility: hidden;" OnClick="btnAgregar_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
                                        <h6>Favor de seleccionar el Almacen al cual se enviará la Requisicion</h6>
                                        <asp:DropDownList ID="ddlAlmacen" runat="server" CssClass="form-control select-basic-simple" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnEnviaRequisiciones" runat="server" CssClass="btn btn-primary" Text="Solicitar" OnClick="btnGuardarRequisiciones_Click" />
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

        function ocultaModalAlmacen() {
            $('#modalSeleccionaAlmacen').modal('hide');
        }
    </script>
</asp:Content>
