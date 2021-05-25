<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" Inherits="PMD_WAS.Captura" Codebehind="Captura.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%--*****************ESTO ES PARA QUE MUESTRE EL SEXY ALERT CUANDO CARGA LA PAGINA **************************--%>
    <%--**(Los scripts se encuentran en la carpeta SexyAlert y hago referencia a ellos en la pagina maestra**--%>
    <script type="text/javascript">
        window.addEvent('domready', function () {
            Sexy = new SexyAlertBox();
            Sexy.info("<b>Bienvenido!</b> <p>Recuerda Revisar Tanto Tus Líneas Programadas Como Tus Líneas Por Solicitud</p>");
        });
    </script>
    <style type="text/css">

        .ajax__tab_xp .ajax__tab_tab{

            height:21px !important;
        }
    </style>
    <script type="text/javascript">
        window.addEvent('domready', function () {
            var Sexy = new SexyAlertBox();
        });
    </script>
    <%--*******************************************--%>
    <script type="text/javascript">
        document.onload = function () {

        }
        function CompararCantidades() {
            alert("Cantidades No Coinciden ")
        }
    </script>
    <script type="text/javascript" language="javascript">
        window.onload = function () {
            var obj = document.getElementById('MainContent_GridView1_txt1_0');
            SumaRealP(obj, 1);
            SumaPlaneado(obj, 1);
        }
    </script>
    <%--*******************************************--%>
    <script type="text/javascript">
        function ProcExitoso1() {
            alert("Se Ha Guardado Exitosamente")
        }
        function Nota() {
            alert("Falta Capturar El Anual De esta Dirección")
        }
    </script>
    <script type="text/javascript">
        function LineasPlaneadas(obj, id) {

            //alert("entro 1")
            var t1 = obj.id.replace('txt' + id, 'txt1');
            var t2 = obj.id.replace('txt' + id, 'txt2');
            var t3 = obj.id.replace('txt' + id, 'txt3');
            var t4 = obj.id.replace('txt' + id, 'txt4');
            var t5 = obj.id.replace('txt' + id, 'txt5');
            var t6 = obj.id.replace('txt' + id, 'txt6');
            var t7 = obj.id.replace('txt' + id, 'txt7');
            var t8 = obj.id.replace('txt' + id, 'txt8');
            var t9 = obj.id.replace('txt' + id, 'txt9');
            var t10 = obj.id.replace('txt' + id, 'txt10');
            var tCom = obj.id.replace('txt' + id, 'txtCom');
            var l = obj.id.replace('txt' + id, 'LblTotalP');
            var lR = obj.id.replace('txt' + id, 'LblTotalR');

            var txt1 = document.getElementById(t1).value;
            var txt2 = document.getElementById(t2).value;
            var txt3 = document.getElementById(t3).value;
            var txt4 = document.getElementById(t4).value;
            var txt5 = document.getElementById(t5).value;
            var txt6 = document.getElementById(t6).value;
            var txt7 = document.getElementById(t7).value;
            var txt8 = document.getElementById(t8).value;
            var txt9 = document.getElementById(t9).value;
            var txt10 = document.getElementById(t10).value;
            var txtCom = document.getElementById(tCom).value;

            var ContColumnasGrid = document.getElementById('<%= HiddenField1.ClientID %>'); //Guarda el valor de la variable cont que se definio en visual basic (que serian las columnas del grid que se encuentran habilitadas)

            if (ContColumnasGrid.value == 5) {
                // Suma en porcentaje del total Programado
                document.getElementById(l).innerHTML = parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5);
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10);


            }
            else {
                var t11 = obj.id.replace('txt' + id, 'txt11');
                var txt11 = document.getElementById(t11).value;
                var t12 = obj.id.replace('txt' + id, 'txt12');
                var txt12 = document.getElementById(t12).value;

                // Suma en porcentaje del total Programado
                document.getElementById(l).innerHTML = parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5) + parseFloat(txt11);
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10) + parseFloat(txt12);



            }

        }

    </script>
    <script type="text/javascript">        //Este script suma lo planeado de las lineas Planeadas
        function SumaPlaneadoP(obj, id) {
            // alert(obj.id);
            var t1 = obj.id.replace('txt' + id, 'txt1');
            var t2 = obj.id.replace('txt' + id, 'txt2');
            var t3 = obj.id.replace('txt' + id, 'txt3');
            var t4 = obj.id.replace('txt' + id, 'txt4');
            var t5 = obj.id.replace('txt' + id, 'txt5');

            var l = obj.id.replace('txt' + id, 'LblTotalP');

            var txt1 = document.getElementById(t1).value;
            var txt2 = document.getElementById(t2).value;
            var txt3 = document.getElementById(t3).value;
            var txt4 = document.getElementById(t4).value;
            var txt5 = document.getElementById(t5).value;


            var ContColumnasGrid = document.getElementById('<%= HiddenField1.ClientID %>'); //Guarda el valor de la variable cont que se definio en visual basic (que serian las columnas del grid que se encuentran habilitadas)

            if (ContColumnasGrid.value == 5) {
                document.getElementById(l).innerHTML = parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5);
                var numero = redondeo2decimales(document.getElementById(l).innerHTML);
                document.getElementById(l).innerHTML = numero
            }
            else {
                var t11 = obj.id.replace('txt' + id, 'txt11');
                var txt11 = document.getElementById(t11).value;

                document.getElementById(l).innerHTML = parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5) + parseFloat(txt11);
                var numero = redondeo2decimales(document.getElementById(l).innerHTML);
                document.getElementById(l).innerHTML = numero

            }
        }
    </script>
    <script type="text/javascript">        //Este script suma lo REal de las lineas Planeadas
        function SumaRealP(obj, id) {
            // alert(obj.id + 'ds');
            var t6 = obj.id.replace('txt' + id, 'txt6');
            var t7 = obj.id.replace('txt' + id, 'txt7');
            var t8 = obj.id.replace('txt' + id, 'txt8');
            var t9 = obj.id.replace('txt' + id, 'txt9');
            var t10 = obj.id.replace('txt' + id, 'txt10');

            var lR = obj.id.replace('txt' + id, 'LblTotalR');
            var hdnId = obj.id.replace('txt' + id, 'hdnTotal');

            var txt6 = document.getElementById(t6).value;
            var txt7 = document.getElementById(t7).value;
            var txt8 = document.getElementById(t8).value;
            var txt9 = document.getElementById(t9).value;
            var txt10 = document.getElementById(t10).value;


            var ContColumnasGrid = document.getElementById('<%= HiddenField1.ClientID %>'); //Guarda el valor de la variable cont que se definio en visual basic (que serian las columnas del grid que se encuentran habilitadas)

            if (ContColumnasGrid.value == 5) {
                // Suma en porcentaje del total Programado
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10);

                var numero = redondeo2decimales(document.getElementById(lR).innerHTML);
                document.getElementById(lR).innerHTML = numero
            }
            else {
                var t12 = obj.id.replace('txt' + id, 'txt12');
                var txt12 = document.getElementById(t12).value;

                // Suma en porcentaje del total Programado
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10) + parseFloat(txt12);
                var numero = redondeo2decimales(document.getElementById(lR).innerHTML);
                document.getElementById(lR).innerHTML = numero

                document.getElementById(hdnId).value = document.getElementById(lR).innerHTML;
            }
        }
    </script>
    <script type="text/javascript">        //Este script calcula el promedio del programa de las lineas Programadas
        function SumaPlaneado(obj, id) {
            // alert(obj.id + 'dasd');
            var t1 = obj.id.replace('txt' + id, 'txt1');
            var t2 = obj.id.replace('txt' + id, 'txt2');
            var t3 = obj.id.replace('txt' + id, 'txt3');
            var t4 = obj.id.replace('txt' + id, 'txt4');
            var t5 = obj.id.replace('txt' + id, 'txt5');

            var l = obj.id.replace('txt' + id, 'LblTotalP');

            var txt1 = document.getElementById(t1).value;
            var txt2 = document.getElementById(t2).value;
            var txt3 = document.getElementById(t3).value;
            var txt4 = document.getElementById(t4).value;
            var txt5 = document.getElementById(t5).value;

            var ContColumnasGrid = document.getElementById('<%= HiddenField1.ClientID %>'); //Guarda el valor de la variable cont que se definio en visual basic (que serian las columnas del grid que se encuentran habilitadas)

            if (ContColumnasGrid.value == 5) {
                document.getElementById(l).innerHTML = (parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5)) / ContColumnasGrid.value;
                if (document.getElementById(l).innerHTML == 0) {
                    document.getElementById(l).innerHTML == 100;
                }
            }
            else {
                var t11 = obj.id.replace('txt' + id, 'txt11');
                var txt11 = document.getElementById(t11).value;
                document.getElementById(l).innerHTML = (parseFloat(txt1) + parseFloat(txt2) + parseFloat(txt3) + parseFloat(txt4) + parseFloat(txt5) + parseFloat(txt11)) / ContColumnasGrid.value;
                if (document.getElementById(l).innerHTML == 0) {
                    document.getElementById(l).innerHTML == 100;
                }

            }

            var Pl = redondeo2decimales(document.getElementById(l).innerHTML);
            document.getElementById(l).innerHTML = Pl
        }
    </script>
    <script type="text/javascript">        //Este script calcula el promedio de lo REalizado y Lo solicitado de las lineas Programadas
        function SumaSolicitado(obj, id) {
            // alert(obj.id);
            var t6 = obj.id.replace('txt' + id, 'txt6');
            var t7 = obj.id.replace('txt' + id, 'txt7');
            var t8 = obj.id.replace('txt' + id, 'txt8');
            var t9 = obj.id.replace('txt' + id, 'txt9');
            var t10 = obj.id.replace('txt' + id, 'txt10');


            var t13 = obj.id.replace('txt' + id, 'txt13');
            var t14 = obj.id.replace('txt' + id, 'txt14');
            var t15 = obj.id.replace('txt' + id, 'txt15');
            var t16 = obj.id.replace('txt' + id, 'txt16');
            var t17 = obj.id.replace('txt' + id, 'txt17');

            var lS = obj.id.replace('txt' + id, 'lblTotalS');
            var lR = obj.id.replace('txt' + id, 'LblTotalR');

            var TotalPorc = obj.id.replace('txt' + id, 'lblTotalPorcentaje');
            var TotalMensual = obj.id.replace('txt' + id, 'lblTotalMensual');

            //Variable del Promedio
            var Porc1 = obj.id.replace('txt' + id, 'lblPor1');
            var Porc2 = obj.id.replace('txt' + id, 'lblPor2');
            var Porc3 = obj.id.replace('txt' + id, 'lblPor3');
            var Porc4 = obj.id.replace('txt' + id, 'lblPor4');
            var Porc5 = obj.id.replace('txt' + id, 'lblPor5');



            //            estas variables son de lo realizado
            var txt6 = document.getElementById(t6).value;
            var txt7 = document.getElementById(t7).value;
            var txt8 = document.getElementById(t8).value;
            var txt9 = document.getElementById(t9).value;
            var txt10 = document.getElementById(t10).value;

            //            estas variables son de lo solicitado
            var txt13 = document.getElementById(t13).value;
            var txt14 = document.getElementById(t14).value;
            var txt15 = document.getElementById(t15).value;
            var txt16 = document.getElementById(t16).value;
            var txt17 = document.getElementById(t17).value;


            ////////////////////////////
            var ContColumnasGrid = document.getElementById('<%= HiddenField1.ClientID %>'); //Guarda el valor de la variable cont que se definio en visual basic (que serian las columnas del grid que se encuentran habilitadas)

            if (ContColumnasGrid.value == 5) {

                //          Suma de lo  Total  Realizado
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10);
                varTotalReal = redondeo2decimales(document.getElementById(lR).innerHTML);
                document.getElementById(lR).innerHTML = varTotalReal

                //          Suma de lo  Total  Solicitado
                document.getElementById(lS).innerHTML = parseFloat(txt13) + parseFloat(txt14) + parseFloat(txt15) + parseFloat(txt16) + parseFloat(txt17);
                varTotalSoli = redondeo2decimales(document.getElementById(lS).innerHTML);
                document.getElementById(lS).innerHTML = varTotalSoli

                //AL IGUAL QUE EN VB SE HACEN LAS VALIDACIONES DE CADA COLUMNA PARA CALCULAR EL PORCENTAJE

                //PRIMERA COLUMNA
                if ((parseFloat(txt6) > 0) && (parseFloat(txt13) > 0)) {
                    document.getElementById(Porc1).innerHTML = (parseFloat(txt6) / parseFloat(txt13)) * 100; //REaliza la operacion de Porcentaje
                    var numero = redondeo2decimales(document.getElementById(Porc1).innerHTML);
                    document.getElementById(Porc1).innerHTML = numero
                }
                else if ((parseFloat(txt6) > 0) && (parseFloat(txt13) == 0)) {
                    document.getElementById(Porc1).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc1).innerHTML = 0;
                }

                //SEGUNDA COLUMNA
                if ((parseFloat(txt7) > 0) && (parseFloat(txt14) > 0)) {
                    document.getElementById(Porc2).innerHTML = (parseFloat(txt7) / parseFloat(txt14)) * 100; //REaliza la operacion de Porcentaje
                    var numero2 = redondeo2decimales(document.getElementById(Porc2).innerHTML);
                    document.getElementById(Porc2).innerHTML = numero2
                }
                else if ((parseFloat(txt7) > 0) && (parseFloat(txt14) == 0)) {
                    document.getElementById(Porc2).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc2).innerHTML = 0;
                }

                //TERCERA COLUMNA
                if ((parseFloat(txt8) > 0) && (parseFloat(txt15) > 0)) {
                    document.getElementById(Porc3).innerHTML = (parseFloat(txt8) / parseFloat(txt15)) * 100; //REaliza la operacion de Porcentaje
                    var numero3 = redondeo2decimales(document.getElementById(Porc3).innerHTML);
                    document.getElementById(Porc3).innerHTML = numero3
                }
                else if ((parseFloat(txt8) > 0) && (parseFloat(txt15) == 0)) {
                    document.getElementById(Porc3).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc3).innerHTML = 0;
                }

                //CUARTA COLUMNA
                if ((parseFloat(txt9) > 0) && (parseFloat(txt16) > 0)) {
                    document.getElementById(Porc4).innerHTML = (parseFloat(txt9) / parseFloat(txt16)) * 100; //REaliza la operacion de Porcentaje
                    var numero4 = redondeo2decimales(document.getElementById(Porc4).innerHTML);
                    document.getElementById(Porc4).innerHTML = numero4
                }
                else if ((parseFloat(txt9) > 0) && (parseFloat(txt16) == 0)) {
                    document.getElementById(Porc4).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc4).innerHTML = 0;
                }

                //QUINTA COLUMNA
                if ((parseFloat(txt10) > 0) && (parseFloat(txt17) > 0)) {
                    document.getElementById(Porc5).innerHTML = (parseFloat(txt10) / parseFloat(txt17)) * 100; //REaliza la operacion de Porcentaje
                    var numero5 = redondeo2decimales(document.getElementById(Porc5).innerHTML);
                    document.getElementById(Porc5).innerHTML = numero5
                }
                else if ((parseFloat(txt10) > 0) && (parseFloat(txt17) == 0)) {
                    document.getElementById(Porc5).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc5).innerHTML = 0;
                }


            }
            else {
                var t12 = obj.id.replace('txt' + id, 'txt12'); //sexta columna de lo Realizado
                var t18 = obj.id.replace('txt' + id, 'txt18'); //sexta columna de lo Solicitado
                var Porc6 = obj.id.replace('txt' + id, 'lblPor6'); //Porciento de la sexta columna
                var txt12 = document.getElementById(t12).value;
                var txt18 = document.getElementById(t18).value;



                //          Suma de lo  Total  Realizado
                document.getElementById(lR).innerHTML = parseFloat(txt6) + parseFloat(txt7) + parseFloat(txt8) + parseFloat(txt9) + parseFloat(txt10) + parseFloat(txt12);
                varTotalReal = redondeo2decimales(document.getElementById(lR).innerHTML);
                document.getElementById(lR).innerHTML = varTotalReal
                //          Suma de lo  Total  Solicitado
                document.getElementById(lS).innerHTML = parseFloat(txt13) + parseFloat(txt14) + parseFloat(txt15) + parseFloat(txt16) + parseFloat(txt17) + parseFloat(txt18);
                varTotalSoli = redondeo2decimales(document.getElementById(lS).innerHTML);
                document.getElementById(lS).innerHTML = varTotalSoli

                //PRIMERA COLUMNA
                if ((parseFloat(txt6) > 0) && (parseFloat(txt13) > 0)) {
                    document.getElementById(Porc1).innerHTML = (parseFloat(txt6) / parseFloat(txt13)) * 100; //REaliza la operacion de Porcentaje
                    var numero = redondeo2decimales(document.getElementById(Porc1).innerHTML);
                    document.getElementById(Porc1).innerHTML = numero
                }
                else if ((parseFloat(txt6) > 0) && (parseFloat(txt13) == 0)) {
                    document.getElementById(Porc1).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc1).innerHTML = 0;
                }

                //SEGUNDA COLUMNA
                if ((parseFloat(txt7) > 0) && (parseFloat(txt14) > 0)) {
                    document.getElementById(Porc2).innerHTML = (parseFloat(txt7) / parseFloat(txt14)) * 100; //REaliza la operacion de Porcentaje
                    var numero2 = redondeo2decimales(document.getElementById(Porc2).innerHTML);
                    document.getElementById(Porc2).innerHTML = numero2
                }
                else if ((parseFloat(txt7) > 0) && (parseFloat(txt14) == 0)) {
                    document.getElementById(Porc2).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc2).innerHTML = 0;
                }

                //TERCERA COLUMNA
                if ((parseFloat(txt8) > 0) && (parseFloat(txt15) > 0)) {
                    document.getElementById(Porc3).innerHTML = (parseFloat(txt8) / parseFloat(txt15)) * 100; //REaliza la operacion de Porcentaje
                    var numero3 = redondeo2decimales(document.getElementById(Porc3).innerHTML);
                    document.getElementById(Porc3).innerHTML = numero3
                }
                else if ((parseFloat(txt8) > 0) && (parseFloat(txt15) == 0)) {
                    document.getElementById(Porc3).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc3).innerHTML = 0;
                }

                //CUARTA COLUMNA
                if ((parseFloat(txt9) > 0) && (parseFloat(txt16) > 0)) {
                    document.getElementById(Porc4).innerHTML = (parseFloat(txt9) / parseFloat(txt16)) * 100; //REaliza la operacion de Porcentaje
                    var numero4 = redondeo2decimales(document.getElementById(Porc4).innerHTML);
                    document.getElementById(Porc4).innerHTML = numero4
                }
                else if ((parseFloat(txt9) > 0) && (parseFloat(txt16) == 0)) {
                    document.getElementById(Porc4).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc4).innerHTML = 0;
                }

                //QUINTA COLUMNA
                if ((parseFloat(txt10) > 0) && (parseFloat(txt17) > 0)) {
                    document.getElementById(Porc5).innerHTML = (parseFloat(txt10) / parseFloat(txt17)) * 100; //REaliza la operacion de Porcentaje
                    var numero5 = redondeo2decimales(document.getElementById(Porc5).innerHTML);
                    document.getElementById(Porc5).innerHTML = numero5
                }
                else if ((parseFloat(txt10) > 0) && (parseFloat(txt17) == 0)) {
                    document.getElementById(Porc5).innerHTML = 100;
                }
                else {
                    document.getElementById(Porc5).innerHTML = 0;
                }
                //SEXTA COLUMNA
                if ((parseFloat(txt12) > 0) && (parseFloat(txt18) > 0)) {
                    document.getElementById(Porc6).innerHTML = (parseFloat(txt12) / parseFloat(txt18)) * 100; //REaliza la operacion de Porcentaje
                    var numero6 = redondeo2decimales(document.getElementById(Porc6).innerHTML);
                    document.getElementById(Porc6).innerHTML = numero6
                }
                else if ((parseFloat(txt12) > 0) && (parseFloat(txt18) == 0)) {
                    document.getElementById(Porc6).innerHTML = 100;
                }

                else {
                    document.getElementById(Porc6).innerHTML = 0;

                }


            }

            /////////////////////////////////////////////////////////     
            //          Porcentaje Total (Realizado/solicitado)

            if ((document.getElementById(lR).innerHTML == 0) && (document.getElementById(lS).innerHTML == 0) && (document.getElementById(TotalMensual).innerHTML == 0)) {
                document.getElementById(TotalPorc).innerHTML = 0;   //Si el acumulado de lo realizado y lo solicitado Es 0 asi como su Total del mes, entonces Su Total de Porcentaje será 0
            }
            else {
                document.getElementById(TotalPorc).innerHTML = 100;  //Si el acumulado de lo realizado y lo solicitado Es 0 pero su Total del mes no, entonces Su Total de Porcentaje será 100
            }
            if ((document.getElementById(lR).innerHTML > 0) && (document.getElementById(lS).innerHTML == 0)) {       //Si el acumulado de lo realizado es mayor que 0 y lo solicitado Es 0 entonces Su Total de Porcentaje será 100
                document.getElementById(TotalPorc).innerHTML = 100;
            }

            if ((document.getElementById(lR).innerHTML > 0) && (document.getElementById(lS).innerHTML > 0)) {
                document.getElementById(TotalPorc).innerHTML = (document.getElementById(lR).innerHTML / document.getElementById(lS).innerHTML) * 100; //REaliza la operacion de Porcentaje
            }


            var TotalPor = redondeo2decimales(document.getElementById(TotalPorc).innerHTML);
            document.getElementById(TotalPorc).innerHTML = TotalPor

        }
    </script>
    <script type="text/javascript">
        function ValidarCadenaExpReg() {
            cadena = "^[A-Z]|[a-z]";
            re = new RegExp(cadena);

            if (document.getElementById("t").value.match(re))
                alert("Aceptado");
            else
                alert("Rechazado");
        }
    </script>
    <link href="Css/BorderTable.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="text-align: left; margin: 0px 0px 20px 0px">
        <asp:Label ID="Label23" Width="250px" ForeColor="Black" runat="server" Font-Size="17"
            Text="Captura De Información"></asp:Label>
        <asp:Label ID="LabelMes" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
        <asp:Label ID="LblAño" ForeColor="Black" runat="server" Text="Label" Font-Size="17">  </asp:Label>
    </div>
    <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="lblSecretaria" runat="server" Text="Label" Font-Size="25">   </asp:Label><br />
        <asp:Label ID="LblArea" runat="server" Text="Label" Font-Size="17">   </asp:Label><br />
    </div>
    <asp:ScriptManager ID="Script1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div style="text-align: center; margin-bottom: 30px">
        <asp:Label ID="Mensaje" runat="server" ForeColor="Red" Font-Size="Medium"> * Campos Obligatorios</asp:Label>
    </div>

    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1"
        Width="1030px">
        <asp:TabPanel runat="server" HeaderText="Líneas Programadas" ID="TabPanel1">
            <ContentTemplate>
                <div style="margin: 40px 0px 30px 0px">
                    <asp:UpdatePanel ID="PanelCheck" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div style="text-align: center; margin-bottom: 30px">
                                <asp:CheckBox runat="server" ID="chkCom0" Text="Estrategia,línea o sublínea que tiene valor cero en su corte mensual"
                                    Font-Bold="True" AutoPostBack="True" /><br />
                                <asp:CheckBox runat="server" ID="chkCom" Text="Mostrar Área De Comentarios" Font-Bold="True"
                                    AutoPostBack="True" CausesValidation="True" /><br />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="updGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" RowStyle-BorderStyle="Solid"
                            Style="margin-right: 0px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td colspan="3">Información </td>
                                                <td style="width: 80px;" rowspan="2">Semana
                                                    <br />
                                                    Tipo </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70px;">ID </td>
                                                <td style="width: 440px;">Estrategia, Línea y SubLínea </td>
                                                <td style="width: 120px;">Unidad de Medida </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label></td>
                                                <td style="width: 311px;">
                                                    <asp:Label ID="lblEstrategia" runat="server" Text='<%# Eval("Descr") %>'></asp:Label></td>
                                                <td style="width: 90px; text-align: center;">
                                                    <asp:Label ID="lblUnidadMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>'></asp:Label></td>
                                                <td align="center" style="width: 60px;">
                                                    <asp:Label ID="Label6" runat="server" Text="P" Style="display: inline-block; height: 40px;"></asp:Label><br />
                                                    <asp:Label ID="Label7" runat="server" Text="R" Style="display: inline-block; height: 30px;"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt1" onkeyup="javascript:SumaPlaneadoP(this, '1');" runat="server"
                                            Text='<%# Eval("Semana1") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt1" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        <asp:TextBox ID="txt6" onkeyup="javascript:SumaRealP(this, '6');" runat="server"
                                            Text='<%# Eval("Semana1R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt6" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt2" onkeyup="javascript:SumaPlaneadoP(this, '2');" runat="server"
                                            Text='<%# Eval("Semana2") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt2" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt7" onkeyup="javascript:SumaRealP(this, '7');" runat="server"
                                            Text='<%# Eval("Semana2R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt7" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt3" onkeyup="javascript:SumaPlaneadoP(this, '3');" runat="server"
                                            Text='<%# Eval("Semana3") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt3" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt8" onkeyup="javascript:SumaRealP(this, '8');" runat="server"
                                            Text='<%# Eval("Semana3R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt8" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt4" onkeyup="javascript:SumaPlaneadoP(this, '4');" runat="server"
                                            Text='<%# Eval("Semana4") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt4" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt9" onkeyup="javascript:SumaRealP(this, '9');" runat="server"
                                            Text='<%# Eval("Semana4R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt9" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt5" onkeyup="javascript:SumaPlaneadoP(this, '5');" runat="server"
                                            Text='<%# Eval("Semana5") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt5" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt10" onkeyup="javascript:SumaRealP(this, '10');" runat="server"
                                            Text='<%# Eval("Semana5R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt10" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt11" onkeyup="javascript:SumaPlaneadoP(this,'11');" runat="server"
                                            Text='<%#Eval("Semana6") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt11" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt12" onkeyup="javascript:SumaRealP(this,'12');" runat="server"
                                            Text='<%# Eval("Semana6R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt12" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Acumulado Mensual">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:UpdatePanel ID="ipd" runat="server" UpdateMode="Conditional">
                                            

                                            <ContentTemplate>
                                                

                                                <asp:Label ID="LblTotalP" runat="server" Text='<%# Eval("Acumulado") %>' Width="50"
                                                    Height="40px"></asp:Label><asp:HiddenField ID="hdnTotal" runat="server" />
                                                

                                                <br />
                                                

                                                <asp:Label ID="LblTotalR" runat="server" Text='<%# Eval("AcumuladoR") %>' Width="60"
                                                    Height="30px"></asp:Label>
                                                

                                            </ContentTemplate>
                                            

                                        </asp:UpdatePanel>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Total Mensual">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:UpdatePanel ID="updTM" runat="server" UpdateMode="Conditional">
                                            

                                            <ContentTemplate>
                                                

                                                <table>
                                                    

                                                    <tr>
                                                        

                                                        <td align="center">
                                                            

                                                            <asp:Label ID="lblTotalMensual" runat="server" Text='<%# Eval(Mes) %>' Width="50px"></asp:Label><asp:HiddenField ID="HiddenComP" Value='<%# Eval("ComP") %>' runat="server" />
                                                            

                                                            <asp:HiddenField ID="HiddenComR" Value='<%# Eval("ComR") %>' runat="server" />
                                                            

                                                        </td>
                                                        

                                                    </tr>
                                                    

                                                </table>
                                                

                                            </ContentTemplate>
                                            

                                        </asp:UpdatePanel>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Observaciones" Visible="false">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txtCom" runat="server" TextMode="MultiLine" Width="130px" onkeyup="ValidarCadenaExpReg()"></asp:TextBox><asp:TextBox ID="txtCom2" runat="server" TextMode="MultiLine" Width="130px"></asp:TextBox></ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                            </Columns>
                            

                            <EmptyDataTemplate>
                                

                                <table width="1000px">
                                    

                                    <tr>
                                        

                                        <td align="center">
                                            

                                            <asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label></td>
                                        

                                    </tr>
                                    

                                </table>
                                

                            </EmptyDataTemplate>
                            

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            

                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            

                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            

                            <RowStyle ForeColor="#000066" />
                            

                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            

                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            

                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            

                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            

                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            

                        </asp:GridView>
                        

                    </ContentTemplate>
                    

                </asp:UpdatePanel>
                

                <div style="text-align: center; margin-top: 30px">
                    

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        

                        <ContentTemplate>
                            

                            <a style="margin-right: 250px;" href="javascript:window.history.back();">« Volver atrás</a><asp:Button
                                ID="Button1" runat="server" Text="Guardar" Style="width: 100px" />
                            

                        </ContentTemplate>
                        

                    </asp:UpdatePanel>
                    

                </div>
                
 
            </ContentTemplate>





            
        </asp:TabPanel>
        <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Líneas Por Solicitud">
            <ContentTemplate>
                

                <div style="margin: 40px 0px 30px 0px">
                    

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            

                            <div style="text-align: center; margin-bottom: 30px">
                                

                                <asp:CheckBox runat="server" ID="ChkCorte2" Text="Estrategia,línea o sublínea que tiene valor cero en su corte mensual"
                                    Font-Bold="True" AutoPostBack="True" CausesValidation="True" /><br />
                                

                                <asp:CheckBox runat="server" ID="ChkCom2" Text="Mostrar Área De Comentarios" Font-Bold="True"
                                    AutoPostBack="True" CausesValidation="True" /><br />
                                

                            </div>
                            
 
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                    


                </div>
                

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        

                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Style="margin-right: 0px">
                            

                            <AlternatingRowStyle BackColor="White" />
                            

                            <Columns>
                                

                                <asp:TemplateField>
                                    

                                    <HeaderTemplate>
                                        

                                        <table>
                                            

                                            <tr>
                                                

                                                <td colspan="3">Información </td>
                                                

                                                <td style="width: 100px;" rowspan="2">Semana

                                                    <br />
                                                    

                                                    Tipo </td>
                                                

                                            </tr>
                                            

                                            <tr>
                                                

                                                <td style="width: 80px;">ID </td>
                                                

                                                <td style="width: 440px;">Estrategia, Línea y SubLínea </td>
                                                

                                                <td style="width: 120px;">Unidad de Medida </td>
                                                

                                            </tr>
                                            

                                        </table>
                                        

                                    </HeaderTemplate>
                                    

                                    <ItemTemplate>
                                        

                                        <table>
                                            

                                            <tr>
                                                

                                                <td style="width: 70px;">
                                                    

                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label></td>
                                                

                                                <td style="width: 300px;">
                                                    

                                                    <asp:Label ID="lblEstrategia" runat="server" Text='<%# Eval("Descr") %>'></asp:Label></td>
                                                

                                                <td style="width: 100px; text-align: center;">
                                                    

                                                    <asp:Label ID="lblUnidadMedida" runat="server" Text='<%# Eval("Unidad_Medida") %>'></asp:Label></td>
                                                

                                                <td align="center" style="width: 70px;">
                                                    

                                                    <asp:Label ID="lblP" runat="server" Style="display: inline-block; height: 27px;">Programa</asp:Label><br />
                                                    

                                                    <br />
                                                    

                                                    <br />
                                                    

                                                    <asp:Label ID="Label2" runat="server" Style="display: inline-block; height: 32px;">Realizado:</asp:Label><br />
                                                    

                                                    <asp:Label ID="Label3" runat="server" Style="display: inline-block; height: 8px;">Solicitado:</asp:Label><br />
                                                    

                                                    <br />
                                                    

                                                    <asp:Label ID="Label5" runat="server" Style="display: inline-block; height: 3px;">Porcentaje:</asp:Label></td>
                                                

                                            </tr>
                                            

                                        </table>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt1" onkeyup="javascript:SumaPlaneado(this, '1');" runat="server"
                                            Text='<%# Eval("Semana1") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt1" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt6" onkeyup="javascript:SumaSolicitado(this, '6');" runat="server"
                                            Text='<%# Eval("Semana1R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt6" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt13" onkeyup="javascript:SumaSolicitado(this, '13');" runat="server"
                                            Text='<%# Eval("Semana1S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt13" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor1" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno1" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt2" onkeyup="javascript:SumaPlaneado(this, '2');" runat="server"
                                            Text='<%# Eval("Semana2") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt2" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt7" onkeyup="javascript:SumaSolicitado(this, '7');" runat="server"
                                            Text='<%# Eval("Semana2R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt7" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt14" onkeyup="javascript:SumaSolicitado(this, '14');" runat="server"
                                            Text='<%# Eval("Semana2S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt14" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor2" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno2" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt3" onkeyup="javascript:SumaPlaneado(this, '3');" runat="server"
                                            Text='<%# Eval("Semana3") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt3" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt8" onkeyup="javascript:SumaSolicitado(this, '8');" runat="server"
                                            Text='<%# Eval("Semana3R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt8" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt15" onkeyup="javascript:SumaSolicitado(this, '15');" runat="server"
                                            Text='<%# Eval("Semana3S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt15" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor3" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno3" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt4" onkeyup="javascript:SumaPlaneado(this, '4');" runat="server"
                                            Text='<%# Eval("Semana4") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt4" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt9" onkeyup="javascript:SumaSolicitado(this, '9');" runat="server"
                                            Text='<%# Eval("Semana4R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt9" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt16" onkeyup="javascript:SumaSolicitado(this, '16');" runat="server"
                                            Text='<%# Eval("Semana4S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt16" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor4" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno4" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt5" onkeyup="javascript:SumaPlaneado(this, '5');" runat="server"
                                            Text='<%# Eval("Semana5") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt5" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt10" onkeyup="javascript:SumaSolicitado(this, '10');" runat="server"
                                            Text='<%# Eval("Semana5R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt10" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt17" onkeyup="javascript:SumaSolicitado(this, '17');" runat="server"
                                            Text='<%# Eval("Semana5S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt17" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor5" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno5" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField>
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txt11" onkeyup="javascript:SumaPlaneado(this,'11');" runat="server"
                                            Text='<%#Eval("Semana6") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt11" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <br />
                                        

                                        <asp:TextBox ID="txt12" onkeyup="javascript:SumaSolicitado(this,'12');" runat="server"
                                            Text='<%# Eval("Semana6R") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt12" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:TextBox ID="txt18" onkeyup="javascript:SumaSolicitado(this, '18');" runat="server"
                                            Text='<%# Eval("Semana6S") %>' Width="50" onkeypress="javascript:return solonumeros(event)"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Introducir Valor"
                                                Text="*" ControlToValidate="txt18" Font-Bold="True" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                        

                                        <asp:Label ID="lblPor6" runat="server" Text="0"></asp:Label><asp:Label ID="lblSigno6" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Acumulado Mensual">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:Label ID="LblTotalP" runat="server" Text='<%# Eval("Acumulado") %>' Width="50" Height="23">
                                        </asp:Label><asp:Label ID="lblSigno" runat="server" Width="10" Height="5">%</asp:Label><br />
                                        

                                        <br />
                                        

                                        <br />
                                        

                                        <asp:Label ID="LblTotalR" runat="server" Text='<%# Eval("AcumuladoR") %>' Width="60"
                                            Height="41"></asp:Label><br />
                                        

                                        <asp:Label ID="lblTotalS" runat="server" Text='<%# Eval("AcumuladoS") %>'> </asp:Label><br />
                                        

                                        <br />
                                        

                                        <asp:Label ID="lblTotalPorcentaje" runat="server" Width="50" Height="5" Text="0"></asp:Label><asp:Label ID="Label1" runat="server" Text="%"></asp:Label>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Total Mensual">
                                    

                                    <ItemTemplate>
                                        

                                        <table>
                                            

                                            <tr>
                                                

                                                <td align="center">
                                                    

                                                    <asp:Label ID="lblTotalMensual" runat="server" Text='<%# Eval(Mes) %>' Width="36px"></asp:Label><asp:Label ID="Label4" runat="server" Text='%' Width="10px"></asp:Label><asp:HiddenField ID="Hidden2ComP" Value='<%# Eval("ComP") %>' runat="server" />
                                                    

                                                    <asp:HiddenField ID="Hidden2ComR" Value='<%# Eval("ComR") %>' runat="server" />
                                                    

                                                </td>
                                                

                                            </tr>
                                            

                                        </table>
                                        

                                    </ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                                <asp:TemplateField HeaderText="Observaciones" Visible="false">
                                    

                                    <ItemTemplate>
                                        

                                        <asp:TextBox ID="txtCom" runat="server" TextMode="MultiLine" Width="130px"></asp:TextBox><br />
                                        

                                        <asp:TextBox ID="txtCom2" runat="server" TextMode="MultiLine" Width="130px"></asp:TextBox></ItemTemplate>
                                    

                                </asp:TemplateField>
                                

                            </Columns>
                            

                            <EmptyDataTemplate>
                                

                                <table width="1000px">
                                    

                                    <tr>
                                        

                                        <td align="center">
                                            

                                            <asp:Label ID="lblSD" runat="server" ForeColor="Blue" Text="Sin Datos" Font-Size="Large"></asp:Label></td>
                                        

                                    </tr>
                                    

                                </table>
                                

                            </EmptyDataTemplate>
                            

                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            

                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            

                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            

                            <RowStyle ForeColor="#000066" />
                            

                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            

                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            

                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            

                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            

                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            

                        </asp:GridView>
                        
 
                    </ContentTemplate>
                    
                </asp:UpdatePanel>
                


                <div style="text-align: center; margin-top: 30px">
                    

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <a style="margin-right: 250px;" href="javascript:window.history.back();">« Volver atrás</a>

                            <asp:Button ID="Button2" runat="server" Text="Guardar" />
                        </ContentTemplate>
                        
                    </asp:UpdatePanel>
                    


                </div>
                
 
            </ContentTemplate>





            
        </asp:TabPanel>
    </asp:TabContainer>
    <br />
</asp:Content>
