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
    </div>
</body>
</html>';