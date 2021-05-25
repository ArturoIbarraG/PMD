<%@ Page Language="VB" Title="Asignar Recursos Metas" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.AsignarRecursosMetas" Codebehind="AsignarRecursosMetas.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #modalIframeURL .modal-header {
            padding: 2px 8px;
        }

        #modalIframeURL .modal-body {
            padding: 0;
        }

        #modalIframeURL .modal-footer {
            padding: 8px;
            border: none;
        }

        .alert p {
            font-size: 16px;
        }

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

        .labelIndicador {
            min-height: 60px;
            display: block;
            border: 1px solid #DDD;
            margin-bottom: 15px;
            margin-top: 6px;
            padding: 7px;
        }

        .tabla-metas input {
            width: 50px;
        }

        .tabla-metas thead {
        }

            .tabla-metas thead tr th {
            }

        .tabla-metas tbody tr td {
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="updTareas" runat="server">
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
                        <asp:DropDownList ID="ddlDireccion" runat="server" AutoPostBack="true" CssClass="form-control select-basic-simple" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-md-6">
                        <h6 class="subtitle">Actividad</h6>
                        <asp:DropDownList ID="ddlLinea" runat="server" CssClass="form-control select-basic-simple" AutoPostBack="true" OnSelectedIndexChanged="ddlLinea_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-12 col-md-6 text-right">
                        <br />
                        <button id="btnMetasLinea" runat="server" class="btn btn-secondary " data-target="#modalMetasLinea" data-toggle="modal">Fijar metas</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 text-center">
                        <br />
                        <asp:Button ID="btnReinicia" runat="server" ClientIDMode="Static" OnClick="btnReinicia_Click" Style="display: none;" />
                        <input type="button" value="Agregar Sub Actividad" style="display: none;" class="btn btn-secondary" onclick="javascript: validaLineaSeleccionada(this, event);" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:Panel ID="pnlAceptado" runat="server" CssClass="alert alert-success" role="alert" Visible="false">
                            <h4 class="alert-heading">¡Presupuesto Aceptado!</h4>
                            <p>
                                El presupuesto para la Línea ha sido <b>Aceptado</b> con la siguiente información: <b>
                                    <asp:Label ID="lblComentariosAceptado" runat="server" Text=""></asp:Label></b>.
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="pnlRechazado" runat="server" CssClass="alert alert-danger" role="alert" Visible="false">
                            <h4 class="alert-heading">¡Presupuesto Rechazado!</h4>
                            <p>
                                El presupuesto para la Línea ha sido <b>Rechazado</b> con la siguiente información: <b>
                                    <asp:Label ID="lblComentariosRechazado" runat="server" Text=""></asp:Label></b>.
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="pnlReducido" runat="server" CssClass="alert alert-warning" role="alert" Visible="false">
                            <h4 class="alert-heading">¡Presupuesto Reducido!</h4>
                            <p>
                                El presupuesto para la Línea ha sido <b>Reducido</b> con la siguiente información: <b>
                                    <asp:Label ID="lblComentariosReducido" runat="server" Text=""></asp:Label></b>.
                            </p>
                        </asp:Panel>
                    </div>
                </div>

            </div>
            <hr />
            <div class="container-fluid">
                <div class="row">
                    <div class="col-4">
                        <asp:Panel ID="pnlProgramaLinea" runat="server" Visible="false">
                            <h2>Programa presupuestal </h2>
                            <b>
                                <asp:Label ID="lblProgramaLinea" runat="server" CssClass="labelIndicador"></asp:Label></b>
                        </asp:Panel>
                    </div>
                    <div class="col-4">
                        <asp:Panel ID="pnlComponenteLinea" runat="server" Visible="false">
                            <h2>Componente </h2>
                            <b>
                                <asp:Label ID="lblComponenteLinea" runat="server" CssClass="labelIndicador"></asp:Label></b>
                        </asp:Panel>
                    </div>
                    <div class="col-4">
                        <asp:Panel ID="pnlIndicadorLinea" runat="server" Visible="false">
                            <h2>Indicador</h2>
                            <b>
                                <asp:Label ID="lblIndicadorLinea" runat="server" CssClass="labelIndicador"></asp:Label></b>
                        </asp:Panel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <asp:Label ID="lblNombreTareaActual" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblMesTareaActual" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblIdSubActividad" runat="server" Visible="false"></asp:Label>
                        <div class="table table-responsive">
                            <asp:GridView ID="grdTareas" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Sub actividad" DataField="Nombre_Tarea" />
                                    <asp:BoundField HeaderText="Indicador" DataField="Indicador" />
                                    <asp:TemplateField HeaderText="TOTAL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# String.Format("{0:c2}", Eval("Total")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ene.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnero" runat="server" Text='<%# String.Format("{0:c2}", Eval("ENERO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Feb.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFebrero" runat="server" Text='<%# String.Format("{0:c2}", Eval("FEBRERO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mar.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMARZO" runat="server" Text='<%# String.Format("{0:c2}", Eval("MARZO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Abr.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblABRIL" runat="server" Text='<%# String.Format("{0:c2}", Eval("ABRIL")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="May.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMAYO" runat="server" Text='<%# String.Format("{0:c2}", Eval("MAYO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jun.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJUNIO" runat="server" Text='<%# String.Format("{0:c2}", Eval("JUNIO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Jul.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJULIO" runat="server" Text='<%# String.Format("{0:c2}", Eval("JULIO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ago.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAGOSTO" runat="server" Text='<%# String.Format("{0:c2}", Eval("AGOSTO")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sep.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSEPTIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("SEPTIEMBRE")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Oct.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOCTUBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("OCTUBRE")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nov.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNOVIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("NOVIEMBRE")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dic.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDICIEMBRE" runat="server" Text='<%# String.Format("{0:c2}", Eval("DICIEMBRE")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div class="dropdown">
                                                <button class="btn btn-secondary  dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    Asignar Recursos
                                                </button>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                    <asp:LinkButton ID="lnkBeneficiados" runat="server" CssClass="dropdown-item" Visible="false" CommandName="beneficiados" CommandArgument='<%#String.Format("{0}|{1}|{2}", Eval("Id_Subactividad"), 1, Eval("Nombre_Tarea")) %>' OnCommand="lnkModals_Command" Text="Beneficiados"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkEvento" runat="server" CssClass="dropdown-item" CommandName="evento" CommandArgument='<%#String.Format("{0}|{1}|{2}", Eval("Id_Subactividad"), 1, Eval("Nombre_Tarea")) %>' OnCommand="lnkModals_Command" Text="Evento" Visible="false"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkMaterialesEnero" runat="server" CssClass="dropdown-item" CommandName="materiales" CommandArgument='<%#String.Format("{0}|{1}|{2}", Eval("Id_Subactividad"), 1, Eval("Nombre_Tarea")) %>' OnCommand="lnkModals_Command" Text="Materiales, servicios y obras"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkPersonasEnero" runat="server" Visible="false" CssClass="dropdown-item" CommandName="personas" CommandArgument='<%#String.Format("{0}|{1}|{2}", Eval("Id_Subactividad"), 1, Eval("Nombre_Tarea")) %>' OnCommand="lnkModals_Command" Text="Personas"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkEventos" runat="server" CssClass="dropdown-item" CommandName="evento" CommandArgument='<%#String.Format("{0}|{1}|{2}|{3}", Eval("Id_Subactividad"), Eval("Id_Linea"), Eval("Nombre_Tarea"), Eval("SubActividad")) %>' OnCommand="lnkModals_Command" Text="Eventos"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkAsignaMetas" runat="server" CssClass="dropdown-item" CommandName="meta" CommandArgument='<%#String.Format("{0}|{1}|{2}", Eval("Id_Subactividad"), Eval("Nombre_Tarea"), Eval("Nombre_Tarea")) %>' OnCommand="lnkModals_Command" Text="Metas"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkVehiculosEnero" runat="server" Visible="false" CssClass="dropdown-item" CommandName="vehiculos" CommandArgument='<%#String.Format("{0}|{1}", Eval("Id_Subactividad"), 1) %>' OnCommand="lnkModals_Command" Text="Vehículos"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-12">
                                                <h6>No hay información disponible</h6>
                                            </div>
                                        </div>
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnSecretaria" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnDireccion" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnActividad" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnSubActividad" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnAdministracion" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnAnio" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnSubActividadNombre" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--    MODAL FIJAR METAS ACTIVIDAD  -->
    <div id="modalMetasLinea" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Fijar metas</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updFijarMetas" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblTituloMetaLineas" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 table-responsive">
                                        <table class="tabla-metas table table-sm table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="2">Indicador</th>
                                                    <th>Enero</th>
                                                    <th>Febrero</th>
                                                    <th>Marzo</th>
                                                    <th>Abril</th>
                                                    <th>Mayo</th>
                                                    <th>Junio</th>
                                                    <th>Julio</th>
                                                    <th>Agosto</th>
                                                    <th>Septiembre</th>
                                                    <th>Octubre</th>
                                                    <th>Noviembre</th>
                                                    <th>Diciembre</th>
                                                    <th>TOTAL</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="simple">
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblIndicadorMeta" runat="server" CssClass="display: block; width: 250px;"></asp:Label></b>
                                                    </td>
                                                    <td>
                                                        <b>Meta</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta1" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta2" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta3" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta4" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta5" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta6" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta7" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta8" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta9" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta10" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta11" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMeta12" runat="server" CssClass="meta" onblur="javascript:sumaMetas(this,event);"></asp:TextBox></td>
                                                    <td class="right">
                                                        <asp:Label ID="lblTotalMetas" runat="server" ClientIDMode="Static"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <b>BENEFICIADOS</b>
                                                    </td>
                                                    <td>
                                                        <b>Hombres</b>
                                                        <hr />
                                                        <b>Mujeres</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH1" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM1" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH2" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM2" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH3" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM3" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH4" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM4" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH5" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM5" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH6" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM6" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH7" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM7" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH8" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM8" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH9" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM9" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH10" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM10" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH11" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM11" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenH12" runat="server" CssClass="beneficiados" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiados(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenM12" runat="server" CssClass="beneficiadas" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadas(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBenHTotal" runat="server" ClientIDMode="Static"></asp:Label>
                                                        <hr />
                                                        <asp:Label ID="lblBenMTotal" runat="server" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <br />
                                        <asp:Button ID="btnGuardaMetas" runat="server" CssClass="btn btn-secondary " Text="Guardar cambios" OnClick="btnGuardaMetas_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!-- FIJAR METAS SUB ACTIVIDAD -->
    <div id="modalMetasSubActividad" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Fijar metas</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblTituloSubActividad" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 table-responsive">
                                        <table class="tabla-metas table table-sm table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="2">Indicador</th>
                                                    <th>Enero</th>
                                                    <th>Febrero</th>
                                                    <th>Marzo</th>
                                                    <th>Abril</th>
                                                    <th>Mayo</th>
                                                    <th>Junio</th>
                                                    <th>Julio</th>
                                                    <th>Agosto</th>
                                                    <th>Septiembre</th>
                                                    <th>Octubre</th>
                                                    <th>Noviembre</th>
                                                    <th>Diciembre</th>
                                                    <th>TOTAL</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="simple">
                                                    <td>
                                                        <b>
                                                            <asp:Label ID="lblIndicadorSA" runat="server" CssClass="display: block; width: 250px;"></asp:Label></b>
                                                    </td>
                                                    <td>
                                                        <b>Meta</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA1" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA2" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA3" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA4" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA5" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA6" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA7" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA8" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA9" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA10" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA11" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtMetaSA12" runat="server" CssClass="metaSA" onblur="javascript:sumaMetasSA(this,event);"></asp:TextBox></td>
                                                    <td class="right">
                                                        <asp:Label ID="lblTotalMetasSA" runat="server" ClientIDMode="Static"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <b>BENEFICIADOS</b>
                                                    </td>
                                                    <td>
                                                        <b>Hombres</b>
                                                        <hr />
                                                        <b>Mujeres</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA1" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA1" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA2" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA2" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA3" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA3" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA4" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA4" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA5" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA5" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA6" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA6" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA7" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA7" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA8" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA8" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA9" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA9" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA10" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA10" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA11" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA11" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBenHSA12" runat="server" CssClass="beneficiadosSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadosSA(this,event);"></asp:TextBox>
                                                        <hr />
                                                        <asp:TextBox ID="txtBenMSA12" runat="server" CssClass="beneficiadasSA" onkeypress="javascript:validaNumeros(this, event);" onblur="javascript:sumaBeneficiadasSA(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBenHSATotal" runat="server" ClientIDMode="Static"></asp:Label>
                                                        <hr />
                                                        <asp:Label ID="lblBenMSATotal" runat="server" ClientIDMode="Static"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <br />
                                        <asp:Button ID="btnGuardaMetaSubActividad" runat="server" CssClass="btn btn-secondary " Text="Guardar cambios" OnClick="btnGuardaMetaSubActividad_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!-- MODAL AGREGA EVENTO-->
    <div id="modalEventos" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Listado de eventos</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEvento" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-10">
                                        <asp:Label ID="lblTituloEvento" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-2 text-right">
                                        <a href="#" class="btn btn-link" onclick='javascript:return agregaNuevoEvento(<%# Eval("folio") %>);'>Nuevo evento</a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdEventosActuales" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                                <Columns>
                                                    <asp:BoundField DataField="folio" HeaderText="Folio" />
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                                    <asp:BoundField DataField="lugar" HeaderText="Lugar" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href="#" class="btn btn-link" onclick='javascript:return muestraFichaEvento(<%# Eval("folio") %>);'>Ver ficha</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <a href="#" class="btn btn-link" onclick='javascript:return muestraRequerimientoEvento(<%# Eval("folio") %>);'>Ver requerimientos</a>
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
                                <div class="row">
                                    <div class="col-12 hidden">
                                        <asp:Button ID="btnRecargaEventos" runat="server" OnClick="btnRecargaEventos_Click" />
                                        <asp:Label ID="lblIdEvento" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%--  <div id="modalEventos" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Agregar Evento</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updEvento" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblTituloEvento" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <a id="muestraEditarEvento" data-toggle="collapse" data-target="#agregarEvento" class="collapse-action collapsed" href="#">
                                            <h4>
                                                <i class="fas fa-caret-right collapse"></i>
                                                <i class="fas fa-caret-down normal"></i>
                                                Nuevo Evento
                                            </h4>
                                        </a>
                                        <div id="agregarEvento" class="container-fluid collapse">
                                            <br />
                                            <div class="row">
                                                <div class="col-3">
                                                    <p>Nombre</p>
                                                </div>
                                                <div class="col-9">
                                                    <asp:TextBox ID="txtNombreEvento" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <p>
                                                        Frecuencia del evento:
                                                    </p>
                                                </div>
                                                <div class="col-9">
                                                    <asp:DropDownList ID="ddlFrecuencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFrecuencia_SelectedIndexChanged" CssClass="form-control select-basic-simple">
                                                        <asp:ListItem Text="Una vez" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Semanal" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Mensual" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3"></div>
                                                <div class="col-9">
                                                    <asp:Panel ID="pnlUnaVez" runat="server">
                                                        <div class="">
                                                            <div class="row">
                                                                <div class="col-12">
                                                                    <label>Por favor seleciona la fecha del evento</label>
                                                                    <asp:TextBox ID="txtFechaEvento" runat="server" CssClass="datepicker" Width="100%"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlSemanal" runat="server">
                                                        <label>Por favor selecciona los dias por semana que se llevara a cabo el evento</label>
                                                        <div class="">
                                                            <div class="row">
                                                                <div class="col-6">
                                                                    <label>Fecha inicio:</label>
                                                                    <asp:TextBox ID="txtFechaInicioSemanal" runat="server" CssClass="datepicker" Width="100%"></asp:TextBox>
                                                                </div>
                                                                <div class="col-6">
                                                                    <label>Fecha fin:</label>
                                                                    <asp:TextBox ID="txtFechaFinSemanal" runat="server" CssClass="datepicker" Width="100%"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <br />
                                                            <div class="row">
                                                                <div class="col-12 list-group-horizontal">
                                                                    <asp:CheckBox ID="chkLunes" runat="server" Width="14%" Text="Lunes" />
                                                                    <asp:CheckBox ID="chkMartes" runat="server" Width="14%" Text="Martes" />
                                                                    <asp:CheckBox ID="chkMiercoles" runat="server" Width="14%" Text="Miércoles" />
                                                                    <asp:CheckBox ID="chkJueves" runat="server" Width="14%" Text="Jueves" />
                                                                    <asp:CheckBox ID="chkViernes" runat="server" Width="14%" Text="Viernes" />
                                                                    <asp:CheckBox ID="chkSabado" runat="server" Width="14%" Text="Sábado" />
                                                                    <asp:CheckBox ID="chkDomingo" runat="server" Width="14%" Text="Domingo" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlMensual" runat="server">
                                                        <label>Por favor selecciona el día de el mes en el que se llevará a cabo el evento</label>
                                                        <div class="">
                                                            <div class="row">
                                                                <div class="col-1">
                                                                    <p>Enero</p>
                                                                    <asp:DropDownList ID="ddlMensualEnero" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>FEBRERO</p>
                                                                    <asp:DropDownList ID="ddlMensualFebrero" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>MARZO</p>
                                                                    <asp:DropDownList ID="ddlMensualMarzo" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>ABRIL</p>
                                                                    <asp:DropDownList ID="ddlMensualAbril" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>MAYO</p>
                                                                    <asp:DropDownList ID="ddlMensualMayo" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>JUNIO</p>
                                                                    <asp:DropDownList ID="ddlMensualJunio" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>JULIO</p>
                                                                    <asp:DropDownList ID="ddlMensualJulio" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>AGOSTO</p>
                                                                    <asp:DropDownList ID="ddlMensualAgosto" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>SEPTIEMBRE</p>
                                                                    <asp:DropDownList ID="ddlMensualSeptiembre" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>Octubre</p>
                                                                    <asp:DropDownList ID="ddlMensualOctubre" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>NOVIEMBRE</p>
                                                                    <asp:DropDownList ID="ddlMensualNoviembre" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-1">
                                                                    <p>DICIEMBRE</p>
                                                                    <asp:DropDownList ID="ddlMensualDiciembre" runat="server">
                                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                                        <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-3">
                                                    <p>Tipo de ubicación del evento</p>
                                                </div>
                                                <div class="col-9">
                                                    <asp:DropDownList ID="ddlTipoEvento" runat="server" Width="100%">
                                                        <asp:ListItem Value="1" Text="Aire libre"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Cerrado"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <p>Tipo de evento</p>
                                                </div>
                                                <div class="col-9">
                                                    <asp:DropDownList ID="ddlTipoEvento2" runat="server">
                                                        <asp:ListItem Value="1" Text="Público"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Interno"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Capacitación"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-3">
                                                    <p>Aforo estimado</p>
                                                </div>
                                                <div class="col-9">
                                                    <asp:TextBox ID="txtAforoEstimado" runat="server" Width="100%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-12">
                                                    <p>Materiales a utilizar</p>
                                                    <asp:LinkButton ID="lnkRecargaMaterialesEvento" runat="server" OnClick="lnkRecargaMaterialesEvento_Click" CssClass="right" Text="Recargar materiales"></asp:LinkButton>
                                                    <asp:GridView ID="grdMaterialesEvento" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                                        <Columns>
                                                            <asp:BoundField DataField="Clave_Gastos" HeaderText="Clave Gastos" />
                                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                            <asp:TemplateField HeaderText="Cantidad">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdnIdMaterial" runat="server" Value='<%# Eval("Id_Material") %>' />
                                                                    <asp:TextBox ID="txtCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>
                                                            <div class="container-fluid">
                                                                <div class="row">
                                                                    <div class="col-12">
                                                                        <h3>No hay materiales agregados</h3>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="row">
                                                <div class="col-6">
                                                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn  btn-secondary" Text="Cancelar" OnClick="btnLimpiar_Click" />
                                                </div>
                                                <div class="col-6 text-right">
                                                    <asp:Button ID="btnGuardaEvento" runat="server" CssClass="btn  btn-secondary" Text="Guardar" OnClick="btnGuardaEvento_Click" />
                                                </div>
                                            </div>

                                        </div>
                                        <hr />
                                        <a data-toggle="collapse" data-target="#eventosActuales" class="collapse-action collapsed" href="#">
                                            <h4>
                                                <i class="fas fa-caret-right collapse"></i>
                                                <i class="fas fa-caret-down normal"></i>
                                                Eventos actuales
                                            </h4>
                                        </a>
                                        <div id="eventosActuales" class="collapse show">
                                            <br />
                                            <asp:GridView ID="grdEventosActuales" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-bordered">
                                                <Columns>
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo ubicación" />
                                                    <asp:BoundField DataField="TipoEvento" HeaderText="Tipo evento" />
                                                    <asp:TemplateField HeaderText="Aforo estimado">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAforo" runat="server" Text='<%# String.Format("{0:n}", Eval("Aforo")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEditar" runat="server" CssClass="btn btn-xs btn-link" Text="EDITAR" OnCommand="btnEditar_Command" CommandName="new" CommandArgument='<%# Eval("Id_Evento") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="container-fluid">
                                                        <div class="row">
                                                            <div class="col-12">
                                                                <h3>No hay eventos agregados</h3>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>--%>

    <!--MODAL AGREGA TAREA-->
    <div id="modalAgregaTarea" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Agregar Sub Actividad Operativa</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updModalTarea" runat="server">
                        <ContentTemplate>
                            <div id="containerLineas" class="container-fluid">
                                <div class="row">
                                    <div class="col-3">
                                        <p>Clave de Gasto</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:DropDownList ID="ddlClave" runat="server" CssClass="campo_obligatorio">
                                            <asp:ListItem Text="Clave 1" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Tipo de Sub actividad</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:DropDownList ID="ddlTipoTarea" runat="server" CssClass="campo_obligatorio"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Nombre</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtNombreTarea" runat="server" Width="100%" CssClass="campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-3">
                                        <p>Mes</p>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList ID="ddlMes" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-3">
                                        <asp:CheckBox ID="chkTodosIgual" runat="server" AutoPostBack="true" Text="Todos igual" OnCheckedChanged="chkTodosIgual_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="row" style="display: none;">
                                    <div class="col-3">
                                        <p>Monto</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtMonto" runat="server" Width="100%" Text="0" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Meta</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtMeta" runat="server" Width="100%" CssClass="campo_obligatorio" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Fecha compromiso</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtFechaCompromiso" Width="100%" runat="server" CssClass="datepicker campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3">
                                        <p>Comentarios</p>
                                    </div>
                                    <div class="col-9">
                                        <asp:TextBox ID="txtComentarios" runat="server" Width="100%" TextMode="MultiLine" Rows="3" CssClass="campo_obligatorio"></asp:TextBox>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <asp:Button ID="btnAgregar" runat="server" CssClass="btn  btn-secondary" OnClientClick="javascript:validaCampos(this,event);" data-target="#containerLineas" Text="Agregar Sub actividad" OnClick="btnAgregar_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <!--MODAL AGREGA PERSONAS-->
    <div id="modalAgregaPersonas" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Asignar personas</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updPersonas" runat="server">
                        <ContentTemplate>
                            <div class="tabla-pmd container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p>Mes</p>
                                        <asp:DropDownList ID="ddlMesPersonas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMesPersonas_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <p style="visibility: hidden;">Mes</p>
                                        <asp:CheckBox ID="chkMesPersonas" runat="server" Text="Todos los meses" OnCheckedChanged="chkMesPersonas_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h3>Proporciona el porcentaje de tiempo para los puestos asignados</h3>
                                        <br />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <b>PUESTO</b>
                                    </div>
                                    <div class="col-4">
                                        <b>EMPLEADO</b>
                                    </div>
                                    <div class="col-3"></div>
                                    <div class="col-1"></div>
                                </div>
                                <hr />
                                <asp:ListView ID="lstPersonas" runat="server">
                                    <ItemTemplate>
                                        <div class="row item">
                                            <div class="col-4">
                                                <asp:Label ID="lblPuesto" runat="server" Text='<%# Eval("Puesto") %>'></asp:Label>
                                            </div>
                                            <div class="col-4">
                                                <asp:HiddenField ID="hdnClave" runat="server" Value='<%# Eval("Clave") %>' />
                                                <asp:Label ID="lblPersona" runat="server" Text='<%# Eval("Empleado") %>'></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="text-right" Text='<%# Eval("Horas") %>' onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                            </div>
                                            <div class="col-1">
                                                <span>horas</span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <hr />
                                        <asp:Button ID="btnGuardaPersonas" runat="server" CssClass="btn btn-secondary" Text="Guardar" OnClick="btnGuardaPersonas_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!--MODAL DE MATERIALES-->
    <div id="modalAgregaMateriales" class="modal fade modal-big" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Seleccionar materiales</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updAgregaMateriales" runat="server">
                        <ContentTemplate>
                            <div class="tabla-pmd container-fluid">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblTituloMateriales" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4 text-center">
                                        <b>PRESUPUESTADO</b><br />
                                        <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-4 text-center">
                                        <b>CAPTURADO</b><br />
                                        <asp:Label ID="lblPresupuestoCapturado" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-4 text-center">
                                        <b>DIFERENCIA</b><br />
                                        <asp:Label ID="lblPresupuestoDiferencia" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-3">
                                        <p>Mes</p>
                                        <asp:DropDownList ID="ddlMesMateriales" runat="server" CssClass="form-control select-basic-simple" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlMesMateriales_SelectedIndexChanged"></asp:DropDownList><br />
                                        <asp:CheckBox ID="chkTodosMesesMateriales" runat="server" Text="Todos los meses" OnCheckedChanged="chkTodosMesesMateriales_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-2">
                                        <p style="visibility: hidden;">Mes</p>
                                        <asp:Button ID="btnCopiarMesSiguiente" runat="server" Font-Size="12px" CssClass="btn btn-link" Text="Copiar al mes siguiente" OnClick="btnCopiarMesSiguiente_Click" />
                                    </div>
                                    <div class="col-4">
                                        <p>Material</p>
                                        <asp:DropDownList ID="ddlMaterial" runat="server" Width="100%" CssClass="select-basic-simple"></asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <p>Cantidad</p>
                                        <asp:TextBox ID="txtCantidadMaterial" runat="server" CssClass="form-control campo_obligatorio" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                    <div class="col-2">
                                        <p style="visibility: hidden;">Cantidad</p>
                                        <asp:Button ID="btnAgregaMaterial" runat="server" CssClass="btn  btn-secondary" OnClientClick="javascript:validaCampos(this,event);" data-target="#modalAgregaMateriales" Text="Agregar" OnClick="btnAgregaMaterial_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <br />
                                        <asp:GridView ID="grdMateriales" runat="server" AllowPaging="true" PageSize="5" CssClass="table table-sm table-hover table-bordered" OnPageIndexChanging="grdMateriales_PageIndexChanging" AutoGenerateColumns="false" Width="100%">
                                            <PagerSettings Mode="Numeric" Position="Bottom" Visible="true" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Material" DataField="Nombre_Material" />
                                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnQuitaMaterial" runat="server" CssClass="btn btn-link " Text="Quitar" OnClick="btnQuitaMaterial_Click" CommandArgument='<%# Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h4>No hay información que mostrar</h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!--MODAL DE VEHICULOS-->
    <div id="modalAgregaVehiculos" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Seleccionar Vehículos</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updVehiculos" runat="server">
                        <ContentTemplate>
                            <div class="tabla-pmd container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p>Mes</p>
                                        <asp:DropDownList ID="ddlMesVehiculo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMesVehiculo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-2">
                                        <p style="visibility: hidden;">Mes</p>
                                        <asp:CheckBox ID="chkMesesVehiculos" runat="server" AutoPostBack="true" Text="Todos los meses" OnCheckedChanged="chkMesesVehiculos_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h3>Proporciona los kilometros de uso de los vehículos</h3>
                                        <br />
                                    </div>
                                </div>
                                <asp:ListView ID="lstVehiculos" runat="server">
                                    <ItemTemplate>
                                        <div class="row item">
                                            <div class="col-8">
                                                <asp:HiddenField ID="hdnIdVehiculo" runat="server" Value='<%# Eval("llave_vehi") %>' />
                                                <asp:Label ID="lblVehiculo" runat="server" Text='<%# String.Format("{0} {1} {2}", Eval("linea_vehi"), Eval("model_vehi"), Eval("marca_vehi")) %>'></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox ID="txtPorcentajeVehiculo" runat="server" CssClass="text-right" Text='<%# Eval("Kilometros") %>' onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                            </div>
                                            <div class="col-1">
                                                <span>Kilometros</span>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="row">
                                    <div class="col-12 text-right">
                                        <hr />
                                        <asp:Button ID="btnAgregaVehiculos" runat="server" CssClass="btn btn-secondary" Text="Guardar" OnClick="btnAgregaVehiculos_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!--MODAL DE BENEFICIADOS-->
    <div id="modalBeneficiados" class="modal fade modal-medium" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Beneficiados del programa</h2>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updBeneficiados" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-4">
                                        <p>Mes</p>
                                        <asp:DropDownList ID="ddlMesBeneficiados" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMesBeneficiados_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    <div class="col-2">
                                        <p style="visibility: hidden;">Mes</p>
                                        <asp:CheckBox ID="chkMesBeneficiados" runat="server" AutoPostBack="true" Text="Todos los meses" OnCheckedChanged="chkMesBeneficiados_CheckedChanged" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <h4>Proporciona la cantidad de personas beneficiarias del programa</h4>
                                    </div>
                                </div>
                                <div id="beneficiadosContent" class="row">
                                    <div class="col-4">
                                        <p>Hombres</p>
                                        <asp:TextBox ID="txtHombresBeneficiados" runat="server" CssClass="text-right campo_obligatorio" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p>Mujeres</p>
                                        <asp:TextBox ID="txtMujeresBeneficiadas" runat="server" CssClass="text-right campo_obligatorio" onkeypress="javascript:validaNumeros(this, event);"></asp:TextBox>
                                    </div>
                                    <div class="col-4">
                                        <p style="visibility: hidden;">Mujeres</p>
                                        <asp:Button ID="btnGuardaBeneficiados" runat="server" CssClass="btn btn-secondary " OnClientClick="javascript:validaCampos(this,event);" data-target="#beneficiadosContent" Text="Guardar" OnClick="btnGuardaBeneficiados_Click" />
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <h4>Agrega las colonias beneficiadas del programa</h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <p>Colonia</p>
                                        <asp:DropDownList ID="ddlColonias" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <p style="visibility: hidden;">Colonia</p>
                                        <asp:CheckBox ID="chkTodasColonias" runat="server" AutoPostBack="true" Text="Todas las colonias" OnCheckedChanged="chkTodasColonias_CheckedChanged" />
                                    </div>
                                    <div class="col-4">
                                        <p style="visibility: hidden;">Colonia</p>
                                        <asp:Button ID="btnAgregaColonias" runat="server" CssClass="btn  btn-secondary" Text="Agregar colonia" OnClick="btnAgregaColonias_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="grdColoniasAsignadas" AllowPaging="true" PageSize="5" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="grdColoniasAsignadas_PageIndexChanging">
                                            <PagerSettings Mode="Numeric" Position="Bottom" Visible="true" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Colonia" DataField="NombrColonia" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnQuitaColonia" runat="server" CssClass="btn btn-link " Text="Quitar" OnClick="btnQuitaColonia_Click" CommandArgument='<%# Eval("Id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h4>No hay información que mostrar</h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <div id="modalIframeURL" class="modal modal-full fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="screen-iframe">
                        <h6>Cargando...</h6>
                    </div>
                    <iframe id="iframeModalURL" width="100%" height="500px"></iframe>
                </div>
                <div class="modal-footer">
                    <a class="btn btn-primary right" data-dismiss="modal">Cerrar</a>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        $(document).ready(function () {
            try {
                $(".datepicker").datepicker({
                    "dateFormat": "dd/mm/yy",
                    "changeYear": false,
                    "minDate": '01/01/2020',
                    "maxDate": '31/12/2020',
                });
            }
            catch (x) { console.log(x); }


        });

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {

            $('#modalIframeURL').on('hidden.bs.modal', function () {
                document.getElementById("<%= btnRecargaEventos.ClientID %>").click();
            });

            //
            try {
                $(".datepicker").datepicker({
                    "dateFormat": "dd/mm/yy",
                    "changeYear": false,
                    "minDate": '01/01/2020',
                    "maxDate": '31/12/2020',
                });
            }
            catch (x) { console.log(x); }

            $('.select-basic-simple').select2();

            //metas
            sumaMetas();

            sumaBeneficiadas();

            sumaBeneficiados();
        }


        function validaLineaSeleccionada(o, e) {
            var value = $('select[id$="ddlLinea"]').val();
            if (parseInt(value) <= 0 || value == undefined) {
                alert('Por favor selecciona una Línea.');
                e.preventDefault();
                return false;
            }
            else {
                document.getElementById('<%= btnReinicia.ClientID %>').click();
            }
        }

        function abreModalTareas() {
            $('#modalAgregaTarea').modal('show');
        }

        function ocultaModalLinea() {
            $('#modalAgregaTarea').modal('hide');
        }

        function abreModalMateriales() {
            $('#modalAgregaMateriales').modal('show');
        }

        function abreModalPersonas() {
            $('#modalAgregaPersonas').modal('show');
        }

        function abreModalVehiculo() {
            $('#modalAgregaVehiculos').modal('show');
        }

        function abreModalBeneficiados() {
            $('#modalBeneficiados').modal('show');
        }

        function abreModalBeneficiadosSubActividad() {
            $('#modalMetasSubActividad').modal('show');
        }

        function MuestraModalEvento() {
            $('#modalEventos').modal('show');
        }

        function MuestraEditarEvento() {
            $('#muestraEditarEvento').click();
        }

        function sumaMetas(o, e) {
            var total = 0;
            var metas = $('.meta');
            metas.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblTotalMetas').text(total);
        }

        function sumaMetasSA(o, e) {
            var total = 0;
            var metas = $('.metaSA');
            metas.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblTotalMetasSA').text(total);
        }

        function sumaBeneficiados(o, e) {
            var total = 0;
            var beneficiados = $('.beneficiados');
            beneficiados.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblBenHTotal').text(total);
        }

        function sumaBeneficiadas(o, e) {
            var total = 0;
            var beneficiadas = $('.beneficiadas');
            beneficiadas.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblBenMTotal').text(total);
        }

        function sumaBeneficiadasSA(o, e) {
            var total = 0;
            var beneficiadas = $('.beneficiadasSA');
            beneficiadas.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblBenMSATotal').text(total);
        }

        function sumaBeneficiadosSA(o, e) {
            var total = 0;
            var beneficiados = $('.beneficiadosSA');
            beneficiados.each(function () {
                var m = $(this).val();
                var meta = parseInt(m);
                if (meta !== NaN && meta != 'NaN' && m != '')
                    total += meta;
            });

            $('#lblBenHSATotal').text(total);
        }

        function muestraFichaEvento(folio) {
            //$('#modalEventos').modal('hide');
            var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio=' + folio; //var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio=' + folio;
            abreModalIframe(url);
            return false;
        }

        function muestraRequerimientoEvento(folio) {
            //$('#modalEventos').modal('hide');
            var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/RequerimientosDirector.aspx?folio=' + folio; //var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/RequerimientosDirector.aspx?folio=' + folio;
            abreModalIframe(url);
            return false;
        }

        function abreModalIframe(url) {
            document.getElementById('iframeModalURL').src = url;
            $('.screen-iframe').show();
            $('#modalIframeURL').modal('show');
            setTimeout(function () {
                $('.screen-iframe').hide();
            }, 1500);
        }

        function agregaNuevoEvento(folio) {
            //$('#modalEventos').modal('hide');
            var secretaria = $('#hdnSecretaria').val();
            var direccion = $('#hdnDireccion').val();
            var actividad = $('#hdnActividad').val();
            var subActividad = $('#hdnSubActividad').val();
            var administracion = $('#hdnAdministracion').val();
            var anio = $('#hdnAnio').val();

            var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/Alta.aspx?folio=' + folio + '&secretaria=' + secretaria + '&direccion=' + direccion + '&actividad=' + actividad + '&subactividad=' + subActividad + '&administracion=' + administracion + '&anio=' + anio; //var url = 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/Alta.aspx?folio=' + folio;
            console.log(url);
            abreModalIframe(url);
            return false;
        }
    </script>
</asp:Content>

