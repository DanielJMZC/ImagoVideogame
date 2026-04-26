using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("Targets")]
    public Transform targetPlayerPaper;
    public Transform targetCameraPaper;
    public Transform targetPlayerPuestos;
    public Transform targetCameraPuestos;

    [Header("Player")]
    public GameObject player;

    static bool yaInicio = false;

    void Awake()
    {
        if (!yaInicio)
        {
            yaInicio = true;
            PlayerPrefs.SetInt("IndoorTeleport", 0);
        }
    }


    void Start()
    {
        int position = PlayerPrefs.GetInt("IndoorTeleport", 0);

        if (SceneManager.GetActiveScene().name == "IndoorHouses")
        {
            if (position == 0)
            {
                player.transform.position = targetPlayerPaper.position;

                if (Camera.main != null)
                {
                    Camera.main.transform.position = new Vector3(
                        targetCameraPaper.position.x,
                        targetCameraPaper.position.y,
                        Camera.main.transform.position.z
                    );
                }
            }
            else if (position == 1)
            {
                player.transform.position = targetPlayerPuestos.position;

                if (Camera.main != null)
                {
                    Camera.main.transform.position = new Vector3(
                        targetCameraPuestos.position.x,
                        targetCameraPuestos.position.y,
                        Camera.main.transform.position.z
                    );
                }

            }

        }
        else if (SceneManager.GetActiveScene().name == "RhythmShowdown")
        {
            if (position == 0)
            {
                player.transform.position = targetPlayerPaper.position;

                if (Camera.main != null)
                {
                    Camera.main.transform.position = new Vector3(
                        targetCameraPaper.position.x,
                        targetCameraPaper.position.y,
                        Camera.main.transform.position.z
                    );
                }
            }
            else if (position == 1)
            {
                player.transform.position = targetPlayerPuestos.position;

                if (Camera.main != null)
                {
                    Camera.main.transform.position = new Vector3(
                        targetCameraPuestos.position.x,
                        targetCameraPuestos.position.y,
                        Camera.main.transform.position.z
                    );
                }

            }
        }
        
    }
}
