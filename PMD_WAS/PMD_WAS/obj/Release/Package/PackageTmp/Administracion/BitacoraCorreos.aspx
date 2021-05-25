<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="BitacoraCorreos.aspx.vb" Inherits="PMD_WAS.BitacoraCorreos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updBitacoraCorreo" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-8">
                        <p class="simple">Fecha de consulta:</p>
                        <div class="row">
                            <div class="col-12 col-lg-6">
                                <small>Desde:</small><br>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtfechaIni" runat="server" CssClass="form-control"
                                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                                            TabIndex="11"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtfechaIni_CalendarExtender" runat="server" TargetControlID="txtfechaIni"
                                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtfechaIni" ForeColor="red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-12 col-lg-6">
                                <small>hasta</small><br>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtfechafin" runat="server" CssClass="form-control"
                                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                                            TabIndex="11"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfechafin"
                                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                            ControlToValidate="txtfechafin" ForeColor="red"></asp:RequiredFieldValidator>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                    </div>
                    <div class="col-12 col-md-4">
                        <p class="simple">Estatus</p>
                        <small style="visibility: hidden;">hasta</small><br>
                        <asp:DropDownList ID="ddlEstatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Todos"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Enviados"></asp:ListItem>
                            <asp:ListItem Value="2" Text="No enviados"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-12 text-right">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-sm btn-primary" OnClick="btnBuscar_Click" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdBitacoraCorreo" runat="server" CssClass="table table-hover table-bordered" Width="100%" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                <asp:BoundField HeaderText="Destinatario" DataField="destinatario" DataFormatString="{0:n0}" />
                                <asp:BoundField HeaderText="CC" DataField="CC" />
                                <asp:BoundField HeaderText="Fecha" DataField="fecha" DataFormatString="{0:MM/dd/yyyy}" />
                                <asp:BoundField HeaderText="Asunto" DataField="asunto" />
                                <asp:BoundField HeaderText="Mensaje" DataField="mensaje" />
                                <asp:BoundField HeaderText="Enviado" DataField="enviado"  />
                            </Columns>
                            <EmptyDataTemplate>
                                <h4>No hay información que mostrar.</h4>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>
