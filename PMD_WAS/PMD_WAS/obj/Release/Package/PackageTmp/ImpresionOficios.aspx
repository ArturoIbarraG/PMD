<%@ Page Language="VB" AutoEventWireup="false" Title="Impresión de Oficio Presupuestal" Inherits="PMD_WAS.ImpresionOficios" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="ImpresionOficios.aspx.vb" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:UpdatePanel ID="updOficios" runat="server">
        <ContentTemplate>

            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Año:</h6>
                                    <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Secretaría/Instituto:</h6>
                                    <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" Style="width: 95% !important;" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblSecretaria" runat="server" Style="float: right; right: 4px; position: absolute;"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 col-md-6">
                                    <h6 class="subtitle">Dependencia:</h6>
                                    <asp:DropDownList ID="ddlDependencia" runat="server" Style="width: 95% !important;" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlDependencia_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Label ID="lblDependencia" runat="server" Style="float: right; right: 4px; position: absolute;"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView ID="grdOficios" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="Id_SubActividad" />
                                    <asp:TemplateField HeaderText="Clave gasto">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClaveGastos" runat="server" Text='<%# String.Format("{0} - {1}", Eval("Clave_Gastos"), Eval("ClaveGastos")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Sub Actividad" DataField="SubActividad" />
                                    <asp:TemplateField HeaderText="Unidad Administrativa">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnidadAdmin" runat="server" Text='<%# String.Format("{0}{1}", Eval("Id_Secretaria"), Eval("Id_Dependencia")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnIdSubActividad" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:HiddenField ID="hdnTotal" runat="server" Value='<%# Eval("Total") %>' />
                                            <asp:HiddenField ID="hdnClaveGastos" runat="server" Value='<%# Eval("Clave_Gastos") %>' />
                                            <asp:HiddenField ID="hdnID" runat="server" Value='<%# Eval("Id") %>' />
                                            <asp:Label ID="lblTotales" runat="server" Text='<%# String.Format("{0:c2}", Eval("Total")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="FUNCTION DEL GASTO" DataField="FuncionGasto" Visible="false" />
                                    <asp:BoundField HeaderText="RECURSO" DataField="Recurso" />
                                    <asp:BoundField HeaderText="FUENTE FINANCIAMIENTO" DataField="FuenteFinanciamiento" />

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnqMostrarPDF" runat="server" Text="Ver oficio" CommandArgument='<%# Eval("Id") %>' OnCommand="lnqMostrarPDF_Command"></asp:LinkButton>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="modalIframeURL" class="modal modal-big fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <iframe id="iframeModalURL" width="100%" height="600px"></iframe>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-primary right" data-dismiss="modal">Cerrar</a>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function openModalIFrame(url) {
            console.log(url);
            document.getElementById('iframeModalURL').src = url;
            $('#modalIframeURL').modal('show');
        }

        function muestraMensajeError(error) {
            console.log(error);
            alert(error);
        }
    </script>
</asp:Content>
