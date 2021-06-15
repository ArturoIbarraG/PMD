<%@ Page EnableEventValidation="false" Language="vb" Title="Autorización de Obras Públicas" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="ObraPublicaAutorizacion.aspx.vb" Inherits="PMD_WAS.ObraPublicaAutorizacion" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updObraPublicaAutorizacion" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <h4 class="title">
                    Autorización de Obras Públicas
                </h4>
                <div class="container-body col-12">
                    <div class="container">
                        <div class="row">
                            <div class="col-12 col-md-5">
                                <h6 class="subtitle">Secretaría/Instituto:</h6>
                                <select class="form-control select-basic-simple">
                                    <option disabled selected>Seleccione la Secretaría</option>
                                    <option>SECRETARÍA DE FINANZAS Y TESORERÍA</option>
                                </select>
                            </div>
                            <div class="col-12 col-md-5">
                                <h6 class="subtitle">Direcciones:</h6>
                                <select class="form-control select-basic-simple">
                                    <option disabled selected>Seleccione la Dirección</option>
                                    <option>DIRECCIÓN DE INGRESOS</option>
                                </select>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-md-5">
                                <h6 class="subtitle">Mes:</h6>
                                <select class="form-control select-basic-simple">
                                    <option disabled selected>Seleccione un Mes</option>
                                    <option>ENERO</option>
                                    <option>FEBRERO</option>
                                </select>
                            </div>
                            <div class="col-12 col-md-5">
                                <h6 class="subtitle">Año:</h6>
                                <select class="form-control select-basic-simple">
                                    <option disabled selected>Seleccione el Año</option>
                                    <option>2020</option>
                                    <option>2021</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="gridObrasPublicas" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                <Columns>
                                    <asp:BoundField HeaderText="ID" DataField="opID" />
                                    <asp:BoundField HeaderText="Nombre" DataField="opNombre" />
                                    <asp:BoundField HeaderText="Descripción" DataField="opDescripcion" />
                                    <asp:BoundField HeaderText="Ubicación" DataField="opUbicacion" />
                                    <asp:BoundField HeaderText="Origen de Fondos" DataField="opOrigenFondos" />
                                    <asp:BoundField HeaderText="Estatus" DataField="opEstatus" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnAutorizarRechazar" runat="server" CssClass="btn btn-link" Text="Autorizar/Rechazar" OnCommand="btnAutorizarRechazar_Command" CommandArgument='<%# Eval("opID") %>' CommandName="autorizarRechazar"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <h6>No hay Obras Públicas pendientes de autorización.</h6>
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

    <div id="modalAutorizarOP" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Autorizar Obra Pública</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAutorizarOP" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <h3>Detalles de la Obra Pública:</h3>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Nombre:</h6>
                                        <asp:TextBox ID="txtOPNombre" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Descripción:</h6>
                                        <asp:TextBox ID="txtOPDescripcion" runat="server" CssClass="form-control" Rows="2" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Origen de Fondos:</h6>
                                        <asp:TextBox ID="txtOPOrigenFondos" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Monto de Asignación:</h6>
                                        <asp:TextBox ID="txtOPMontoAsignacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Ubicación:</h6>
                                        <asp:TextBox ID="txtOPUbicacion" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <h3>Campos restantes (Llenar para poder autorizar):</h3>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Número de contrato:</h6>
                                        <asp:TextBox ID="txtOPNumeroContrato" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Contratista:</h6>
                                        <asp:TextBox ID="txtOPContratista" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Tipo de Adjudicación:</h6>
                                        <asp:TextBox ID="txtOPTipoAdjudicacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Número de Adjudicación:</h6>
                                        <asp:TextBox ID="txtOPNumeroAdjudicacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Monto Total:</h6>
                                        <asp:TextBox ID="txtOPMontoTotal" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Monto de Anticipo:</h6>
                                        <asp:TextBox ID="txtOPMontoAnticipo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6>Fecha de Inicio del Contrato:</h6>
                                        <asp:TextBox ID="txtOPFechaInicio" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6>Fecha de Terminación del Contrato:</h6>
                                        <asp:TextBox ID="txtOPFechaTerminacion" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6>Fecha de Firma del Contrato:</h6>
                                        <asp:TextBox ID="txtOPFechaFirma" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6>Fechas de Estimaciones:</h6>
                                        <asp:TextBox ID="txtOPFechaEstimaciones" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-3">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btnRechazarOP" runat="server" CssClass="btn btn-danger" Text="Rechazar"/>
                                    </div>
                                    <div class="col-3">
                                        <asp:Button ID="btnAutorizarOP" runat="server" CssClass="btn btn-success" Text="Autorizar" OnCommand="btnAutorizarOP_Command"/>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnIdActividad" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abreModalOP() {
            $('#modalAutorizarOP').modal('show');
        }

        function ocultaModalOP() {
            $('#modalAutorizarOP').modal('hide');
        }
    </script>

</asp:Content>
