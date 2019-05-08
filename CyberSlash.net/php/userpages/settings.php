<?php
    include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php';
    $logoutURL = $_SERVER['HTTP_HOST'].'/php/userpages/logout.php';
    $loginURL = $_SERVER['HTTP_HOST'].'/php/userpages/loginpage.php';
    $adminURL = $_SERVER['HTTP_HOST'].'/php/admin/index.php';


    if(!$user->is_logged_in()){ 
        header('location: https://'.$loginURL);
        exit;
    }

    if($_SESSION['username'] == "cow"){
        header('location: https://'.$loginURL);
    } else {

    }

?>
<!DOCTYPE html>
<html>
<meta charset="utf-8">
    <head>
     
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
        <link rel="stylesheet" href="../css/settings.css">
        <title>Cyberslash</title>
        <script src="https://sdk.accountkit.com/en_US/sdk.js"></script>
    </head>
    <body>
        <div id="header"> 
            <?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
        </div>
        <div id="main">
            <a id="frontPageButton" href="//<?php echo $logoutURL; ?>"><span>Logout</span></a>
        </div>
    </body>
</html>
