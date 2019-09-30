<?php include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php'; ?>
<!DOCTYPE html>
<html>
    <head>

        <!-- Global site tag (gtag.js) - Google Analytics -->
        <script async src="https://www.googletagmanager.com/gtag/js?id=UA-138931852-1"></script>
        <script>
        window.dataLayer = window.dataLayer || [];
        function gtag(){dataLayer.push(arguments);}
        gtag('js', new Date());

        gtag('config', 'UA-138931852-1');
        </script>
        
        <meta charset="utf-8">
         
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
        <link rel="stylesheet" href="../css/frontpage.css">
        <title>Cyberslash</title>
        <script src="https://sdk.accountkit.com/en_US/sdk.js"></script>

        <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
        <script>
            (adsbygoogle = window.adsbygoogle || []).push({
                google_ad_client: "ca-pub-5731273266586983",
                enable_page_level_ads: true
            });
        </script>


    </head>
    <body>
        <div id="header"> 
            <?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
        </div>
        <div>
        
        </div>
        <div id="blogsection">
        
            <?php
                try {
                    $stmt = $db->query('SELECT postID, postTitle, postDesc, postDate FROM blog_posts ORDER BY postID DESC');
                    while($row = $stmt->fetch()){
                        
                        echo '<div class="blog-post">';
                            echo '<a id="title" href="viewpost.php?id='.$row['postID'].'">'.$row['postTitle'].'</a>';
                            echo '<p>Posted on '.date('jS M Y ', strtotime($row['postDate'])).'</p>';
                            echo '<a href="viewpost.php?id='.$row['postID'].'">'.$row['postDesc'].'</a>';				
                            echo '<a id="read" href="viewpost.php?id='.$row['postID'].'">Read More</a>';				
                        echo '</div>';
                    }
                } catch(PDOException $e) {
                    echo $e->getMessage();
                }
            ?>
        </div>
    </body>
</html>
