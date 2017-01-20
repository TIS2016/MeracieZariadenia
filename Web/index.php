<?php
$dateFrom = date('Y-m-d',strtotime("-7 day"));
$dateTo = date("Y-m-d");

function get_data($od, $do, $sonda_id) {
    $value = [
        $od,
        $do,
        $sonda_id
    ];
    return json_encode($value, JSON_FORCE_OBJECT);
}
if (isset($_GET['a']) && $_GET['a'] == 'get_data') {
    $od = $_GET['od']? : $dateTo;
    $do = $_GET['do']? : $dateTo;
    $sonda_id = $_GET['sonda_id'];
    echo get_data($od, $do, $sonda_id);
    return;
}
echo '<html>
    <head>
         <meta http-equiv="Content-Language" content="sk" />
         <meta charset="UTF-8">

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <!-- HERE LOAD JQUERY GRAPH -->
        <script type="text/javascript">
            $(function() {
                
                // here initialize grapz
                
                date = [1,2,3];
                values = [1,2,3];
                date1 = new Date();
                date2 = new Date();

                // initialize form handler
                $("form").on("submit", function(e) {
                    e.preventDefault();

                    $.ajax({
                        type: "get",
                        url: "index.php?a=get_data",
                        data: {
                            od : $(this).find("[name=od]").val(),
                            do : $(this).find("[name=do]").val(),
                            sonda_id : $(this).attr("id") 
                        },
                        dataType: "json",
                        success: function(result) {
                            $(".result").html("");
                            $.each(result, function(key,value) {

                                if (key==0) {
                                    date1 = value;
                                }
                                else if (key==1) date2 = value;
                            });

                            date = [];
                            values  = [];
                            graph_element = "";
                            name = "";
                            if (result[2] == "sonda1") {
                                graph_element="container";
                                name="Sonda 1";
                            }
                            else {
                                graph_element="container2";
                                name="Sonda 2";
                            }
                            // naplnanie random datami
                            date1 = new Date(date1);
                            date2 = new Date(date2);

                            while (date1<=date2) {
                                date1 = new Date(date1);
                                result = date1.toISOString().substring(0,10);
                                date.push(result);
                                values.push(Math.floor((Math.random() * 10) + 60)/100);
                                //$(".result").append("<p>" + result + "</p>");
                                date1.setDate( date1.getDate() + 1 );                        
                            }
                            // koniec naplnania random datami
                            var chart = Highcharts.chart(graph_element, {

                                chart: {
                                    type: "line",
                                    zoomType: "x"
                                },

                                title: {
                                    text: "Meranie "+name
                                },


                                xAxis: {
                                    //tickInterval: 14 * 24 * 3600 * 1000, // two weeks
                                    tickWidth: 0,
                                    categories: date, // sem vkladam datumy
                                    gridLineWidth: 1,
                                    labels: {
                                        align: "left",
                                        x: 3,
                                        y: -3
                                    }
                                },

                                yAxis: [{ // left y axis
                                    title: {
                                        text: null
                                    },
                                    labels: {
                                        align: "left",
                                        x: 3,
                                        y: 16,
                                        format: "{value:.,0f}"
                                    },
                                    showFirstLabel: false
                                }, { // right y axis
                                    linkedTo: 0,
                                    gridLineWidth: 0,
                                    opposite: true,
                                    title: {
                                        text: null
                                    },
                                    labels: {
                                        align: "right",
                                        x: -3,
                                        y: 16,
                                        format: "{value:.,0f}"
                                    },
                                    showFirstLabel: false
                                }],

                                legend: {
                                    align: "left",
                                    verticalAlign: "top",
                                    y: 20,
                                    floating: true,
                                    borderWidth: 0
                                },

                                tooltip: {
                                    shared: true,
                                    crosshairs: true
                                },

                                plotOptions: {
                                    series: {
                                        cursor: "pointer",
                                        point: {
                                            events: {

                                            }
                                        },
                                        marker: {
                                            lineWidth: 1
                                        }
                                    }
                                },

                                series: [{
                                    name: "Hodnota1",
                                    data: values, // sem vkladam hodnoty
                                    lineWidth: 4,
                                    marker: {
                                        radius: 4
                                    }
                                }]
                            });

                        },
                        error: function() {
                            console.log("error");
                        }
                    });
                });

            });
        </script>
    </head>
<body style="text-align: center;">
    <div style="margin-top: 30px">
        <h1> Výpis údajov dvoch sond meracieho zariadenia na grafoch </h1>
        <form id="sonda1" style="margin-top: 60px">
            <h2> Sonda 1 </h2>
            <b>OD:</b>
            <input type="time" name="odCas" value="00:00" /> 
            <input type="date" name="od" value="' . $dateFrom  . '" /> 
            <b style="margin-left: 50px;">DO:</b>
            <input type="time" name="doCas" value="00:00" /> 
            <input type="date" name="do" value="' . $dateTo . '" /> 
            <button type="submit" name="a" value="get_data" >Načítaj data</button>
        </form>
        
        <div class="result">
        </div>

        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        <form id="sonda2" style="margin-top: 60px">
            <h2> Sonda 2 </h2>
            <b>OD:</b>
            <input type="time" name="odCas" value="00:00" /> 
            <input type="date" name="od" value="' . $dateFrom  . '" /> 
            <b style="margin-left: 50px;">DO:</b>
            <input type="time" name="doCas" value="00:00" /> 
            <input type="date" name="do" value="' . $dateTo . '" /> 
            <button type="submit" name="a" value="get_data" >Načítaj data</button>
        </form>

        <div id="container2" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        <script src="https://code.highcharts.com/highcharts.js"></script>
        <script src="https://code.highcharts.com/modules/data.js"></script>
        <script src="https://code.highcharts.com/modules/exporting.js"></script>

        <!-- Additional files for the Highslide popup effect -->
        <script src="https://www.highcharts.com/samples/static/highslide-full.min.js"></script>
        <script src="https://www.highcharts.com/samples/static/highslide.config.js" charset="utf-8"></script>
        <link rel="stylesheet" type="text/css" href="https://www.highcharts.com/samples/static/highslide.css" />

    </div>
</body>
</html>';