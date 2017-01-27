
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


    function array_to_csv_download($array, $filename = "export.csv", $delimiter=",") {
        ob_end_clean();
        $f = fopen('php://memory', 'w');
        $firstline = array("datum", "sonda1", "sonda2"); 
        fputcsv($f, $firstline, $delimiter);

        /* less rows way
        foreach ($array as $line) { 
            fputcsv($f, $line, $delimiter); 
        }
        */

        //less columns way
        for ($i=0;$i<count($array[0]);$i++) {
            $line = array($array[0][$i],$array[1][$i],$array[2][$i]);
            fputcsv($f, $line, $delimiter);
        }


        // reset the file pointer to the start of the file
        fseek($f, 0);
        // tell the browser it's going to be a csv file
        header('Content-Type: application/csv');
        // tell the browser we want to save it instead of displaying it
        header('Content-Disposition: attachment; filename="'.$filename.'";');
        // make php send the generated csv lines to the browser
        //exit();
        fpassthru($f);
    }
?>
