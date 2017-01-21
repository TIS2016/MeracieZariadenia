
<?php
    function getData($timeFrom,$timeTo, $probeId = null){
        $data = array(array(),array(),array(),array());
        $con = pg_connect("host=ams.dnp.fmph.uniba.sk port=5432 dbname=measured_value user=postgres password=kjkskvak")
            or die ("Could not connect to server\n"); 
                   
        $query = "SELECT * FROM measured_value WHERE '".$timeFrom."' <= time AND '".$timeTo."' >= time"; 
        //echo($query);     
        
        $rs = pg_query($con, $query) or die("Cannot execute query: $query\n");
       

        

       
        if (!$probeId){
            while ($row = pg_fetch_row($rs)) {
                //echo "<br>$row[0] $row[1] $row[2] $row[3]\r\n ";
                array_push($data[0],  $row[1]);
                array_push($data[1],  $row[2]);
                array_push($data[2],  $row[3]);
        }
        }else{
            if ($probeId == 1){
                while ($row = pg_fetch_row($rs)) {
                    array_push($data[0],  $row[1]);
                    array_push($data[1],  $row[2]);
                }
            }else{
                while ($row = pg_fetch_row($rs)) {
                    array_push($data[0],  $row[1]);
                    array_push($data[2],  $row[3]);
                }
            }
        }
        

        pg_close($con); 
        return $data;
    }
?>
