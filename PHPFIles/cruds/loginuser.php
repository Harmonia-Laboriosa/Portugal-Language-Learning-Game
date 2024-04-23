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
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];

$usernamecheckquery = "SELECT * FROM `player` WHERE username = '". $usernameClean ."';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die("2");

if($usernamecheckresult->num_rows !=1)
{
    echo("3");
    exit();
}
else
{
    $fetchedpassword = mysqli_fetch_assoc($usernamecheckresult)["password"];
    if(password_verify(($password), $fetchedpassword))
    {
        $playerInfo = "SELECT * FROM `player` WHERE username = '". $usernameClean ."';";
        $playerInfoResult = mysqli_query($con, $playerInfo) or die("5");
        $existingPlayerInfo = mysqli_fetch_assoc($playerInfoResult);
        $playerUsername = $existingPlayerInfo["username"];
        $playerScore = $existingPlayerInfo["score"];
        echo($playerUsername . ":" . $playerScore);
    }
    else
    {
        echo("4");
    }
}

$con->close();

//Error codes
//1 - Database connection error
//2 - usernamequery run into an error
//3 - Username not existing or there is more than 1 in the table
//4 - Password was not able to be verified
//5 - PlayerInfo Query Failed

?>