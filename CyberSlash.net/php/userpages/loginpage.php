<?php
    include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php';

    //$user->logout();
    if( $user->is_logged_in() ){ 
        
        if($_SESSION['username'] == "cow"){
            header('Location: ../admin/index.php');
        } else if(!$user->is_logged_in()){ 
            //header('Location: $loginURL'); 
        } 
        else {
            header('Location: ../frontpage.php');
            //header('Location: ../admin/index.php');      
        }
    } 
?>
<!DOCTYPE html>
<html>
    <head>
         
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
            <div id="login">
                <?php

                    //process login form if submitted
                    if(isset($_POST['submit'])){

                        $username = trim($_POST['username']);
                        $password = trim($_POST['password']);
                        
                        if($user->login($username,$password)){ 

                            //logged in return to index page
                            header('Location: ../frontpage.php');
                            exit;
                        

                        } else {
                            $message = '<div class="error"><a>Wrong username or password</a></div>';
                        }

                    }//end if submit

                    if(isset($message)){ echo $message; }
                ?>

                <form action="" method="post" autocomplete="on">
                    <a>Log in with CyberSlash account</a>
                    <br>
                    <label for="fname">Email</label>
                    <input id="username" type="text" name="username" placeholder="example@cyberslash.net" autocomplete="on">
                    <br>
                    <label for="fname">Password</label>
                    <input type="password" name="password" id="password" autocomplete="on">
                    <br>
                    <label>Or <a href="register_user.php" style="color:#5bc0be;">register</a> a new account</label>
                    <br>                    
                    <input type="submit" name="submit" value="Login">
                </form>
            </div>
        </div>
        
    </body>
</html>
