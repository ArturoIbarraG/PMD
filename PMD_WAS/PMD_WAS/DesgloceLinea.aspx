<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="PMD_WAS.DesgloceLinea" Codebehind="DesgloceLinea.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--ESTOS 2 SCRIPTS SON PARA HACER LA BUSQUEDA EN EL GRID YA CARGADO, Y EN EL GRID SOLO SE MANDA LLAMAR LA CLASE--%>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/JScriptBusquedaEnGrid.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.12.1.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <style type="text/css">
        select {
            max-width: 100%;
            width: 350px;
        }

        .table-sm {
            font-size: 11px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="SCRIPTM" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updTareas" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Desgloce de las Líneas"></asp:Label>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-5">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-5 text-right">
                                    <span style="font-size: medium;">Administración:</span>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-5 text-right">
                                    <span style="font-size: medium;">Año</span>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlAnio" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-5 text-right">
                                    <span style="font-size: medium;">Secretaría/Instituto:</span>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-5 text-right">
                                    <span style="font-size: medium;">Direcciones:</span>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-5 text-right">
                                    <span style="font-size: medium;">Línea</span>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlLinea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-7">
                        <h3>Resumen de la Línea</h3>
                        <br />
                        <asp:GridView ID="grdLineaResumen" runat="server" CssClass="table table-hover table-bordered table-sm" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="ENERO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnero" runat="server" Text='<%# String.Format("{0:c2}", Eval("ENERO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FEBRERO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFebrero" runat="server" Text='<%# String.Format("{0:c2}", Eval("FEBRERO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MARZO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMARZO" runat="server" Text='<%# String.Format("{0:c2}", Eval("MARZO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ABRIL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblABRIL" runat="server" Text='<%# String.Format("{0:c2}", Eval("ABRIL")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MAYO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMAYO" runat="server" Text='<%# String.Format("{0:c2}", Eval("MAYO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JUNIO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJUNIO" runat="server" Text='<%# String.Format("{0:c2}", Eval("JUNIO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="JULIO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJULIO" runat="server" Text='<%# String.Format("{0:c2}", Eval("JULIO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AGOSTO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAGOSTO" runat="server" Text='<%# String.Format("{0:c2}", Eval("AGOSTO")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SEPTIEMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSEPTIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("SEPTIEMBRE")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OCTUBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOCTUBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("OCTUBRE")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOVIEMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOVIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("NOVIEMBRE")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DICIEMBRE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDICIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("DICIEMBRE")) %>'></asp:Label>
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
                <hr />
                <div class="row">
                    <div class="col-12">
                        <asp:HiddenField ID="hdnNombreTarea" runat="server" />
                        <asp:GridView ID="grdTareas" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAsignar" runat="server" CssClass="btn  btn-link" OnCommand="btnAsignar_Command" CommandArgument='<%# Eval("Nombre_Tarea") %>' Text="Registrar" OnClientClick="javascript:muestraAsignaCoordinador();" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Clave_Gasto" HeaderText="Clave Gasto" />
                                <asp:BoundField DataField="Nombre_Tarea" HeaderText="Nombre Tarea" />
                                <asp:BoundField DataField="Fecha_Compromiso_Coord" HeaderText="Fecha compromiso" />
                                <asp:BoundField DataField="Meta_Coord" HeaderText="Meta" />
                                <asp:BoundField DataField="Asignado2" HeaderText="Asignado" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="modalAsignaTarea" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Información de la Tarea</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalTarea" runat="server">
                        <ContentTemplate>
                            <div id="containerDesgloceLineas" class="container-fluid">
                                <div class="row">
                                    <div class="col-3">
                                        <p>Meta</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtMeta" runat="server" Width="100%" onkeypress="javascript:validaNumeros(this, event);" CssClass="campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Asignado a</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:DropDownList ID="ddlPersona" runat="server"  CssClass="campo_obligatorio">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Fecha compromiso</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtFechaCompromiso" Width="100%" runat="server" CssClass="datepicker campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Comentarios</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtComentarios" runat="server" Width="100%" TextMode="MultiLine" Rows="3" CssClass="campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn  btn-secondary" Text="Guardar" OnClick="btnAgregar_Click" OnClientClick="javascript:validaCampos(this,event);" data-target="#containerDesgloceLineas" />
                                    </div>
                                </div>
                            </div>
                            <asp:LinkButton ID="lnkCargaInfoLinea" runat="server" OnClick="lnkCargaInfoLinea_Click" CssClass="hidden"></asp:LinkButton>
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
            //
            $(".datepicker").datepicker({ "dateFormat": "dd/mm/yy" });
        }

        function muestraAsignaCoordinador() {
            $('#modalAsignaTarea').modal('show');
        }

        function ocultaAsignadoCoordinador() {
            $('#modalAsignaTarea').modal('hide');
        }
    </script>
</asp:Content>
