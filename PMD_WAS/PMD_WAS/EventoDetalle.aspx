<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" Inherits="PMD_WAS.EventoDetalle" CodeBehind="EventoDetalle.aspx.vb" %>

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

        .labelIndicador {
            min-height: 50px;
            height: auto;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updEvento" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdnFolio" runat="server" />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4" style="border-right: 1px solid #EEE;">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <h2>Actividad</h2>
                                    <b>
                                        <asp:Label ID="lblActividad" runat="server" CssClass="labelIndicador"></asp:Label>
                                    </b>
                                </div>
                                <div class="col-12">
                                    <h2>Sub actividad</h2>
                                    <b>
                                        <asp:Label ID="lblSubActividad" runat="server" CssClass="labelIndicador"></asp:Label>
                                    </b>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-8">
                        <div class="container-fluid">

                            <div class="row">
                                <div class="col-3">
                                    <asp:Panel ID="pnlEvento" runat="server">
                                        <h2>Evento </h2>
                                        <b>
                                            <asp:Label ID="lblEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                                <div class="col-3">
                                    <asp:Panel ID="pnlFecha" runat="server">
                                        <h2>Fecha </h2>
                                        <b>
                                            <asp:Label ID="lblFecha" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                                <div class="col-3">
                                    <asp:Panel ID="pnlAforo" runat="server">
                                        <h2>Aforo</h2>
                                        <b>
                                            <asp:Label ID="lblAforo" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                                <div class="col-3">
                                    <asp:Panel ID="pnlAireLibre" runat="server">
                                        <h2>Tipo evento</h2>
                                        <b>
                                            <asp:Label ID="lblTipoEvento" runat="server" CssClass="labelIndicador"></asp:Label></b>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h2>Descripcion</h2>
                                    <b>
                                        <asp:Label ID="lblDescripcion" runat="server" CssClass="labelIndicador"></asp:Label>
                                    </b>
                                </div>
                            </div>

                        </div>
                    </div>

                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <h2>Presupuesto </h2>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregarPresupuesto" runat="server" Visible="false" CssClass="btn  btn-secondary" Text="Agregar presupuesto" OnClick="btnAgregarPresupuesto_Click" /><br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdDesglocePresupuesto" runat="server" CssClass="table table-hover table-bordered" Width="100%" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="SECRETARIA" DataField="Secretaria" />
                                    <asp:BoundField HeaderText="DEPENDENCIA" DataField="Direccion" />
                                    <asp:BoundField HeaderText="ID LINEA" DataField="Id_Linea" />
                                    <asp:BoundField HeaderText="LINEA" DataField="Descr_estrategia" />
                                    <asp:BoundField HeaderText="ID SUB ACTIVIDAD" DataField="Id_Subactividad" />
                                    <asp:BoundField HeaderText="SUBACTIVIDAD" DataField="Subactividad" />
                                    <asp:BoundField HeaderText="ID CLAVE GASTOS" DataField="Clave_Gastos" />
                                    <asp:BoundField HeaderText="CLAVE GASTOS" DataField="ClaveGastos" />
                                    <asp:TemplateField HeaderText="Presupuesto">
                                        <ItemTemplate>
                                            <b style="text-align: right;">
                                                <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("Total")) %>'></asp:Label>
                                            </b>
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
                <hr />
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Label ID="lblTotalPresupuesto" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalAsignaSubActividad" class="modal fade modal-small" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Asignar Sub Actividad</h2>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalActividades" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <b>
                                            <h3>El evento no esta ligado con ninguna Sub Actividad, favor de asignar una antes de continuar.</h3>
                                        </b>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-4">
                                        <p>Actividad</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlActividad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <p>Sub Actividad</p>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlSubActividad" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-center">
                                        <asp:Button ID="btnAsignaLinea" runat="server" CssClass="btn  btn-secondary" Text="Asignar Sub Actividad" OnClick="btnAsignaLinea_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalAgregaMateriales" class="modal faade modal-big" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Materiales a utilizar</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAgregaMateriales" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12 col-md-3">
                                        <asp:TextBox ID="txtBuscarRapida" runat="server" ClientIDMode="Static" CssClass="form-control" onkeyup="javascript:onSearchGrid();" placeholder="Busqueda rápida..."></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="grdMaterialesEvento" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                            <Columns>
                                                <asp:BoundField DataField="Clave_Gastos" HeaderText="Clave Gastos" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnIdMaterial" runat="server" Value='<%# Eval("Id_Material") %>' />
                                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <h3>No hay materiales agregados</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <!-- EVENTOS PARA COMUNICACION -->
                                        <asp:GridView ID="grdMaterialesComunicacion" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                            <Columns>
                                                <asp:BoundField DataField="Clave_Gastos" HeaderText="Clave Gastos" />
                                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnIdMaterial" runat="server" Value='<%# Eval("Id") %>' />
                                                        <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <h3>No hay materiales agregados</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnGuardaEvento" runat="server" CssClass="btn  btn-secondary" Text="Guardar" OnClick="btnGuardaEvento_Click" />
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
            onSearchGrid();
        }

        function muestraModalAsignaLinea() {
            $('#modalAsignaSubActividad').modal('show');
        }

        function ocultaModalAsignaLinea() {
            $('#modalAsignaSubActividad').modal('hide');
        }

        function muestraModalMateriales() {
            $('#modalAgregaMateriales').modal('show');
        }

        function ocultaModalMateriales() {
            $('#modalAgregaMateriales').modal('hide');
        }

        function onSearchGrid() {
            var value = $('#txtBuscarRapida').val().toLowerCase();
            $("#grdMaterialesEvento > tbody > tr").filter(function () { $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1) });
            $("#grdMaterialesEvento > tbody > tr:eq(0)").show();
        }
    </script>

</asp:Content>
