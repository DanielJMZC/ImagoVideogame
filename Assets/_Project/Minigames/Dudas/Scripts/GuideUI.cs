using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GuideUI : MonoBehaviour
{
    public GameObject canvas;
    public Text dialogText;
    public Image portrait;

    public Sprite sprite;

    private string[] dialogos;
    private int index;

    private NPCBase currentNPC;


    void Start()
    {
        portrait.sprite = sprite;
    }

    public void StartDialog(string[] lines, NPCBase npc)
    {
        dialogos = lines;
        index = 0;
        currentNPC = npc;

        canvas.SetActive(true);
        dialogText.text = dialogos[index];
    }

    void Update()
    {
        if (!canvas.activeSelf) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }

    void NextLine()
    {
        index++;

        if (index < dialogos.Length)
        {
            dialogText.text = dialogos[index];
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        canvas.SetActive(false);

        currentNPC.EndInteraction();
    }
}