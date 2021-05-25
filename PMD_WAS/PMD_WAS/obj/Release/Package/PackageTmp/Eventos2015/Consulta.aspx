<%@ Page Language="VB" AutoEventWireup="false" Title="Consulta Mapa" Inherits="PMD_WAS.Consulta" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Codebehind="Consulta.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <link href="https://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/estilo_captura.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDPRE4FFH6CS9NnXSQzmd9fPPKD0Jh8GEU"></script>
    <script type="text/javascript">
        var map;
        var cm_openInfowindow;
        var cont = 0;
        var Vmapx = 0;
        var infoWin = new google.maps.InfoWindow()



        function cm_load() {

            var myLatlng = new google.maps.LatLng(25.7555708446777, -100.290703710373);
            var myOptions = {
                zoom: 13,
                center: myLatlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            //map = new google.maps.Map(document.getElementById("map"), myOptions);
            cont = 0;

            //var PasoCoord = [
            var PasoCoord = [new google.maps.LatLng(25.770395, -100.266944), new google.maps.LatLng(25.77039, -100.26707), new google.maps.LatLng(25.770319, -100.268699), new google.maps.LatLng(25.770311, -100.268763), new google.maps.LatLng(25.770303, -100.268827), new google.maps.LatLng(25.769885, -100.272223), new google.maps.LatLng(25.769835, -100.272644), new google.maps.LatLng(25.769785, -100.273122), new google.maps.LatLng(25.769611, -100.274502), new google.maps.LatLng(25.769578, -100.27483), new google.maps.LatLng(25.769557, -100.275026), new google.maps.LatLng(25.769389, -100.276511), new google.maps.LatLng(25.769379, -100.2766), new google.maps.LatLng(25.769255, -100.277571), new google.maps.LatLng(25.769104, -100.278747), new google.maps.LatLng(25.769022, -100.279774), new google.maps.LatLng(25.768849, -100.28084), new google.maps.LatLng(25.768799, -100.281279), new google.maps.LatLng(25.768586, -100.282981), new google.maps.LatLng(25.768076, -100.287114),
            new google.maps.LatLng(25.768044, -100.287408), new google.maps.LatLng(25.767788, -100.289355), new google.maps.LatLng(25.767721, -100.28986), new google.maps.LatLng(25.767578, -100.290912), new google.maps.LatLng(25.767442, -100.292013), new google.maps.LatLng(25.767291, -100.293035), new google.maps.LatLng(25.767354, -100.293234), new google.maps.LatLng(25.767302, -100.293314), new google.maps.LatLng(25.766724, -100.298375), new google.maps.LatLng(25.766511, -100.299976), new google.maps.LatLng(25.766612, -100.300203), new google.maps.LatLng(25.766355, -100.302174), new google.maps.LatLng(25.766258, -100.302465), new google.maps.LatLng(25.765834, -100.30547), new google.maps.LatLng(25.765821, -100.30558), new google.maps.LatLng(25.765483, -100.308304), new google.maps.LatLng(25.765138, -100.31108), new google.maps.LatLng(25.764889, -100.313085), new google.maps.LatLng(25.764601, -100.315407), new google.maps.LatLng(25.764583, -100.315589), new google.maps.LatLng(25.764423, -100.31727), new google.maps.LatLng(25.764292, -100.318637), new google.maps.LatLng(25.76425, -100.319075), new google.maps.LatLng(25.764211, -100.319481),
            new google.maps.LatLng(25.764213, -100.319669), new google.maps.LatLng(25.764191, -100.319914), new google.maps.LatLng(25.763951, -100.321499), new google.maps.LatLng(25.763885, -100.322), new google.maps.LatLng(25.763807, -100.322589), new google.maps.LatLng(25.763668, -100.322544), new google.maps.LatLng(25.76299, -100.32881), new google.maps.LatLng(25.755443, -100.327319), new google.maps.LatLng(25.754743, -100.327181), new google.maps.LatLng(25.74991, -100.326226), new google.maps.LatLng(25.749987, -100.325343), new google.maps.LatLng(25.750255, -100.322138), new google.maps.LatLng(25.750034, -100.322149), new google.maps.LatLng(25.748628, -100.322213), new google.maps.LatLng(25.748335, -100.322226), new google.maps.LatLng(25.748025, -100.322211), new google.maps.LatLng(25.746445, -100.32228), new google.maps.LatLng(25.746446, -100.322194), new google.maps.LatLng(25.746394, -100.3222),
            new google.maps.LatLng(25.746341, -100.322206), new google.maps.LatLng(25.745283, -100.322268), new google.maps.LatLng(25.745156, -100.322151), new google.maps.LatLng(25.745298, -100.321103), new google.maps.LatLng(25.74503, -100.321234), new google.maps.LatLng(25.744947, -100.321274), new google.maps.LatLng(25.744508, -100.32149), new google.maps.LatLng(25.744433, -100.321526),
            new google.maps.LatLng(25.743971, -100.321753), new google.maps.LatLng(25.743881, -100.321797), new google.maps.LatLng(25.743441, -100.322012), new google.maps.LatLng(25.743405, -100.322028), new google.maps.LatLng(25.74287, -100.322262), new google.maps.LatLng(25.742409, -100.322508), new google.maps.LatLng(25.742234, -100.322596), new google.maps.LatLng(25.742177, -100.322626),
            new google.maps.LatLng(25.741758, -100.322812), new google.maps.LatLng(25.74168, -100.322854), new google.maps.LatLng(25.741215, -100.323072), new google.maps.LatLng(25.741154, -100.323086), new google.maps.LatLng(25.740725, -100.323293), new google.maps.LatLng(25.740683, -100.323346), new google.maps.LatLng(25.740002, -100.323649), new google.maps.LatLng(25.739924, -100.323711), new google.maps.LatLng(25.739276, -100.323734), new google.maps.LatLng(25.739207, -100.323788), new google.maps.LatLng(25.739178, -100.323824), new google.maps.LatLng(25.738728, -100.323831), new google.maps.LatLng(25.738541, -100.323832), new google.maps.LatLng(25.738261, -100.323834), new google.maps.LatLng(25.737658, -100.323891), new google.maps.LatLng(25.7376, -100.322967), new google.maps.LatLng(25.737566, -100.322417), new google.maps.LatLng(25.737497, -100.321317), new google.maps.LatLng(25.737471, -100.320892), new google.maps.LatLng(25.737456, -100.320659), new google.maps.LatLng(25.737415, -100.320001), new google.maps.LatLng(25.73731, -100.318312), new google.maps.LatLng(25.736683, -100.318338), new google.maps.LatLng(25.736208, -100.318356),
            new google.maps.LatLng(25.735732, -100.318374), new google.maps.LatLng(25.73526, -100.318348), new google.maps.LatLng(25.73486, -100.317936), new google.maps.LatLng(25.734789, -100.317876), new google.maps.LatLng(25.73468, -100.317795), new google.maps.LatLng(25.734473, -100.317718), new google.maps.LatLng(25.734186, -100.317773), new google.maps.LatLng(25.733975, -100.317832), new google.maps.LatLng(25.733558, -100.31795), new google.maps.LatLng(25.733433, -100.317983), new google.maps.LatLng(25.733308, -100.318016), new google.maps.LatLng(25.733123, -100.318066), new google.maps.LatLng(25.732641, -100.318209), new google.maps.LatLng(25.73216, -100.318352), new google.maps.LatLng(25.732002, -100.318399), new google.maps.LatLng(25.731907, -100.318073), new google.maps.LatLng(25.732093, -100.318013), new google.maps.LatLng(25.732055, -100.317817), new google.maps.LatLng(25.732017, -100.317621), new google.maps.LatLng(25.731946, -100.317367),
            new google.maps.LatLng(25.731658, -100.316296), new google.maps.LatLng(25.7316, -100.316099), new google.maps.LatLng(25.731541, -100.316118), new google.maps.LatLng(25.731365, -100.316176), new google.maps.LatLng(25.730926, -100.31632), new google.maps.LatLng(25.730681, -100.316401), new google.maps.LatLng(25.730491, -100.316474), new google.maps.LatLng(25.730052, -100.316645), new google.maps.LatLng(25.729913, -100.316698), new google.maps.LatLng(25.728961, -100.317067), new google.maps.LatLng(25.728861, -100.316526), new google.maps.LatLng(25.728696, -100.316726), new google.maps.LatLng(25.728516, -100.316876), new google.maps.LatLng(25.728323, -100.316965), new google.maps.LatLng(25.728298, -100.316977), new google.maps.LatLng(25.728111, -100.317026), new google.maps.LatLng(25.727896, -100.317131), new google.maps.LatLng(25.725851, -100.317107), new google.maps.LatLng(25.723807, -100.317084), new google.maps.LatLng(25.722657, -100.317096), new google.maps.LatLng(25.722689, -100.316916),
            new google.maps.LatLng(25.721758, -100.313866), new google.maps.LatLng(25.720596, -100.309782), new google.maps.LatLng(25.720538, -100.309578), new google.maps.LatLng(25.718467, -100.31032), new google.maps.LatLng(25.717553, -100.307955), new google.maps.LatLng(25.717162, -100.306972), new google.maps.LatLng(25.71517, -100.301967), new google.maps.LatLng(25.714871, -100.301236), new google.maps.LatLng(25.714758, -100.30096), new google.maps.LatLng(25.714526, -100.300393), new google.maps.LatLng(25.714497, -100.300323), new google.maps.LatLng(25.71413, -100.299427), new google.maps.LatLng(25.714099, -100.299351), new google.maps.LatLng(25.713887, -100.298836), new google.maps.LatLng(25.713723, -100.298435), new google.maps.LatLng(25.71336, -100.297548), new google.maps.LatLng(25.713329, -100.297473), new google.maps.LatLng(25.71296, -100.296572), new google.maps.LatLng(25.712731, -100.296013), new google.maps.LatLng(25.71179, -100.293713), new google.maps.LatLng(25.711277, -100.29246), new google.maps.LatLng(25.70932, -100.28768),
            new google.maps.LatLng(25.708657, -100.28566), new google.maps.LatLng(25.708054, -100.283824), new google.maps.LatLng(25.708052, -100.281887), new google.maps.LatLng(25.708126, -100.277272), new google.maps.LatLng(25.708101, -100.275395), new google.maps.LatLng(25.708196, -100.272947), new google.maps.LatLng(25.70822, -100.271468), new google.maps.LatLng(25.708236, -100.270476), new google.maps.LatLng(25.708261, -100.26895), new google.maps.LatLng(25.708038, -100.268539), new google.maps.LatLng(25.707783, -100.268294), new google.maps.LatLng(25.707569, -100.268146), new google.maps.LatLng(25.706245, -100.268162), new google.maps.LatLng(25.705683, -100.268168), new google.maps.LatLng(25.703425, -100.268195), new google.maps.LatLng(25.70266, -100.268194), new google.maps.LatLng(25.702578, -100.265013), new google.maps.LatLng(25.702517, -100.264784), new google.maps.LatLng(25.702494, -100.26472), new google.maps.LatLng(25.702397, -100.264636), new google.maps.LatLng(25.702288, -100.264565), new google.maps.LatLng(25.702041, -100.264474), new google.maps.LatLng(25.702006, -100.264451),
            new google.maps.LatLng(25.701921, -100.264396), new google.maps.LatLng(25.701818, -100.2643), new google.maps.LatLng(25.70175, -100.264197), new google.maps.LatLng(25.701676, -100.264069), new google.maps.LatLng(25.701626, -100.263897), new google.maps.LatLng(25.701583, -100.263534), new google.maps.LatLng(25.701571, -100.262682), new google.maps.LatLng(25.70156, -100.261829), new google.maps.LatLng(25.701544, -100.260691), new google.maps.LatLng(25.701582, -100.260278), new google.maps.LatLng(25.701659, -100.260101), new google.maps.LatLng(25.702152, -100.259648), new google.maps.LatLng(25.70261, -100.25938), new google.maps.LatLng(25.703256, -100.259329), new google.maps.LatLng(25.703474, -100.259342), new google.maps.LatLng(25.703697, -100.259301), new google.maps.LatLng(25.703872, -100.259221), new google.maps.LatLng(25.70427, -100.25868), new google.maps.LatLng(25.704345, -100.258578), new google.maps.LatLng(25.704453, -100.258679), new google.maps.LatLng(25.704503, -100.258586), new google.maps.LatLng(25.704553, -100.258494), new google.maps.LatLng(25.70456, -100.258188), new google.maps.LatLng(25.704491, -100.257838),
            new google.maps.LatLng(25.704256, -100.257338), new google.maps.LatLng(25.70414, -100.256883), new google.maps.LatLng(25.704121, -100.256807), new google.maps.LatLng(25.704063, -100.25623), new google.maps.LatLng(25.704159, -100.255864), new google.maps.LatLng(25.704406, -100.255555), new google.maps.LatLng(25.704782, -100.254917), new google.maps.LatLng(25.705049, -100.254679), new google.maps.LatLng(25.705415, -100.254598), new google.maps.LatLng(25.705495, -100.25458), new google.maps.LatLng(25.705864, -100.254553), new google.maps.LatLng(25.706105, -100.254473), new google.maps.LatLng(25.706255, -100.254324), new google.maps.LatLng(25.706302, -100.254041), new google.maps.LatLng(25.706339, -100.253188), new google.maps.LatLng(25.706246, -100.25287), new google.maps.LatLng(25.70599, -100.252608), new google.maps.LatLng(25.705543, -100.252408), new google.maps.LatLng(25.705131, -100.252189), new google.maps.LatLng(25.704992, -100.251873), new google.maps.LatLng(25.704962, -100.25157), new google.maps.LatLng(25.704993, -100.251404), new google.maps.LatLng(25.705074, -100.251175),
            new google.maps.LatLng(25.705271, -100.250745), new google.maps.LatLng(25.705536, -100.250207), new google.maps.LatLng(25.705676, -100.249898), new google.maps.LatLng(25.705736, -100.24943), new google.maps.LatLng(25.705798, -100.248844), new google.maps.LatLng(25.705786, -100.247961), new google.maps.LatLng(25.705739, -100.246926), new google.maps.LatLng(25.705758, -100.246835), new google.maps.LatLng(25.705778, -100.246743), new google.maps.LatLng(25.705798, -100.24665), new google.maps.LatLng(25.706014, -100.246211), new google.maps.LatLng(25.706299, -100.245807), new google.maps.LatLng(25.706676, -100.245501), new google.maps.LatLng(25.706953, -100.245255), new google.maps.LatLng(25.708116, -100.243677), new google.maps.LatLng(25.708265, -100.24321), new google.maps.LatLng(25.708393, -100.242658), new google.maps.LatLng(25.708477, -100.24198), new google.maps.LatLng(25.708481, -100.241461), new google.maps.LatLng(25.708558, -100.241288), new google.maps.LatLng(25.708597, -100.241199), new google.maps.LatLng(25.709188, -100.239254), new google.maps.LatLng(25.710003, -100.235724),
            new google.maps.LatLng(25.710592, -100.233424), new google.maps.LatLng(25.710859, -100.232731), new google.maps.LatLng(25.710921, -100.232346), new google.maps.LatLng(25.71101, -100.231802), new google.maps.LatLng(25.711013, -100.231754), new google.maps.LatLng(25.711014, -100.231731), new google.maps.LatLng(25.711016, -100.231707), new google.maps.LatLng(25.71112, -100.231022), new google.maps.LatLng(25.71114, -100.23089), new google.maps.LatLng(25.71144, -100.228914), new google.maps.LatLng(25.711498, -100.228172), new google.maps.LatLng(25.711514, -100.22796), new google.maps.LatLng(25.711571, -100.227233), new google.maps.LatLng(25.711903, -100.224938), new google.maps.LatLng(25.711977, -100.22435), new google.maps.LatLng(25.712349, -100.221402), new google.maps.LatLng(25.712386, -100.221106), new google.maps.LatLng(25.712239, -100.220662), new google.maps.LatLng(25.7117, -100.219387), new google.maps.LatLng(25.711652, -100.219046), new google.maps.LatLng(25.711698, -100.218772), new google.maps.LatLng(25.711941, -100.218292), new google.maps.LatLng(25.712797, -100.216895),
            new google.maps.LatLng(25.71301, -100.216556), new google.maps.LatLng(25.713768, -100.215372), new google.maps.LatLng(25.714203, -100.214693), new google.maps.LatLng(25.715244, -100.212893), new google.maps.LatLng(25.715263, -100.212812), new google.maps.LatLng(25.715545, -100.211607), new google.maps.LatLng(25.716316, -100.207798), new google.maps.LatLng(25.71708, -100.207506), new google.maps.LatLng(25.717338, -100.207282), new google.maps.LatLng(25.717532, -100.206872), new google.maps.LatLng(25.7175, -100.206378), new google.maps.LatLng(25.717425, -100.205706), new google.maps.LatLng(25.717493, -100.205392), new google.maps.LatLng(25.717752, -100.205168), new google.maps.LatLng(25.718033, -100.204783), new google.maps.LatLng(25.718176, -100.204356), new google.maps.LatLng(25.718121, -100.203951), new google.maps.LatLng(25.718517, -100.203866), new google.maps.LatLng(25.71857, -100.203632), new google.maps.LatLng(25.718803, -100.203595), new google.maps.LatLng(25.71919, -100.203535), new google.maps.LatLng(25.719582, -100.203474), new google.maps.LatLng(25.719992, -100.20341),
            new google.maps.LatLng(25.720385, -100.203348), new google.maps.LatLng(25.720801, -100.203283), new google.maps.LatLng(25.721196, -100.203222), new google.maps.LatLng(25.721245, -100.203481), new google.maps.LatLng(25.721961, -100.202849), new google.maps.LatLng(25.722231, -100.2029), new google.maps.LatLng(25.722539, -100.205288), new google.maps.LatLng(25.722582, -100.205627), new google.maps.LatLng(25.72259, -100.205685), new google.maps.LatLng(25.724098, -100.2056), new google.maps.LatLng(25.725225, -100.205537), new google.maps.LatLng(25.726218, -100.20554), new google.maps.LatLng(25.726598, -100.205541), new google.maps.LatLng(25.727047, -100.205565), new google.maps.LatLng(25.728611, -100.205932), new google.maps.LatLng(25.729442, -100.206127), new google.maps.LatLng(25.729446, -100.206414), new google.maps.LatLng(25.731581, -100.20688), new google.maps.LatLng(25.731737, -100.206905), new google.maps.LatLng(25.731768, -100.206943), new google.maps.LatLng(25.732744, -100.20671),
            new google.maps.LatLng(25.73329, -100.209186), new google.maps.LatLng(25.733365, -100.209313), new google.maps.LatLng(25.733459, -100.209472), new google.maps.LatLng(25.73511, -100.212752), new google.maps.LatLng(25.736021, -100.214563), new google.maps.LatLng(25.737432, -100.217367), new google.maps.LatLng(25.737442, -100.217424), new google.maps.LatLng(25.737677, -100.21882), new google.maps.LatLng(25.737683, -100.218861), new google.maps.LatLng(25.737727, -100.219193), new google.maps.LatLng(25.737736, -100.219237), new google.maps.LatLng(25.737744, -100.219281), new google.maps.LatLng(25.737775, -100.219631), new google.maps.LatLng(25.739463, -100.219414), new google.maps.LatLng(25.739565, -100.219403), new google.maps.LatLng(25.74061, -100.225618), new google.maps.LatLng(25.741805, -100.233637), new google.maps.LatLng(25.741781, -100.233843), new google.maps.LatLng(25.74153, -100.236004), new google.maps.LatLng(25.74151, -100.236178), new google.maps.LatLng(25.742043, -100.235427), new google.maps.LatLng(25.745057, -100.231185), new google.maps.LatLng(25.745805, -100.231013), new google.maps.LatLng(25.747376, -100.232176), new google.maps.LatLng(25.747744, -100.232452),
            new google.maps.LatLng(25.748112, -100.232728), new google.maps.LatLng(25.748449, -100.23298), new google.maps.LatLng(25.748717, -100.233182), new google.maps.LatLng(25.748709, -100.233217), new google.maps.LatLng(25.747736, -100.237587), new google.maps.LatLng(25.747726, -100.237631), new google.maps.LatLng(25.745956, -100.238), new google.maps.LatLng(25.74683, -100.238467), new google.maps.LatLng(25.748807, -100.239604), new google.maps.LatLng(25.749628, -100.240111), new google.maps.LatLng(25.750285, -100.24049), new google.maps.LatLng(25.750853, -100.24098), new google.maps.LatLng(25.752259, -100.242133), new google.maps.LatLng(25.753797, -100.243394), new google.maps.LatLng(25.754123, -100.243654), new google.maps.LatLng(25.754867, -100.244295), new google.maps.LatLng(25.755181, -100.244509), new google.maps.LatLng(25.755355, -100.244656), new google.maps.LatLng(25.755681, -100.244968), new google.maps.LatLng(25.755677, -100.245142), new google.maps.LatLng(25.755717, -100.245381),
            new google.maps.LatLng(25.755896, -100.246461), new google.maps.LatLng(25.755924, -100.246691), new google.maps.LatLng(25.755915, -100.247168), new google.maps.LatLng(25.755942, -100.247502), new google.maps.LatLng(25.755989, -100.247711), new google.maps.LatLng(25.756077, -100.247892), new google.maps.LatLng(25.756107, -100.247935), new google.maps.LatLng(25.756138, -100.247978), new google.maps.LatLng(25.756282, -100.248177), new google.maps.LatLng(25.756482, -100.24849), new google.maps.LatLng(25.756788, -100.248815), new google.maps.LatLng(25.75707, -100.249115), new google.maps.LatLng(25.757331, -100.2494), new google.maps.LatLng(25.757378, -100.24945), new google.maps.LatLng(25.757541, -100.249771), new google.maps.LatLng(25.757707, -100.250099), new google.maps.LatLng(25.757924, -100.250507), new google.maps.LatLng(25.758018, -100.250683), new google.maps.LatLng(25.758111, -100.250859), new google.maps.LatLng(25.758426, -100.251238), new google.maps.LatLng(25.759024, -100.252317), new google.maps.LatLng(25.759124, -100.252422), new google.maps.LatLng(25.759223, -100.252527), new google.maps.LatLng(25.759153, -100.253072), new google.maps.LatLng(25.759374, -100.25338), new google.maps.LatLng(25.759737, -100.254084), new google.maps.LatLng(25.759792, -100.254189), new google.maps.LatLng(25.76021, -100.254999), new google.maps.LatLng(25.760873, -100.256309), new google.maps.LatLng(25.761414, -100.257379), new google.maps.LatLng(25.761538, -100.257623),
            new google.maps.LatLng(25.76166, -100.257864), new google.maps.LatLng(25.762345, -100.259245), new google.maps.LatLng(25.76255, -100.259658), new google.maps.LatLng(25.763165, -100.260898), new google.maps.LatLng(25.763389, -100.260841), new google.maps.LatLng(25.763712, -100.262196), new google.maps.LatLng(25.764072, -100.263703), new google.maps.LatLng(25.763685, -100.263836), new google.maps.LatLng(25.763928, -100.265347), new google.maps.LatLng(25.764214, -100.267124), new google.maps.LatLng(25.764581, -100.267003), new google.maps.LatLng(25.764947, -100.266881), new google.maps.LatLng(25.765312, -100.266761), new google.maps.LatLng(25.765665, -100.266644), new google.maps.LatLng(25.766009, -100.266528), new google.maps.LatLng(25.766244, -100.26631), new google.maps.LatLng(25.767721, -100.266434), new google.maps.LatLng(25.768091, -100.266471), new google.maps.LatLng(25.768496, -100.266513), new google.maps.LatLng(25.768871, -100.266551), new google.maps.LatLng(25.769244, -100.266589), new google.maps.LatLng(25.769618, -100.266627), new google.maps.LatLng(25.770024, -100.266669),
            new google.maps.LatLng(25.770399, -100.266707), new google.maps.LatLng(25.770395, -100.266944)
            ];

            map = new google.maps.Map(document.getElementById('map'), myOptions);

            var flightPlanCoordinates = PasoCoord;


            Polyx = new google.maps.Polygon({
                paths: flightPlanCoordinates,
                strokeColor: "#6666FF",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#3366CC",
                fillOpacity: 0.10
            });
            Polyx.setMap(map);
            // Add a listener for the click event
            google.maps.event.addListener(Polyx, 'click', showArrays);


            //alert("termino mapa");
            /*    GeneraPuntos();*/
        }







        function showArrays(event) {
            // Since this Polygon only has one path, we can call getPath()
            // to return the MVCArray of LatLngs
            var vertices = this.getPath();
            var contentString = "<b>Bermuda Triangle Polygon</b><br />";
            contentString += "Clicked Location: <br />" + event.latLng.lat() + "," + event.latLng.lng() + "<br />";
            // Iterate over the vertices.
            //    for (var i = 0; i < vertices.length; i++) {
            //        var xy = vertices.getAt(i);
            //        contentString += "<br />" + "Coordinate: " + i + "<br />" + xy.lat() + "," + xy.lng();
            //    }

            // Replace our Info Window's content and position
            //alert(contentString);
            infowindow.setContent(contentString);
            infowindow.setPosition(event.latLng);
            //alert(contentString);
            infowindow.open(map);
        }

        function MarcaPunto() {
            var txt1 = document.getElementById('TextLng').value;
            var txt2 = document.getElementById('TextLat').value;
            var txtTx = document.getElementById('TextTitulo').value;




            var cadenxr = '';


            var NombreEvento = '';
            var FechaEvento = '';
            var HrEvento = '';
            var Descr = '';
            var Secre = '';
            var SecreNombr = '';
            var Lugar = '';
            //nuevo
            var alcalde = '';

            var myarray = txtTx.split("#");

            NombreEvento = myarray[0];
            FechaEvento = myarray[1];
            HrEvento = myarray[2];
            Descr = myarray[3];
            Secre = myarray[4];
            Lugar = myarray[5];
            //nuevo
            alcalde = myarray[6];



            //var titulo = Folior;
            var titulo = NombreEvento;
            var textoInfo = "<div id=\"content\">" +
                "<h2>" + NombreEvento + "</h2>" +
                "<div id=\"bodyContent\">" +
                "<p><b>FECHA: </b>" + FechaEvento + "<p><b>HORA: </b>" + HrEvento + " " + "HRS." + "</p>" +
                "<p><b>EVENTO: </b>" + NombreEvento + "<p><b>DESCRIPCION: </b>" + Descr + "</p>" +
                "<p><b>LUGAR: </b>" + Lugar + "</p>" +
                "</div>" +
                "</div>"




            //             //var titulo = Folior;
            //             var titulo = Tipor;
            //             var textoInfo = "<div id=\"content\">" +
            //                    "<h2>" + Tipor + "</h2>" +
            //                    "<div id=\"bodyContent\">" +
            //                     "<p><b>FECHA: </b>" + Origen + "<p><b>HORA: </b>" + Folior + "</p>" +
            //                    "<p>" + SecreNombr + "<p><b>EVENTO: </b>" + Tipor + "<p><b>DESCRIPCION: </b>" + Fechar + "</p>" +
            //                    "</div>" +
            //                    "</div>"



            //             var titulo = Tipor;
            //             var textoInfo = "<div id=\"content\">" +
            //                    "<h2>" + Tipor + "</h2>" +
            //                    "<div id=\"bodyContent\">" +
            //                     "<p>" + Origen + "<p>" + Folior + "</p>" +
            //                    "<p>" + SecreNombr + "<p>" + Tipor + "<p>" + Fechar + "</p>" +
            //                    "</div>" +
            //                    "</div>"


            ////"<b>" + Folior + "<\/b><p><b>" + Secre + "<\/b><p><b>" + Tipor + "<\/b><p>" + Fechar + "<\/b><p>";



            //var image = 'iconos/accident.png';
            //var image = 'iconos/iconblue.png';
            var image = '';
            var lat = txt1;
            var lng = txt2;
            //Otroicon = 1;

            //var Otroicon = 1;


            //if (Secre == '1') { image = 'iconos/iconorange.png'; Otroicon = 0; }  //Cabildo	
            //if (Secre == '0') { image = 'iconos/iconblue.png'; Otroicon = 0; }  //OFICINA EJECUTIVA DEL ALCALDE	Pre	0

            if (alcalde == '0') {

                if (Secre == '303') { image = 'iconos/Punteros_Eventos/Ubic%20Naranja.png'; Otroicon = 0; }  //SECRETARIA DEL AYUNTAMIENTO 
                if (Secre == '304') { image = 'iconos/Punteros_Eventos/Ubic%20Cafe.png'; }  //SECRETARIA DE FINANZAS Y TESORERÍA
                if (Secre == '305') { image = 'iconos/Punteros_Eventos/Ubic%20Blanco.png'; Otroicon = 0; }  //DIRECCIÓN GENERAL DEL D.I.F
                if (Secre == '306') { image = 'iconos/Punteros_Eventos/Ubic%20Amarillo.png'; Otroicon = 0; }  //CONTRALORÍA MUNICIPAL
                if (Secre == '307') { image = 'iconos/Punteros_Eventos/Ubic%20Gris.png'; Otroicon = 0; }  //SECRETARIA OBRAS PÚBLICAS, DESARROLLO URBANO Y MEDIO AMBIENTE
                if (Secre == '309') { image = 'iconos/Punteros_Eventos/Ubic%20Rosa.png'; Otroicon = 0; }  //SECRETARIA DE DESARROLLO HUMANO
                if (Secre == '310') { image = 'iconos/Punteros_Eventos/Ubic%20Verde.png'; Otroicon = 0; }  //SECRETARIA DE SERVICIOS PÚBLICOS
                if (Secre == '312') { image = 'iconos/Punteros_Eventos/Ubic%20Azul.png'; Otroicon = 0; }  //SECRETARIA DE SEGURIDAD
                if (Secre == '316') { image = 'iconos/Punteros_Eventos/Ubic%20Celeste.png'; Otroicon = 0; }  //SECRETARIA PARTICIPACIÓN CIUDADANA
                if (Secre == '317') { image = 'iconos/Punteros_Eventos/Ubic%20Negro.png'; Otroicon = 0; }  //STAFF



            }


            if (alcalde == '1') {
                if (Secre == '303') { image = 'iconos/Punteros_Eventos/V%20Naranja.png'; Otroicon = 0; }  //SECRETARIA DEL AYUNTAMIENTO 
                if (Secre == '304') { image = 'iconos/Punteros_Eventos/V%20Cafe.png'; Otroicon = 0; }  //SECRETARIA DE FINANZAS Y TESORERÍA
                if (Secre == '305') { image = 'iconos/Punteros_Eventos/V%20Blanco.png'; Otroicon = 0; }  //DIRECCIÓN GENERAL DEL D.I.F
                if (Secre == '306') { image = 'iconos/Punteros_Eventos/V%20Amarillo.png'; Otroicon = 0; }  //CONTRALORÍA MUNICIPAL
                if (Secre == '307') { image = 'iconos/Punteros_Eventos/V%20Gris.png'; Otroicon = 0; }  //SECRETARIA OBRAS PÚBLICAS, DESARROLLO URBANO Y MEDIO AMBIENTE
                if (Secre == '309') { image = 'iconos/Punteros_Eventos/V%20Rosa.png'; Otroicon = 0; }  //SECRETARIA DE DESARROLLO HUMANO
                if (Secre == '310') { image = 'iconos/Punteros_Eventos/V%20Verde.png'; Otroicon = 0; }  //SECRETARIA DE SERVICIOS PÚBLICOS
                if (Secre == '312') { image = 'iconos/Punteros_Eventos/V%20Azul.png'; Otroicon = 0; }  //SECRETARIA DE SEGURIDAD
                if (Secre == '316') { image = 'iconos/Punteros_Eventos/V%20Celeste.png'; Otroicon = 0; }  //SECRETARIA PARTICIPACIÓN CIUDADANA
                if (Secre == '317') { image = 'iconos/Punteros_Eventos/V%20Negro.png'; Otroicon = 0; }  //STAFF


            }






            //if (Secre == '301') { image = 'iconos/Punteros/MINIS/CABILDO.png'; Otroicon = 0; }  //Cabildo	
            //if (Secre == '302') { image = 'iconos/Punteros/MINIS/ALCALDE.png'; Otroicon = 0; }  //OFICINA EJECUTIVA DEL ALCALDE	Pre	0
            //if (Secre == '303') { image = 'iconos/Punteros/MINIS/AYUNTAMIENTO.png'; Otroicon = 0; }  //SECRETARIA DEL AYUNTAMIENTO	Ay	0
            //if (Secre == '304') { image = 'iconos/Punteros/MINIS/TESORERIA.png'; Otroicon = 0; }  //SECRETARIA DE FINANZAS Y TESORERÍA	FT	0
            //if (Secre == '305') { image = 'iconos/Punteros/MINIS/DIF.png'; Otroicon = 0; } //DIRECCIÓN GENERAL DEL D.I.F	DIF	1
            //if (Secre == '306') { image = 'iconos/Punteros/MINIS/CONTRALORIA.png'; Otroicon = 0; } //CONTRALORÍA MUNICIPAL	SFP	1
            //if (Secre == '307') { image = 'iconos/Punteros/MINIS/OBRAS.png'; Otroicon = 0; } //SECRETARIA DE OBRAS PUB. E ING. VIAL	DUO	0
            //if (Secre == '309') { image = 'iconos/Punteros/MINIS/DESARROLLO.png'; Otroicon = 0; } //SECRETARIA DE DESARROLLO HUMANO	DS	0
            //if (Secre == '310') { image = 'iconos/Punteros/MINIS/PUBLICOS.png'; Otroicon = 0; } //SECRETARIA DE SERVICIOS PÚBLICOS	SP	1
            //if (Secre == '311') { image = 'iconos/Punteros/MINIS/ADMINISTRACION.png'; Otroicon = 0; } //SECRETARIA DE ADMINISTRACIÓN	Ad	1
            //if (Secre == '312') { image = 'iconos/Punteros/MINIS/SEGURIDAD.png'; Otroicon = 0; } //SECRETARIA DE SEGURIDAD	PT	1
            //if (Secre == '314') { image = 'iconos/Punteros/MINIS/AMBIENTE.png'; Otroicon = 0; } //SRIA. DE DES. URBANO Y MEDIO AMBIENTE
            //if (Secre == '315') { image = 'iconos/Punteros/MINIS/PREVENCION.png'; Otroicon = 0; } //PREVISIÓN SOCIAL	prev	0
            //if (Secre == '390') { image = 'iconos/Punteros/MINIS/INSTITUTOS.png'; Otroicon = 0; } //INSTITUTOS MUNICIPALES	INST	1
            //if (Secre == '901') { image = 'iconos/Punteros/MINIS/DEPORTE.png'; Otroicon = 0; } //INSTITUTO MUNICIPAL DEL DEPORTE NICOLAITA	Depo	1
            //if (Secre == '902') { image = 'iconos/Punteros/MINIS/JUVENTUD.png'; Otroicon = 0; } //INSTITUTO DE LA JUVENTUD DE SAN NICOLAS	Juve	1
            //if (Secre == '903') { image = 'iconos/Punteros/MINIS/MUJER.png'; Otroicon = 0; } //INSTITUTO MUNICIPAL DE LA MUJER DE SAN NICOLÁS	Mujr	1
            //if (Secre == '904') { image = 'iconos/Punteros/MINIS/PLAN.png'; Otroicon = 0; } //INSTITUTO DE PLAN. Y DES. MUNICIPAL SAN NICOLAS	Plan	1
            //if (Secre == '333') { image = 'iconos/Punteros/MINIS/INFORMATICA.png'; Otroicon = 0; }  //PRUEBAS INFORMATICA	PRU	0

            //if (Otroicon == 1) { image = 'iconos/Punteros/MINIS/INFORMATICA.png'; }



            var latlng = new google.maps.LatLng(lat, lng);
            map.setCenter(latlng);
            marcador = new google.maps.Marker({
                position: latlng,
                map: map,
                title: titulo,
                icon: image,
                draggable: false

            });
            cont = cont + 1;
            google.maps.event.addListener(marcador, 'click', function () {
                infoWin.setContent(textoInfo);
                infoWin.open(map, this);
                document.getElementById('TxtFoliox').value = Folior;
                document.getElementById('txtorigen').value = Origen;


            });
            limpiarCampos();
        }


        function limpiarCampos() {
            document.getElementById('TextLng').value = '';
            document.getElementById('TextLat').value = '';
            document.getElementById('TextTitulo').value = '';
        }


        function GeneraPuntos() {
            //var boton = new document.getElementById('BtbAct');
            //boton.click();

            cont = 0;
            cm_load();
            var Vmapx = document.getElementById('txtValMap').value;
            if (Vmapx == 0) {
                document.getElementById('txtValMap').value = 1;
            }
            var ix = 0;
            var cad = "";
            var iniciar = 0;
            var entrox = 0;
            var CadRx = "";
            var cadena = document.getElementById('TextBox3').value;
            var xIdx = "";
            var SubCad = "";
            var xPunto = "";

            for (ix = 0; ix <= cadena.length; ix++) {
                cad = "";
                cad = cadena.substring(ix - 1, ix);

                if (cad == "*" && iniciar == 1) { //inicia Cadena
                    iniciar = 0;
                    //alert(cad); // entra muy bien
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
                    //alert(xIdx);  *111111(x,y)*
                    //alert(xPunto);

                    xPunto = xPunto.replace("(", "")
                    xPunto = xPunto.replace(")", "")

                    var mytool_array = xPunto.split(",");
                    //alert(mytool_array[0] + "-" + mytool_array[1] );
                    var Longx = mytool_array[0];
                    var Latx = mytool_array[1];
                    //alert(Longx);
                    //alert(Latx);
                    //crear Puntos
                    document.getElementById('TextLng').value = Longx;
                    document.getElementById('TextLat').value = Latx;
                    document.getElementById('TextTitulo').value = xIdx;

                    //                                if (xPunto.length == 21) {
                    //                                    alert("yo")
                    //                                       MarcaPunto();
                    //                                }

                    MarcaPunto();

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

            Visibles();
        }


        /*setTimeout('cm_load()', 500);*/


        //    function Visibles() {

        //////        var a = document.myForm.Panel1;
        //////        var b = document.myForm.Panel2;
        //////        a.visible = true;
        //////        b.visible = false;

        //    document.getElementById('Button2').disabled = true

        //       

        //    }






        function Button2_onclick() {

        }

        function Button2_onclick() {

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

        function Button2_onclick() {

        }

    </script>

    <style type="text/css">
        .style16 {
            width: 2508px;
            height: 19px;
        }


        .style19 {
            height: 42px;
            width: 100%;
        }


        .style45 {
            height: 16px;
        }

        .style49 {
            height: 11px;
        }
    </style>
    <div class="container">
        <div class="row">
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Fecha Inicial:</h6>
                <asp:TextBox ID="txt_fechaini" runat="server" onkeypress="javascript:return nocaptura(event)" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="txt_fechaini_CalendarExtender" runat="server"
                    Format="yyyy-MM-dd" PopupPosition="BottomRight" TargetControlID="txt_fechaini"
                    TodaysDateFormat="yyyy-MM-dd">
                </asp:CalendarExtender>
            </div>
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Fecha Final:</h6>
                <asp:TextBox ID="txt_fechafin" runat="server" onkeypress="javascript:return nocaptura(event)" CssClass="form-control"></asp:TextBox>
                <asp:CalendarExtender ID="txt_fechafin_CalendarExtender" runat="server"
                    DaysModeTitleFormat="yyyy-MM-dd" Format="yyyy-MM-dd"
                    TargetControlID="txt_fechafin" TodaysDateFormat="yyyy-MM-dd">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Colonia:</h6>
                <asp:DropDownList ID="drp_colonia" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
            </div>
            <div class="col-12 col-md-6">
                <h6 class="subtitle">Secretaria:</h6>
                <asp:DropDownList ID="drp_secre" runat="server" CssClass="form-control select-basic-simple"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-md-6">
                <input id="Button2" onclick="GeneraPuntos();" type="button" value="Ver Mapa" class="btn btn-secondary" onclick="return Button2_onclick()" onclick="return Button2_onclick()" onclick="return Button2_onclick()" />
            </div>
            <div class="col-12 col-md-6 text-right">
                <asp:Button ID="btn_analizar" runat="server" Text="Analizar" CssClass="btn btn-secondary" />
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <asp:TextBox ID="txtValMap" runat="server" BorderStyle="None" ForeColor="White" Height="22px"
                    Width="0px" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextBox4" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    Font-Size="Smaller" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextLng0" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextLat0" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextTitulo0" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextBox3" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    Font-Size="Smaller" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextLng" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextLat" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextTitulo" runat="server" Height="16px" Width="0px" BorderStyle="None"
                    ReadOnly="True"></asp:TextBox>
                <asp:Label ID="lbl_total1" runat="server" Font-Size="Smaller" Visible="False" ForeColor="#CC3300">Total de folios:</asp:Label>
                <asp:Label ID="lbl_total" runat="server" Font-Size="Smaller" Visible="False" ForeColor="#CC3300"></asp:Label>

                <asp:Label ID="lbl_nulos" runat="server" Font-Size="Smaller" Visible="False"></asp:Label>
                <asp:Label ID="lblCuantosNulos" runat="server" Font-Size="Smaller" Visible="False" ForeColor="White"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <div id="map" style="width: auto; height: 350px" align="left">
                </div>
            </div>
        </div>
    </div>

    <div id="divusuarioAlta">
        <asp:Label ID="Label3" runat="server" Text="" Width="50px"></asp:Label>
    </div>
    <div id="divnominaAlta">
        <asp:Label ID="Label19" runat="server" Text="" Width="50px"></asp:Label>
    </div>
</asp:Content>

