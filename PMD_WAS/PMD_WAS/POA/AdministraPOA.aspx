<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="AdministraPOA.aspx.vb" Inherits="PMD_WAS.AdministraPOA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updAdminPOA" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaria:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Dirección:</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary" Text="Agregar POA" OnClick="btnAgregar_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <br />
                        <asp:GridView ID="grdPOA" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                            <Columns>
                                <asp:BoundField HeaderText="Id Programa" DataField="idPrograma" />
                                <asp:BoundField HeaderText="Programa" DataField="programa" />
                                <asp:BoundField HeaderText="Id Componente" DataField="idComponente" />
                                <asp:BoundField HeaderText="Componente" DataField="componente" />
                                <asp:BoundField HeaderText="Id Actividad" DataField="idActividad" />
                                <asp:BoundField HeaderText="Actividad" DataField="actividad" />
                                <asp:BoundField HeaderText="Id Subactividad" DataField="idSubActividad" />
                                <asp:BoundField HeaderText="Subactividad" DataField="Subactividad" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnQuitar" runat="server" CssClass="btn btn-link text-danger" Text="Quitar" OnCommand="btnQuitar_Command" CommandArgument='<%# Eval("id") %>' CommandName="quitar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h6>No hay eventos agregados</h6>
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

    <div id="modalAgregaPOA" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Administrar POA</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updAgregaPOA" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <p class="simple">Secretaria</p>
                                        <asp:DropDownList ID="ddlSecretariaModal" runat="server" CssClass="select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretariaModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-6">
                                        <p class="simple">Direccion</p>
                                        <asp:DropDownList ID="ddlDireccionModal" runat="server" CssClass="select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccionModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <p class="simple">Programa</p>
                                        <asp:DropDownList ID="ddlProgramaModal" runat="server" CssClass="select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlProgramaModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-6">
                                        <p class="simple">Componente</p>
                                        <asp:DropDownList ID="ddlComponenteModal" runat="server" CssClass="select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlComponenteModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <p class="simple">Actividad</p>
                                        <asp:DropDownList ID="ddlActividadModal" runat="server" CssClass="select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlActividadModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-6">
                                        <p class="simple">Sub actividad</p>
                                        <asp:DropDownList ID="ddlSubActividadModal" runat="server" CssClass="select-basic-simple" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarPOA" runat="server" CssClass="btn btn-primary" OnClick="btnGuardarPOA_Click" Text="Guardar POA" />
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
        function abreModalPOA() {
            $('#modalAgregaPOA').modal('show');
        }

        function cierraModalPOA() {
            $('#modalAgregaPOA').modal('hide');
        }
    </script>
</asp:Content>
