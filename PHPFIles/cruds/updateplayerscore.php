<?php

$con = mysqli_connect('localhost', 'root', 'Harmonia@123#', 'course');

if(mysqli_connect_errno())
{
    echo("1");
    exit();
}

$appkey = $_POST["apppassword"];

if($appkey != "thisisfromtheapp")
{
    exit();
}

$username = $_POST["username"];
$score = $_POST["score"];

$usernamecheckquery = "SELECT * FROM `player` WHERE username = '". $username ."';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die("2");

if($usernamecheckresult->num_rows !=1)
{
    echo("3");
    exit();
}

$updateuserquery = "UPDATE `player` SET score = '".$score."' WHERE username = '". $username ."';";
mysqli_query($con, $updateuserquery) or die("4");

echo("0");

$con->close();


//Error codes
//0 - Sucess
//1 - Database connection error
//2 - usernamecheckquery run into an error
//3 - User already exists
//4 - update check query failed

?>