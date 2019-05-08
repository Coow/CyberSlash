<?php 

$root = realpath($_SERVER["DOCUMENT_ROOT"]);
include "$root/includes/config.php"; 


$stmt = $db->prepare('SELECT postID, postTitle, postCont, postDate FROM blog_posts WHERE postID = :postID');
$stmt->execute(array(':postID' => $_GET['id']));
$row = $stmt->fetch();

//if post does not exists redirect user.
if($row['postID'] == ''){
	header('Location: ./');
	exit;
}

?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Blog - <?php echo $row['postTitle'];?></title>
	 
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
	<style><?php include $_SERVER['DOCUMENT_ROOT']."/css/blog.css"; ?></style>
        
</head>
<body>
	<div id="header"> 
		<?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
    </div>
	<div id="wrapper" style="color:white; margin:0 20%; font-size:20px; line-height:1.2;">

		<?php	
			echo '<div>';
				echo '<h1>'.$row['postTitle'].'</h1>';
				echo '<p>Posted on '.date('jS M Y', strtotime($row['postDate'])).'</p>';
				echo '<hr />';
				echo '<p>'.$row['postCont'].'</p>';				
			echo '</div>';
		?>

	</div>

</body>
</html>