<?php
    include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php';

    $url1 = $_SERVER['HTTP_HOST'];
    $params = explode('.', $url1);

    $wipURL = $_SERVER['HTTP_HOST'].'/php/WIP.php';

    $title = "Cyberslash";

    if($params[0] == 'beta') {
        $title = "Beta - Cyberslash";
    } else {
        header('location: https://'.$wipURL); 
    }
?>

<!DOCTYPE html>
<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<meta charset="utf-8">
    <head>
     
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
        <link rel="stylesheet" href="../css/stats.css">
        <title><?php echo $title ?></title>
        <script src="https://sdk.accountkit.com/en_US/sdk.js"></script>
        <script src="https://d3js.org/d3.v5.min.js"></script>
        <script><?php include $_SERVER['DOCUMENT_ROOT'].'/git_stats/assets/highstock.js'; ?></script>
        <style></style>
    
    </head>
    <body>
    
        <div id="header"> 
            <?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
        </div>
        <script type="text/javascript" src="//cdn.jsdelivr.net/gh/restingcoder/discord-widget@1.1/discord-widget.min.js"></script>
        <script type="text/javascript">
            discordWidget.init({
                serverId: '487647814726320149',
                title: 'CyberSlash Discord',
                join: false,
                joinText: 'Join Server',
                alphabetical: false,
                theme: 'dark',
                hideChannels: true,
                showAllUsers: true,
                allUsersDefaultState: true,
                showNick: true,
                userName: '',
                useCDN: true
            });
            discordWidget.render();
        </script>
        <div class="discord-widget"></div>

        <div id="main">
            <iframe src="https://ghbtns.com/github-btn.html?user=coow&repo=cows-hacknslash&type=star&count=true&size=large" frameborder="0" scrolling="0" width="120px" height="30px"></iframe>
            <iframe src="https://ghbtns.com/github-btn.html?user=coow&repo=cows-hacknslash&type=fork&count=true&size=large" frameborder="0" scrolling="0" width="140px" height="30px"></iframe>

        </div>  
    </body>
</html>
