<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="DesglocePresupuesto.aspx.vb" Inherits="PMD_WAS.DesglocePresupuesto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updDesglocePresupuesto" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Administración:</h6>
                        <asp:DropDownList ID="ddlAdmon" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlAdmon_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Año</h6>
                        <asp:DropDownList ID="ddlAnio" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Secretaría/Instituto:</h6>
                        <asp:DropDownList ID="ddlSecretaria" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlSecretaria_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Direcciones:</h6>
                        <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Mes</h6>
                        <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-control select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                            <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                            <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-6">
                        <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar..." CssClass="form-control" Width="100%" onkeyup="javascript:buscaRequerimiento(this,event);"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdDesglocePresupuesto" runat="server" ClientIDMode="Static" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Clave gastos" DataField="claveGastos" />
                                <asp:BoundField HeaderText="Nombre" DataField="Descripcion" />
                                <asp:TemplateField HeaderText="Presupuesto autorizado" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresAutorizado" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoAutorizado")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Presupuesto comprometido" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresComprometido" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoComprometido")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Presupuesto reservado" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresReservado" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoReservado")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Presupuesto libre" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresLibre" runat="server" Text='<%# String.Format("{0:c2}", Eval("presupuestoLibre")) %>'></asp:Label>
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

    <script type="text/javascript">
        function buscaRequerimiento(o, e) {
            var valor = $(o).val().toLowerCase();
            if (valor != '') {
                $('#grdDesglocePresupuesto tbody tr').each(function (i) {
                    if (i > 0) {
                        var html = $(this).html().toLowerCase();
                        if (html.indexOf(valor) > 0) {
                            $(this).show();
                        }
                        else {
                            $(this).hide();
                        }
                    }

                });
                //$('#grdRequerimientos tbody tr:eq(0)').show();
            }
            else {
                $('#grdDesglocePresupuesto tbody tr').show();
            }
        }
    </script>
</asp:Content>
