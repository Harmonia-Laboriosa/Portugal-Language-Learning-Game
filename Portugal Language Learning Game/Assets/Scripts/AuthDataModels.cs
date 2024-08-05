// AuthDataModels.cs

using System;

[Serializable]
public class User
{
    public int id;
    public string username;
    public string email;
    public string first_name;
    public string last_name;
    public string role;
    public int game_score;
}

[Serializable]
public class AuthData
{
    public string accessToken;
    public string refreshToken;
    public User user;
}
