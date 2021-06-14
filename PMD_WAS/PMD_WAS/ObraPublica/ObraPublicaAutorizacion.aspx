<%@ Page Language="vb" Title="Autorización de Obras Públicas" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="ObraPublicaAutorizacion.aspx.vb" Inherits="PMD_WAS.ObraPublicaAutorizacion" %>

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
                                            <asp:Button ID="btnAutorizar" runat="server" CssClass="btn btn-link" Text="Autorizar/Rechazar"/>
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
</asp:Content>
