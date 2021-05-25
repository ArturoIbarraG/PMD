<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="AdminPMD.aspx.vb" Inherits="PMD_WAS.AdminPMD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updAdminPMD" runat="server">
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
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregarPrograma" runat="server" CssClass="btn btn-primary" Text="Agregar Programa" OnClick="btnAgregarPrograma_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdPMD" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server" CssClass="btn btn-link" Style="font-size: 24px; font-weight: 600; padding: 0; margin: 0;" ToolTip="Ver los componentes" Text="+" OnCommand="btnEditar_Command" CommandName="ver" CommandArgument='<%# Eval("id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Año" HeaderText="Año" />
                                <asp:BoundField DataField="idProg" HeaderText="Id Programa" />
                                <asp:BoundField DataField="programa" HeaderText="Programa" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-link" Text="Editar" OnCommand="btnEditar_Command" CommandArgument='<%# Eval("id") %>' CommandName="editarPrograma" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAgregarComponente" runat="server" CssClass="btn btn-link" Style="font-size: 15px; font-weight: 600; padding: 0; margin: 0;" ToolTip="Agregar componente" OnCommand="btnEditar_Command" Text="Nuevo componente" CommandName="nuevoComponente" CommandArgument='<%# String.Format("{0}", Eval("id")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="0" HeaderStyle-Width="0">
                                    <ItemTemplate>
                                        <tr>
                                            <td></td>
                                            <td colspan="9">
                                                <asp:GridView ID="grdComponentes" runat="server" CssClass="table table-striped table-small table-detail" Width="100%" Visible="false" AutoGenerateColumns="false">
                                                    <Columns>
                                                      <asp:BoundField DataField="Año" HeaderText="Año" />
                                                        <asp:BoundField DataField="idComp" HeaderText="Id Componente" />
                                                        <asp:BoundField DataField="componente" HeaderText="Componente" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-link" Text="Editar" OnCommand="btnEditar_Command" CommandArgument='<%# Eval("id") %>' CommandName="editarComponente" />
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

    <div id="modalAgregaPrograma" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Programa</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAddPMD" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-2">
                                        <p class="simple">ID Programa</p>
                                        <asp:TextBox ID="txtIdPrograma" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-10">
                                        <p class="simple">Descripcion Programa</p>
                                        <asp:TextBox ID="txtDescrPrograma" runat="server" CssClass="form-control" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarPrograma" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarPrograma_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnIDPrograma" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>


      <div id="modalAgregaComponente" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Componetes</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                 <div class="row">
                                    <div class="col-2">
                                        <p class="simple">ID Componente</p>
                                        <asp:TextBox ID="txtIdComponente" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                                    </div>
                                    <div class="col-10">
                                        <p class="simple">Descripcion Componente</p>
                                        <asp:TextBox ID="txtDescrComponente" runat="server" CssClass="form-control" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarComponetne" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarComponetne_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnIdComponente" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

   

    <script type="text/javascript">
        function abreModalPrograma() {
            $('#modalAgregaPrograma').modal('show');
        }

        function ocultaModalPrograma() {
            $('#modalAgregaPrograma').modal('hide');
        }

        function abreModalComponente() {
            $('#modalAgregaComponente').modal('show');
        }

        function ocultaModalComponente() {
            $('#modalAgregaComponente').modal('hide');
        }
    </script>
</asp:Content>

