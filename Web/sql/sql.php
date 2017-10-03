
<?php
    function getData($timeFrom,$timeTo, $probeId = null, $isCsv = 0){
        // array(array(),array(),array(),array()); [0] time  [1] probe1 values [2] probe2 values [3] prepare for status strings
        $data = array(array(),array(),array(),array());
        $con = pg_connect("host=ams.dnp.fmph.uniba.sk port=5432 dbname=measured_value user=postgres password=kjkskvak")
            or die ("Could not connect to server\n"); 
        $query = "SELECT count(*) FROM measured_value WHERE '".$timeFrom."' <= time AND '".$timeTo."' >= time"; 
        $rs = pg_query($con, $query) or die("Cannot execute query: $query\n");           
        $row = pg_fetch_row($rs);
        
        // change /constant to change number of data in graph
        if ( isCsv == 0){
            $count = round($row[0]/300) + 1;
        }else{
            $count = round($row[0]/3000) + 1;  
        }
        
       

        

        if ($probeId != 2){
            $query = "SELECT distinct on(g_id) id/'".$count."' as g_id, time, value1
            FROM measured_value
            WHERE time between timestamp '".$timeFrom."' and timestamp '".$timeTo."'
            ORDER BY g_id asc, value1;";
            $rs = pg_query($con, $query) or die("Cannot execute query: $query\n");
            while ($row = pg_fetch_row($rs)) {
                array_push($data[0],  $row[1]);
                array_push($data[1],  $row[2]);
            }
        }
        if ($probeId != 1){
            $query = "SELECT distinct on(g_id) id/'".$count."' as g_id, time, value2
            FROM measured_value
            WHERE time between timestamp '".$timeFrom."' and timestamp '".$timeTo."'
            ORDER BY g_id asc, value2;";
            $rs = pg_query($con, $query) or die("Cannot execute query: $query\n");
            while ($row = pg_fetch_row($rs)) {
                array_push($data[0],  $row[1]);
                array_push($data[2],  $row[2]);
            }
        }

        
        

        pg_close($con); 
               
        return $data;
    }


    function array_to_csv_download($array, $filename = "export.csv", $delimiter=",") {
        ob_end_clean();
        $f = fopen('php://memory', 'w');
        $firstline = array("datum probe1", "value probe1"); 
        fputcsv($f, $firstline, $delimiter);

        /* less rows way
        foreach ($array as $line) { 
            fputcsv($f, $line, $delimiter); 
        }
        */

        //less columns way
        $count = count($array[1]);
        for ($i=0;$i<$count;$i++) {
            $line = array($array[0][$i],$array[1][$i]);
            fputcsv($f, $line, $delimiter);
           // $line = array($array[0][$count+$i],$array[2][$i]);
            //fputcsv($f, $line, $delimiter);
        }
        
        $secondProbe = array("", ""); 
        fputcsv($f, $secondProbe, $delimiter);
        $secondProbe = array("datum probe2", "value probe2"); 
        fputcsv($f, $secondProbe, $delimiter);
        
        for ($i=0;$i<$count;$i++) {

           $line = array($array[0][$count+$i],$array[2][$i]);
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
