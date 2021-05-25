<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="AdminPOA.aspx.vb" Inherits="PMD_WAS.AdminPOA" %>

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
                        <h6 class="subtitle">Programa:</h6>
                        <asp:DropDownList ID="ddlPrograma" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlPrograma_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Componentes:</h6>
                        <asp:DropDownList ID="ddlComponente" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlComponente_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregaPOA" runat="server" CssClass="btn btn-primary" Text="Agregar Actividad" OnClick="btnAgregaPOA_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdPOA" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server" CssClass="btn btn-link" Style="font-size: 24px; font-weight: 600; padding: 0; margin: 0;" ToolTip="Ver las Sub actividades" Text="+" OnCommand="btnVer_Command" CommandName="ver" CommandArgument='<%# Eval("id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Id Programa" DataField="idPrograma" />
                                <asp:BoundField HeaderText="Programa" DataField="programa" />
                                <asp:BoundField HeaderText="Id Componente" DataField="idComponente" />
                                <asp:BoundField HeaderText="Componente" DataField="componente" />
                                <asp:BoundField HeaderText="ID" DataField="idActividad" />
                                <asp:BoundField HeaderText="Actividad" DataField="actividad" />
                                <asp:BoundField HeaderText="Clasificacion" DataField="Clasificacion" />
                                <asp:BoundField HeaderText="Unidad medida" DataField="Unidad_Medida" />
                                <asp:BoundField HeaderText="Indicador" DataField="Indicador" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-link" Text="Editar" OnCommand="btnVer_Command" CommandArgument='<%# Eval("id") %>' CommandName="editar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAgregarSubactividad" runat="server" CssClass="btn btn-link" Style="font-size: 15px; font-weight: 600; padding: 0; margin: 0;" ToolTip="Agregar nueva Sub actividad" OnCommand="btnVer_Command" Text="Nueva Sub actividades" CommandName="add" CommandArgument='<%# Eval("id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0" HeaderStyle-Width="0">
                                    <ItemTemplate>
                                        <tr>
                                            <td></td>
                                            <td colspan="11">
                                                <asp:GridView ID="grdSubActividades" runat="server" CssClass="table table-striped table-small table-detail" Width="100%" Visible="false" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Id Subactividad" DataField="idSubActividad" />
                                                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                                        <asp:BoundField HeaderText="Indicador" DataField="Indicador" />
                                                        <asp:BoundField HeaderText="Fuente verificacion" DataField="fuenteVerificacion" />
                                                        <asp:BoundField HeaderText="Unidad medida" DataField="unidadMedida" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-link" Text="Editar" OnCommand="btnVer_Command" CommandArgument='<%# Eval("id") %>' CommandName="editarSA" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="container-fluid">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <h6>No hay sub actividades agregadas</h6>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
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

    <div id="modalAgregaPOA" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">POA</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAddPOA" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Programa:</h6>
                                        <asp:DropDownList ID="ddlProgramaModal" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlProgramaModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Componentes:</h6>
                                        <asp:DropDownList ID="ddlComponenteModal" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlComponente_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Id Actividad:</h6>
                                        <asp:TextBox ID="txtIdActividad" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Nombre actividad:</h6>
                                        <asp:TextBox ID="txtNombreActividad" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Clasificacion:</h6>
                                        <asp:TextBox ID="txtClasificacion" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Unidad medida:</h6>
                                        <asp:TextBox ID="txtUnidadMedida" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Indicador:</h6>
                                        <asp:TextBox ID="txtIndicador" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarPOA" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarPOA_Click" />
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

    <div id="modalAgregaSubActividad" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Sub actividad</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAddSubActividad" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Id Actividad:</h6>
                                        <asp:Label ID="lblIdActividad" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Id Sub actividad:</h6>
                                        <asp:TextBox ID="txtIdSubActividad" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Nombre sub actividad:</h6>
                                        <asp:TextBox ID="txtNombreSubActividad" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Indicador:</h6>
                                        <asp:TextBox ID="txtIndicadorSA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 col-md-6">
                                        <h6 class="subtitle">Unidad medida:</h6>
                                        <asp:TextBox ID="txtUnidadMedidaSA" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardaSubActividad" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardaSubActividad_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnIdSubActividad" runat="server" />
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

        function ocultaModalPOA() {
            $('#modalAgregaPOA').modal('hide');
        }

        function abreModalSubActividad() {
            $('#modalAgregaSubActividad').modal('show');
        }

        function ocultaModalSubActividad() {
            $('#modalAgregaSubActividad').modal('hide');
        }
    </script>
</asp:Content>
