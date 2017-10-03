<?php

require 'sql/sql.php';

$dateFrom = date('Y-m-d',strtotime("-1 day"));
$timeFrom = '00:00';
$dateTo = date("Y-m-d");
$timeTo = $timeFrom;
$values = array();


function get_data($od, $do, $sonda_id, $values) {
    $value = [
        $od,
        $do,
        $sonda_id,
        $values
    ];
    return json_encode($value, JSON_FORCE_OBJECT);
}
if (isset($_GET['a']) && $_GET['a'] == 'get_data') {
    $od = $_GET['od']." ".$_GET['odCas'];
    $do = $_GET['do']." ".$_GET['doCas'];
    $sonda_id = $_GET['sonda_id'];
    
    $values = getData($od,$do);
    echo get_data($od, $do, $sonda_id,$values);
    return;
}

if (isset($_GET['csv_submit']) && $_GET['csv_submit'] == 'get_csv') {
    $od = $_GET['od']." ".$_GET['odCas'];
    $do = $_GET['do']." ".$_GET['doCas'];
    $values = getData($od,$do);
    array_to_csv_download($values);
    exit();
}
echo '<html>
    <head>
         <meta http-equiv="Content-Language" content="sk" />
         <meta charset="UTF-8">

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <!-- HERE LOAD JQUERY GRAPH -->
        <script type="text/javascript">
            $(function() {
                
                // here initialize graph
                
                date = [1,2,3];
                values = [1,2,3];
                date1 = new Date();
                date2 = new Date();

                // initialize form handler
                $(".data-form").on("submit", function(e) {
                    e.preventDefault();

                    $.ajax({
                        type: "get",
                        url: "index.php?a=get_data",
                        data: {
                            od : $(this).find("[name=od]").val(),
                            do : $(this).find("[name=do]").val(),
                            sonda_id : $(this).attr("id"),
                            doCas : $(this).find("[name=doCas]").val(),
                            odCas : $(this).find("[name=odCas]").val(),

                        },
                        dataType: "json",
                        success: function(result) {
                            $(".result").html("");
                            console.log(result);
                            date = result[3][0];
                            values  = [];
                            graph_element = "";
                            name = "";
                            if (result[2] == "sonda1") {
                                graph_element="container";
                                name="Sonda 1";
                                values = result[3][1];
                            }
                            else {
                                graph_element="container2";
                                name="Sonda 2";
                                values = result[3][2];
                            }
                            var array = $.map(date, function(value, index) {
                                return [value];
                            });
                            var array2 = $.map(values, function(value, index) {
                                return [parseFloat(value)];
                            });
                            date = array;
                            values = array2;

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
                                        y: 103
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
                                    name: "Hodnota",
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
        <form id="sonda1" class="data-form" style="margin-top: 60px">
            <h2> Sonda 1 </h2>
            <b>OD:</b>
            <input type="time" name="odCas" value="'. $timeFrom .'"  /> 
            <input type="date" name="od" value="' . $dateFrom  . '" /> 
            <b style="margin-left: 50px;">DO:</b>
            <input type="time" name="doCas" value="'. $timeTo .'"  /> 
            <input type="date" name="do" value="' . $dateTo . '" /> 
            <button type="submit" name="a" value="get_data" >Načítaj data</button>
        </form>
        
        <div class="result">
        </div>

        <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        <form id="sonda2" class="data-form" style="margin-top: 60px">
            <h2> Sonda 2 </h2>
            <b>OD:</b>
            <input type="time" name="odCas" value="'. $timeFrom .'" /> 
            <input type="date" name="od" value="' . $dateFrom  . '" /> 
            <b style="margin-left: 50px;">DO:</b>
            <input type="time" name="doCas" value="'.$timeTo.'" /> 
            <input type="date" name="do" value="' . $dateTo . '" /> 
            <button type="submit" name="a" value="get_data" >Načítaj data</button>
        </form>

        <div id="container2" style="min-width: 310px; height: 400px; margin: 0 auto"></div>

        
        <form id="csv_form" style="margin-top: 60px">
            <h2> Export csv súboru </h2>
            <b>OD:</b>
            <input type="time" name="odCas" value="'. $timeFrom .'" /> 
            <input type="date" name="od" value="' . $dateFrom  . '" /> 
            <b style="margin-left: 50px;">DO:</b>
            <input type="time" name="doCas" value="'.$timeTo.'" /> 
            <input type="date" name="do" value="' . $dateTo . '" /> 
            <button type="submit" name="csv_submit" value="get_csv" >Stiahnúť CSV súbor</button>
        </form>

        <script src="https://code.highcharts.com/highcharts.js"></script>
        <script src="https://code.highcharts.com/modules/data.js"></script>
        <script src="https://code.highcharts.com/modules/exporting.js"></script>

        <!-- Additional files for the Highslide popup effect -->
        <script src="https://www.highcharts.com/samples/static/highslide-full.min.js"></script>
        <script src="https://www.highcharts.com/samples/static/highslide.config.js" charset="utf-8"></script>
        <link rel="stylesheet" type="text/css" href="https://www.highcharts.com/samples/static/highslide.css" />
        <link rel="stylesheet" type="text/css" href="css/style.css" />

    </div>
</body>
</html>';