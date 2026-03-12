using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator buttonAnimator;
    public void startGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void click()
    {
        buttonAnimator.Play("Click");
        startGame();
    }
}
