<?php
$dateFrom = date('Y-m-d',strtotime("-7 day"));
$dateTo = date("Y-m-d");
function get_data($od, $do) {
    $value = [
        $od,
        $do,
    ];
    return json_encode($value, JSON_FORCE_OBJECT);
}
if (isset($_GET['a']) && $_GET['a'] == 'get_data') {
    $od = $_GET['od']? : $dateTo;
    $do = $_GET['do']? : $dateTo;
    echo get_data($od, $do);
    return;
}
echo '<html>
    <head>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <!-- HERE LOAD JQUERY GRAPH -->
        <script type="text/javascript">
            $(function() {
                
                // here initialize grap
                
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
                            do : $(this).find("[name=do]").val()
                        },
                        dataType: "json",
                        success: function(result) {
                            $(".result").html("");
                            $.each(result, function(key,value) {
                                $(".result").append("<p><b>" + key + "</b>: " + value + "</p>");
                                if (key==0) {
                                    date1 = value;
                                }
                                else date2 = value;
                            });
                            date1 = new Date(date1);
                            date2 = new Date(date2);
                            date = [];
                            values  = [];
                            while (date1<=date2) {
                                date1 = new Date(date1);
                                result = date1.toISOString().substring(0,10);
                                date.push(result);
                                values.push(Math.floor((Math.random() * 10) + 60)/100);
                                //$(".result").append("<p>" + result + "</p>");
                                date1.setDate( date1.getDate() + 1 );                        
                            }
                            var chart = Highcharts.chart("container", {

                                chart: {
                                    type: "line",
                                    zoomType: "x"
                                },

                                title: {
                                    text: "Meranie {INSERT NAME}"
                                },


                                xAxis: {
                                    //tickInterval: 14 * 24 * 3600 * 1000, // two weeks
                                    tickWidth: 0,
                                    categories: date,
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
                                    data: values,
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
<body>
    <form>
        OD:<br />
        <input type="date" name="od" value="' . $dateFrom  . '" /> <br />
        DO:<br />
        <input type="date" name="do" value="' . $dateTo . '" /> <br />
        <button type="submit" name="a" value="get_data" >Load data</button>
    </form>
    
    <div class="result">
    </div>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>

    <!-- Additional files for the Highslide popup effect -->
    <script src="https://www.highcharts.com/samples/static/highslide-full.min.js"></script>
    <script src="https://www.highcharts.com/samples/static/highslide.config.js" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="https://www.highcharts.com/samples/static/highslide.css" />

    <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
</body>
</html>';