<?php
    $tempURL = $_SERVER['HTTP_HOST'].'/php/frontpage.php';

    $cookie_name = "visited";
    $cookie_value = "true";

    $frontpageURL = $_SERVER['HTTP_HOST'].'/php/frontpage.php';

    if(isset($_COOKIE[$cookie_name])) {
        //echo "Cookie named '" . $cookie_name . "' is not set!";
        header('location: https://'.$frontpageURL); 
    } else {
        $cookie_name = "visited";
        $cookie_value = "true";
        setcookie($cookie_name, $cookie_value, time() + (86400 * 30), "/"); // 86400 = 1 day
    }
?>

<!DOCTYPE html>
<html lang="en">
<head>


    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-138931852-1"></script>
    <script>
    window.dataLayer = window.dataLayer || [];
    function gtag(){dataLayer.push(arguments);}
    gtag('js', new Date());

    gtag('config', 'UA-138931852-1');
    </script>

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Cyberslash</title>

    <!-- Font Awesome stylesheet -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">

    <!-- Landing page stylesheet -->
    <link rel="stylesheet" href="landingpage.css">
    <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
    <script>
        (adsbygoogle = window.adsbygoogle || []).push({
            google_ad_client: "ca-pub-5731273266586983",
            enable_page_level_ads: true
        });
    </script>

</head>

<body>
    <div class="topinfo">
        <!-- Game title and elevator pitch -->
        <div id="game-info">
            <a id="title" style="font-size: 8em">CYBERSLASH</a>
            <a id="undertitle" style="font-size: 38px;color: #3A506B;">A game about going through cyber dungeons, while slashing cyber things</a>
        </div>
        
        <div class="frontPageButton">
            <a id="frontPageButton" href="//<?php echo $tempURL; ?>"><span>Continue to main Website</span></a>
        </div>

        <!-- Social media buttons -->
        <div id="social">
            <a id="button" href="https://discordapp.com/invite/hxcJsXu" target="_blank"><span class="fab fa-discord fa-5x"></span></a>
            <a id="button" class="github" href="https://github.com/Coow/cows-hacknslash" target="_blank"><span class="fab fa-github fa-7x"></span></a>
            <a id="button" href="https://docs.google.com/document/d/1efqXgjajaEB8XXEQDPFiBsHdwfwno5MTNYbWqtctqqQ/" target="_blank"><span class="far fa-file-alt fa-5x"></span></a>
        </div>

        <div class="footer" style="color: #1C2541; font-size: 38px;">
            <a>Website and game under construction</a> 
        </div>
    </div>
    <div class="botinfo">
        <div class="about">
            <a id="about" style="font-size: 118px">About</a>
            <a id="aboutText" style="font-size: 38px;color: #3A506B;">CyberSlash is a dungeon crawler, with different skills to level up, puzzles to solve, and bosses to defeat! </a>
        </div>
        <div class="opensource">
            <a id="about" style="font-size: 118px">Open Source</a>
            <a id="aboutText" style="font-size: 38px;color: #3A506B;">The game, and its assets (Website, InGame models, etc.) is free and Open Source for you to view and contribute to.</a>
        </div>
    </div>
</body>
</html>
