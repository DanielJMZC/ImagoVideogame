using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D.Animation;

public class SkinManager : MonoBehaviour
{
    public SpriteLibrary spriteLibrary;

    public SpriteLibraryAsset player1;
    public SpriteLibraryAsset player2;
    public SpriteLibraryAsset player3;
    public SpriteLibraryAsset player4;
    public SpriteLibraryAsset player5;
    public SpriteLibraryAsset player6;

    //private string apiUrl = "http://127.0.0.1:5530";
    private string apiUrl = "http://10.14.255.43:5530";

    void Start()
    {
        FetchSkinFromAPI(apiUrl);
    }

    public void FetchSkinFromAPI(string url)
    {
        int userID = PlayerPrefs.GetInt("user_id", 0);
        string full_url = url + "/users/" + userID + "/skin";
        StartCoroutine(GetSkin(full_url));
    }

    IEnumerator GetSkin(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.certificateHandler = new ForceAcceptAll();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API Error: " + request.error);
            yield break;
        }

        int skin = int.Parse(request.downloadHandler.text);

        SetSkin(skin);
    }

    public void SetSkin(int skin)
    {
        switch (skin)
        {
            case 1:
                spriteLibrary.spriteLibraryAsset = player1;
                break;

            case 2:
                spriteLibrary.spriteLibraryAsset = player2;
                break;

            case 3:
                spriteLibrary.spriteLibraryAsset = player3;
                break;

            case 4:
                spriteLibrary.spriteLibraryAsset = player4;
                break;

            case 5:
                spriteLibrary.spriteLibraryAsset = player5;
                break;

            case 6:
                spriteLibrary.spriteLibraryAsset = player6;
                break;

            default:
                Debug.LogWarning("Invalid skin id: " + skin);
                break;
        }

        StartCoroutine(ForceRefreshSpriteLibrary());
    }

    IEnumerator ForceRefreshSpriteLibrary()
    {
        spriteLibrary.enabled = false;

        yield return null; // wait 1 frame

        spriteLibrary.enabled = true;
    }
}