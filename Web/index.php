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
        <link rel="stylesheet" href="css/chartist.css">
        <link rel="stylesheet" href="css/chartist-plugin-tooltip.css">
        <link rel="stylesheet" href="scss/chartist-plugin-tooltip.scss">
        <script src="https://cdn.jsdelivr.net/chartist.js/latest/chartist.min.js"></script>
        <script src="scripts/chartist-plugin-tooltip.js"></script>

        <!-- HERE LOAD JQUERY GRAPH -->
        <script type="text/javascript">
            $(function() {
                
                // here initialize grap
                
            
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
        <h1> TESTOVACIE GRAFY MERANI </h1>
    <div class="ct-chart" id="chart1" style="margin-top: 120px;height: 300px;"></div>
    <div style="margin-bottom: 120px;"></div>
    <div class="ct-chart" id="chart2" style="height: 300px;"></div>
    <script>
        pole1count = [];
        pole1values = [];
        pole2values = [];
        for ($i=1;$i<100;$i++) {
            if ($i%10==0) pole1count.push(($i/10)+".12.2016");
            else pole1count.push(" ");
            pole1values.push(Math.floor((Math.random() * 10) + 60)/100);
            pole2values.push(Math.floor((Math.random() * 10) + 30)/100);
        }
        // Initialize a Line chart in the container with the ID chart1
        new Chartist.Line("#chart1", {
        labels: pole1count,
        series: [pole1values]
        }, {
            plugins: [
                Chartist.plugins.tooltip()
            ]
        });
    </script>
    <script>
        console.log(pole1values);

        // Initialize a Line chart in the container with the ID chart2
        new Chartist.Line("#chart2", {
        labels: pole1count,
        series: [pole2values]
        }, {
            plugins: [
                Chartist.plugins.tooltip()
            ]
        });
    </script>
    </div>
</body>
</html>';