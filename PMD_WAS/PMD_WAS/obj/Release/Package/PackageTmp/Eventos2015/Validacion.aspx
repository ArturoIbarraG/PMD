<%@ Page Language="VB" AutoEventWireup="false" Title="Valida Evento" Inherits="PMD_WAS.Validacion" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="Validacion.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #modalIframeURL .modal-header {
            padding: 2px 8px;
        }

        #modalIframeURL .modal-body {
            padding: 0;
        }

        #modalIframeURL .modal-footer {
            padding: 8px;
            border: none;
        }
    </style>
    <link href="estilo_validacion.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function alerta_Validacion_Exito() {
            mensajeCustom('success', 'Proceso terminado', 'Validadas con éxito!!');
        }

        function Complemento_Exitoso() {
            $('#modalCompletarInformacion').modal("hide");
            mensajeCustom('success', 'Proceso terminado', 'Validadas con éxito!!');
        }

        function alertaEnviaCorreo() {
            mensajeCustom('success', 'E-m@il!!', 'Mensaje Enviado!!!');
        }

        function alertaErrorEnvioCorreo() {
            mensajeCustom('error', 'E-m@il!!', 'No se envio correo,vuelva a intentarlo!!!');
        }


        function alerta_Validacion_AmbosCheck() {
            mensajeCustom('warning', 'Check Box', 'Favor de Seleccionar solo 1 Opción "Aprobar o  No Aprobar" !!');
        }



        function alerta_Alta_Error() {
            mensajeCustom('error', 'ERROR', 'No se guardo la información!!');
        }

        function alerta_Alta_Error_PuntoMapa() {
            mensajeCustom('error', 'ERROR', 'No se guardo:PUNTO EN MAPA');
        }


        function alerta_Alta_Error_DatosFaltantes() {

            mensajeCustom('error', 'ERROR', 'Favor de capturar todos los datos!!');
        }

        function alerta_Alta_Error_NoExisteFolio() {
            mensajeCustom('error', 'ERROR FOLIO EVENTO', 'No existe el Folio Capturado!!');
        }


        function alerta_Alta_Error_Hora() {
            mensajeCustom('warning', 'Favor de validar', 'Recuerde el formato de Hora, desde 00:00 hasta 23:59');
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

        // solo numeros con decimales
        function solonumerosdecimales(e) {

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
            if ((key < 46 && key > 46) && (key < 48 || key > 57)) {

                return false;
            }

            return true;
        }


        //No se captura nada//

        function nocaptura(e) {

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
            if (key < 0 || key > 0) {
                return false;
            }

            return true;
        }



        function solonumletras(e) {

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
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122)) {
                return true;
            }

            return false;
        }

        function solonumletrasMayMinyespacios(e) {

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
            if ((key >= 32 && key <= 32) || (key >= 45 && key <= 45) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumletrasMayespacios(e) {

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
            if ((key >= 32 && key <= 32) || (key >= 44 && key <= 46) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241) || (key >= 193 && key <= 193) || (key >= 201 && key <= 201) || (key >= 205 && key <= 205) || (key >= 211 && key <= 211) || (key >= 218 && key <= 218) || (key >= 225 && key <= 225) || (key >= 233 && key <= 233) || (key >= 237 && key <= 237) || (key >= 243 && key <= 243) || (key >= 250 && key <= 250)) {
                return true;
            }

            return false;
        }


        function solonumletrasMay(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/) Y 45(-)
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumerosparafecha(e) {

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
            if (key < 48 || key > 58) {
                return false;
            }

            return true;
        }

        var patron = new Array(2, 2)
        function mascara(d, sep, pat, nums) {
            if (d.valant != d.value) {
                val = d.value
                largo = val.length
                val = val.split(sep)
                val2 = ''
                for (r = 0; r < val.length; r++) {
                    val2 += val[r]
                }
                if (nums) {
                    for (z = 0; z < val2.length; z++) {
                        if (isNaN(val2.charAt(z))) {
                            letra = new RegExp(val2.charAt(z), "g")
                            val2 = val2.replace(letra, "")
                        }
                    }
                }
                val = ''
                val3 = new Array()
                for (s = 0; s < pat.length; s++) {
                    val3[s] = val2.substring(0, pat[s])
                    val2 = val2.substr(pat[s])
                }
                for (q = 0; q < val3.length; q++) {
                    if (q == 0) {
                        val = val3[q]
                    }
                    else {
                        if (val3[q] != "") {
                            val += sep + val3[q]
                        }
                    }
                }
                d.value = val
                d.valant = val
            }
            if (val.length == 5) {
                hora = val.split(":")
                if ((hora[0] > 23) || (hora[1] > 59))
                    alert("Recuerde el formato de Horas, desde 00:00 hasta 23:59 ")

            }

        }

        function abreModalIframe(url) {
            document.getElementById('iframeModalURL').src = url;
            $('#modalIframeURL').modal('show');
        }

    </script>

    <div id="modalIframeURL" class="modal modal-big fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <iframe id="iframeModalURL" width="100%" height="500px"></iframe>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-primary right" data-dismiss="modal">Cerrar</a>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-7">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <h3>Gráfico de comportamiento</h3>
                            <br />
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updFiltros" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-4">
                                    <h6 class="subtitle">Administración:</h6>
                                    <asp:DropDownList ID="ddlAdministracion" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAdministracion_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <h6 class="subtitle">Año:</h6>
                                    <asp:DropDownList ID="ddlAnio" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="form-control select-basic-simple" onchange="javascript:eventosDetallePresupuestoPorEstatus();"></asp:DropDownList>
                                </div>
                                <div class="col-4">
                                    <h6 class="subtitle">Estatus</h6>
                                    <select id="tipoEventoSelected" onchange="javascript:eventosDetallePresupuestoPorEstatus();" class="form-control select-basic-simple">
                                        <option value="-1">Todos (Aprobados, Pendientes y Rechazados)</option>
                                        <option value="3" selected="selected">Aprobados y Pendientes</option>
                                        <option value="1">Aprobados</option>
                                        <option value="0">Pendientes</option>
                                        <option value="2">Rechazados</option>
                                    </select>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <div class="col-12">
                            <br />
                            <div id="graficoAnual">
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-5">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <h3>Presupuesto </h3>
                            <br />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-7">
                            <p>Presupuesto comprometido:</p>
                        </div>
                        <div class="col-5">
                            <asp:Label ID="lblPresupuestoComprometido" runat="server" ClientIDMode="Static" Text="$ 10,000"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-7">
                            <p>Presupuesto disponible:</p>
                        </div>
                        <div class="col-5">
                            <asp:Label ID="lblPresupuestoDisponible" runat="server" ClientIDMode="Static" Text="$ 10,000"></asp:Label>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-7">
                            <p>Presupuesto Total:</p>
                        </div>
                        <div class="col-5">
                            <asp:Label ID="lblPresupuestoTotal" runat="server" ClientIDMode="Static" Text="$ 150,000"></asp:Label>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <hr />
        <h2>Eventos pendientes por validar</h2>
        <br />
        <div class="row">
            <div class="col-12">
                <div class="table-responsive">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Folio">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFolioEvento" runat="server" Value='<%# Eval("Folio") %>' />
                                    <a class="btn btn-link" onclick="javascript:abreModalIframe('/Eventos2015/ConsultaGeneral.aspx?folio=1&readOnly=1&FolioSS=<%# Eval("Folio")%>');" href="#">
                                        <%# Eval("Folio") %>
                                    </a>

                                    <a class="btn btn-link" onclick="javascript:abreModalIframe('/Eventos2015/Ficha.aspx?folio=<%# Eval("Folio")%>&readOnly=1');" href="#">Ficha
                                    </a>

                                    <a class="btn btn-link" onclick="javascript:abreModalIframe('/Eventos2015/Requerimientos.aspx?folio=<%# Eval("Folio")%>&readOnly=1');" href="#">Req
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Evento">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvento" runat="server" Text='<%# Eval("nombre_evento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Lugar">
                                <ItemTemplate>
                                    <asp:Label ID="lblLugar" runat="server" Text='<%# Eval("lugar") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fecha Evento">
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaEvento" runat="server" Text='<%# Eval("fecha") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Horario evento">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# String.Format("Desde las {0} a las {1}", Eval("hr_ini"), Eval("hr_fin")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Horario arribo Alcalde">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# If(String.IsNullOrEmpty(Eval("hr_alcalde")), "No asistirá", String.Format("Desde las {0} a las {1}", Eval("hr_alcalde"), Eval("hr_salida"))) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salida Alcalde" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label20" runat="server" Text='<%# Eval("hr_salida") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hora Término" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# Eval("hr_fin") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Secretario">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# Eval("Secretario") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Solicitante">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# Eval("Solicitante") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Telefono">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# Eval("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aprobar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckAprobada" runat="server" Checked="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Aprobar" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckNoAprobada" runat="server" Checked="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Auditoria Interna" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckAuditoriaInterna" runat="server" Checked="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Auditoria Externa" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckAuditoriaExterna" runat="server" Checked="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Comentario" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComentarioNoAprobado" runat="server" Width="90px" Text="" TextMode="MultiLine"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Presupuesto Aprox." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPresupuesto" runat="server" Text='<%# String.Format("{0:C2}", Eval("presupuesto")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Presupuesto Comunicación" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPresupuestoCom" runat="server" Text='<%# String.Format("{0:C2}", Eval("presupuestoComunicacion")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="¿Evento presencial?" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEventoPresencial" runat="server" Text='<%# Eval("esPresencial") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <hr />
                            <h4>No hay información que mostrar.</h4>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <br />
        <hr />
        <div class="row">
            <div class="col-12 text-right">
                <asp:Button ID="btnGuardar" CssClass="btn btn-primary" runat="server" Text="Guardar" />
            </div>
        </div>
    </div>

    <a href="http://apycom.com/" style="color: White">.</a>
    <div id="divusuarioVal">
        <asp:Label ID="Label4" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaVal">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <%--  <input type="button" value="PROBAR" onclick="javascript: muestraModalComplementarInformacion();" />
    <asp:Button ID="btnTestCorreo" runat="server" Text="Probar correo" OnClick="btnTestCorreo_Click" />--%>
    <!--    MODAL COMPLETAR    -->

    <div id="modalCompletarInformacion" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Complementar presupuesto</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEventoAprobado" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdnFolioActual" runat="server" Value="0" />
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-8">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-12">
                                                    <h2>
                                                        <asp:Label ID="lblEventoCount" runat="server" Text="Evento 1 de 2."></asp:Label></h2>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-6">
                                                    <h3 style="font-weight: 600;">
                                                        <asp:Label ID="lblNombreEvento" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                                <div class="col-6">
                                                    <h3 style="font-weight: 600;">
                                                        <asp:Label ID="lblFechaEvento" runat="server"></asp:Label><br />
                                                        <asp:Label ID="lblHoraEvento" runat="server"></asp:Label>
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-12">
                                                    <h4 style="margin-bottom: 10px;">Confirmar asistencia al evento.</h4>
                                                    <asp:CheckBox ID="chkConfirmaAsistenciaAlcalde" runat="server" Text="Si asisitiré" />
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlAnimacion" runat="server" Visible="false">
                                                <br />
                                                <div class="row">
                                                    <div class="col-12">
                                                        <h4>¿Desea agregar animación al evento?</h4>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-3">
                                                        <label>Tipo animación</label>
                                                        <asp:DropDownList ID="ddlTipoAnimacion" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoAnimacion_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-3">
                                                        <label>Animación</label>
                                                        <asp:DropDownList ID="ddlAnimacion" runat="server" Width="100%"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-3">
                                                        <label>Cantidad</label>
                                                        <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-3">
                                                        <label style="visibility: hidden; display: block;">Dummy</label>
                                                        <asp:Button ID="btnAgregarAnimacion" runat="server" Text="Agregar" CssClass="btn  btn-secondary" OnCommand="lnqAgregarAnimacion_Command"></asp:Button>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-12">
                                                        <asp:GridView ID="grdAnimacion" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowDeleting="grdAnimacion_RowDeleting"
                                                            CellPadding="4" ForeColor="#333333" AutoGenerateDeleteButton="true"
                                                            GridLines="None"
                                                            Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Animación">
                                                                    <ItemTemplate>
                                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("IdAnimacion") %>' />
                                                                        <asp:HiddenField ID="hdnFolioEvento" runat="server" Value='<%# Eval("Folio") %>' />
                                                                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Animacion") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Cantidad">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# String.Format("{0:N0}", Eval("Cantidad")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("Total")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <h5 style="border: 1px solid #EEE; padding: 15px;">No hay información disponible.</h5>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-12">
                                                    <h2>PRESUPUESTO </h2>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-8">
                                                    <p>Presupuesto Comprometido:</p>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <asp:Label ID="lblPresupuestoAnimacionCompr" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnAnimacionComprometido" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-8">
                                                    <p>Presupuesto Disponible:</p>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <asp:Label ID="lblPresupuestoAnimacionDisponible" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnAnimacionDisponible" runat="server" />
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-8">
                                                    <p>Presupuesto Total:</p>
                                                </div>
                                                <div class="col-4 text-right">
                                                    <asp:Label ID="lblPresupuestoAnimacionTotal" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hdnAnimacionTotal" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <asp:Button ID="btnAnterior" runat="server" Text="Anterior" CssClass="btn btn-primary" OnClick="btnAnterior_Click" />
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnSiguiente" runat="server" Text="Continuar" CssClass="btn btn-primary" OnClick="btnSiguiente_Click" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%--  <asp:UpdatePanel ID="updRechazar" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h2>Por favor, complemente la información antes de continuar</h2>
                                    </div>
                                </div>
                                <br />
                                <asp:Repeater ID="rptComplemento" runat="server" OnItemDataBound="rptComplemento_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnFolioEvento" runat="server" Value='<%# Eval("folio") %>' />
                                        <div class="row">
                                            <div class="col-12">
                                                <h3>
                                                    <asp:Label ID="lblNombreEvento" runat="server" Text='<%# String.Format("{0} - {1}", Eval("folio"), Eval("nombre")) %>'></asp:Label></h3>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-6">
                                                <p>¿Asistirá el Alcalde?</p>
                                            </div>
                                            <div class="col-6">
                                                <asp:CheckBox ID="chkAsisteAlcalde" runat="server" />
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-5">
                                                <span>Animación</span><br />
                                                <asp:DropDownList ID="ddlAnimacion" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                            <div class="col-5">
                                                <span>Cantidad</span><br />
                                                <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-2">
                                                <span style="visibility: hidden;">Animación</span><br />
                                                <asp:Button ID="btnAgregarAnimacion" runat="server" Text="Agregar" CommandName="animacion" CommandArgument='<%# Eval("folio") %>' CssClass="btn  btn-secondary" OnCommand="lnqAgregarAnimacion_Command"></asp:Button>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-12">

                                                <asp:GridView ID="grdAnimacion" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdAnimacion_RowDeleting"
                                                    CellPadding="4" ForeColor="#333333"
                                                    GridLines="None"
                                                    Width="100%">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Animación">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("IdAnimacion") %>' />
                                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Animacion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cantidad">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCantidad" runat="server" Text='<%# String.Format("{0:N0}", Eval("Cantidad")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnGuardarComplemento" runat="server" Text="Terminar" CssClass="btn  btn-primary" OnClick="btnGuardarComplemento_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/Chart.min.js"></script>
    <script type="text/javascript">
        function muestraModalComplementarInformacion() {
            $('#modalCompletarInformacion').modal('show');
        }

        $(document).ready(function () {
            eventosDetallePresupuestoPorEstatus();
        });

        function eventosDetallePresupuestoPorEstatus() {
            var url = '<%= ResolveClientUrl("~/WSEventos.asmx/RegresaGraficoAnualEventosPresupuesto") %>';

            var parametros = '{año:' + $("#ddlAnio option:selected").val() + ',estatus:' + $('#tipoEventoSelected').val() + '}';
            var datasetEventos = [];
            $.ajax({
                type: 'POST',
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: parametros,
                success: function (data) {
                    console.log(data);
                    var json = JSON.parse(data.d);
                    if (json !== undefined && json.length > 1 && json[0].row !== null) {
                        var content = $('#graficoAnual');
                        content.html('');
                        content.append('<canvas id="canvasGraficaAnual"></canvas>');
                        console.log(data.d);
                        for (i = 0; i < json.length; i++) {
                            var columns = [];
                            var values = [];
                            for (x = 1; x < Object.keys(json[i]).length; x++) {

                                columns.push(Object.keys(json[i])[x]);
                                values.push(parseInt(json[i][Object.keys(json[i])[x]]));

                            }

                            //Get the color
                            var colorX = materialColor();
                            var evento = {
                                label: json[i].Tipo,
                                backgroundColor: colorX,
                                data: values
                            };

                            datasetEventos.push(evento);
                        }

                        var barCharData = {
                            labels: columns, //['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                            datasets: datasetEventos
                        };
                        var config = {
                            type: 'bar',
                            data: barCharData,
                            options: {
                                responsive: true,
                                title: {
                                    display: false
                                },
                                tooltips: {
                                    mode: 'index',
                                    intersect: false,
                                    callbacks: {
                                        label: function (tooltipItem, data) {
                                            var label = (data.datasets[tooltipItem.datasetIndex].label + " - Presupuesto: $" + Number(tooltipItem.yLabel).toFixed(0).replace(/./g, function (c, i, a) {
                                                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                            }) || '');
                                            return tooltipItem.yLabel == 0 ? 0 : label;
                                        }
                                    }
                                },
                                legend: {
                                    display: false
                                },
                                scales: {
                                    xAxes: [{
                                        display: true,
                                        scaleLabel: {
                                            display: true,
                                            labelString: 'Mes'
                                        },
                                        stacked: true
                                    }],
                                    yAxes: [{
                                        display: true,
                                        scaleLabel: {
                                            display: true,
                                            labelString: 'Presupeusto'
                                        },
                                        stacked: true,
                                        ticks: {
                                            callback: function (label, index, labels) {
                                                return '$ ' + Number(label).toFixed(0).replace(/./g, function (c, i, a) {
                                                    return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                                });
                                            }
                                        }
                                    }]
                                }
                            }
                        };

                        var ctx = document.getElementById('canvasGraficaAnual').getContext('2d');
                        new Chart(ctx, config);

                        ////ajusta el tamaño al grafico
                        //var height = $(window).height();
                        //$('#canvasGraficaAnual').css({ 'max-height': (height - 220) + 'px', 'max-width': ((height - 220) * 2), 'margin': '0px auto' });
                    }
                    else {
                        $('#graficoAnual').html('<i class="fas fa-info-circle"></i><h5>No hay información que mostrar.</h5>');
                    }
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });

        }

        function eventosGeneralPorEstatus() {
            var url = '<%= ResolveClientUrl("~/WSEventos.asmx/RegresaComportamientoAnualPresupuestoEventos") %>';
            var parametros = '{}';
            $.ajax({
                type: 'POST',
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: parametros,
                success: function (data) {
                    var json = JSON.parse(data.d);
                    var content = $('#graficoAnual');
                    content.html('');
                    content.append('<canvas id="canvasGraficaAnual"></canvas>');
                    var dataSetDesgloceAnual = [];
                    //EVENTOS PENDIENTES DE VALIDAR
                    var pendientesValidar = {
                        label: 'Eventos pendientes de Validar',
                        backgroundColor: '#3F51B5',
                        type: 'bar',
                        data: [
                            json[0].Enero,
                            json[0].Febrero,
                            json[0].Marzo,
                            json[0].Abril,
                            json[0].Mayo,
                            json[0].Junio,
                            json[0].Julio,
                            json[0].Agosto,
                            json[0].Septiembre,
                            json[0].Octubre,
                            json[0].Noviembre,
                            json[0].Diciembre
                        ]
                    };

                    //EVENTOS ACEPTADOS
                    var eventosAceptados = {
                        label: 'Eventos pendientes de Validar',
                        backgroundColor: '#77cc6d',
                        type: 'bar',
                        data: [
                            json[1].Enero,
                            json[1].Febrero,
                            json[1].Marzo,
                            json[1].Abril,
                            json[1].Mayo,
                            json[1].Junio,
                            json[1].Julio,
                            json[1].Agosto,
                            json[1].Septiembre,
                            json[1].Octubre,
                            json[1].Noviembre,
                            json[1].Diciembre
                        ]
                    };

                    //EVENTOS RECHAZADOS
                    var eventosRechazados = {
                        label: 'Eventos pendientes de Validar',
                        backgroundColor: '#FF5722',
                        type: 'bar',
                        data: [
                            json[2].Enero,
                            json[2].Febrero,
                            json[2].Marzo,
                            json[2].Abril,
                            json[2].Mayo,
                            json[2].Junio,
                            json[2].Julio,
                            json[2].Agosto,
                            json[2].Septiembre,
                            json[2].Octubre,
                            json[2].Noviembre,
                            json[2].Diciembre
                        ]
                    };

                    dataSetDesgloceAnual.push(pendientesValidar);
                    dataSetDesgloceAnual.push(eventosAceptados);
                    dataSetDesgloceAnual.push(eventosRechazados);

                    var barCharData = {
                        labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                        datasets: dataSetDesgloceAnual
                    };

                    var config = {
                        type: 'bar',
                        data: barCharData,
                        options: {
                            responsive: true,
                            title: {
                                display: true,
                                text: ''
                            },
                            tooltips: {
                                mode: 'index',
                                intersect: false,
                                callbacks: {
                                    label: function (tooltipItem, data) {
                                        if (tooltipItem.datasetIndex === 0) {
                                            return 'PENDIENTES MXN: $' + Number(tooltipItem.yLabel).toFixed(0).replace(/./g, function (c, i, a) {
                                                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                            });
                                        } else if (tooltipItem.datasetIndex === 1) {
                                            return 'ACEPTADOS MXN: $' + Number(tooltipItem.yLabel).toFixed(0).replace(/./g, function (c, i, a) {
                                                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                            });
                                        }
                                        else if (tooltipItem.datasetIndex === 2) {
                                            return 'RECHAZADOS MXN: $' + Number(tooltipItem.yLabel).toFixed(0).replace(/./g, function (c, i, a) {
                                                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                            });
                                        }

                                    }
                                }
                            },
                            hover: {
                                mode: 'nearest',
                                intersect: true
                            },
                            legend: {
                                display: false
                            },
                            scales: {
                                xAxes: [{
                                    display: true,
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Mes'
                                    },
                                    stacked: true
                                }],
                                yAxes: [{
                                    display: true,
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'MXN'
                                    },
                                    stacked: true,
                                    ticks: {
                                        callback: function (label, index, labels) {
                                            return '$ ' + Number(label).toFixed(0).replace(/./g, function (c, i, a) {
                                                return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
                                            });
                                        }
                                    }
                                }]
                            }
                        }
                    };
                    var ctx = document.getElementById('canvasGraficaAnual').getContext('2d');
                    new Chart(ctx, config);
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });
        }

        function materialColor() {
            // colors from https://github.com/egoist/color-lib/blob/master/color.json
            var colors = {
                "red": {
                    ////"50": "#ffebee",
                    "100": "#ffcdd2",
                    "200": "#ef9a9a",
                    "300": "#e57373",
                    "400": "#ef5350",
                    "500": "#f44336",
                    "600": "#e53935",
                    "700": "#d32f2f",
                    "800": "#c62828",
                    "900": "#b71c1c",
                    "hex": "#f44336",
                    "a100": "#ff8a80",
                    "a200": "#ff5252",
                    "a400": "#ff1744",
                    "a700": "#d50000"
                },
                "pink": {
                    ////"50": "#fce4ec",
                    "100": "#f8bbd0",
                    "200": "#f48fb1",
                    "300": "#f06292",
                    "400": "#ec407a",
                    "500": "#e91e63",
                    "600": "#d81b60",
                    "700": "#c2185b",
                    "800": "#ad1457",
                    "900": "#880e4f",
                    "hex": "#e91e63",
                    "a100": "#ff80ab",
                    "a200": "#ff4081",
                    "a400": "#f50057",
                    "a700": "#c51162"
                },
                "purple": {
                    //"50": "#f3e5f5",
                    "100": "#e1bee7",
                    "200": "#ce93d8",
                    "300": "#ba68c8",
                    "400": "#ab47bc",
                    "500": "#9c27b0",
                    "600": "#8e24aa",
                    "700": "#7b1fa2",
                    "800": "#6a1b9a",
                    "900": "#4a148c",
                    "hex": "#9c27b0",
                    "a100": "#ea80fc",
                    "a200": "#e040fb",
                    "a400": "#d500f9",
                    "a700": "#aa00ff"
                },
                "deepPurple": {
                    //"50": "#ede7f6",
                    "100": "#d1c4e9",
                    "200": "#b39ddb",
                    "300": "#9575cd",
                    "400": "#7e57c2",
                    "500": "#673ab7",
                    "600": "#5e35b1",
                    "700": "#512da8",
                    "800": "#4527a0",
                    "900": "#311b92",
                    "hex": "#673ab7",
                    "a100": "#b388ff",
                    "a200": "#7c4dff",
                    "a400": "#651fff",
                    "a700": "#6200ea"
                },
                "indigo": {
                    //"50": "#e8eaf6",
                    "100": "#c5cae9",
                    "200": "#9fa8da",
                    "300": "#7986cb",
                    "400": "#5c6bc0",
                    "500": "#3f51b5",
                    "600": "#3949ab",
                    "700": "#303f9f",
                    "800": "#283593",
                    "900": "#1a237e",
                    "hex": "#3f51b5",
                    "a100": "#8c9eff",
                    "a200": "#536dfe",
                    "a400": "#3d5afe",
                    "a700": "#304ffe"
                },
                "blue": {
                    ////"50": "#e3f2fd",
                    "100": "#bbdefb",
                    "200": "#90caf9",
                    "300": "#64b5f6",
                    "400": "#42a5f5",
                    "500": "#2196f3",
                    "600": "#1e88e5",
                    "700": "#1976d2",
                    "800": "#1565c0",
                    "900": "#0d47a1",
                    "hex": "#2196f3",
                    "a100": "#82b1ff",
                    "a200": "#448aff",
                    "a400": "#2979ff",
                    "a700": "#2962ff"
                },
                //"lightBlue": {
                //    //"50": "#e1f5fe",
                //    "100": "#b3e5fc",
                //    "200": "#81d4fa",
                //    "300": "#4fc3f7",
                //    "400": "#29b6f6",
                //    "500": "#03a9f4",
                //    "600": "#039be5",
                //    "700": "#0288d1",
                //    "800": "#0277bd",
                //    "900": "#01579b",
                //    "hex": "#03a9f4",
                //    "a100": "#80d8ff",
                //    "a200": "#40c4ff",
                //    "a400": "#00b0ff",
                //    "a700": "#0091ea"
                //},
                "cyan": {
                    ////"50": "#e0f7fa",
                    "100": "#b2ebf2",
                    "200": "#80deea",
                    "300": "#4dd0e1",
                    "400": "#26c6da",
                    "500": "#00bcd4",
                    "600": "#00acc1",
                    "700": "#0097a7",
                    "800": "#00838f",
                    "900": "#006064",
                    "hex": "#00bcd4",
                    "a100": "#84ffff",
                    "a200": "#18ffff",
                    "a400": "#00e5ff",
                    "a700": "#00b8d4"
                },
                "teal": {
                    ////"50": "#e0f2f1",
                    "100": "#b2dfdb",
                    "200": "#80cbc4",
                    "300": "#4db6ac",
                    "400": "#26a69a",
                    "500": "#009688",
                    "600": "#00897b",
                    "700": "#00796b",
                    "800": "#00695c",
                    "900": "#004d40",
                    "hex": "#009688",
                    "a100": "#a7ffeb",
                    "a200": "#64ffda",
                    "a400": "#1de9b6",
                    "a700": "#00bfa5"
                },
                "green": {
                    ////"50": "#e8f5e9",
                    //"100": "#c8e6c9",
                    "200": "#a5d6a7",
                    "300": "#81c784",
                    "400": "#66bb6a",
                    "500": "#4caf50",
                    "600": "#43a047",
                    "700": "#388e3c",
                    "800": "#2e7d32",
                    "900": "#1b5e20",
                    "hex": "#4caf50",
                    "a100": "#b9f6ca",
                    "a200": "#69f0ae",
                    "a400": "#00e676",
                    "a700": "#00c853"
                },
                "lightGreen": {
                    ////"50": "#f1f8e9",
                    "100": "#dcedc8",
                    "200": "#c5e1a5",
                    "300": "#aed581",
                    "400": "#9ccc65",
                    "500": "#8bc34a",
                    "600": "#7cb342",
                    "700": "#689f38",
                    "800": "#558b2f",
                    "900": "#33691e",
                    "hex": "#8bc34a",
                    //"a100": "#ccff90",
                    "a200": "#b2ff59",
                    //"a400": "#76ff03",
                    "a700": "#64dd17"
                },
                "lime": {
                    ////"50": "#f9fbe7",
                    "100": "#f0f4c3",
                    //"200": "#e6ee9c",
                    //"300": "#dce775",
                    "400": "#d4e157",
                    "500": "#cddc39",
                    "600": "#c0ca33",
                    "700": "#afb42b",
                    "800": "#9e9d24",
                    "900": "#827717",
                    "hex": "#cddc39",
                    //"a100": "#f4ff81",
                    //"a200": "#eeff41",
                    //"a400": "#c6ff00",
                    "a700": "#aeea00"
                },
                "yellow": {
                    ////"50": "#fffde7",
                    //"100": "#fff9c4",
                    //"200": "#fff59d",
                    //"300": "#fff176",
                    //"400": "#ffee58",
                    "500": "#ffeb3b",
                    "600": "#fdd835",
                    "700": "#fbc02d",
                    "800": "#f9a825",
                    "900": "#f57f17",
                    "hex": "#ffeb3b",
                    //"a100": "#ffff8d",
                    //"a200": "#ffff00",
                    "a400": "#ffea00",
                    "a700": "#ffd600"
                },
                "amber": {
                    ////"50": "#fff8e1",
                    "100": "#ffecb3",
                    "200": "#ffe082",
                    "300": "#ffd54f",
                    "400": "#ffca28",
                    "500": "#ffc107",
                    "600": "#ffb300",
                    "700": "#ffa000",
                    "800": "#ff8f00",
                    "900": "#ff6f00",
                    "hex": "#ffc107",
                    //"a100": "#ffe57f",
                    "a200": "#ffd740",
                    "a400": "#ffc400",
                    "a700": "#ffab00"
                },
                "orange": {
                    //"50": "#fff3e0",
                    "100": "#ffe0b2",
                    "200": "#ffcc80",
                    "300": "#ffb74d",
                    "400": "#ffa726",
                    "500": "#ff9800",
                    "600": "#fb8c00",
                    "700": "#f57c00",
                    "800": "#ef6c00",
                    "900": "#e65100",
                    "hex": "#ff9800",
                    "a100": "#ffd180",
                    "a200": "#ffab40",
                    "a400": "#ff9100",
                    "a700": "#ff6d00"
                },
                "deepOrange": {
                    //"50": "#fbe9e7",
                    "100": "#ffccbc",
                    "200": "#ffab91",
                    "300": "#ff8a65",
                    "400": "#ff7043",
                    "500": "#ff5722",
                    "600": "#f4511e",
                    "700": "#e64a19",
                    "800": "#d84315",
                    "900": "#bf360c",
                    "hex": "#ff5722",
                    "a100": "#ff9e80",
                    "a200": "#ff6e40",
                    "a400": "#ff3d00",
                    "a700": "#dd2c00"
                },
                "brown": {
                    ////"50": "#efebe9",
                    "100": "#d7ccc8",
                    "200": "#bcaaa4",
                    "300": "#a1887f",
                    "400": "#8d6e63",
                    "500": "#795548",
                    "600": "#6d4c41",
                    "700": "#5d4037",
                    "800": "#4e342e",
                    "900": "#3e2723",
                    "hex": "#795548"
                },
                //"grey": {
                //    "200": "#eeeeee",
                //    "300": "#e0e0e0",
                //    "400": "#bdbdbd",
                //    "500": "#9e9e9e",
                //    "600": "#757575",
                //    "700": "#616161",
                //    "800": "#424242",
                //    "900": "#212121",
                //    "hex": "#9e9e9e"
                //},
                "blueGrey": {
                    ////"50": "#eceff1",
                    "100": "#cfd8dc",
                    "200": "#b0bec5",
                    "300": "#90a4ae",
                    "400": "#78909c",
                    "500": "#607d8b",
                    "600": "#546e7a",
                    "700": "#455a64",
                    "800": "#37474f",
                    "900": "#263238",
                    "hex": "#607d8b"
                }
            }
            // pick random property
            //var property = pickRandomProperty(colors);
            var colorList = colors[pickRandomProperty(colors)];
            var newColorKey = pickRandomProperty(colorList);
            var newColor = colorList[newColorKey];
            return newColor;
        }

        function pickRandomProperty(obj) {
            var result;
            var count = 0;
            for (var prop in obj)
                if (Math.random() < 1 / ++count)
                    result = prop;
            return result;
        }
    </script>
</asp:Content>
