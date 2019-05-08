<?php 
	$frontpageURL = $_SERVER['HTTP_HOST'].'/php/frontpage.php';
?>
<h1>Blog</h1>
<ul id='adminmenu'>
	<li><a href='index.php'>Blog</a></li>
	<li><a href='users.php'>Users</a></li>
	<li><a href="//<?php echo $frontpageURL; ?>" target="_blank">View Website</a></li>
	<li><a href='logout.php'>Logout</a></li>
</ul>
<div class='clear'></div>
<hr />