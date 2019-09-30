<?php
//include config
include $_SERVER['DOCUMENT_ROOT'].'/includes/config.php';

$loginURL = $_SERVER['HTTP_HOST'].'/php/userpages/loginpage.php';
$frontpageURL = $_SERVER['HTTP_HOST'].'/php/frontpage.php';

if(!$user->is_logged_in()){ 
	header('location: https://'.$loginURL);
	exit;
}

if($_SESSION['username'] == "cow"){

} else {
	header('location: https://'.$frontpageURL);  
}

//show message from add / edit page
if(isset($_GET['delpost'])){ 

	$stmt = $db->prepare('DELETE FROM blog_posts WHERE postID = :postID') ;
	$stmt->execute(array(':postID' => $_GET['delpost']));

	header('Location: index.php?action=deleted');
	exit;
} 

?>
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
	<title>Admin</title>
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">
	<style>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.css'; ?>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/css/admin/index.css'; ?>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/css/admin/adminmenu.css'; ?>
	</style>
  <script language="JavaScript" type="text/javascript">
  function delpost(id, title)
  {
	  if (confirm("Are you sure you want to delete '" + title + "'"))
	  {
	  	window.location.href = 'index.php?delpost=' + id;
	  }
  }

  </script>
</head>
<body>
	<div id="header"> 
		<?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.html'; ?>
	</div>
	<div id="wrapper">

	<?php include('menu.php');?>

	<?php 
	//show message from add / edit page
	if(isset($_GET['action'])){ 
		echo '<h3>Post '.$_GET['action'].'.</h3>'; 
	} 
	?>

	<table>
	<tr>
		<th>Title</th>
		<th>Date</th>
		<th>Action</th>
	</tr>
	<?php
		try {

			$stmt = $db->query('SELECT postID, postTitle, postDate FROM blog_posts ORDER BY postID DESC');
			while($row = $stmt->fetch()){
				
				echo '<tr>';
				echo '<td>'.$row['postTitle'].'</td>';
				echo '<td>'.date('jS M Y', strtotime($row['postDate'])).'</td>';
				?>

				<td>
					<a href="edit-post.php?id=<?php echo $row['postID'];?>">Edit</a> | 
					<a href="javascript:delpost('<?php echo $row['postID'];?>','<?php echo $row['postTitle'];?>')">Delete</a>
				</td>
				
				<?php 
				echo '</tr>';

			}

		} catch(PDOException $e) {
		    echo $e->getMessage();
		}
	?>
	</table>

	<p id="addPost"><a href='add-post.php'>Add Post</a></p>

</div>

</body>
</html>
