<?php

include_once "globals.php";

$GLOBALS["DB_host"]='sql15.cpt1.host-h.net';
$GLOBALS["DB_USER"]='bigfueghdt_5';
$GLOBALS["DB_PASS"]='Y4mzERWafW8';
$GLOBALS["DB_database"]='fluxnetwork';

function ConnectDB(){
    $db = mysqli_connect($GLOBALS["DB_host"], $GLOBALS["DB_USER"], $GLOBALS["DB_PASS"]) or die('Could not connect: ' . mysql_error());
    mysqli_select_db($db, $GLOBALS["DB_database"]) or die('Could not select database');
    return $db;
}

function RegisterSite($sitename, $url, $ownerid, $description, $imageurl){    
    $link = ConnectDB();
    $fluxid = UID();
    $fluxid = str_replace("{", "", $fluxid);
    $fluxid = str_replace("}", "", $fluxid);
    $query = "INSERT into sites (`name`,`url`,`ownerid`,`fluxid`, `description`, `imageurl`) values('$sitename', '$url', '$ownerid','$fluxid', '$description', '$imageurl');";
    //echo $query;
    mysqli_query($link,  $query); // Unicode
    return $fluxid;
}

function GetList( $page, $num) {
    $return = "[";
    $link = ConnectDB();    
    $query = "SELECT * from sites;";
    //echo $query;
    $result = mysqli_query($link,  $query); // Unicode    
    
    $num_rows = mysqli_num_rows($result);
    
    if (mysqli_num_rows($result) == 0) {
    echo "No rows found, nothing to print so am exiting";
    exit;
}

    if($num_rows>0){      
            while ($row = mysqli_fetch_assoc($result)) {
                $return .= "{";
                  $return .= '"name":"' . $row["name"] . '",';
                  $return .= '"url":"' . $row["url"] . '",';
                  $return .= '"description":"' . $row["description"] . '",';
                  $return .= '"date":"' . $row["date"] . '"';
                $return .= "},";
          }    
      }
  mysqli_close( $link );
  
  $return = rtrim($return,",");
    
  $return .= "]";
    
    
    return $return;
}

/*
    $backup_file = 'db-backup-'.time().'.sql';

    // get backup
    $mybackup = backup_tables("myhost","mydbuser","mydbpasswd","mydatabase","*");

    // save to file
    $handle = fopen($backup_file,'w+');
    fwrite($handle,$mybackup);
    fclose($handle);
*/

function BackupHighScores(){
    if (!file_exists("../backups")){
        mkdir("../backups");
    }
    $today = date("Y_m_d_h_i_s");
    $backup_file = 'db-backup-'.$today.'.sql';
    $mybackup = backup_tables($GLOBALS["DB_host"],$GLOBALS["DB_USER"],$GLOBALS["DB_PASS"],$GLOBALS["DB_database"],"*");
    $handle = fopen("../backups/".$backup_file,'w+');
    fwrite($handle,$mybackup);
    fclose($handle);
}
function &backup_tables($host, $user, $pass, $name, $tables = '*'){
  $data = "\n/*---------------------------------------------------------------".
          "\n  SQL DB BACKUP ".date("d.m.Y H:i")." ".
          "\n  HOST: {$host}".
          "\n  DATABASE: {$name}".
          "\n  TABLES: {$tables}".
          "\n  ---------------------------------------------------------------*/\n";
  //$link = mysql_connect($host,$user,$pass);
  $link = ConnectDB();
  mysqli_query($link, "SET NAMES `utf8` COLLATE `utf8_general_ci`" ); // Unicode

  if($tables == '*'){ //get all of the tables
    $tables = array();
    $result = mysqli_query($link,"SHOW TABLES");
    while($row = mysqli_fetch_row($result)){
      $tables[] = $row[0];
    }
  }else{
    $tables = is_array($tables) ? $tables : explode(',',$tables);
  }

  foreach($tables as $table){
    $data.= "\n/*---------------------------------------------------------------".
            "\n  TABLE: `{$table}`".
            "\n  ---------------------------------------------------------------*/\n";
    $data.= "DROP TABLE IF EXISTS `{$table}`;\n";
    $res = mysqli_query($link,"SHOW CREATE TABLE `{$table}`");
    $row = mysqli_fetch_row($res);
    $data.= $row[1].";\n";

    $result = mysqli_query($link,"SELECT * FROM `{$table}`");
    $num_rows = mysqli_num_rows($result);

    if($num_rows>0){
      $vals = Array(); $z=0;
      for($i=0; $i<$num_rows; $i++){
        $items = mysqli_fetch_row($result);
        $vals[$z]="(";
        for($j=0; $j<count($items); $j++){
          if (isset($items[$j])) { $vals[$z].= "'".mysqli_real_escape_string($link,$items[$j])."'"; } else { $vals[$z].= "NULL"; }
          if ($j<(count($items)-1)){ $vals[$z].= ","; }
        }
        $vals[$z].= ")"; $z++;
      }
      $data.= "INSERT INTO `{$table}` VALUES ";
      $data .= "  ".implode(";\nINSERT INTO `{$table}` VALUES ", $vals).";\n";
    }
  }
  mysqli_close( $link );
  return $data;
}
?>