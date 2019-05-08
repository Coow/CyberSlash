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
if(isset($_GET['deluser'])){ 

	//if user id is 1 ignore
	if($_GET['deluser'] !='1'){

		$stmt = $db->prepare('DELETE FROM blog_members WHERE memberID = :memberID') ;
		$stmt->execute(array(':memberID' => $_GET['deluser']));

		header('Location: users.php?action=deleted');
		exit;

	}
} 

?>
<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Admin - Users</title>
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" integrity="sha384-fnmOCqbTlWIlj8LyTjo7mOUStjsKC4pOpQbqyi7RrhN7udi9RwhKkMHpvLbHG9Sr" crossorigin="anonymous">

  <script language="JavaScript" type="text/javascript">
  function deluser(id, title)
  {
	  if (confirm("Are you sure you want to delete '" + title + "'"))
	  {
	  	window.location.href = 'users.php?deluser=' + id;
	  }
  }
	</script>
		<style>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/php/sections/header.css'; ?>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/css/admin/users.css'; ?>
			<?php include $_SERVER['DOCUMENT_ROOT'].'/css/admin/adminmenu.css'; ?>
	</style>
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
		echo '<h3>User '.$_GET['action'].'.</h3>'; 
	} 
	?>

	<table>
	<tr>
		<th>Username</th>
		<th>Email</th>
		<th>Action</th>
	</tr>
	<?php
		try {

			$stmt = $db->query('SELECT memberID, username, email, isAdmin FROM blog_members ORDER BY username');
			while($row = $stmt->fetch()){
				
				echo '<tr>';
				echo '<td>'.$row['username'].'</td>';
				echo '<td>'.$row['email'].'</td>';
				?>

				<td>
					
					<?php if($row['isAdmin'] != 1){?>
						<a href="edit-user.php?id=<?php echo $row['memberID'];?>">Edit</a> 
						| <a href="javascript:deluser('<?php echo $row['memberID'];?>','<?php echo $row['username'];?>')">Delete</a>
					<?php } else {?>
						<a> User is admin</a>
					<?php } ?>
				</td>
				
				<?php 
				echo '</tr>';

			}

		} catch(PDOException $e) {
		    echo $e->getMessage();
		}
	?>
	</table>

	<p><a href='add-user.php'>Add User</a></p>

</div>

</body>
</html>
