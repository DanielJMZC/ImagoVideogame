[System.Serializable]
public class LoginRequest
{
    public string correo;
    public string encrypted_password;
}

[System.Serializable]
public class LoginResponse
{
    public int user_id;
    public int country_id;
}