<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" Inherits="PMD_WAS.AltaDirector" MasterPageFile="~/MasterGlobal.master" Codebehind="AltaDirector.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ OutputCache VaryByParam="none" Duration="1" Location="Client" %>
 
<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/estilo_captura.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <!----AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU-->
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
     
    <style type="text/css">
        .style1 {
            width: 400px;
            height: 4px;
        }

        .style20 {
            width: 763px;
        }

        #divlblNombreOperador {
            width: 100px;
        }
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12 text-center">
                <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" Font-Size="25pt" Text="Alta de Evento"></asp:Label>
                <br />
                <br />
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div id="divprincipal">
                    <br />
                    <br />
                    <div id="divalta">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-12">
                                            <div class="container-fluid">
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label2" runat="server" Text="Folio:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:Label ID="txtfolio" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label4" runat="server" Text="Nombre Evento:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtEvento" runat="server" Text="" Width="100%" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                                            Enabled="False" TabIndex="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtEvento" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label5" runat="server" Text="Secretaria:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlSecretaria" runat="server" Height="19px" Width="100%" AutoPostBack="true"
                                                            Enabled="False" TabIndex="2">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*"
                                                            ForeColor="red" ControlToValidate="ddlSecretaria"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label6" runat="server" Text="Dirección:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlDireccion" runat="server" Height="19px" Width="100%" Enabled="False" AutoPostBack="true" OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged"
                                                            TabIndex="3">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                            ForeColor="red" ControlToValidate="ddlDireccion"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="lblActividad" runat="server" Text="Actividad"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlActividad" runat="server" Height="19px" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlActividad_SelectedIndexChanged"
                                                            TabIndex="3">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                            ForeColor="red" ControlToValidate="ddlActividad"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="lblSubActividad" runat="server" Text="Sub Actividad"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlSubActividad" runat="server" Height="19px" Width="100%" TabIndex="3">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                            ForeColor="red" ControlToValidate="ddlSubActividad"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label20" runat="server" Text="Tipo Evento:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlTipoEvento" runat="server" Height="19px" Width="100%"
                                                            Enabled="False" TabIndex="4">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label21" runat="server" Text="Descripción:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtDescripcion" runat="server" Width="100%" TextMode="MultiLine"
                                                            onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                                            TabIndex="5" Height="38px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtDescripcion" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label13" runat="server" Text="Lugar:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtLugar" runat="server" Width="100%"
                                                            onkeypress="javascript:return  solonumletrasMayespacios(event)" TabIndex="6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtLugar" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label7" runat="server" Text="Colonia:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlColonia" runat="server" Height="19px" Width="100%" AutoPostBack="True"
                                                            Enabled="False" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label8" runat="server" Text="Calle:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList ID="ddlCalle" runat="server" Height="19px" Width="100%"
                                                            Enabled="False" TabIndex="8">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                            ForeColor="red" ControlToValidate="ddlCalle"></asp:RequiredFieldValidator>
                                                        <asp:Label ID="label16" runat="server" Text="*" Visible="False"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="label11" runat="server" Text="Num.Ext:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtext" runat="server" Text="" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                                    Enabled="False" TabIndex="9"></asp:TextBox>
                                                                <asp:Label ID="label14" runat="server" Text="*" Visible="False"></asp:Label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="label12" runat="server" Text="Num.Int:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox ID="txtint" runat="server" Text="" Width="100%" Style="text-transform: uppercase"
                                                                    onkeypress="javascript:return solonumletrasMay(event)" Enabled="False"
                                                                    TabIndex="10"></asp:TextBox>
                                                                <asp:Label ID="label15" runat="server" Text="*" Visible="False"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="label9" runat="server" Text="Fecha Evento:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>

                                                                        <asp:TextBox ID="txtfecha" runat="server" Width="100%" Enabled="False"
                                                                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                                                                            TabIndex="11"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="txtfecha_CalendarExtender" runat="server" TargetControlID="txtfecha"
                                                                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                                                                        </asp:CalendarExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                            ControlToValidate="txtfecha" ForeColor="red"></asp:RequiredFieldValidator>

                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="label22" runat="server" Text="Hora de Inicio:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox SIZE="5" ID="txthrini" runat="server" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                                    onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                                                                    TabIndex="13"></asp:TextBox>
                                                                <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                                                                <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txthrini" ForeColor="red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-6">
                                                        <asp:CheckBox ID="CheckAlcalde" runat="server"
                                                            Text="Seleccione si asistirá Alcalde:" TextAlign="Left" AutoPostBack="True"
                                                            TabIndex="12" />
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="lblHoraAlcalde" runat="server" Text="Arribo de Alcalde:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox SIZE="5" ID="txthrAlcalde" runat="server" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                                    onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                                                                    TabIndex="14"></asp:TextBox>
                                                                <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                                                                <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->
                                                                <asp:Label ID="label23" runat="server" Text="*" Visible="False"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-6">
                                                        <asp:CheckBox ID="CheckLimpieza" runat="server"
                                                            Text="Apoyo  de Servicios Públicos:" TextAlign="Left" AutoPostBack="True"
                                                            TabIndex="17" />
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="Label1" runat="server" Text="Salida de Alcalde:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox SIZE="5" ID="txtHrSalidaAlcalde" runat="server" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                                    onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                                                                    TabIndex="15"></asp:TextBox>
                                                                <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                                                                <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-6">
                                                        <asp:CheckBox ID="CheckPrensa" runat="server" Text="Seleccione si asistirá Prensa:"
                                                            TextAlign="Left" AutoPostBack="True" TabIndex="18" />
                                                    </div>
                                                    <div class="col-6">
                                                        <div class="row">
                                                            <div class="col-6">
                                                                <asp:Label ID="lblHoraFinal" runat="server" Text="Hora de Término:"></asp:Label>
                                                            </div>
                                                            <div class="col-6">
                                                                <asp:TextBox SIZE="5" ID="txthrfin" runat="server" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                                    onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                                                                    TabIndex="16"></asp:TextBox>
                                                                <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                                                                <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                    ControlToValidate="txthrfin" ForeColor="red"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label10" runat="server" Text="Núm.Beneficiado(s):"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtnumbenef" runat="server" Text="" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                            Enabled="False" TabIndex="19"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtnumbenef" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="label17" runat="server" Text="Núm. Asistentes:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtAsistentes" runat="server" Text="" Width="100%" onkeypress="javascript:return solonumeros(event)"
                                                            Enabled="False" TabIndex="19"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                            ControlToValidate="txtAsistentes" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="lblResponsableEvento" runat="server" Text="Responsable del Evento:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtResponsableEvento" runat="server" Width="100%"
                                                            onkeypress="javascript:return  solonumletrasMayespacios(event)"
                                                            TabIndex="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtResponsableEvento" ForeColor="red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-5">
                                                        <asp:Label ID="lblTelefonoResponsable" runat="server" Text="Teléfono del Responsable:"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox ID="txtTelefonoResponsable" runat="server"
                                                            onkeypress="javascript:return  solonumeros(event)" MaxLength="10"
                                                            TabIndex="21"></asp:TextBox>
                                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate = "txtTelefonoResponsable"  ForeColor="red"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <%--       <div id="divddlevento">
                        <asp:DropDownList ID="ddl1" runat="server" Height="19px" Width="321px" Enabled="False">
                        </asp:DropDownList>
                    </div>--%>
                                <asp:TextBox ID="TextBox2" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"
                                    Style="margin-bottom: 0px" ForeColor="white"></asp:TextBox>
                                <asp:TextBox ID="TextLng" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"></asp:TextBox>
                                <asp:TextBox ID="TextLat" runat="server" Height="1px" ClientIDMode="Static" ReadOnly="True" Width="1px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        <%--  <div id="divlblmsjerror">
                <asp:Label ID="label13" runat="server" Text="* Campo Obligatorio" Visible="True"></asp:Label>
            </div>--%>
                    </div>
                    <div id="divbtnAceptarMapa">
                        <asp:Button ID="btnGuardar" CssClass="button" runat="server" Text="Guardar" Width="65px"
                            OnClientClick="OpenPopupx();" TabIndex="26" />
                    </div>
                    <div id="divbtnCancelarMapa">
                        <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" Width="65px"
                            Visible="False" TabIndex="27" />
                    </div>

                    <%--         <div id="divExcelMapa">
                <asp:Button ID="btnExcel" CssClass="button" runat="server" Text=" Exportar" Visible="False" />
            </div>--%>
                    <%--             <div id="divRequerimiento">
                <asp:Button ID="Button2" CssClass="button" runat="server" Text="Requerimiento" 
                     Width="110px" />
            </div>--%>
                    <%--            <div id="divFicha">
                <asp:Button ID="btnFicha" CssClass="button" runat="server" Text="Ficha" />
            </div>--%>
                    <%--  <div id="divmsjaltaMapa">
                <asp:Label ID="lblmsjalta" runat="server" Text="" Font-Bold="True" ForeColor="#084B8A"
                    Visible="False"></asp:Label>
            </div>--%>
                    <div id="divgridMapa">
                        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#000065" />
                        </asp:GridView>
                    </div>

                    <table class="style1" style="border: 1px outset #0066CC; margin-left: 51%;">
                        <tr>
                            <td class="style20" align="center">


                                <div style="display: none;">
                                    <asp:TextBox ID="txt_coord_x" runat="server" ClientIDMode="Static" ReadOnly="True" AutoPostBack="True"></asp:TextBox>
                                    <asp:TextBox ID="txt_coord_y" runat="server" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
                                    &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Height="21px" Width="384px">
                                    </asp:DropDownList>
                                </div>

                                <asp:Button ID="Button1" runat="server" Text="Ubicación actual" Width="116px" Visible="False"
                                    Height="20px" />
                                <asp:Button ID="btn_consultar" runat="server" Text="Consultar" Visible="False" Height="21px" />
                                <asp:Button ID="btn_pone_punto" runat="server" Text="Pone punto" Visible="False"
                                    Height="21px" />
                                <asp:Panel ID="Panel1" runat="server" Height="400px" Width="480px">
                                    <div id="map_canvas" style="width: 470px; height: 390px">
                                    </div>

                                    <asp:TextBox ID="TextBox1" runat="server" Height="1px" ReadOnly="True" Width="1px"></asp:TextBox>
                                    <%--<asp:TextBox ID="TextBox2" runat="server" Height="34px" ReadOnly="True" Width="169px"
                                    Style="margin-bottom: 0px" forecolor = "white" ></asp:TextBox>--%>

                                    <asp:TextBox ID="TextTitulo" runat="server" Height="1px" ReadOnly="True" ClientIDMode="Static" Width="1px"></asp:TextBox>
                                </asp:Panel>

                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="updOperador" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="divOperadoPor">
                                <h3>Operado por</h3>
                                <br />
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-4">
                                            <span>Puesto:</span>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlPuestoOperador" runat="server" Width="100%" OnSelectedIndexChanged="ddlPuestoOperador_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-4">
                                            <span>Empleado:</span>
                                        </div>
                                        <div class="col-8">
                                            <asp:DropDownList ID="ddlEmpleadoOperador" runat="server" Width="100%" OnSelectedIndexChanged="ddlEmpleadoOperador_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-4">
                                            <span>Teléfono:</span>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtTelefonoOperador" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-4">
                                            <span>Correo:</span>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtCorreoOperador" runat="server" Width="100%"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </div>

            </div>
        </div>
    </div>


    <a href="http://apycom.com/" style="color: White"></a>

    <div id="divusuarioAlta">
        <asp:Label ID="Label3" runat="server" Text="" Width="50px"></asp:Label>
    </div>


    <div id="divnominaAlta">
        <asp:Label ID="Label19" runat="server" Width="50px" Height="16px"></asp:Label>
    </div>

    <div id="divinputbox">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

        function beginReq(sender, args) {
        }

        function endReq(sender, args) {
            $('.select-basic-simple').select2();
            geolocalizame();
        }


        function alerta_Alta_Exito(folio) {
            var contenido = $('<div class="container-fluid">')
                .append('<div class"row"><div class="col-12"><p>El evento se ha creado correctamente, pero antes de poder ser "validado" deberá de completar la información en "Ficha" y "Requerimientos"</p></div></div><hr />')
                .append($('<div class="row"></div>').append($('<div class="col-6 text-left"></div>').append(createButton('Ir a "Ficha"', function () {
                    swal.close();
                    window.open('/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio=' + folio, '_self');
                }))).append($('<div class="col-6 text-right"></div>')
                    .append(createButton('Ir a "Requerimientos"', function () {
                        swal.close();
                        window.open('/PlaneacionFinanciera/Eventos2015/Requerimientos.aspx?folio=' + folio, '_self');
                        console.log('Req');
                    })))
                );

            swal({
                html: contenido,
                type: "info",
                showConfirmButton: false,
                showCancelButton: false
            });
        }

        function createButton(text, cb) {
            return $('<button class="btn  btn-primary">' + text + '</button>').on('click', cb);
        }

        function alerta_Alta_Error() {
            mensajeCustom('error', 'Algo salió mal', 'No se guardo la información!!');
        }

        function alerta_Alta_Error_PuntoMapa() {
            mensajeCustom('warning', 'Algo falta', 'No se guardo:PUNTO EN MAPA, Favor de Guardarlo en pantalla de CAMBIOS');
        }


        function alerta_Alta_Error_FechaPosterior() {
            mensajeCustom('error', 'Algo salió mal', 'No se permite guardar el evento, tiene que ser una fecha posterior al día de hoy!!');
        }



        function alerta_Alta_Volver_PuntoMapa() {
            mensajeCustom('info', 'PUNTO EN EL MAPA', 'Volver a Colocar Punto en el Mapa');
        }


        function alerta_Alta_Error_NoPermiteGuardarEvento() {
            var dias = '<%= VariablesGlobales.DIAS_ESPERA_EVENTO %>';
            mensajeCustom('info', 'FECHA DEL EVENTO', 'No se permite guardar el evento, porque no está dentro de los ' + dias + ' días hábiles permitidos!!');
        }

        function alerta_Alta_Error_NoPermiteGuardarEventoYPuntoMapa() {
            var dias = '<%= VariablesGlobales.DIAS_ESPERA_EVENTO %>';
            mensajeCustom('info', 'FECHA DEL EVENTO', 'No se permite guardar el evento, porque no está dentro de los ' + dias + ' días hábiles permitidos!!');
        }

        function alerta_Alta_Error_Fecha() {
            mensajeCustom('warning', 'Formato incorrecto', 'Recuerde el formato de fecha, YYYY-MM-DD');
        }


        function alerta_Alta_Error_Hora() {
            mensajeCustom('warning', 'Formato incorrecto', 'Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }

        function alerta_Alta_Error_HoraAlcalde() {
            mensajeCustom('warning', 'Formato incorrecto', 'HORA ALCALDE:Recuerde el formato de Hora, desde 00:00 hasta 23:59');
        }


        var map; //importante definirla fuera de la funcion initialize() para poderla usar desde otras funciones.
        var infowindow = new google.maps.InfoWindow({ maxWidth: 200 });
        var poly;
        var Polyx;
        var PxLngLat = [];
        var Munx;
        function initialize() {

            var Lx1 = document.getElementById('TextLng').value;
            var Lx2 = document.getElementById('TextLat').value;
            if (Lx1 == '') {

                Munx = new google.maps.LatLng(25.754757464040086, -100.2895021203766);
                var mapOptions = {
                    zoom: 16,
                    center: Munx,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };


                map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
                navigator.geolocation.getCurrentPosition(pedirPosicion);

            } else {
                console.log('entra aqui');
                console.log(Lx2);
                console.log(Lx1);
                Munx = new google.maps.LatLng(Lx2, Lx1);
                var mapOptions = {
                    zoom: 16,
                    center: Munx,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                GeneraPuntos();

                map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);

                ////

                var titulo = 'Punto';
                var latlng = new google.maps.LatLng(Lx2, Lx1);
                map.setCenter(latlng);
                marcador2 = new google.maps.Marker({
                    position: latlng,
                    draggable: true,
                    map: map,
                    title: titulo
                });
                ////

                Polyx = new google.maps.Polygon({
                    paths: PxLngLat,
                    strokeColor: "#6666FF",
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: "#3366CC",
                    fillOpacity: 0.15
                });
                Polyx.setMap(map);

                google.maps.event.addListener(marcador2, 'dragend', function () {
                    var Puntos = marcador2.getPosition();
                    var Latx = Puntos.lat();
                    var Lonx = Puntos.lng();
                    //alert(Latx + ',' + Lonx);
                    document.getElementById('txt_coord_x').value = Latx;
                    document.getElementById('txt_coord_y').value = Lonx;




                });
            }
        }

        function OpenPopupx() {

            var rx = document.getElementById('txt_coord_x').value;
            var ry = document.getElementById('txt_coord_y').value;
            var link = "Variables.aspx?vx=" + rx + "&vy=" + ry;
            window.open(link, "prueba", "location=1,status=1,scrollbars=yes,resizable=yes,left=2000,top=2000,width=5px,height=5px,titlebar=0,toolbar=0,menubar=0");
            return false;

        }


        function pedirPosicion(pos) {

            var centro = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
            map.setCenter(centro); //pedimos que centre el mapa..
            map.setMapTypeId(google.maps.MapTypeId.ROADMAP); //y lo volvemos un mapa callejero
            MarcaPunto(pos);
        }


        function geolocalizame() {

            initialize();


        }

        function showArrays(event) {
            var vertices = this.getPath();
            var contentString = "<b>Bermuda Triangle Polygon</b><br />";
            contentString += "Clicked Location: <br />" + event.latLng.lat() + "," + event.latLng.lng() + "<br />";
            infowindow.setContent(contentString);
            infowindow.setPosition(event.latLng);
            infowindow.open(map);

        }

        function GeneraPuntos() {

            cont = 0;
            var ix = 0;
            var cad = "";
            var iniciar = 0;
            var entrox = 0;
            var CadRx = "";
            var cadena = document.getElementById('TextBox2').value;
            var xIdx = "";
            var SubCad = "";
            var xPunto = "";

            for (ix = 0; ix <= cadena.length; ix++) {
                cad = "";
                cad = cadena.substring(ix - 1, ix);
                if (cad == "*" && iniciar == 1) { //inicia Cadena
                    iniciar = 0;
                    xPunto = "";
                    entrox = 0;
                    xIdx = "";
                    for (iy = 0; iy <= CadRx.length; iy++) {
                        SubCad = "";
                        SubCad = CadRx.substring(iy - 1, iy);

                        if (SubCad == "(") {
                            entrox = 1;
                        }
                        if (entrox == 1) {
                            xPunto = xPunto + SubCad;
                        }
                        if (entrox == 0) {
                            xIdx = xIdx + SubCad;
                        }
                    }

                    xPunto = xPunto.replace("(", "")
                    xPunto = xPunto.replace(")", "")

                    var mytool_array = xPunto.split(",");
                    //alert(mytool_array[0] + "-" + mytool_array[1] );
                    var Longx = mytool_array[0];
                    var Latx = mytool_array[1];
                    //crear Puntos

                    document.getElementById('TextLng').value = Longx;
                    document.getElementById('TextLat').value = Latx;
                    document.getElementById('txt_coord_x').value = Longx;
                    document.getElementById('txt_coord_y').value = Latx;
                    document.getElementById('TextTitulo').value = xIdx;

                    if (xPunto.length == 21) {

                        addLatLng();
                        cont = cont + 1;
                    }
                    cadRx = "";
                }
                if (cad == "*" && iniciar == 0) { //inicia Cadena
                    iniciar = 1;
                    CadRx = ""
                }
                if (cad != "*" && iniciar == 1) {
                    CadRx = CadRx + cad;
                }
            }

        }

        function addLatLng() {
            var txt1 = document.getElementById('txt_coord_x').value;
            var txt2 = document.getElementById('txt_coord_y').value;
            var txtTx = document.getElementById('TextTitulo').value;
            var lat = txt1;
            var lng = txt2;
            var latlng = new google.maps.LatLng(lat, lng);
            PxLngLat.push(latlng);
        }



        function MarcaPunto(pos) {

            //alert("¡Hola! Estas en : " + pos.coords.latitude + "," + pos.coords.longitude + " Rango de localización de +/- " + pos.coords.accuracy + " metros");
            var Lx1 = document.getElementById('TextLng').value;
            var Lx2 = document.getElementById('TextLat').value;

            if (Lx1 == '') {
                var txt1 = pos.coords.latitude;
                var txt2 = pos.coords.longitude;
            }
            else {
                var txt1 = document.getElementById('TextLat').value;
                var txt2 = document.getElementById('TextLng').value;
            }

            var titulo = 'Punto';
            var textoInfo = 'Información';

            var lat = parseFloat(txt1);
            var lng = parseFloat(txt2);
            document.getElementById('txt_coord_x').value = lat;
            document.getElementById('txt_coord_y').value = lng;

            var latlng = new google.maps.LatLng(lat, lng);
            map.setCenter(latlng);
            marcador = new google.maps.Marker({
                position: latlng,
                draggable: true,
                map: map,
                title: titulo
            });

            //google.maps.event.addListener(marcador, 'click', function () { //esto es para cuando sea click en el marcador
            google.maps.event.addListener(marcador, 'dragend', function () { //para cuando se arrastre el marcador
                var Puntos = marcador.getPosition();
                var Latx = Puntos.lat();
                var Lonx = Puntos.lng();
                document.getElementById('txt_coord_x').value = Latx;
                document.getElementById('txt_coord_y').value = Lonx;


            });
        }



        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }


        //No se captura nada//

        function nocaptura(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 0 || key > 0) {
                return false;
            }

            return true;
        }



        function solonumletras(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122)) {
                return true;
            }

            return false;
        }

        function solonumletrasMayMinyespacios(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/)
            if ((key >= 32 && key <= 32) || (key >= 45 && key <= 45) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumletrasMayespacios(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/)
            if ((key >= 32 && key <= 32) || (key >= 44 && key <= 46) || (key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 97 && key <= 122) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241) || (key >= 193 && key <= 193) || (key >= 201 && key <= 201) || (key >= 205 && key <= 205) || (key >= 211 && key <= 211) || (key >= 218 && key <= 218) || (key >= 225 && key <= 225) || (key >= 233 && key <= 233) || (key >= 237 && key <= 237) || (key >= 243 && key <= 243) || (key >= 250 && key <= 250)) {
                return true;
            }

            return false;
        }


        function solonumletrasMay(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            //46(.) Y 47 (/) Y 45(-)
            if ((key >= 48 && key <= 57) || (key >= 65 && key <= 90) || (key >= 209 && key <= 209) || (key >= 241 && key <= 241)) {
                return true;
            }

            return false;
        }


        function solonumerosparafecha(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }
            //46(.) Y 47 (/)
            if (key < 48 || key > 58) {
                return false;
            }

            return true;
        }



        var patron = new Array(2, 2)
        function mascara(d, sep, pat, nums) {
            if (d.valant != d.value) {
                val = d.value
                largo = val.length
                val = val.split(sep)
                val2 = ''
                for (r = 0; r < val.length; r++) {
                    val2 += val[r]
                }
                if (nums) {
                    for (z = 0; z < val2.length; z++) {
                        if (isNaN(val2.charAt(z))) {
                            letra = new RegExp(val2.charAt(z), "g")
                            val2 = val2.replace(letra, "")
                        }
                    }
                }
                val = ''
                val3 = new Array()
                for (s = 0; s < pat.length; s++) {
                    val3[s] = val2.substring(0, pat[s])
                    val2 = val2.substr(pat[s])
                }
                for (q = 0; q < val3.length; q++) {
                    if (q == 0) {
                        val = val3[q]
                    }
                    else {
                        if (val3[q] != "") {
                            val += sep + val3[q]
                        }
                    }
                }
                d.value = val
                d.valant = val
            }
            if (val.length == 5) {
                hora = val.split(":")
                if ((hora[0] > 23) || (hora[1] > 59))
                    alert("Recuerde el formato de Horas, desde 00:00 hasta 23:59 ")

            }

        }

        (function ($) {

            geolocalizame();

        })(jQuery);
    </script>


</asp:Content>
