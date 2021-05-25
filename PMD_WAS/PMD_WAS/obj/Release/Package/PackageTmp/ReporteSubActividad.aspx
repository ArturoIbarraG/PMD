<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.ReporteSubActividad" MasterPageFile="~/MasterGlobal.master" Codebehind="ReporteSubActividad.aspx.vb" %>

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
   
    <asp:UpdatePanel ID="updTareas" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Reporte Sub Actividad"></asp:Label>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <span style="font-size: medium;">Administración:</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-2"></div>
                    <div class="col-2">
                        <span style="font-size: medium;">Año</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlAnio" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <span style="font-size: medium;">Secretaría/Instituto:</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-2"></div>
                    <div class="col-2">
                        <span style="font-size: medium;">Direcciones:</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <span style="font-size: medium;">Línea</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlLinea" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-2"></div>
                    <div class="col-2">
                        <span style="font-size: medium;">Sub Actividad</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlSubActividad" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <span style="font-size: medium;">Clave Gastos</span>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList ID="ddlClaveGastos" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-6">
                        <asp:Button ID="btnExportar" runat="server" CssClass="btn  btn-secondary" Text="Exportar Excel" OnClick="btnExportar_Click" />
                    </div>
                    <div class="col-6 text-right">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="btn  btn-secondary" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <div class="table-responsive">

                            <asp:GridView ID="grdReporte" runat="server" CssClass="table table-sm table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="ClasificacionAdministrativa" HeaderText="Clasificación Administrativa" />
                                    <asp:BoundField DataField="ClasificacionFuncional" HeaderText="Clasificación Funcional" />
                                    <asp:BoundField DataField="ClasificacionProgramatica" HeaderText="Clasificación Programatica" />
                                    <asp:BoundField DataField="ClasificacionObjetoGasto" HeaderText="Clasificación Objeto Gasto" />
                                    <asp:BoundField DataField="ClasificacionTipoGasto" HeaderText="Clasificación Tipo Gasto" />
                                    <asp:BoundField DataField="Secretaria" HeaderText="Secretaria" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Dependencia" />
                                    <asp:BoundField DataField="Programa" HeaderText="Programa" />
                                    <asp:BoundField DataField="Componente" HeaderText="Componente" />
                                    <asp:BoundField DataField="Actividad" HeaderText="Actividad" />
                                    <asp:BoundField DataField="ClavePresupuestal" HeaderText="Clave Presupuestal" />
                                    <asp:BoundField DataField="IdLinea" HeaderText="Id Linea" />
                                    <asp:BoundField DataField="Linea" HeaderText="Linea" />
                                    <asp:BoundField DataField="IndicadorLinea" HeaderText="Indicador (Línea)" />
                                    <asp:BoundField DataField="Unidad_Medida" HeaderText="Unidad Medida (Línea)" />
                                    <asp:BoundField DataField="Formula" HeaderText="Fórmula (Línea)" />
                                    <asp:BoundField DataField="Id_SubActividad" HeaderText="Id SubActividad" />
                                    <asp:BoundField DataField="SubActividad" HeaderText="Sub Actividad" />
                                    <asp:TemplateField HeaderText="Clave Gastos">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClaveGastos" runat="server" Text='<%# String.Format("{0} - {1}", Eval("Clave_Gastos"), Eval("ClaveGastos")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="Indicador" HeaderText="Indicador (SubActividad)" />
                                    <asp:BoundField DataField="Frecuencia" HeaderText="Frecuencia (SubActividad)" />
                                    <asp:BoundField DataField="V1" HeaderText="V1" />
                                    <asp:BoundField DataField="V2" HeaderText="V2" />
                                    <asp:BoundField DataField="Año" HeaderText="Año" />
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:c2}" />
                                   <%-- <asp:BoundField DataField="Enero" HeaderText="Enero" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Febrero" HeaderText="Febrero" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Marzo" HeaderText="Marzo" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Abril" HeaderText="Abril" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Mayo" HeaderText="Mayo" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Junio" HeaderText="Junio" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Julio" HeaderText="Julio" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Agosto" HeaderText="Agosto" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Septiembre" HeaderText="Septiembre" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Octubre" HeaderText="Octubre" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Noviembre" HeaderText="Noviembre" DataFormatString="{0:c2}" />
                                    <asp:BoundField DataField="Diciembre" HeaderText="Diciembre" DataFormatString="{0:c2}" />--%>
                                    <asp:BoundField DataField="FuncionGasto" HeaderText="Funcion de Gasto" />
                                    <asp:BoundField DataField="Recurso" HeaderText="Recurso" />
                                    <asp:BoundField DataField="FuenteFinanciamiento" HeaderText="Fuente Financiamiento" />
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportar" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>
