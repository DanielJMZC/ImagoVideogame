using UnityEngine;

public class SignTeleport : Interactable
{
    public GameObject canvasConfirmacion;

    public Transform playerTarget;
    public Transform cameraTarget;

    public PlayerControl player;

    public void Start()
    {
        canvasConfirmacion.SetActive(false);
    }

    public override void Interact()
    {
        interactionLocked = true;
        player.inAction = true;

        canvasConfirmacion.SetActive(true);


        UnityEngine.UI.Button[] buttons = canvasConfirmacion.GetComponentsInChildren<UnityEngine.UI.Button>();

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(ConfirmTeleport);
        buttons[1].onClick.AddListener(CancelTeleport);
    }

    public void ConfirmTeleport()
    {
        player.transform.position = playerTarget.position;

        if (Camera.main != null)
        {
            Camera.main.transform.position = new Vector3(
                cameraTarget.position.x,
                cameraTarget.position.y,
                Camera.main.transform.position.z
            );
        }

        //Debug.Log("Player target: " + playerTarget.name);
        //Debug.Log("Position: " + playerTarget.position);

        CloseUI();
    }

    public void CancelTeleport()
    {
        CloseUI();
    }

    void CloseUI()
    {
        canvasConfirmacion.SetActive(false);
        interactionLocked = false;
        player.inAction = false;
    }
}