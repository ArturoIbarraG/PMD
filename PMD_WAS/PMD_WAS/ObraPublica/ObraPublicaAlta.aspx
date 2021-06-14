<%@ Page Language="vb" Title="Alta de Obra Pública" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="ObraPublicaAlta.aspx.vb" Inherits="PMD_WAS.ObraPublicaAlta" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updObraPublicaAlta" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <h4 class="title">
                    Alta de Obra Pública
                </h4>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            <h5>Información del presupuesto:</h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">
                            <div class="border text-center">
                                <h6>PRESUPUESTO AUTORIZADO</h6>
                                <h4>
                                    <span>$6,640,000.00</span>
                                </h4>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="border text-center">
                                <h6>PRESUPUESTO PRE-COMPROMETIDO</h6>
                                <h4>
                                    <span>$33,264.46</span>
                                </h4>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="border text-center">
                                <h6>PRESUPUESTO COMPROMETIDO</h6>
                                <h4>
                                    <span>$12,013.40</span>
                                </h4>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="border text-center">
                                <h6>PRESUPUESTO DISPONIBLE</h6>
                                <h4>
                                    <span>$4,094,722.14</span>
                                </h4>
                            </div>
                        </div>
                    </div>
                </div>
                <hr />
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6>Nombre de Obra:</h6>
                            <asp:TextBox ID="txtOPNombre" CssClass="form-control" runat="server">NombreTemp</asp:TextBox>
                        </div>
                        <div class="col-12 col-md-5">
                            <h6>Origen de Fondos:</h6>
                            <asp:TextBox ID="txtOPOrigenFondos" CssClass="form-control" runat="server">Fondos Centralizados</asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6>Descripción de Obra:</h6>
                            <asp:TextBox ID="txtOPDescripcion" CssClass="form-control" runat="server" Rows="2" TextMode="MultiLine">Construcción de Andador</asp:TextBox>
                        </div>
                        <div class="col-12 col-md-5">
                            <h6>Ubicación de la Obra:</h6>
                            <asp:TextBox ID="txtOPUbicacion" CssClass="form-control" runat="server">Calle Popocatepetl</asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6>Monto de Asignación Aprobada:</h6>
                            <asp:TextBox ID="txtOPMontoAsignacion" onkeypress="numberOnly(event);" CssClass="form-control" runat="server" TextMode="Number">5195431.39</asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-12 col-md-6">
                            <h6>Fecha de firma del Contrato:</h6>
                            <asp:TextBox ID="txtOPFechaFirma" CssClass="form-control" TextMode="Date" runat="server">2021-06-10</asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 col-md-6">
                            <h6>Adjuntar Contrato:</h6>
                            <input type="file" class="form-control-file"/>
                        </div>
                    </div>--%>
                    <hr/>
                    <asp:Button ID="btnProcesar" style="width: 300px;" CssClass="btn btn-primary float-right mx-auto" Text="Procesar" runat="server"/>
                   <%-- <input type="submit" style="width: 300px;" class="" value="Procesar" id="btnProcesar"/>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/Scripts/ObraPublicaScripts/ObraPublicaScript.js" type="text/javascript"></script>
</asp:Content>
