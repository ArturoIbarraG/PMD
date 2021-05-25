<%@ Page Language="VB" Title="Autorización de Gastos" AutoEventWireup="false" Inherits="PMD_WAS.AutorizacionLineas" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="AutorizacionLineas.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updAutorizacion" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-5">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Año:</h6>
                                    <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="true" style="width:95% !important;" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Secretaría/Instituto:</h6>
                                    <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" style="width:95% !important;" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblSecretaria" runat="server" Style="float: right; right: 4px; position: absolute;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Dependencia:</h6>
                                    <asp:DropDownList ID="ddlDependencia" runat="server" AutoPostBack="true" style="width:95% !important;" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDependencia_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblDependencia" runat="server" Style="float: right; right: 4px; position: absolute;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <h6 class="subtitle">Actividad:</h6>
                                    <asp:DropDownList ID="ddlActividad" runat="server" AutoPostBack="true" style="width:95% !important;" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="Label1" runat="server" Style="float: right; right: 4px; position: absolute;"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-md-7">
                        <h2><asp:Label ID="lblPresupuestoLeyenda" runat="server"></asp:Label></h2>
                        <table width="100%" class="table table-sm table-hover table-bordered">
                            <tr>
                                <td>FUENTE FINANCIAMIENTO</td>
                                <td>PRESUPUESTADO</td>
                                <td>CAPTURADO</td>
                                <td>DIFERENCIA</td>
                            </tr>
                            <asp:Repeater ID="rptPresupuesto" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%# String.Format("{0} - {1}", Eval("Recurso"), Eval("FuenteFinanciamiento")) %>'></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="lblPresupuestado" runat="server" Text='<%# String.Format("{0:c2}", Eval("Presupuestado")) %>'></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="lblCapturado" runat="server" Text='<%# String.Format("{0:c2}", Eval("Capturado")) %>'></asp:Label></td>
                                        <td class="text-right">
                                            <asp:Label ID="lblDiferencia" runat="server" Text='<%# String.Format("{0:c2}", Eval("Diferencia")) %>'></asp:Label></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tfoot>
                                <tr>
                                    <th class="text-right">Total:</th>
                                    <th class="text-right">
                                        <asp:Label ID="lblFooterPresupuestado" runat="server" Text="$ 0"></asp:Label>
                                    </th>
                                    <th class="text-right">
                                        <asp:Label ID="lblFooterCapturado" runat="server" Text="$ 0"></asp:Label>
                                    </th>
                                    <th class="text-right">
                                        <asp:Label ID="lblFooterDiferencia" runat="server" Text="$ 0"></asp:Label>
                                    </th>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12">
                    <br />
                    <hr />
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn  btn-secondary" Style="float: right;" Text="ACTUALIZAR" OnClick="btnActualizar_Click" /><br />
                    <br />
                    <div class="table table-responsive">
                        <asp:GridView ID="grdPresupuestos" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%" OnRowDataBound="grdPresupuestos_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="Id_SubActividad" />
                                <asp:TemplateField HeaderText="Clave gasto">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClaveGastos" runat="server" Text='<%# String.Format("{0} - {1}", Eval("Clave_Gastos"), Eval("ClaveGastos")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Sub Actividad" DataField="SubActividad" />
                                <asp:BoundField HeaderText="Evento" DataField="Evento" />
                                <asp:TemplateField HeaderText="Unidad Administrativa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnidadAdmin" runat="server" Text='<%# String.Format("{0}{1}", Eval("Id_Secretaria"), Eval("Id_Dependencia")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnFolioEvento" runat="server" Value='<%# Eval("FolioEvento") %>' />
                                        <asp:HiddenField ID="hdnIdSubActividad" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:HiddenField ID="hdnTotal" runat="server" Value='<%# Eval("Total") %>' />
                                        <asp:HiddenField ID="hdnClaveGastos" runat="server" Value='<%# Eval("Clave_Gastos") %>' />
                                        <asp:Label ID="lblTotales" runat="server" Text='<%# String.Format("{0:c2}", Eval("Total")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Funcion del Gasto" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnFuncionGasto" runat="server" Value='<%# Eval("Id_FuncionGasto") %>' />
                                        <asp:DropDownList ID="ddlFuncionGasto" runat="server" Width="150px"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="Recurso">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnRecurso" runat="server" Value='<%# Eval("Id_Recurso") %>' />
                                        <asp:DropDownList ID="ddlTipoRecurso" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoRecurso_SelectedIndexChanged"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Fuente Financiamiento" ControlStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnFF" runat="server" Value='<%# Eval("Id_FuenteFinanciamiento") %>' />
                                        <asp:DropDownList ID="ddlFuenteFinanciamiento"  CssClass="form-control select-basic-simple" Width="100%" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estatus" ControlStyle-Width="150px">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnEstatus" runat="server" Value='<%# Eval("Id_Estatus") %>' />
                                        <asp:DropDownList ID="ddlEstatus" runat="server" Width="100%" CssClass="form-control select-basic-simple">
                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Aprobado" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Reducir" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Rechazado" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
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
            <div class="row">
                <div class="col-12 text-right">
                    <h2>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label></h2>
                </div>
            </div>
            <asp:HiddenField ID="hdnLinea" runat="server" />
        </ContentTemplate>

    </asp:UpdatePanel>

    <!-- MODAL REDUCIR -->
    <div id="modalReducir" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Reducir Presupuesto</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalReducir" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p>Presupuesto Sueldo</p>
                                        <asp:TextBox ID="txtReducirSueldo" runat="server" CssClass="form-control campo_obligatorio" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Presupuesto Materiales</p>
                                        <asp:TextBox ID="txtReducirMateriales" runat="server" CssClass="form-control campo_obligatorio" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Presupuesto Vehiculos</p>
                                        <asp:TextBox ID="txtReducirVehiculos" runat="server" CssClass="form-control campo_obligatorio" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <br />
                                        <p>Comentarios:</p>
                                        <asp:TextBox ID="txtComentariosReducir" runat="server" CssClass="form-control campo_obligatorio" Width="100%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnGuardaReducir" runat="server" CssClass="btn btn-secondary" Text="Reducir" OnClick="btnGuardaReducir_Click" OnClientClick="javascript:validaCampos(this,event);" data-target="#modalReducir" />
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>
        </div>
    </div>

    <!-- MODAL RECHAZAR -->
    <div id="modalRechazar" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Rechazar Presupuesto</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updRechazar" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <p>Comentarios</p>
                                        <asp:TextBox ID="txtComentariosRechazar" runat="server" CssClass="form-control campo_obligatorio" Width="100%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:HiddenField ID="hdnSueldoRechazar" runat="server" />
                                        <asp:HiddenField ID="hdnMaterialesRechazar" runat="server" />
                                        <asp:HiddenField ID="hdnVehiculosRechazar" runat="server" />
                                        <asp:Button ID="btnRechazar" runat="server" CssClass="btn btn-secondary" Text="Rechazar" OnClick="btnRechazar_Click" OnClientClick="javascript:validaCampos(this,event);" data-target="#modalRechazar" />
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
        function muestraModalReducir() {
            $('#modalReducir').modal('show');
        }
        function ocultaModalReducir() {
            $('#modalReducir').modal('hide');
        }

        function muestraModalRechazar() {
            $('#modalRechazar').modal('show');
        }
        function ocultaModalRechazar() {
            $('#modalRechazar').modal('hide');
        }
    </script>
</asp:Content>
