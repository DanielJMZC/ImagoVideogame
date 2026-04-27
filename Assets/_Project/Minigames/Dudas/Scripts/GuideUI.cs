using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GuideUI : MonoBehaviour
{
    public GameObject canvas;
    public Text dialogText;
    public Image portrait;

    public Sprite sprite;

    private List<Dialogo> dialogos;
    private int index;

    private NPCBase currentNPC;

    public AudioClip sfx;


    void Start()
    {
        portrait.sprite = sprite;
    }

    /*public void StartDialog(string[] lines, NPCBase npc)
    {
        dialogos = lines;
        index = 0;
        currentNPC = npc;

        canvas.SetActive(true);
        dialogText.text = dialogos[index];
    }*/

    public void StartDialog(List<Dialogo> lines, NPCBase npc)
    {
        dialogos = lines;
        index = 0;
        currentNPC = npc;

        canvas.SetActive(true);
        dialogText.text = dialogos[index].Texto;
    }

    void Update()
    {
        if (!canvas.activeSelf) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }

    /*void NextLine()
    {
        index++;
        SFXManager.Instance.PlaySFX(sfx);

        if (index < dialogos.Length)
        {
            dialogText.text = dialogos[index];
        }
        else
        {
            EndDialog();
        }
    }*/

    void NextLine()
    {
        index++;
        SFXManager.Instance.PlaySFX(sfx);

        if (index < dialogos.Count)
        {
            dialogText.text = dialogos[index].Texto;
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