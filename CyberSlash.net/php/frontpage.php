<html>
    <head>
        <style><?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.css'; ?></style>
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
        <link rel="stylesheet" href="../css/frontpage.css">
        <title>Cyberslash</title>
        <script src="https://sdk.accountkit.com/en_US/sdk.js"></script>
        <?php 
            $imageOne = $_SERVER['HTTP_HOST'].'/images/blogs/blogpost.png';
        ?>
    </head>
    <body>
        <div id="header"> 
            <?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
        </div>
        <div id="blogsection">
            <div class="blog-post">
                <div class="heading" >
                    <a id="title">Development challenges...</a><br>
                    <a>by</a>
                    <a>Cow</a><br>
                    <a>5. April 2019</a>
                </div>
                <div class="image">
                    <img src="../images/blogs/vscode.png" style="filter: blur(2px);">
                </div>
            </div>
            <div class="blog-post">
                <div class="heading">
                    <a id="title">Launching the website!</a><br>
                    <a>by</a>
                    <a>Cow</a><br>
                    <a>3. April 2019</a>
                </div> 
                <a href="../php/blogposts/opening-the-website.php"><img src="../images/blogs/blogpost.png"></a>
            </div>
        </div>
    </body>
</html>
