﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.Master" CodeBehind="Solicitudes.aspx.vb" Inherits="PMD_WAS.Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updRequisiciones" runat="server">
        <ContentTemplate>
             <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h5>
                            <asp:Label ID="lblInformacionRequisicion" runat="server" Style="font-size: 1.2rem;" Text="Solicitudes correspondientes al mes de <b>Enero</b> del <b>2021</b> de la <b>Administración 2018-2021</b>"></asp:Label>
                        </h5>
                        <asp:HiddenField ID="hdnAdmon" runat="server" />
                        <asp:HiddenField ID="hdnAnio" runat="server" />
                        <asp:HiddenField ID="hdnMes" runat="server" />
                    </div>
                </div>
                <br />
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
                        <h6 class="subtitle">Actividad:</h6>
                        <asp:DropDownList ID="ddlActividad" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged" TabIndex="3">
                        </asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Sub Actividad:</h6>
                        <asp:DropDownList ID="ddlSubActividad" runat="server" TabIndex="3" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlSubActividad_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12">
                        <asp:GridView ID="grdSolicitudes" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Núm. Adquisicion" DataField="folio" />
                                <asp:BoundField HeaderText="Proveedor" DataField="proveedor" />

                                <asp:TemplateField HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%# String.Format("{0:n2}", Eval("cantidad")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Unidad" DataField="unidad" />
                                <asp:TemplateField HeaderText="Precio unitario">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%# String.Format("{0:c2}", Eval("precioUnitario")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IVA">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIVA" runat="server" Text='<%# String.Format("{0:n0} %", Eval("iva")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("total")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Estatus" DataField="estatus" />
                                <asp:BoundField HeaderText="Empleado" DataField="usuarioAutorizado" />
                                <asp:TemplateField HeaderText="Fecha autorización">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFecha" runat="server" Text='<%# String.Format("{0:dd/MMMM/yyyy HH:mm tt}", Eval("fechaAutorizado")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <h5>No hay información disponible</h5>
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
</asp:Content>
