<%@ Page Language="VB" AutoEventWireup="false" Title="Usuarios" Inherits="PMD_WAS.Usuarios" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="Usuarios.aspx.vb" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="estilo_requerimiento.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <style type="text/css">
        .container-border {
            border: 1px solid #AAA;
            padding: 10px;
        }

        .labelIndicador {
            min-height: 50px;
            height: auto;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }

        h5 {
            font-size: 1.4em;
            margin-bottom: 6px;
        }

        #MainContent_txtFechaInsta_CalendarExtender_container {
            z-index: 9999;
        }
    </style>
    <asp:UpdatePanel ID="updUsuarios" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaria</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Direccion</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnAgregaUsuario" runat="server" CssClass="btn btn-secondary" Style="float: right;" Text="Agregar usuario" OnClick="btnAgregaUsuario_Click" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-4">
                        <asp:TextBox ID="txtBuscar" runat="server" Width="100%" placeholder="Buscar..." OnTextChanged="txtBuscar_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblClaveEmpleado" runat="server" Visible="false" Text="0"></asp:Label>
                        <asp:GridView ID="grdUsuarios" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Clave empleado" DataField="Clave_empl" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombr_empl" />
                                <asp:BoundField HeaderText="Secretaria" DataField="Nombr_secretaria" />
                                <asp:BoundField HeaderText="Dependencia" DataField="Nombr_direccion" />
                                <asp:BoundField HeaderText="Correo" DataField="Correo" />
                                <asp:BoundField HeaderText="Puesto" DataField="Puesto" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" ForeColor="Blue" Text="EDITAR" CssClass="btn btn-link " CommandName="editar" OnCommand="btn_Command" CommandArgument='<%# Eval("Clave_empl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" ForeColor="Red" Text="ELIMINAR" CssClass="btn btn-link  " CommandName="eliminar" OnCommand="btn_Command" CommandArgument='<%# Eval("Clave_empl") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Correo" Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnEmpleado" runat="server" Value='<%# Eval("Clave_empl") %>' />
                                        <asp:TextBox ID="txtCorreo" runat="server" Text='<%# Eval("Correo") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h3>No hay información disponible</h3>
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

    <div id="modalAgregarUsuario" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Usuario</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updInfoEmpleado" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-6">
                                        <h6 class="subtitle">Clave empleado</h6>
                                        <asp:TextBox ID="txtClaveEmpleado" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <h6 class="subtitle">Nombre empleado</h6>
                                        <asp:TextBox ID="txtNombreEmpleado" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-6">
                                        <h6 class="subtitle">Correo</h6>
                                        <asp:TextBox ID="txtCorreoModal" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-6">
                                        <h6 class="subtitle">Puesto</h6>
                                        <asp:DropDownList ID="ddlPuestoModal" runat="server" Width="100%" CssClass="form-control select-basic-simple">
                                            <asp:ListItem Text="Secreatario" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Director" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Coordinador" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Empleado" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Enlace administrativo" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Coordinador administrativo" Value="5"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel ID="pnlContrasenia" runat="server" CssClass="col-6">
                                        <p>Contraseña</p>
                                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control"></asp:TextBox>
                                    </asp:Panel>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <h4>Direcciones</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-5">
                                        <h6 class="subtitle">Secreatria</h6>
                                        <asp:DropDownList ID="ddlSecretariaModal" runat="server" Width="100%" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretariaModal_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="col-5">
                                        <h6 class="subtitle">Direccion</h6>
                                        <asp:DropDownList ID="ddlDireccionModal" runat="server" Width="100%" CssClass="form-control select-basic-simple"></asp:DropDownList>
                                    </div>
                                    <div class="col-2">
                                        <h6 class="subtitle" style="visibility: hidden;">Direccion</h6>
                                        <asp:Button ID="btnAgregar" runat="server" Text="Agreagr direccion" OnClick="btnAgregar_Click" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="grdDirecciones" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="Secretaria" DataField="secretaria" />
                                                <asp:BoundField HeaderText="Direccion" DataField="direccion" />
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnQuitar" runat="server" Text="Quitar" OnCommand="btnQuitar_Command" CommandArgument='<%# Eval("idDireccion") %>' CssClass="btn btn-link" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <h3>MENU</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Repeater ID="rptMenuPrincipal" runat="server" OnItemDataBound="rptMenuPrincipal_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="col-6">
                                                <br />
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-8">
                                                            <h6><%# Eval("nombre").ToString() %></h6>
                                                        </div>
                                                        <div class="col-4">
                                                            <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("activo") %>' />
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                                <hr />
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <asp:Repeater ID="rptSubMenu" runat="server" OnItemDataBound="rptSubMenu_ItemDataBound">
                                                            <ItemTemplate>
                                                                <div class="col-8">
                                                                    <label><%# Eval("nombre").ToString() %></label>
                                                                </div>
                                                                <div class="col-4">
                                                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                                                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("activo") %>' />
                                                                </div>
                                                                <div class="col-12">
                                                                    <div>
                                                                        <div class="row">
                                                                            <asp:Repeater ID="rptSubSubMenu" runat="server">
                                                                                <ItemTemplate>
                                                                                    <div class="col-8">
                                                                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("id") %>' />
                                                                                        <label style="padding-left: 30px;">- <%# Eval("nombre").ToString() %></label>
                                                                                    </div>
                                                                                    <div class="col-4">
                                                                                        <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("activo") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-6">
                                        <a class="btn btn-primary" href="#" data-dismiss="modal">Cancelar</a>
                                    </div>
                                    <div class="col-6 text-right">
                                        <asp:Button ID="btnGuardarUsuario" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarUsuario_Click" />
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
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        $(document).ready(function () {

            $('.select-basic-simple').select2();
        });

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {

            $('.select-basic-simple').select2();
        }

        function muestraModalUsuario() {
            $('#modalAgregarUsuario').modal('show');
        }
        function ocultaModalUsuario() {
            $('#modalAgregarUsuario').modal('hide');
        }

        function muestraMensajeUsuarioExistente() {
            swal({
                type: 'info',
                title: 'Usuario ya registrado',
                text: 'La clave de el empleado que se intenta guarda ya existe, favor de valdiar.',
                confirmButtonText: 'Aceptar'
            });
        }
    </script>
</asp:Content>
