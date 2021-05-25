<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" CodeBehind="Bitacora.aspx.vb" Inherits="PMD_WAS.Bitacora" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updBitacora" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-6 col-md-4">
                        <h6 class="subtitle">Fecha inicio:</h6>
                        <asp:TextBox ID="txtfechaIni" runat="server" CssClass="form-control"
                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                            TabIndex="11"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfechaIni_CalendarExtender" runat="server" TargetControlID="txtfechaIni"
                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="txtfechaIni" ForeColor="red"></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-6 col-md-4">
                        <h6 class="subtitle">Fecha fin:</h6>
                        <asp:TextBox ID="txtfechaFin" runat="server" CssClass="form-control"
                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                            TabIndex="11"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfechaFin"
                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd"></asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtfechaFin" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" Text="Buscar" />
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdBitacora" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Secretaría" DataField="Secretaria" />
                                <asp:BoundField HeaderText="Dirección" DataField="Direccion" />
                                <asp:BoundField HeaderText="Nómina" DataField="Nomina" />
                                <asp:BoundField HeaderText="Empleado" DataField="Empleado" />
                                <asp:BoundField HeaderText="Puesto" DataField="Puesto" />
                                <asp:BoundField HeaderText="Fecha" DataField="FechaLog" />
                                <asp:BoundField HeaderText="Log" DataField="Log" />
                                <asp:BoundField HeaderText="IP" DataField="IP" />
                                <asp:BoundField HeaderText="Máquina" DataField="Maquina" />
                            </Columns>
                            <EmptyDataTemplate>
                                <h5>No hay información que mostrar</h5>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
