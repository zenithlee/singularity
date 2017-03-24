<?php
header('Content-type: application/json');
error_reporting(-1);
ini_set('display_errors', 'On');

include "globals.php";
include "db.php";
/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

$result = "";
$name = SafeGet("name");
$url = SafeGet("url");
$ownerid = SafeGet("ownerid");

$f = SafeGet("f");
if ( $f == "register") {
    $description = SafeGet("description");
    $imageurl = SafeGet("iamgeurl");
    $result .= RegisterSite($name, $url, $ownerid, $description, $imageurl);
}
else
if ( $f == "test") {
    $result .= RegisterSite("My Site", "http://bigfun.co.za/singular", "test@bigfun.co.za");
}
else if ( $f == "dir") {
    $page = SafeGet("p");
    $num = 20;
    $result .= GetList($page, $num);
}
else if ( $f == "search") {
    $page = SafeGet("p");
    $num = 20;
    $result = GetList($page, $num);
}
else {
    $result = "ok";
}

//echo json_encode($result);
echo $result;