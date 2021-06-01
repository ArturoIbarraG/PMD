<%@ Page Language="vb" Title="Seguimiento de Obras Públicas" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="ObraPublicaSeguimiento.aspx.vb" Inherits="PMD_WAS.ObraPublicaSeguimiento" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updObraPublicaSeguimiento" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <h4 class="title">
                    Seguimiento de Obras Públicas
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
                    <h2>Obras Públicas de Enero 2021</h2>
                    <table class="table table-bordered table-hover">
                        <tbody>
                            <tr>
                                <th>ID</th>
                                <th>Nombre</th>
                                <th>Descripción</th>
                                <th>Departamento</th>
                                <th>Concursante</th>
                                <th>Responsable</th>
                                <th>Estado</th>
                                <th>Detalles</th>
                            </tr>
                            <tr>
                                <td>1</td>
                                <td>Construcción Carretera Ecuador</td>
                                <td>Construcción de la Carretera Ecuador en el municipio de San Nicolás de los Garza.</td>
                                <td>DIRECCIÓN GENERAL</td>
                                <td>EMPRESA 1</td>
                                <td>Arturo Ibarra</td>
                                <td>En proceso</td>
                                <td>
                                    <button class="btn btn-link">Ver Detalles</button>
                                </td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>Repavimentado de Universidad</td>
                                <td>Repavimentación de la Avenida Universidad, a altura de HEB Universidad.</td>
                                <td>DIRECCIÓN GENERAL</td>
                                <td>EMPRESA 2</td>
                                <td>Arturo Ibarra</td>
                                <td>En proceso</td>
                                <td>
                                    <button class="btn btn-link">Ver Detalles</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
