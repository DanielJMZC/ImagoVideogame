using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField inputCorreo;
    public TMP_InputField inputPassword;
    public TMP_Text textoError;


    public void OnLoginButton()
    {
        string correo = inputCorreo.text.Trim();
        string password = inputPassword.text.Trim();


        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
        {
            if (textoError != null)
                textoError.text = "Llena todos los campos";
            return;
        }

        StartCoroutine(Login(correo, password));
    }

    IEnumerator Login(string correo, string password)
    {
        string url = "http://127.0.0.1:5530/users/login/videogame";

        LoginRequest data = new LoginRequest
        {
            correo = correo,
            encrypted_password = password
        };

        string json = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error conexión: " + request.error);

            if (textoError != null)
                textoError.text = "No se pudo conectar al servidor";

            yield break;
        }

        string response = request.downloadHandler.text;

        if (string.IsNullOrEmpty(response))
        {
            if (textoError != null)
                textoError.text = "Respuesta vacía";

            yield break;
        }

        Debug.Log("Response: " + response);


        LoginResponse res = null;

        try
        {
            res = JsonUtility.FromJson<LoginResponse>(response);
        }
        catch
        {
            if (textoError != null)
                textoError.text = "Error al procesar datos";

            yield break;
        }

        if (res == null || res.user_id == 0)
        {
            if (textoError != null)
                textoError.text = "Credenciales incorrectas";

            yield break;
        }

  
        PlayerPrefs.SetInt("user_id", res.user_id);
        PlayerPrefs.Save();

        SceneManager.LoadScene("RhythmShowdown");
    }
}