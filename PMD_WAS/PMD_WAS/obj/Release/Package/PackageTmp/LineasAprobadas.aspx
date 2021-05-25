<%@ Page Language="VB" AutoEventWireup="false" Inherits="PMD_WAS.LineasAprobadas" MasterPageFile="~/Site.master" Codebehind="LineasAprobadas.aspx.vb" %>

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
    <asp:ScriptManager ID="SCRIPTM" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="updAutorizacion" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-center">
                        <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Líneas Aprobadas"></asp:Label>
                        <br />
                        <br />
                    </div>
                </div>
                <div class="row">
                    <div class="col-5 text-right">
                        <span style="font-size: medium;">Año:</span>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlAnio" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"></asp:DropDownList>
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
                        <span style="font-size: medium;">Dependencia:</span>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlDependencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDependencia_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <br />
                        <hr />
                        <asp:GridView ID="grdPresupuestos" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="ID" />
                                <asp:BoundField HeaderText="Línea" DataField="Descr_linea" />
                                <asp:TemplateField HeaderText="Presupuesto Sueldo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSueldo" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoSueldo")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Presupuesto Materiales">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMateriales" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoMaterial")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Presupuesto Vehículos">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVehiculos" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoVehiculos")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotales" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoSueldo") + Eval("presupuestoMaterial") + Eval("presupuestoVehiculos")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                            </Columns>
                            <EmptyDataTemplate>
                                <h3 style="text-align:center;">No hay presupuestos aprobados</h3>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnLinea" runat="server" />
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
