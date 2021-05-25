<%@ Page Language="VB" Title="Asignación de Recursos Humanos" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.AsignacionRecursos" Codebehind="AsignacionRecursos.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .item-asignacion input {
            max-width: 50px !important;
            text-align: right;
        }

        .table-footer span {
            text-align: right;
        }

        select {
            max-width: 100%;
            width: 350px;
        }

        input {
            max-width: 100%;
        }

        .item {
            padding: 7px;
        }

        .tabla-pmd > .item:nth-child(2n+1):not(:first-child) {
            background-color: #EEE;
        }

        .table {
            max-width: 100%;
        }

        .btn-header {
            float: right;
            font-size: 12px;
            cursor: pointer;
            text-decoration: underline !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="updAutorizacion" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaría/Instituto:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Dependencia:</h6>
                        <asp:DropDownList ID="ddlDependencia" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDependencia_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div id="panelAsignacion" class="row">
                    <div class="col-12">
                        <hr />
                        <asp:Panel ID="pnlAsignacionRecursos" runat="server" Visible="false">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p style="font-size: medium;">Tipo de asignación</p>
                                        <asp:DropDownList ID="ddlTipoAsignacion" runat="server" AutoPostBack="true" CssClass="select-basic-simple" OnSelectedIndexChanged="ddlTipoAsignacion_SelectedIndexChanged">
                                            <asp:ListItem Text="Por empleado" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Por puesto" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <p style="font-size: medium;">
                                            <asp:Label ID="labelElementoAsignacion" runat="server"></asp:Label>
                                        </p>
                                        <asp:DropDownList ID="ddlElementoAsignar" runat="server" AutoPostBack="true" CssClass="select-basic-simple" OnSelectedIndexChanged="ddlElementoAsignar_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-2">
                                        <p style="font-size: medium; visibility: hidden;">Frecuencia de asignación</p>
                                        <button class="btn  btn-secondary" data-target="#modalAgregarEmpleado" data-toggle="modal">Solicitar recurso</button>
                                    </div>
                                    <div class="col-2">
                                        <asp:Label ID="lblEmpleadosContador" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblElementoAsignar" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-2">
                                        <asp:Button ID="btnAnterior" runat="server" Text="<" ToolTip="Anterior" OnClick="btnAnterior_Click" />
                                    </div>
                                    <div class="col-8">
                                    </div>
                                    <div class="col-2 text-right">
                                        <asp:Button ID="btnSiguiente" runat="server" Text=">" ToolTip="Siguiente" OnClick="btnSiguiente_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:Repeater ID="rptEmpleadoSubActividad" runat="server" OnItemDataBound="rptEmpleadoSubActividad_ItemDataBound">
                                <ItemTemplate>
                                    <br />
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <b>NOMBRE ACTIVIDAD:</b> <%# Eval("Nombr_linea") %>
                                                <asp:HiddenField ID="hdnLinea" runat="server" Value='<%# Eval("ID") %>' />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-12">

                                                <asp:Panel ID="pnlMensual" runat="server">
                                                    <table width="100%" class="table table-sm table-hover table-bordered table-asignacion">
                                                        <tr>
                                                            <td style="width: 28%">Subactividad</td>
                                                            <td style="width: 6%">ENERO 
                                                                <a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 1);">COPIAR >></a>
                                                            </td>
                                                            <td style="width: 6%">FEBRERO
                                                                <a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 2);">COPIAR >></a>
                                                            </td>
                                                            <td style="width: 6%">MARZO
                                                                <a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 3);">COPIAR >></a>
                                                            </td>
                                                            <td style="width: 6%">ABRIL<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 4);">COPIAR >></a></td>
                                                            <td style="width: 6%">MAYO<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 5);">COPIAR >></a></td>
                                                            <td style="width: 6%">JUNIO<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 6);">COPIAR >></a></td>
                                                            <td style="width: 6%">JULIO<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 7);">COPIAR >></a></td>
                                                            <td style="width: 6%">AGOSTO<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 8);">COPIAR >></a></td>
                                                            <td style="width: 6%">SEPTIEMBRE<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 9);">COPIAR >></a></td>
                                                            <td style="width: 6%">OCTUBRE<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 10);">COPIAR >></a></td>
                                                            <td style="width: 6%">NOVIEMBRE<a class="btn  btn-link btn-header" onclick="javascript:copiarTodos(this, 11);">COPIAR >></a></td>
                                                            <td style="width: 6%">DICIEMBRE</td>
                                                        </tr>
                                                        <asp:Repeater ID="rptSubActividadesMeses" runat="server" OnItemDataBound="rptSubActividades_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnSubActividad" runat="server" Value='<%# Eval("IdSubActividad") %>' />
                                                                <tr class="item-asignacion">
                                                                    <td>
                                                                        <b><%# Eval("Nombre") %></b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt1" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="1" Text='<%# Eval("Enero") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt2" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="2" Text='<%# Eval("Febrero") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt3" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="3" Text='<%# Eval("Marzo") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt4" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="4" Text='<%# Eval("Abril") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt5" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="5" Text='<%# Eval("Mayo") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt6" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="6" Text='<%# Eval("Junio") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt7" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="7" Text='<%# Eval("Julio") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt8" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="8" Text='<%# Eval("Agosto") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt9" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="9" Text='<%# Eval("Septiembre") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt10" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="10" Text='<%# Eval("Octubre") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt11" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="11" Text='<%# Eval("Noviembre") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                    <td>
                                                                        <asp:TextBox ID="txt12" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="12" Text='<%# Eval("Diciembre") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                </tr>

                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td colspan="13">
                                                                        <asp:Panel ID="pnlSinDatos" runat="server">
                                                                            <h4 style="text-align: center;">La Actividad no cuenta con <b>Subactividades</b></h4>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>

                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlAnual" runat="server">
                                                    <table width="100%" class="table table-sm table-hover table-bordered">
                                                        <tr>
                                                            <td style="width: 50%;">Subactividad</td>
                                                            <td style="width: 50%;">2020</td>
                                                        </tr>
                                                        <asp:Repeater ID="rptSubActividadesAños" runat="server" OnItemDataBound="rptSubActividades_ItemDataBound">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnSubActividad" runat="server" Value='<%# Eval("IdSubActividad") %>' />
                                                                <tr class="item-asignacion">
                                                                    <td style="width: 50%;">
                                                                        <b><%# Eval("Nombre") %></b>
                                                                    </td>
                                                                    <td style="width: 50%;">
                                                                        <asp:TextBox ID="txtAnual" runat="server" onblur="javascript:validaPorcentajeAsignacion(this,event);" data-group="2020" Text='<%# Eval("2020") %>' MaxLength="3" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox></td>
                                                                </tr>

                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <tr>
                                                                    <td colspan="13">
                                                                        <asp:Panel ID="pnlSinDatos" runat="server">
                                                                            <h4 style="text-align: center;">La Actividad no cuenta con <b>Subactividades</b></h4>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>

                                                            </FooterTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:Panel ID="pnlFooterMensual" runat="server">
                                                    <table width="100%" class="table table-sm table-hover table-footer">
                                                        <tfoot>
                                                            <tr>
                                                                <td style="width: 28%; text-align: right;">
                                                                    <asp:Label ID="labelFooterAsignacionMensual" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl1" data-summary="1" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl2" data-summary="2" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl3" data-summary="3" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl4" data-summary="4" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl5" data-summary="5" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl6" data-summary="6" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl7" data-summary="7" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl8" data-summary="8" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl9" data-summary="9" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl10" data-summary="10" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl11" data-summary="11" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                                <td style="width: 6%">
                                                                    <asp:Label ID="lbl12" data-summary="12" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlFooterAnual" runat="server">
                                                    <table width="100%" class="table table-sm table-hover table-footer">
                                                        <tfoot>
                                                            <tr>
                                                                <td style="width: 50%; text-align: right;">
                                                                    <asp:Label ID="labelFooterAsignacionAnual" runat="server"></asp:Label>
                                                                </td>
                                                                <td style="width: 50%">
                                                                    <asp:Label ID="lblTotalAnual" data-summary="2020" runat="server" Text="[ 0 %]"></asp:Label></td>
                                                            </tr>
                                                        </tfoot>
                                                    </table>
                                                </asp:Panel>

                                            </div>
                                        </div>
                                    </div>

                                </FooterTemplate>
                            </asp:Repeater>
                            <hr />
                            <div class="row">
                                <div class="col-12 text-right">
                                    <br />
                                    <asp:Button ID="btnGuardarCambios" runat="server" data-validador="0" Text="GUARDAR ASIGNACIÓN" OnClick="btnGuardarCambios_Click" CssClass="btn  btn-secondary" />
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalAgregarEmpleado" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Solicitar Recurso</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updAgregarEmpleado" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p>Puesto</p>
                                        <asp:DropDownList ID="ddlPuestoAgregar" runat="server" Width="100%"></asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <p>Cantidad</p>
                                        <asp:TextBox ID="txtCantidadEmpleados" runat="server" Width="100%" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Modalidad</p>
                                        <asp:DropDownList ID="ddlModalidad" runat="server" Width="100%">
                                            <asp:ListItem Text="Base" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Temporal" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <br />
                                        <asp:Button ID="btnAgregarEmpleados" runat="server" data-validador="0" CssClass="btn  btn-secondary" Text="Solicitar" OnClick="btnAgregarEmpleados_Click" />
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

        var validarCambios = false;
        function beginReq(sender, args) {

        }

        function endReq(sender, args) {
            $('.select-basic-simple').select2();

            $('#panelAsignacion input[type="text"]').on("change paste keyup", function () {
                validarCambios = true;
            });

            $('input[type="submit"],button, select').on("click change", function (event) {
                var validador = $(this).data('validador') == '0';
                if (!validador && validarCambios) {
                    if (confirm('Tienes cambios pendientes por guardar, ¿deseas continuar y perder los cambios?') === false) {
                        event.preventDefault();
                        event.stopPropagation();
                        return false;
                    }
                    else
                        validarCambios = false;
                }
            });

            validaPorGrupo(1);
            validaPorGrupo(2);
            validaPorGrupo(3);
            validaPorGrupo(4);
            validaPorGrupo(5);
            validaPorGrupo(6);
            validaPorGrupo(7);
            validaPorGrupo(8);
            validaPorGrupo(9);
            validaPorGrupo(10);
            validaPorGrupo(11);
            validaPorGrupo(12);
        }

        function quitaValidarCambios() {
            validarCambios = false;
        }

        function validaPorcentajeAsignacion(o, e) {
            var $actual = $(o);
            var group = $actual.data('group');
            var totales = $('[data-group="' + group + '"]');
            var porcentajeTotal = 0;
            totales.each(function () {
                var t = $(this);
                porcentajeTotal += parseInt(t.val());
            });

            if (porcentajeTotal > 100) {
                alert('El porcentaje de asignación por Frecuencia no debe ser mayor a [100 %]');
                $actual.val('0');
            }
            else {
                var summary = $('[data-summary="' + group + '"]');
                summary.text('[ ' + porcentajeTotal.toString() + ' % ]');
            }
        }

        function ocultaModalSolicitarRecurso() {
            $('#modalAgregarEmpleado').modal('hide');
            $('.modal-backdrop').remove();
        }

        function validaPorGrupo(group) {
            var totales = $('[data-group="' + group + '"]');
            var porcentajeTotal = 0;
            totales.each(function () {
                var t = $(this);
                porcentajeTotal += parseInt(t.val());
            });

            var summary = $('[data-summary="' + group + '"]');
            summary.text('[ ' + porcentajeTotal.toString() + ' % ]');
        }

        function copiarTodos(elment, group) {
            var parent = $(elment).closest('.table-asignacion');
            var newGroup = group + 1;
            while (newGroup <= 12) {
                parent.find('.item-asignacion').each(function () {
                    var row = $(this);
                    var value = row.find('input[data-group="' + group + '"]').val();
                    row.find('input[data-group="' + newGroup + '"]').val(value);
                });
                newGroup = newGroup + 1;
            }

            validaPorGrupo(1);
            validaPorGrupo(2);
            validaPorGrupo(3);
            validaPorGrupo(4);
            validaPorGrupo(5);
            validaPorGrupo(6);
            validaPorGrupo(7);
            validaPorGrupo(8);
            validaPorGrupo(9);
            validaPorGrupo(10);
            validaPorGrupo(11);
            validaPorGrupo(12);

            return false;
        }
    </script>
</asp:Content>
