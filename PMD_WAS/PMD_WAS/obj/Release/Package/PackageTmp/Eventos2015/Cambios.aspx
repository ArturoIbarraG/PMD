<%@ Page Language="VB" Title="Cambios" AutoEventWireup="false" Inherits="PMD_WAS.Cambios" MasterPageFile="~/MasterGlobal.master" Codebehind="Cambios.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/button.css" rel="stylesheet" type="text/css" />
    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/estilo_captura.css" rel="stylesheet" type="text/css" />
    <!----AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU-->
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>

    <div id="divprincipal" style="margin: 5% 10% 5% 10%">

        <div id="divalta">
            <div id="divlblfolio">
                <asp:Label ID="label2" runat="server" Text="Folio:"></asp:Label>
            </div>
            <div id="divlblevento">
                <asp:Label ID="label4" runat="server" Text="Nombre Evento:"></asp:Label>
            </div>
            <div id="divlblsecretaria">
                <asp:Label ID="label5" runat="server" Text="Secretaria:"></asp:Label>
            </div>
            <div id="divlbldireccion">
                <asp:Label ID="label6" runat="server" Text="Dirección:"></asp:Label>
            </div>
            <div id="divlblTipoEvento">
                <asp:Label ID="label20" runat="server" Text="Tipo Evento:"></asp:Label>
            </div>
            <div id="divlblDescripcion">
                <asp:Label ID="label21" runat="server" Text="Descripción:"></asp:Label>
            </div>
            <div id="divlblLugar">
                <asp:Label ID="label13" runat="server" Text="Lugar:"></asp:Label>
            </div>

            <div id="divlblcolonia">
                <asp:Label ID="label7" runat="server" Text="Colonia:"></asp:Label>
            </div>
            <div id="divlblcalle">
                <asp:Label ID="label8" runat="server" Text="Calle:"></asp:Label>
            </div>
            <div id="divlblnumext">
                <asp:Label ID="label11" runat="server" Text="Num.Ext:"></asp:Label>
            </div>
            <div id="divlblnumint">
                <asp:Label ID="label12" runat="server" Text="Num.Int:"></asp:Label>
            </div>
            <div id="divlblfecha">
                <asp:Label ID="label9" runat="server" Text="Fecha Evento:"></asp:Label>
            </div>
            <div id="divtxtfolio">
                <asp:TextBox ID="txtfolio" runat="server" placeholder="INGRESE FOLIO" Font-Size="small"
                    onkeypress="javascript:return solonumeros(event)" AutoPostBack="True"
                    TabIndex="1"></asp:TextBox>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div id="divtxtHora1">
                        <asp:TextBox SIZE="5" ID="txthrini" runat="server" Width="45px" onkeypress="javascript:return solonumeros(event)"
                            onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                            Enabled="False" TabIndex="14"></asp:TextBox>
                        <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                        <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*"
                            ControlToValidate="txthrini" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>




                    <div id="divtxtHora">
                        <asp:TextBox SIZE="5" ID="txthrfin" runat="server" Width="45px" onkeypress="javascript:return solonumeros(event)"
                            onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                            Enabled="False" TabIndex="17"></asp:TextBox>
                        <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                        <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            ControlToValidate="txthrfin" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>

                    <div id="divtxtHoraAlcalde">
                        <asp:TextBox SIZE="5" ID="txthralcalde" runat="server" Width="45px" onkeypress="javascript:return solonumeros(event)"
                            onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                            Enabled="False" TabIndex="15"></asp:TextBox>

                        <%--             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*"
                            ControlToValidate="txthralcalde" ForeColor="red"></asp:RequiredFieldValidator>--%>
                    </div>


                    <div id="divlblnumbenef">
                        <asp:Label ID="label10" runat="server" Text="Núm.Beneficiado(s):"></asp:Label>
                    </div>
                    <div id="divlblCheckPrensa">
                        <asp:CheckBox ID="CheckPrensa" runat="server" Text="Seleccione si asistirá Prensa:"
                            TextAlign="Left" Enabled="False" TabIndex="19" />
                    </div>
                    <div id="divlblCheckLimpiezaArea">
                        <asp:CheckBox ID="CheckLimpieza" runat="server"
                            Text="Apoyo  de Servicios Públicos:" TextAlign="Left" Enabled="False"
                            TabIndex="18" />
                    </div>

                    <div id="divlblResponsableEvento">
                        <asp:Label ID="lblResponsableEvento" runat="server" Text="Responsable del Evento:"></asp:Label>
                    </div>
                    <div id="divtxtResponsableEvento">
                        <asp:TextBox ID="txtResponsableEvento" runat="server" Width="265px"
                            onkeypress="javascript:return  solonumletrasMayespacios(event)"
                            Enabled="False" TabIndex="21"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="txtResponsableEvento" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>

                    <div id="divlblTelefonoResponsable">
                        <asp:Label ID="lblTelefonoResponsable" runat="server" Text="Teléfono del Responsable:"></asp:Label>
                    </div>
                    <div id="divtxtTelefonoResponsable">
                        <asp:TextBox ID="txtTelefonoResponsable" runat="server"
                            onkeypress="javascript:return  solonumeros(event)" MaxLength="10"
                            Enabled="False" TabIndex="22"></asp:TextBox>
                    </div>

                    <div id="divlblCheckAlcalde">
                        <asp:CheckBox ID="CheckAlcalde" runat="server"
                            Text="Seleccione si asistirá Alcalde:" TextAlign="Left" Enabled="False"
                            TabIndex="13" />
                    </div>
                    <div id="divlblHoraAlcalde">
                        <asp:Label ID="lblHoraAlcalde" runat="server" Text="Arribo de Alcalde:"></asp:Label>
                    </div>
                    <div id="divlblHoraSalidaAlcalde">
                        <asp:Label ID="lblHrSalidaAlcalde" runat="server" Text="Salida de Alcalde:"></asp:Label>
                    </div>
                    <div id="divtxtHoraSalidaAlcalde">
                        <asp:TextBox SIZE="5" ID="txtHrSalidaAlcalde" runat="server" Width="45px" onkeypress="javascript:return solonumeros(event)"
                            onkeyup="mascara(this,':',patron,true)" MaxLength="5" AutoPostBack="True"
                            TabIndex="16" Enabled="False"></asp:TextBox>
                        <!-- <input type="time" name="hora" value="00:00" max="24:00" min="01:00" step="1" Width ="43px">-->
                        <!--<input type = "time" name="hora" value="00:00" style="width: 4368px">-->

                    </div>


                    <div id="divlblmsjcambio">
                        <asp:Label ID="lblmsjcambio" runat="server" Text="" Visible="False"></asp:Label>
                    </div>
                    <div id="divddlevento">
                        <asp:TextBox ID="txtEvento" runat="server" Text="" Width="318px" onkeypress="javascript:return  solonumletrasMayespacios(event)"
                            Enabled="False" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            ControlToValidate="txtEvento" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                    <%--       <div id="divddlevento">
                        <asp:DropDownList ID="ddl1" runat="server" Height="19px" Width="321px" Enabled="False">
                        </asp:DropDownList>
                    </div>--%>
                    <div id="divddlsecretaria">
                        <asp:DropDownList ID="ddl2" runat="server" Height="19px" Width="321px" AutoPostBack="True"
                            Enabled="False" TabIndex="3">
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="ddl2"></asp:RequiredFieldValidator>
                    </div>
                    <div id="divddldireccion">
                        <asp:DropDownList ID="ddl3" runat="server" Height="19px" Width="321px" Enabled="False"
                            AutoPostBack="True" TabIndex="4">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                            ControlToValidate="ddl3" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                    <div id="divddlTipoEvento">
                        <asp:DropDownList ID="DropDownList2" runat="server" Height="19px" Width="321px"
                            Enabled="False" TabIndex="5">
                        </asp:DropDownList>
                    </div>
                    <div id="divddlDescripcion">
                        <asp:TextBox ID="txtDescripcion" runat="server" Width="318px" TextMode="MultiLine"
                            onkeypress="javascript:return  solonumletrasMayespacios(event)"
                            Enabled="False" TabIndex="6" Height="38px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            ControlToValidate="txtDescripcion" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>

                    <div id="divtxtLugar">
                        <asp:TextBox ID="txtLugar" runat="server" Width="318px"
                            onkeypress="javascript:return  solonumletrasMayespacios(event)"
                            TabIndex="7" Enabled="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                            ControlToValidate="txtLugar" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>

                    <div id="divddlcalle">
                        <asp:DropDownList ID="ddl5" runat="server" Height="19px" Width="321px" AutoPostBack="True"
                            Enabled="False" TabIndex="9">
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="divddlcolonia">
                <asp:DropDownList ID="ddl4" runat="server" Height="19px" Width="321px" AutoPostBack="True"
                    Enabled="False" TabIndex="8">
                </asp:DropDownList>
            </div>
            <%--<div id="divddlcalle">
                <asp:DropDownList ID="ddl5" runat="server" Height="19px" Width="321px" AutoPostBack="True"
                    Enabled="False">
                </asp:DropDownList>
            </div>--%>
            <div id="divlblHora">
                <asp:Label ID="label22" runat="server" Text="Hora de Inicio:"></asp:Label>

            </div>
            <div id="divlblHoraFinal">
                <asp:Label ID="lblHoraFinal" runat="server" Text="Hora de Término:"></asp:Label>
            </div>

            <!--<div id="diverrorevento">
                <asp:Label ID="label1111" runat="server" Text="*" Visible="False"></asp:Label>
            </div>-->

            <div id="diverrorcalle">
                <asp:Label ID="label16" runat="server" Text="*" Visible="False"></asp:Label>
            </div>
            <div id="divnumext">
                <asp:TextBox ID="txtext" runat="server" Text="" Width="45px" onkeypress="javascript:return solonumeros(event)"
                    Enabled="False" TabIndex="10"></asp:TextBox>
            </div>
            <div id="divlblerrornumext">
                <asp:Label ID="label14" runat="server" Text="*" Visible="False"></asp:Label>
            </div>
            <div id="divnumint">
                <asp:TextBox ID="txtint" runat="server" Text="" Width="45px" Style="text-transform: uppercase"
                    onkeypress="javascript:return solonumletrasMay(event)" Enabled="False"
                    TabIndex="11"></asp:TextBox>
            </div>
            <div id="divlblerrornumint">
                <asp:Label ID="label15" runat="server" Text="*" Visible="False"></asp:Label>
            </div>
            <!--<div id="divlblerrorfecha">
                <asp:Label ID="label18" runat="server" Text="*" Visible="False"></asp:Label>
            </div>-->



            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div id="divtxtfecha">
                        <asp:TextBox ID="txtfecha" runat="server" Width="65px" Enabled="False"
                            onkeypress="javascript:return nocaptura(event)" AutoPostBack="True"
                            TabIndex="12"></asp:TextBox>
                        <asp:CalendarExtender ID="txtfecha_CalendarExtender" runat="server" TargetControlID="txtfecha"
                            TodaysDateFormat="yyyy-MM-dd" Format="yyyy-MM-dd">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            ControlToValidate="txtfecha" ForeColor="red"></asp:RequiredFieldValidator>
                    </div>
                </ContentTemplate>

            </asp:UpdatePanel>

            <div id="diverrorHora">
                <asp:Label ID="label23" runat="server" Text="*" Visible="False"></asp:Label>
            </div>
            <div id="divtxtnumbenef">
                <asp:TextBox ID="txtnumbenef" runat="server" Text="" Width="65px" onkeypress="javascript:return solonumeros(event)"
                    Enabled="False" TabIndex="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                    ControlToValidate="txtnumbenef" ForeColor="red"></asp:RequiredFieldValidator>
            </div>
            <!--<div id="diverrornumbenef">
                <asp:Label ID="label17" runat="server" Text="*" Visible="False"></asp:Label>
            </div>-->
            <%--   <div id="divlblmsjerror">
                <asp:Label ID="label13" runat="server" Text="* Campo Obligatorio" Visible="False"></asp:Label>
            </div>--%>
            <div id="divbtnCancelarMapa">
                <asp:Button ID="btnCancelar" CssClass="button" runat="server" Text="Cancelar" Width="65px"
                    Visible="False" TabIndex="28" />
            </div>


            <div id="divbtnAceptarMapa">
                <asp:Button ID="btnAceptar" CssClass="button" runat="server" Text="Guardar" Width="65px"
                    Visible="False" OnClientClick="OpenPopupx();" TabIndex="27" />
            </div>

            <%--
            <div id="divExcelMapa">
                <asp:Button ID="btnExportar" CssClass="button" runat="server" Text="Exportar" Visible="False" />
            </div>--%>


            <%--            <div id="divmsjaltaMapa">
                <asp:Label ID="lblmsjalta" runat="server" Text="Se actualizó con éxito" ForeColor="#084B8A"
                    Font-Bold="True" Visible="False"></asp:Label>
            </div>
            --%>

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
                <div id="divinputboxCambio">
                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("hiddenfield1") %>' />
                </div>
            </div>
        </div>
        <table class="style1" style="border: 1px outset #0066CC; margin-left: 51%">
            <tr>
                <td class="style20" align="center">
                    <table class="style1">
                        <tr>
                            <div style="display: none;">
                                <asp:TextBox ID="txt_coord_x" runat="server" Height="22px" Width="38px" ReadOnly="true" ClientIDMode="Static"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:TextBox ID="txt_coord_y" runat="server" Height="16px" Width="51px" ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:DropDownList ID="DropDownList1" runat="server" Height="33px" Width="122px">
                                </asp:DropDownList>
                            </div>
                            <asp:Button ID="Button1" runat="server" Text="Ubicación actual" Width="76px" Visible="False"
                                Height="22px" />
                            <asp:Button ID="btn_consultar" runat="server" Text="Consultar" Visible="False" Height="23px"
                                Width="45px" />
                            <asp:Button ID="btn_pone_punto" runat="server" Text="Pone punto" Visible="False"
                                Height="21px" />
                            <%--<td class="style4" align="center" valign="top">--%>
                            <%--<asp:Panel ID="Panel1" runat="server" Height="242px" Width="400px">--%>
                            <asp:Panel ID="Panel1" runat="server" Height="400px" Width="480px"
                                TabIndex="23">
                                <%-- <div id="map_canvas" style="width: 308px; height: 236px">--%>
                                <div id="map_canvas" style="width: 470px; height: 390px">
                                </div>
                                <asp:TextBox ID="TextBox1" runat="server" Height="1px" ReadOnly="True" Width="1px"
                                    Visible="True"></asp:TextBox>
                                <%--<asp:TextBox ID="TextBox2" runat="server" Height="1px" ReadOnly="True" Width="169px"  Style="margin-bottom: 0px" Visible="True"></asp:TextBox>--%>
                                <asp:TextBox ID="TextBox2" runat="server" Height="1px" ReadOnly="True" Width="1px" ClientIDMode="Static"
                                    Style="margin-bottom: 0px" Visible="True"></asp:TextBox>
                                <asp:TextBox ID="TextLng" runat="server" Height="1px" ReadOnly="True" Width="1px" ClientIDMode="Static"
                                    Visible="True"></asp:TextBox>
                                <asp:TextBox ID="TextLat" runat="server" Height="1px" ReadOnly="True" Width="1px" ClientIDMode="Static"
                                    Visible="True"></asp:TextBox>
                                <asp:TextBox ID="TextTitulo" runat="server" Height="1px" ReadOnly="True" Width="1px" ClientIDMode="Static"></asp:TextBox>
                            </asp:Panel>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style21"></td>
            </tr>
        </table>
        <div id="divOperadoPorCambios">


            <div id="divlblNombreOperador">

                <asp:Label ID="lblOperadopor" runat="server" Text="Operado por:"></asp:Label>
            </div>
            <div id="divtxtNombreOperador">
                <asp:TextBox ID="txtOperadopor" runat="server" Width="300px"
                    onkeypress="javascript:return  solonumletrasMayespacios(event)"
                    Enabled="False" TabIndex="23"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="txtOperadopor"></asp:RequiredFieldValidator>
            </div>

            <div id="divlblPuestoOperador">
                <asp:Label ID="lblPuestoOperador" runat="server" Text="Puesto:"></asp:Label>


            </div>
            <div id="divtxtPuestoOperador">
                <asp:TextBox ID="txtPuestoOperador" runat="server" Width="300px"
                    onkeypress="javascript:return  solonumletrasMayespacios(event)"
                    Enabled="False" TabIndex="24"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="txtPuestoOperador"></asp:RequiredFieldValidator>


            </div>
            <div id="divlblTelefonoOperador">
                <asp:Label ID="lblTelOperador" runat="server" Text="Teléfono:"></asp:Label>


            </div>
            <div id="divtxtTelefonoOperador">
                <asp:TextBox ID="txtTelefonoOperador" runat="server" Width="120px"
                    onkeypress="javascript:return  solonumeros(event)" MaxLength="10"
                    Enabled="False" TabIndex="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="txtTelefonoOperador"></asp:RequiredFieldValidator>
            </div>
            <div id="divlblCorreoOperador">
                <asp:Label ID="lblCorreoOperador" runat="server" Text="Correo:"></asp:Label>
            </div>
            <div id="divtxtCorreoOperador">
                <asp:TextBox ID="txtCorreoOperador" runat="server" Width="120px" Enabled="False"
                    TabIndex="26"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" ForeColor="red" ControlToValidate="txtCorreoOperador"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="*" ValidationExpression="\S+@\S+\.\S+\w+" ControlToValidate="txtCorreoOperador" ForeColor="red"></asp:RegularExpressionValidator>

            </div>
        </div>
    </div>

    <a href="http://apycom.com/" style="color: White"></a>
    <div id="divusuarioAlta">
        <asp:Label ID="Label3" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaAlta">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divinputbox">
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </div>

    <script type="text/javascript">




        function alerta_Cambio_Exito() {
            var Sexy = new SexyAlertBox();
            Sexy.alert("<b>Cambio realizado correctamente!!</b>");

        }

        function alerta_Cambio_Error() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo la información!!</b></p>");

        }


        function alerta_Cambio_Error_FechaPosterior() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se permite guardar el evento, tiene que ser una fecha posterior al día de hoy!!</b></p>");

        }




        function alerta_Cambio_Error_YaValidada() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>VALIDACION</h1><p><b>No se permite realizar cambios porque el evento ya fue Validado por Relaciones Públicas</b></p><p>Ir a Consultas/Consulta Estatus(pantalla para consultar estatus de la solicitud de evento).</p>");

        }




        function alerta_Cambio_Error_PuntoMapa() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>ERROR</h1><p><b>No se guardo:PUNTO EN MAPA, Favor de Guardarlo en pantalla de CAMBIOS</b></p>");

        }


        function alerta_Cambio_Error_Hora() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de Horas, desde 00:00 hasta 23:59</b>");

        }

        function alerta_Cambio_Error_HoraAlcalde() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>HORA ALCALDE:Recuerde el formato de Horas, desde 00:00 hasta 23:59</b>");

        }



        function alerta_Cambio_Error_NoPermiteGuardarEvento() {
            var Sexy = new SexyAlertBox();
            Sexy.error("<h1>FECHA DEL EVENTO</h1><p><b>No se permite guardar el evento, porque no está dentro de los 3 días hábiles permitidos!!</b></p>");

        }

        function alerta_Cambio_Error_NoPermiteGuardarEventoYPuntoMapa() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<h1>FECHA DEL EVENTO</h1><br><p>La fecha seleccionada no está dentro de los 3 días hábiles permitidos</p><p>Favor de seleccionar una fecha válida!!</p>");

        }

        function alerta_Cambio_Error_Fecha() {
            var Sexy = new SexyAlertBox();
            Sexy.info("<b>Recuerde el formato de fecha, YYYY-MM-DD </b>");

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


        function Button2_onclick() {

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
