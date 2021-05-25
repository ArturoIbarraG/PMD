<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Site.master" Inherits="PMD_WAS.SeguimientoLinea" Codebehind="SeguimientoLinea.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--ESTOS 2 SCRIPTS SON PARA HACER LA BUSQUEDA EN EL GRID YA CARGADO, Y EN EL GRID SOLO SE MANDA LLAMAR LA CLASE--%>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/JScriptBusquedaEnGrid.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/bootstrap.js" type="text/javascript"></script>
    <script src="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Scripts/jquery-ui-1.12.1.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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
    <asp:ScriptManager ID="SCRIPTM" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="updTareas" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Seguimiento de las Tareas"></asp:Label>
                        <br />
                        <br />
                    </div>
                </div>
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
                        <asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <hr />

                <div class="row">
                    <div class="col-12" style="overflow: auto;">
                        <asp:Label ID="lblNombreTareaActual" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblMesTareaActual" runat="server" Visible="false"></asp:Label>
                        <asp:GridView ID="grdTareas" runat="server" CssClass="table table-bordered tabla-pmd" ClientIDMode="Static" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Tipo Tarea" DataField="Tipo_Tarea" />
                                <asp:BoundField HeaderText="Nombre Tarea" DataField="Nombre_Tarea" />
                                <asp:TemplateField HeaderText="Avance">
                                    <ItemTemplate>
                                        <div class="progress">
                                            <div class="progress-bar" role="progressbar" style="width: <%# Eval("avance") %>%;" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDetalle" runat="server" CssClass="btn btn-link " Text="VER DETALLE" OnClick="btnDetalle_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalSeguimientoTarea" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Seguimiento Tarea</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalTarea" runat="server">
                        <ContentTemplate>
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
        });

        function beginReq(sender, args) { }

        function endReq(sender, args) {

        }

        function modalSeguimientoTarea() {
            $('#modalSeguimientoTarea').modal('show');
        }

    </script>
</asp:Content>
