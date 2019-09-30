<?php
//include config
include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php';

$frontpageURL = $_SERVER['HTTP_HOST'].'/php/frontpage.php';

//log user out
$user->logout();
header('location: https://'.$frontpageURL);
?>