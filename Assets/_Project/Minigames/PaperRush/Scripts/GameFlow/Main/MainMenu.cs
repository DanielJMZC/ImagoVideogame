using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void click()
    {
        startGame();
    }

    public void exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
