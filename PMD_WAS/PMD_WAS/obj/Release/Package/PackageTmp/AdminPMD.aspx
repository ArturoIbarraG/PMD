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
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaría/Instituto:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Direcciones:</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregaPMD" runat="server" CssClass="btn btn-primary" Text="Agregar PMD" OnClick="btnAgregaPMD_Click" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdPMD" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                <asp:BoundField DataField="Año" HeaderText="Año" />
                                <asp:BoundField DataField="Secretaria" HeaderText="Secretaria" />
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                <asp:BoundField DataField="idProg" HeaderText="Id Prog" />
                                <asp:BoundField DataField="programa" HeaderText="Programa" />
                                <asp:BoundField DataField="idComp" HeaderText="Id Comp" />
                                <asp:BoundField DataField="objetivo" HeaderText="Componente" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-link" Text="Editar" OnCommand="btnEditar_Command" CommandArgument='<%# Eval("ID") %>' CommandName="editar" />
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
    
    <div id="modalAgregaPMD" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">PMD</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="upAddPMD" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <p class="simple">Secretaria</p>
                                        <asp:DropDownList ID="ddlSecretariaModal" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlSecretariaModal_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-6">
                                        <p class="simple">Dirección</p>
                                        <asp:DropDownList ID="ddlDireccionModal" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                                    </div>
                                </div>
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
                                        <asp:Button ID="btnGuardarPMD" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarPMD_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnID" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function abreModalPMD() {
            $('#modalAgregaPMD').modal('show');
        }

        function ocultaModalPMD() {
            $('#modalAgregaPMD').modal('hide');
        }
    </script>
</asp:Content>

