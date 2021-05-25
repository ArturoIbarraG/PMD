<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.Eventos2015_CatalogoImprevistos" Codebehind="CatalogoImprevistos.aspx.vb" %>

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
    <asp:UpdatePanel ID="updImprevistosGrid" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-5">
                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar..." OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="col-3"></div>
                    <div class="col-4 text-right">
                        <asp:Button ID="btnAgregarImprevisto" runat="server" CssClass="btn btn-secondary" Text="Agregar Imprevisto" OnClick="btnAgregarImprevisto_Click" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblIdImprevisto" runat="server" Visible="false"></asp:Label>
                        <asp:GridView ID="grdImprevistos" runat="server" Width="100%" CssClass="table table-bordered table-hover" CellPadding="4" DataKeyNames="id" AllowPaging="true" OnPageIndexChanging="grdImprevistos_PageIndexChanging"
                            GridLines="None" TabIndex="6" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="id" />
                                <asp:BoundField HeaderText="Requisición" DataField="nombre" />
                                <asp:BoundField HeaderText="Clave de Gastos" DataField="claveGastos" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" ForeColor="Blue" Text="EDITAR" CssClass="btn btn-link " CommandName="editar" OnCommand="btn_Command" CommandArgument='<%# Eval("id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" ForeColor="Red" Text="ELIMINAR" CssClass="btn btn-link  " CommandName="eliminar" OnCommand="btn_Command" CommandArgument='<%# Eval("id") %>' />
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


    <div id="modalAgregarImprevistos" class="modal fade modal-small" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Imprevistos</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updImprevistos" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Nombre</h6>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h6 class="subtitle">Clave Gastos</h6>
                                        <asp:DropDownList ID="ddlClaveGastos" runat="server" Width="100%" CssClass="select-basic-simple form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-secondary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardaImprevisto" runat="server" CssClass="btn btn-secondary" Text="Guardar" OnClick="btnGuardaImprevisto_Click" />
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

        function abreModalImprevisto() {
            $('#modalAgregarImprevistos').modal('show');
        }

        function ocultaModalImprevisto() {
            $('#modalAgregarImprevistos').modal('hide');
        }

    </script>
</asp:Content>
