using UnityEngine;
using UnityEngine.SceneManagement;
public class Doors : Interactable
{
    public int teleport = 0;
    public override void Interact()
    {
        if (SceneManager.GetActiveScene().name == "RhythmShowdown")
        {
            PlayerPrefs.SetInt("IndoorTeleport", teleport);
            SceneManager.LoadScene("IndoorHouses");
            
        }
        else if (SceneManager.GetActiveScene().name == "IndoorHouses")
        {
            PlayerPrefs.SetInt("IndoorTeleport", teleport);
            SceneManager.LoadScene("RhythmShowdown");
        }
    }
}
