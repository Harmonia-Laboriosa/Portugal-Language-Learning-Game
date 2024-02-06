<?php
$con = mysqli_connect('localhost', 'root', 'root', 'course');

if (mysqli_connect_errno()) {
    echo("1");
    exit();
}

$username = $_POST["username"];
$email = $_POST["email"];
$password = $_POST["password"];
$hashed_password = password_hash($password, PASSWORD_DEFAULT);
$appkey = $_POST["appPassword"];
if($appkey!= "thisisfromtheapp!"){
    exit();
}

$usernamecheckquery = "SELECT username FROM `player` WHERE username = '" . $username . "'";
$usernamecheck = mysqli_query($con, $usernamecheckquery) or die("2");

if (mysqli_num_rows($usernamecheck) > 0) {
    echo("3");
    exit();
}

$emailcheckquery = "SELECT email FROM `player` WHERE email = '" . $email . "'";
$emailcheck = mysqli_query($con, $emailcheckquery) or die("4");

if (mysqli_num_rows($emailcheck) > 0) {
    echo("5");
    exit();
}

$insertuserquery = "INSERT INTO `player` (username, email, password) VALUES ('" . $username . "', '" . $email . "', '" . $hashed_password . "')";
mysqli_query($con, $insertuserquery) or die("6");

echo("0");
mysqli_close($con);
?>
