<%@ Page Language="VB" AutoEventWireup="false" Title="Eventos" MasterPageFile="~/MasterPage/MasterNuevaImagen.master" Inherits="PMD_WAS.Eventos_Eventos" Codebehind="Eventos.aspx.vb" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href='../packages/core/main.css' rel='stylesheet' />
    <link href='../packages/daygrid/main.css' rel='stylesheet' />
    <link href='../packages/timegrid/main.css' rel='stylesheet' />
    <link href='../packages/list/main.css' rel='stylesheet' />
    <script src='../packages/core/main.js'></script>
    <script src='../packages/interaction/main.js'></script>
    <script src='../packages/daygrid/main.js'></script>
    <script src='../packages/timegrid/main.js'></script>
    <script src='../packages/list/main.js'></script>
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                locale: 'es',
                plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],
                height: 'parent',
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },
                buttonText: {
                    today: 'Hoy',
                    month: 'Mensual',
                    week: 'Semanal',
                    day: 'Diario',
                    list: 'Lista'
                },
                defaultView: 'dayGridMonth',
                //defaultDate: '2019-08-12',
                navLinks: true, // can click day/week names to navigate views
                editable: true,
                eventLimit: true, // allow "more" link when too many events
            });

            calendar.render();

            var url = '<%= ResolveClientUrl("~/WSEventos.asmx/ObtieneEventos") %>';
            var employe = '<%= Session("Clave_empl") %>';
            var parametros = '{numEmpleado:' + employe + '}';
            $.ajax({
                type: 'POST',
                url: url,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: parametros,
                success: function (data) {
                    console.log(data);
                    var EVENTOS = [];
                    var json = JSON.parse(data.d);
                    for (var i = 0; i < json.length; i++) {
                        var e = {
                            title: json[i].Nombre,
                            start: json[i].Fecha,
                            url: 'https://admin.sanicolas.gob.mx/PlaneacionFinanciera/EventoDetalle.aspx?id=' + json[i].Id
                        };
                        EVENTOS.push(e);
                    }
                    calendar.addEventSource(EVENTOS)
                },
                error: function (xhr) {
                    alert(xhr.responseText);
                }
            });

            //setTimeout(function () {
            //    var events = [
            //        {
            //            title: 'All Day Event',
            //            start: '2019-08-01',
            //        },
            //        {
            //            title: 'Long Event',
            //            start: '2019-08-07',
            //            end: '2019-08-10'
            //        },
            //        {
            //            groupId: 999,
            //            title: 'Repeating Event',
            //            start: '2019-08-09T16:00:00'
            //        },
            //        {
            //            groupId: 999,
            //            title: 'Repeating Event',
            //            start: '2019-08-16T16:00:00'
            //        },
            //        {
            //            title: 'Conference',
            //            start: '2019-08-11',
            //            end: '2019-08-13'
            //        },
            //        {
            //            title: 'Meeting',
            //            start: '2019-08-12T10:30:00',
            //            end: '2019-08-12T12:30:00'
            //        },
            //        {
            //            title: 'Lunch',
            //            start: '2019-08-12T12:00:00'
            //        },
            //        {
            //            title: 'Meeting',
            //            start: '2019-08-12T14:30:00'
            //        },
            //        {
            //            title: 'Happy Hour',
            //            start: '2019-08-12T17:30:00'
            //        },
            //        {
            //            title: 'Dinner',
            //            start: '2019-08-12T20:00:00'
            //        },
            //        {
            //            title: 'Birthday Party',
            //            start: '2019-08-13T07:00:00'
            //        },
            //        {
            //            title: 'Click for Google',
            //            url: 'http://google.com/',
            //            start: '2019-08-28'
            //        }
            //    ]

            //    calendar.addEventSource(events)
            //}, 1000);
        });

    </script>
    <style>
        html, body {
            overflow: hidden; /* don't do scrollbars */
            font-family: Arial, Helvetica Neue, Helvetica, sans-serif;
            font-size: 14px;
        }

        #calendar-container {
            position: relative;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            min-height: 700px;
        }

        .fc-header-toolbar {
            /*
        the calendar will be butting up against the edges,
        but let's scoot in the header's buttons
        */
            padding-top: 1em;
            padding-left: 1em;
            padding-right: 1em;
        }

        .fc-title {
            color: #FFF !important;
        }

        .fc-content {
            padding: 4px;
        }
    </style>

    <asp:UpdatePanel ID="updCalendario" runat="server">
        <ContentTemplate>
            <div id="calendar-container">
                <div id='calendar'></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
