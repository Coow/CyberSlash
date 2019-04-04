<html>
    <head>
        <style><?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.css'; ?></style>
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
        <style><?php include $_SERVER['DOCUMENT_ROOT'].'/css/loginpage.css'; ?></style>
        <title>Cyberslash</title>
        <script src="https://sdk.accountkit.com/en_US/sdk.js"></script>
    </head>
    <body>

    <div id="fb-root"></div>
<script async defer crossorigin="anonymous" src="https://connect.facebook.net/en_GB/sdk.js#xfbml=1&version=v3.2&appId=1213775908772084&autoLogAppEvents=1"></script>
        <div id="header"> 
            <?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
        </div>
        <div class="main">
            <div>Login here</div>
            <div class="fb-login-button" data-size="large" data-button-type="login_with" data-auto-logout-link="false" data-use-continue-as="false"></div>
            <div id="discord-button">
                <a class="fab fa-discord fa-1x" id="discord-logo"></a>
                <a id="discord-button-text" href="#">Log in with Discord</a>
            </div>
            <div>or</div>
            <div id="login-form">
                
                <form action="#">
                    <a>Log in with CyberSlash account</a>
                    <br>
                    <label for="fname">Email</label>
                    <input type="text" name="email" placeholder="example@cyberslash.net">
                    <br>
                    <label for="fname">Password</label>
                    <input type="password" name="password" >
                    <br><br>
                    <input type="submit" value="Log in">
                </form>
            </div>
        </div>
        
    </body>
</html>
